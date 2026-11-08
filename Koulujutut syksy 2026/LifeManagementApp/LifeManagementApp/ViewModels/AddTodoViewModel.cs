using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

public partial class AddTodoViewModel : ObservableObject
{
    private readonly ITodoRepository _repository;

    public AddTodoViewModel(ITodoRepository repository)
    {
        _repository = repository;
    }

    public List<int> PriorityOptions { get; } = new() { 1, 2, 3, 4, 5 };

    [ObservableProperty] private string _title = string.Empty;
    [ObservableProperty] private int _priority = 3;
    [ObservableProperty] private DateTime _dueDate = DateTime.Today;
    [ObservableProperty] private string _errorMessage = string.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        // Validointi: tyhjää otsikkoa ei hyväksytä
        if (string.IsNullOrWhiteSpace(Title))
        {
            ErrorMessage = "Otsikko ei voi olla tyhjä.";
            return;
        }

        await _repository.AddAsync(new TodoItem
        {
            Title = Title.Trim(),
            Priority = Priority,
            DueDate = DueDate
        });

        Title = string.Empty;
        ErrorMessage = string.Empty;
        await Shell.Current.GoToAsync("..");
    }
}
