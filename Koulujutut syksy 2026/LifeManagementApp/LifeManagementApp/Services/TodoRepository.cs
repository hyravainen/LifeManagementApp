using LifeManagementApp.Data;
using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeManagementApp.Services;

public class TodoRepository : ITodoRepository
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    private bool _created;

    public TodoRepository(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    // Luo tietokannan ja taulun ensimmäisellä käyttökerralla (Code First)
    private async Task<AppDbContext> GetContextAsync()
    {
        var db = await _factory.CreateDbContextAsync();
        if (!_created)
        {
            await db.Database.EnsureCreatedAsync();
            _created = true;
        }
        return db;
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        await using var db = await GetContextAsync();
        return await db.Todos.AsNoTracking().ToListAsync();
    }

    // Top 3: keskeneräiset, prioriteetti suurin ensin, sitten eräpäivä aikaisin ensin
    public async Task<List<TodoItem>> GetTop3Async()
    {
        await using var db = await GetContextAsync();
        return await db.Todos
            .AsNoTracking()
            .Where(t => !t.IsDone)
            .OrderByDescending(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .Take(3)
            .ToListAsync();
    }

    public async Task AddAsync(TodoItem item)
    {
        await using var db = await GetContextAsync();
        db.Todos.Add(item);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem item)
    {
        await using var db = await GetContextAsync();
        db.Todos.Update(item);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(TodoItem item)
    {
        await using var db = await GetContextAsync();
        db.Todos.Remove(item);
        await db.SaveChangesAsync();
    }
}
