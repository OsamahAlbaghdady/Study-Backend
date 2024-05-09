namespace BackEndStructuer.DATA.DTOs.SettingUpdate
{

    public class SettingUpdate
    {
        public string? WelcomeMessage { get; set; }
        
        public IFormFile? WelcomeVideoUrl { get; set; }
        
        public string? ContactWhatsApp { get; set; }
        
        public string? ContactTelegram { get; set; }
    }
}
