using TodoApi.DTOs;
using TodoApi.Entities;

namespace TodoApi.Mapping;

public static class TodoMapper
{
    public static TodoItem ToEntity(TodoCreateDto d) => new()
    {
        Title = d.Title,
        Description = d.Description,
        Priority = d.Priority,
        Category = d.Category,
        DueDate = d.DueDate,
        EstimatedHours = d.EstimatedHours,
        IsCompleted = false
    };

    public static TodoItem ToEntity(TodoUpdateDto d, int id) => new()
    {
        Id = id,
        Title = d.Title,
        Description = d.Description,
        IsCompleted = d.IsCompleted,
        Priority = d.Priority,
        Category = d.Category,
        DueDate = d.DueDate,
        EstimatedHours = d.EstimatedHours
    };

    public static TodoResponseDto ToResponse(TodoItem e) => new()
    {
        Id = e.Id,
        Title = e.Title,
        Description = e.Description,
        IsCompleted = e.IsCompleted,
        Priority = e.Priority,
        Category = e.Category,
        DueDate = e.DueDate,
        EstimatedHours = e.EstimatedHours,
        CreatedTime = e.CreatedTime,
        EditedTime = e.EditedTime
    };
}