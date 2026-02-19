CREATE DATABASE RedarborApiDb;
GO

-- Crear usuario de aplicación
CREATE LOGIN RedarborUser WITH PASSWORD = 'RedarBor123!';

USE RedarborApiDb;
GO
CREATE USER RedarborUser FOR LOGIN RedarborUser;
ALTER ROLE db_datareader ADD MEMBER RedarborUser;
ALTER ROLE db_datawriter ADD MEMBER RedarborUser;
