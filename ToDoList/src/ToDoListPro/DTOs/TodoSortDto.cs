using TodoApi.Entities;
namespace TodoApi.DTOs;

public class TodoSortDto
{
    public string? SortBy { get; set; } = "createdtime"; // createdtime|duedate|priority|title
    public bool Descending { get; set; } = false;
}