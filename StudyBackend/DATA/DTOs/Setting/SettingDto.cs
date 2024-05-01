namespace BackEndStructuer.DATA.DTOs.SettingDto
{

    public class SettingDto : BaseDto<Guid>
    {
        public string? WelcomeMessage { get; set; }
        
        public string? WelcomeVideoUrl { get; set; }
        
        public string? ContactWhatsApp { get; set; }
        
        public string? ContactTelegram { get; set; }

    }
}
