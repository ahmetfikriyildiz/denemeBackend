using Microsoft.AspNetCore.Mvc;
using denemeBackend.DTOs;
using denemeBackend.Services;

namespace denemeBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentService _documentService;

        public DocumentController(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetById(Guid id)
        {
            var document = await _documentService.GetByIdAsync(id);
            if (document == null)
                return NotFound();

            return Ok(document);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetAll()
        {
            var documents = await _documentService.GetAllAsync();
            return Ok(documents);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetByClientId(Guid clientId)
        {
            var documents = await _documentService.GetByClientIdAsync(clientId);
            return Ok(documents);
        }

        [HttpPost]
        public async Task<ActionResult<DocumentDto>> Create([FromBody] CreateDocumentDto dto)
        {
            var document = await _documentService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = document.Id }, document);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDocumentDto dto)
        {
            var success = await _documentService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _documentService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
} 