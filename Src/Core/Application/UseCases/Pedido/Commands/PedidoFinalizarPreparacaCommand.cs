﻿using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Pedido.Commands
{
    internal class PedidoFinalizarPreparacaCommand : IRequest<ModelResult>
    {
        public PedidoFinalizarPreparacaCommand(Guid id, string[]? businessRules = null)
        {
            Id = id;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}