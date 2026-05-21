using Encode.Application.DTO.Tasks;
using Encode.Domain.Entities;

namespace Encode.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetTasks(
            string teamId);

        Task<TaskItem> CreateTask(
            Guid userId,
            string teamId,
            CreateTaskRequest request);

        Task UpdateStatus(
            Guid taskId,
            string teamId,
            UpdateTaskStatusRequest request);

        Task Delete(
            Guid taskId,
            Guid userId);
    }
}
