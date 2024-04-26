namespace BackEndStructuer.DATA.DTOs.UniversityDegreeFilter
{

    public class UniversityDegreeFilter : BaseFilter 
    {
            
            public Guid? UniversityId { get; set; }
            
            public Guid? DegreeId { get; set; }
    }
}
