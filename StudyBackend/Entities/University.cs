namespace BackEndStructuer.Entities
{
    public class University : BaseEntity<Guid>
    {

        public string? Name { get; set; }

        public Guid? CountryId { get; set; }

        public Country? Country { get; set; }

        public List<UniversityDegree>? UniversityDegrees { get; set; } = new List<UniversityDegree>();



    }
}
