namespace BackEndStructuer.Entities
{
    public class Country : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        
        
        public string? VideoUrl { get; set; }

    }
}
