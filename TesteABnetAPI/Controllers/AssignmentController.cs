using BackEndTesteABnet.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TesteABnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerBase
    {
        [HttpPost(Name = "CreateAssignment")]
        public void PostAssignment(Assignment assignment)
        {

        }


        [HttpGet(Name = "GetAllAssignment")]
        public IEnumerable<Assignment> GetAllAssignment()
        {
            return null;
        }

        [HttpGet(Name = "GetAssignmentById")]
        public Assignment GetAssignmentById()
        {
            return null;
        }

        [HttpPut(Name = "UpdateAssignment")]
        public void PutAssignment(Assignment assignment)
        {

        }

        [HttpDelete(Name = "DeleteAssignment")]
        public void DeleteAssignment(Assignment assignment) 
        { 
        
        }
    }
}
