using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Models;
using TaskApi.Services;

[Route("api/[controller]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly TaskContext _context;
    private readonly LogService _logService;

    public TasksController(TaskContext context, LogService logService)
    {
        _context = context;
        _logService = logService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ApiTask>>> GetTasks()
    {
        _logService.Log(Request.Headers["Developer-Name"], "GetTasks called");
        return await _context.Tasks.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiTask>> GetTask(int id)
    {
        _logService.Log(Request.Headers["Developer-Name"], $"GetTask called for ID: {id}");
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return task;
    }

    [HttpPost]
    public async Task<ActionResult<ApiTask>> PostTask(ApiTask task)
    {
        _logService.Log(Request.Headers["Developer-Name"], "PostTask called");
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTask(int id, ApiTask task)
    {
        _logService.Log(Request.Headers["Developer-Name"], $"PutTask called for ID: {id}");
        if (id != task.Id)
        {
            return BadRequest();
        }

        _context.Entry(task).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TaskExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        _logService.Log(Request.Headers["Developer-Name"], $"DeleteTask called for ID: {id}");
        var task = await _context.Tasks.FindAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TaskExists(int id)
    {
        return _context.Tasks.Any(e => e.Id == id);
    }
}
