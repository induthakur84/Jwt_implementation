using Jwt_implementation.Dto;

namespace Jwt_implementation.Services.IServices
{

    //interface for user service
    //just declaring the method for user registration
    public interface IUserService
    {
        Task<UserResponseDto> Register(UserRegisterDto userRegisterDto);
    }
}
