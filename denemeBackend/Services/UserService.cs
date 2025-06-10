using denemeBackend.DTOs;
using denemeBackend.Models;
using denemeBackend.Repositories;

namespace denemeBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : new UserDto(user.Id, user.Email, user.Name, user.CreatedAt);
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto(u.Id, u.Email, u.Name, u.CreatedAt));
        }

        public async Task<UserDto> CreateAsync(CreateUserDto dto)
        {
            // Hash password here in real app!
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = dto.Email,
                PasswordHash = dto.Password, // Replace with hash in production!
                Name = dto.Name,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return new UserDto(user.Id, user.Email, user.Name, user.CreatedAt);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            user.Name = dto.Name;
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;
            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
} 