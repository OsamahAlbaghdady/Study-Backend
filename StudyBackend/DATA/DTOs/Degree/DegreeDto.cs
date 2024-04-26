namespace BackEndStructuer.DATA.DTOs.DegreeDto
{

    public class DegreeDto : BaseDto<Guid>
    {

        public string? Name { get; set; }

        public string? VideoUrl { get; set; }
    }
}
