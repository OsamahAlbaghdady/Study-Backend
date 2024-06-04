namespace BackEndStructuer.DATA.DTOs.CountryDto
{

    public class CountryDto : BaseDto<Guid>
    {
        public string Name { get; set; }
        
        public string VideoUrl { get; set; }
    }
}
