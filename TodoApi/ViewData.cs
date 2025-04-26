using System;
using Microsoft.EntityFrameworkCore;
using TodoApi.Infrastructure.Data;
using TodoApi.Core.Entities;

namespace TodoApi
{
    public class ViewData
    {
        public static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseSqlite("Data Source=Todo.db")
                .Options;

            using (var context = new TodoContext(options))
            {
                Console.WriteLine("All Todo Items:");
                Console.WriteLine("---------------");
                
                var todos = context.TodoItems.ToList();
                foreach (var todo in todos)
                {
                    Console.WriteLine($"ID: {todo.Id}");
                    Console.WriteLine($"Title: {todo.Title}");
                    Console.WriteLine($"Description: {todo.Description}");
                    Console.WriteLine($"Is Completed: {todo.IsCompleted}");
                    Console.WriteLine($"Created At: {todo.CreatedAt}");
                    Console.WriteLine($"Completed At: {todo.CompletedAt}");
                    Console.WriteLine("---------------");
                }
            }
        }
    }
} 