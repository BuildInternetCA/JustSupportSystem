namespace JustSupportSystem.Models
{
    public class SupportTicketType : JDBBase
    {
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public bool Internal { get; set; }
    }
}
