namespace CW9.Models.DTOs;

public class GetPatientFullDataDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }

    public IEnumerable<GetPrescriptionDto> Prescriptions { get; set; }
}