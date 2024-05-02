using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.DegreeFieldUpdate
{

    public class DegreeFieldUpdate
    {
            
            [Required]
            public Guid DegreeId { get; set; }
            
            [Required]
            public Guid FieldId { get; set; }
            
            [Required]
            public long? Price { get; set; }
            
            [Required]
            public Guid UniversityId { get; set; }
            
            [Required]
            public DateTime StartDate { get; set; }
        
            [Required]
            public DateTime EndDate { get; set; }
            
            
    }
}
