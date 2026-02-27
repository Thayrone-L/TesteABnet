using BackEndTesteABnet.Entities;

namespace TesteABnetAPI.Interfaces
{
    public interface AssignmentServiceInterface
    {
            void CreateAssignment(Assignment assignment);
            List<Assignment> GetAllAssignment();
            Assignment GetAssignmentById(int id);
            void UpdateAssignment(Assignment assignment);
            void DeleteAssignment(int id);
    }
}
