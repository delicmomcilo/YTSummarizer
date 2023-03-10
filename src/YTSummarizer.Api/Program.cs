using YTSummarizer.Services;
using YTSummarizer.Auth;
var builder = WebApplication.CreateBuilder(args);

YTSummarizer.Services.Startup.ConfigureServices(builder.Services);
YTSummarizer.Auth.Startup.Configure(builder);
// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
