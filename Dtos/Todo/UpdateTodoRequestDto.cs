using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_api.Dtos.Todo
{
    public class UpdateTodoRequestDto
    {
        
      public string Task { get; set; }
      public bool IsCompleted { get; set; }
    }
}