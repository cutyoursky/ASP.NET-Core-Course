using SocialMediaExcercise;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("SocialMediaLinksOptions.json", optional: true, reloadOnChange: true);
});

builder.Services.AddControllersWithViews();
builder.Services.Configure<SocialMediaLinksOptions>(builder.Configuration.GetSection("SocialMediaLinks"));
var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();


app.Run();
