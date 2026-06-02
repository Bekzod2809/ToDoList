namespace ToDoListPro.DTOs
{
    public class TodoCreateDto
    {
        public long UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LevelName { get; set; }
    }
}
