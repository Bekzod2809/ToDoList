namespace ToDoListPro.DTOs
{
    public class TodoUpdateDto
    {
        public long TodoId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string LevelName { get; set; }
        public bool IsCompleted { get; set; }
    }
}
