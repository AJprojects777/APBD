using System;
using System.Collections.Generic;

interface IHazardNotifier
{
    void NotifyHazard(string message);
}

class OverfillException : Exception
{
    public OverfillException(string message) : base(message) { }
}

abstract class Container
{
    public int containerNumber = 1;
    public string SerialNumber { get; }
    public double MaxLoad { get; }
    public double CurrentLoad { get; protected set; }

    protected Container(string type, double maxLoad)
    {
        SerialNumber = $"KON-{type}-{containerNumber++}";
        MaxLoad = maxLoad;
        CurrentLoad = 0;
    }

    public virtual void Load(double weight)
    {
        if (CurrentLoad + weight > MaxLoad)
            throw new OverfillException($"Overfill in container {SerialNumber}");
        CurrentLoad += weight;
    }

    public virtual void Unload()
    {
        CurrentLoad = 0;
    }
}

class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; }

    public LiquidContainer(double maxLoad, bool isHazardous)
        : base("L", maxLoad)
    {
        IsHazardous = isHazardous;
    }

    public override void Load(double weight)
    {
        double limit = IsHazardous ? MaxLoad * 0.5 : MaxLoad * 0.9;
        if (weight > limit)
            NotifyHazard($"Attempted unsafe load in {SerialNumber}");
        base.Load(weight);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine("[HAZARD] " + message);
    }
}

class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; }

    public GasContainer(double maxLoad, double pressure)
        : base("G", maxLoad)
    {
        Pressure = pressure;
    }

    public override void Unload()
    {
        CurrentLoad *= 0.05;
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine("[HAZARD] " + message);
    }
}

class RefrigeratedContainer : Container
{
    public double Temperature { get; }

    public RefrigeratedContainer(double maxLoad, double temperature)
        : base("C", maxLoad)
    {
        Temperature = temperature;
    }
}

class Ship
{
    public string Name { get; }
    public double MaxWeight { get; }
    public int MaxContainers { get; }
    public List<Container> Containers { get; }

    public Ship(string name, double maxWeight, int maxContainers)
    {
        Name = name;
        MaxWeight = maxWeight;
        MaxContainers = maxContainers;
        Containers = new List<Container>();
    }

    public void LoadContainer(Container container)
    {
        if (Containers.Count >= MaxContainers || GetTotalWeight() + container.MaxLoad > MaxWeight)
            throw new InvalidOperationException("Cannot load container on ship");
        Containers.Add(container);
    }

    public void RemoveContainer(string serialNumber)
    {
        Containers.RemoveAll(c => c.SerialNumber == serialNumber);
    }

    private double GetTotalWeight()
    {
        double weight = 0;
        foreach (var c in Containers) weight += c.MaxLoad;
        return weight;
    }
}

class Program
{
    static void Main()
    {
        Ship ship = new Ship("CargoX", 50000, 10);
        Console.WriteLine($"Statek {ship.Name} utworzony. Maksymalna liczba kontenerów: {ship.MaxContainers}, Maksymalna waga: {ship.MaxWeight} kg.");

        LiquidContainer milkContainer = new LiquidContainer(10000, false);
        GasContainer heliumContainer = new GasContainer(8000, 50);
        RefrigeratedContainer bananaContainer = new RefrigeratedContainer(5000, -10);

        Console.WriteLine($"Utworzono kontenery: {milkContainer.SerialNumber}, {heliumContainer.SerialNumber}, {bananaContainer.SerialNumber}");

        try
        {
            milkContainer.Load(9000); // Powinno się udać
            heliumContainer.Load(8000); // Powinno się udać
            bananaContainer.Load(4500); // Powinno się udać
            Console.WriteLine("Załadowano towary do kontenerów.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Błąd: {e.Message}");
        }

        try
        {
            ship.LoadContainer(milkContainer);
            ship.LoadContainer(heliumContainer);
            ship.LoadContainer(bananaContainer);
            Console.WriteLine("Kontenery załadowane na statek.");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Błąd: {e.Message}");
        }

        Console.WriteLine($"\nStatek {ship.Name} przewozi {ship.Containers.Count} kontenerów:");
        foreach (var container in ship.Containers)
        {
            Console.WriteLine($" - {container.SerialNumber}, Aktualna masa ładunku: {container.CurrentLoad} kg");
        }

        heliumContainer.Unload();
        Console.WriteLine($"Po rozładunku {heliumContainer.SerialNumber} wciąż zawiera {heliumContainer.CurrentLoad} kg gazu.");

        ship.RemoveContainer(milkContainer.SerialNumber);
        Console.WriteLine($"Po usunięciu {milkContainer.SerialNumber} statek przewozi {ship.Containers.Count} kontenerów.");

        try
        {
            bananaContainer.Load(6000);
        }
        catch (OverfillException e)
        {
            Console.WriteLine($"Błąd przeładowania: {e.Message}");
        }

        Console.WriteLine("Testowanie zakończone.");
    }
}
