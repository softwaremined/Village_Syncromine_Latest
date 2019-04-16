insert into planmonth
select prodmonth, sectionid, case when p.activity = 9 then 0 else p.activity end ,'N', 'MP', Startdate,p.Workplaceid, 
case when p.activity = 9 then 9 else case when p.activity = 1 then 11 else 1 end end TargetID, 
OrgunitDS, OrgunitAS, OrgunitNS, 
null RomingCrew, 
000 Locked, 
000 Auth, SQMTotal,SQMReef, SQMWaste, FL, 
000 ReefFL,
000 WasteFL,
000 FaceAdvance,
000 IdealSW,SW,CW,GT,
000 GT,Content/1000, 
000 facecmgt,
000 faceKG, Tons, TonsReef, TonsWaste, 
000 Facevalue, 
000 Cubicmetres, Cubics, 
000 CubicsReef,
000 CubicsWaste, 
000 CubicsTons,Null CubicsReefTons,Null CubicsWasteTons,
000 CubicGrams,
000 CubicDepth,null ActualDepth,
000 CubicGT,
000 TrammedTons,
000 TrammedValue,
000 TrammedLevel,Adv, AdvReef, AdvWaste,planprimm DevMain,plansecm DevSec,null DevSecReef,
000 DevCap,
000 LockedDate,
000 LockedBy,
000 DrillRig,
000 StoppedDate, PlanEnddate,
000 IsStopped,
000 TopEnd,
000 AUtoUnPLan,0 LabourStrength,p.OreFlowid,null MOCycle,null VAC,null DC,null MOCycleNum,
0 devFlag , 0 CMKGT,0 UraniumBrokenKg,w.endheight, w.endwidth, w.density, w.reefwaste, 0
from pas_dnk.dbo.planmonth p inner join pas_dnk.dbo.workplace w on w.workplaceid = p.workplaceid 
where prodmonth >= '201507' and startdate is not null

go
insert into planmonth
select prodmonth, sectionid, case when p.activity = 9 then 0 else p.activity end ,'N', 'LP', Startdate,p.Workplaceid, 
case when p.activity = 9 then 9 else case when p.activity = 1 then 11 else 1 end end TargetID, OrgunitDS, OrgunitAS, OrgunitNS, 
null RomingCrew, 
000 Locked, 
000 Auth, SQMTotal,SQMReef, SQMWaste, FL, 
000 ReefFL,
000 WasteFL,
000 FaceAdvance,
000 IdealSW,SW,CW,GT,
000 GT,Content/1000, 
000 facecmgt,
000 faceKG, Tons, TonsReef, TonsWaste, 
000 Facevalue, 
000 Cubicmetres, Cubics, 
000 CubicsReef,
000 CubicsWaste, 
000 CubicsTons,Null CubicsReefTons,Null CubicsWasteTons,
000 CubicGrams,
000 CubicDepth,null ActualDepth,
000 CubicGT,
000 TrammedTons,
000 TrammedValue,
000 TrammedLevel,Adv, AdvReef, AdvWaste,planprimm DevMain,plansecm DevSec,null DevSecReef,
000 DevCap,
000 LockedDate,
000 LockedBy,
000 DrillRig,
000 StoppedDate, PlanEnddate,
000 IsStopped,
000 TopEnd,
000 AUtoUnPLan,0 LabourStrength,p.OreFlowid,null MOCycle,null VAC,null DC,null MOCycleNum,
0 devFlag , 0 CMKGT,0 UraniumBrokenKg,w.endheight, w.endwidth, w.density, w.reefwaste, 0
from pas_dnk.dbo.planmonthlocked p inner join pas_dnk.dbo.workplace w on w.workplaceid = p.workplaceid 
 where prodmonth >= '201507' and startdate is not null
go
update p set locked = '1', auth = '1' from  planmonth p inner join planmonth pm on p.workplaceid = pm.workplaceid and
p.prodmonth = pm.prodmonth and
p.activity = pm.activity and
p.plancode <> pm.plancode 
where p.plancode = 'MP' and pm.plancode = 'LP' 
update pm set locked = '1', auth = '1' from  planmonth p inner join planmonth pm on p.workplaceid = pm.workplaceid and
p.prodmonth = pm.prodmonth and
p.activity = pm.activity and
p.plancode <> pm.plancode 
where p.plancode = 'MP' and pm.plancode = 'LP' 
go
Alter Table Planning Add CycleInput VarChar(10)
GO

alter table planning
add ProblemID varchar(30) null
go
Update Planning set CycleInput = 'CAL'
go
insert into planning
select p.prodmonth, p.sectionid, p.workplaceid, case when p.activity = 9 then 0 else p.activity end , p.calendardate, 'MP', 'N', p.shiftday,
p.sqm, p.sqmreef, p.sqmwaste, p.adv, p.advreef, p.advwaste, p.tons, p.tonsreef, p.tonswaste, p.content,
pm.FL, 
000 ReefFL,
000 WasteFL,
pm.sw, pm.cw,pm.gt, 
000 GT, 
000 cubicmetres, p.cubics, 0,0,0,0,0,0,0,
p.booktons, p.booktonsreef, p.booktonswaste,p.bookgrams, p.bookadv, 0,0,p.booksqm, p.booksqmreef, p.booksqmwaste,
p.bookcubics, 0, 0, 0,0, p.reefwaste, pm.fl, pm.sw, 0, bookcmgt , pm.cw, '', '', 0,0,0,
0,0,0,null,0,null,null,null, null ABSPrec, BookCubics, BookSweeps, BookreSweeps, BookVamps,
PEGID , PegToFace, PegDist, BookOpenUp, BookSecM, BookCode,CheckSqm ,  pb.SBossNotes , pb.CausedLostBlast, 'CAL', p.bookprob
from pas_dnk.dbo.planning p inner join
pas_dnk.dbo.planmonth pm on pm.workplaceid = p.workplaceid and pm.sectionid = p.sectionid and 
pm.prodmonth = p.prodmonth and pm.activity = p.activity 
left outer join pas_dnk.dbo.ProblemBook pb on pb.calendardate = p.calendardate and
pb.workplaceid = p.workplaceid and pb.problemid = p.BookProb and p.sectionid = pb.sectionid where p.prodmonth >= '201507'
go
insert into users values ('Mineware', null, null, null, null, '1', null, null, null, null, null, null, null, null, null, null, null)
