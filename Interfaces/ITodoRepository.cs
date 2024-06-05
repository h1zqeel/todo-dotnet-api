using todo_api.Dtos.Todo;
using todo_api.Models;

namespace todo_api.Interfaces
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllAsync(string userId);
        Task<Todo?> GetByIdAsync(int id, string userId);
        Task<Todo> CreateAsync(Todo todo);
        Task<Todo?> UpdateAsync(int id, string userId, UpdateTodoRequestDto todoDto);
        Task<Todo?> DeleteAsync(int id, string userId);
    }
}