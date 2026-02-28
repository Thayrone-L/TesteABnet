using BackEndTesteABnet.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TesteABnetAPI.Interfaces
{
    public interface AssignmentServiceInterface
    {
            Task<bool> CreateAssignment(Assignment assignment);
            Task<List<Assignment>>GetAllAssignment(BackEndTesteABnet.Entities.Enums.Status? status, BackEndTesteABnet.Entities.Enums.Priority? priority, bool? overdue, string? search);
            Task<Assignment> GetAssignmentById(int id);
            Task<bool> UpdateAssignment(Assignment assignment);
            Task<bool> DeleteAssignment(int id);
    }
}
