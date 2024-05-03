using System.ComponentModel.DataAnnotations;
using BackEndStructuer.DATA.DTOs.File;

namespace BackEndStructuer.DATA.DTOs.DegreeForm
{

    public class DegreeForm 
    {
        [Required]
        public string Name { get; set; }

        public string? VideoUrl { get; set; }
    }
}
