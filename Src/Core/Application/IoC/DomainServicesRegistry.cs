using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]

    internal static class DomainServicesRegistry
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IService<Domain.Entities.Notificacao>), typeof(NotificacaoService));
            services.AddScoped(typeof(IService<Domain.Entities.PedidoItem>), typeof(PedidoItemService));
            services.AddScoped(typeof(IPedidoService), typeof(PedidoService));
        }
    }
}