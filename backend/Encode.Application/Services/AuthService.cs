using Encode.Application.DTO.Auth;
using Encode.Application.Interfaces;
using Encode.Domain.Entities;
using Encode.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IJwtService _jwt;

    public AuthService(AppDbContext db, IJwtService jwt)
    {
        _db = db;
        _jwt = jwt;
    }

    public async Task<string> Register(RegisterRequest request)
    {
        var exists = await _db.Users.AnyAsync(x => x.Email == request.Email);

        if (exists)
        {
            throw new Exception("Email already exists");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),

            Equipo = request.Equipo
        };

        await _db.Users.AddAsync(user);

        await _db.SaveChangesAsync();

        return _jwt.Generate(user);
    }

    public async Task<string> Login(LoginRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (user is null)
        {
            throw new Exception("Invalid credentials");
        }

        var validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

        if (!validPassword)
        {
            throw new Exception("Invalid credentials");
        }

        return _jwt.Generate(user);
    }
}