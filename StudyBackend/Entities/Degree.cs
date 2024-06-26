namespace BackEndStructuer.Entities
{
    public class Degree : BaseEntity<Guid>
    {
        public string? Name { get; set; }

        public List<UniversityDegree>? UniversityDegrees { get; set; } = new List<UniversityDegree>();

        public List<DegreeField>? DegreeFields { get; set; } = new List<DegreeField>();

    }
}
