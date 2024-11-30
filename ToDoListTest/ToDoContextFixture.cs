using Microsoft.EntityFrameworkCore;

namespace ToDoListTest;

public class ToDoContextFixture : IDisposable
{
    public ToDoContext Context { get; private set; }

    public ToDoContextFixture()
    {
        var options = new DbContextOptionsBuilder<ToDoContext>()
            .UseSqlServer("Server=172.29.60.101,1466;Database=app;User=sa;Password=P@55word;TrustServerCertificate=True")
            .Options;

        Context = new ToDoContext(options);

        // 初始化資料庫結構
        Context.Database.EnsureCreated();

        // 添加測試數據
        Context.ToDo.AddRange(
            new ToDo { Title = "Test Task 1", IsCompleted = false },
            new ToDo { Title = "Test Task 2", IsCompleted = true }
        );

        Context.SaveChanges();
    }

    public void Dispose()
    {
        // 測試完成後刪除數據
        Context.Database.ExecuteSqlRaw("DELETE FROM ToDo");
        Context.Dispose();
    }
}