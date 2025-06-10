using denemeBackend.DTOs;

namespace denemeBackend.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<IEnumerable<InvoiceDto>> GetByClientIdAsync(Guid clientId);
        Task<IEnumerable<InvoiceDto>> GetByUserIdAsync(Guid userId);
        Task<InvoiceDto> CreateAsync(Guid userId, CreateInvoiceDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateInvoiceDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
} 