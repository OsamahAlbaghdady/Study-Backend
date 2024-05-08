namespace BackEndStructuer.DATA.DTOs.QuestionDto
{
    

    public class QuestionDto : BaseDto<Guid>
    {
        public string? QuestionTitle { get; set; }
        
        public string? QuestionAnswer { get; set; }

        public CountryDto.CountryDto? Country { get; set; }
    }
}
