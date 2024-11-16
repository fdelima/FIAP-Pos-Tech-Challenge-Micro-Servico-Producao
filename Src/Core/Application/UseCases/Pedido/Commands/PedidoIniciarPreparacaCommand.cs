using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands
{
    internal class PedidoIniciarPreparacaCommand : IRequest<ModelResult>
    {
        public PedidoIniciarPreparacaCommand(Guid id, string[]? businessRules = null)
        {
            Id = id;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}