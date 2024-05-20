using WssConsultingTask.API.Extensions;
using WssConsultingTask.API.Middlewares;
using WssConsultingTask.Application;
using WssConsultingTask.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment()) 
{
    app.UseMigrateDatabase();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Departaments}/{action=Index}/{id?}");

app.Run();
