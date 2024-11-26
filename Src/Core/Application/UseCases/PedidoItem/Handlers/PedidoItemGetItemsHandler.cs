using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.PedidoItem.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.PedidoItem.Handlers
{
    internal class PedidoItemGetItemsHandler : IRequestHandler<PedidoItemGetItemsCommand, PagingQueryResult<Domain.Entities.PedidoItem>>
    {
        private readonly IService<Domain.Entities.PedidoItem> _service;

        public PedidoItemGetItemsHandler(IService<Domain.Entities.PedidoItem> service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.PedidoItem>> Handle(PedidoItemGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
