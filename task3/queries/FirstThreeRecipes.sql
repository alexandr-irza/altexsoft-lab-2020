--Выбрать первые 3 рецепта с ингридиентами, которые находятся в категории с ID = 3 или любой подкатегории
--чтобы общее количество записей было 3. Например, у Вас есть в категории с айди=3 один рецепт, 
--а также еще 3 подкатегории, и в каждой по 2 рецепта. В результате должно быть: 1 запись из 3 категории и еще 2 записи из подкатегории

with Categories_CTE(CategoryId, CategoryParentId, CategoryName, CategoryPath)
AS
(
	select c.Id, c.ParentId, c.Name, cast(c.Id as nvarchar(max)) + '\' as CategoryPath
	from Categories c
	where c.ParentId is null
	union all
	select c.Id, c.ParentId, c.Name, CategoryPath + cast(c.Id as nvarchar(max)) + '\'
	from Categories c
	inner join Categories_CTE cc on cc.CategoryId = c.ParentId
)

select x.Id as RecipeId, x.Name as RecipeName, ri.IngredientId, i.Name as IngredientName, x.CategoryId, x.CategoryPath
from ( select top 3 r.Id, r.Name, c.CategoryId, c.CategoryPath
	from Recipes r
	inner join Categories_CTE c on c.CategoryId = r.CategoryId
	where c.CategoryPath like '3\%'
	order by c.CategoryPath) x
left join RecipeIngredients ri on ri.RecipeId = x.Id
left join Ingredients i on ri.IngredientId = i.Id
