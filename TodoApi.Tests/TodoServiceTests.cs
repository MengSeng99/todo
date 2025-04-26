using System;
using System.Threading.Tasks;
using Moq;
using TodoApi.Core.Entities;
using TodoApi.Core.Interfaces;
using TodoApi.Application.Services;
using Xunit;

namespace TodoApi.Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _mockRepository;
        private readonly TodoService _todoService;

        public TodoServiceTests()
        {
            _mockRepository = new Mock<ITodoRepository>();
            _todoService = new TodoService(_mockRepository.Object);
        }

        [Fact]
        public async Task CreateTodo_WithValidTitle_ShouldCreateTodo()
        {
            // Arrange
            var title = "Test Todo";
            var description = "This is a test description";
            var expectedTodo = new TodoItem
            {
                Id = 1,
                Title = title,
                Description = description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow
            };

            _mockRepository.Setup(r => r.AddAsync(It.IsAny<TodoItem>()))
                .ReturnsAsync(expectedTodo);

            // Act
            var result = await _todoService.CreateTodoAsync(title, description);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(title, result.Title);
            Assert.Equal(description, result.Description);
            Assert.False(result.IsCompleted);
        }

        [Fact]
        public async Task CreateTodo_WithEmptyTitle_ShouldThrowException()
        {
            // Arrange
            var title = "";
            var description = "This is a test description";

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _todoService.CreateTodoAsync(title, description));
        }

        [Fact]
        public async Task UpdateTodo_WithValidId_ShouldUpdateTodo()
        {
            // Arrange
            var id = 1;
            var existingTodo = new TodoItem
            {
                Id = id,
                Title = "Original Title",
                Description = "Original Description",
                IsCompleted = false
            };

            _mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(existingTodo);

            var newTitle = "New Title";
            var newDescription = "New Description";
            var newIsCompleted = true;

            // Act
            await _todoService.UpdateTodoAsync(id, newTitle, newDescription, newIsCompleted);

            // Assert
            _mockRepository.Verify(r => r.UpdateAsync(It.Is<TodoItem>(t => 
                t.Id == id && 
                t.Title == newTitle && 
                t.Description == newDescription && 
                t.IsCompleted == newIsCompleted)), 
                Times.Once);
        }

        [Fact]
        public async Task UpdateTodo_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            var id = 999;
            _mockRepository.Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((TodoItem)null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => 
                _todoService.UpdateTodoAsync(id, "New Title", "New Description", true));
        }
    }
} 