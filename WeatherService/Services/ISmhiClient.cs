namespace WeatherService.Services;

public interface ISmhiClient
{
    Task<string> GetWeatherJsonAsync(double lat, double lon, CancellationToken ct = default);
}