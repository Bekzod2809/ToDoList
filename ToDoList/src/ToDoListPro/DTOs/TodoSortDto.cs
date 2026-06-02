namespace ToDoListPro.DTOs
{
    public class TodoSortDto
    {
        public string SortBy { get; set; } = "CreatedAt";
        public bool IsAscending { get; set; } = false;
    }
}
