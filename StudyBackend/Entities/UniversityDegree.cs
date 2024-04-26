namespace BackEndStructuer.Entities
{
    public class UniversityDegree : BaseEntity<Guid>
    {
        public Guid? UniversityId { get; set; }
        
        public University? University { get; set; }
        
        public Guid? DegreeId { get; set; }
        
        public Degree? Degree { get; set; }
    }
}
