namespace BackEndStructuer.Entities
{
    public class Field : BaseEntity<Guid>
    {

        public string? Name { get; set; }

        public List<DegreeField>? DegreeFields { get; set; } = new List<DegreeField>();


    }
}
