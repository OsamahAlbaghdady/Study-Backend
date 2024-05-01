using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.SettingForm
{

    public class SettingForm 
    {
        [Required]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

    }
}
