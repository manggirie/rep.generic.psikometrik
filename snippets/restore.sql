RESTORE FILELISTONLY
FROM DISK = 'D:\temp\epsikologi_310516'
GO


----Make Database to single user Mode
ALTER DATABASE epsikologi
SET SINGLE_USER WITH
ROLLBACK IMMEDIATE

----Restore Database
RESTORE DATABASE epsikologi
FROM DISK = 'D:\temp\epsikologi_310516'
WITH MOVE 'epsikologi' TO 'd:\temp\epsikologi.mdf',
MOVE 'epsikologi_log' TO 'd:\temp\epsikologi_log.ldf'

/*If there is no error in statement before database will be in multiuser
mode.
If error occurs please execute following command it will convert
database in multi user.*/
ALTER DATABASE epsikologi SET MULTI_USER
GO