using TodoApi.Entities;
namespace TodoApi.DTOs;

public class TodoFilterDto
{
    public bool? IsCompleted { get; set; }
    public Priority? Priority { get; set; }
    public string? Category { get; set; }
    public DateTime? DueDate { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
