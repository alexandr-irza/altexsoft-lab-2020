DELETE Recipes; --Remove exist recipes
SET IDENTITY_INSERT Recipes ON;
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (1, 5, N'���� ����������', N'����������� ������ ����������� �����');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (2, 2, N'�������', N'������������ �������� �������');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (3, 7, N'�������', N'������� �� ����������� �������');
INSERT INTO Recipes (Id, CategoryId, Name, Description) VALUES (4, 7, N'�������', N'������ �������� ��������');
SET IDENTITY_INSERT Recipes OFF;