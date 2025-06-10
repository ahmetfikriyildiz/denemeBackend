using denemeBackend.DTOs;
using denemeBackend.Models;
using denemeBackend.Repositories;

namespace denemeBackend.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientDto?> GetByIdAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            return client == null ? null : new ClientDto(
                client.Id,
                client.UserId,
                client.Name,
                client.Email,
                client.Phone,
                client.CompanyName,
                client.Notes,
                client.CreatedAt
            );
        }

        public async Task<IEnumerable<ClientDto>> GetAllAsync()
        {
            var clients = await _clientRepository.GetAllAsync();
            return clients.Select(c => new ClientDto(
                c.Id,
                c.UserId,
                c.Name,
                c.Email,
                c.Phone,
                c.CompanyName,
                c.Notes,
                c.CreatedAt
            ));
        }

        public async Task<IEnumerable<ClientDto>> GetByUserIdAsync(Guid userId)
        {
            var clients = await _clientRepository.GetByUserIdAsync(userId);
            return clients.Select(c => new ClientDto(
                c.Id,
                c.UserId,
                c.Name,
                c.Email,
                c.Phone,
                c.CompanyName,
                c.Notes,
                c.CreatedAt
            ));
        }

        public async Task<ClientDto> CreateAsync(Guid userId, CreateClientDto dto)
        {
            var client = new Client
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                CompanyName = dto.CompanyName,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow
            };

            await _clientRepository.AddAsync(client);
            await _clientRepository.SaveChangesAsync();

            return new ClientDto(
                client.Id,
                client.UserId,
                client.Name,
                client.Email,
                client.Phone,
                client.CompanyName,
                client.Notes,
                client.CreatedAt
            );
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateClientDto dto)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return false;

            client.Name = dto.Name;
            client.Email = dto.Email;
            client.Phone = dto.Phone;
            client.CompanyName = dto.CompanyName;
            client.Notes = dto.Notes;

            _clientRepository.Update(client);
            await _clientRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return false;

            _clientRepository.Delete(client);
            await _clientRepository.SaveChangesAsync();
            return true;
        }
    }
} 