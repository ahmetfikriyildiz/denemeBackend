using denemeBackend.DTOs;
using denemeBackend.Models;
using denemeBackend.Repositories;

namespace denemeBackend.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDto?> GetByIdAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            return invoice == null ? null : new InvoiceDto(
                invoice.Id,
                invoice.ClientId,
                invoice.UserId,
                invoice.Amount,
                invoice.Currency,
                invoice.Description,
                invoice.IssuedAt,
                invoice.Paid
            );
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return invoices.Select(i => new InvoiceDto(
                i.Id,
                i.ClientId,
                i.UserId,
                i.Amount,
                i.Currency,
                i.Description,
                i.IssuedAt,
                i.Paid
            ));
        }

        public async Task<IEnumerable<InvoiceDto>> GetByClientIdAsync(Guid clientId)
        {
            var invoices = await _invoiceRepository.GetByClientIdAsync(clientId);
            return invoices.Select(i => new InvoiceDto(
                i.Id,
                i.ClientId,
                i.UserId,
                i.Amount,
                i.Currency,
                i.Description,
                i.IssuedAt,
                i.Paid
            ));
        }

        public async Task<IEnumerable<InvoiceDto>> GetByUserIdAsync(Guid userId)
        {
            var invoices = await _invoiceRepository.GetByUserIdAsync(userId);
            return invoices.Select(i => new InvoiceDto(
                i.Id,
                i.ClientId,
                i.UserId,
                i.Amount,
                i.Currency,
                i.Description,
                i.IssuedAt,
                i.Paid
            ));
        }

        public async Task<InvoiceDto> CreateAsync(Guid userId, CreateInvoiceDto dto)
        {
            var invoice = new Invoice
            {
                Id = Guid.NewGuid(),
                ClientId = dto.ClientId,
                UserId = userId,
                Amount = dto.Amount,
                Currency = dto.Currency,
                Description = dto.Description,
                IssuedAt = DateTime.UtcNow,
                Paid = false
            };

            await _invoiceRepository.AddAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();

            return new InvoiceDto(
                invoice.Id,
                invoice.ClientId,
                invoice.UserId,
                invoice.Amount,
                invoice.Currency,
                invoice.Description,
                invoice.IssuedAt,
                invoice.Paid
            );
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateInvoiceDto dto)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null) return false;

            invoice.Amount = dto.Amount;
            invoice.Currency = dto.Currency;
            invoice.Description = dto.Description;
            invoice.Paid = dto.Paid;

            _invoiceRepository.Update(invoice);
            await _invoiceRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null) return false;

            _invoiceRepository.Delete(invoice);
            await _invoiceRepository.SaveChangesAsync();
            return true;
        }
    }
} 