using TodoApi.Entities;

namespace TodoApi.DTOs;

public class TodoResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; }
    public string? Category { get; set; }
    public DateTime? DueDate { get; set; }
    public double EstimatedHours { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? EditedTime { get; set; }
}