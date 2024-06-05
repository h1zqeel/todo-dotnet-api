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

        public async Task<Todo?> DeleteAsync(int id)
        {
            var todo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);

            if(todo == null) {
                return null;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<List<Todo>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<Todo?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<Todo?> UpdateAsync(int id, UpdateTodoRequestDto todoDto)
        {
            var existingTodo = await _context.Todos.FirstOrDefaultAsync(x => x.Id == id);

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