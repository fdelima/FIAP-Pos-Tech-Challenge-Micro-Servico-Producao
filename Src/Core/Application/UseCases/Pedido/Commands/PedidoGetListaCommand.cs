using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands
{
    public class PedidoGetListaCommand : IRequest<PagingQueryResult<Domain.Entities.Pedido>>
    {
        public PedidoGetListaCommand(IPagingQueryParam filter)
        {
            Filter = filter;
        }

        public IPagingQueryParam Filter { get; }
    }
}