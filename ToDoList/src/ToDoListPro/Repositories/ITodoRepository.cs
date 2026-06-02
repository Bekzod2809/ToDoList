using TodoApi.DTOs;
using TodoApi.Entities;

namespace TodoApi.Repositories;

public interface ITodoRepository
{
    Task<TodoItem> AddAsync(TodoItem item);
    Task<(IReadOnlyList<TodoItem> Items, int TotalCount)> GetAllAsync(TodoFilterDto filter, TodoSortDto sort);
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem?> UpdateAsync(TodoItem item);
    Task<bool> DeleteAsync(int id);

    // bonus
    Task<IReadOnlyList<TodoItem>> GetAllNoPagingAsync();
    Task<int> CountAsync(bool isCompleted);
    Task<TodoItem?> GetNearestDueAsync();
    Task<IReadOnlyList<TodoItem>> SearchAsync(string keyword, bool inDescription);
}