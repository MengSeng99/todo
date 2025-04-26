using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TodoApi.Application.Services;
using TodoApi.Core.Entities;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all todo items")]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
        {
            var todos = await _todoService.GetAllTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get todo item by ID")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
                return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new todo item")]
        public async Task<ActionResult<TodoItem>> Create([FromBody] TodoItem todoItem)
        {
            try
            {
                var createdTodo = await _todoService.CreateTodoAsync(todoItem.Title, todoItem.Description);
                return CreatedAtAction(nameof(GetById), new { id = createdTodo.Id }, createdTodo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a todo item")]
        public async Task<IActionResult> Update(int id, [FromBody] TodoItem todoItem)
        {
            try
            {
                await _todoService.UpdateTodoAsync(id, todoItem.Title, todoItem.Description, todoItem.IsCompleted);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a todo item")]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoService.DeleteTodoAsync(id);
            return NoContent();
        }
    }
} 