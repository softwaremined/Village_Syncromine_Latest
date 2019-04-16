insert into hierarch
select hierarchicalid, description from pas_central.dbo.hierarch where hierarchicaltype = 'Pro'
go
--insert into section
--select prodmonth, sectionid, name, reporttosectionid, hierarchicalid from pas_central.dbo.section
--where prodmonth in 
--('201507', '201508', '201509', '201510', '201511', '201512',
--'201601', '201602', '201603', '201603', '201604', '201605', '201606', '201607', '201608', '201609', '201610', '201611', '201612',
--'201701', '201702', '201703', '201703', '201704', '201705', '201706', '201707', '201708', '201709', '201710', '201711', '201712')
--go

--insert into [SECTION_COMPLETE]
--select s.prodmonth, s.sectionid, s.name, s1.sectionid, s1.name
--, s2.sectionid, s2.name, s3.sectionid, s3.name , s4.sectionid, s4.name
--, s5.sectionid, s5.name  
--from section s, section s1, section s2, section s3, section s4, section s5 
--where s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth 
--and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth 
--and s2.reporttosectionid = s3.sectionid and s2.prodmonth = s3.prodmonth 
--and s3.reporttosectionid = s4.sectionid and s3.prodmonth = s4.prodmonth
--and s4.reporttosectionid = s5.sectionid and s4.prodmonth = s5.prodmonth

--and s.hierarchicalid = 6 
--go
insert into [CODE_ACTIVITY] select * from pas_central.dbo.[CODE_ACTIVITY]
insert into [WPTYPE_SETUPCODES] select * from pas_central.dbo.[WPTYPE_SETUPCODES]
insert into [WPTYPE_SETUP] select * from pas_central.dbo.[WPTYPE_SETUP]
insert into [WORKPLACE_NAME_CHANGE] select * from pas_central.dbo.[WORKPLACE_NAME_CHANGE]
insert into [WORKPLACE_INACTIVATION_REASON] select * from pas_central.dbo.[WORKPLACE_INACTIVATION_REASON]

go
insert into [REEF] select * from pas_central.dbo.[REEF]
insert into [CODE_WPTYPEWPNumberLink] select * from pas_central.dbo.[CODE_WPTYPEWPNumberLink]
insert into [CODE_WPTYPEWPDirectionLink] select * from pas_central.dbo.[CODE_WPTYPEWPDirectionLink]
insert into [CODE_WPTYPEWPDetailLink] select * from pas_central.dbo.[CODE_WPTYPEWPDetailLink]
insert into [CODE_WPTYPEWPDescNoLink] select * from pas_central.dbo.[CODE_WPTYPEWPDescNoLink]
insert into [CODE_WPTYPEWPDescLink] select * from pas_central.dbo.[CODE_WPTYPEWPDescLink]
insert into [CODE_WPTypeLevelLink] select * from pas_central.dbo.[CODE_WPTypeLevelLink]
insert into [CODE_WPTypeGridLink] select * from pas_central.dbo.[CODE_WPTypeGridLink]
insert into [Code_WPTypeClassifyChange] select * from pas_central.dbo.[Code_WPTypeClassifyChange]
insert into [CODE_WPTYPE] select * from pas_central.dbo.[CODE_WPTYPE]
go
insert into [CODE_WPSTATUS] select * from pas_central.dbo.[CODE_WPSTATUS]
insert into [CODE_WPNUMBER] select * from pas_central.dbo.[CODE_WPNUMBER]
insert into [CODE_WPDIVISION] select divisioncode, description, selected, density, editable, WPLastUsed from pas_central.dbo.[CODE_WPDIVISION]
insert into [CODE_WPDETAIL] select * from pas_central.dbo.[CODE_WPDETAIL]
insert into [CODE_WPDESCRIPTIONNO] select * from pas_central.dbo.[CODE_WPDESCRIPTIONNO]
insert into [CODE_WPDESCRIPTION] select * from pas_central.dbo.[CODE_WPDESCRIPTION]
insert into [CODE_WPCLASSIFICATION] select * from pas_central.dbo.[CODE_WPCLASSIFICATION]
insert into [STOPINGPROCESSCODES] select name from pas_central.dbo.[STOPINGPROCESSCODES]
insert into [ENDTYPE] select * from pas_central.dbo.[ENDTYPE]
insert into [CODE_OREFLOW] select * from pas_central.dbo.[CODE_OREFLOW]
insert into [OREFLOWENTITIES] select divisioncode+oreflowid, oreflowid, * from pas_central.dbo.[OREFLOWENTITIES] o  inner join  pas_central.dbo.code_wpdivision d on o.mine2 = d.mine where d.selected = 'Y' and d.Editable = 1
insert into [COMMONAREAS] select name from pas_central.dbo.[COMMONAREAS]
insert into [COMMONAREASUBSECTIONS] select commonarea,name from pas_central.dbo.[COMMONAREASUBSECTIONS]
go

insert into [WORKPLACE] select * from pas_central.dbo.[WORKPLACE]
go
insert into [WORKPLACE_CLASSIFICATION] select * from pas_central.dbo.[WORKPLACE_CLASSIFICATION]
insert into [WORKPLACE_INACTIVATION] select * from pas_central.dbo.[WORKPLACE_INACTIVATION]
go
insert into code_calendar select distinct c.calendarcode, description from pas_central.dbo.code_calendar c inner join pas_central.dbo.seccal s on s.calendarcode = c.calendarcode where s.prodmonth >= '201507' 
insert into code_calendar select distinct c.calendarcode, description from pas_central.dbo.code_calendar c where description <> 'section calendar'
insert into seccal select * from pas_central.dbo.seccal where prodmonth >= '201507'
go
INSERT INTO [SAMPLING]
select * from pas_central.dbo.SAMPLING where calendardate>= '20150615'
go

