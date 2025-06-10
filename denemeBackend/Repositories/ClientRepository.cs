using denemeBackend.Data;
using denemeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace denemeBackend.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetByIdAsync(Guid id) =>
            await _context.Clients.FindAsync(id);

        public async Task<IEnumerable<Client>> GetAllAsync() =>
            await _context.Clients.ToListAsync();

        public async Task<IEnumerable<Client>> GetByUserIdAsync(Guid userId) =>
            await _context.Clients.Where(c => c.UserId == userId).ToListAsync();

        public async Task AddAsync(Client client) =>
            await _context.Clients.AddAsync(client);

        public void Update(Client client) =>
            _context.Clients.Update(client);

        public void Delete(Client client) =>
            _context.Clients.Remove(client);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
} 