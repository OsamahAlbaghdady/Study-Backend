using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.UniversityForm
{

    public class UniversityForm 
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid CountryId { get; set; }

    }
}
