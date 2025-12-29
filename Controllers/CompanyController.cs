using JustSupportSystem.DTO;
using JustSupportSystem.JSystem;
using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JustSupportSystem.Controllers
{
    [AgentManagerUserAccess]
    public class CompanyController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        private int PageSize = 25;

        [Route("/companies/{pageNo}")]
        public IActionResult Index(int pageNo, string name)
        {
            string query = "";
            var companies = JDB.Companies.Where(c => !c.IsDeleted);
            if (!string.IsNullOrEmpty(name))
            {
                query=$"&name={name}";
                companies.Where(c => c.CompanyName.Contains(name));
            }
           
            UITable table = new UITable
            {
                Title = "Companies",
                Description = "List of all companies",
                PageNo = pageNo,
                PageSize = PageSize,
                TotalRecords = companies.Count(),
                EnableActions = true,
                PaginationUrl = $"/companies?pageNo=[~pageNo]{query}",
                AddButton = new UIButton
                {
                    Title = "Add new Company",
                    Url = "/company/add"
                },
                EditButton = new UIButton
                {
                    Title = "View",
                    Url = "/company/view/[~Company ID]"
                },
                Data = new UITableData()
            };
            table.Data.Headers = new Dictionary<int, string>
            {
                { 0, "Company ID" },
                { 1, "Company Code" },
                { 2, "Company Name" },
                { 3, "User Count" },
                { 4, "Ticket Count" }
            };
            table.Data.Rows = new List<Dictionary<string, string>>();
            var companiesFinal = companies.ListByPageNumber(pageNo,25).Select(p => new CompanyViewDTO
            {
                CompanyId = p.Id,
                CompanyName = p.CompanyName,
                CompanyCode = p.CompanyCode,
                UserCount = p.UserAccounts.Where(p => !p.IsDeleted).Count(),
                TicketCount = p.SupportTickets.Where(p => p.Status == 1 || p.Status == 3).Count()
            });
            foreach (var company in companiesFinal)
            {
                var row = new Dictionary<string, string>
                {
                    { "Company ID", company.CompanyId.ToString() },
                    { "Company Code", company.CompanyCode },
                    { "Company Name", company.CompanyName },
                    { "User Count", company.UserCount.ToString() },
                    { "Ticket Count", company.TicketCount.ToString() }
                };
                table.Data.Rows.Add(row);
            }
            return Table(table);
        }

        [Route("/company/add")]
        public async Task<IActionResult> AddCompany()
        {
            CompanyFormDTO formDTO = new CompanyFormDTO();
            formDTO.SetForm(JDB);
            return await Form(formDTO);
        }

        [HttpPost]
        [Route("/company/add")]
        public async Task<IActionResult> AddCompany(CompanyFormDTO model)
        {
            Company company = new Company();
            company.CompanyCode = model.Code;
            company.CompanyName = model.Name;
            company.Notes = model.Notes;
            company.IsClient = false;
            JDB.Companies.Add(company);
            JDB.SaveChanges();
            return Redirect("/companies/1");
        }

        [Route("/company/edit")]
        public async Task<IActionResult> EditCompanyAsync(long id)
        {
            var company = JDB.Companies.FirstOrDefault(p => p.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            CompanyFormDTO form = new CompanyFormDTO();
            form.SetForm(JDB);
            form.ActionUrl = "/company/edit?id="+id;
            form.FormTitle = "Update Company";
            form.SubmitButtonText = "Update";
            form.Name = company.CompanyName;
            form.Code = company.CompanyCode;
            form.Notes = company.Notes;
            form.DbId = company.Id;
            form.DefaultAgentId = company.DefaultAgentId ?? 0;
            return await Form(form);
        }

        [HttpPost]
        [Route("/company/edit")]
        public async Task<IActionResult> EditCompanyAsync(long id, CompanyFormDTO form)
        {
            var company = JDB.Companies.FirstOrDefault(p => p.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            company.CompanyName = form.Name;
            company.CompanyCode = form.Code;
            company.Notes = form.Notes;
            company.DefaultAgentId = form.DefaultAgentId;
            return await Form(form);
        }


        [Route("/company/view/{id}")]
        public IActionResult ViewCompany(long id, int pageNo = 1)
        {
            var company = JDB.Companies.Include(p => p.DefaultAgent).AsNoTracking().Where(p => !p.IsDeleted && p.Id == id)
                .Select(p => new CompanyViewDetailsDTO
                {
                    CompanyId = p.Id,
                    CompanyName = p.CompanyName,
                    CompanyCode = p.CompanyCode,
                    UserCount = p.UserAccounts.Where(p => !p.IsDeleted).Count(),
                    TicketCount = p.SupportTickets.Where(p => p.Status == 1 || p.Status == 3).Count(),
                    DefaultAgent = p.DefaultAgent.FirstName,
                    Notes = p.Notes
                })
                .FirstOrDefault();
            if (company == null)
            {
                return NotFound();
            }
            var users = JDB.UserAccounts.Where(p => !p.IsDeleted && p.CompanyId == company.CompanyId)
            
                .ListByPageNumber(pageNo, 50).AsNoTracking().Select(p => UserAccountViewDTO.GetView(p))   .AsNoTracking().ToList();

            company.UserTable = new UITable
            {

                Title = "Users",
                Description = "Users under this company",
                PageNo = pageNo,
                PageSize = 50,
                TotalRecords = company.UserCount,
                EnableActions = true,
                PaginationUrl = $"/company/view/{id}/?pageNo=[~pageNo]",
                AddButton = new UIButton
                {
                    Title = "Add user",
                    Url = $"/company/{id}/user/add"
                },
                EditButton = new UIButton
                {
                    Title = "Edit",
                    Url = $"/company/{id}/user/edit/[~User Id]"
                },
                ViewButton = new UIButton
                {
                    Title = "Bar Code",
                    Url = $"/company/{id}/user/bar-code/[~User Id]"
                }
             };

            company.UserTable.Data = new UITableData();
            company.UserTable.Data.Headers = new Dictionary<int, string>
            {
                { 0, "User Id" },
                { 1, "First Name" },
                { 2, "Last Name" },
                { 3, "Email" },
                { 4, "Role" },
            };

            foreach (var user in users)
            {
                company.UserTable.AddRow(new Dictionary<string, string>
                {
                    { "User Id", user.DbId.ToString()},
                    { "First Name", user.FirstName},
                    { "Last Name", user.LastName},
                    { "Email", user.Email},
                    { "Role", user.UserRole}
                });
            }


            company.CustomFields = new UITable
            {

                Title = "Custom Ticket Fields",
                Description = "Custom fields for this company",
                PageNo = pageNo,
                PageSize = 50,
                TotalRecords = company.UserCount,
                EnableActions = true,
                AddButton = new UIButton
                {
                    Title = "Add user",
                    Url = $"/company/{id}/user/add"
                },
                EditButton = new UIButton
                {
                    Title = "Edit",
                    Url = $"/company/{id}/user/edit/[~User Id]"
                },
                ViewButton = new UIButton
                {
                    Title = "Bar Code",
                    Url = $"/company/{id}/user/bar-code/[~User Id]"
                }
            };

            company.CustomFields.Data = new UITableData();
            company.CustomFields.Data.Headers = new Dictionary<int, string>
            {
                { 0, "Field Id" },
                { 1, "Field Name" },
                { 2, "Field Type" },
            };

            foreach (var user in users)
            {
                company.CustomFields.AddRow(new Dictionary<string, string>
                {
                    { "Field Id", user.DbId.ToString()},
                    { "Field Name", user.FirstName},
                    { "Field Type", user.LastName}
                });
            }

            return View(company);
        }

        [Route("/company/{id}/user/add")]
        public async Task<IActionResult> AddUser(long id)
        {
            UserAccountFormDTO model = new UserAccountFormDTO();
            model.CompanyId = id;
            model.SetForm(JDB);
            return await Form(model);
        }

        [HttpPost]
        [Route("/company/{id}/user/add")]
        public IActionResult AddUser(long id, UserAccountFormDTO model)
        {
            TotpService service = new TotpService();
            UserAccount user = new UserAccount();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Password =  model.Password.Encrypt();
            user.UserRole = model.Role;
            user.CompanyId = model.CompanyId;
            user.IsSSOUser = true;
            user.IsTOTPRequired = true;
            user.FailedTries = 0;
            user.TOTPSecret = service.GenerateSecretKey();

            JDB.UserAccounts.Add(user);
            JDB.SaveChanges();
            return Redirect($"/company/view/{id}");
        }

        [Route("/company/{id}/user/edit/{userid}")]
        public async Task<IActionResult> EditUser(long id, long userid)
        {
            var user = JDB.UserAccounts.FirstOrDefault(p => p.Id == userid);
            if (user == null)
            {
                return NotFound();
            }
            UserAccountFormDTO model = new UserAccountFormDTO();
            model.CompanyId = id;
            model.DbId = userid;
            model.SetForm(JDB);
            model.FormTitle = "Edit User";
            model.SubmitButtonText = "Update";
            model.ActionUrl = $"/company/{id}/user/edit/{userid}";
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Role = user.UserRole;
            model.Password = "***";
            return await Form(model);
        }

        [HttpPost]
        [Route("/company/{id}/user/edit/{userid}")]
        public IActionResult EditUser(long id, long userid, UserAccountFormDTO model)
        {
            var user = JDB.UserAccounts.FirstOrDefault(p => p.Id == userid);
            if (user == null)
            {
                return NotFound();
            }
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserRole = model.Role;
            if (!model.Password.Equals("***"))
            {
                user.Password = model.Password.Encrypt();
            }
            if(!string.IsNullOrEmpty(model.ResetAccount) && model.ResetAccount.Equals("1"))
            {
                user.FailedTries = 0;
                user.PasswordResetRequired = true;
            }
            JDB.SaveChanges();
            return Redirect($"/company/view/{id}");
        }

        [Route("/company/{id}/user/bar-code/{UserId}")]
        public IActionResult BarCode(long id, long UserId)
        {
            var user = JDB.UserAccounts.AsNoTracking().FirstOrDefault(p => p.Id == UserId);
            if (user == null)
            {
                return NotFound();
            }
            TotpService service = new TotpService();
            var image = service.GenerateQRCode(user.Email, user.TOTPSecret);
            string base64Image = Convert.ToBase64String(image);
            ViewBag.BarCodeUrl = "data:image/png;base64," + base64Image;
            return View(user);
        }
    }
}
