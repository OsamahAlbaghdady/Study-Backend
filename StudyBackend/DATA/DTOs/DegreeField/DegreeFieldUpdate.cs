using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.DegreeFieldForm
{

    public class DegreeFieldForm 
    {
        
        [Required]
        public Guid DegreeId { get; set; }
        
        [Required]
        public Guid FieldId { get; set; }
        
        [Required]
        public long? Price { get; set; }
        
        [Required]
        public Guid UniversityId { get; set; }
        
        


    }
}
