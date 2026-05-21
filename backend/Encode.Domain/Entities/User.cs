namespace Encode.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Nombre { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public string Equipo { get; set; } = default!;
}