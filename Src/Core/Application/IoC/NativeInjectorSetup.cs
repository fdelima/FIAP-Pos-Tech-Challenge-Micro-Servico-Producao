﻿using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.IoC
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
        public static void RegisterAppDependencies(this IServiceCollection services)
        {
            services.RegisterDomainServices();
            services.RegisterValidators();
            services.RegisterAppControllers();
            services.RegisterCommands();
        }
    }
}
