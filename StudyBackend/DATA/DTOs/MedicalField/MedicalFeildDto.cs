namespace BackEndStructuer.DATA.DTOs.MedicalFeild;

public class MedicalFieldDto : BaseDto<Guid>
{
    public string? Name { get; set; }

    public string? VideoUrl { get; set; }
}