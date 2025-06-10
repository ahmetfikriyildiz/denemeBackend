using denemeBackend.Data;
using denemeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace denemeBackend.Repositories
{
    public class TaskItemRepository : ITaskItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id) =>
            await _context.Tasks.FindAsync(id);

        public async Task<IEnumerable<TaskItem>> GetAllAsync() =>
            await _context.Tasks.ToListAsync();

        public async Task<IEnumerable<TaskItem>> GetByClientIdAsync(Guid clientId) =>
            await _context.Tasks.Where(t => t.ClientId == clientId).ToListAsync();

        public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId) =>
            await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

        public async Task AddAsync(TaskItem task) =>
            await _context.Tasks.AddAsync(task);

        public void Update(TaskItem task) =>
            _context.Tasks.Update(task);

        public void Delete(TaskItem task) =>
            _context.Tasks.Remove(task);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
} 