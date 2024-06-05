using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_api.Dtos.Todo
{
    public class TodoDto
    {
        public int Id { get; set; }
		public string Task { get; set; }
    }
}