--������� ������ 3 ������� � �������������, ������� ��������� � ��������� � ID = 3 ��� ����� ������������
select top 3 * 
from Recipes r
left join RecipeIngredients ri on ri.RecipeId = r.Id
left join Ingredients i on i.Id = ri.IngredientId
--where CategoryId = 7