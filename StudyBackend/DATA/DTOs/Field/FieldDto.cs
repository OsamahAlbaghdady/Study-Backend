namespace BackEndStructuer.DATA.DTOs.FieldDto
{

    public class FieldDto : BaseDto<Guid>
    {
        
        public string? Name { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }

    }
}
