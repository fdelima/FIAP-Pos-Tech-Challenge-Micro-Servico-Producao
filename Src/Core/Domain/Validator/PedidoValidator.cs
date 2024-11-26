using FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Messages;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Producao.Domain.Validator
{
    /// <summary>
    /// Regras de validação da model
    /// </summary>
    public class PedidoValidator : AbstractValidator<Entities.Pedido>
    {
        /// <summary>
        /// Contrutor das regras de validação da model
        /// </summary>
        public PedidoValidator()
        {
            RuleFor(c => c.IdPedido).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.IdDispositivo).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.Data).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.DataStatusPagamento).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.DataStatusPedido).NotEmpty().WithMessage(ValidationMessages.RequiredField);
            RuleFor(c => c.PedidoItems).Must(x => x.Count() > 0).WithMessage(ValidationMessages.OneMandatoryItem);
            RuleForEach(c => c.PedidoItems).SetValidator(x => new PedidoItemValidator());
        }
    }
}
