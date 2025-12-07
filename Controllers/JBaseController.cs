using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    public class JBaseController : Controller
    {
        protected JDBContext JDB;

        public JBaseController(JDBContext jDBContext)
        {
            JDB = jDBContext;
        }

    }
}
