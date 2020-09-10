﻿DELETE Recipes; --Remove exist recipes
SET IDENTITY_INSERT Recipes ON;
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (1, 5, N'Борщ Украинский', N'Стародявний рецепт украинского борща');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (2, 2, N'Окрошка', N'Тонизирующая холодная закуска');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (3, 7, N'Бисквит', N'Бисквит по бабушкиному рецепту');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (4, 7, N'Медовик', N'Рецепт маминого медовика');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (5, 7, N'Торт "Спартак"', N'Рецепт от мамы');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (6, 7, N'Торт "Три молока"', N'Рецепт от мамы');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (7, 7, N'Торт "Губка Боб"', N'Рецепт от мамы');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (8, 7, N'Торт "Зебра"', N'Рецепт от мамы');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (9, 7, N'Торт "Сникерс"', N'Рецепт от мамы');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (10, 7, N'Торт "Брауни"', N'Рецепт от мамы');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (11, 3, N'Классический чизкейк', N'Рецепт классического чизкейка');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (12, 3, N'Дульсе-де-лече', N'Аргентина');

SET IDENTITY_INSERT Recipes OFF;