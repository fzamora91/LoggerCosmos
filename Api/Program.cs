using Application;
using Infrastructure;
using Shared.Core;



public class Program
{
    // M�todo Main como punto de entrada
    public static void Main(string[] args)
    {
        // Crea un builder para configurar la aplicaci�n
        var builder = WebApplication.CreateBuilder(args);

        // A�ade servicios al contenedor
        EngineContext.Create();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddApplicationServices(builder.Configuration);
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Crea la aplicaci�n
        var app = builder.Build();

        EngineContext.Current.Configure(app.Services);

        // Configura el pipeline HTTP
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Ejecuta la aplicaci�n
        app.Run();
    }
}
