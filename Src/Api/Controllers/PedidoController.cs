using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller dos Pedidos cadastrados
    /// </summary>
    [Route("api/[Controller]")]
    public class PedidoController : ApiController
    {
        private readonly IPedidoController _controller;

        /// <summary>
        /// Construtor do controller dos Pedidos cadastrados
        /// </summary>
        public PedidoController(IPedidoController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Pedido em preparação.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpPatch("{id}/IniciarPreparacao")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> IniciarPreparacaoAsync(Guid id)
        {
            return ExecuteCommand(await _controller.IniciarPreparacaoAsync(id));
        }

        /// <summary>
        /// Pedido pronto.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpPatch("{id}/FinalizarPreparacao")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FinalizarPreparacaoAsync(Guid id)
        {
            return ExecuteCommand(await _controller.FinalizarPreparacaoAsync(id));
        }

        /// <summary>
        /// Pedido finalizado.
        /// </summary>
        /// <param name="id">Identificador do Pedido cadastrado.</param>
        /// <returns>Retorna o result do Pedido cadastrado.</returns>
        /// <response code="200">Pedido deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Pedido.</response>
        [HttpPatch("{id}/Finalizar")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FinalizarAsync(Guid id)
        {
            return ExecuteCommand(await _controller.FinalizarAsync(id));
        }
    }
}