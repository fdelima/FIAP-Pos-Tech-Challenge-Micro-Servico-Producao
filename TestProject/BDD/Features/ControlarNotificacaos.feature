Feature: ControlarNotificacaos
	Para controlar os notificacaos da lanchonete
	Eu preciso das seguindes funcionalidades
	Adicionar um notificacao
	Alterar um notificacao
	Consultar um notificacao
	Listar os notificacaos
	Iniciar preparação do notificacao
	Finalizar preparação do notificacao
	Finalizar o notificacao
	Deletar um notificacao

Scenario: Controlar notificacaos
	Given Recebendo um notificacao na lanchonete vamos preparar o notificacao
	And Adicionar o notificacao
	And Encontrar o notificacao
	And Alterar o notificacao
	And Consultar o notificacao
	And Listar o notificacao
	And Iniciar preparação do notificacao
	And Finalizar preparação do notificacao
	When Finalizar o notificacao
	Then posso deletar o notificacao