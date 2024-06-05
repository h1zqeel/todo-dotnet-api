using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace todo_api.Models
{
	public class Todo
	{
		public int Id { get; set; }
		public string Task { get; set; }
		public bool IsCompleted { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
	}
}