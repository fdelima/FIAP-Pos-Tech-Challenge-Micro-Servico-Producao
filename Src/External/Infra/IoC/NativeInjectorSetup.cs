using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Infra.IoC
{
    /// <summary>
    /// Configura a injeção de dependência
    /// </summary>
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    public static class NativeInjectorSetup
    {
        /// <summary>
        /// Registra as dependências aos serviços da aplicação
        /// </summary>
        public static void RegisterInfraDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDatabase(configuration);
            services.RegisterGateways();
        }
    }
}
