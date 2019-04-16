---CHANGE DATABASE NAME!!!!

/****** Object:  Synonym [dbo].[tblDepartments]    Script Date: 2017/09/08 11:06:31 AM ******/
DROP SYNONYM [dbo].[tblDepartments]
GO

/****** Object:  Synonym [dbo].[tblDepartments]    Script Date: 2017/09/08 11:06:31 AM ******/
CREATE SYNONYM [dbo].[tblDepartments] FOR [Syncromine].[DBO].[tblDepartments]
GO


DROP SYNONYM [dbo].[tblProfiles]
GO

/****** Object:  Synonym [dbo].[tblProfiles]    Script Date: 2017/09/08 11:06:41 AM ******/
CREATE SYNONYM [dbo].[tblProfiles] FOR [Syncromine].[DBO].[tblProfiles]
GO


DROP SYNONYM [dbo].[tblUsers]
GO

/****** Object:  Synonym [dbo].[tblUsers]    Script Date: 2017/09/08 11:06:44 AM ******/
CREATE SYNONYM [dbo].[tblUsers] FOR [Syncromine].[DBO].[tblUsers]
GO