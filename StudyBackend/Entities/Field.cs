namespace BackEndStructuer.Entities
{
    public class Field : BaseEntity<Guid>
    {

        public string? Name { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public List<DegreeField>? DegreeFields { get; set; } = new List<DegreeField>();


    }
}
