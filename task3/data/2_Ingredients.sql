﻿DELETE Ingredients; --Remove exist ingredients
SET IDENTITY_INSERT Ingredients ON;
INSERT INTO Ingredients (Id, Name) VALUES (1, N'Мука');
INSERT INTO Ingredients (Id, Name) VALUES (2, N'Сахар');
INSERT INTO Ingredients (Id, Name) VALUES (3, N'Яйца');
INSERT INTO Ingredients (Id, Name) VALUES (4, N'Сода');
INSERT INTO Ingredients (Id, Name) VALUES (5, N'Сметана');
INSERT INTO Ingredients (Id, Name) VALUES (6, N'Картошка');
INSERT INTO Ingredients (Id, Name) VALUES (7, N'Капуста');
INSERT INTO Ingredients (Id, Name) VALUES (8, N'Помидор');
INSERT INTO Ingredients (Id, Name) VALUES (9, N'Перец');
INSERT INTO Ingredients (Id, Name) VALUES (10, N'Мед');
INSERT INTO Ingredients (Id, Name) VALUES (11, N'Цукаты');
INSERT INTO Ingredients (Id, Name) VALUES (12, N'Молоко');
INSERT INTO Ingredients (Id, Name) VALUES (13, N'Творог');
SET IDENTITY_INSERT Ingredients OFF;