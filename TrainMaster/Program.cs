using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TrainMaster.Extensions;
using TrainMaster.Extensions.ExtensionsLogs;
using TrainMaster.Infrastracture.Connections;

var builder = WebApplication.CreateBuilder(args);

// ✅ Porta fixa (opcional, igual ao seu modelo funcional)
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // http://localhost:5000
});

// ✅ Inicializa log
LogExtension.InitializeLogger();
var loggerSerialLog = LogExtension.GetLogger();
loggerSerialLog.Information("Logging initialized.");

// ✅ Serviços
builder.Services.AddControllers(); // Apenas API (sem Razor)
builder.Services.AddEndpointsApiExplorer(); // Swagger
builder.Services.AddSwaggerGen();           // Swagger

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddAuthorization();

var app = builder.Build();

// ✅ Middleware de exceções customizado
app.UseMiddleware<ExceptionMiddleware>();

// ✅ Swagger sempre visível
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TrainMaster API v1");
    c.RoutePrefix = string.Empty; // Swagger direto na raiz
});

// ✅ CORS e cabeçalhos para APIs
app.UseCors("CorsPolicy");

app.Use(async (context, next) =>
{
    context.Response.Headers.TryAdd("Access-Control-Allow-Origin", "*");
    context.Response.Headers.TryAdd("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
    context.Response.Headers.TryAdd("Access-Control-Allow-Headers", "Content-Type, Authorization");

    if (context.Request.Method == HttpMethods.Options)
    {
        context.Response.StatusCode = 204;
        await context.Response.CompleteAsync();
        return;
    }

    await next.Invoke();
});

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ✅ Migração automática (opcional)
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    context.Database.Migrate();
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "Erro ao aplicar migração!");
}

app.Run();
