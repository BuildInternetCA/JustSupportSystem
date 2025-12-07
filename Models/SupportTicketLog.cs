using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class SupportTicketLog : JUserBase
    {
        public long? SupportTicketId { get; set; }
        public short LogType { get; set; }
        public string? Description { get; set; }
        //-----------------------------------------------------------
        [ForeignKey("SupportTicketId")]
        public virtual SupportTicket? SupportTicket { get; set; }
    }
}
