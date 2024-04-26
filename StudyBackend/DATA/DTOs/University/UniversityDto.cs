namespace BackEndStructuer.DATA.DTOs.UniversityDto
{
    using CountryDto;

    public class UniversityDto : BaseDto<Guid>
    {
        public string Name { get; set; }

        public CountryDto? Country { get; set; }
    }
}