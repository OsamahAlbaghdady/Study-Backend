using System.ComponentModel.DataAnnotations;

namespace StudyBackend.DATA.DTOs.DegreeField;

public class DegreeFieldMultiUpdate
{

    [Required]
    public Guid Id { get; set; }
    
    public Guid? DegreeId { get; set; }
            
    public Guid? FieldId { get; set; }
            
    public long? Price { get; set; }
            
    public Guid? UniversityId { get; set; }
            
    public DateTime? StartDate { get; set; }
        
    public DateTime? EndDate { get; set; }
}