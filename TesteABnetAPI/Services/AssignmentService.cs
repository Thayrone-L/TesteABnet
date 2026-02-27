using BackEndTesteABnet.Entities;

namespace TesteABnetAPI.Services
{
    public class AssignmentService : Interfaces.AssignmentServiceInterface
    {
        public void CreateAssignment(Assignment assignment)
        {
            if (assignment.Title == "string" || assignment.Title.Length == 0|| assignment.Title == null) 
            { 
                throw new ArgumentException("Title is required"); 
            }

            List<Assignment> assignments = GetAllAssignment();

            if (assignments.Any(a => a.Title == assignment.Title && a.Status == 0 )) 
            {
                throw new ArgumentException("Cannot create an assignment with the same Title as an pendent assignment");
            }


            throw new NotImplementedException();
        }

        public void DeleteAssignment(int id)
        {
            throw new NotImplementedException();
        }

        public List<Assignment> GetAllAssignment()
        {
            throw new NotImplementedException();
        }

        public Assignment GetAssignmentById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateAssignment(Assignment assignment)
        {
            throw new NotImplementedException();
        }
    }
}
