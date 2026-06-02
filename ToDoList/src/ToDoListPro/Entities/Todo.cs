namespace ToDoListPro.Entities;

public class Todo
{
    public long TodoId { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string LevelName { get; set; }
    public bool IsCompleted { get; set; } = false;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
