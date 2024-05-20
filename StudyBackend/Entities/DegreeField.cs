namespace BackEndStructuer.Entities
{
    public class DegreeField : BaseEntity<Guid>
    {
        
        public Guid? DegreeId { get; set; }
        
        public Degree? Degree { get; set; }
        
        public Guid? FieldId { get; set; }
        
        public Field? Field { get; set; }
        
        public long? Price { get; set; }

        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        public Guid? UniversityId { get; set; }
        
        public University? University { get; set; }


    }
}
