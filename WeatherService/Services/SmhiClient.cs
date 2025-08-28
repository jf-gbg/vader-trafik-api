namespace WeatherService.Services;
public class SmhiClient : ISmhiClient
{
    private readonly HttpClient _http;

    public SmhiClient(HttpClient http)
    {
        _http = http ?? throw new ArgumentNullException(nameof(http));
    }

    public async Task<string> GetWeatherJsonAsync(double lat, double lon, CancellationToken ct = default)
    {
        // SMHI point forecast endpoint
        var path = $"api/category/pmp3g/version/2/geotype/point/lon/{lon}/lat/{lat}/data.json";

        using var resp = await _http.GetAsync(path, ct);
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadAsStringAsync(ct);
    }
}