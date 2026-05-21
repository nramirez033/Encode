namespace Encode.Application.DTO.Auth
{
    public class RegisterRequest
    {
        public string Nombre { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string Equipo { get; set; }
    }
}
