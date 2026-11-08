using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

public partial class TodoListViewModel : ObservableObject
{
    private readonly ITodoRepository _repository;
    private bool _sortByPriority = true;

    public TodoListViewModel(ITodoRepository repository)
    {
        _repository = repository;
    }

    public ObservableCollection<TodoItem> Items { get; } = new();

    [RelayCommand]
    private async Task LoadAsync()
    {
        var all = await _repository.GetAllAsync();

        var sorted = _sortByPriority
            ? all.OrderByDescending(t => t.Priority).ThenBy(t => t.DueDate)
            : all.OrderBy(t => t.DueDate).ThenByDescending(t => t.Priority);

        Items.Clear();
        foreach (var item in sorted) Items.Add(item);
    }

    [RelayCommand]
    private async Task SortByPriorityAsync()
    {
        _sortByPriority = true;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task SortByDueDateAsync()
    {
        _sortByPriority = false;
        await LoadAsync();
    }

    [RelayCommand]
    private async Task ToggleDoneAsync(TodoItem item)
    {
        item.IsDone = !item.IsDone;
        await _repository.UpdateAsync(item);
        await LoadAsync();
    }

    [RelayCommand]
    private async Task DeleteAsync(TodoItem item)
    {
        await _repository.DeleteAsync(item);
        await LoadAsync();
    }

    [RelayCommand]
    private async Task GoToAddAsync()
    {
        await Shell.Current.GoToAsync("AddTodoPage");
    }
}
