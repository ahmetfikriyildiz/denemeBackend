using denemeBackend.DTOs;
using denemeBackend.Models;
using denemeBackend.Repositories;

namespace denemeBackend.Services
{
    public class TaskItemService : ITaskItemService
    {
        private readonly ITaskItemRepository _taskRepository;

        public TaskItemService(ITaskItemRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<TaskItemDto?> GetByIdAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task == null ? null : new TaskItemDto(
                task.Id,
                task.ClientId,
                task.UserId,
                task.Title,
                task.Description,
                task.Status,
                task.DueDate,
                task.CreatedAt
            );
        }

        public async Task<IEnumerable<TaskItemDto>> GetAllAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return tasks.Select(t => new TaskItemDto(
                t.Id,
                t.ClientId,
                t.UserId,
                t.Title,
                t.Description,
                t.Status,
                t.DueDate,
                t.CreatedAt
            ));
        }

        public async Task<IEnumerable<TaskItemDto>> GetByClientIdAsync(Guid clientId)
        {
            var tasks = await _taskRepository.GetByClientIdAsync(clientId);
            return tasks.Select(t => new TaskItemDto(
                t.Id,
                t.ClientId,
                t.UserId,
                t.Title,
                t.Description,
                t.Status,
                t.DueDate,
                t.CreatedAt
            ));
        }

        public async Task<IEnumerable<TaskItemDto>> GetByUserIdAsync(Guid userId)
        {
            var tasks = await _taskRepository.GetByUserIdAsync(userId);
            return tasks.Select(t => new TaskItemDto(
                t.Id,
                t.ClientId,
                t.UserId,
                t.Title,
                t.Description,
                t.Status,
                t.DueDate,
                t.CreatedAt
            ));
        }

        public async Task<TaskItemDto> CreateAsync(Guid userId, CreateTaskItemDto dto)
        {
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                ClientId = dto.ClientId,
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate,
                CreatedAt = DateTime.UtcNow
            };

            await _taskRepository.AddAsync(task);
            await _taskRepository.SaveChangesAsync();

            return new TaskItemDto(
                task.Id,
                task.ClientId,
                task.UserId,
                task.Title,
                task.Description,
                task.Status,
                task.DueDate,
                task.CreatedAt
            );
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateTaskItemDto dto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            task.Title = dto.Title;
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;

            _taskRepository.Update(task);
            await _taskRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null) return false;

            _taskRepository.Delete(task);
            await _taskRepository.SaveChangesAsync();
            return true;
        }
    }
} 