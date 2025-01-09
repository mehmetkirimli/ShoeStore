using AutoMapper;
using ShoeStore.DTO;
using ShoeStore.Entities;
using ShoeStore.Repositories.Implementation;
using ShoeStore.Services.Implementation;

namespace ShoeStore.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository , IMapper mapper)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        public async Task AddUserAsync(UserDTO userDto)
        {
            //Todo : Bu metod admin için kullanılabilir, hızlı bir şekilde kullanıcı eklemek için
            await _userRepository.AddAsync(_mapper.Map<User>(userDto));
        }

        public async Task DeleteUserAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {   
            var list = await _userRepository.GetAllAsync();
            return (IEnumerable<UserDTO>)_mapper.Map<UserDTO>(list);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            await _userRepository.UpdateAsync(_mapper.Map<User>(userDto));
        }

        public async Task<UserDTO> RegisterUser(RegisterDTO registerDto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                PasswordHash = hashedPassword,  // Hashlenmiş şifreyi veritabanına kaydediyoruz
                DateCreated = DateTime.Now,
                LastLogin = DateTime.Now,
                Addresses = new List<Address>()  // Adres bilgisi olmayabilir
            };

            await _userRepository.AddAsync(user);
            return _mapper.Map<UserDTO>(user);
        }

        public bool ValidateUserPassword(string enteredPassword, string storedPasswordHash) // Kullanıcı girişinde şifre kontrolü
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPasswordHash);
        }

    }
}
