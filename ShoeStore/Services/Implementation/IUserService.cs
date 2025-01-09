using ShoeStore.DTO;
using ShoeStore.Entities;

namespace ShoeStore.Services.Implementation
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task AddUserAsync(UserDTO userDto);
        Task UpdateUserAsync(UserDTO userDto);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> RegisterUser(RegisterDTO registerDto);
    }
}
