using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Handlers
{
    public class PedidoIniciarPreparacaHandler : IRequestHandler<PedidoIniciarPreparacaCommand, ModelResult>
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
