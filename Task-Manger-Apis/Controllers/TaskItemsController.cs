using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OritsoTaskManager.Data;
using OritsoTaskManager.Models;

namespace OritsoTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TaskItemsController(AppDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _context.TaskItems.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TaskItem task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Convert to UTC
            task.CreatedOn = DateTime.UtcNow;
            task.LastUpdatedOn = DateTime.UtcNow;

            if (task.DueDate.Kind == DateTimeKind.Unspecified)
            {
                task.DueDate = DateTime.SpecifyKind(task.DueDate, DateTimeKind.Utc);
            }

            // Optional: default values for update info if not passed
            if (string.IsNullOrEmpty(task.UpdatedByName)) task.UpdatedByName = task.CreatedByName;
            if (task.UpdatedById == 0) task.UpdatedById = task.CreatedById;

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItem updatedTask)
        {
            if (id != updatedTask.Id) return BadRequest();
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.DueDate = updatedTask.DueDate;
            task.Status = updatedTask.Status;
            task.Remarks = updatedTask.Remarks;
            task.LastUpdatedOn = DateTime.UtcNow;
            task.UpdatedById = updatedTask.UpdatedById;
            task.UpdatedByName = updatedTask.UpdatedByName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task == null) return NotFound();
            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string query)
        {
            var results = await _context.TaskItems
                .Where(t => t.Title.Contains(query) || t.Description.Contains(query))
                .ToListAsync();
            return Ok(results);
        }
    }
}

