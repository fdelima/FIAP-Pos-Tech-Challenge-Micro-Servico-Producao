using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Extensions;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados das ações
    /// </summary>
    public class NotificacaoMock
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
                    new Notificacao
                    {
                        IdNotificacao = Guid.NewGuid(),
                        Mensagem = "Mensagem de teste",
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
                    new Notificacao
                    {
                        IdNotificacao = Guid.Empty,
                        Mensagem = null
                    }
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
                var notificacoes = new List<Notificacao>();
                for (var index2 = 1; index2 <= quantidade; index2++)
                {
                    notificacoes.Add(new Notificacao
                    {
                        Data = DateTime.Now,
                        Mensagem = "Mensagem de teste",
                        IdDispositivo = Guid.NewGuid()
                    });
                }
                var param = new PagingQueryParam<Notificacao>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
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
                var notificacoes = new List<Notificacao>();
                for (var index2 = 1; index2 <= quantidade; index2++)
                {
                    notificacoes.Add(new Notificacao
                    {
                        Data = DateTime.Now,
                        Mensagem = "Mensagem de teste",
                        IdDispositivo = Guid.Empty
                    });
                }
                var param = new PagingQueryParam<Notificacao>() { CurrentPage = 1, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    notificacoes
                };
            }
        }
    }
}