INSERT INTO [CALTYPE] ([CalendarCode], [CalendarDate], [Workingday])
Select * from pas_central.dbo.CALTYPE where CalendarDate >= (select min(begindate) from pas_central.dbo.seccal where prodmonth >= '201507')
GO

INSERT INTO [CALENDARMILL]
SELECT * FROM pas_central.dbo.CALENDARMILL  where MillMonth >= '201507'
go
INSERT INTO CALENDAROTHER
SELECT * FROM pas_central.dbo.CALENDARSAFETY where SafetyMonth >= '201507'
go
INSERT INTO CALENDAROTHER
SELECT * FROM pas_central.dbo.CALENDARcost where costMonth >= '201507'
go
insert into sysset select * from pas_central.dbo.sysset
go

insert into CODE_PROBLEM_TYPE values ('1', 0, 'Safety & Env', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('2', 0, 'Support', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('3', 0, 'Mining', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('4', 0, 'Blasting', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('5', 0, 'Cleaning', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('6', 0, 'Logistics', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('7', 0, 'Labour', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('8', 0, 'Equip.Fail', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('9', 0, 'Materials', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('10', 0, 'Geology', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('11', 0, 'Services', 0, 'Y')
insert into CODE_PROBLEM_TYPE values ('12', 0, 'Trackless', 0, 'Y')
go
insert into CODE_PROBLEM_TYPE select ProblemTypeid, 1, description, color, deleted from code_problem_type
insert into [CODE_PROBLEM]
select ProblemID, Description, 0, 0, 'Y', 1,'Gainsboro', 'N' from pas_central.dbo.problem
go
insert into [CODE_PROBLEM]
select problemid, description, 1, 0, 'Y', 1, 'Gainsboro', 'N' from [CODE_PROBLEM]
INSERT INTO [PROBLEM_TYPE]
select LEFT(Problemgroupcode, CHARINDEX('.', Problemgroupcode) - 1) ,  problemid,0, 'Y'  from pas_central.dbo.problem
INSERT INTO [PROBLEM_TYPE]
select LEFT(Problemgroupcode, CHARINDEX('.', Problemgroupcode) - 1) ,  problemid,1, 'Y'  from pas_central.dbo.problem

INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) Code, HQCat, 0,0,  'Y', null, null   from pas_central.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) + '1' Code, HQCat, 0,0,  'Y', null, null   from pas_central.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
GO
INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) Code, HQCat, 1,0,  'Y', null, null   from pas_central.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) + '1' Code, HQCat, 1,0,  'Y', null, null   from pas_central.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')


INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),0,'Y'  from pas_central.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),0,'Y'  from pas_central.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),1,'Y'  from pas_central.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),1,'Y'  from pas_central.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')

go
insert into crew
select distinct c.calendardate, department,null,null from pas_central.dbo.caltype c , pas_central.dbo.vw_orgunits o 
where c.calendardate >= '20150615'
go
insert into code_planning values ('LP', 'Locked PLanning')
insert into code_planning values ('MP', 'Monthly PLanning')

go
insert into CODE_PREPLANNINgTYPES
select * from cpm_sibanye.dbo.CODE_PREPLANNINgTYPES


insert into [CODE_MINEMETHOD]
select * from cpm_sibanye.dbo.[CODE_MINEMETHOD]
go
insert into [OLDGOLD_UNIT]
select * from cpm_sibanye.dbo.[OLDGOLD_UNIT] 
go
insert into [OLDGOLD_TYPE]
select * from cpm_sibanye.dbo.[OLDGOLD_TYPE] 

insert into [BOOKINGHoisting] select * from pas_central.[dbo].[BOOKINGHoisting] where millmonth >= '201507'
insert into [BOOKINGMilling] select * from pas_central.[dbo].[BOOKINGMilling] where millmonth >= '201507'
insert into [BOOKINGTRAMMING] select * from pas_central.[dbo].[BOOKINGTRAMMING] where millmonth >= '201507'
insert into [BookingTrammingWP] select * from pas_central.[dbo].[BookingTrammingWP] where millmonth >= '201507'
insert into [BookingTrammingWP] select * from pas_central.[dbo].[BUS_PLANNING_STORAGES] where prodmonth >= '201507'
insert into [KRIGING] select * from pas_central.[dbo].[KRIGING]where prodmonth >= '201507'
insert into [PLANNING_STORAGES] select * from pas_central.[dbo].[PLANNING_STORAGES]where prodmonth >= '201507'
insert into [SICCAPTURE] select SICKey, Calendardate, Sectionid, Sortheading, Millmonth, Value, Workplaceid, Problemcode, Progvalue from pas_central.[dbo].[SICCAPTURE]where millmonth >= '201507'
insert into [SICCLEANED] select * from pas_central.[dbo].[SICCLEANED]where millmonth >= '201507'
insert into [TM1IMPORT] select * from pas_central.[dbo].[TM1IMPORT]
insert into [TOPPANELSSELECTED] select prodmonth, workplaceid, activity, sectionid from pas_central.[dbo].[TOPPANELSSELECTED]where prodmonth >= '201507'
go
insert into [GENERICREPORTANNUAL] select * from pas_central.[dbo].[GENERICREPORTANNUAL]
insert into [GENERICREPORTQUARTER] select * from pas_central.[dbo].[GENERICREPORTQUARTER]
insert into [PRODMONTH] select * from pas_central.[dbo].[PRODMONTH]where prodmonth >= '201507'
