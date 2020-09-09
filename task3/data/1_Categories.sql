DELETE Categories WHERE ParentId IS NOT NULL; --Remove sub categories
DELETE Categories; --Remove root categories
SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (Id, ParentId, Name) VALUES (1, NULL, N'Горячие блюда');
INSERT INTO Categories (Id, ParentId, Name) VALUES (2, NULL, N'Холодные блюда');
INSERT INTO Categories (Id, ParentId, Name) VALUES (3, NULL, N'Десерты');
INSERT INTO Categories (Id, ParentId, Name) VALUES (4, 1, N'Супы');
INSERT INTO Categories (Id, ParentId, Name) VALUES (5, 1, N'Борщи');
INSERT INTO Categories (Id, ParentId, Name) VALUES (6, 2, N'Салаты');
INSERT INTO Categories (Id, ParentId, Name) VALUES (7, 3, N'Торты');
SET IDENTITY_INSERT Categories OFF;