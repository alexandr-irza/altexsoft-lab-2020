DELETE Ingredients; --Remove exist ingredients
SET IDENTITY_INSERT Ingredients ON;
INSERT INTO Ingredients (Id, Name) VALUES (1, N'����');
INSERT INTO Ingredients (Id, Name) VALUES (2, N'�����');
INSERT INTO Ingredients (Id, Name) VALUES (3, N'����');
INSERT INTO Ingredients (Id, Name) VALUES (4, N'����');
INSERT INTO Ingredients (Id, Name) VALUES (5, N'�������');
INSERT INTO Ingredients (Id, Name) VALUES (6, N'��������');
INSERT INTO Ingredients (Id, Name) VALUES (7, N'�������');
INSERT INTO Ingredients (Id, Name) VALUES (8, N'�������');
INSERT INTO Ingredients (Id, Name) VALUES (9, N'�����');
INSERT INTO Ingredients (Id, Name) VALUES (10, N'���');
SET IDENTITY_INSERT Ingredients OFF;