using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CW9.Models;

[Table("Medicament")]
public class Medicament
{
    [Key] public int IdMedicament { get; set; }
    [MaxLength(100)] public required string Name { get; set; } = null!;
    [MaxLength(100)] public required string Description { get; set; } = null!;
    [MaxLength(100)] public required string Type { get; set; } = null!;

    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
}