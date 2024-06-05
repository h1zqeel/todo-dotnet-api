using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_api.Models;
using todo_api.Dtos.Todo;

namespace todo_api.Mappers
{
    public static class TodoMapper
    {
        public static TodoDto ToTodoDto(this Todo todoModel)
        {
            return new TodoDto
            {
                Id = todoModel.Id,
                Task = todoModel.Task
            };
        }

        public static Todo ToTodoModel(this CreateTodoRequestDto createTodoRequestDto)
        {
            return new Todo
            {
                Task = createTodoRequestDto.Task,
                IsCompleted = false
            };
        }
    }
}