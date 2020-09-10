--Выбрать первые 3 рецепта с ингридиентами, которые находятся в категории с ID = 3 или любой подкатегории
select x.Id as RecipeId, x.Name as RecipeName, ri.IngredientId, i.Name as IngredientName, x.CategotyId, x.CategoryName
from ( select top 3  r.Id, r.Name, c.Id as CategotyId, c.Name as CategoryName
	from Recipes r
	inner join Categories c on r.CategoryId = c.Id
	where c.Id = 3 or c.ParentId = 3
	order by c.ParentId) x
left join RecipeIngredients ri on ri.RecipeId = x.Id
left join Ingredients i on ri.IngredientId = i.Id
