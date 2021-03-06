﻿using FluentValidation;
using RecipeBook2.Core.Entities;

namespace RecipeBook2.Core.Validators
{
    public class CategoryValidator : AbstractValidator<Category> 
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 100).WithMessage("{PropertyName} should be more than {MinLength}  chars and less than {MaxLength} chars");
        }
    }
}
