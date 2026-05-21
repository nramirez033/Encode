using TaskStatus = Encode.Domain.Enums.TaskStatus;

namespace Encode.Application.DTO.Tasks
{
    public class UpdateTaskStatusRequest
    {
        public TaskStatus Estado { get; set; }
    }
}
