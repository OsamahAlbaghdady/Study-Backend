namespace BackEndStructuer.Entities
{
    public class Setting : BaseEntity<Guid>
    {
        public string? WelcomeMessage { get; set; }
        
        public string? WelcomeVideoUrl { get; set; }
        
        public string? ContactWhatsApp { get; set; }
        
        public string? ContactTelegram { get; set; }
    }
}
