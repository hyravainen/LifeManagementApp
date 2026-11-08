using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LifeManagementApp.Models;
using LifeManagementApp.Services;

namespace LifeManagementApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private const double KiteWindLimit = 5.0; // m/s

    private readonly ITodoRepository _todoRepository;
    private readonly IWeatherService _weatherService;

    public HomeViewModel(ITodoRepository todoRepository, IWeatherService weatherService)
    {
        _todoRepository = todoRepository;
        _weatherService = weatherService;
    }

    public ObservableCollection<TodoItem> TopTodos { get; } = new();

    [ObservableProperty] private double _temperature;
    [ObservableProperty] private double _windSpeed;
    [ObservableProperty] private bool _hasWeather;
    [ObservableProperty] private bool _isKiteWeather;
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private string _errorMessage = string.Empty;

    [RelayCommand]
    private async Task LoadAsync()
    {
        IsLoading = true;
        ErrorMessage = string.Empty;

        try
        {
            var top = await _todoRepository.GetTop3Async();
            TopTodos.Clear();
            foreach (var t in top) TopTodos.Add(t);

            var weather = await _weatherService.GetCurrentWeatherAsync();
            Temperature = weather.Temperature;
            WindSpeed = weather.WindSpeed;
            IsKiteWeather = weather.WindSpeed > KiteWindLimit;
            HasWeather = true;
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or InvalidOperationException)
        {
            HasWeather = false;
            IsKiteWeather = false;
            ErrorMessage = "Säätä ei saatu haettua. Tarkista verkkoyhteys.";
        }
        finally
        {
            IsLoading = false;
        }
    }
}
