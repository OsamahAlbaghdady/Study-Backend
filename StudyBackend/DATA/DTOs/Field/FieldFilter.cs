namespace BackEndStructuer.DATA.DTOs.FieldFilter
{

    public class FieldFilter : BaseFilter 
    {
        public string? Name { get; set; }

        public Guid? DegreeId  { get; set; }
        
        public bool? Priority { get; set; } = false;

    }
}
