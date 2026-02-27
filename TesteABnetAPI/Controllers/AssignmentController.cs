using BackEndTesteABnet.Entities;
using Microsoft.AspNetCore.Mvc;
using TesteABnetAPI.Interfaces;
using TesteABnetAPI.Services;

namespace TesteABnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssignmentController : ControllerBase
    {
        private readonly AssignmentServiceInterface _service;

        public AssignmentController(AssignmentServiceInterface service)
        {
            _service = service;
        }

        [HttpPost(Name = "CreateAssignment")]
        public void PostAssignment(Assignment assignment)
        {
            _service.CreateAssignment(assignment);
        }

        [HttpGet(Name = "GetAllAssignment")]
        public IEnumerable<Assignment> GetAllAssignment()
        {
            return _service.GetAllAssignment();
        }

        [HttpGet("{id}")]
        public Assignment GetAssignmentById(int id)
        {
            return _service.GetAssignmentById(id);
        }

        [HttpPut(Name = "UpdateAssignment")]
        public void PutAssignment(Assignment assignment)
        {
            _service.UpdateAssignment(assignment);
        }

        [HttpDelete(Name = "DeleteAssignment")]
        public void DeleteAssignment(int id) 
        { 
            _service.DeleteAssignment(id);
        }
    }
}
