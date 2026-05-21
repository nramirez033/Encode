namespace Encode.Application.DTOs.Auth;

public class UserMeResponse
{
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Equipo { get; set; } = default!;
}