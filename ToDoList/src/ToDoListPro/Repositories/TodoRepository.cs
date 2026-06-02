using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Entities;

namespace TodoApi.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;
    public TodoRepository(AppDbContext context) => _context = context;

    public async Task<TodoItem> AddAsync(TodoItem item)
    {
        await _context.TodoItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<(IReadOnlyList<TodoItem> Items, int TotalCount)> GetAllAsync(TodoFilterDto filter, TodoSortDto sort)
    {
        var query = _context.TodoItems.AsNoTracking().AsQueryable();

        if (filter.IsCompleted.HasValue)
            query = query.Where(t => t.IsCompleted == filter.IsCompleted.Value);
        if (filter.Priority.HasValue)
            query = query.Where(t => t.Priority == filter.Priority.Value);
        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(t => t.Category == filter.Category);
        if (filter.DueDate.HasValue)
            query = query.Where(t => t.DueDate.HasValue && t.DueDate.Value.Date == filter.DueDate.Value.Date);

        query = (sort.SortBy?.ToLower()) switch
        {
            "duedate" => sort.Descending ? query.OrderByDescending(t => t.DueDate) : query.OrderBy(t => t.DueDate),
            "priority" => sort.Descending ? query.OrderByDescending(t => t.Priority) : query.OrderBy(t => t.Priority),
            "title" => sort.Descending ? query.OrderByDescending(t => t.Title) : query.OrderBy(t => t.Title),
            _ => sort.Descending ? query.OrderByDescending(t => t.CreatedTime) : query.OrderBy(t => t.CreatedTime),
        };

        var totalCount = await query.CountAsync();
        var page = filter.PageNumber < 1 ? 1 : filter.PageNumber;
        var size = filter.PageSize < 1 ? 10 : filter.PageSize;
        var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();
        return (items, totalCount);
    }

    public async Task<TodoItem?> GetByIdAsync(int id) => await _context.TodoItems.FindAsync(id);

    public async Task<TodoItem?> UpdateAsync(TodoItem item)
    {
        var existing = await _context.TodoItems.FindAsync(item.Id);
        if (existing is null) return null;
        existing.Title = item.Title;
        existing.Description = item.Description;
        existing.IsCompleted = item.IsCompleted;
        existing.Priority = item.Priority;
        existing.Category = item.Category;
        existing.DueDate = item.DueDate;
        existing.EstimatedHours = item.EstimatedHours;
        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.TodoItems.FindAsync(id);
        if (existing is null) return false;
        _context.TodoItems.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IReadOnlyList<TodoItem>> GetAllNoPagingAsync() =>
        await _context.TodoItems.AsNoTracking().ToListAsync();

    public async Task<int> CountAsync(bool isCompleted) =>
        await _context.TodoItems.CountAsync(t => t.IsCompleted == isCompleted);

    public async Task<TodoItem?> GetNearestDueAsync() =>
        await _context.TodoItems.AsNoTracking()
            .Where(t => t.DueDate.HasValue && !t.IsCompleted)
            .OrderBy(t => t.DueDate).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<TodoItem>> SearchAsync(string keyword, bool inDescription)
    {
        var query = _context.TodoItems.AsNoTracking();
        query = inDescription
            ? query.Where(t => t.Description != null && t.Description.Contains(keyword))
            : query.Where(t => t.Title.Contains(keyword));
        return await query.ToListAsync();
    }
}