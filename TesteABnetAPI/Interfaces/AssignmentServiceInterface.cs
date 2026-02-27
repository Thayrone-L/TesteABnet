using BackEndTesteABnet.Entities;

namespace TesteABnetAPI.Interfaces
{
    public interface AssignmentServiceInterface
    {
            void CreateAssignment(Assignment assignment);
            IEnumerable<Assignment> GetAllAssignment();
            Assignment GetAssignmentById(int id);
            void UpdateAssignment(Assignment assignment);
            void DeleteAssignment(int id);
    }
}
