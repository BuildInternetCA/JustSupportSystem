using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class SupportTicketCustomField : JDBBase
    {
        public long? SupportTicketId { get; set; }
        public long? CustomFieldMasterId { get; set; }

        [ForeignKey("CustomFieldMasterId")]
        public virtual CustomFieldMaster? CustomFieldMaster { get; set; }
        public string? FieldValue { get; set; }
        //-----------------------------------------------------------
        [ForeignKey("SupportTicketId")]
        public virtual SupportTicket? SupportTicket { get; set; }
    }
}
