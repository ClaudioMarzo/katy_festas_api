using FluentValidation;
using KatyFestas.Application.DTOs.Item;

namespace KatyFestas.Application.Validators.Item;

public class CreateItemValidator : AbstractValidator<CreateItemDto>
{
    public CreateItemValidator()
    {
        RuleFor(x => x.StoreId)
            .NotEmpty().WithMessage("StoreId é obrigatório.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId é obrigatório.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição é obrigatória.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Preço deve ser maior que zero.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantidade em estoque não pode ser negativa.");
    }
}
