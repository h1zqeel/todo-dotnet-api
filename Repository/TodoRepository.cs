using todo_api.Models;
using todo_api.Data;
using todo_api.Dtos.Todo;
using todo_api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace todo_api.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDBContext _context;
        public TodoRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Todo> CreateAsync(Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<Todo?> DeleteAsync(int id, string userId)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if(todo == null) {
                return null;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<List<Todo>> GetAllAsync(string userId)
        {
            return await _context.Todos.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id, string userId)
        {
            return await _context.Todos.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
        }

        public async Task<Todo?> UpdateAsync(int id, string userId, UpdateTodoRequestDto todoDto)
        {
            var existingTodo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

            if(existingTodo == null) {
                return null;
            }

            existingTodo.Task = todoDto.Task;
            existingTodo.IsCompleted = todoDto.IsCompleted;

            await _context.SaveChangesAsync();

            return existingTodo;
        }
    }
}