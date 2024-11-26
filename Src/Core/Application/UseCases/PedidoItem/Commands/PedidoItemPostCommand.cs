using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.PedidoItem.Commands
{
    internal class PedidoItemPostCommand : IRequest<ModelResult>
    {
        public PedidoItemPostCommand(Domain.Entities.PedidoItem entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.PedidoItem Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}