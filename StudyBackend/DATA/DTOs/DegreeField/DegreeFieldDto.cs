namespace BackEndStructuer.DATA.DTOs.DegreeField
{
    public class DegreeFieldDto : BaseDto<Guid>
    {
        public DegreeDto.DegreeDto? Degree { get; set; }
        
        public FieldDto.FieldDto? Field { get; set; }
        
        public string? Price { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public Guid? UniversityId { get; set; }
        
        public string? UniversityName { get; set; }


        public Guid? CountryId { get; set; }
        
        public string? CountryName { get; set; }

        public bool? IsValid { get; set; }

    }
}
