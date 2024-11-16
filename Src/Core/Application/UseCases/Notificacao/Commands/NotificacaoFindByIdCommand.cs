using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Notificacao.Commands
{
    public class NotificacaoFindByIdCommand : IRequest<ModelResult>
    {
        public NotificacaoFindByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}