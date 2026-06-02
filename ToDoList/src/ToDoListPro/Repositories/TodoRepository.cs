using Microsoft.EntityFrameworkCore;
using ToDoListPro.Data;
using ToDoListPro.Entities;

namespace ToDoListPro.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(long id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task AddAsync(Todo item)
        {
            await _context.Todos.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Todo item)
        {
            _context.Todos.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var item = await _context.Todos.FindAsync(id);
            if (item != null)
            {
                _context.Todos.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}