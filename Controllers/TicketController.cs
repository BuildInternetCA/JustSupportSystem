using JustSupportSystem.DTO;
using JustSupportSystem.JSystem;
using JustSupportSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace JustSupportSystem.Controllers
{
    [UserAccess]
    public class TicketController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        [Route("/tickets/{pageNo}")]
        public IActionResult Index(int pageNo, short? status, int assignedToMe = 0, long companyId = 0, long agentId = 0, long clientUserId = 0)
        {
            var user = LoggedInUser();
            if (user == null) return NotFound();
            var role = user.GetUserRoleEnum();
            string query = "";
            if (user.CompanyId == null)
            {
                user.CompanyId = 0;
            }


            var tickets = JDB.SupportTickets
                    .Include(p => p.AssignedAgent)
                    .Include(p => p.UserAccount)
                    .Where(p => !p.IsDeleted);

            if (role == UserRoleEnum.Client_Manager || role == UserRoleEnum.Client_User)
            {
                tickets = tickets.Where(p => p.CompanyId == user.CompanyId);
                if (role == UserRoleEnum.Client_User)
                {
                    tickets = tickets.Where(p => p.CreatedByUser == user.Id);
                }
                if (role == UserRoleEnum.Client_Manager)
                {
                    if (clientUserId != 0)
                    {
                        query += $"&clientUserId={clientUserId}";
                        tickets = tickets.Where(p => p.CreatedByUser == clientUserId);
                    }
                }
            }
            else if (role == UserRoleEnum.Agent_User || role == UserRoleEnum.Agent_Manager || role == UserRoleEnum.System_Admin)
            {
                if (companyId != 0)
                {
                    query += $"&companyId={companyId}";
                    tickets = tickets.Where(p => p.CompanyId == companyId);
                }
                if (assignedToMe == 1)
                {
                    query += $"&assignedToMe=1";
                    tickets = tickets.Where(p => p.AssignedAgentId == user.Id);
                }
                if (assignedToMe == 2)
                {
                    query += $"&assignedToMe=2";
                    tickets = tickets.Where(p => p.CreatedByUser == user.Id);
                }
                if (agentId != 0)
                {
                    query += $"&agentId={agentId}";
                    tickets = tickets.Where(p => p.AssignedAgentId == agentId);
                }
                if (clientUserId != 0)
                {
                    query += $"&clientUserId={clientUserId}";
                    tickets = tickets.Where(p => p.CreatedByUser == clientUserId);
                }
                if (role == UserRoleEnum.Agent_User)
                {
                    var companies = JDB.Companies.Include(p => p.UserAccounts)
                                                .Where(p => p.DefaultAgentId != null && p.DefaultAgentId == user.Id)
                                                .Select(p => p.Id);

                    tickets = tickets.Where(p => p.AssignedAgentId == user.Id || companies.Any(k => k == p.CompanyId));
                }
            }
            if (status != null && status>0)
            {
                query += $"&status={status}";
                tickets = tickets.Where(p => p.Status == status);
            }
            else if(status!=-1)
            {
                query += $"&status={status}";
                tickets = tickets.Where(p => p.Status == 1 || p.Status == 3);
            }

            UITable table = new UITable
            {
                Title = "Companies",
                Description = "List of all companies",
                PageNo = pageNo,
                PageSize = 25,
                TotalRecords = tickets.Count(),
                PaginationUrl = $"/tickets?pageNo=[~pageNo]{query}",
                Data = new UITableData()
            };
            //{ "Ticket ID", TicketID.ToString() },
            //    { "Subject", Subject ?? "" },
            //    { "Status", Status ?? "" },
            //    { "By", CreatedBy ?? "" },
            //    { "Created On", CreatedOn ?? "" },
            //    { "Assigned To", AssingedTo ?? "" },
            var finalData = tickets.ListByPageNumber(pageNo, 25).Select(p => TicketListViewDTO.GetView(p));
            table.AddHeader(new Dictionary<int, string>
                    {
                        { 0, "Ticket ID" },
                        { 1, "Subject" },
                        { 2, "Status" },
                        { 3, "By" },
                        { 4, "Created On" },
                        { 5, "Assigned To" }
                    });

            foreach (var item in finalData)
            {
                table.AddRow(item.ToDictionary());
            }

            table.AddButton = new UIButton
            {
                Title = "Add New Ticket",
                Url = "/tickets/create"
            };
            table.EditButton = new UIButton
            {
                Title = "View",
                Url = "/tickets/edit?id=[~Ticket ID]"
            };

            return Table(table);
        }


    }
}
