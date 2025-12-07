using System.ComponentModel.DataAnnotations.Schema;

namespace JustSupportSystem.Models
{
    public class Company : JDBBase
    {
        public string ? CompanyName { get; set; }
        public string? CompanyCode { get; set; }
        public string? Notes { get; set; }
        public bool IsClient { get; set; }

        [ForeignKey("DefaultAgent")]
        public long? DefaultAgentId { get; set; }
        public virtual UserAccount? DefaultAgent { get; set; }
        public virtual ICollection<CustomFieldMaster>? CustomFieldMasters { get; set; }
        public virtual ICollection<UserAccount>? UserAccounts { get; set; }
        public virtual ICollection<SupportTicket>? SupportTickets { get; set; }
        
    }
}
