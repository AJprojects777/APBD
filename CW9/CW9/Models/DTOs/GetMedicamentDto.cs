namespace CW9.Models.DTOs;

public class GetMedicamentDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = null!;
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}