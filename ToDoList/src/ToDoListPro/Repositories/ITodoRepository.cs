using ToDoListPro.Entities;

namespace ToDoListPro.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(long id);
        Task AddAsync(Todo item);
        Task UpdateAsync(Todo item);
        Task DeleteAsync(long id);
    }
}