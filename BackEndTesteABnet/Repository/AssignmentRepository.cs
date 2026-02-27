using BackEndTesteABnet.Data;
using BackEndTesteABnet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTesteABnet.Repository
{
    public class AssignmentRepository
    {
        private readonly AppDbContext _context;

        public AssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Assignment>> GetAllAsync()
        {
            return _context.Assignments.ToList();
        }

        public async Task AddAsync(Assignment assignment)
        {
            _context.Assignments.Add(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAssignment(Assignment assignment)
        {
            _context.Assignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAssignment(Assignment assignment)
        {
            _context.Assignments.Remove(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task<Assignment> GetByIdAsync(int id)
        {
            return _context.Assignments.Find(id);
        }
    }
}
