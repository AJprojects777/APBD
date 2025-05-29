using CW9.Models;
using Microsoft.EntityFrameworkCore;

namespace CW9.Data;

public class ClinicDbContext : DbContext
{
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public ClinicDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var doctor = new Doctor
        {
            IdDoctor = 1,
            FirstName = "Jan",
            LastName = "Kowalski",
            Email = "jan.kowalski@gmail.com"
        };

        var patient = new Patient
        {
            IdPatient = 1,
            FirstName = "Hedgehog",
            LastName = "The great",
            Birthdate = new DateTime(2003, 1, 1)
        };

        var medicament = new Medicament
        {
            IdMedicament = 1,
            Name = "Vodka",
            Description = "Helps survive studies",
            Type = "The only one"
        };

        var prescription = new Prescription
        {
            IdPrescription = 1,
            Date = new DateTime(1999, 1, 1),
            DueDate = new DateTime(2025, 12, 29),
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient
        };

        var prescriptionMedicament = new PrescriptionMedicament
        {
            IdPrescription = prescription.IdPrescription,
            IdMedicament = medicament.IdMedicament,
            Dose = 2,
            Details = "At least 0.5l at the beginning and the end of day"
        };

        modelBuilder.Entity<PrescriptionMedicament>().HasData(prescriptionMedicament);
        modelBuilder.Entity<Prescription>().HasData(prescription);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Doctor>().HasData([doctor]);
    }
}