using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.DegreeFieldUpdate
{

    public class DegreeFieldUpdate
    {
            
            public Guid? DegreeId { get; set; }
            
            public Guid? FieldId { get; set; }
            
            public long? Price { get; set; }
            
            public Guid? UniversityId { get; set; }
            
            public DateTime? StartDate { get; set; }
        
            public DateTime? EndDate { get; set; }
            
            
    }
}
