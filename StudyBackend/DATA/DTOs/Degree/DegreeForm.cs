using System.ComponentModel.DataAnnotations;
using BackEndStructuer.DATA.DTOs.File;

namespace BackEndStructuer.DATA.DTOs.DegreeUpdate
{

    public class DegreeForm
    {
        [Required]
        public string Name { get; set; }

        public IFormFile Video { get; set; }
    }
}
