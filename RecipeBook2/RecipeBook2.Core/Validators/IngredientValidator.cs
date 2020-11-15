using FluentValidation;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Core.Validators
{
    public class IngredientValidator : AbstractValidator<Ingredient>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 100).WithMessage("{PropertyName} should be more than {MinLength}  chars and less than {MaxLength} chars");
        }
    }
}
