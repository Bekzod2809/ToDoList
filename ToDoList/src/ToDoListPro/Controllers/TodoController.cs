using Microsoft.AspNetCore.Mvc;
using TodoApi.DTOs;
using TodoApi.Mapping;
using TodoApi.Repositories;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TodoController : ControllerBase
{
    private readonly ITodoRepository _repo;
    public TodoController(ITodoRepository repo) => _repo = repo;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] TodoFilterDto filter, [FromQuery] TodoSortDto sort)
    {
        var (items, total) = await _repo.GetAllAsync(filter, sort);
        return Ok(new { TotalCount = total, filter.PageNumber, filter.PageSize, Items = items.Select(TodoMapper.ToResponse) });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item is null) return NotFound($"Id={id} bo'yicha task topilmadi.");
        return Ok(TodoMapper.ToResponse(item));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TodoCreateDto dto)
    {
        var created = await _repo.AddAsync(TodoMapper.ToEntity(dto));
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, TodoMapper.ToResponse(created));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TodoUpdateDto dto)
    {
        var updated = await _repo.UpdateAsync(TodoMapper.ToEntity(dto, id));
        if (updated is null) return NotFound($"Id={id} bo'yicha task topilmadi.");
        return Ok(TodoMapper.ToResponse(updated));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (!await _repo.DeleteAsync(id)) return NotFound($"Id={id} bo'yicha task topilmadi.");
        return NoContent();
    }

    // ---------- BONUS ----------
    [HttpGet("stats/completed-count")]
    public async Task<IActionResult> CompletedCount() => Ok(new { CompletedCount = await _repo.CountAsync(true) });

    [HttpGet("stats/uncompleted-count")]
    public async Task<IActionResult> UncompletedCount() => Ok(new { UncompletedCount = await _repo.CountAsync(false) });

    [HttpGet("stats/nearest-due")]
    public async Task<IActionResult> NearestDue()
    {
        var item = await _repo.GetNearestDueAsync();
        if (item is null) return NotFound("Yaqin muddatli task topilmadi.");
        return Ok(TodoMapper.ToResponse(item));
    }

    [HttpGet("group/by-category")]
    public async Task<IActionResult> GroupByCategory()
    {
        var all = await _repo.GetAllNoPagingAsync();
        return Ok(all.GroupBy(t => t.Category ?? "Belgilanmagan")
            .Select(g => new { Category = g.Key, Count = g.Count(), Items = g.Select(TodoMapper.ToResponse) }));
    }

    [HttpGet("group/by-priority")]
    public async Task<IActionResult> GroupByPriority()
    {
        var all = await _repo.GetAllNoPagingAsync();
        return Ok(all.GroupBy(t => t.Priority)
            .Select(g => new { Priority = g.Key.ToString(), Count = g.Count(), Items = g.Select(TodoMapper.ToResponse) }));
    }

    [HttpPatch("{id:int}/toggle")]
    public async Task<IActionResult> Toggle(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        if (item is null) return NotFound($"Id={id} bo'yicha task topilmadi.");
        item.IsCompleted = !item.IsCompleted;
        var updated = await _repo.UpdateAsync(item);
        return Ok(TodoMapper.ToResponse(updated!));
    }

    [HttpGet("search/title")]
    public async Task<IActionResult> SearchByTitle([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return BadRequest("keyword bo'sh bo'lmasin.");
        return Ok((await _repo.SearchAsync(keyword, false)).Select(TodoMapper.ToResponse));
    }

    [HttpGet("search/description")]
    public async Task<IActionResult> SearchByDescription([FromQuery] string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword)) return BadRequest("keyword bo'sh bo'lmasin.");
        return Ok((await _repo.SearchAsync(keyword, true)).Select(TodoMapper.ToResponse));
    }
}