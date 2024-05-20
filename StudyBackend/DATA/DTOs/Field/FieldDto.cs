namespace BackEndStructuer.DATA.DTOs.FieldDto
{

    public class FieldDto : BaseDto<Guid>
    {
        
        public string? Name { get; set; }
        
     
        public bool? Priority { get; set; } = false;

    }
}
