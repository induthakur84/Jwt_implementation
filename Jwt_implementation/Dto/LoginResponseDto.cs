namespace Jwt_implementation.Dto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public UserResponseDto User { get; set; }
    }
}
