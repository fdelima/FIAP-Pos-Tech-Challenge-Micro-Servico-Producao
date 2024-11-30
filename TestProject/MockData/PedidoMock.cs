using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.ValuesObject;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Extensions;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados das ações
    /// </summary>
    public class PedidoMock
    {

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    DateTime.Now,
                    enmPedidoStatus.RECEBIDO.ToString(),
                    DateTime.Now,
                    enmPedidoStatusPagamento.APROVADO.ToString(),   
                    DateTime.Now,
                    new PedidoItem[]
                    {
                        new PedidoItem {
                             IdPedidoItem =  Guid.NewGuid(),
                            IdProduto =  Guid.NewGuid(),
                            Data = DateTime.Now,
                            Quantidade = 1
                        }
                    }
                };
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty,
                    Guid.Empty,
                    DateTime.Now,
                    enmPedidoStatus.RECEBIDO.ToString(),
                    DateTime.MinValue,
                    enmPedidoStatusPagamento.PENDENTE.ToString(),
                    DateTime.MinValue,
                    new PedidoItem[]
                    {
                        new PedidoItem {
                            Quantidade = 0
                        }
                    }
                };
        }

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
            {
                var pedidos = new List<Pedido>();
                for (var index2 = 1; index <= quantidade; index++)
                {
                    var idPedido = Guid.NewGuid();
                    pedidos.Add(new Pedido
                    {
                        IdPedido = idPedido,
                        IdDispositivo = Guid.NewGuid(),
                        PedidoItems = new PedidoItem[]
                        {
                        new PedidoItem {
                            IdPedidoItem = Guid.NewGuid(),
                            IdPedido = idPedido,
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 1
                        }
                        },
                        Status = ((enmPedidoStatus)new Random().Next(0, 2)).ToString(),
                    });
                }
                var param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    pedidos
                };
            }

        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaInValidos(int quantidade)
        {
            var pedidos = new List<Pedido>();
            for (var index = 1; index <= quantidade; index++)
                pedidos.Add(new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    IdDispositivo = Guid.NewGuid(),
                    PedidoItems = new PedidoItem[]
                    {
                        new PedidoItem {
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 1
                        }
                    },
                    Status = ((enmPedidoStatus)new Random().Next(0, 2)).ToString()
                });

            pedidos.Add(
                new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    PedidoItems = new PedidoItem[]
                    {
                        new PedidoItem {
                            IdProduto =  Guid.NewGuid(),
                            Quantidade = 1
                        }
                    },
                    Status = enmPedidoStatus.FINALIZADO.ToString()
                });

            for (var index = 1; index <= quantidade; index++)
            {
                var param = new PagingQueryParam<Pedido>() { CurrentPage = index, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    pedidos
                };
            }
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty
                };
        }
    }
}
