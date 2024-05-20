using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.MedicalFeild;

public class MedicalFieldForm
{
    [Required]
    public string? Name { get; set; }

    public IFormFile Video { get; set; }
}