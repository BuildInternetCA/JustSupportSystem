using JustSupportSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JustSupportSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JAPIBaseController : ControllerBase
    {
        protected readonly JDBContext DbCtx;
        public JAPIBaseController(JDBContext jDBContext)
        {
            DbCtx = jDBContext;
        }
    }
}
