using FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Handlers
{
    internal class PedidoIniciarPreparacaHandler : IRequestHandler<PedidoIniciarPreparacaCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoIniciarPreparacaHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoIniciarPreparacaCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.IniciarPreparacaoAsync(command.Id, command.BusinessRules);
        }
    }
}
