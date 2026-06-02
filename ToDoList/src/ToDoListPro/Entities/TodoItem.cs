namespace TodoApi.Entities;

public enum Priority { Low = 0, Medium = 1, High = 2 }

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public Priority Priority { get; set; } = Priority.Medium;
    public string? Category { get; set; }
    public DateTime? DueDate { get; set; }
    public double EstimatedHours { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime? EditedTime { get; set; }
}