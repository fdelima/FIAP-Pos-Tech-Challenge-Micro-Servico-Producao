using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Handlers
{
    public class PedidoPutHandler : IRequestHandler<PedidoPutCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoPutHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoPutCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command.Entity, command.BusinessRules);
        }
    }
}
