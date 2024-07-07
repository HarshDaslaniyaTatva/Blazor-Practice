

using WebApi.Dtos.Dtos;

namespace WebApi.Dtos.Response
{
    public class LoginResponseDto
    {
        public UserDto? User { get; set; }
        public string token { get; set; } = string.Empty;
    }
}
