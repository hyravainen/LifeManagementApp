using LifeManagementApp.Models;

namespace LifeManagementApp.Services;

public interface IWeatherService
{
    Task<WeatherInfo> GetCurrentWeatherAsync();
}
