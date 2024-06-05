using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_api.Data;
using todo_api.Dtos.Todo;
using todo_api.Interfaces;
using todo_api.Mappers;

namespace todo_api.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class Todo : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ITodoRepository _todoRepository;
        public Todo(ApplicationDBContext dbcontext, ITodoRepository todoRepository)
        {
            _dbContext = dbcontext;
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            var todos = await _todoRepository.GetAllAsync();
            var todosDto = todos.Select(t=> t.ToTodoDto());

            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var todo = await _todoRepository.GetByIdAsync(id);

            if(todo == null) {
                return NotFound();
            }

            return Ok(todo.ToTodoDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequestDto todoDto) {
            var todo = todoDto.ToTodoModel();

            await _dbContext.Todos.AddAsync(todo);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTodoRequestDto todoDto) {
            var todo = await _todoRepository.UpdateAsync(id, todoDto);

            if(todo == null) {
                return NotFound();
            }

            return Ok(todo.ToTodoDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var todo = await _todoRepository.DeleteAsync(id);

            if(todo == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}