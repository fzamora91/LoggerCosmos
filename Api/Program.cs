using Application;
using Infrastructure;
using Shared.Core;



public class Program
{
    // Método Main como punto de entrada
    public static void Main(string[] args)
    {
        // Crea un builder para configurar la aplicación
        var builder = WebApplication.CreateBuilder(args);

        // Añade servicios al contenedor
        EngineContext.Create();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Crea la aplicación
        var app = builder.Build();

        EngineContext.Current.Configure(app.Services);

        // Configura el pipeline HTTP
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Ejecuta la aplicación
        app.Run();
    }
}
