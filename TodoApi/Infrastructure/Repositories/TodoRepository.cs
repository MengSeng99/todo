using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Core.Entities;
using TodoApi.Core.Interfaces;
using TodoApi.Infrastructure.Data;

namespace TodoApi.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem> AddAsync(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task UpdateAsync(TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.TodoItems.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
        }
    }
} 