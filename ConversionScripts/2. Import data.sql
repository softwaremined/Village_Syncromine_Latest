insert into hierarch
select hierarchicalid, description from pas_dnk.dbo.hierarch where hierarchicaltype = 'Pro'
go
insert into section
select prodmonth, sectionid, name, reporttosectionid, hierarchicalid, OpsPlanLink, Employeeno  from pas_dnk.dbo.section
where prodmonth in 
('201507', '201508', '201509', '201510', '201511', '201512',
'201601', '201602', '201603', '201603', '201604', '201605', '201606', '201607', '201608', '201609', '201610', '201611', '201612',
'201701', '201702', '201703', '201703', '201704', '201705', '201706', '201707', '201708', '201709', '201710', '201711', '201712')
go

insert into [SECTION_COMPLETE]
select s.prodmonth, s.sectionid, s.name, s1.sectionid, s1.name
, s2.sectionid, s2.name, s3.sectionid, s3.name , s4.sectionid, s4.name
, s5.sectionid, s5.name  
from section s, section s1, section s2, section s3, section s4, section s5 
where s.reporttosectionid = s1.sectionid and s.prodmonth = s1.prodmonth 
and s1.reporttosectionid = s2.sectionid and s1.prodmonth = s2.prodmonth 
and s2.reporttosectionid = s3.sectionid and s2.prodmonth = s3.prodmonth 
and s3.reporttosectionid = s4.sectionid and s3.prodmonth = s4.prodmonth
and s4.reporttosectionid = s5.sectionid and s4.prodmonth = s5.prodmonth

and s.hierarchicalid = 6 
go
insert into [CODE_ACTIVITY] select activity, description from pas_dnk.dbo.[CODE_ACTIVITY] where activity in (0,1,2,8)
insert into [CODE_WPACTIVITY] select * from pas_dnk.dbo.[CODE_ACTIVITY] where activity in (0,1,10,11)
insert into [WPTYPE_SETUPCODES] select * from pas_dnk.dbo.[WPTYPE_SETUPCODES]
insert into [CODE_GRID] select * from pas_dnk.dbo.[CODE_GRID]
insert into CODE_DIRECTION select * from pas_dnk.dbo.CODE_DIRECTION
insert into [WPTYPE_SETUP] select * from pas_dnk.dbo.[WPTYPE_SETUP]
insert into [WORKPLACE_NAME_CHANGE] select * from pas_dnk.dbo.[WORKPLACE_NAME_CHANGE]
insert into [WORKPLACE_INACTIVATION_REASON] select * from pas_dnk.dbo.[WORKPLACE_INACTIVATION_REASON]

go
insert into [REEF] select * from pas_dnk.dbo.[REEF]
insert into [CODE_WPTYPEWPNumberLink] select * from pas_dnk.dbo.[CODE_WPTYPEWPNumberLink]
insert into [CODE_WPTYPEWPDirectionLink] select * from pas_dnk.dbo.[CODE_WPTYPEWPDirectionLink]
insert into [CODE_WPTYPEWPDetailLink] select * from pas_dnk.dbo.[CODE_WPTYPEWPDetailLink]
insert into [CODE_WPTYPEWPDescNoLink] select * from pas_dnk.dbo.[CODE_WPTYPEWPDescNoLink]
insert into [CODE_WPTYPEWPDescLink] select * from pas_dnk.dbo.[CODE_WPTYPEWPDescLink]
insert into [CODE_WPTypeLevelLink] select * from pas_dnk.dbo.[CODE_WPTypeLevelLink]
insert into [CODE_WPTypeGridLink] select * from pas_dnk.dbo.[CODE_WPTypeGridLink]
insert into [Code_WPTypeClassifyChange] select * from pas_dnk.dbo.[Code_WPTypeClassifyChange]
insert into [CODE_WPTYPE] select * from pas_dnk.dbo.[CODE_WPTYPE]
go
insert into [CODE_WPSTATUS] select * from pas_dnk.dbo.[CODE_WPSTATUS]
insert into [CODE_WPNUMBER] select * from pas_dnk.dbo.[CODE_WPNUMBER]
insert into [CODE_WPDIVISION] select * from pas_dnk.dbo.[CODE_WPDIVISION]
insert into [CODE_WPDETAIL] select * from pas_dnk.dbo.[CODE_WPDETAIL]
insert into [CODE_WPDESCRIPTIONNO] select * from pas_dnk.dbo.[CODE_WPDESCRIPTIONNO]
insert into [CODE_WPDESCRIPTION] select * from pas_dnk.dbo.[CODE_WPDESCRIPTION]
insert into [CODE_WPCLASSIFICATION] select * from pas_dnk.dbo.[CODE_WPCLASSIFICATION]
insert into [STOPINGPROCESSCODES] select name from pas_dnk.dbo.[STOPINGPROCESSCODES]
insert into [ENDTYPE] select * from pas_dnk.dbo.[ENDTYPE]
insert into [CODE_OREFLOW] select * from pas_dnk.dbo.[CODE_OREFLOW]
insert into [OREFLOWENTITIES] select * from pas_dnk.dbo.[OREFLOWENTITIES]
insert into [COMMONAREAS] select name from pas_dnk.dbo.[COMMONAREAS]
insert into [COMMONAREASUBSECTIONS] select commonarea,name from pas_dnk.dbo.[COMMONAREASUBSECTIONS]
go

