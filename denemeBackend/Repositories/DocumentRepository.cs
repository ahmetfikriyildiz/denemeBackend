using denemeBackend.Data;
using denemeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace denemeBackend.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Document?> GetByIdAsync(Guid id) =>
            await _context.Documents.FindAsync(id);

        public async Task<IEnumerable<Document>> GetAllAsync() =>
            await _context.Documents.ToListAsync();

        public async Task<IEnumerable<Document>> GetByClientIdAsync(Guid clientId) =>
            await _context.Documents.Where(d => d.ClientId == clientId).ToListAsync();

        public async Task AddAsync(Document document) =>
            await _context.Documents.AddAsync(document);

        public void Update(Document document) =>
            _context.Documents.Update(document);

        public void Delete(Document document) =>
            _context.Documents.Remove(document);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
} 