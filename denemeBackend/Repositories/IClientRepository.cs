using denemeBackend.Models;

namespace denemeBackend.Repositories
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(Guid id);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<IEnumerable<Client>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Client client);
        void Update(Client client);
        void Delete(Client client);
        Task SaveChangesAsync();
    }
} 