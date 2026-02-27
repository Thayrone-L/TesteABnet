using BackEndTesteABnet.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TesteABnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerBase
    {
        [HttpGet(Name = "GetAssignment")]
        public IEnumerable<Assignment> Get()
        {
            return null;

        }
    }
}
