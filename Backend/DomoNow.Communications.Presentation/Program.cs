using DomoNow.Communications.Application.Settings;
using DomoNow.Communications.Infrastructure.Settings;
using DomoNow.Communications.Presentation.Middleware;
using DomoNow.Communications.Presentation.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.Presentation().Infrastructure(builder.Configuration).Application();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler("/error");
app.UseMiddleware<ApiMiddleware>();
app.Run();
