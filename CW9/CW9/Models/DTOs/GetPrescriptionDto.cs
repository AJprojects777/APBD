namespace CW9.Models.DTOs;

public class GetPrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }

    public IEnumerable<GetMedicamentDto> Medicaments { get; set; }
    public GetDoctorDto Doctor { get; set; } = null!;
}