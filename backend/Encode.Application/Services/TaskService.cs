using Microsoft.EntityFrameworkCore;
using Encode.Application.Interfaces;
using Encode.Domain.Entities;
using Encode.Domain.Enums;
using Encode.Infrastructure.Data;
using Encode.Application.DTO.Tasks;
using TaskStatus = Encode.Domain.Enums.TaskStatus;

namespace Encode.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly AppDbContext _db;

        public TaskService(
            AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TaskItem>> GetTasks(string teamName)
        {
            return await _db.Tasks
                .Where(x => x.Equipo == teamName)
                .ToListAsync();
        }

        public async Task<TaskItem>
        CreateTask(Guid userId, string teamId, CreateTaskRequest request)
        {
            var task = new TaskItem
                {
                    Id = Guid.NewGuid(),
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    Estado = TaskStatus.Pending,
                    Equipo = teamId,
                    CreadoPorUsuarioId = userId,
                    FechaCreado = DateTime.UtcNow
                };

            await _db.Tasks.AddAsync(task);
            await _db.SaveChangesAsync();

            return task;
        }

        public async Task UpdateStatus(Guid taskId,string teamId, UpdateTaskStatusRequest request)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(x =>
                        x.Id == taskId &&
                        x.Equipo == teamId);

            if (task is null)
            {
                throw new Exception("Task not found");
            }

            task.Estado = request.Estado;

            await _db.SaveChangesAsync();
        }

        public async Task Delete(Guid taskId, Guid userId)
        {
            var task = await _db.Tasks.FirstOrDefaultAsync(x =>
                        x.Id == taskId);

            if (task is null)
            {
                throw new Exception("Task not found");
            }

            if (task.CreadoPorUsuarioId != userId)
            {
                throw new UnauthorizedAccessException( "You can only delete your own tasks");
            }

            _db.Tasks.Remove(task);

            await _db.SaveChangesAsync();
        }
    }
}
