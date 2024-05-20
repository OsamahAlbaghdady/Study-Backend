namespace BackEndStructuer.Entities
{
    public class Question : BaseEntity<Guid>
    {

        public string? QuestionTitle { get; set; }
        
        public string? QuestionAnswer { get; set; }

        public Guid? CountryId { get; set; }

        public Country? Country { get; set; }

        public string? VideoUrl { get; set; }
    }
}
