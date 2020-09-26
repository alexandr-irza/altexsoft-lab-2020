﻿using RecipeBook2.Core.Interfaces;

namespace RecipeBook2.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private RecipeBookContext _context;
        public ICategoryRepository Categories { get; }
        public IIngredientRepository Ingredients { get; }
        public IRecipeRepository Recipes { get; }
        public IRecipeIngredientRepository RecipeIngredients { get; }
        public IRecipeStepRepository RecipeSteps { get; }

        public UnitOfWork(RecipeBookContext context, ICategoryRepository categoryRepository, 
            IIngredientRepository ingredientRepository, IRecipeRepository recipeRepository, IRecipeIngredientRepository recipeIngredientRepository,
            IRecipeStepRepository recipeStepRepository)
        {
            Categories = categoryRepository;
            Recipes = recipeRepository;
            Ingredients = ingredientRepository;
            RecipeIngredients = recipeIngredientRepository;
            RecipeSteps = recipeStepRepository;
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}