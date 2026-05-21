using Microsoft.EntityFrameworkCore;
using Encode.Domain.Entities;
using TaskStatus = Encode.Domain.Enums.TaskStatus;

namespace Encode.Infrastructure.Data;

public static class SeedData
{
    public static async Task InitializeAsync(
        AppDbContext context)
    {
        if (await context.Users.AnyAsync())
        {
            return;
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Nombre = "Admin",
            Email = "admin@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            Equipo = "equipo1"
        };

        var user2 = new User
        {
            Id = Guid.NewGuid(),
            Nombre = "Admin 2",
            Email = "admin2@test.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
            Equipo = "equipo2"
        };

        var task = new TaskItem
        {
            Id = Guid.NewGuid(),
            Titulo = "Tarea Demo",
            Descripcion = "Primera tarea del sistema",

            Estado = TaskStatus.Pending,
            Equipo = "equipo1",
            CreadoPorUsuarioId =user.Id,
            FechaCreado = DateTime.UtcNow
        };

        await context.Users.AddAsync(user);
        await context.Users.AddAsync(user2);
        await context.Tasks.AddAsync(task);
        await context.SaveChangesAsync();
    }
}