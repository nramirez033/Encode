using Encode.Domain.Enums;
using TaskStatus = Encode.Domain.Enums.TaskStatus;

namespace Encode.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = default!;

    public string Descripcion { get; set; } = default!;

    public TaskStatus Estado { get; set; }

    public string Equipo { get; set; } = default!;

    public Guid CreadoPorUsuarioId { get; set; }

    public DateTime FechaCreado { get; set; }
}
