using FluentValidation;
using KatyFestas.Application.DTOs.Category;

namespace KatyFestas.Application.Validators.Category;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryDto>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.StoreId)
            .NotEmpty().WithMessage("StoreId é obrigatório.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.");
    }
}
