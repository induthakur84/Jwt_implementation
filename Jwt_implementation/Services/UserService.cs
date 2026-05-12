using Jwt_implementation.Dto;
using Jwt_implementation.Models;
using Jwt_implementation.Services.IServices;

namespace Jwt_implementation.Services
{

    //business logic for user registration
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<UserResponseDto> Register(UserRegisterDto userRegisterDto)
        {
            //create a new user object
            var user = new User
            {
                Name = userRegisterDto.Name,
                Username = userRegisterDto.Username,
                Password = userRegisterDto.Password,
                Age = userRegisterDto.Age,
                Role = userRegisterDto.Role
            };
            //add the user to the database


            await _context.Users.AddAsync(user);
            // here we can save changes asynchonously to the database
            await _context.SaveChangesAsync();
            //return the save user data (without password)

            return new UserResponseDto
            {
               // Id = user.Id,
                Name = user.Name,
                Username = user.Username,
                Age = user.Age,
                Role = user.Role
            };
        }
    }
}
