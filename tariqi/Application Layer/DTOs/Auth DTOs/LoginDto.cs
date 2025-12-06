namespace tariqi.Application_Layer.DTOs.Auth_DTOs
{
    public class LoginDto
    {
        public string Identifier { get; set; } = string.Empty; // ممكن يكون Email أو PhoneNumber
        public string Password { get; set; } = string.Empty;
    }
}
