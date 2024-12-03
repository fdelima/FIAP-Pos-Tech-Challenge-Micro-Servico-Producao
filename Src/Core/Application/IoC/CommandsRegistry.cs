using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Notificacao.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Notificacao.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]

    internal static class CommandsRegistry
    {
        public static void RegisterCommands(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //Notificacao
            services.AddScoped<IRequestHandler<NotificacaoPostCommand, ModelResult>, NotificacaoPostHandler>();
            services.AddScoped<IRequestHandler<NotificacaoPutCommand, ModelResult>, NotificacaoPutHandler>();
            services.AddScoped<IRequestHandler<NotificacaoDeleteCommand, ModelResult>, NotificacaoDeleteHandler>();
            services.AddScoped<IRequestHandler<NotificacaoFindByIdCommand, ModelResult>, NotificacaoFindByIdHandler>();
            services.AddScoped<IRequestHandler<NotificacaoGetItemsCommand, PagingQueryResult<Notificacao>>, NotificacaoGetItemsHandler>();

            //Pedido
            services.AddScoped<IRequestHandler<PedidoPostCommand, ModelResult>, PedidoPostHandler>();
            services.AddScoped<IRequestHandler<PedidoPutCommand, ModelResult>, PedidoPutHandler>();
            services.AddScoped<IRequestHandler<PedidoDeleteCommand, ModelResult>, PedidoDeleteHandler>();
            services.AddScoped<IRequestHandler<PedidoFindByIdCommand, ModelResult>, PedidoFindByIdHandler>();
            services.AddScoped<IRequestHandler<PedidoGetItemsCommand, PagingQueryResult<Pedido>>, PedidoGetItemsHandler>();
            services.AddScoped<IRequestHandler<PedidoGetListaCommand, PagingQueryResult<Domain.Entities.Pedido>>, PedidoGetIListaHandler>();
            services.AddScoped<IRequestHandler<PedidoIniciarPreparacaCommand, ModelResult>, PedidoIniciarPreparacaHandler>();
            services.AddScoped<IRequestHandler<PedidoFinalizarPreparacaCommand, ModelResult>, PedidoFinalizarPreparacaHandler>();
            services.AddScoped<IRequestHandler<PedidoFinalizarCommand, ModelResult>, PedidoFinalizarHandler>();

        }
    }
}
