namespace YTSummarizer.Services;

public class Startup
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IOpenAIService, OpenAIService>();
    }
}
