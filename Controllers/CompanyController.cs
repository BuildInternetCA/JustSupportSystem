using JustSupportSystem.DTO;
using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    public class CompanyController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        private int PageSize = 10;

        [Route("/companies")]
        public IActionResult Index(int pageNo=1)
        {
            //var companies = JDB.Companies.Where(c => !c.IsDeleted).Skip((pageNo - 1) * PageSize).Take(PageSize).ToList();
            UITable table = new UITable
            {
                Title = "Companies",
                Description = "List of all companies",
                PageNo = pageNo,
                PageSize = PageSize,
                TotalRecords = 99,
                EnableActions = true,
                PaginationUrl = "/companies?pageNo=[~pageNo]",
                AddButton = new UIButton
                {
                    Title = "Add Company",
                    Url = "/Company/EditCompany"
                },
                EditButton = new UIButton
                {
                    Title = "Edit",
                    Url = Url.Action("EditCompany", "Company")
                },
                DeleteButton = new UIButton
                {
                    Title = "Delete",
                    Url = Url.Action("DeleteCompany", "Company"),
                    Type = UIButtonType.Danger
                },
                Data = new UITableData
                {
                    Headers = new Dictionary<int, string>
                    {
                        { 0, "ID" },
                        { 1, "Name" },
                        { 2, "Address" },
                        { 3, "Phone" }
                    },
                    Rows = new List<Dictionary<string, string>>
                    {
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "1" },
                            { "Name", "Company A" },
                            { "Address", "123 Main St" },
                            { "Phone", "555-1234" }
                        },
                        new Dictionary<string, string>
                        {
                            { "ID", "2" },
                            { "Name", "Company B" },
                            { "Address", "456 Elm St" },
                            { "Phone", "555-5678" }
                        }
                }
                }
            };
            //Rows = JDB.Companies
            //            .Where(c => !c.IsDeleted)
            //            .Skip((pageNo - 1) * PageSize)
            //            .Take(PageSize)
            //            .Select(c => new List<string> { c.Id.ToString(), c.Name, c.Address, c.Phone })
            //            .ToList()
            return Table(table);
        }

        public IActionResult AddCompany()
        {
            return View();
        }
        public IActionResult EditCompany()
        {
            return View();
        }

        public IActionResult DeleteCompany()
        {
            return View();

        }

        public IActionResult ViewCompany()
        {
            return View();
        }
    }
}
