using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Extensions;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados das ações
    /// </summary>
    public class PedidoItemMock
    {
        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    DateTime.Now,
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    1
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
                    DateTime.Now,
                    Guid.Empty,
                    Guid.Empty,
                    0
                };
        }

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        /// Mock de dados válidos para consulta
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
            {
                var notificacoes = new List<PedidoItem>();
                for (var index2 = 1; index2 <= quantidade; index2++)
                {
                    notificacoes.Add(new PedidoItem
                    {
                        IdPedidoItem = Guid.NewGuid(),
                        Data = DateTime.Now,
                        IdPedido = Guid.NewGuid(),
                        IdProduto = Guid.NewGuid(),
                        Quantidade = 1
                    });
                }
                var param = new PagingQueryParam<PedidoItem>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    notificacoes
                };
            }
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaInValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
            {
                var notificacoes = new List<PedidoItem>();
                for (var index2 = 1; index2 <= quantidade; index2++)
                {
                    notificacoes.Add(new PedidoItem
                    {
                        IdPedidoItem = Guid.Empty,
                        Data = DateTime.Now,
                        IdPedido = Guid.Empty,
                        IdProduto = Guid.Empty,
                        Quantidade = 0
                    });
                }
                var param = new PagingQueryParam<PedidoItem>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    notificacoes
                };
            }
        }

        /// <summary>
        /// Mock de dados Válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
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
