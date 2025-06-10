using denemeBackend.DTOs;

namespace denemeBackend.Services
{
    public interface IDocumentService
    {
        Task<DocumentDto?> GetByIdAsync(Guid id);
        Task<IEnumerable<DocumentDto>> GetAllAsync();
        Task<IEnumerable<DocumentDto>> GetByClientIdAsync(Guid clientId);
        Task<DocumentDto> CreateAsync(CreateDocumentDto dto);
        Task<bool> UpdateAsync(Guid id, UpdateDocumentDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
} 