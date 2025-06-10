using denemeBackend.Models;

namespace denemeBackend.Repositories
{
    public interface IInvoiceRepository
    {
        Task<Invoice?> GetByIdAsync(Guid id);
        Task<IEnumerable<Invoice>> GetAllAsync();
        Task<IEnumerable<Invoice>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<Invoice>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(Invoice invoice);
        Task SaveChangesAsync();
    }
} 