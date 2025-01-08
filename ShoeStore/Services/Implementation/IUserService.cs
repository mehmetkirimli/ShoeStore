using ShoeStore.DTO;
using ShoeStore.Entities;

namespace ShoeStore.Services.Implementation
{
    public interface IUserService
    {
        Task<UserDTO> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<IEnumerable<User>> GetAllUsersAsync();

    }
}
