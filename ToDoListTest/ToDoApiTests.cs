using Microsoft.EntityFrameworkCore;

namespace ToDoListTest;

public class ToDoApiTests : IClassFixture<ToDoContextFixture>
{
    private readonly ToDoContext _context;

    public ToDoApiTests(ToDoContextFixture fixture)
    {
        _context = fixture.Context;
    }

    [Fact]
    public async Task GetAllTodos_ShouldReturnAllTodos()
    {
        // Act
        var todos = await _context.ToDo.ToListAsync();

        // Assert
        Assert.NotNull(todos); // 確保 todos 不為 null
        Assert.Equal(2, todos.Count); // 檢查集合的長度是否為 2
        Assert.Contains(todos, todo => todo.Title == "Test Task 1"); // 檢查是否包含特定項目
        Assert.Contains(todos, todo => todo.Title == "Test Task 2"); // 檢查是否包含特定項目
    }
}