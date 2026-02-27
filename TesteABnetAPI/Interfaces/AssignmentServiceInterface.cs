using BackEndTesteABnet.Entities;

namespace TesteABnetAPI.Interfaces
{
    public interface AssignmentServiceInterface
    {
            void CreateAssignment(Assignment assignment);
            Task<List<Assignment>>GetAllAssignment(BackEndTesteABnet.Entities.Enums.Status? status, BackEndTesteABnet.Entities.Enums.Priority? priority, bool? overdue, string? search);
            Task<Assignment> GetAssignmentById(int id);
            void UpdateAssignment(Assignment assignment);
            void DeleteAssignment(int id);
    }
}