insert into [WORKPLACE] select *, null from pas_dnk.dbo.[WORKPLACE]
go
insert into [WORKPLACE_CLASSIFICATION] select * from pas_dnk.dbo.[WORKPLACE_CLASSIFICATION]
insert into [WORKPLACE_INACTIVATION] select * from pas_dnk.dbo.[WORKPLACE_INACTIVATION]
go
insert into code_calendar select distinct c.calendarcode, description from pas_dnk.dbo.code_calendar c inner join pas_dnk.dbo.seccal s on s.calendarcode = c.calendarcode where s.prodmonth >= '201507' 
insert into code_calendar select distinct c.calendarcode, description from pas_dnk.dbo.code_calendar c where description <> 'section calendar'
insert into seccal select * from pas_dnk.dbo.seccal where prodmonth >= '201507'
go
INSERT INTO [SAMPLING]
select * from pas_dnk.dbo.SAMPLING where calendardate>= '20150615'
update SAMPLING set RIF = 0
GO
go

INSERT INTO [CALTYPE] ([CalendarCode], [CalendarDate], [Workingday])
Select * from pas_dnk.dbo.CALTYPE where CalendarDate >= (select min(begindate) from pas_dnk.dbo.seccal where prodmonth >= '201507')
GO

INSERT INTO [CALENDARMILL]
SELECT * FROM pas_dnk.dbo.CALENDARMILL  where MillMonth >= '201507'
go
INSERT INTO CALENDAROTHER
SELECT * FROM pas_dnk.dbo.CALENDARSAFETY where SafetyMonth >= '201507'
go
INSERT INTO CALENDAROTHER
SELECT * FROM pas_dnk.dbo.CALENDARcost where costMonth >= '201507'
go
insert into sysset  select * from pas_dnk.dbo.sysset
go

alter table sysset
add AllowSWCWBook varchar(1) null,
SWCheck int null,
CWCheck int null,
ProblemNew varchar(1) null,
Problem_ProblemTypeLink varchar(1) null,
ProblemGroup_ProblemTypeLink varchar(1) null,
ProblemForceNote varchar(1) null,
ProblemNewValidation varchar(1) null,
BookFL varchar(1) null,
DisableBookingCyclePlan varchar(1) null,
SamplingValue varchar(1) null,
SamplingUseLatestForPlan varchar(1) null,
MaxAdvDev numeric (13,3) null,
FinYearStart int null,
FinYearEnd int null,
EngCaptLevel int null,
EngBackDateDays int null,
B1_Color varchar(50) null,
B2_Color varchar(50) null,
B3_Color varchar(50) null
go

update sysset
set A_Color='-4128832',
 B_Color='-16192',
 S_Color='-64'
