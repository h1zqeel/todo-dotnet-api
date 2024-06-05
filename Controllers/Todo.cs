using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_api.Data;
using todo_api.Dtos.Todo;
using todo_api.Interfaces;
using todo_api.Mappers;
using todo_api.Models;

namespace todo_api.Controllers
{
    [Route("api/todo")]
    [ApiController]
    public class Todo : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ITodoRepository _todoRepository;
        private readonly UserManager<User> _userManager;
        public Todo(ApplicationDBContext dbcontext, ITodoRepository todoRepository, UserManager<User> userManager)
        {
            _dbContext = dbcontext;
            _todoRepository = todoRepository;
            _userManager = userManager;
        }

        private User GetCurrentUser()
        {
            return HttpContext.Items["CurrentUser"] as User;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [RequiresUserAuthentication]
        public async Task<IActionResult> GetAll() {
            var user = GetCurrentUser();

            var todos = await _todoRepository.GetAllAsync(user.Id);
            var todosDto = todos.Select(t=> t.ToTodoDto());

            return Ok(todosDto);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [RequiresUserAuthentication]
        public async Task<IActionResult> GetById([FromRoute] int id) {
            var user = GetCurrentUser();

            var todo = await _todoRepository.GetByIdAsync(id, user.Id);

            if(todo == null) {
                return NotFound();
            }

            return Ok(todo.ToTodoDto());
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [RequiresUserAuthentication]
        public async Task<IActionResult> Create([FromBody] CreateTodoRequestDto todoDto) {
            var user = GetCurrentUser();

            var todo = todoDto.ToTodoModel(user.Id);

            await _dbContext.Todos.AddAsync(todo);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo.ToTodoDto());
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [RequiresUserAuthentication]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTodoRequestDto todoDto) {
            var user = GetCurrentUser();

            var todo = await _todoRepository.UpdateAsync(id, user.Id, todoDto);

            if(todo == null) {
                return NotFound();
            }

            return Ok(todo.ToTodoDto());
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [RequiresUserAuthentication]
        public async Task<IActionResult> Delete([FromRoute] int id) {
            var user = GetCurrentUser();

            var todo = await _todoRepository.DeleteAsync(id, user.Id);

            if(todo == null) {
                return NotFound();
            }

            return NoContent();
        }
    }
}