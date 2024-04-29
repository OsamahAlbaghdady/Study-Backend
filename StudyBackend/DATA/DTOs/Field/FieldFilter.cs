namespace BackEndStructuer.DATA.DTOs.FieldFilter
{

    public class FieldFilter : BaseFilter 
    {
        public string? Name { get; set; }

        public Guid? DegreeId  { get; set; }
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
    }
}
