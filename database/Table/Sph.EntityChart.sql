IF OBJECT_ID('Sph.EntityChart', 'U') IS NOT NULL
  DROP TABLE Sph.EntityChart
GO


CREATE TABLE Sph.EntityChart
(
	 [Id] VARCHAR(50) PRIMARY KEY NOT NULL
	,[EntityDefinitionId] VARCHAR(50) NOT NULL
	,[Entity] VARCHAR(255) NOT NULL
	,[EntityViewId] VARCHAR(50) NOT NULL
	,[IsDashboardItem] BIT NOT NULL DEFAULT 0
	,[Json] VARCHAR(MAX) NOT NULL
	,[Name] VARCHAR(255) NOT NULL
	,[CreatedDate] SMALLDATETIME NOT NULL DEFAULT GETDATE()
	,[CreatedBy] VARCHAR(255) NULL
	,[ChangedDate] SMALLDATETIME NOT NULL DEFAULT GETDATE()
	,[ChangedBy] VARCHAR(255) NULL
)
GO 
