namespace BackEndStructuer.DATA.DTOs.DegreeFieldFilter
{

    public class DegreeFieldFilter : BaseFilter 
    {
            
            public Guid? DegreeId { get; set; }
            
            public Guid? FieldId { get; set; }

            public Guid? CountryId { get; set; }
            
            public Guid? UniversityId { get; set; }
            
            
    }
}
