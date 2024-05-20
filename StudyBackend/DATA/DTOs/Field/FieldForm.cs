using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.FieldForm
{

    public class FieldForm 
    {
        
        [Required]
        public string Name { get; set; }
        
        public bool? Priority { get; set; } = false;



    }
}
