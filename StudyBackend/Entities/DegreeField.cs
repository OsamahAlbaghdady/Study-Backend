namespace BackEndStructuer.Entities
{
    public class DegreeField : BaseEntity<Guid>
    {
        
        public Guid? DegreeId { get; set; }
        
        public Degree? Degree { get; set; }
        
        public Guid? FieldId { get; set; }
        
        public Field? Field { get; set; }
        
    }
}
