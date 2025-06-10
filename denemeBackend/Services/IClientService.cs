using denemeBackend.DTOs;

namespace denemeBackend.Services
{
    public interface IClientService
    {
        Task<ClientDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<IEnumerable<ClientDto>> GetByUserIdAsync(Guid userId);
        Task<ClientDto> CreateAsync(Guid userId, CreateClientDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateClientDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
} 