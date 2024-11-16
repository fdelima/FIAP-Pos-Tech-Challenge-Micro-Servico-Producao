using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces
{
    public interface IPedidoService : IService<Pedido>
    {
        /// <summary>
        /// Pedido em preparação.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> IniciarPreparacaoAsync(Guid id, string[]? businessRules = null);

        /// <summary>
        /// Pedido pronto.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> FinalizarPreparacaoAsync(Guid id, string[]? businessRules = null);

        /// <summary>
        /// Pedido finalizado.
        /// </summary>
        /// <param name="id">id do pedido</param>
        Task<ModelResult> FinalizarAsync(Guid id, string[]? businessRules = null);
    }
}
