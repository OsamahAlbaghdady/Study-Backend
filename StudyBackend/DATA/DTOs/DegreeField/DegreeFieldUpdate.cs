using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.DegreeFieldForm
{

    public class DegreeFieldForm 
    {
        
        [Required]
        public Guid DegreeId { get; set; }
        
        [Required]
        public Guid FieldId { get; set; }

    }
}
