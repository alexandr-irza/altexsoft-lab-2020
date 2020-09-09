--¬ыбрать первые 3 рецепта с ингридиентами, которые наход€тс€ в категории с ID = 3 или любой подкатегории
select top 3 * 
from Recipes r
left join RecipeIngredients ri on ri.RecipeId = r.Id
left join Ingredients i on i.Id = ri.IngredientId
--where CategoryId = 7