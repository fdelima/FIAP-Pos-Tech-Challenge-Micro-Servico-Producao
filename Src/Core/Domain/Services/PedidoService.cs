using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.ValuesObject;
using FluentValidation;

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
        /// Regra para carregar o pedido e seus itens.
        /// </summary>
        public async override Task<ModelResult> FindByIdAsync(Guid Id)
        {
            Pedido? result = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Pedido>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Regras para inserção do pedido
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Pedido entity, string[]? businessRules = null)
        {
            List<string> lstWarnings = new List<string>();

            if (string.IsNullOrWhiteSpace(entity.Status) || string.IsNullOrWhiteSpace(entity.StatusPagamento)
              || !entity.Status.Equals(enmPedidoStatus.RECEBIDO.ToString())
              || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'RECEBIDO' e status do pagamento 'APROVADO'. ");
                businessRules = temp.ToArray();
            }

            if (businessRules != null)
                lstWarnings.AddRange(businessRules);

            await _gateway.InsertAsync(new Notificacao
            {
                IdNotificacao = Guid.NewGuid(),
                Data = DateTime.Now,
                IdDispositivo = entity.IdDispositivo,
                Mensagem = $"Pedido recebido."
            });

            return await base.InsertAsync(entity, lstWarnings.ToArray());
        }

        /// <summary>
        /// Regra para atualização do pedido e suas dependências.
        /// </summary>
        public async override Task<ModelResult> UpdateAsync(Pedido entity, string[]? businessRules = null)
        {
            Pedido? dbEntity = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.PedidoItems, x => x.IdPedido == entity.IdPedido);

            for (int i = 0; i < dbEntity.PedidoItems.Count; i++)
            {
                PedidoItem item = dbEntity.PedidoItems.ElementAt(i);
                if (!entity.PedidoItems.Any(x => x.IdPedidoItem.Equals(item.IdPedidoItem)))
                    dbEntity.PedidoItems.Remove(dbEntity.PedidoItems.First(x => x.IdPedidoItem.Equals(item.IdPedidoItem)));
            }

            for (int i = 0; i < entity.PedidoItems.Count; i++)
            {
                PedidoItem item = entity.PedidoItems.ElementAt(i);
                if (!dbEntity.PedidoItems.Any(x => x.IdPedidoItem.Equals(item.IdPedidoItem)))
                {
                    item.IdPedidoItem = item.IdPedidoItem.Equals(default) ? Guid.NewGuid() : item.IdPedidoItem;
                    dbEntity.PedidoItems.Add(item);
                }
            }

            await _gateway.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Regra para retornar os Pedidos cadastrados
        /// A lista de pedidos deverá retorná-los com suas descrições, ordenados com a seguinte regra:
        /// 1. Pronto > Em Preparação > Recebido;
        /// 2. Pedidos mais antigos primeiro e mais novos depois;
        /// 3. Pedidos com status Finalizado não devem aparecer na lista.
        /// </summary>
        public async ValueTask<PagingQueryResult<Pedido>> GetListaAsync(IPagingQueryParam filter)
        {
            filter.SortDirection = "Desc";
            return await _gateway.GetItemsAsync(filter, x => x.Status != enmPedidoStatus.FINALIZADO.ToString(), o => o.Data);
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

            if (string.IsNullOrWhiteSpace(entity.Status) || string.IsNullOrWhiteSpace(entity.StatusPagamento)
                || !entity.Status.Equals(enmPedidoStatus.RECEBIDO.ToString())
                || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'RECEBIDO' e status do pagamento 'APROVADO'. ");
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

            if (string.IsNullOrWhiteSpace(entity.Status) || string.IsNullOrWhiteSpace(entity.StatusPagamento)
                || !entity.Status.Equals(enmPedidoStatus.EM_PREPARACAO.ToString())
                || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'EM_PREPARACAO' e status do pagamento 'APROVADO'. ");
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

            if (string.IsNullOrWhiteSpace(entity.Status) || string.IsNullOrWhiteSpace(entity.StatusPagamento)
                || !entity.Status.Equals(enmPedidoStatus.PRONTO.ToString())
                || !entity.StatusPagamento.Equals(enmPedidoStatusPagamento.APROVADO.ToString()))
            {
                List<string> temp = businessRules == null ? new List<string>() : new List<string>(businessRules);
                temp.Add($"Permitido somente pedido com status 'PRONTO' e status do pagamento 'APROVADO'. ");
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
