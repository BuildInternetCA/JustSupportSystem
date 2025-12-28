using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    public class TicketController(JDBContext jDBContext) : JBaseController(jDBContext)
    {
        [Route("/tickets")]
        public IActionResult Index(int pageNo, SupportTicketStatusEnum status)
        {
            return View();
        }
    }
}
