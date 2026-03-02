using FluentValidation;
using KatyFestas.Application.DTOs.Item;

namespace KatyFestas.Application.Validators.Item;

public class UpdateItemValidator : AbstractValidator<UpdateItemDto>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Descrição deve ter no máximo 1000 caracteres")
            .When(x => !string.IsNullOrWhiteSpace(x.Description));

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero")
            .When(x => x.Price.HasValue);

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Estoque não pode ser negativo")
            .When(x => x.StockQuantity.HasValue);
    }
}
