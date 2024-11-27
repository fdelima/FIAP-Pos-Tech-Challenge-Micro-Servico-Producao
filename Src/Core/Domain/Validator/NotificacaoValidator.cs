using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class NotificacaoValidator : AbstractValidator<Notificacao>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public NotificacaoValidator()
        {
            RuleFor(c => c.IdDispositivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Mensagem).NotEmpty().WithMessage(ValidationMessages.RequiredField);
        }
    }
}
