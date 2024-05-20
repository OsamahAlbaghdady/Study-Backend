namespace BackEndStructuer.DATA.DTOs.QuestionForm
{

    public class QuestionUpdate
    {
        public string? QuestionTitle { get; set; }
        
        public string? QuestionAnswer { get; set; }

        public Guid? CountryId { get; set; }

        public IFormFile? Video { get; set; }
    }
}
