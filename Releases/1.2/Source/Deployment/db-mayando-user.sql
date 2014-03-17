/* Before executing this script:
    - Replace {DBNAME} with the name of your database (e.g. Mayando).
    - Replace {DBUSER} with the database user name used to access your database from
      the application.
    - Replace {DBPASSWORD} with the database password used to access your database
      from the application.
*/

USE {DBNAME}

/**********************************************************************/
/* Creates a login and makes the user a member of db_owner            */
/**********************************************************************/

-- Declare variables for database name, username and password
DECLARE @dbName sysname,
        @dbUser sysname,
        @dbPwd nvarchar(max);

-- Set variables for database name, username and password
SET @dbName = '{DBNAME}';
SET @dbUser = '{DBUSER}';
SET @dbPwd = '{DBPASSWORD}';

DECLARE @cmd nvarchar(max)

-- Create login
IF( SUSER_SID(@dbUser) is null )
BEGIN
    print '-- Creating login '
    SET @cmd = N'CREATE LOGIN ' + quotename(@dbUser) + N' WITH PASSWORD ='''+ replace(@dbPwd, '''', '''''') + N''''
    EXEC(@cmd)
END

-- Create database user and map to login
-- and add user to the datareader, datawriter, ddladmin and securityadmin roles
--
SET @cmd = N'USE ' + quotename(@DBName) + N'; 
IF( NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = ''' + replace(@dbUser, '''', '''''') + N'''))
BEGIN
    print ''-- Creating user'';
    CREATE USER ' + quotename(@dbUser) + N' FOR LOGIN ' + quotename(@dbUser) + N';
    print ''-- Adding user'';
    EXEC sp_addrolemember ''db_owner'', ''' + replace(@dbUser, '''', '''''') + N''';
END'
EXEC(@cmd)
GO