using denemeBackend.Models;

namespace denemeBackend.Repositories
{
    public interface ITaskItemRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<TaskItem>> GetByUserIdAsync(Guid userId);
        Task AddAsync(TaskItem task);
        void Update(TaskItem task);
        void Delete(TaskItem task);
        Task SaveChangesAsync();
    }
} 