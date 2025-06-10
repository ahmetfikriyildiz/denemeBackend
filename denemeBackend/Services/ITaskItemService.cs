using denemeBackend.DTOs;

namespace denemeBackend.Services
{
    public interface ITaskItemService
    {
        Task<TaskItemDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskItemDto>> GetAllAsync();
        Task<IEnumerable<TaskItemDto>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<TaskItemDto>> GetByUserIdAsync(Guid userId);
        Task<TaskItemDto> CreateAsync(Guid userId, CreateTaskItemDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateTaskItemDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
} 