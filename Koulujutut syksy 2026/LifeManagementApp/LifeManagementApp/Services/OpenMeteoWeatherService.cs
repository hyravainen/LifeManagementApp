using System.Net.Http.Json;
using LifeManagementApp.Models;
using LifeManagementApp.Services.Dto;

namespace LifeManagementApp.Services;

public class OpenMeteoWeatherService : IWeatherService
{
    // Helsinki. HUOM: wind_speed_unit=ms, muuten tuuli tulee km/h
    private const string Url =
        "https://api.open-meteo.com/v1/forecast?latitude=60.17&longitude=24.94" +
        "&current=temperature_2m,wind_speed_10m&wind_speed_unit=ms";

    private readonly HttpClient _http;

    public OpenMeteoWeatherService(HttpClient http)
    {
        _http = http;
        _http.Timeout = TimeSpan.FromSeconds(10);
    }

    public async Task<WeatherInfo> GetCurrentWeatherAsync()
    {
        var response = await _http.GetFromJsonAsync<OpenMeteoResponse>(Url);
        if (response?.Current is null)
            throw new InvalidOperationException("Säädata puuttuu vastauksesta.");

        return new WeatherInfo
        {
            Temperature = response.Current.Temperature,
            WindSpeed = response.Current.WindSpeed
        };
    }
}
