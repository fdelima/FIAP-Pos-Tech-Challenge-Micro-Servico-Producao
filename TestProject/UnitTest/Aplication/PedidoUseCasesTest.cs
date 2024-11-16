using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.ValuesObject;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoUseCasesTest
    {
        private readonly IPedidoService _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoUseCasesTest()
        {
            _service = Substitute.For<IPedidoService>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdDispositivo = idDispositivo,
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoPostCommand command = new PedidoPostCommand(pedido);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(pedido)));

            //Act
            PedidoPostHandler handler = new PedidoPostHandler(_service);
            ModelResult result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(Guid idDispositivo)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdDispositivo = idDispositivo
            };

            PedidoPostCommand command = new PedidoPostCommand(pedido);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Pedido>()));

            //Act
            PedidoPostHandler handler = new PedidoPostHandler(_service);
            ModelResult result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoPutCommand command = new PedidoPutCommand(idPedido, pedido);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            PedidoPutHandler handler = new PedidoPutHandler(_service);
            ModelResult result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idPedido, Guid idDispositivo)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo
            };

            PedidoPutCommand command = new PedidoPutCommand(idPedido, pedido);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(pedido)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Pedido>()));

            //Act
            PedidoPutHandler handler = new PedidoPutHandler(_service);
            ModelResult result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarPedido(Guid idPedido)
        {
            ///Arrange
            PedidoDeleteCommand command = new PedidoDeleteCommand(idPedido);

            //Mockando retorno do serviço de domínio.
            _service.DeleteAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            PedidoDeleteHandler handler = new PedidoDeleteHandler(_service);
            ModelResult result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorId(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoFindByIdCommand command = new PedidoFindByIdCommand(idPedido);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(pedido)));

            //Act
            PedidoFindByIdHandler handler = new PedidoFindByIdHandler(_service);
            ModelResult result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoComCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            PagingQueryParam<Pedido> param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };
            PedidoGetItemsCommand command = new PedidoGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, bool>>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));

            //Act
            PedidoGetItemsHandler handler = new PedidoGetItemsHandler(_service);
            PagingQueryResult<Pedido> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }


        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoSemCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> pedidos)
        {
            ///Arrange
            PedidoGetItemsCommand command = new PedidoGetItemsCommand(filter, sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(pedidos))));

            //Act
            PedidoGetItemsHandler handler = new PedidoGetItemsHandler(_service);
            PagingQueryResult<Pedido> result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.Content.Any());
        }

        #region [ Xunit MemberData ]

        /// <summary>
        /// Mock de dados
        /// </summary>
        public static IEnumerable<object[]> ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
        {
            switch (tipo)
            {
                case enmTipo.Inclusao:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return PedidoMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                        return PedidoMock.ObterDadosConsulta(quantidade);
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return PedidoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return PedidoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
