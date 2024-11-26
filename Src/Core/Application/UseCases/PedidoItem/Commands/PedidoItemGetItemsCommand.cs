using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.PedidoItem.Commands
{
    internal class PedidoItemGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.PedidoItem>>
    {
        public PedidoItemGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.PedidoItem, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public PedidoItemGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.PedidoItem, bool>> expression, Expression<Func<Domain.Entities.PedidoItem, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.PedidoItem, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.PedidoItem, object>> SortProp { get; }
    }
}