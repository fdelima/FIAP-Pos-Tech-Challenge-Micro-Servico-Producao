using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.ValuesObject;
using FluentValidation;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Services
{
    public class PedidoService : BaseService<Pedido>, IPedidoService
    {
        protected readonly IGateways<Notificacao> _notificacaoGateway;

        /// <summary>
        /// Lógica de negócio referentes ao pedido.
        /// </summary>
        /// <param name="gateway">Gateway de pedido a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        /// <param name="notificacaoGateway">Gateway de notificação a ser injetado durante a execução</param>
        /// <param name="dispositivoGateway">Gateway de dispositivo a ser injetado durante a execução</param>
        /// <param name="clienteGateway">Gateway de cliente a ser injetado durante a execução</param>
        /// <param name="produtoGateway">Gateway de produto a ser injetado durante a execução</param>
        public PedidoService(IGateways<Pedido> gateway,
            IValidator<Pedido> validator,
            IGateways<Notificacao> notificacaoGateway)
            : base(gateway, validator)
        {
            _notificacaoGateway = notificacaoGateway;
        }

        /// <summary>
        /// Regras base para inserção.
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Pedido entity, string[]? businessRules = null)
        {

            if (!entity.Status.Equals(enmPedidoStatus.RECEBIDO.ToString())
                || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'RECEBIDO' e status do pagamento 'APROVADO'. " +
                    $"Pedido: {entity.IdPedido} status:{entity.Status} status pagamento: {entity.StatusPagamento}");
                businessRules = temp.ToArray();
            }

            return await base.InsertAsync(entity, businessRules);
        }

        /// <summary>
        /// Regra para colocar o pedido em preparação.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> IniciarPreparacaoAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            Pedido? entity = await _gateway.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (!entity.Status.Equals(enmPedidoStatus.RECEBIDO.ToString())
                || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'RECEBIDO' e status do pagamento 'APROVADO'. " +
                    $"Pedido: {entity.IdPedido} status:{entity.Status} status pagamento: {entity.StatusPagamento}");
                businessRules = temp.ToArray();
            }

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.EM_PREPARACAO.ToString();

            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transacao = _gateway.BeginTransaction();
            _notificacaoGateway.UseTransaction(transacao);

            await _gateway.UpdateAsync(entity);
            await _gateway.CommitAsync();

            await _notificacaoGateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido em preparação."
            }); ;
            await _notificacaoGateway.CommitAsync();

            await transacao.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Regra para colocar o pedido pronto.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> FinalizarPreparacaoAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            Pedido? entity = await _gateway.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (!entity.Status.Equals(enmPedidoStatus.EM_PREPARACAO.ToString())
                || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'EM_PREPARACAO' e status do pagamento 'APROVADO'. " +
                    $"Pedido: {entity.IdPedido} status:{entity.Status} status pagamento: {entity.StatusPagamento}");
                businessRules = temp.ToArray();
            }

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.PRONTO.ToString();

            Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transacao = _gateway.BeginTransaction();
            _notificacaoGateway.UseTransaction(transacao);

            await _gateway.UpdateAsync(entity);
            await _gateway.CommitAsync();

            await _notificacaoGateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido pronto."
            });
            await _notificacaoGateway.CommitAsync();

            await transacao.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }

        /// <summary>
        /// Regra para colocar o pedido finalizado.
        /// </summary>
        /// <param name="id">id do pedido</param>
        public async Task<ModelResult> FinalizarAsync(Guid id, string[]? businessRules = null)
        {
            ModelResult ValidatorResult = new ModelResult();

            Pedido? entity = await _gateway.FindByIdAsync(id);

            if (entity == null)
                ValidatorResult = ModelResultFactory.NotFoundResult<Pedido>();

            if (!entity.Status.Equals(enmPedidoStatus.PRONTO.ToString())
                || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'PRONTO' e status do pagamento 'APROVADO'. " +
                    $"Pedido: {entity.IdPedido} status:{entity.Status} status pagamento: {entity.StatusPagamento}");
                businessRules = temp.ToArray();
            }

            if (businessRules != null)
                ValidatorResult.AddError(businessRules);

            if (!ValidatorResult.IsValid)
                return ValidatorResult;

            entity.Status = enmPedidoStatus.FINALIZADO.ToString();

            await _gateway.UpdateAsync(entity);
            await _gateway.CommitAsync();

            return ModelResultFactory.UpdateSucessResult<Pedido>(entity);

        }
    }
}
