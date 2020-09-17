TRUNCATE TABLE RecipeSteps; --Remove exist recipes
TRUNCATE TABLE RecipeIngredients; --Remove exist data
DELETE Recipes; --Remove exist recipes
DELETE Ingredients; --Remove exist ingredients
DELETE Categories WHERE ParentId IS NOT NULL; --Remove sub categories
DELETE Categories; --Remove root categories
