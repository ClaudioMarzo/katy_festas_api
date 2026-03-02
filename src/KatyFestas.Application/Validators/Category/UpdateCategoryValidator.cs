using FluentValidation;
using KatyFestas.Application.DTOs.Category;

namespace KatyFestas.Application.Validators.Category;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Name));
    }
}
