using LifeManagementApp.Models;

namespace LifeManagementApp.Services;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetAllAsync();
    Task<List<TodoItem>> GetTop3Async();
    Task AddAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
    Task DeleteAsync(TodoItem item);
}
