DELETE Categories WHERE ParentId IS NOT NULL; --Remove sub categories
DELETE Categories; --Remove root categories
SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (Id, ParentId, Name) VALUES (1, NULL, N'������� �����');
INSERT INTO Categories (Id, ParentId, Name) VALUES (2, NULL, N'�������� �����');
INSERT INTO Categories (Id, ParentId, Name) VALUES (3, NULL, N'�������');
INSERT INTO Categories (Id, ParentId, Name) VALUES (4, 1, N'����');
INSERT INTO Categories (Id, ParentId, Name) VALUES (5, 1, N'�����');
INSERT INTO Categories (Id, ParentId, Name) VALUES (6, 2, N'������');
INSERT INTO Categories (Id, ParentId, Name) VALUES (7, 3, N'�����');
SET IDENTITY_INSERT Categories OFF;