CREATE TABLE [dbo].[OLDGOLD_DEPTH](
 [cm_Greater] [smallint] NULL,
 [cm_Less] [smallint] NULL,
 [Factor] [real] NULL
) ON [PRIMARY]

GO

INSERT INTO OLDGOLD_DEPTH([cm_Greater],[cm_Less],[Factor])VALUES('0','5','0.2')
go
INSERT INTO OLDGOLD_DEPTH([cm_Greater],[cm_Less],[Factor])VALUES('6','15','0.5')
go
INSERT INTO OLDGOLD_DEPTH([cm_Greater],[cm_Less],[Factor])VALUES('16','50','0.8')
go
INSERT INTO OLDGOLD_DEPTH([cm_Greater],[cm_Less],[Factor])VALUES('51','900','1')
go

insert into users
select * from PAS_DNK_Syncromine_ForRestore.dbo.users 
insert into users_section
select * from PAS_DNK_Syncromine_ForRestore.dbo.users_section 