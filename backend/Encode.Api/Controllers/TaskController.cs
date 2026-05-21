using Encode.Application.DTO.Tasks;
using Encode.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TaskManager.Api.Controllers;

[Authorize]
[ApiController]
[Route("tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Obtener tareas del equipo del usuario autenticado
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var teamId = GetTeamId();
        var tasks = await _taskService.GetTasks(teamId);
        return Ok(tasks);
    }

    /// <summary>
    /// Crear tarea
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var userId = GetUserId();
        var teamId = GetTeamId();
        var task = await _taskService.CreateTask(userId,teamId,request);
        return Ok(task);
    }

    /// <summary>
    /// Cambiar estado
    /// </summary>
    [HttpPut("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id,[FromBody] UpdateTaskStatusRequest request)
    {
        var teamId = GetTeamId();
        await _taskService.UpdateStatus(id, teamId, request);
        return NoContent();
    }

    /// <summary>
    /// Eliminar tarea (solo creador)
    /// </summary>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var userId = GetUserId();
        await _taskService.Delete(id, userId);
        return NoContent();
    }

    private Guid GetUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new UnauthorizedAccessException("Invalid token");
        }

        return Guid.Parse(userId);
    }

    private string GetTeamId()
    {
        var teamId = User.FindFirst("Equipo")?.Value;

        if (string.IsNullOrWhiteSpace(teamId))
        {
            throw new UnauthorizedAccessException("Invalid token");
        }

        return teamId;
    }
}