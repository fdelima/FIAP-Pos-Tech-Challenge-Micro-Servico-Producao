using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Application.UseCases.Pedido.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Services;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Validator;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.ValuesObject;
using FluentValidation;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Domain
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class PedidoServiceTest
    {
        private readonly IGateways<Pedido> _gatewayPedidoMock;
        private readonly IValidator<Pedido> _validator;
        private readonly IGateways<Notificacao> _notificacaoGatewayMock;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public PedidoServiceTest()
        {
            _validator = new PedidoValidator();
            _gatewayPedidoMock = Substitute.For<IGateways<Pedido>>();
            _notificacaoGatewayMock = Substitute.For<IGateways<Notificacao>>();
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
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Act
            ModelResult result = await domainService.InsertAsync(pedido);

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
                IdDispositivo = idDispositivo,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Act
            ModelResult result = await domainService.InsertAsync(pedido);

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

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayPedidoMock.UpdateAsync(Arg.Any<Pedido>())
                .Returns(Task.FromResult(pedido));

            //Act
            ModelResult result = await domainService.UpdateAsync(pedido);

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

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Act
            ModelResult result = await domainService.UpdateAsync(pedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarPedido(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            _gatewayPedidoMock.DeleteAsync(idPedido)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            ModelResult result = await domainService.DeleteAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorIdComDadosValidos(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            ModelResult result = await domainService.FindByIdAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarPedidoPorIdComDadosInvalidos(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Act
            ModelResult result = await domainService.FindByIdAsync(idPedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedido(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));


            //Act
            PagingQueryResult<Pedido> result = await domainService.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoComCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            PagingQueryParam<Pedido> param = new PagingQueryParam<Pedido>() { CurrentPage = 1, Take = 10 };
            PedidoGetItemsCommand command = new PedidoGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Pedido>>(),
                Arg.Any<Expression<Func<Pedido, bool>>>(),
                Arg.Any<Expression<Func<Pedido, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));

            //Act
            PagingQueryResult<Pedido> result = await _gatewayPedidoMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarPedidoSemCondicao(IPagingQueryParam filter, Expression<Func<Pedido, object>> sortProp, IEnumerable<Pedido> Pedidos)
        {
            ///Arrange
            PedidoGetItemsCommand command = new PedidoGetItemsCommand(filter, sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayPedidoMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Pedido>>(new PagingQueryResult<Pedido>(new List<Pedido>(Pedidos))));

            //Act
            PagingQueryResult<Pedido> result = await _gatewayPedidoMock.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa o iniciar preparação com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task IniciarPreparacaoComDadosValidos(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            ModelResult result = await domainService.IniciarPreparacaoAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa o iniciar preparação com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task IniciarPreparacaoComDadosInvalidos(Guid idPedido, Guid idDispositivo)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            ModelResult result = await domainService.IniciarPreparacaoAsync(idPedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa o finalizar preparação com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task FinalizarPreparacaoComDadosValidos(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                Status = enmPedidoStatus.EM_PREPARACAO.ToString(),
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            ModelResult result = await domainService.FinalizarPreparacaoAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa o finalizar preparação com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task FinalizarPreparacaoComDadosInvalidos(Guid idPedido, Guid idDispositivo)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            ModelResult result = await domainService.FinalizarPreparacaoAsync(idPedido);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa o finalizar com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task FinalizarComDadosValidos(Guid idPedido, Guid idDispositivo, enmPedidoStatusPagamento statusPagamento)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                Status = enmPedidoStatus.PRONTO.ToString(),
                StatusPagamento = statusPagamento.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            ModelResult result = await domainService.FinalizarAsync(idPedido);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa o finalizar preparação com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task FinalizarComDadosInvalidos(Guid idPedido, Guid idDispositivo)
        {
            ///Arrange
            Pedido pedido = new Pedido
            {
                IdPedido = idPedido,
                IdDispositivo = idDispositivo,
                Status = enmPedidoStatus.RECEBIDO.ToString(),
                StatusPagamento = enmPedidoStatusPagamento.PENDENTE.ToString()
            };

            PedidoService domainService = new PedidoService(_gatewayPedidoMock, _validator, _notificacaoGatewayMock);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayPedidoMock.FindByIdAsync(idPedido)
                .Returns(new ValueTask<Pedido>(pedido));

            //Act
            ModelResult result = await domainService.FinalizarAsync(idPedido);

            //Assert
            Assert.False(result.IsValid);
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
                default:
                    return null;
            }
        }

        #endregion

    }
}
