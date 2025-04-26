using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Core.Entities;
using TodoApi.Core.Interfaces;

namespace TodoApi.Application.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public async Task<TodoItem> GetTodoByIdAsync(int id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        public async Task<TodoItem> CreateTodoAsync(string title, string description)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            var todoItem = new TodoItem
            {
                Title = title,
                Description = description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            return await _todoRepository.AddAsync(todoItem);
        }

        public async Task UpdateTodoAsync(int id, string title, string description, bool isCompleted)
        {
            var todoItem = await _todoRepository.GetByIdAsync(id);
            if (todoItem == null)
                throw new ArgumentException("Todo item not found");

            todoItem.Title = title;
            todoItem.Description = description;
            todoItem.IsCompleted = isCompleted;
            if (isCompleted && !todoItem.CompletedAt.HasValue)
                todoItem.CompletedAt = DateTime.UtcNow;
            else if (!isCompleted)
                todoItem.CompletedAt = null;

            await _todoRepository.UpdateAsync(todoItem);
        }

        public async Task DeleteTodoAsync(int id)
        {
            await _todoRepository.DeleteAsync(id);
        }
    }
} 