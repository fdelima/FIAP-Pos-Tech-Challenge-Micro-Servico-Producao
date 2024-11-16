using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.ValuesObject;

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
            for (int index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
        {
            for (int index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty
                };
        }

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
        {
            List<Pedido> pedidos = new List<Pedido>();
            for (int index = 1; index <= quantidade; index++)
                pedidos.Add(new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    IdDispositivo = Guid.NewGuid(),
                    Status = ((enmPedidoStatus)new Random().Next(1, 2)).ToString()
                });

            for (int index = 1; index <= quantidade; index++)
            {
                PagingQueryParam<Pedido> param = new PagingQueryParam<Pedido>() { CurrentPage = index, Take = 10 };
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
            List<Pedido> pedidos = new List<Pedido>();
            for (int index = 1; index <= quantidade; index++)
                pedidos.Add(new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    IdDispositivo = Guid.NewGuid(),
                    Status = ((enmPedidoStatus)new Random().Next(1, 2)).ToString()
                });

            pedidos.Add(
                new Pedido
                {
                    IdPedido = Guid.NewGuid(),
                    Status = enmPedidoStatus.FINALIZADO.ToString()
                });

            for (int index = 1; index <= quantidade; index++)
            {
                PagingQueryParam<Pedido> param = new PagingQueryParam<Pedido>() { CurrentPage = index, Take = 10 };
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
            for (int index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
        {
            for (int index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty
                };
        }
    }
}
