using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTSummarizer.Auth.Security;

namespace YTSummarizer.Auth.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ISecurity _security;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ISecurity security)
    {
        _logger = logger;
        _security = security;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public string Get()
    {
        return _security.GetUserIdFromAccessToken(Request) ?? "";
    }
}
