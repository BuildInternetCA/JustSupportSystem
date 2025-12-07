using JustSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    public class JBaseController : Controller
    {
        protected readonly JDBContext DbCtx;
        public JBaseController(JDBContext jDBContext)
        {
            DbCtx = jDBContext;
        }
    }
}
