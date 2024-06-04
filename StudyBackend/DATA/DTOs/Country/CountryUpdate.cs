using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.CountryForm
{

    public class CountryForm 
    {
        [Required]
        public string Name { get; set; }
        
        public IFormFile? Video { get; set; }

    }
}
