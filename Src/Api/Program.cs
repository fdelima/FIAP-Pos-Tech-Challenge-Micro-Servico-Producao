using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Api;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.IoC;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
public class Program
{
    public static void Main(string[] args)
    {

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        App.SetAtributesAppFromDll();

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.ConfigureModelValidations();

        builder.Services.AddSwagger("Web Api C# Sample");

        builder.Services.RegisterDependencies(builder.Configuration);

        WebApplication app = builder.Build();

        app.ConfigureSwagger();

        app.ConfigureReDoc();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.AddGlobalErrorHandler();

        app.Run();
    }
}