using TodoApi.Entities;

namespace TodoApi.Data;

public static class TodoSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.TodoItems.Any()) return;
        var now = DateTime.UtcNow;
        var items = new List<TodoItem>
        {
            new() { Title = "Loyiha setup",           Description = "Solution va folder structure", IsCompleted = true,  Priority = Priority.High,   Category = "Setup",   DueDate = now.AddDays(-3), EstimatedHours = 2, CreatedTime = now.AddDays(-6) },
            new() { Title = "Entity yaratish",         Description = "TodoItem entity 10 property",  IsCompleted = true,  Priority = Priority.High,   Category = "Backend", DueDate = now.AddDays(-2), EstimatedHours = 1, CreatedTime = now.AddDays(-6) },
            new() { Title = "DbContext sozlash",       Description = "AppDbContext + DbSet",         IsCompleted = true,  Priority = Priority.Medium, Category = "Backend", DueDate = now.AddDays(-1), EstimatedHours = 1, CreatedTime = now.AddDays(-5) },
            new() { Title = "Repository yozish",       Description = "CRUD metodlari",               IsCompleted = false, Priority = Priority.High,   Category = "Backend", DueDate = now.AddDays(1),  EstimatedHours = 4, CreatedTime = now.AddDays(-4) },
            new() { Title = "Controller endpointlari", Description = "5 ta REST endpoint",           IsCompleted = false, Priority = Priority.High,   Category = "Backend", DueDate = now.AddDays(2),  EstimatedHours = 3, CreatedTime = now.AddDays(-4) },
            new() { Title = "Validation qo'shish",     Description = "DTO validatsiyasi",            IsCompleted = false, Priority = Priority.Medium, Category = "Quality", DueDate = now.AddDays(3),  EstimatedHours = 2, CreatedTime = now.AddDays(-3) },
            new() { Title = "Filter va sort",          Description = "Query funksiyalari",           IsCompleted = false, Priority = Priority.Medium, Category = "Feature", DueDate = now.AddDays(4),  EstimatedHours = 3, CreatedTime = now.AddDays(-3) },
            new() { Title = "Pagination",              Description = "Skip / Take",                  IsCompleted = false, Priority = Priority.Low,    Category = "Feature", DueDate = now.AddDays(4),  EstimatedHours = 1, CreatedTime = now.AddDays(-2) },
            new() { Title = "Swagger hujjat",          Description = "Endpoint description",         IsCompleted = false, Priority = Priority.Low,    Category = "Docs",    DueDate = now.AddDays(5),  EstimatedHours = 1, CreatedTime = now.AddDays(-2) },
            new() { Title = "README yozish",           Description = "Foydalanish yo'riqnomasi",     IsCompleted = false, Priority = Priority.Low,    Category = "Docs",    DueDate = now.AddDays(6),  EstimatedHours = 1, CreatedTime = now.AddDays(-1) },
        };
        context.TodoItems.AddRange(items);
        await context.SaveChangesAsync();
    }

    ///ssss
}