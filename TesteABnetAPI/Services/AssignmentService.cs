using BackEndTesteABnet.Entities;
using BackEndTesteABnet.Entities.Enums;
using BackEndTesteABnet.Repository;
using TesteABnetAPI.Interfaces;

public class AssignmentService : AssignmentServiceInterface
{
    private readonly AssignmentRepository _repository;

    public AssignmentService(AssignmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> CreateAssignment(Assignment assignment)
    {
        if (string.IsNullOrWhiteSpace(assignment.Title))
            throw new ArgumentException("Title is required");

        var assignments = await GetAllAssignment(null, null, null, null);

        if (assignments.Any(a => a.Title == assignment.Title && a.Status == Status.PENDING))
            throw new ArgumentException("Cannot create duplicate pending assignment");

        await _repository.AddAsync(assignment);

        return true;
    }

    public async Task<bool> DeleteAssignment(int id)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            return false;

        await _repository.DeleteAssignment(existing);

        return true;
    }

    public async Task<bool> UpdateAssignment(Assignment assignment)
    {
        var existing = await _repository.GetByIdAsync(assignment.Id);

        if (existing == null)
            return false;

        await _repository.UpdateAssignment(assignment);

        return true;
    }

    public async Task<Assignment?> GetAssignmentById(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<Assignment>> GetAllAssignment(
        Status? status,
        Priority? priority,
        bool? overdue,
        string? search)
    {
        var assignments = await _repository.GetAllAsync();

        var ordered = assignments
            .OrderByDescending(a => a.Overdue)
            .ThenByDescending(a => a.Priority)
            .ThenBy(a => a.DueDate == null)
            .ThenBy(a => a.DueDate)
            .ThenBy(a => a.CreatedAt)
            .ToList();

        return ordered.Where(a =>
            (!status.HasValue || a.Status == status.Value) &&
            (!priority.HasValue || a.Priority == priority.Value) &&
            (!overdue.HasValue || (a.DueDate.HasValue && a.DueDate < DateTime.UtcNow)) &&
            (string.IsNullOrEmpty(search) ||
             a.Title.Contains(search, StringComparison.OrdinalIgnoreCase) ||
             (a.Description != null && a.Description.Contains(search, StringComparison.OrdinalIgnoreCase)))
        ).ToList();
    }
}