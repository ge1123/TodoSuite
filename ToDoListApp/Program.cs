using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 註冊 DbContext
builder.Services.AddDbContext<ToDoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 註冊 Swagger 服務
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ToDo API",
        Version = "v1",
        Description = "A simple ToDo API"
    });
});

var app = builder.Build();

// 啟用 Swagger 中間件和 Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo API V1");
    c.RoutePrefix = string.Empty; // 將 Swagger UI 設為根路徑 (可選)
});


// 建立 API 路由
app.MapGet("/api/todos", async (ToDoContext db) => await db.ToDo.ToListAsync());
app.MapGet("/api/todos/{id}", async (int id, ToDoContext db) =>
    await db.ToDo.FindAsync(id) is ToDo todo ? Results.Ok(todo) : Results.NotFound());
app.MapPost("/api/todos", async (ToDo todo, ToDoContext db) =>
{
    db.ToDo.Add(todo);
    await db.SaveChangesAsync();
    return Results.Created($"/api/todos/{todo.Id}", todo);
});
app.MapPut("/api/todos/{id}", async (int id, ToDo updatedTodo, ToDoContext db) =>
{
    var todo = await db.ToDo.FindAsync(id);
    if (todo is null) return Results.NotFound();

    todo.Title = updatedTodo.Title;
    todo.IsCompleted = updatedTodo.IsCompleted;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/api/todos/{id}", async (int id, ToDoContext db) =>
{
    var todo = await db.ToDo.FindAsync(id);
    if (todo is null) return Results.NotFound();

    db.ToDo.Remove(todo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();

public class ToDo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
}

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
    {
    }

    public DbSet<ToDo> ToDo { get; set; }
}