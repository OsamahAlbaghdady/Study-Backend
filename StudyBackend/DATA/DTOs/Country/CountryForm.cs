using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.CountryUpdate
{

    public class CountryForm
    {
        [Required]
        public string? Name { get; set; }
        
        public IFormFile? Video { get; set; }


    }
}
