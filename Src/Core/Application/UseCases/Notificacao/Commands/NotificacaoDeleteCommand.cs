﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Notificacao.Commands
{
    public class NotificacaoDeleteCommand : IRequest<ModelResult>
    {
        public NotificacaoDeleteCommand(Guid id, string[]? businessRules = null)
        {
            Id = id;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}