go
insert into CODE_PROBLEM_TYPE values ('1', 0, 'Safety & Env', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('2', 0, 'Support', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('3', 0, 'Mining', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('4', 0, 'Blasting', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('5', 0, 'Cleaning', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('6', 0, 'Logistics', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('7', 0, 'Labour', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('8', 0, 'Equip.Fail', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('9', 0, 'Materials', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('10', 0, 'Geology', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('11', 0, 'Services', 0, 'N')
insert into CODE_PROBLEM_TYPE values ('12', 0, 'Trackless', 0, 'N')
go
insert into CODE_PROBLEM_TYPE select ProblemTypeid, 1, description, color, deleted from code_problem_type
insert into [CODE_PROBLEM]
select ProblemID, Description, 0, 0, 'Y', 1,'Gainsboro', 'N' from pas_dnk.dbo.problem
go
insert into [CODE_PROBLEM]
select problemid, description, 1, 0, 'Y', 1, 'Gainsboro', 'N' from [CODE_PROBLEM]
INSERT INTO [PROBLEM_TYPE]
select LEFT(Problemgroupcode, CHARINDEX('.', Problemgroupcode) - 1) ,  problemid,0, 'N'  from pas_dnk.dbo.problem
INSERT INTO [PROBLEM_TYPE]
select LEFT(Problemgroupcode, CHARINDEX('.', Problemgroupcode) - 1) ,  problemid,1, 'N'  from pas_dnk.dbo.problem

INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) Code, HQCat, 0,0,  'N', null, null   from pas_dnk.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) + '1' Code, HQCat, 0,0,  'N', null, null   from pas_dnk.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
GO
INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) Code, HQCat, 1,0,  'N', null, null   from pas_dnk.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [CODE_PROBLEM_NOTE]
select distinct left(upper(HQCat),3) + '1' Code, HQCat, 1,0,  'N', null, null   from pas_dnk.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')


INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),0,'N'  from pas_dnk.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),0,'N'  from pas_dnk.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),1,'N'  from pas_dnk.dbo.problem where HQCat not in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')
INSERT INTO [PROBLEM_NOTE]
select distinct problemid,left(upper(HQCat),3),1,'N'  from pas_dnk.dbo.problem where HQCat in 
('Ground conditions (safety reasons)', 'Lack of supply (timber,tires)', 'Other safety related issues')

go
insert into crew
select distinct c.calendardate, department,null,null, null, null from pas_dnk.dbo.caltype c , pas_dnk.dbo.vw_orgunits o 
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

insert into [BOOKINGHoisting] select * from pas_dnk.[dbo].[BOOKINGHoisting] where millmonth >= '201507'
insert into [BOOKINGMilling] select * from pas_dnk.[dbo].[BOOKINGMilling] where millmonth >= '201507'
insert into [BOOKINGTRAMMING] select * from pas_dnk.[dbo].[BOOKINGTRAMMING] where millmonth >= '201507'
insert into [BookingTrammingWP] select * from pas_dnk.[dbo].[BookingTrammingWP] where millmonth >= '201507'
insert into [BookingTrammingWP] select * from pas_dnk.[dbo].[BUS_PLANNING_STORAGES] where prodmonth >= '201507'
insert into [KRIGING] select *, null, null from pas_dnk.[dbo].[KRIGING]where prodmonth >= '201507'
update kriging set activity = 0 where activity = 9
insert into [PLANNING_STORAGES] select * from pas_dnk.[dbo].[PLANNING_STORAGES]where prodmonth >= '201507'
insert into [SICCAPTURE] select SICKey, Calendardate, Sectionid, Sortheading, Millmonth, Value, Workplaceid, Problemcode, Progvalue, null, null from pas_dnk.[dbo].[SICCAPTURE]where millmonth >= '201507'
insert into [SURVEY] select * from pas_dnk.[dbo].[SURVEY] where prodmonth >= '201507'
insert into [SICCLEANED] select * from pas_dnk.[dbo].[SICCLEANED]where millmonth >= '201507'
insert into [TM1IMPORT] select * from pas_dnk.[dbo].[TM1IMPORT]
insert into [TOPPANELSSELECTED] select prodmonth, workplaceid, activity, sectionid from pas_dnk.[dbo].[TOPPANELSSELECTED]where prodmonth >= '201507'
update [TOPPANELSSELECTED] set activity = 0 where activity = 9

go
insert into [GENERICREPORTANNUAL] select * from pas_dnk.[dbo].[GENERICREPORTANNUAL]
insert into [GENERICREPORTQUARTER] select * from pas_dnk.[dbo].[GENERICREPORTQUARTER]
insert into [PRODMONTH] select * from pas_dnk.[dbo].[PRODMONTH]where prodmonth >= '201507'
insert into [PEG] select *, null from pas_dnk.[dbo].[PEG]
go
INSERT INTO BUSINESSPLAN_LOCKS
SELECT * FROM PAS_Dnk.dbo.[BusinessPlan_Locks] where ProdMonthStart >= '201507'
GO
INSERT INTO [Survey_Locks]
SELECT * FROM PAS_Dnk.dbo.[Survey_Locks] where prodmonth >= '201507'
GO
insert into PlanHoist
select * from PAS_Dnk.dbo.PlanHoist where millmonth >= '201507'
go
insert into [BOOKINGHoist]
select * from PAS_Dnk.dbo.[BOOKINGHoist] where millmonth >= '201507'
GO

Insert into BOOKINGMilling
Select * from PAS_Dnk.dbo.BOOKINGMilling where millmonth >= '201507'
GO
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('BC','Blast Cubby','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('BEC','Blast end & Cubby','1')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('BF','Backfill','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('BL','Blast','1')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('BRL','Breaker Line','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('BV','Blast & Install Ventilation','1')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('C','Cubby','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('CL','Clean','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('CM','Cycle Mining','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('CON','Construction','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('CR','Crew Move','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('EQ','Equipping','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('P','Install Pipes','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('RP','Rails and Pipes','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('SC','Support Cubby','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('SL','Sliping','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('SS','Schedule Stop','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('SU','Support','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('SUBL','Support and Blast','1')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('SWP','Sweep','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('TR','TRAINING','0')
INSERT INTO CODE_Cycle([CycleCode],[Description],[CanBlast])VALUES('W','Winch or WatJet Move','0')
go
insert into  [CODE_SECURITYTYPE]
select '1','Approval'
GO

insert into  [CODE_SECURITYTYPE]
select '2','Notify'
GO

Set Identity_Insert PlanProt_FieldTypes ON
GO

insert into PlanProt_FieldTypes (FieldTypeID, FieldDescription)
Select 1,'Grouping' Union
Select 2,'Number' Union
Select 3,'Real' Union
Select 4,'Date' Union
Select 5,'Alpha' Union
Select 6,'Selection' 
go

Set Identity_Insert PlanProt_FieldTypes ON
GO

Insert into [Code_StopeTypes]
select StopeTypeDesc from PAS_Dnk.dbo.Code_StopeTypes
GO


insert into CODE_BOOKING
values (0,'BL','Blast')
go

insert into CODE_BOOKING
values (0,'DP','Dual Panel')
go

insert into CODE_BOOKING
values (0,'NPB','Planned not Blasted')
go

insert into CODE_BOOKING
values (0,'PR','Problem')
go

insert into CODE_BOOKING
values (0,'ST','Stopped')
go


insert into CODE_BOOKING
values (1,'BL','Blast')
go

insert into CODE_BOOKING
values (1,'DP','Dual Panel')
go

insert into CODE_BOOKING
values (1,'NP','Not In Position')
go

insert into CODE_BOOKING
values (1,'PR','Problem')
go

insert into CODE_BOOKING
values (1,'ST','Stopped')
go


insert into code_siccapture
select kpi,description from pas_dnk.[dbo].[Code_SICCapture]
go
declare @WorkplaceID varchar(10)
declare @Value numeric(18,2)


DECLARE AA_Cursor CURSOR FOR
select WorkplaceID, max(Value) Value from Peg
group by WorkplaceID

OPEN AA_Cursor;
FETCH NEXT FROM AA_Cursor
into @WorkplaceID, @Value;

WHILE @@FETCH_STATUS = 0
BEGIN
    update Peg
	set CalendarDate ='2011-01-01'
	where WorkplaceID = @WorkplaceID and
	        Value = @Value

    FETCH NEXT FROM AA_Cursor
    Into @WorkplaceID, @Value;
END; 
CLOSE AA_Cursor;
DEALLOCATE AA_Cursor;
go

update peg
set calendardate ='2010-12-31'
where CalendarDate is null
go

insert into code_cleantypes
select [CleanTypeDesc],[SurvRep]  from pas_dnk.dbo.code_cleantypes
insert into [CODE_CUBICTYPES]
select [CubicTypeDesc],[Activity]  from pas_dnk.dbo.[CODE_CUBICTYPES]
insert into [CODE_INDICATORS]
select [IndicatorDesc] from pas_dnk.dbo.[CODE_INDICATORS]