﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Handlers
{
    public class PedidoFinalizarPreparacaHandler : IRequestHandler<PedidoFinalizarPreparacaCommand, ModelResult>
    {
        private readonly IPedidoService _service;

        public PedidoFinalizarPreparacaHandler(IPedidoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(PedidoFinalizarPreparacaCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FinalizarPreparacaoAsync(command.Id, command.BusinessRules);
        }
    }
}
