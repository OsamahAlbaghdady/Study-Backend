using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.FileCreation;

public class FileCreationForm
{
    [Required]
    public string name { get; set; }

    [Required]
    public bool isAuth { get; set; }
    
    
    [Required]
    public bool isInt { get; set; }

    
}