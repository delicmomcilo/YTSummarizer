using Microsoft.AspNetCore.Mvc;
using YTSummarizer.Services;

namespace YTSummarizer.Api.Controllers;

[ApiController]
[Route("summarize")]
public class YTSummarizeController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IOpenAIService _openAIService;

    private readonly ILogger<YTSummarizeController> _logger;

    public YTSummarizeController(ILogger<YTSummarizeController> logger, IOpenAIService openAIService)
    {
        _openAIService = openAIService;
        _logger = logger;
    }

    [HttpGet("short")]
    public async Task<String> GetShortAsync()
    {

        var response = await _openAIService.AskChatGPT("Where did Nikola tesla come from ? ");
        return response ?? "Could not answer";
    }

    [HttpGet("long")]
    public IEnumerable<WeatherForecast> GetLong()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("structured")]
    public IEnumerable<WeatherForecast> GetStructured()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
