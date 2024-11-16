using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]

    internal static class ControllersRegistry
    {
        public static void RegisterAppControllers(this IServiceCollection services)
        {
            //Controlles
            services.AddScoped(typeof(IPedidoController), typeof(PedidoController));
        }
    }
}