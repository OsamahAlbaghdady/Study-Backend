namespace BackEndStructuer.Entities
{
    public class Field : BaseEntity<Guid>
    {

        public string? Name { get; set; }

        public long? Price { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

    }
}
