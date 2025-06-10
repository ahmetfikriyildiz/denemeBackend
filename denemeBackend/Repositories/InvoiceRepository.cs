using denemeBackend.Data;
using denemeBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace denemeBackend.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice?> GetByIdAsync(Guid id) =>
            await _context.Invoices.FindAsync(id);

        public async Task<IEnumerable<Invoice>> GetAllAsync() =>
            await _context.Invoices.ToListAsync();

        public async Task<IEnumerable<Invoice>> GetByClientIdAsync(Guid clientId) =>
            await _context.Invoices.Where(i => i.ClientId == clientId).ToListAsync();

        public async Task<IEnumerable<Invoice>> GetByUserIdAsync(Guid userId) =>
            await _context.Invoices.Where(i => i.UserId == userId).ToListAsync();

        public async Task AddAsync(Invoice invoice) =>
            await _context.Invoices.AddAsync(invoice);

        public void Update(Invoice invoice) =>
            _context.Invoices.Update(invoice);

        public void Delete(Invoice invoice) =>
            _context.Invoices.Remove(invoice);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
} 