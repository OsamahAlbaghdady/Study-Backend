namespace BackEndStructuer.DATA.DTOs.UniversityFilter
{

    public class UniversityFilter : BaseFilter 
    {
        public string? Name { get; set; }

        public Guid? CountryId { get; set; }

        public Guid? FieldId { get; set; }
    }
}
