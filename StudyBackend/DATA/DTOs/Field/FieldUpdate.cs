using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.FieldForm
{

    public class FieldForm 
    {
        [Required]
        public string Name { get; set; }
        

        [Required]
        public DateTime StartDate { get; set; }
        
        [Required]
        public DateTime EndDate { get; set; }

    }
}
