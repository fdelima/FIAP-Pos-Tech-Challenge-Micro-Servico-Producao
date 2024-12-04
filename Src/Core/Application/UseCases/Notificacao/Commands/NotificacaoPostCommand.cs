using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Notificacao.Commands
{
    public class NotificacaoPostCommand : IRequest<ModelResult>
    {
        public NotificacaoPostCommand(Domain.Entities.Notificacao entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Notificacao Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}