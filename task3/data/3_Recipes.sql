DELETE Recipes; --Remove exist recipes
SET IDENTITY_INSERT Recipes ON;
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (1, 5, N'Борщ Украинский', N'Стародявний рецепт украинского борща');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (2, 2, N'Окрошка', N'Тонизирующая холодная закуска');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (3, 7, N'Бисквит', N'Бисквит по бабушкиному рецепту');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (4, 7, N'Медовик', N'Рецепт маминого медовика');
SET IDENTITY_INSERT Recipes OFF;