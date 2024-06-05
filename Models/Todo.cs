using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_api.Models
{
	public class Todo
	{
		public int Id { get; set; }
		public string Task { get; set; }
		public bool IsCompleted { get; set; }
	}
}