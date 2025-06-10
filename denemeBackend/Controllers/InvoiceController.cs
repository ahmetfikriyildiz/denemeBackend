using Microsoft.AspNetCore.Mvc;
using denemeBackend.DTOs;
using denemeBackend.Services;

namespace denemeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceDto>> GetById(Guid id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAll()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetByClientId(Guid clientId)
        {
            var invoices = await _invoiceService.GetByClientIdAsync(clientId);
            return Ok(invoices);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetByUserId(Guid userId)
        {
            var invoices = await _invoiceService.GetByUserIdAsync(userId);
            return Ok(invoices);
        }

        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> Create([FromBody] CreateInvoiceDto dto)
        {
            // TODO: Get actual user ID from authentication
            var userId = Guid.NewGuid(); // Temporary for testing
            var invoice = await _invoiceService.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInvoiceDto dto)
        {
            var success = await _invoiceService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _invoiceService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
} 