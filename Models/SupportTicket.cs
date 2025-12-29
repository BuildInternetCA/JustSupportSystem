using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class SupportTicket : JUserBase
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public short Status { get; set; }
       
        //-----------------------------------------------------------
        public long? AssignedAgentId { get; set; }

        [ForeignKey("AssignedAgentId")]
        public virtual UserAccount? AssignedAgent { get; set; }

        //-----------------------------------------------------------
        //-----------------------------------------------------------
        public long? CompanyId { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company? Company { get; set; }

        //-----------------------------------------------------------
        public long SupportTicketTypeId { get; set; }

        [ForeignKey("SupportTicketTypeId")]
        public virtual SupportTicketType? SupportTicketType { get; set; }

        public SupportTicketStatusEnum GetStatusEnum()
        {
            return (SupportTicketStatusEnum)Status;
        }

        public ICollection<SupportTicketCustomField>? CustomFields { get; set; }

        public long? MasterSupportTicketId { get; set; } = 0;

    }
}
