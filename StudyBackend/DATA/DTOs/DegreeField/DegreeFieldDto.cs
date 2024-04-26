namespace BackEndStructuer.DATA.DTOs.DegreeField
{
    public class DegreeFieldDto : BaseDto<Guid>
    {
        public DegreeDto.DegreeDto? Degree { get; set; }
        
        public FieldDto.FieldDto? Field { get; set; }
    }
}
