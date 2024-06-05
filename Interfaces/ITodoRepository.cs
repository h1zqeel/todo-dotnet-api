using todo_api.Dtos.Todo;
using todo_api.Models;

namespace todo_api.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync();
        Task<Todo?> GetByIdAsync(int id);
        Task<Todo> CreateAsync(Todo todo);
        Task<Todo?> UpdateAsync(int id, UpdateTodoRequestDto todoDto);
        Task<Todo?> DeleteAsync(int id);
    }
}