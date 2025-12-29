using JustSupportSystem.Models;
using System.Drawing.Printing;

namespace JustSupportSystem.DTO
{
    public class TicketListViewDTO
    {
        public long TicketID { get; set; }
        public string? Subject { get; set; }
        public string? Status { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
        public string? AssingedTo { get; set; }

        public static TicketListViewDTO GetView(SupportTicket ticket)
        {
            TicketListViewDTO model = new TicketListViewDTO();
            model.TicketID = ticket.Id;
            model.Subject = ticket.Title;
            model.CreatedBy = ticket.UserAccount != null ? ticket.UserAccount.FullName : "N/A";
            model.CreatedOn = ticket.DateAdded.ToString("yyyy-MM-dd HH:mm");
            model.AssingedTo = ticket.AssignedAgent != null ? ticket.AssignedAgent.FullName : "Unassigned";
            model.Status = ticket.GetStatusEnum().ToString();
            return model;
        }

        public Dictionary<string, string> ToDictionary()
        {
            return new Dictionary<string, string>
            {
                { "Ticket ID", TicketID.ToString() },
                { "Subject", Subject ?? "" },
                { "Status", Status ?? "" },
                { "By", CreatedBy ?? "" },
                { "Created On", CreatedOn ?? "" },
                { "Assigned To", AssingedTo ?? "" },
            };
        }
    }
}
