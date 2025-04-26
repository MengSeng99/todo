using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Core.Entities;

namespace TodoApi.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem> GetByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<TodoItem> AddAsync(TodoItem todoItem);
        Task UpdateAsync(TodoItem todoItem);
        Task DeleteAsync(int id);
    }
} 