using System.ComponentModel.DataAnnotations;

namespace BackEndStructuer.DATA.DTOs.QuestionUpdate
{

    public class QuestionForm
    {   
        [Required]
        public string QuestionTitle { get; set; }
        
        public string? QuestionAnswer { get; set; }

        [Required]
        public Guid CountryId { get; set; }


        public IFormFile? Video { get; set; }
        
    }
}
