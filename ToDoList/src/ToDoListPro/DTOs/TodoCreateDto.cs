namespace ToDoListPro.DTOs
{
    public class TodoCreateDto
    {
        public long TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LevelName { get; set; }
    }
}
