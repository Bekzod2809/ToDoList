namespace ToDoListPro.DTOs
{
    public class TodoFilterDto
    {
        public long? UserId { get; set; }
        public string? LevelName { get; set; }
        public bool? IsCompleted { get; set; }
        public string? SearchText { get; set; }
    }
}
