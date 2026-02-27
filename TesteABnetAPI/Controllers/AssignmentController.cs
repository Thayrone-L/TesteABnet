using BackEndTesteABnet.Entities;
using BackEndTesteABnet.Entities.Enums;
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

        [HttpGet]
        public async Task<ActionResult<List<Assignment>>> GetAllAssignment(Status? status = null, Priority? priority = null, bool? overdue = null, string? search = null)
        {
            var result = await _service.GetAllAssignment(status, priority ,overdue, search);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Assignment GetAssignmentById(int id)
        {
            return _service.GetAssignmentById(id).Result;
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
