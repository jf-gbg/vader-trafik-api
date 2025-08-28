using Microsoft.AspNetCore.Mvc;
using WeatherService.Services;

namespace WeatherService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ISmhiClient _smhi;

    public WeatherController(ISmhiClient smhi)
    {
        _smhi = smhi;
    }

    // GET api/weather?lat=59.33&lon=18.07
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] double? lat, [FromQuery] double? lon)
    {
        if (!lat.HasValue || !lon.HasValue)
            return BadRequest("Provide lat and lon query parameters.");

        try
        {
            var json = await _smhi.GetWeatherJsonAsync(lat.Value, lon.Value);
            return Content(json, "application/json");
        }
        catch (Exception ex)
        {
            return StatusCode(502, $"SMHI request failed: {ex.Message}");
        }
    }
}
