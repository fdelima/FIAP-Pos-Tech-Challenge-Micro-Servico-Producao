using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Infra.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Infra.IoC
{
    internal static class GatewaysRegistry
    {
        public static void RegisterGateways(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IGateways<>), typeof(BaseGateway<>));
        }
    }
}