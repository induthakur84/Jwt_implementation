using Jwt_implementation.Dto;
using Jwt_implementation.Models;
using Jwt_implementation.Services.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Jwt_implementation.Services
{

    //business logic for user registration
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public UserService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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



        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {

            //find user by username

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == loginDto.Username);


            //if user not found then we can throw the error

            if (user == null)
                throw new Exception("User not found");


            var token = GenerateToken(user);




            return new LoginResponseDto
            {
                Token = token,
                User = new UserResponseDto
                {
                    Name = user.Name,
                    Username = user.Username,
                    Age = user.Age,
                    Role = user.Role
                }
            };
        }


        #region Private method

        private string GenerateToken(User user)
        {


            // here we need to read jwt setting from appsettings.json file


            var jwtSettings = _configuration.GetSection("Jwt");



            // here we convert the secret key to byte array 


            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));



            //create signing credentials using the secret key and the hashing algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);



            //payload for the token (claims)

            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("Username", user.Username),
                new Claim("Age", user.Age.ToString()),  
            };


            //signature

            //create the token
            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            //convert the token object to string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        #endregion
    }
}
