using Microsoft.AspNetCore.Mvc;
using NLog;
using NLog.Extensions.Logging;
using TestTask_Infopulse.BLL;
using TestTask_Infopulse.BLL.Extensions;
using TestTask_Infopulse.BLL.Services.LoggerService;
using TestTask_Infopulse.DataAccess;
using TestTask_Infopulse.Web;
using TestTask_Infopulse.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("SQLConnection");

builder.Services.AddSqlServer(connectionString)
    .AddUnitOfWork<AppDbContext>()
    .AddRepositories();
builder.Services.AddResponseCaching();
builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add(CacheProfiles.FiveSeconds, new CacheProfile
    {
        Duration = 5
    });
});
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(MediatrEntryPoint).Assembly));
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddAutoMapper(typeof(MediatrEntryPoint).Assembly);
builder.Services.AddEndpointsApiExplorer();
LogManager.Configuration = new NLogLoggingConfiguration(builder.Configuration.GetSection("NLog"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
