namespace ToDoListPro.DTOs
{
    public class TodoSortDto
    {
        
        public bool ? IsAscending { get; set; } = false;
        public long ? TodoId { get; set; }
        public long UserId { get; set; }
    }
}
