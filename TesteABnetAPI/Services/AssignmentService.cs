using BackEndTesteABnet.Entities;
using BackEndTesteABnet.Entities.Enums;
using BackEndTesteABnet.Repository;
using TesteABnetAPI.Interfaces;

namespace TesteABnetAPI.Services
{
    public class AssignmentService : Interfaces.AssignmentServiceInterface
    {
        private readonly AssignmentRepository _repository;

        public AssignmentService(AssignmentRepository repository)
        {
            _repository = repository;
        }
        public async void CreateAssignment(Assignment assignment)
        {
            if (assignment.Title == "string" || assignment.Title.Length == 0|| assignment.Title == null) 
            { 
                throw new ArgumentException("Title is required"); 
            }

            List<Assignment> assignments = await GetAllAssignment(null, null, null, null);

            if (assignments.Any(a => a.Title == assignment.Title && a.Status == 0 )) 
            {
                throw new ArgumentException("Cannot create an assignment with the same Title as an pendent assignment");
            }

            _repository.AddAsync(assignment);
        }

        public void DeleteAssignment(int id)
        {
            _repository.DeleteAssignment(new Assignment { Id = id });
        }

        public async Task<List<Assignment>> GetAllAssignment(BackEndTesteABnet.Entities.Enums.Status? status, BackEndTesteABnet.Entities.Enums.Priority? priority, bool? overdue, string? search)
        {
            List<Assignment> assignments = await _repository.GetAllAsync();

            List<Assignment> OrderedAssignments = assignments.OrderByDescending(a => a.Overdue == true)
                              .ThenByDescending(a => a.Priority)
                              .ThenBy(a => a.DueDate == null)
                              .ThenBy(a => a.DueDate)
                              .ThenBy(a => a.CreatedAt)
                              .ToList();

             return OrderedAssignments.FindAll(a =>
                                                (!status.HasValue || a.Status == status.Value) &&
                                                (!priority.HasValue || a.Priority == priority.Value) &&
                                                (!overdue.HasValue || (a.DueDate.HasValue && a.DueDate < DateTime.UtcNow)) &&
                                                (string.IsNullOrEmpty(search) ||
                                                a.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                                a.Description.Contains(search, StringComparison.OrdinalIgnoreCase))
                                              );
        }

        public async Task<Assignment> GetAssignmentById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public void UpdateAssignment(Assignment assignment)
        {
            _repository.UpdateAssignment(assignment);
        }
    }
}
