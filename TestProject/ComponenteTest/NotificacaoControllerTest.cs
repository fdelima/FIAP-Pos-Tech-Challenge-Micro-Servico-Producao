﻿//using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain;
//using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
//using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Models;
//using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.ValuesObject;
//using Newtonsoft.Json;
//using System.Net.Http.Json;
//using TestProject.Infra;
//using Xunit.Gherkin.Quick;

//namespace TestProject.ComponenteTest
//{
//    /// <summary>
//    /// Classe de teste.
//    /// </summary>
//    [FeatureFile("./BDD/Features/ControlarNotificacaos.feature")]
//    public class NotificacaoControllerTest : Feature, IClassFixture<ComponentTestsBase>
//    {
//        private readonly ApiTestFixture _apiTest;
//        private ModelResult expectedResult;
//        Notificacao _notificacao;

//        /// <summary>
//        /// Construtor da classe de teste.
//        /// </summary>
//        public NotificacaoControllerTest(ComponentTestsBase data)
//        {
//            _apiTest = data._apiTest;
//        }
//        private class ActionResult
//        {
//            public List<string> Messages { get; set; }
//            public List<string> Errors { get; set; }
//            public Notificacao Model { get; set; }
//            public bool IsValid { get; set; }
//        }

//        [Given(@"Recebendo um notificacao na lanchonete vamos preparar o notificacao")]
//        public void PrepararNotificacao()
//        {
//            _notificacao = new Notificacao
//            {
//                IdDispositivo = Guid.NewGuid(),
//                Mensagem = "Mensagem de teste",
//            };
//        }

//        [And(@"Adicionar o notificacao")]
//        public async Task AdicionarNotificacao()
//        {
//            expectedResult = ModelResultFactory.InsertSucessResult<Notificacao>(_notificacao);

//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.PostAsJsonAsync(
//                "api/producao/notificacao", _notificacao);

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);

//            _notificacao = actualResult.Model;

//            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
//            Assert.Equal(expectedResult.Messages, actualResult.Messages);
//            Assert.Equal(expectedResult.Errors, actualResult.Errors);

//            Assert.True(true);
//        }

//        [And(@"Encontrar o notificacao")]
//        public async Task EncontrarNotificacao()
//        {
//            expectedResult = ModelResultFactory.SucessResult(_notificacao);

//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.GetAsync(
//                $"api/producao/notificacao/{_notificacao.IdNotificacao}");

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
//            _notificacao = actualResult.Model;

//            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
//            Assert.Equal(expectedResult.Messages, actualResult.Messages);
//            Assert.Equal(expectedResult.Errors, actualResult.Errors);
//        }

//        [And(@"Alterar o notificacao")]
//        public async Task AlterarNotificacao()
//        {
//            expectedResult = ModelResultFactory.UpdateSucessResult<Notificacao>(_notificacao);

//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.PutAsJsonAsync(
//                $"api/producao/notificacao/{_notificacao.IdNotificacao}", _notificacao);

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
//            _notificacao = actualResult.Model;

//            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
//            Assert.Equal(expectedResult.Messages, actualResult.Messages);
//            Assert.Equal(expectedResult.Errors, actualResult.Errors);
//        }

//        [And(@"Consultar o notificacao")]
//        public async Task ConsultarNotificacao()
//        {
//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.PostAsJsonAsync(
//                $"api/producao/notificacao/consult", new PagingQueryParam<Notificacao> { ObjFilter = _notificacao });

//            var responseContent = await response.Content.ReadAsStringAsync();
//            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

//            Assert.True(actualResult.content != null);
//        }

//        [And(@"Listar o notificacao")]
//        public async Task ListarPardido()
//        {
//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.GetAsync(
//                $"api/producao/notificacao/Lista");

//            var responseContent = await response.Content.ReadAsStringAsync();
//            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

//            Assert.True(actualResult.content != null);
//        }

//        [And(@"Iniciar preparação do notificacao")]
//        public async Task IniciarPreparacaoNotificacao()
//        {
//            expectedResult = ModelResultFactory.UpdateSucessResult<Notificacao>(_notificacao);

//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.PatchAsJsonAsync(
//                $"api/producao/notificacao/{_notificacao.IdNotificacao}/IniciarPreparacao", _notificacao.IdNotificacao);

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
//            _notificacao = actualResult.Model;

//            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
//            Assert.Equal(expectedResult.Messages, actualResult.Messages);
//            Assert.Equal(expectedResult.Errors, actualResult.Errors);
//        }

//        [And(@"Finalizar preparação do notificacao")]
//        public async Task FinalizarPreparacaoNotificacao()
//        {
//            expectedResult = ModelResultFactory.UpdateSucessResult<Notificacao>(_notificacao);

//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.PatchAsJsonAsync(
//                $"api/producao/notificacao/{_notificacao.IdNotificacao}/FinalizarPreparacao", _notificacao.IdNotificacao);

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
//            _notificacao = actualResult.Model;

//            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
//            Assert.Equal(expectedResult.Messages, actualResult.Messages);
//            Assert.Equal(expectedResult.Errors, actualResult.Errors);
//        }

//        [When(@"Finalizar o notificacao")]
//        public async Task FinalizarNotificacao()
//        {
//            expectedResult = ModelResultFactory.UpdateSucessResult<Notificacao>(_notificacao);

//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.PatchAsJsonAsync(
//                $"api/producao/notificacao/{_notificacao.IdNotificacao}/Finalizar", _notificacao.IdNotificacao);

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
//            _notificacao = actualResult.Model;

//            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
//            Assert.Equal(expectedResult.Messages, actualResult.Messages);
//            Assert.Equal(expectedResult.Errors, actualResult.Errors);
//        }

//        [Then(@"posso deletar o notificacao")]
//        public async Task DeletarNotificacao()
//        {
//            expectedResult = ModelResultFactory.DeleteSucessResult<Notificacao>();

//            var client = _apiTest.GetClient();
//            HttpResponseMessage response = await client.DeleteAsync(
//                $"api/producao/notificacao/{_notificacao.IdNotificacao}");

//            var responseContent = await response.Content.ReadAsStringAsync();
//            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
//            _notificacao = null;

//            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
//            Assert.Equal(expectedResult.Messages, actualResult.Messages);
//            Assert.Equal(expectedResult.Errors, actualResult.Errors);
//        }
//    }
//}
