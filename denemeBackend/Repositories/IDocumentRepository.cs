using denemeBackend.Models;

namespace denemeBackend.Repositories
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(Guid id);
        Task<IEnumerable<Document>> GetAllAsync();
        Task<IEnumerable<Document>> GetByClientIdAsync(Guid clientId);
        Task AddAsync(Document document);
        void Update(Document document);
        void Delete(Document document);
        Task SaveChangesAsync();
    }
} 