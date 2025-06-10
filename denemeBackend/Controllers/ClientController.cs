using Microsoft.AspNetCore.Mvc;
using denemeBackend.DTOs;
using denemeBackend.Services;
using denemeBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace denemeBackend.Controllers
{
    /// <summary>
    /// Controller for managing client-related operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ApplicationDbContext _context;

        public ClientController(IClientService clientService, ApplicationDbContext context)
        {
            _clientService = clientService;
            _context = context;
        }

        /// <summary>
        /// Gets a client by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the client.</param>
        /// <returns>The client if found; otherwise, returns NotFound.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientDto>> GetById(Guid id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null)
                return NotFound();

            return Ok(client);
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>A list of all clients.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
        {
            var clients = await _clientService.GetAllAsync();
            return Ok(clients);
        }

        /// <summary>
        /// Gets all clients associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A list of clients associated with the user.</returns>
        [HttpGet("user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<ClientDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetByUserId(Guid userId)
        {
            var clients = await _clientService.GetByUserIdAsync(userId);
            return Ok(clients);
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="dto">The client data to create.</param>
        /// <returns>The created client.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientDto>> Create([FromBody] CreateClientDto dto)
        {
            try
            {
                // Get the first user from the database or create one if none exists
                var user = await _context.Users.FirstOrDefaultAsync();
                if (user == null)
                {
                    user = new Models.User
                    {
                        Id = Guid.NewGuid(),
                        Email = "admin@example.com",
                        PasswordHash = "temp", // In a real app, this should be properly hashed
                        Name = "Admin User",
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }

                var client = await _clientService.CreateAsync(user.Id, dto);
                return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
                // Log the exception here
                return StatusCode(500, new { error = "An error occurred while creating the client.", details = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="id">The unique identifier of the client to update.</param>
        /// <param name="dto">The updated client data.</param>
        /// <returns>No content if successful; otherwise, returns NotFound.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClientDto dto)
        {
            var success = await _clientService.UpdateAsync(id, dto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a client.
        /// </summary>
        /// <param name="id">The unique identifier of the client to delete.</param>
        /// <returns>No content if successful; otherwise, returns NotFound.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _clientService.DeleteAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
} 