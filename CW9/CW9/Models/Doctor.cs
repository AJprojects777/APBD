using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW9.Models;

[Table("Doctor")]
public class Doctor
{
    [Key] public int IdDoctor { get; set; }
    public required string FirstName { get; set; } = null!;
    public required string LastName { get; set; } = null!;
    public required string Email { get; set; } = null!;

    public virtual ICollection<Prescription> Prescriptions { get; set; } = null!;
}