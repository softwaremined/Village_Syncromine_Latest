

ALTER TABLE users
DROP CONSTRAINT DF_USERS_RemActTrack
go

ALTER TABLE users
DROP CONSTRAINT DF_USERS_RemOverdue
go
ALTER procedure [dbo].[sp_SICReport_Total]  --'MINEWARE', '2016-07-01', 'F', '4' 
--declare
@UserID varchar(10),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int,
@MOName varchar(50)
--set @UserID ='MINEWARE'
--set @CalendarDate = '2017-01-11'
--set @SectionID = 'GM'
--set @Section ='1'
--set @MOName ='S Mofokeng'
as

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)
declare @TheQuery3 varchar(8000)
declare @TheQuery4 varchar(8000)
declare @TheQuery5 varchar(8000)
declare @TheQuery6 varchar(8000)
declare @TheQuery7 varchar(8000)
declare @TheStartDate varchar(10)
declare @TheEndDate varchar(10)

select @TheStartDate = case when  convert(varchar(10),min(prevstartdate),120) is null then 
convert(varchar(10),min(startdate),120) else  convert(varchar(10),min(prevstartdate),120) end,
 @TheEndDate = convert(varchar(10),max(enddate),120)
from temp_sectionstartdate
where UserID = @UserID and
StartDate <= @CalendarDate and
EndDate >= @CalendarDate
--SELECT @TheStartDate
if (@TheStartDate is NULL)
BEGIN
	Select '' TheSection1, '' TheSection, 
'' workplaceid,
Dev_Check = Convert(decimal(10,1), 0),
Stp_Check = Convert(decimal(10,1), 0),
--Nr of Panels   
[Stp_PD_NoPanels] = Convert(Numeric(10,0), 0),  
[Stp_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_PP_NoPanels] = Convert(Numeric(10,0), 0),
[Stp_BP_NoPanels] = Convert(Numeric(10,0), 0),
--Nr of Dev Panels 
[Dev_PD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_NoPanels] = Convert(Numeric(10,0), 0),  
[Dev_BP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_NoPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_NoPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Stope Panels
[Stp_BD_ClnPanels] = Convert(Numeric(10,0), 0),
--No of Cleaned Dev Panels
[Dev_PD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VD_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_PP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_BP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_VP_ClnPanels] = Convert(Numeric(10,0), 0),
[Dev_FP_ClnPanels] = Convert(Numeric(10,0), 0),
--Shiftnr   
TheField1 = '''', 
ShiftNo =  Convert(Numeric(10,0), 0), 
TotalShft = Convert(Numeric(10,0), 0),
--M2 Mined
Stp_Plan_Sqm = Convert(Numeric(10,0), 0),
Stp_PD_Sqm = Convert(Numeric(10,0), 0),
Stp_BD_Sqm = Convert(Numeric(10,0), 0),
Stp_VD_Sqm = Convert(Numeric(10,0), 0),
Stp_PP_Sqm = Convert(Numeric(10,0), 0),
Stp_BP_Sqm = Convert(Numeric(10,0), 0),  
Stp_VP_Sqm = Convert(Numeric(10,0), 0),
Stp_FP_Sqm = Convert(Numeric(10,0), 0),
Stp_NewDay_Sqm = Convert(Numeric(10,0), 0),
Stp_Prev_Sqm = Convert(Numeric(10,0), 0),
Stp_PrevVar_Sqm = Convert(Numeric(10,0), 0),
Stp_DPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_PPerc_Sqm = Convert(Numeric(10,2), 0),
Stp_FPSqm = Convert(Numeric(10,0), 0),	                                                                                                                         
--High Grade m2 Mined  
Stp_PD_HgSqm = Convert(Numeric(10,0), 0),
Stp_BD_HgSqm = Convert(Numeric(10,0), 0),
Stp_VD_HgSqm = Convert(Numeric(10,0), 0),
Stp_PP_HgSqm = Convert(Numeric(10,0), 0),
Stp_BP_HgSqm = Convert(Numeric(10,0), 0),  
Stp_VP_HgSqm = Convert(Numeric(10,0), 0),
Stp_FP_HgSqm = Convert(Numeric(10,0), 0),
Stp_NewDay_HgSqm = Convert(Numeric(10,0), 0),     
--Total Dev m2 Mined  
[Dev_PD_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BD_Sqm] = Convert(Numeric(10,0), 0),  
[Dev_PP_Sqm] = Convert(Numeric(10,0), 0),
[Dev_BP_Sqm] =Convert(Numeric(10,0), 0),
--Total FL                               
[Stp_PD_FL] = Convert(Numeric(10,0), 0),
[Stp_BD_FL] = Convert(Numeric(10,0), 0),
[Stp_VD_FL] = Convert(Numeric(10,0), 0),
[Stp_PP_FL] =Convert(Numeric(10,0), 0),
[Stp_BP_FL] = Convert(Numeric(10,0), 0),
[Stp_VP_FL] = Convert(Numeric(10,0), 0),
[Stp_FP_FL] = Convert(Numeric(10,0), 0),

[Stp_PD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_BD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VD_FLNS] = Convert(Numeric(10,0), 0),
[Stp_PP_FLNS] =Convert(Numeric(10,0), 0),
[Stp_BP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_VP_FLNS] = Convert(Numeric(10,0), 0),
[Stp_FP_FLNS] = Convert(Numeric(10,0), 0),
	
[Stp_PD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BD_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VD_CleanFL] =Convert(Numeric(10,0), 0),
[Stp_PP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_BP_CleanFL] = Convert(Numeric(10,0), 0),
[Stp_VP_CleanFL] = Convert(Numeric(10,0), 0), 
[Stp_FP_CleanFL] = Convert(Numeric(10,0), 0),
--Total Dev FL   
[Dev_PD_FL] = Convert(Numeric(10,0), 0),
[Dev_BD_FL] = Convert(Numeric(10,0), 0),
[Dev_VD_FL] = Convert(Numeric(10,0), 0),             
[Dev_PP_FL] = Convert(Numeric(10,0), 0),
[Dev_BP_FL] = Convert(Numeric(10,0), 0),
[Dev_VP_FL] = Convert(Numeric(10,0), 0),
 
--Total Reef Metres  
Dev_Plan_Adv = Convert(Numeric(10,2), 0),
Dev_PD_RAdv = Convert(Numeric(10,2), 0),
Dev_BD_RAdv = Convert(Numeric(10,2), 0),
Dev_VD_RAdv = Convert(Numeric(10,2), 0),
Dev_PP_RAdv = Convert(Numeric(10,2), 0),
Dev_BP_RAdv = Convert(Numeric(10,2), 0),
Dev_VP_RAdv = Convert(Numeric(10,2), 0),
Dev_Prev_RAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_RAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_RAdv = Convert(Numeric(10,2), 0),
Dev_FP_RAdv = Convert(Numeric(10,2), 0),
		
--Total Waste Metres                                   
Dev_PD_WAdv = Convert(Numeric(10,2), 0),
Dev_BD_WAdv = Convert(Numeric(10,2), 0),
Dev_VD_WAdv = Convert(Numeric(10,2), 0),
Dev_PP_WAdv = Convert(Numeric(10,2), 0),
Dev_BP_WAdv = Convert(Numeric(10,2), 0),
Dev_VP_WAdv = Convert(Numeric(10,2), 0),
Dev_Prev_WAdv = Convert(Numeric(10,2), 0),
Dev_PrevVar_WAdv = Convert(Numeric(10,2), 0),
Dev_DPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_PPerc_WAdv = Convert(Numeric(10,2), 0),
Dev_FP_WAdv = Convert(Numeric(10,2), 0),	

--Total Metres                                                  
Dev_PD_Adv = Convert(Numeric(10,2), 0),
Dev_BD_Adv = Convert(Numeric(10,2), 0),
Dev_VD_Adv = Convert(Numeric(10,2), 0),
Dev_PP_Adv = Convert(Numeric(10,2), 0),
Dev_BP_Adv = Convert(Numeric(10,2), 0),
Dev_VP_Adv = Convert(Numeric(10,2), 0),
Dev_Prev_Adv = Convert(Numeric(10,2), 0),
Dev_PrevVar_Adv = Convert(Numeric(10,2), 0),
Dev_DPerc_Adv = Convert(Numeric(10,2), 0),
Dev_PPerc_Adv = Convert(Numeric(10,2), 0),	
Dev_FP_Adv = Convert(Numeric(10,2), 0),

--High Grade Reef Metres
Dev_PD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VD_HgRAdv = Convert(Numeric(10,2), 0),
Dev_PP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_BP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_VP_HgRAdv = Convert(Numeric(10,2), 0),
Dev_FP_HgRAdv = Convert(Numeric(10,2), 0),

--tons                          
Dev_PD_RTons = Convert(Numeric(10,2), 0),
Dev_BD_RTons = Convert(Numeric(10,2), 0),
Dev_VD_RTons = Convert(Numeric(10,2), 0),
Dev_PP_RTons = Convert(Numeric(10,2), 0),
Dev_BP_RTons = Convert(Numeric(10,2), 0),
Dev_VP_RTons = Convert(Numeric(10,2), 0),
Dev_FP_RTons = Convert(Numeric(10,2), 0),
Dev_PD_WTons =Convert(Numeric(10,2), 0),
Dev_BD_WTons = Convert(Numeric(10,2), 0),
Dev_VD_WTons = Convert(Numeric(10,2), 0),
Dev_PP_WTons = Convert(Numeric(10,2), 0),
Dev_BP_WTons = Convert(Numeric(10,2), 0),
Dev_VP_WTons = Convert(Numeric(10,2), 0),
Dev_FP_WTons = Convert(Numeric(10,2), 0),

Da_StopeTons = Convert(Numeric(10,2), 0),
DaB_StopeTons = Convert(Numeric(10,2), 0),
DaV_StopeTons = Convert(Numeric(10,2), 0),   
D_StopeTons = Convert(Numeric(10,2), 0),           
B_StopeTons = Convert(Numeric(10,2), 0),
V_StopeTons = Convert(Numeric(10,2), 0),       
F_StopeTons = Convert(Numeric(10,2), 0),

--SQM Lost
Stp_BD_Mis = Convert(Numeric(10,1), 0),
Stp_BD_Labour = Convert(Numeric(10,1), 0),
Stp_BD_Other = Convert(Numeric(10,1), 0),
Stp_BP_Mis = Convert(Numeric(10,1), 0),
Stp_BP_Labour = Convert(Numeric(10,1), 0),
Stp_BP_Other = Convert(Numeric(10,1), 0),
TheField20 = '''',
TheField21 = '''',TheField22 = '''',    
TheField23 = '''', TheField24 = '''', TheField25 = '''',TheField26 = '''',TheField27 = '''',    
TheField28 = '''',

--Sweepings       
Stp_PP_Sweeps = Convert(Numeric(10,0), 0),

--Reef SW Excl Gullies
Stp_PP_ReefSW = Convert(Numeric(10,1), 0),
Stp_BP_ReefSW = Convert(Numeric(10,1), 0),
Stp_VP_ReefSW = Convert(Numeric(10,1), 0),
Stp_FP_ReefSW = Convert(Numeric(10,1), 0),

--Total cmg/t                         
[Stp_PP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_BP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_VP_Sqmcmgt] = Convert(Numeric(10,0), 0),
[Stp_FP_Sqmcmgt] = Convert(Numeric(10,0), 0),
 
 --Content   
[Stp_PP_Content] = Convert(Numeric(10,4), 0),                                    
[Stp_BP_Content] = Convert(Numeric(10,4), 0),    
[Stp_VP_Content] = Convert(Numeric(10,4), 0),    
[Stp_PD_Content] = Convert(Numeric(10,4), 0),    
[Stp_BD_Content] =Convert(Numeric(10,4), 0),    
[Stp_VD_Content] = Convert(Numeric(10,4), 0),    
[Stp_FP_Content] = Convert(Numeric(10,4), 0)

END
ELSE
BEGIN
   
set @TheQuery = 'select ' 
IF (@Section = 1)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_5 +'':''+sc.Name_5,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 2)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_4 +'':''+sc.Name_4,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 3)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_3 +'':''+sc.Name_3,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END
IF (@Section = 4)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								 isnull(sc.SectionID_1 +'':''+sc.Name_1,'''') TheSection, '
END 
set @TheQuery = @TheQuery + ' 
isnull(workplaceid,'''') workplaceid,
Dev_Check = Convert(decimal(10,1),sum(isnull(DevCheck,0))),
Stp_Check = Convert(decimal(10,1),sum(isnull(StopeCheck,0))),
--Nr of Stope Blasted Panels   
[Stp_PD_NoPanels] = sum(Da_StopeTrue),  
[Stp_BD_NoPanels] = sum(DaB_StopeTrue), 
[Stp_PP_NoPanels] = sum(D_StopeTrue), 
[Stp_BP_NoPanels] = sum(B_StopeTrue),
--Nr of Dev Blasted Panels 
[Dev_PD_NoPanels] = sum(Da_DevTrue), 
[Dev_BD_NoPanels] = sum(DaB_DevTrue), 
[Dev_VD_NoPanels] = sum(DaB_DevTrue) - sum(Da_DevTrue), 
[Dev_PP_NoPanels] = sum(D_DevTrue),    
[Dev_BP_NoPanels] = sum(B_DevTrue),
[Dev_VP_NoPanels] = sum(B_DevTrue) - sum(D_DevTrue),
[Dev_FP_NoPanels] = sum(B_DevTrue) + sum(F_DevTrue),
--No of Cleaned Stope Panels
[Stp_BD_ClnPanels] = sum(DaB_CleanTrue),
--No of Cleaned Dev Panels
[Dev_PD_ClnPanels] = sum(Da_DevCleanTrue),
[Dev_BD_ClnPanels] = sum(DaB_DevCleanTrue),
[Dev_VD_ClnPanels] = sum(DaB_DevCleanTrue) - sum(Da_DevCleanTrue),
[Dev_PP_ClnPanels] = sum(D_DevCleanTrue),
[Dev_BP_ClnPanels] = sum(B_DevCleanTrue),
[Dev_VP_ClnPanels] = sum(B_DevCleanTrue) - sum(D_DevCleanTrue),
[Dev_FP_ClnPanels] = sum(B_DevCleanTrue) + sum(F_DevCleanTrue),

--Shiftnr   

TheField1 = '''', 
ShiftNo =  max(isnull(shiftnr,0)),   
TotalShft = max(isnull(Totalnumdays,0)),
--M2 Mined
Stp_Plan_Sqm = Convert(Numeric(10,0),sum(DM_m2)),
Stp_PD_Sqm = Convert(Numeric(10,0),sum(Da_m2)),  
Stp_BD_Sqm = Convert(Numeric(10,0),SUM(DaB_Sqm)),
Stp_VD_Sqm = Convert(Numeric(10,0),SUM(DaB_Sqm) - sum(Da_m2)),
Stp_PP_Sqm = Convert(Numeric(10,0),sum(D_m2)),
Stp_BP_Sqm = Convert(Numeric(10,0),sum(B_Sqm)),  
Stp_VP_Sqm = Convert(Numeric(10,0),sum(B_Sqm) - sum(D_m2)),
Stp_FP_Sqm = Convert(Numeric(10,0),sum(B_Sqm) + sum(F_m2)),
Stp_NewDay_Sqm = Convert(Numeric(10,0),case when (max(totalnumdays)- max(Shiftnr)) = 0 then 0 
				else (sum(DM_m2) - sum(B_Sqm))/(max(totalnumdays)- max(Shiftnr)) end), 
Stp_Prev_Sqm = Convert(Numeric(10,0),sum(BP_Sqm)),
Stp_PrevVar_Sqm = Convert(Numeric(10,0),sum(B_Sqm) - sum(BP_Sqm)), 
Stp_DPerc_Sqm = case when sum(isnull(Da_m2,0)) > 0 then
		Convert(Numeric(10,2),sum(DaB_Sqm)/sum(isnull(Da_m2,0)) * 100 ) else 0 end,
Stp_PPerc_Sqm = case when sum(isnull(D_m2,0)) > 0 then
		Convert(Numeric(10,2),sum(B_Sqm)/sum(isnull(D_m2,0)) * 100 ) else 0 end,
Stp_FPSqm = Convert(Numeric(10,0),sum(B_Sqm) + sum(F_m2)), 			                                                                                                                         

--High Grade m2 Mined  
Stp_PD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(Da_tm21)) else Convert(Numeric(10,0),sum(Da_tm2)) end, 
Stp_BD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(DaB_tm21)) else Convert(Numeric(10,0),sum(DaB_tm2)) end, 
Stp_VD_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(DaB_tm21) - sum(Da_tm21)) else Convert(Numeric(10,0),sum(DaB_tm2) - sum(Da_tm2)) end, 
Stp_PP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(D_tm21)) else Convert(Numeric(10,0),sum(D_tm2)) end,
Stp_BP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(B_tm21)) else Convert(Numeric(10,0),sum(B_tm2)) end,   
Stp_VP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(B_tm21) - sum(D_tm21)) else Convert(Numeric(10,0),sum(B_tm2) - sum(D_tm2)) end, 
Stp_FP_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(F_Tm21)) else Convert(Numeric(10,0),sum(F_Tm2)) end, 
--Stp_NewDay_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
--					else (sum(DM_tm21) - sum(B_tm21))/(max(totalnumdays) - max(Shiftnr)) end) else 0 end, 
Stp_NewDay_HgSqm = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),
						case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
							 else (sum(DM_tm21) - sum(B_tm21))/(max(totalnumdays) - max(Shiftnr)) end) 
					    else Convert(Numeric(10,0),case when (max(totalnumdays) - max(Shiftnr)) = 0 then 0 
				   else (sum(DM_tm2) - sum(B_tm2))/(max(totalnumdays) - max(Shiftnr)) end) end, 
     
--Total Dev m2 Mined  
[Dev_PD_Sqm] = Convert(Numeric(10,0),sum(Da_Devm2)),  
[Dev_BD_Sqm] = Convert(Numeric(10,0),sum(DaB_Devm2)),     
[Dev_PP_Sqm] = Convert(Numeric(10,0),sum(D_Devm2)),
[Dev_BP_Sqm] = Convert(Numeric(10,0),sum(B_Devm2)),   
--Total FL                               
[Stp_PD_FL] = Convert(Numeric(10,0),isnull(sum(Da_FL),0)),
[Stp_BD_FL] = Convert(Numeric(10,0),isnull(sum(DaB_FL),0)),
[Stp_VD_FL] = Convert(Numeric(10,0),isnull(sum(DaB_FL),0) - isnull(sum(Da_FL),0)),
[Stp_PP_FL] = Convert(Numeric(10,0),isnull(sum(D_FL),0)),
[Stp_BP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0)),
[Stp_VP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0) - isnull(sum(D_FL),0)),
[Stp_FP_FL] = Convert(Numeric(10,0),isnull(sum(B_FL),0) + isnull(sum(F_FL),0)),

[Stp_PD_FLNS] = Convert(Numeric(10,0),isnull(sum(Da_FLNS),0)),
[Stp_BD_FLNS] = Convert(Numeric(10,0),isnull(sum(DaB_FLNS),0)),
[Stp_VD_FLNS] = Convert(Numeric(10,0),isnull(sum(DaB_FLNS),0) - isnull(sum(Da_FLNS),0)),
[Stp_PP_FLNS] = Convert(Numeric(10,0),isnull(sum(D_FLNS),0)),
[Stp_BP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0)),
[Stp_VP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0) - isnull(sum(D_FLNS),0)),
[Stp_FP_FLNS] = Convert(Numeric(10,0),isnull(sum(B_FLNS),0) + isnull(sum(F_FLNS),0)),
	
[Stp_PD_CleanFL] = Convert(Numeric(10,0),sum(isnull(Da_CleanFL,0))), 
[Stp_BD_CleanFL] = Convert(Numeric(10,0),sum(isnull(DaB_CleanFL,0))), 
[Stp_VD_CleanFL] = Convert(Numeric(10,0),sum(isnull(DaB_CleanFL,0)) - sum(isnull(Da_CleanFL,0))),  
[Stp_PP_CleanFL] = Convert(Numeric(10,0),sum(isnull(D_CleanFL,0))),
[Stp_BP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0))), 
[Stp_VP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0)) - sum(isnull(D_CleanFL,0))),  
[Stp_FP_CleanFL] = Convert(Numeric(10,0),sum(isnull(B_CleanFL,0)) + sum(isnull(F_CleanFL,0))),  
--Total Dev FL   
[Dev_PD_FL] = case when sum(Da_DevWAdv) + sum(Da_DevRAdv) > 0 then      
  Convert(Numeric(10,0),sum(Da_Devm2) / (sum(Da_DevWAdv) + sum(Da_DevRAdv))) else 0 end,  
[Dev_BD_FL] = case when sum(DaB_DevRAdv) > 0 then     
  Convert(Numeric(10,0),sum(DaB_Devm2) / sum(DaB_DevRAdv)) else 0 end,    
[Dev_VD_FL] = (case when sum(DaB_DevWAdv) + sum(DaB_DevRAdv) > 0 then     
   Convert(Numeric(10,0),sum(DaB_Devm2) / (sum(DaB_DevWAdv) + sum(DaB_DevRAdv))) else 0 end) - 
   (case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then
  Convert(Numeric(10,0),sum(D_Devm2) / sum(D_DevWAdv + D_DevRAdv)) else 0 end),                             
[Dev_PP_FL] = case when sum(D_DevWAdv + D_DevRAdv) > 0 then   
  Convert(Numeric(10,0),sum(D_Devm2) / (sum(D_DevWAdv) + sum(D_DevRAdv))) else 0 end, 
[Dev_BP_FL] = case when sum(B_DevRAdv) > 0 then    
   Convert(Numeric(10,0),sum(B_Devm2) / sum(B_DevRAdv)) else 0 end, 
[Dev_VP_FL] = (case when sum(B_DevWAdv) + sum(B_DevRAdv) > 0 then          
   Convert(Numeric(10,0),sum(B_Devm2) / (sum(B_DevWAdv) + sum(B_DevRAdv))) else 0 end) - 
   (case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then 
   Convert(Numeric(10,0),sum(D_Devm2) / (sum(D_DevWAdv) + sum(D_DevRAdv))) else 0 end),'
set @TheQuery1 = '   
--Total Reef Metres  
Dev_Plan_Adv = Convert(Numeric(10,2),sum(isnull(DM_DevAdv,0))), 
Dev_PD_RAdv = Convert(Numeric(10,2),sum(Da_DevRAdv)),
Dev_BD_RAdv = Convert(Numeric(10,2),sum(DaB_DevRAdv)), 
Dev_VD_RAdv = Convert(Numeric(10,2),sum(DaB_DevRAdv) - sum(Da_DevRAdv)),  
Dev_PP_RAdv = Convert(Numeric(10,2),sum(D_DevRAdv)),
Dev_BP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv)),
Dev_VP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) - sum(D_DevRAdv)), 
Dev_Prev_RAdv = Convert(Numeric(10,2),sum(BP_DevRAdv)),
Dev_PrevVar_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) - sum(BP_DevRAdv)), 
Dev_DPerc_RAdv = case when sum(Da_DevRAdv) > 0 then
		Convert(Numeric(10,2),(sum(DaB_DevRAdv)/sum(Da_DevRAdv)*100)) else 0 end, 
Dev_PPerc_RAdv = case when sum(D_DevRAdv) > 0 then
		Convert(Numeric(10,2),(sum(B_DevRAdv)/sum(D_DevRAdv)*100)) else 0 end,
Dev_FP_RAdv = Convert(Numeric(10,2),sum(B_DevRAdv) + sum(F_DevRAdv)),  
		
--Total Waste Metres                                   
Dev_PD_WAdv = Convert(Numeric(10,2),sum(Da_DevWAdv)), 
Dev_BD_WAdv = Convert(Numeric(10,2),sum(DaB_DevWAdv)), 
Dev_VD_WAdv = Convert(Numeric(10,2),sum(DaB_DevWAdv)- sum(Da_DevWAdv)),
Dev_PP_WAdv = Convert(Numeric(10,2),sum(D_DevWAdv)),
Dev_BP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv)),
Dev_VP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) - sum(D_DevWAdv)), 
Dev_Prev_WAdv = Convert(Numeric(10,2),sum(BP_DevWAdv)),
Dev_PrevVar_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) - sum(BP_DevWAdv)), 
Dev_DPerc_WAdv = case when sum(Da_DevWAdv) > 0 then
		Convert(Numeric(10,2),(sum(DaB_DevWAdv)/sum(Da_DevWAdv)*100)) else 0 end, 
Dev_PPerc_WAdv = case when sum(D_DevWAdv) > 0 then
		Convert(Numeric(10,2),(sum(B_DevWAdv)/sum(D_DevWAdv)*100)) else 0 end, 
Dev_FP_WAdv = Convert(Numeric(10,2),sum(B_DevWAdv) + sum(F_DevWAdv)), 		

--Total Metres                                                  
Dev_PD_Adv = Convert(Numeric(10,2),sum(Da_DevWAdv) + sum(Da_DevRAdv)), 
Dev_BD_Adv = Convert(Numeric(10,2),sum(DaB_DevRAdv)+sum(DaB_DevWAdv)), 
Dev_VD_Adv = Convert(Numeric(10,2),(sum(DaB_DevRAdv) + sum(DaB_DevWAdv)) - (sum(Da_DevWAdv) +sum(Da_DevRAdv))),
Dev_PP_Adv = Convert(Numeric(10,2),sum(D_DevWAdv) + sum(D_DevRAdv)), 
Dev_BP_Adv = Convert(Numeric(10,2),sum(B_DevRAdv) + sum(B_DevWAdv)),   
Dev_VP_Adv = Convert(Numeric(10,2),(sum(B_DevRAdv) + sum(B_DevWAdv)) - (sum(D_DevWAdv) + sum(D_DevRAdv))),
Dev_Prev_Adv = Convert(Numeric(10,2),sum(BP_DevRAdv) + sum(BP_DevWAdv)),   
Dev_PrevVar_Adv = Convert(Numeric(10,2),(sum(B_DevRAdv) + sum(B_DevWAdv)) - (sum(BP_DevRAdv) + sum(BP_DevWAdv))),
Dev_DPerc_Adv = case when sum(Da_DevRAdv) + sum(Da_DevWAdv) > 0 then
		Convert(Numeric(10,2),((sum(DaB_DevRAdv) + sum(DaB_DevWAdv)) / (sum(Da_DevRAdv) + sum(Da_DevWAdv))*100)) else 0 end, 
Dev_PPerc_Adv = case when sum(D_DevWAdv) + sum(D_DevRAdv) > 0 then
		Convert(Numeric(10,2),((sum(B_DevRAdv) + sum(B_DevWAdv)) / (sum(D_DevWAdv) + sum(D_DevRAdv))*100)) else 0 end, 	
Dev_FP_Adv = Convert(Numeric(10,2),sum(B_TDevRAdv) + sum(B_TDevWAdv) + sum(F_TDevRAdv)),

--High Grade Reef Metres
case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,0),sum(Da_tm21)) else Convert(Numeric(10,0),sum(Da_tm2)) end, 
Dev_PD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(Da_TDevRAdv1)) else Convert(Numeric(10,2),sum(Da_TDevRAdv)) end,
Dev_BD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(DaB_TDevRAdv1)) else Convert(Numeric(10,2),sum(DaB_TDevRAdv)) end,
Dev_VD_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(DaB_TDevRAdv1) - sum(Da_TDevRAdv1)) else Convert(Numeric(10,2),sum(DaB_TDevRAdv) - sum(Da_TDevRAdv)) end,
Dev_PP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(D_TDevRAdv1)) else Convert(Numeric(10,2),sum(D_TDevRAdv)) end,
Dev_BP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(B_TDevRAdv1)) else Convert(Numeric(10,2),sum(B_TDevRAdv)) end,
Dev_VP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(B_TDevRAdv1) - sum(D_TDevRAdv1)) else Convert(Numeric(10,2),sum(B_TDevRAdv) - sum(D_TDevRAdv)) end,
Dev_FP_HgRAdv = case when sum(TheTopPanelsMO) > 0 then Convert(Numeric(10,2),sum(F_TDevRAdv1)) else Convert(Numeric(10,2),sum(F_TDevRAdv)) end,

--tons                          
Dev_PD_RTons = Convert(Numeric(10,2),sum(Da_DevRTons)),
Dev_BD_RTons = Convert(Numeric(10,2),sum(DaB_DevRTons)),
Dev_VD_RTons = Convert(Numeric(10,2),sum(DaB_DevRTons) - sum(Da_DevRTons)),
Dev_PP_RTons = Convert(Numeric(10,2),sum(D_DevRTons)),
Dev_BP_RTons = Convert(Numeric(10,2),sum(B_DevRTons)), 
Dev_VP_RTons = Convert(Numeric(10,2),sum(B_DevRTons) - sum(D_DevRTons)), 
Dev_FP_RTons = Convert(Numeric(10,2),sum(B_DevRTons) + sum(F_DevRTons)),
Dev_PD_WTons = Convert(Numeric(10,2),sum(Da_DevWTons)), 
Dev_BD_WTons = Convert(Numeric(10,2),sum(DaB_DevWTons)),
Dev_VD_WTons = Convert(Numeric(10,2),sum(DaB_DevWTons) - sum(Da_DevWTons)), 
Dev_PP_WTons = Convert(Numeric(10,2),sum(D_DevWTons)), 
Dev_BP_WTons = Convert(Numeric(10,2),sum(B_DevWTons)), 
Dev_VP_WTons = Convert(Numeric(10,2),sum(B_DevWTons) - sum(D_DevWTons)),
Dev_FP_WTons = Convert(Numeric(10,2),sum(B_DevWTons) + sum(F_DevWTons)),

Da_StopeTons = Convert(Numeric(10,2),sum(Da_StopeTons)), 
DaB_StopeTons = Convert(Numeric(10,2),sum(DaB_StopeTons)), 
DaV_StopeTons = Convert(Numeric(10,2),sum(DaB_StopeTons) - sum(Da_StopeTons)),        
D_StopeTons = Convert(Numeric(10,2),sum(D_StopeTons)),                 
B_StopeTons = Convert(Numeric(10,2),sum(B_StopeTons)),  
V_StopeTons = Convert(Numeric(10,2),sum(B_StopeTons) - sum(D_StopeTons)),         
F_StopeTons = Convert(Numeric(10,2),sum(F_StopeTons)), ' 
set @TheQuery2 = '
--SQM Lost
Stp_BD_Mis = Convert(Numeric(10,1),sum(Da_Misefires)), 
Stp_BD_Labour = Convert(Numeric(10,1),sum(Da_Labour)), 
Stp_BD_Other = Convert(Numeric(10,1),sum(Da_Other)), 
Stp_BP_Mis = Convert(Numeric(10,1),sum(D_Misefires)), 
Stp_BP_Labour = Convert(Numeric(10,1),sum(D_Labour)), 
Stp_BP_Other = Convert(Numeric(10,1),sum(D_Other)),   
TheField20 = '''',
TheField21 = '''',TheField22 = '''',    
TheField23 = '''', TheField24 = '''', TheField25 = '''',TheField26 = '''',TheField27 = '''',    
TheField28 = '''',

--Sweepings       
Stp_PP_Sweeps = Convert(Numeric(10,0),sum(D_ReefSweep + D_WasteSweep)),   

--Reef SW Excl Gullies
Stp_PP_ReefSW = case when sum(D_Reefm2) > 0 then
	Convert(Numeric(10,1),sum(D_ReefSqmSW) / sum(D_Reefm2)) else 0 end, 
Stp_BP_ReefSW = case when sum(B_Reefm2) > 0 then
	Convert(Numeric(10,1),sum(B_ReefSqmSW) / sum(B_Reefm2)) else 0 end, 
Stp_VP_ReefSW = case when sum(B_Reefm2) + sum(D_Reefm2) > 0 then
	Convert(Numeric(10,1),(sum(B_ReefSqmSW) + sum(D_ReefSqmSW)) / (sum(B_Reefm2) + sum(D_Reefm2))) else 0 end,
Stp_FP_ReefSW = case when sum(B_Reefm2) + sum(F_Reefm2) > 0 then
	(sum(B_ReefSqmSW) + sum(F_ReefSqmSW))/(sum(B_Reefm2) + sum(F_Reefm2)) else 0 end,	

--Total cmg/t                         
[Stp_PP_Sqmcmgt] = case when sum(D_Reefm2) > 0 then
	Convert(Numeric(10,0),sum(D_sqmreefcmgt) / sum(D_Reefm2)) else 0 end,
[Stp_BP_Sqmcmgt] = case when sum(B_Reefm2) > 0 then
	Convert(Numeric(10,0),sum(B_sqmreefcmgt) / sum(B_Reefm2)) else 0 end, 
[Stp_VP_Sqmcmgt] = case when sum(B_Reefm2) + sum(D_Reefm2) > 0 then
	Convert(Numeric(10,0),(sum(B_sqmreefcmgt) + sum(D_sqmreefcmgt)) / (sum(B_Reefm2) + sum(D_Reefm2))) else 0 end,
[Stp_FP_Sqmcmgt] = case when sum(B_Reefm2) + sum(F_reefm2) > 0 then
	(sum(B_sqmreefcmgt) + sum(F_sqmreefcmgt)) / (sum(B_Reefm2) + sum(F_reefm2)) else 0 end, 
 
 --Content   
[Stp_PP_Content] = Convert(Numeric(10,4),sum(D_grams)/1000) ,                                       
[Stp_BP_Content] = Convert(Numeric(10,4),sum(B_grams)/1000), 
[Stp_VP_Content] = Convert(Numeric(10,4),(sum(B_grams)/1000) - (sum(D_grams) / 1000)),
[Stp_PD_Content] = Convert(Numeric(10,4),sum(Da_grams)/1000),
[Stp_BD_Content] = Convert(Numeric(10,4),sum(DaB_grams)/1000),   
[Stp_VD_Content] = Convert(Numeric(10,4),(sum(DaB_grams)/1000) - (sum(Da_grams) / 1000)),
[Stp_FP_Content] = Convert(Numeric(10,4),(sum(B_grams)/ 1000) + (sum(F_grams) / 1000))
from
		 (select 
p.workplaceid,p.prodmonth,p.sectionid,p.calendardate,
totalnumdays = case when p.prodmonth = ts.ProdMonth then wd.TotalShifts else 0 end,
Shiftnr = case when (p.prodmonth = ts.ProdMonth) and (wd.calendardate = '''+@CalendarDate+''') 
	then wd.shift else 0 end,
DevCheck = case when (p.prodmonth = ts.ProdMonth) and (p.activity = 1) 
	then 1 else 0 end,
StopeCheck = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then 1 else 0 end,  
TheToppanelsMO = (case when tp.workplaceid is not null then 1 else 0 end),
TheToppanelsMAN = (case when t.workplaceid is not null then 1 else 0 end),
Da_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.sqm > 0) then 1 else 0 end,
DaB_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then 1 else 0 end,     
D_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.sqm > 0) then 1 else 0 end, 
B_Stopetrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then 1 else 0 end,    
	      
DaB_Cleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.Value > 0) then 1 else 0 end,
	 
Da_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end,
DaB_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (cl.Value > 0) and (cl.Sickey = 22) then 1 else 0 end,     
D_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
B_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (cl.Value > 0) and (cl.Sickey = 22) then 1 else 0 end,          
F_Devtrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
Da_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.BookMetresAdvance > 0) then 1 else 0 end,
DaB_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (cl1.Value > 0) and (cl1.Sickey = 23) then 1 else 0 end,     
D_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.BookMetresAdvance > 0) then 1 else 0 end, 
B_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (cl1.Value > 0) and (cl1.Sickey = 23) then 1 else 0 end, 
F_DevCleantrue = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity = 1) and (p.MetresAdvance > 0) then 1 else 0 end, 
D_ReefSweep = 0,                                                                                                                           
D_WasteSweep = 0,'
set @TheQuery3 = '
DM_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) then p.sqm else 0 end, 
DM_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then p.reefsqm else 0 end, 
DM_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) 
	then p.wastesqm  else 0 end,     
DM_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and (t.workplaceid is not null)
	then p.sqm else 0 end, 
DM_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and
	 (t.workplaceid is not null) then p.reefsqm else 0 end, 
DM_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and 
	(t.workplaceid is not null) then p.wastesqm  else 0 end, 
Da_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.sqm else 0 end,
Da_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9))then p.reefsqm else 0 end,
Da_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.wastesqm  else 0 end,  
DaB_Sqm =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookSqm else 0 end,     
DaB_reefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookReefSqm else 0 end,     
DaB_offreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookWasteSqm else 0 end,        
D_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.sqm else 0 end, 
D_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm else 0 end, 
D_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and 
	(p.activity IN (0,9)) then p.wastesqm  else 0 end, 
B_Sqm =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookSqm else 0 end,    
B_reefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookReefSqm else 0 end,    
B_offreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookWasteSqm else 0 end,   
F_m2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.sqm else 0 end, 
F_reefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	 (p.activity IN (0,9)) then p.reefsqm else 0 end, 
F_offreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.wastesqm else 0 end,'
set @TheQuery4 = ' 	
Da_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end,
Da_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.reefsqm else 0 end,
Da_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.wastesqm  else 0 end,  
DaB_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.BookSqm else 0 end, 
DaB_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookReefSqm else 0 end,     
DaB_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) then p.BookWasteSqm else 0 end,        
D_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end, 
D_Treefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.reefsqm else 0 end, 
D_Toffreefm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.wastesqm  else 0 end, 
B_Tm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.BookSqm else 0 end,    
B_Treefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) 
	then p.BookReefSqm else 0 end,    
B_Toffreefm2 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) 
	then p.BookWasteSqm else 0 end, 
F_Tm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (t.workplaceid is not null) then p.sqm else 0 end,

DM_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.activity IN (0,9)) and (tp.workplaceid is not null)
	then p.sqm else 0 end,	
Da_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end,
Da_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.reefsqm else 0 end,
Da_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.wastesqm  else 0 end,  
DaB_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.BookSqm else 0 end, 
DaB_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookReefSqm else 0 end,     
DaB_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) then p.BookWasteSqm else 0 end,        
D_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end, 
D_Treefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.reefsqm else 0 end, 
D_Toffreefm21 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.wastesqm  else 0 end, 
B_Tm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.BookSqm else 0 end,    
B_Treefm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) 
	then p.BookReefSqm else 0 end,    
B_Toffreefm21 =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) 
	then p.BookWasteSqm else 0 end, 
F_Tm21 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (tp.workplaceid is not null) then p.sqm else 0 end,
	
BP_Sqm =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and  
	(p.activity IN (0,9)) then p.BookSqm else 0 end,    
--BP_reefm2 =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
--	(p.activity IN (0,9)) and (pp.ReefWaste = ''R'') then p.BookSqm else 0 end, 
--BP_offreefm2 =  case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
--	(p.activity IN (0,9)) and (pp.ReefWaste = ''W'') then p.BookSqm else 0 end,                               
 
Da_FL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end,   
DaB_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.bookSqm > 0) then P.bookFL else 0 end,   
D_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end, 
B_FL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.bookSqm > 0) then P.bookFL else 0 end,       
F_FL = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then Pp.FL else 0 end, 
	
Da_FLNS =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end,   
DaB_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.SicKey = 22) then cl.Value else 0 end,   
D_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end, 
B_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl.SicKey = 22) then cl.Value else 0 end,       
F_FLNS = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.Sqm > 0) then pp.FL else 0 end, 

Da_CleanFL = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end,
DaB_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (cl1.SicKey = 23) then cl1.Value else 0 end,     
D_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end, '
set @TheQuery5 = ' 
B_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) and (cl1.SicKey = 23) then cl1.Value else 0 end,
F_CleanFL =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) and (p.BookSqm > 0) then p.BookFL else 0 end,
-- vir stoping advance per blast
Da_MAdv =  case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end,   
DaB_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookMetresAdvance else 0 end,  
D_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end,   
B_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookMetresAdvance else 0 end,        
F_MAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.MetresAdvance else 0 end, 
	   
DM_DevAdv = case when (p.prodmonth = ts.ProdMonth) and (p.activity = 1) 
	then p.MetresAdvance else 0 end, 
BP_DevRAdv = case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end,
Da_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end, 
DaB_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end,
D_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end,
B_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') then p.BookMetresAdvance else 0 end, 
F_DevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefAdv else 0 end,                               

BP_DevWAdv = case when (p.prodmonth = ts.PrevProdMonth) and (p.ShiftDay <= wd.Shift) and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
Da_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteAdv else 0 end,  
DaB_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
D_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteAdv else 0 end,
B_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''W'') then p.BookMetresAdvance else 0 end,
F_DevWAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate >'''+@CalendarDate+''') AND 
	(p.activity = 1) then p.WasteAdv else 0 end,                                

Da_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,  
DaB_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end,
D_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,
B_TDevRAdv = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end, 
F_TDevRAdv = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) and (t.workplaceid is not null) then p.ReefAdv else 0 end,                                   

B_TDevWAdv = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''W'') and (t.workplaceid is not null) then p.BookMetresAdvance else 0 end,
Da_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,  
DaB_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end,
D_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,
B_TDevRAdv1 = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) and (pp.ReefWaste = ''R'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end, 
F_TDevRAdv1 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) and (tp.workplaceid is not null) then p.ReefAdv else 0 end,                                   

B_TDevWAdv1 = case when  (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) and (pp.ReefWaste = ''W'') and (tp.workplaceid is not null) then p.BookMetresAdvance else 0 end,


Da_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end,
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity  else 0 end, 
DaB_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity = 1) then p.BookReefTons else 0 end,
	--(pp.ReefWaste = ''R'') then p.BookMetresAdvance * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                               
D_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end,
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity  else 0 end, 
B_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookReefTons else 0 end,
	--and (pp.ReefWaste = ''R'') p.BookMetresAdvance * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                               
F_DevRTons = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity = 1) then p.ReefTons else 0 end, 
	--p.AdvReef * pp.DWidth * DHeight * sl.RockDensity else 0 end,   

Da_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) then p.WasteTons else 0 end,
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity  else 0 end,
DaB_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookWasteTons else 0 end,
	--and (pp.ReefWaste = ''W'') then p.BookAdv * pp.DHeight * pp.DWidth * sl.RockDensity  else 0 end,                   
D_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity = 1) then p.WasteTons else 0 end,
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity  else 0 end,
B_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity = 1) then p.BookWasteTons else 0 end,
	--and (pp.ReefWaste = ''W'') then p.BookAdv * pp.DHeight * pp.DWidth * sl.RockDensity else 0 end,  
F_DevWTons = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) then p.WasteTons else 0 end,  
	--p.AdvWaste * pp.DWidth * DHeight * sl.RockDensity else 0 end, '
Set @TheQuery7 = '      
Da_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,        
D_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,         
DaB_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookTons else 0 end,
	--p.BookSqm * p.BookSW / 100 * sl.RockDensity else 0 end,         
B_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookTons else 0 end, 
	--p.BookSqm * p.BookSW / 100 * sl.RockDensity else 0 end,          
F_StopeTons = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Tons else 0 end,
	--p.Sqm * pp.SW / 100 * sl.RockDensity else 0 end,                             

Da_DevUReefTons = 0, 
	--case when (p.prodmonth = ts.ProdMonth) and 
	--(p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) and (pp.ReefWaste = ''R'') then p.Units * pp.dens  else 0 end,    
D_DevUReefTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and 
	--(p.activity = 1) and (pp.ReefWaste = ''R'') then p.Units * pp.dens  else 0 end,
Da_DevUWasteTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	--(p.activity = 1) and (pp.ReefWaste = ''W'') then p.Units * pp.dens  else 0 end, 
D_DevUWasteTons = 0,
	--case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	--(p.activity = 1) and (pp.ReefWaste = ''W'') then p.Units * pp.dens  else 0 end,

Da_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Grams else 0 end, 
DaB_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.BookGrams else 0 end,
D_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.Grams else 0 end, 
B_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and    
	(p.activity IN (0,9)) then p.BookGrams else 0 end,                                       
F_Grams = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.Grams else 0 end,

Da_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm * p.SW else 0 end,
DaB_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm * p.BookSW else 0 end,                                          
D_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.reefsqm * p.SW else 0 end,
B_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm  * p.BookSW else 0 end,                                                                                                          
F_ReefSQMSW = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate > '''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.SW * p.SQM else 0 end,                                                                                                 

D_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.CMGT * p.reefsqm else 0 end,
B_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <='''+@CalendarDate+''') and
	(p.activity IN (0,9)) then p.BookReefSqm * p.Bookcmgt  else 0 end,
Da_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.CMGT * p.reefsqm else 0 end, 
DaB_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and
	 (p.activity IN (0,9)) then p.BookReefSqm * p.Bookcmgt  else 0 end,  
F_sqmreefcmgt = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and 
	(p.activity IN (0,9)) then p.CMGT * p.reefsqm else 0 end,

Da_Misefires = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and
		    (p.ProblemID = ''B1'') and (p.CausedLostBlast = ''Y'') and (p.activity IN (0,9)) then p.sqm else 0 end, 
Da_Labour = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
			(p.ProblemID in (''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and
			(p.activity IN (0,9)) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
Da_Other =  
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate = '''+@CalendarDate+''') and 
			(p.ProblemID not in (''B1'',''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
			(p.activity IN (0,9)) and (p.ProblemID is not null) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
D_Misefires = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and 
		    (p.ProblemID = ''B1'') and (p.CausedLostBlast = ''Y'') and 
			(p.activity IN (0,9)) and (p.sqm > 0) then p.sqm else 0 end, 
D_Labour = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and  
            (p.ProblemID in (''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
            (p.activity IN (0,9)) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end, 
D_Other = 
  case when (p.prodmonth = ts.ProdMonth) and  (p.CalendarDate <= '''+@CalendarDate+''') and  
			(p.ProblemID not in (''B1'',''N1'',''N2'',''N3'',''N4'',''TR'',''R 1'',''R 2'',''R 3'',''R 4'')) and 
			(p.activity IN (0,9))  and (p.ProblemID is not null) and (p.CausedLostBlast = ''Y'') then p.sqm else 0 end,
F_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.calendardate > '''+@CalendarDate+''') and
	(p.activity = 1) then (p.MetresAdvance * pp.DWidth) else 0 end,  			 
--DaB_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance + p.BookAdv) * pp.DWidth else 0 end,
DaB_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance) * pp.DWidth else 0 end,
D_Devm2 = case when (p.prodmonth = ts.ProdMonth) and (p.CalendarDate <= '''+@CalendarDate+''')and (p.activity = 1) then (p.WasteAdv + p.ReefAdv) * pp.dwidth else 0 end,
Da_Devm2 = case when(p.prodmonth = ts.ProdMonth) and (p.CalendarDate = '''+@CalendarDate+''')and (p.activity = 1) then (p.WasteAdv + p.ReefAdv) * pp.dwidth else 0 end,  
B_Devm2 = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance) * pp.dwidth else 0 end                               
--B_Devm2 = case when (p.prodmonth = ts.ProdMonth) and   (p.CalendarDate <= '''+@CalendarDate+''') and (p.activity = 1) then (p.BookMetresAdvance + p.BookMetresAdvance) * pp.DWidth else 0 end
from Planning p 
left outer join PLANMONTH pp on
  pp.workplaceid = p.workplaceid and 
  pp.Prodmonth = p.Prodmonth and  
  pp.SectionID = p.SectionID and 
  pp.Activity = p.Activity and 
  pp.PlanCode = p.PlanCode and
  pp.IsCubics = p.IsCubics 
--left outer join problems_Complete r on
--	p.workplaceid = r.workplaceid and 
--	p.Prodmonth = r.Prodmonth and  
--	p.SectionID = r.SectionID and 
--	p.Activity = r.Activity and   
--	p.CalendarDate = r.CalendarDate
inner join section_complete sc on
  sc.SectionID = p.sectionid and 
  sc.prodmonth = p.prodmonth ' 
set @TheQuery6 = ''
  IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + '
left outer join TopPanelsSelected t on
  t.Prodmonth = p.Prodmonth and
  t.SectionID = sc.SectionID_5 and
  t.WorkplaceID = p.WorkplaceID and
  t.Activity = p.Activity 
  left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_2 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity  '
 END 
  IF (@Section = 1) or (@Section = 2)or (@Section = 3)
BEGIN
   set @TheQuery6 = @TheQuery6 + '
left outer join TopPanelsSelected t on
  t.Prodmonth = p.Prodmonth and
  t.SectionID = sc.SectionID_5 and
  t.WorkplaceID = p.WorkplaceID and
  t.Activity = p.Activity 
  left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_2 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity'
 END
 set @TheQuery6 = @TheQuery6 + '  
left outer join Temp_SectionStartDate ts on
  ts.SectionID = p.SectionID and
  ts.UserID = '''+@UserID+''' 
left outer join TempWorkdaysMO wd on
  wd.UserID = '''+@UserID+''' and
  wd.SectionID = sc.SectionID_2 and
  wd.Prodmonth = ts.ProdMonth and
  wd.CalendarDate = '''+@CalendarDate+'''
left outer join tempworkdaysMO wd1 on
  wd1.UserID = '''+@UserID+''' and   
  wd1.sectionid = ts.SectionID and  
  wd1.ProdMonth = ts.PrevProdMonth and
  wd1.Shift = wd.Shift 
left outer join SICCapture cl on
  cl.CalendarDate = p.CalendarDate and
  cl.SICKey in (22) and
  cl.SectionID = sc.SectionID_1 and
  cl.WorkplaceID = p.WorkplaceID
  left outer join SICCapture cl1 on
  cl1.CalendarDate = p.CalendarDate and
  cl1.SICKey in (23) and
  cl1.SectionID = sc.SectionID_1 and
  cl1.WorkplaceID = p.WorkplaceID,    
 Sysset sl                   
where p.CalendarDate >= '''+@TheStartDate+''' and p.CalendarDate <= '''+@TheEndDate+''' and
		p.PlanCode = ''MP'' and p.IsCubics = ''N'' '
IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_2 = '''+@SectionID+''' and sc.Name_2 = '''+@MOName+''' '
END 
if(@Section = 3)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_3 = '''+@SectionID+''' '
end
if(@Section = 2)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_4 = '''+@SectionID+''' '
end 
if(@Section = 1)
begin
 set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_5 = '''+@SectionID+''' '
end
set @TheQuery6 = @TheQuery6 + ') a 
left outer join Temp_SectionStartDate t on
  t.SectionID = a.SectionID and
  t.UserID = '''+@UserID+''' 
inner join Section_Complete sc on
  sc.ProdMonth = t.ProdMonth and
  sc.SectionID = a.SectionID ' 
  if(@Section = 4)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_2 ='''+CAST(@SectionID AS VARCHAR(10))+''' and 
				sc.Name_2 = '''+@MOName+''' '
  end
  if(@Section = 3)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_3 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
  if(@Section = 2)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_4 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
  if(@Section = 1)
  begin
	set @TheQuery6 = @TheQuery6 + ' and sc.SectionID_5 ='''+CAST(@SectionID AS VARCHAR(10))+''''
  end
set @TheQuery6 = @TheQuery6 +'group by ' 
IF (@Section = 1)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_5 +'':''+sc.Name_5,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 2)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_4 +'':''+sc.Name_4,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 3)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_3 +'':''+sc.Name_3,'''') , 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , '
END
IF (@Section = 4)
BEGIN
   set @TheQuery6 = @TheQuery6 + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') , 
								 isnull(sc.SectionID_1 +'':''+sc.Name_1,'''') , '
END 
set @TheQuery6 = @TheQuery6 + ' a.Workplaceid with rollup '


--Print @TheQuery
--Print @TheQuery1
--Print @TheQuery2
--Print @TheQuery3
--Print @TheQuery4
--Print @TheQuery5
--Print @TheQuery7
--Print @TheQuery6

Exec (@TheQuery+@TheQuery1+@TheQuery2+@TheQuery3+@TheQuery4+@TheQuery5+ @TheQuery7+@TheQuery6)
	
END
 go
 go
 ALTER procedure [dbo].[sp_SICReport_Tramming] --'MINEWARE', '2013/12/02', '1', '1' 
--declare
@UserID varchar(20),
@CalendarDate varchar(10),
@SectionID varchar(20),
@Section int,
@MOName varchar(50)
as

--set @UserID = 'MINEWARE'
--set @CalendarDate = '2017-01-11'
--set @SectionID = 'GM'
--set @Section = 1
--set @MOName = 'S Mofokeng'

declare @TheQuery varchar(8000)
declare @TheQuery1 varchar(8000)
declare @TheQuery2 varchar(8000)

declare @TheStartDate varchar(10),
@TheEndDate varchar(10),
@TheMonth varchar(10)

SET NOCOUNT ON;

--IF (@Section = 1) or (@Section = 2) or (@Section = 3)
--BEGIN
	select @TheStartDate = convert(varchar(10),a.StartDate,120),
		   @TheEndDate = convert(varchar(10),a.EndDate,120),	
		   @TheMonth = a.MillMonth
	from
		(select min(cm.StartDate) StartDate, max(cm.EndDate) EndDate, cm.MillMonth  
			  from CODE_CALENDAR cc
			  inner join CALENDARMILL cm on
				cm.CalendarCode = cc.CalendarCode
			  where cm.StartDate <= @CalendarDate and 
			  cm.EndDate >= @CalendarDate and
			  cc.Description = 'Mill Calendar' 
			  group by cm.MillMonth
		) a
--END
--ELSE
--BEGIN
--	select @TheStartDate = convert(varchar(10),a.StartDate,120),
--		   @TheEndDate = convert(varchar(10),a.EndDate,120),
--		   @TheMonth = a.Prodmonth
--	from
--		(select distinct(s.BeginDate) StartDate, s.EndDate EndDate, s.Prodmonth
--			from seccal s 
--			inner join SectionComplete sc on
--			  sc.ProdMonth = s.ProdMonth and
--			  sc.SBID = s.SectionID
--			where s.BeginDate <= @CalendarDate and
--				  s.EndDate >= @CalendarDate and
--				  sc.MOID = @SectionID
--		) a
--END  

if (@TheStartDate is NULL)
BEGIN
  select '' TheSection1, '' TheSection,
	 Dev_Count = Convert(decimal(13,2),0),
	 Stp_Count = Convert(decimal(13,2),0),
	 TheActivity = '',
	 DayP_Broken = Convert(decimal(13,2),0),
	 DayB_Broken = Convert(decimal(13,2),0),
	 DayV_Broken = Convert(decimal(13,2),0),
	 ProgP_Broken = Convert(decimal(13,2),0),	
	 ProgF_Broken = Convert(decimal(13,2),0),
	 ProgB_Broken = Convert(decimal(13,2),0),	
	 ProgV_Broken = Convert(decimal(13,2),0),
	 DayP_Tot_Tons = Convert(decimal(13,2),0),
	 DayB_Tot_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 ProgB_Tot_Tons = Convert(decimal(13,2),0),		
	 MonthPlan_Stp_Tons = Convert(decimal(13,2),0),
	 DayP_Stp_Tons = Convert(decimal(13,2),0),
	 DayB_Stp_Tons = Convert(decimal(13,2),0),				  
	 ProgP_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 ProgB_Stp_Tons = Convert(decimal(13,2),0),					  					  						  					  
	 MonthPlan_Dev_ReefTons = Convert(decimal(13,2),0),					  
	 DayP_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayB_Dev_ReefTons = Convert(decimal(13,2),0),				   
	 ProgP_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgB_Dev_ReefTons = Convert(decimal(13,2),0),				  
	 MonthPlan_Dev_WasteTons = Convert(decimal(13,2),0),				  						  
	 DayP_Dev_WasteTons = Convert(decimal(13,2),0),
	 DayB_Dev_WasteTons = Convert(decimal(13,2),0),				  
	 ProgP_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgB_Dev_WasteTons = Convert(decimal(13,2),0),						  
	 MonthPlan_Dev_TotalTons = Convert(decimal(13,2),0),						  					  
	 DayP_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Dev_TotalTons = Convert(decimal(13,2),0),				  					  					  
	 ProgP_Dev_TotalTons = Convert(decimal(13,2),0),
	 ProgB_Dev_TotalTons = Convert(decimal(13,2),0),
	 DayB_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgB_Stp_TopTons = Convert(decimal(13,2),0),
	 MonthPlan_Stp_TopTons = Convert(decimal(13,2),0),
	 DayP_Stp_TopTons = Convert(decimal(13,2),0),
	 ProgP_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_TopTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Stp_TopTons = Convert(decimal(13,2),0),
	 DayV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgV_Stp_Tons = Convert(decimal(13,2),0),
	 ProgF_Stp_Tons = Convert(decimal(13,2),0),
	 DayV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgV_Tot_Tons = Convert(decimal(13,2),0),
	 ProgF_Tot_Tons = Convert(decimal(13,2),0),
	 DayV_Dev_ReefTons = Convert(decimal(13,2),0),
	 ProgV_Dev_ReefTons = Convert(decimal(13,2),0),
	 DayV_Dev_WasteTons = Convert(decimal(13,2),0),
	 ProgV_Dev_WasteTons = Convert(decimal(13,2),0),					   
	 DayV_Dev_TotalTons = Convert(decimal(13,2),0),					  						  
	 ProgV_Dev_TotalTons = Convert(decimal(13,2),0)
END
ELSE
BEGIN
	set @TheQuery = '
select isnull(TheSection,'''') TheSection1, isnull(TheSection,'''') TheSection, 
 TheActivity = isnull(TheActivity,''''),
 Dev_Count = Convert(decimal(13,2),sum(Dev_Count)),
 Stp_Count = Convert(decimal(13,2),sum(Stp_Count)),
 DayP_Broken = Convert(decimal(13,2),sum(DayP_Broken)),
 DayB_Broken = Convert(decimal(13,2),sum(DayB_Broken)),
 DayV_Broken = Convert(decimal(13,2),sum(DayB_Broken) - sum(DayP_Broken)),
 ProgP_Broken = Convert(decimal(13,2),sum(ProgP_Broken)),		
 ProgB_Broken = Convert(decimal(13,2),sum(ProgB_Broken)),	
 ProgF_Broken = Convert(decimal(13,2),sum(ProgB_Broken) + sum(ProgF_Broken)),	
 ProgV_Broken = Convert(decimal(13,2),sum(ProgB_Broken) - sum(ProgP_Broken)),	
 DayP_Tot_Tons = Convert(decimal(13,2),isnull(sum(DayP_Tot_Tons),0)),
 DayB_Tot_Tons = Convert(decimal(13,2),isnull(sum(DayB_Tot_Tons),0)),				  
 ProgP_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgP_Tot_Tons),0)),
 ProgF_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgF_Tot_Tons),0)),
 ProgB_Tot_Tons = Convert(decimal(13,2),isnull(sum(ProgB_Tot_Tons),0)),		
 MonthPlan_Stp_Tons = Convert(decimal(13,2),sum(MonthPlan_Stp_Tons)),
 DayP_Stp_Tons = Convert(decimal(13,2),sum(DayP_Stp_Tons)),
 DayB_Stp_Tons = Convert(decimal(13,2),sum(DayB_Stp_Tons)),				  
 ProgP_Stp_Tons = Convert(decimal(13,2),sum(ProgP_Stp_Tons)),
 ProgF_Stp_Tons = Convert(decimal(13,2),sum(ProgF_Stp_Tons)),
 ProgB_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons)),					  						  						  					  
 MonthPlan_Dev_ReefTons = Convert(decimal(13,2),sum(MonthPlan_Dev_ReefTons)),					  
 DayP_Dev_ReefTons = Convert(decimal(13,2),sum(DayP_Dev_ReefTons)),
 DayB_Dev_ReefTons = Convert(decimal(13,2),sum(DayB_Dev_ReefTons)),				   
 ProgP_Dev_ReefTons = Convert(decimal(13,2),sum(ProgP_Dev_ReefTons)),
 ProgB_Dev_ReefTons = Convert(decimal(13,2),sum(ProgB_Dev_ReefTons)),				  
 MonthPlan_Dev_WasteTons = Convert(decimal(13,2),sum(MonthPlan_Dev_WasteTons)),				  						  
 DayP_Dev_WasteTons = Convert(decimal(13,2),sum(DayP_Dev_WasteTons)),
 DayB_Dev_WasteTons = Convert(decimal(13,2),sum(DayB_Dev_WasteTons)),				  
 ProgP_Dev_WasteTons = Convert(decimal(13,2),sum(ProgP_Dev_WasteTons)),
 ProgB_Dev_WasteTons = Convert(decimal(13,2),sum(ProgB_Dev_WasteTons)),						  
 MonthPlan_Dev_TotalTons = Convert(decimal(13,2),sum(MonthPlan_Dev_TotalTons)),						  					  
 DayP_Dev_TotalTons = Convert(decimal(13,2),sum(DayP_Dev_TotalTons)),
 DayB_Dev_TotalTons = Convert(decimal(13,2),sum(DayB_Dev_TotalTons)),				  					  					  
 ProgP_Dev_TotalTons = Convert(decimal(13,2),sum(ProgP_Dev_TotalTons)),
 ProgB_Dev_TotalTons = Convert(decimal(13,2),sum(ProgB_Dev_TotalTons)),
 DayB_Stp_TopTons = Convert(decimal(13,2),sum(DayB_Stp_TopTons)),
 ProgB_Stp_TopTons = Convert(decimal(13,2),sum(ProgB_Stp_TopTons)),
 MonthPlan_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then MonthPlan_Stp_TopTons1 else MonthPlan_Stp_TopTons end)),	
 DayP_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then DayP_Stp_TopTons1 else DayP_Stp_TopTons end)),
 ProgP_Stp_TopTons = Convert(decimal(13,2),sum(case when TopPanelsMO <> 0 then ProgP_Stp_TopTons1 else ProgP_Stp_TopTons end )),
 DayV_Stp_TopTons = Convert(decimal(13,2),sum(DayB_Stp_TopTons) - sum(case when TopPanelsMO <> 0 then DayP_Stp_TopTons1 else DayP_Stp_TopTons end)),					  						  
 ProgV_Stp_TopTons = Convert(decimal(13,2),sum(ProgB_Stp_TopTons) - sum(ProgP_Stp_TopTons)),
 DayV_Stp_Tons = Convert(decimal(13,2),sum(DayB_Stp_Tons) - sum(DayP_Stp_Tons)),
 ProgV_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons) - sum(ProgP_Stp_Tons)),
 ProgF_Stp_Tons = Convert(decimal(13,2),sum(ProgB_Stp_Tons) + sum(ProgF_Stp_Tons)),
 DayV_Tot_Tons = Convert(decimal(13,2),sum(DayB_Tot_Tons) - sum(DayP_Tot_Tons)),
 ProgV_Tot_Tons = Convert(decimal(13,2),sum(ProgB_Tot_Tons) - sum(ProgP_Tot_Tons)),
 ProgF_Tot_Tons = Convert(decimal(13,2),sum(ProgB_Tot_Tons) + sum(ProgF_Tot_Tons)),
 DayV_Dev_ReefTons = Convert(decimal(13,2),sum(DayB_Dev_ReefTons) - sum(DayP_Dev_ReefTons)),
 ProgV_Dev_ReefTons = Convert(decimal(13,2),sum(ProgB_Dev_ReefTons) - sum(ProgP_Dev_ReefTons)),
 DayV_Dev_WasteTons = Convert(decimal(13,2),sum(DayB_Dev_WasteTons) - sum(DayP_Dev_WasteTons)),
 ProgV_Dev_WasteTons = Convert(decimal(13,2),sum(ProgB_Dev_WasteTons) - sum(ProgP_Dev_WasteTons)),					   
 DayV_Dev_TotalTons = Convert(decimal(13,2),sum(DayB_Dev_TotalTons) - sum(DayP_Dev_TotalTons)),					  						  
 ProgV_Dev_TotalTons = Convert(decimal(13,2),sum(ProgB_Dev_TotalTons) -	sum(ProgP_Dev_TotalTons))
from
(
(
  select  '
IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery = @TheQuery + ' case when t.Sectionid = ''Unidentified'' then t.SectionID else isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') end TheSection1, 
		case when t.Sectionid = ''Unidentified'' then t.SectionID else isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') end TheSection,  '
END
IF (@Section = 4)
BEGIN
   set @TheQuery = @TheQuery + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								 isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
END  
set @TheQuery = @TheQuery + ' Activity, TheActivity = case when t.Activity IN (0,9) then ''Stp'' else ''Dev'' end,
   DayP_Broken = 0,	
   DayB_Broken = 0,	
   ProgP_Broken = 0,		
   ProgF_Broken = 0,
   ProgB_Broken = 0,		
   DayP_Tot_Tons = 0,	
   DayB_Tot_Tons = sum(case when (t.CalendarDate = '''+@CalendarDate+''')   
						then (t.DTons + t.ATons + t.NTons)  else 0 end),					     		  
   ProgP_Tot_Tons = Convert(decimal(13,2),0),
   ProgF_Tot_Tons = Convert(decimal(13,2),0),
   ProgB_Tot_Tons = sum(t.DTons + t.ATons + t.NTons),		
   DayB_Stp_Tons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Stp_Tons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then (t.DTons + t.ATons + t.NTons) else 0 end), 
   DayB_Dev_ReefTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.ReefWaste = ''R'') and
							(t.Activity = 1) 
					    then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Dev_WasteTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.ReefWaste = ''W'') and
							(t.Activity = 1)  
  					   then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Dev_TotalTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity = 1)  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_ReefTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.ReefWaste = ''R'') and
							(t.Activity = 1)   
  					   then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_WasteTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.ReefWaste = ''W'') and
							(t.Activity = 1)   
  						then (t.DTons + t.ATons + t.NTons) else 0 end),
   ProgB_Dev_TotalTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity = 1)  
						then (t.DTons + t.ATons + t.NTons) else 0 end),
   DayB_Stp_TopTons = sum(case when (t.CalendarDate = '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then t.TopTons else 0 end),
   ProgB_Stp_TopTons = sum(case when (t.CalendarDate <= '''+@CalendarDate+''') and (t.Activity IN (0,9))  
						then t.TopTons else 0 end),		 						
   Dev_Count = sum(case when (t.Activity = 1) then 1 else 0 end), 	
   Stp_Count = sum(case when (t.Activity IN (0,9)) then 1 else 0 end),
   TopPanelsMO = 0,
   TopPanelsMAN = 0,
   MonthPlan_Stp_Tons = 0,	
   DayP_Stp_Tons = 0,						  
   ProgP_Stp_Tons = 0,				  					  						  					  
   ProgF_Stp_Tons = 0,
   MonthPlan_Dev_ReefTons = 0,					  
   DayP_Dev_ReefTons = 0,				   
   ProgP_Dev_ReefTons = 0,						  
   MonthPlan_Dev_WasteTons = 0,					  						  
   DayP_Dev_WasteTons = 0,							  
   ProgP_Dev_WasteTons = 0,							  
   MonthPlan_Dev_TotalTons = 0,				  					  
   DayP_Dev_TotalTons = 0,ProgP_Dev_TotalTons = 0,MonthPlan_Stp_TopTons = 0,
   MonthPlan_Stp_TopTons1 = 0,DayP_Stp_TopTons = 0, 
   ProgP_Stp_TopTons = 0,DayP_Stp_TopTons1 = 0, ProgP_Stp_TopTons1 = 0  									
 from BookingTramming t
 left outer join vw_Sections_Complete_MO sc on
   sc.ProdMonth = t.millMonth and
   sc.SectionID_2 = t.SectionID  
 where t.CalendarDate >= '''+@TheStartDate+''' and
	   t.CalendarDate <= '''+@CalendarDate+''' '
set @TheQuery2 = ''
IF (@Section = 4)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_2 = '''+@SectionID+''' and sc.Name_2 = '''+@MOName+''' '

END 
IF (@Section = 3)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_3 = '''+@SectionID+''' '

END
IF (@Section = 2)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_4 = '''+@SectionID+''' '

END
IF (@Section = 1)
BEGIN	   
	set @TheQuery2 = @TheQuery2 + ' and sc.SectionID_5 = '''+@SectionID+''' '

END
set @TheQuery1 = ' group by '
IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' t.SectionID, sc.SectionID_2, sc.Name_2, '
END
IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' sc.SectionID_2, sc.Name_2, '
END
set @TheQuery1 = @TheQuery1 + 'Activity '
  set @TheQuery1 = @TheQuery1 + '
 )union
(select '
--IF (@Section = 1)
--BEGIN
--   set @TheQuery1 = @TheQuery1 + ' isnull(sc.MOID +'':''+sc.MOName,'''') TheSection1, 
--								   isnull(sc.MOID +'':''+sc.MOName,'''') TheSection, '
--END
--IF (@Section = 4)
--BEGIN
--   set @TheQuery1 = @TheQuery1 + ' isnull(sc.MOID +'':''+sc.MOName,'''') TheSection1, 
--								   isnull(sc.MOID +'':''+sc.MOName,'''') TheSection, '
--END  
set @TheQuery1 = @TheQuery1 + ' isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection1, 
								   isnull(sc.SectionID_2 +'':''+sc.Name_2,'''') TheSection, '
set @TheQuery1 = @TheQuery1 + ' p.Activity, TheActivity=  case when p.Activity IN (0,9) then ''Stp'' else ''Dev'' end,
 DayP_Broken = sum(case when (p.CalendarDate = '''+@CalendarDate+''')   and (p.Activity IN (0,9)) 
						  then (p.BookTons) else 0 end),
 DayB_Broken = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.booktons)
						  else 0 end),
 ProgP_Broken = sum(case when (p.CalendarDate <= '''+@CalendarDate+''')  and (p.Activity IN (0,9)) 
						  then (p.BookTons) else 0 end),
 ProgF_Broken = sum(case when (p.CalendarDate > '''+@CalendarDate+''')  and (p.Activity IN (0,9)) 
						  then (p.Tons) else 0 end),
 ProgB_Broken = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.Booktons)
						  else 0 end),		
 DayP_Tot_Tons = sum(case when (p.CalendarDate = '''+@CalendarDate+''')  
						  then (p.BookTons) else 0 end),	
 DayB_Tot_Tons = Convert(decimal(13,5),0),				  
 ProgP_Tot_Tons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''')
						  then (p.BookTons)else 0 end),
 ProgF_Tot_Tons = sum(case when (p.CalendarDate > '''+@CalendarDate+''')
						  then (p.Tons)else 0 end),
 ProgB_Tot_Tons = 0,	
 DayB_Stp_Tons = 0,	
 ProgB_Stp_Tons = 0,	
 DayB_Dev_ReefTons = 0,	
 DayB_Dev_WasteTons = 0,	
 DayB_Dev_TotalTons = 0,	
 ProgB_Dev_ReefTons = 0,	
 ProgB_Dev_WasteTons = 0,	
 ProgB_Dev_TotalTons = 0,	
 DayB_Stp_TopTons = 0,	
 ProgB_Stp_TopTons = 0,				
 Dev_Count = sum(case when (p.Activity = 1) then 1 else 0 end),
 Stp_Count = sum(case when (p.Activity IN (0,9)) then 1 else 0 end),
 ToppanelsMO = sum(case when tp1.workplaceid is not null then 1 else 0 end),
 ToppanelsMAN = sum(case when tp.workplaceid is not null then 1 else 0 end),
 MonthPlan_Stp_Tons =sum(case when (p.Activity IN (0,9)) 
						  then p.Tons
						  else 0 end),  							  							  
 DayP_Stp_Tons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.BookTons)
						  else 0 end),						  
 ProgP_Stp_Tons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.BookTons)
						  else 0 end),
 ProgF_Stp_Tons = sum(case when (p.CalendarDate > '''+@CalendarDate+''') and (p.Activity IN (0,9)) 
						  then (p.Tons)
						  else 0 end),					  								  						  					  
 MonthPlan_Dev_ReefTons = sum(case when (p.Activity = 1)  
						  then p.ReefTons
						  else 0 end),						  
 DayP_Dev_ReefTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.BookReefTons 
						  else 0 end),					   
 ProgP_Dev_ReefTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1)   
						  then p.bookReeftons
						  else 0 end),						  
 MonthPlan_Dev_WasteTons = sum(case when (p.Activity = 1)  
						  then p.WasteTons
						  else 0 end),						  						  
 DayP_Dev_WasteTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1)   
						  then p.bookWastetons
						  else 0 end),						  
 ProgP_Dev_WasteTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.bookWastetons
						  else 0 end),								  
 MonthPlan_Dev_TotalTons = sum(case when (p.Activity = 1) 
						  then p.tons
						  else 0 end),		
				  					  
 DayP_Dev_TotalTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.booktons
						  else 0 end) ,					  					  					  
 ProgP_Dev_TotalTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity = 1) 
						  then p.booktons
						  else 0 end),
 MonthPlan_Stp_TopTons = sum(case when (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null)
						  then p.tons
						  else 0 end), 
 MonthPlan_Stp_TopTons1 = sum(case when (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null)
						  then p.tons
						  else 0 end),
 DayP_Stp_TopTons = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null)
						  then p.booktons
						  else 0 end), 
 ProgP_Stp_TopTons = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp.Workplaceid is not null) 
						  then p.booktons
						   else 0 end),
 DayP_Stp_TopTons1 = sum(case when (p.CalendarDate = '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null)
						  then p.booktons
						  else 0 end), 
 ProgP_Stp_TopTons1 = sum(case when (p.CalendarDate <= '''+@CalendarDate+''') and (p.Activity IN (0,9)) and
								  (tp1.Workplaceid is not null) 
						  then p.booktons
						   else 0 end)   						  						  						  
from Planning  p 
inner join PLANMONTH pp on
  p.Prodmonth = pp.Prodmonth and
  p.SectionID = pp.Sectionid and
  p.WorkplaceID = pp.Workplaceid and
  p.Activity = pp.Activity and
  p.PlanCode = pp.PlanCode and
  p.IsCubics = pp.IsCubics
inner join Section_Complete sc on
  sc.Prodmonth = p.Prodmonth and
  sc.SectionID = p.SectionID    
inner join WORKPLACE w on
  w.WorkplaceID = p.WorkplaceID '
 IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + '
left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_5 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity 
  left outer join TopPanelsSelected tp1 on
  tp1.Prodmonth = p.Prodmonth and
  tp1.SectionID = sc.SectionID_2 and
  tp1.WorkplaceID = p.WorkplaceID and
  tp1.Activity = p.Activity  '
 END 
  IF (@Section = 1) or (@Section = 2) or (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + '
left outer join TopPanelsSelected tp on
  tp.Prodmonth = p.Prodmonth and
  tp.SectionID = sc.SectionID_5 and
  tp.WorkplaceID = p.WorkplaceID and
  tp.Activity = p.Activity 
  left outer join TopPanelsSelected tp1 on
  tp1.Prodmonth = p.Prodmonth and
  tp1.SectionID = sc.SectionID_2 and
  tp1.WorkplaceID = p.WorkplaceID and
  tp1.Activity = p.Activity'
 END
 set @TheQuery1 = @TheQuery1 + ' 
 
, SYSSET sl 
 where p.CalendarDate >= '''+@TheStartDate+''' and
       p.CalendarDate <= '''+@TheEndDate+''' and p.PlanCode = ''MP'' and p.IsCubics = ''N'' '
IF (@Section = 4)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_2 = '''+@SectionID +'''  and sc.Name_2 = '''+@MOName+''' '
END   
IF (@Section = 3)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_3 = '''+@SectionID +''' '
END 
IF (@Section = 2)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_4 = '''+@SectionID +''' '
END 
IF (@Section = 1)
BEGIN
   set @TheQuery1 = @TheQuery1 + ' and  sc.SectionID_5 = '''+@SectionID +''' '
END   

set @TheQuery1 = @TheQuery1 + 'group by sc.SectionID_2, sc.Name_2, p.Activity ' 
set @TheQuery1 = @TheQuery1 + ' 
 )) a
group by a.TheActivity, a.TheSection 
with Rollup '


--print @TheQuery
--print @TheQuery2
--print @TheQuery1
exec (@TheQuery+@TheQuery2+@TheQuery1)

end
go
CREATE  PROCEDURE [dbo].[sp_GenericReport]
--declare
@NAME_5 varchar(1) = 'Y', 
@NAME_4 varchar(1) = 'Y', 
@NAME_3 varchar(1) = 'Y',  
@NAME_2 varchar(1) = 'Y', 
@NAME_1 varchar(1) = 'Y', 
@NAME varchar(1) = 'Y', 
@Workplace varchar(1) = 'Y',

@RunLevel int = 1,
@SectionID varchar(20) = 'GM',

@ProdMonth varchar(6) = '201612',
@FromMonth varchar(6) = '201612', 
@ToMonth varchar(6) = '201612',
@CalendarDate varchar(10) = '2016-12-12',
@FromDate varchar(10) = '2016-12-12',
@ToDate varchar(10) = '2016-12-12',
 
@ReportType varchar(20) = 'P',  --  P-Production Month, M-From_To Month, D-From-To Date
@TotalsPerMonth Varchar(2) = 'Y',

--@TheOreFlowLevel varchar(8000),
--@TheReef varchar(1000),

--@TheIndicator char,
--@TheMiningMethod char,
@PlanMonth varchar(1) = 'Y',
@PlanMonthLock varchar(1) = 'Y',
@PlanProg varchar(1) = 'Y',
@PlanProgLock varchar(1) = 'Y',
@Book varchar(1) = 'Y',
@Meas varchar(1) = 'Y',
@BussPlan varchar(1) = 'Y',
@Abs varchar(1) = 'N',
@FC varchar(1) = 'N',

@TheStopeLedge varchar(1) = '0' -- 0 All, 1 Stoping, 2 Ledging
as

declare @SelectPart varchar(8000)
declare @SelectPart1 varchar(8000)
declare @SelectPart2 varchar(8000)
declare @SelectPart3 varchar(8000)

declare @PlanSelect varchar(8000)
declare @PlanSelect1 varchar(8000)
declare @PlanSelect2 varchar(8000)
declare @PlanFrom varchar(8000)
declare @PlanWhere varchar(8000)

declare @SurveyUnion varchar(8000)
declare @SurveySelect varchar(8000)
declare @SurveySelect1 varchar(8000)
declare @SurveyFrom varchar(8000)
declare @SurveyWhere varchar(8000)

declare @BusUnion varchar(8000)
declare @BusSelect varchar(8000)
declare @BusSelect1 varchar(8000)
declare @BusFrom varchar(8000)
declare @BusWhere varchar(8000)

declare @LPlanUnion varchar(8000)
declare @LPlanSelect varchar(8000)
declare @LPlanSelect1 varchar(8000)
declare @LPlanSelect2 varchar(8000)
declare @LPlanFrom varchar(8000)
declare @LPlanWhere varchar(8000)

declare @GroupBy varchar (8000)
declare @GroupBy1 varchar (8000)
declare @SelectBy varchar (200)
declare @ReadSection varchar (30) 
 
declare @RunName VARCHAR(100)
 
IF @RunLevel = 1
BEGIN
  SET @RunName = 'Name_5'
  SET @ReadSection = ' sc.SectionID_5 ' 
END
  
IF @RunLevel = 2
BEGIN
  SET @RunName = 'Name_4'
  SET @ReadSection = ' sc.SectionID_4 ' 
END
IF @RunLevel = 3
BEGIN
  SET @RunName = 'Name_3'
  SET @ReadSection = ' sc.SectionID_3 ' 
END
IF @RunLevel = 4
BEGIN
  SET @RunName = 'Name_2'
  SET @ReadSection = ' sc.SectionID_2 ' 
END
IF @RunLevel = 5
BEGIN
  SET @RunName = 'Name_1'
  SET @ReadSection = ' sc.SectionID_1 ' 
END
IF @RunLevel = 6
BEGIN
  SET @RunName = 'Name'
  SET @ReadSection = ' sc.SectionID ' 
END
--IF (@TheRunLevel = 1)
--BEGIN
--	  set @GroupBy = 
--		 ' sc.SectionID_5, sc.Name_5, sc.SectionID_4, sc.Name_4 ,sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name '
--	set @ReadSection = ' sc.SectionID_5 '  
--END

--IF (@TheRunLevel = 2)
--BEGIN
--		set @GroupBy = 
--			' sc.SectionID_4, sc.Name_4, sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name' 
--	set @ReadSection = ' sc.SectionID_4 '  
--END

--IF (@TheRunLevel = 3)
--BEGIN
--		set @GroupBy = 
--			' sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name' 
--	set @ReadSection = ' sc.SectionID_3 ' 
--END

--IF (@TheRunLevel = 4)
--BEGIN
--		set @GroupBy = 
--			' sc.SectionID_2, sc.Name_2,sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name '
--	set @ReadSection = ' sc.SectionID_2 ' 
--END

--IF (@TheRunLevel = 5)
--BEGIN
--		set @GroupBy = ' sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name '
--	set @ReadSection = ' sc.SectionID_1 '   
--END

--IF (@TheLevel = 6)
--BEGIN
--		set @GroupBy = ' sc.SectionID, sc.Name '
--	set @ReadSection = ' sc.SectionID '   
--END

--IF (@TheRunLevel = 7)
--BEGIN
--		set @GroupBy = 'Name_2, c.Name, cs.Name '
--	set @ReadSection = ' sc.Sectionid_5 '   
--END

--IF (@TheRunLevel = 8)
--BEGIN
--		set @GroupBy = 'c.Name, cs.Name '
--	set @ReadSection = ' c.ID '   
--END

--IF (@TheRunLevel = 9)
--BEGIN
--		set @GroupBy = 'cs.Name '
--	set @ReadSection = ' cs.ID '   
--END

set @SelectPart = 'Select  '

if @ReportType = 'P' or @ReportType = 'M'
begin
  if @TotalsPerMonth = 'Y' 
    set @SelectPart = @SelectPart+ ' a.Prodmonth, '
	else 
	set @SelectPart = @SelectPart+ ' 0 Prodmonth, '
end  

if @ReportType = 'D'
begin
    set @SelectPart = @SelectPart+ ' 0 Prodmonth, ' 
end  

--SET the grouping/ selection Column's   

IF @NAME_5 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_5, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_5, ' 
  
IF @NAME_4 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_4, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_4, ' 
  
IF @NAME_3 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_3, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_3, ' 
  
IF @NAME_2 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_2, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_2, ' 
  
IF @NAME_1 = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME_1, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME_1, ' 
  
IF @NAME = 'Y' 
  set @SelectPart = @SelectPart+ ' NAME, '  
else
  set @SelectPart = @SelectPart+ ' Null NAME, '   

IF @Workplace = 'Y' 
  set @SelectPart = @SelectPart+ ' a.Workplace, '  
else
  set @SelectPart = @SelectPart+ ' Null workplace, ' 

set @SelectPart = @SelectPart+'	
			sum(Plan_Stope) Plan_Stope, 
			sum(Plan_Dev) Plan_Dev, 
			Stp_Plan_FL = cast(sum(Stp_Plan_FL) as numeric(18,4)),
			Stp_Plan_FLReef = cast(sum(Stp_Plan_FLReef) as numeric(18,4)),
			Stp_Plan_FLWaste = cast(sum(Stp_Plan_FLWaste) as numeric(18,4)),
			Stp_Plan_Sqm = cast(sum(Stp_Plan_Sqm) as numeric(18,4)),
			Stp_Plan_SqmReef = cast(sum(Stp_Plan_SqmReef) as numeric(10,1)),
			Stp_Plan_SqmWaste = cast(sum(Stp_Plan_SqmWaste) as numeric(18,4)),  
			Stp_Plan_Adv = cast(sum(Stp_Plan_Adv) as numeric(18,4)),
			Stp_Plan_AdvReef = cast(sum(Stp_Plan_AdvReef) as numeric(10,1)),
			Stp_Plan_AdvWaste = cast(sum(Stp_Plan_AdvWaste) as numeric(18,4)),   
			Stp_Plan_SqmSW = cast(sum(Stp_Plan_SqmSW) as numeric(18,4)),
			Stp_Plan_SW = sum(case when Stp_Plan_SqmReef > 0 then Stp_Plan_SqmSW / Stp_Plan_SqmReef else 0 end),
			Stp_Plan_SqmCW = cast(sum(Stp_Plan_SqmCW) as numeric(18,4)),
			Stp_Plan_CW = sum(case when Stp_Plan_SqmReef > 0 then Stp_Plan_SqmCW / Stp_Plan_SqmReef else 0 end),
			Stp_Plan_SqmCmgt = cast(sum(Stp_Plan_SqmCmgt) as numeric(18,4)),
			Stp_Plan_SqmCmgtTotal = cast(sum(Stp_Plan_SqmCmgtTotal) as numeric(18,4)),
			Stp_Plan_Cmgt = sum(case when Stp_Plan_SqmReef > 0 then Stp_Plan_SqmCmgt / Stp_Plan_SqmReef else 0 end),
			Stp_Plan_CmgtTotal = sum(case when Stp_Plan_Sqm > 0 then Stp_Plan_SqmCmgtTotal / Stp_Plan_Sqm else 0 end),
			Stp_Plan_GT = sum(case when Stp_Plan_TonsReef > 0 then Stp_Plan_Kg / Stp_Plan_TonsReef else 0 end),
			Stp_Plan_GTTotal = sum(case when Stp_Plan_Tons > 0 then Stp_Plan_Kg / Stp_Plan_Tons else 0 end),
			Stp_Plan_Tons = cast(sum(Stp_Plan_Tons) as numeric(18,4)),   
			Stp_Plan_TonsReef = cast(sum(Stp_Plan_TonsReef)as numeric(18,4)),
			Stp_Plan_TonsWaste = cast(sum(Stp_Plan_TonsWaste) as numeric(18,4)),
			Stp_Plan_Kg = cast(sum(Stp_Plan_Kg)  as numeric(18,6)),
			Stp_Plan_Cubics = cast(sum(Stp_Plan_Cubics) as numeric(18,6)),
			Stp_Plan_CubicTons = cast(sum(Stp_Plan_CubicTons) as numeric(18,6)),
			Stp_Plan_CubicGT = sum(case when Stp_Plan_CubicTons > 0 then Stp_Plan_CubicGrams / Stp_Plan_CubicTons else 0 end),
			Stp_Plan_CubicGrams = cast(sum(Stp_Plan_CubicGrams) as numeric(18,6)),

			Dev_Plan_AdvReef = sum(Dev_Plan_AdvReef),
			Dev_Plan_AdvWaste = sum(Dev_Plan_AdvWaste),
			Dev_Plan_Primm = sum(Dev_Plan_Primm),
			Dev_Plan_Secm = sum(Dev_Plan_Secm), 
			Dev_Plan_Adv = sum(Dev_Plan_Adv),
			Dev_Plan_TonsReef = sum(Dev_Plan_TonsReef),
			Dev_Plan_TonsWaste = sum(Dev_Plan_TonsWaste), 
			Dev_Plan_Tons = sum( Dev_Plan_Tons),
			Dev_Plan_KG = sum(Dev_Plan_KG),
			Dev_Plan_AdvEH = sum(Dev_Plan_AdvEH), 
			Dev_Plan_EH = sum(case when Dev_Plan_Adv > 0 then Dev_Plan_AdvEH / Dev_Plan_Adv else 0 end),
			Dev_Plan_AdvEW = sum(Dev_Plan_AdvEW),
			Dev_Plan_EW = sum(case when Dev_Plan_Adv > 0 then Dev_Plan_AdvEW / Dev_Plan_Adv else 0 end),
			Dev_Plan_AdvCmgt = sum(Dev_Plan_AdvCmgt),
			Dev_Plan_AdvCmgtTotal = sum(Dev_Plan_AdvCmgtTotal),
			Dev_Plan_Cmgt = sum(case when Dev_Plan_AdvReef > 0 then Dev_Plan_AdvCmgt / Dev_Plan_AdvReef else 0 end),
			Dev_Plan_CmgtTotal = sum(case when Dev_Plan_Adv > 0 then Dev_Plan_AdvCmgtTotal / Dev_Plan_Adv else 0 end),
			Dev_Plan_GT = sum(case when Dev_Plan_TonsReef > 0 then Dev_Plan_KG / Dev_Plan_TonsReef else 0 end),
			Dev_Plan_GTTotal = sum(case when Dev_Plan_Tons > 0 then Dev_Plan_KG / Dev_Plan_Tons else 0 end),
			Dev_Plan_Cubics = Sum(Dev_Plan_Cubics), 
			Dev_Plan_CubicGT = case when sum(Dev_Plan_CubicTons) = 0 then 0
									else sum(Dev_Plan_CubicGrams) / sum(Dev_Plan_CubicTons) end,
			Dev_Plan_CubicTons = Sum(Dev_Plan_CubicTons), 
			Dev_Plan_CubicGrams = Sum(Dev_Plan_CubicGrams) / 1000, 
			Dev_Plan_Labour = Sum(Dev_Plan_Labour), 
			Dev_Plan_ShftInfo = Sum(Dev_Plan_ShftInfo), 
			Dev_Plan_DrillRig = max(Dev_Plan_DrillRig), 

			Stp_PPlan_Sqm = cast(Sum(Stp_PPlan_Sqm) as numeric(18,4)),  
			Stp_PPlan_SqmReef = cast(Sum(Stp_PPlan_SqmReef) as numeric(18,1)),
			Stp_PPlan_SqmWaste = cast(Sum(Stp_PPlan_SqmWaste) as numeric(18,4)),
			Stp_PPlan_Adv = cast(Sum(Stp_PPlan_Adv) as numeric(18,4)),  
			Stp_PPlan_AdvReef = cast(Sum(Stp_PPlan_AdvReef) as numeric(18,1)),
			Stp_PPlan_AdvWaste = cast(Sum(Stp_PPlan_AdvWaste) as numeric(18,4)),
			Stp_PPlan_SqmSW = cast(Sum(Stp_PPlan_SqmSW) as numeric(18,4)),
			Stp_PPlan_SW = sum(case when Stp_PPlan_SqmReef > 0 then Stp_PPlan_SqmSW / Stp_PPlan_SqmReef else 0 end),
			Stp_PPlan_SqmCW = cast(Sum(Stp_PPlan_SqmCW) as numeric(18,4)),  
			Stp_PPlan_CW = sum(case when Stp_PPlan_SqmReef > 0 then Stp_PPlan_SqmCW / Stp_PPlan_SqmReef else 0 end),
			Stp_PPlan_SqmCmgt = cast(Sum(Stp_PPlan_SqmCmgt) as numeric(18,4)),
			Stp_PPlan_SqmCmgtTotal = cast(Sum(Stp_PPlan_SqmCmgtTotal) as numeric(18,4)),
			Stp_PPlan_Cmgt = sum(case when Stp_PPlan_SqmReef > 0 then Stp_PPlan_SqmCmgt / Stp_PPlan_SqmReef else 0 end),
			Stp_PPlan_CmgtTotal = sum(case when Stp_PPlan_Sqm > 0 then Stp_PPlan_SqmCmgtTotal / Stp_PPlan_Sqm else 0 end),
			Stp_PPlan_GT = sum(case when Stp_PPlan_TonsReef > 0 then Stp_PPlan_Kg / Stp_PPlan_TonsReef else 0 end),
			Stp_PPlan_GTTotal = sum(case when Stp_PPlan_Tons > 0 then Stp_PPlan_Kg / Stp_PPlan_Tons else 0 end),
			Stp_PPlan_FL = cast(sum(Stp_PPlan_FL) as numeric(18,4)),
			Stp_PPlan_FLReef = cast(sum(Stp_PPlan_FLReef) as numeric(18,4)),
			Stp_PPlan_FLWaste = cast(sum(Stp_PPlan_FLWaste) as numeric(18,4)),
			Stp_PPlan_Tons = cast(Sum(Stp_PPlan_Tons) as decimal(18,4)),
			Stp_PPlan_TonsReef = cast(Sum(Stp_PPlan_TonsReef) as decimal(18,4)),   
			Stp_PPlan_TonsWaste = cast(Sum(Stp_PPlan_TonsWaste) as decimal(18,4)),
			Stp_PPlan_KG = cast(Sum(Stp_PPlan_KG) as decimal(18,6)),
			Stp_PPlan_Cubics = cast(sum(Stp_PPlan_Cubics) as numeric(18,6)),
			Stp_PPlan_CubicTons = cast(sum(Stp_PPlan_CubicTons) as numeric(18,6)),
			Stp_PPlan_CubicGT = sum(case when Stp_PPlan_CubicTons > 0 then Stp_PPlan_CubicGrams / Stp_PPlan_CubicTons else 0 end),
			Stp_PPlan_CubicGrams = cast(sum(Stp_PPlan_CubicGrams) as numeric(18,6)),

			Dev_PPlan_AdvReef = sum(Dev_PPlan_AdvReef), 
			Dev_PPlan_AdvWaste = sum(Dev_PPlan_AdvWaste),
			Dev_PPlan_Primm = sum(Dev_PPlan_Primm),
			Dev_PPlan_Secm = sum(Dev_PPlan_Secm), 
			Dev_PPlan_Adv = sum(Dev_PPlan_Adv),
			Dev_PPlan_TonsReef = sum(Dev_PPlan_TonsReef), 
			Dev_PPlan_TonsWaste = sum(Dev_PPlan_TonsWaste), 
			Dev_PPlan_Tons = sum(Dev_PPlan_Tons),
			Dev_PPlan_KG = sum(Dev_PPlan_KG),
			Dev_PPlan_AdvEH = sum(Dev_PPlan_AdvEH), 
			Dev_PPlan_EH = sum(case when Dev_PPlan_Adv > 0 then Dev_PPlan_AdvEH / Dev_PPlan_Adv else 0 end),
			Dev_PPlan_AdvEW = sum(Dev_PPlan_AdvEW),
			Dev_PPlan_EW = sum(case when Dev_PPlan_Adv > 0 then Dev_PPlan_AdvEW / Dev_PPlan_Adv else 0 end),
			Dev_PPlan_AdvCmgt = sum(Dev_PPlan_AdvCmgt),
			Dev_PPlan_AdvCmgtTotal = sum(Dev_PPlan_AdvCmgtTotal),
			Dev_PPlan_Cmgt = sum(case when Dev_PPlan_AdvReef > 0 then Dev_PPlan_AdvCmgt / Dev_PPlan_AdvReef else 0 end),
			Dev_PPlan_CmgtTotal = sum(case when Dev_PPlan_Adv > 0 then Dev_PPlan_AdvCmgtTotal / Dev_PPlan_Adv else 0 end),
			Dev_PPlan_GT = sum(case when Dev_PPlan_TonsReef > 0 then Dev_PPlan_KG / Dev_PPlan_TonsReef else 0 end),
			Dev_PPlan_GTTotal = sum(case when Dev_PPlan_Tons > 0 then Dev_PPlan_KG / Dev_Plan_Tons else 0 end),
			Dev_PPlan_Cubics = Sum(Dev_PPlan_Cubics), 
			Dev_PPlan_CubicGT = case when sum(Dev_PPlan_CubicTons) = 0 then 0
									else sum(Dev_PPlan_CubicGrams) / sum(Dev_PPlan_CubicTons) end,
			Dev_PPlan_CubicTons = Sum(Dev_PPlan_CubicTons), 
			Dev_PPlan_CubicGrams = Sum(Dev_PPlan_CubicGrams) / 1000, 
			Dev_PPlan_Labour = Sum(Dev_PPlan_Labour), 
			Dev_PPlan_ShftInfo = Sum(Dev_PPlan_ShftInfo), 
			Dev_PPlan_DrillRig = Sum(Dev_PPlan_DrillRig), '

		set @SelectPart1 = '
			Stp_LPlan_FL = cast(Sum(Stp_LPlan_FL) as numeric(18,4)),
			Stp_LPlan_FLReef = cast(Sum(Stp_LPlan_FLReef) as numeric(18,4)),
			Stp_LPlan_FLWaste = cast(Sum(Stp_LPlan_FLWaste) as numeric(18,4)),
			Stp_LPlan_Sqm = cast(Sum(Stp_LPlan_Sqm) as numeric(18,4)), 
			Stp_LPlan_SqmReef = cast(Sum(Stp_LPlan_SqmReef) as numeric(18,4)),
			Stp_LPlan_SqmWaste = cast(Sum(Stp_LPlan_SqmWaste) as numeric(18,4)),
			Stp_LPlan_Adv = cast(Sum(Stp_LPlan_Adv) as numeric(18,4)),
			Stp_LPlan_AdvReef= cast(Sum(Stp_LPlan_AdvReef) as numeric(18,4)),
			Stp_LPlan_AdvWaste = cast(Sum(Stp_LPlan_AdvWaste) as numeric(18,4)),
			Stp_LPlan_SqmSW = cast(Sum(Stp_LPlan_SqmSW) as numeric(18,4)),
			Stp_LPlan_SW = sum(case when Stp_LPlan_SqmReef > 0 then Stp_LPlan_SqmSW / Stp_LPlan_SqmReef else 0 end),
			Stp_LPlan_SqmCW = cast(Sum(Stp_LPlan_SqmCW) as numeric(18,4)),
			Stp_LPlan_CW = sum(case when Stp_LPlan_SqmReef > 0 then Stp_LPlan_SqmCW / Stp_LPlan_SqmReef else 0 end),
			Stp_LPlan_SqmCmgt = cast(Sum(Stp_LPlan_SqmCmgt) as numeric(18,4)),
			Stp_LPlan_SqmCmgtTotal = cast(Sum(Stp_LPlan_SqmCmgtTotal) as numeric(18,4)),
			Stp_LPlan_Cmgt = sum(case when Stp_LPlan_SqmReef > 0 then Stp_LPlan_SqmCmgt / Stp_LPlan_SqmReef else 0 end),
			Stp_LPlan_CmgtTotal = sum(case when Stp_LPlan_Sqm > 0 then Stp_LPlan_SqmCmgtTotal / Stp_LPlan_Sqm else 0 end),
			Stp_LPlan_GT = sum(case when Stp_LPlan_TonsReef > 0 then Stp_LPlan_Kg / Stp_LPlan_TonsReef else 0 end),
			Stp_LPlan_GTTotal = sum(case when Stp_LPlan_Tons > 0 then Stp_LPlan_Kg / Stp_LPlan_Tons else 0 end),
			Stp_LPlan_Tons = cast(Sum(Stp_LPlan_Tons) as numeric(18,4)),
			Stp_LPlan_TonsReef = cast(Sum(Stp_LPlan_TonsReef) as numeric(18,4)),
			Stp_LPlan_TonsWaste = cast(Sum(Stp_LPlan_TonsWaste) as numeric(18,4)),
			Stp_LPlan_Kg = cast(Sum(Stp_LPlan_Kg) as numeric(18,4)),
			Stp_LPlan_Cubics = cast(sum(Stp_LPlan_Cubics)  as numeric(18,6)),
			Stp_LPlan_CubicTons = cast(sum(Stp_LPlan_CubicTons)  as numeric(18,6)),
			Stp_LPlan_CubicGT = sum(case when Stp_LPlan_CubicTons > 0 then Stp_LPlan_CubicGrams / Stp_LPlan_CubicTons else 0 end),
			Stp_LPlan_CubicGrams = cast(sum(Stp_LPlan_CubicGrams)  as numeric(18,6)),

			Dev_LPlan_AdvReef = sum(Dev_LPlan_AdvReef),
			Dev_LPlan_AdvWaste = sum(Dev_LPlan_AdvWaste),
			Dev_LPlan_Primm = sum(Dev_LPlan_Primm),
			Dev_LPlan_Secm = sum(Dev_LPlan_Secm), 
			Dev_LPlan_Adv = sum(Dev_LPlan_Adv),
			Dev_LPlan_TonsReef = sum(Dev_LPlan_TonsReef),
			Dev_LPlan_TonsWaste = sum(Dev_LPlan_TonsWaste), 
			Dev_LPlan_Tons = sum( Dev_LPlan_Tons),
			Dev_LPlan_KG = sum(Dev_LPlan_KG),
			Dev_LPlan_AdvEH = sum(Dev_LPlan_AdvEH), 
			Dev_LPlan_EH = sum(case when Dev_LPlan_Adv > 0 then Dev_LPlan_AdvEH / Dev_LPlan_Adv else 0 end),
			Dev_LPlan_AdvEW = sum(Dev_LPlan_AdvEW),
			Dev_LPlan_EW = sum(case when Dev_LPlan_Adv > 0 then Dev_LPlan_AdvEW / Dev_LPlan_Adv else 0 end),
			Dev_LPlan_AdvCmgt = sum(Dev_LPlan_AdvCmgt),
			Dev_LPlan_AdvCmgtTotal = sum(Dev_LPlan_AdvCmgtTotal),
			Dev_LPlan_Cmgt = sum(case when Dev_LPlan_AdvReef > 0 then Dev_LPlan_AdvCmgt / Dev_LPlan_AdvReef else 0 end),
			Dev_LPlan_CmgtTotal = sum(case when Dev_LPlan_Adv > 0 then Dev_LPlan_AdvCmgtTotal / Dev_LPlan_Adv else 0 end),
			Dev_LPlan_GT = sum(case when Dev_LPlan_TonsReef > 0 then Dev_LPlan_KG / Dev_LPlan_TonsReef else 0 end),
			Dev_LPlan_GTTotal = sum(case when Dev_LPlan_Tons > 0 then Dev_LPlan_KG / Dev_LPlan_Tons else 0 end),
			Dev_LPlan_Cubics = Sum(Dev_LPlan_Cubics), 
			Dev_LPlan_CubicGT = case when sum(Dev_LPlan_CubicTons) = 0 then 0
									else sum(Dev_LPlan_CubicGrams) / sum(Dev_LPlan_CubicTons) end,
			Dev_LPlan_CubicTons = Sum(Dev_LPlan_CubicTons), 
			Dev_LPlan_CubicGrams = Sum(Dev_LPlan_CubicGrams) / 1000, 
			Dev_LPlan_Labour = Sum(Dev_LPlan_Labour), 
			Dev_LPlan_ShftInfo = Sum(Dev_LPlan_ShftInfo), 
			Dev_LPlan_DrillRig = Sum(Dev_LPlan_DrillRig), 

			Stp_LPPlan_FL = cast(Sum(Stp_LPPlan_FL) as numeric(18,4)),
			Stp_LPPlan_FLReef = cast(Sum(Stp_LPPlan_FLReef) as numeric(18,4)),
			Stp_LPPlan_FLWaste = cast(Sum(Stp_LPPlan_FLWaste) as numeric(18,4)),
			Stp_LPPlan_Sqm = cast(Sum(Stp_LPPlan_Sqm) as numeric(18,4)), 
			Stp_LPPlan_SqmReef = cast(Sum(Stp_LPPlan_SqmReef) as numeric(18,4)),
			Stp_LPPlan_SqmWaste = cast(Sum(Stp_LPPlan_SqmWaste)as numeric(18,4)),
			Stp_LPPlan_Adv = cast(Sum(Stp_LPPlan_Adv) as numeric(18,4)),
			Stp_LPPlan_AdvReef = cast(Sum(Stp_LPPlan_AdvReef) as numeric(18,4)),
			Stp_LPPlan_AdvWaste = cast(Sum(Stp_LPPlan_AdvWaste) as numeric(18,4)),
			Stp_LPPlan_SqmSW = cast(Sum(Stp_LPPlan_SqmSW) as numeric(18,4)),
			Stp_LPPlan_SW = sum(case when Stp_LPPlan_SqmReef > 0 then Stp_LPPlan_SqmSW / Stp_LPPlan_SqmReef else 0 end),
			Stp_LPPlan_SqmCW = cast(Sum(Stp_LPPlan_SqmCW) as numeric(18,4)),
			Stp_LPPlan_CW = sum(case when Stp_LPPlan_SqmReef > 0 then Stp_LPPlan_SqmCW / Stp_LPPlan_SqmReef else 0 end),
			Stp_LPPlan_SqmCmgt = cast(Sum(Stp_LPPlan_SqmCmgt) as numeric(18,4)),
			Stp_LPPlan_SqmCmgtTotal = cast(Sum(Stp_LPPlan_SqmCmgtTotal) as numeric(18,4)),
			Stp_LPPlan_Cmgt = sum(case when Stp_LPPlan_SqmReef > 0 then Stp_LPPlan_SqmCmgt / Stp_LPPlan_SqmReef else 0 end),
			Stp_LPPlan_CmgtTotal = sum(case when Stp_LPPlan_Sqm > 0 then Stp_LPPlan_SqmCmgtTotal / Stp_LPPlan_Sqm else 0 end),
			Stp_LPPlan_GT = sum(case when Stp_LPPlan_TonsReef > 0 then Stp_LPPlan_Kg / Stp_LPPlan_TonsReef else 0 end),
			Stp_LPPlan_GTTotal = sum(case when Stp_LPPlan_Tons > 0 then Stp_LPPlan_Kg / Stp_LPPlan_Tons else 0 end),
			Stp_LPPlan_Tons = cast(Sum(Stp_LPPlan_Tons) as numeric(18,4)),
			Stp_LPPlan_TonsReef = cast(Sum(Stp_LPPlan_TonsReef) as numeric(18,4)),
			Stp_LPPlan_TonsWaste = cast(Sum(Stp_LPPlan_TonsWaste) as numeric(18,4)),
			Stp_LPPlan_Kg = cast(Sum(Stp_LPPlan_Kg) as numeric(18,4)),
			Stp_LPPlan_Cubics = cast(sum(Stp_LPPlan_Cubics) as numeric(18,6)),
			Stp_LPPlan_CubicTons = cast(sum(Stp_LPPlan_CubicTons) as numeric(18,6)),
			Stp_LPPlan_CubicGT = sum(case when Stp_LPPlan_CubicTons > 0 then Stp_LPPlan_CubicGrams / Stp_LPPlan_CubicTons else 0 end),
			Stp_LPPlan_CubicGrams = cast(sum(Stp_LPPlan_CubicGrams) as numeric(18,6)),

			Dev_LPPlan_AdvReef = sum(Dev_LPPlan_AdvReef), 
			Dev_LPPlan_AdvWaste = sum(Dev_LPPlan_AdvWaste),
			Dev_LPPlan_Primm = sum(Dev_LPPlan_Primm),
			Dev_LPPlan_Secm = sum(Dev_LPPlan_Secm), 
			Dev_LPPlan_Adv = sum(Dev_LPPlan_Adv),
			Dev_LPPlan_TonsReef = sum(Dev_LPPlan_TonsReef), 
			Dev_LPPlan_TonsWaste = sum(Dev_LPPlan_TonsWaste), 
			Dev_LPPlan_Tons = sum(Dev_LPPlan_Tons),
			Dev_LPPlan_KG = sum(Dev_LPPlan_KG),
			Dev_LPPlan_AdvEH = sum(Dev_LPPlan_AdvEH), 
			Dev_LPPlan_EH = sum(case when Dev_LPPlan_Adv > 0 then Dev_LPPlan_AdvEH / Dev_LPPlan_Adv else 0 end),
			Dev_LPPlan_AdvEW = sum(Dev_LPPlan_AdvEW),
			Dev_LPPlan_EW = sum(case when Dev_LPPlan_Adv > 0 then Dev_LPPlan_AdvEW / Dev_LPPlan_Adv else 0 end),
			Dev_LPPlan_AdvCmgt = sum(Dev_LPPlan_AdvCmgt),
			Dev_LPPlan_AdvCmgtTotal = sum(Dev_LPPlan_AdvCmgtTotal),
			Dev_LPPlan_Cmgt = sum(case when Dev_LPPlan_AdvReef > 0 then Dev_LPPlan_AdvCmgt / Dev_LPPlan_AdvReef else 0 end),
			Dev_LPPlan_CmgtTotal = sum(case when Dev_LPPlan_Adv > 0 then Dev_LPPlan_AdvCmgtTotal / Dev_LPPlan_Adv else 0 end),
			Dev_LPPlan_GT = sum(case when Dev_LPPlan_TonsReef > 0 then Dev_LPPlan_KG / Dev_LPPlan_TonsReef else 0 end),
			Dev_LPPlan_GTTotal = sum(case when Dev_LPPlan_Tons > 0 then Dev_LPPlan_KG / Dev_LPPlan_Tons else 0 end),
			Dev_LPPlan_Cubics = Sum(Dev_LPPlan_Cubics), 
			Dev_LPPlan_CubicGT = case when sum(Dev_LPPlan_CubicTons) = 0 then 0
									else sum(Dev_LPPlan_CubicGrams) / sum(Dev_LPPlan_CubicTons) end,
			Dev_LPPlan_CubicTons = Sum(Dev_LPPlan_CubicTons), 
			Dev_LPPlan_CubicGrams = Sum(Dev_LPPlan_CubicGrams) / 1000, 
			Dev_LPPlan_Labour = Sum(Dev_LPPlan_Labour), 
			Dev_LPPlan_ShftInfo = Sum(Dev_LPPlan_ShftInfo), 
			Dev_LPPlan_DrillRig = Sum(Dev_LPPlan_DrillRig), '

		set @SelectPart2 = '
			Stp_Book_FL = cast(sum(Stp_Book_FL) as numeric(18,4)),
			Stp_Book_Sqm = cast(Sum(Stp_Book_Sqm) as numeric(18,4)),   
			Stp_Book_SqmReef = cast(Sum(Stp_Book_SqmReef) as numeric(18,1)),
			Stp_Book_SqmWaste = cast(Sum(Stp_Book_SqmWaste) as numeric(18,4)),
			Stp_Book_Adv = cast(Sum(Stp_Book_Adv) as numeric(18,4)),   
			Stp_Book_AdvReef = cast(Sum(Stp_Book_AdvReef) as numeric(18,1)),
			Stp_Book_AdvWaste = cast(Sum(Stp_Book_AdvWaste) as numeric(18,4)),			
			Stp_Book_SqmSW = cast(sum(Stp_Book_SqmSW) as numeric(18,4)), 
			Stp_Book_SW = sum(case when Stp_Book_SqmReef > 0 then Stp_Book_SqmSW / Stp_Book_SqmReef else 0 end),
			Stp_Book_SqmCW = cast(sum(Stp_Book_SqmCW) as numeric(18,4)),
			Stp_Book_CW = sum(case when Stp_Book_SqmReef > 0 then Stp_Book_SqmCW / Stp_Book_SqmReef else 0 end),
			Stp_Book_SqmCmgt = cast(sum(Stp_Book_SqmCmgt) as numeric(18,4)),
			Stp_Book_SqmCmgtTotal = cast(sum(Stp_Book_SqmCmgtTotal) as numeric(18,4)),
			Stp_Book_Cmgt = sum(case when Stp_Book_SqmReef > 0 then Stp_Book_SqmCmgt / Stp_Book_SqmReef else 0 end),
			Stp_Book_CmgtTotal = sum(case when Stp_Book_Sqm > 0 then Stp_Book_SqmCmgtTotal / Stp_Book_Sqm else 0 end),
			Stp_Book_GT = sum(case when Stp_Book_TonsReef > 0 then Stp_Book_Kg / Stp_Book_TonsReef else 0 end),
			Stp_Book_GTTotal = sum(case when Stp_Book_Tons > 0 then Stp_Book_Kg / Stp_Book_Tons else 0 end),
			Stp_Book_Tons = cast(Sum(Stp_Book_Tons) as numeric(18,4)),
			Stp_Book_TonsReef = cast(Sum(Stp_Book_TonsReef) as numeric(18,4)),  
			Stp_Book_TonsWaste = cast(Sum(Stp_Book_TonsWaste) as numeric(18,4)),
			Stp_Book_Kg = cast(Sum(Stp_Book_Kg) as numeric(18,4)),
			Stp_Book_Cubics = cast(sum(Stp_Book_Cubics) as numeric(18,6)),
			Stp_Book_CubicTons = cast(sum(Stp_Book_CubicTons) as numeric(18,6)),
			Stp_Book_CubicGT = sum(case when Stp_Book_CubicTons > 0 then Stp_Book_CubicGrams / Stp_Book_CubicTons else 0 end),
			Stp_Book_CubicGrams = cast(sum(Stp_Book_CubicGrams) as numeric(18,6)), 

			Dev_Book_AdvReef = sum(Dev_Book_AdvReef), 
			Dev_Book_AdvWaste = sum(Dev_Book_AdvWaste),
			Dev_Book_Primm = sum(Dev_Book_Primm),
			Dev_Book_Secm = sum(Dev_Book_Secm), 
			Dev_Book_Adv = sum(Dev_Book_Adv),
			Dev_Book_TonsReef = sum(Dev_Book_TonsReef),
			Dev_Book_TonsWaste = sum(Dev_Book_TonsWaste), 
			Dev_Book_Tons = sum(Dev_Book_Tons),
			Dev_Book_KG = sum(Dev_Book_KG),
			Dev_Book_AdvEH = sum(Dev_Book_AdvEH), 
			Dev_Book_EH = sum(case when Dev_Book_Adv > 0 then Dev_Book_AdvEH / Dev_Book_Adv else 0 end),
			Dev_Book_AdvEW = sum(Dev_Book_AdvEW),
			Dev_Book_EW = sum(case when Dev_Book_Adv > 0 then Dev_Book_AdvEW / Dev_Book_Adv else 0 end),
			Dev_Book_AdvCmgt = sum(Dev_Book_AdvCmgt), 
			Dev_Book_AdvCmgtTotal = sum(Dev_Book_AdvCmgtTotal),
			Dev_Book_Cmgt = sum(case when Dev_Book_AdvReef > 0 then Dev_Book_AdvCmgt / Dev_Book_AdvReef else 0 end),
			Dev_Book_CmgtTotal = sum(case when Dev_Book_Adv > 0 then Dev_Book_AdvCmgtTotal / Dev_Book_Adv else 0 end),
			Dev_Book_GT = sum(case when Dev_Book_TonsReef > 0 then Dev_Book_KG / Dev_Book_TonsReef else 0 end),
			Dev_Book_GTTotal = sum(case when Dev_Book_Tons > 0 then Dev_Book_KG / Dev_Book_Tons else 0 end),
			Dev_Book_Cubics = Sum(Dev_Book_Cubics), 
			Dev_Book_CubicGT = case when sum(Dev_Book_CubicTons) = 0 then 0
									else sum(Dev_Book_CubicGrams) / sum(Dev_Book_CubicTons) end,
			Dev_Book_CubicTons = Sum(Dev_Book_CubicTons), 
			Dev_Book_CubicGrams = Sum(Dev_Book_CubicGrams) / 1000, 
			Dev_Book_Labour = Sum(0), 
			Dev_Book_ShftInfo = Sum(0), 
			Dev_Book_DrillRig = Sum(0), 

			Stp_Meas_Sqm = cast(sum(Stp_Meas_Sqm) as numeric(18,4)),
			Stp_Meas_SqmReef = cast(sum(Stp_Meas_SqmReef) as numeric(18,1)),
			Stp_Meas_SqmWaste = cast(sum(Stp_Meas_SqmWaste) as numeric(18,4)),
			Stp_Meas_FL = cast(sum(Stp_Meas_FL) as numeric(18,4)),
			Stp_Meas_FLReef = cast(sum(Stp_Meas_FLReef) as numeric(18,4)),
			Stp_Meas_FLWaste = cast(sum(Stp_Meas_FLWaste) as numeric(18,4)),
			Stp_Meas_SW = sum(case when Stp_Meas_Sqm > 0 then Stp_Meas_SqmSW / Stp_Meas_Sqm else 0 end),
			Stp_Meas_CW = sum(case when Stp_Meas_Sqm > 0 then Stp_Meas_SqmCW / Stp_Meas_Sqm else 0 end),
			Stp_Meas_SqmCMGT = cast(sum(Stp_Meas_SqmCMGT) as numeric(18,4)),
			Stp_Meas_SqmCMGTTotal = cast(sum(Stp_Meas_SqmCMGTTotal) as numeric(18,4)),
			Stp_Meas_Cmgt = sum(case when Stp_Meas_SqmReef > 0 then Stp_Meas_SqmCmgt / Stp_Meas_SqmReef else 0 end),
			Stp_Meas_CmgtTotal = sum(case when Stp_Meas_Sqm > 0 then Stp_Meas_SqmCmgtTotal / Stp_Meas_Sqm else 0 end),
			Stp_Meas_GT = sum(case when Stp_Meas_TonsReef > 0 then Stp_Meas_Kg / Stp_Meas_TonsReef else 0 end),
			Stp_Meas_GTTotal = sum(case when Stp_Meas_Tons > 0 then Stp_Meas_Kg / Stp_Meas_Tons else 0 end),
			Stp_Meas_SqmSW = cast(sum(Stp_Meas_SqmSW)  as numeric(18,4)),
			Stp_Meas_SqmCW = cast(sum(Stp_Meas_SqmCW)  as numeric(18,4)),
			Stp_Meas_Tons = cast(sum(Stp_Meas_Tons) as numeric(18,4)),
			Stp_Meas_TonsReef = cast(sum(Stp_Meas_TonsReef) as numeric(18,4)),   
			Stp_Meas_TonsWaste = cast(sum(Stp_Meas_TonsWaste) as numeric(18,4)),
			Stp_Meas_Kg = cast(sum(Stp_Meas_Kg) as numeric(18,4)), 
			Stp_Meas_Cubics = cast(sum(Stp_Meas_Cubics) as numeric(18,6)),
			Stp_Meas_CubicTons = cast(sum(Stp_Meas_CubicTons) as numeric(18,6)),
			Stp_Meas_CubicGT = sum(case when Stp_Meas_CubicTons > 0 then Stp_Meas_CubicGrams / Stp_Meas_CubicTons else 0 end),
			Stp_Meas_CubicGrams = cast(sum(Stp_Meas_CubicGrams) as numeric(18,6)), 
			
			Dev_Meas_AdvReef = sum(Dev_Meas_AdvReef), 
			Dev_Meas_AdvWaste = sum(Dev_Meas_AdvWaste),
			Dev_Meas_Primm = sum(Dev_Meas_Primm),
			Dev_Meas_Secm = sum(Dev_Meas_Secm), 
			Dev_Meas_Adv = sum(Dev_Meas_Adv),
			Dev_Meas_TonsReef = sum(Dev_Meas_TonsReef),
			Dev_Meas_TonsWaste = sum(Dev_Meas_TonsWaste), 
			Dev_Meas_Tons = cast(sum(Dev_Meas_Tons) as numeric(18,4)),
			Dev_Meas_KG = sum(Dev_Meas_KG),
			Dev_Meas_AdvEH = sum(Dev_Meas_AdvEH), 
			Dev_Meas_EH = sum(case when Dev_Meas_Adv > 0 then Dev_Meas_AdvEH / Dev_Meas_Adv else 0 end),
			Dev_Meas_AdvEW = sum(Dev_Meas_AdvEW),
			Dev_Meas_EW = sum(case when Dev_Meas_Adv > 0 then Dev_Meas_AdvEW / Dev_Meas_Adv else 0 end),
			Dev_Meas_AdvCmgt = sum(Dev_Meas_AdvCmgt),
			Dev_Meas_AdvCmgtTotal = sum(Dev_Meas_AdvCmgtTotal),
			Dev_Meas_Cmgt = sum(case when Dev_Meas_AdvReef > 0 then Dev_Meas_AdvCmgt / Dev_Meas_AdvReef else 0 end),
			Dev_Meas_CmgtTotal = sum(case when Dev_Meas_Adv > 0 then Dev_Meas_AdvCmgtTotal / Dev_Meas_Adv else 0 end),
			Dev_Meas_GT = sum(case when Dev_Meas_TonsReef > 0 then Dev_Meas_KG / Dev_Meas_TonsReef else 0 end),
			Dev_Meas_GTTotal = sum(case when Dev_Meas_Tons > 0 then Dev_Meas_KG / Dev_Meas_Tons else 0 end),
			Dev_Meas_Cubics = Sum(Dev_Meas_Cubics), 
			Dev_Meas_CubicGT = case when sum(Dev_Meas_CubicTons) = 0 then 0
									else sum(Dev_Meas_CubicGrams) / sum(Dev_Meas_CubicTons) end,
			Dev_Meas_CubicTons = Sum(Dev_Meas_CubicTons), 
			Dev_Meas_CubicGrams = Sum(Dev_Meas_CubicGrams) / 1000, '

		set @SelectPart3 = '	
			Stp_BPlan_FL = cast(sum(Stp_BPlan_FL) as numeric(18,4)),
			Stp_BPlan_Sqm = cast(sum(Stp_BPlan_Sqm) as numeric(18,4)),
			Stp_BPlan_SqmReef = cast(sum(Stp_BPlan_SqmReef) as numeric(18,1)),
			Stp_BPlan_SqmWaste = cast(sum(Stp_BPlan_SqmWaste) as numeric(18,4)),    
			Stp_BPlan_SqmSW = cast(sum(Stp_BPlan_SqmSW) as numeric(18,4)),
			Stp_BPlan_SqmCW = cast(sum(Stp_BPlan_SqmCW) as numeric(18,4)),
			Stp_BPlan_SqmCmgt = cast(sum(Stp_BPlan_SqmCmgt) as numeric(18,4)),
			Stp_BPlan_Tons = cast(sum(Stp_BPlan_Tons) as numeric(18,4)),   
			Stp_BPlan_TonsReef = cast(sum(Stp_BPlan_TonsReef) as numeric(18,4)),
			Stp_BPlan_TonsWaste = cast(sum(Stp_BPlan_Tons) as numeric(18,4)),
			Stp_BPlan_Kg = cast(sum(Stp_BPlan_Kg) as numeric(18,4)),
			Stp_BPlan_OSSqm = cast(sum(Stp_BPlan_OSSqm) as numeric(18,4)),
			Stp_BPlan_OSFSqm = cast(sum(Stp_BPlan_OSFSqm) as numeric(18,4)),
			Stp_BPlan_REEFFL = cast(sum(Stp_BPlan_REEFFL) as numeric(18,4)),
			Stp_BPlan_OSFL = cast(sum(Stp_BPlan_OSFL) as numeric(18,4)),    
			Stp_BPlan_CUBICS = cast(sum(Stp_BPlan_CUBICS) as numeric(18,4)),
			Stp_BPlan_SqmSWFAULT = cast(sum(Stp_BPlan_SqmSWFAULT) as numeric(18,4)),   
			Stp_BPlan_FaultTons = cast(sum(Stp_BPlan_FaultTons) as numeric(18,4)),
			Stp_BPlan_OSTons = cast(sum(Stp_BPlan_OSTons) as numeric(18,4)),
			Stp_BPlan_CUBICTons = cast(sum(Stp_BPlan_CUBICTons) as numeric(18,4)),

			Dev_BPlan_AdvReef = max(Dev_BPlan_AdvReef), 
			Dev_BPlan_AdvWaste = max(Dev_BPlan_AdvWaste),
			Dev_BPlan_Primm = max(Dev_BPlan_Primm),
			Dev_BPlan_Secm = max(Dev_BPlan_Secm), 
			Dev_BPlan_Adv = sum(Dev_BPlan_Adv),
			Dev_BPlan_TonsReef = sum(Dev_BPlan_TonsReef),
			Dev_BPlan_TonsWaste = sum(Dev_BPlan_TonsWaste), 
			Dev_BPlan_Tons = sum(Dev_BPlan_Tons),
			Dev_BPlan_KG = sum(Dev_BPlan_KG),
			Dev_BPlan_AdvEH = sum(Dev_BPlan_AdvEH), 
			Dev_BPlan_EH = sum(case when Dev_BPlan_Adv > 0 then Dev_BPlan_AdvEH / Dev_BPlan_Adv else 0 end),
			Dev_BPlan_AdvEW = sum(Dev_BPlan_AdvEW),
			Dev_BPlan_EW = sum(case when Dev_BPlan_Adv > 0 then Dev_BPlan_AdvEW / Dev_BPlan_Adv else 0 end),
			Dev_BPlan_AdvCmgt = sum(Dev_BPlan_AdvCmgt),
			Dev_BPlan_AdvCmgtTotal = sum(Dev_BPlan_AdvCmgtTotal),
			Dev_BPlan_Cubics = sum(Dev_BPlan_Cubics),
			Dev_BPlan_CubicTons = sum(Dev_BPlan_CubicTons), 
			Dev_BPlan_TonsCmgt = sum(Dev_BPlan_TonsCmgt),
			max(BusFlag) BusFlag,

			Abs_PlanMeas_Sqm = Abs(Sum(Stp_Plan_Sqm) - Sum(Stp_Meas_Sqm)) , 
			Abs_LockMeas_Sqm = Abs(Sum(Stp_LPlan_Sqm) - Sum(Stp_Meas_Sqm)), 
			Abs_PlanLock_Sqm = Abs(Sum(Stp_LPlan_Sqm) - Sum(Stp_Plan_Sqm)), 
			Abs_BookMeas_Sqm = Abs(Sum(Stp_Book_Sqm) - Sum(Stp_Meas_Sqm)), 
			Abs_PlanBook_Sqm = Abs(Sum(Stp_Book_Sqm) - Sum(Stp_Plan_Sqm)), 
			Abs_LockBook_Sqm = Abs(Sum(Stp_Book_Sqm) - Sum(Stp_LPlan_Sqm)), 

			ForeCast_SQM = sum(ForeCast_SQM),
			ForeCast_KG = sum(ForeCast_KG),
			ForeCast_Cmgt = case when sum(ForeCast_SQMDens) = 0 then 0 else
                    sum(ForeCast_Grams) * 100 / sum(ForeCast_SQMDens) end
		 from   
		 ('
IF (@ReportType = 'P') or (@ReportType = 'M')
BEGIN
	IF (@PlanMonth = 'Y') or (@PlanProg = 'Y') or (@Book = 'Y')
	BEGIN
		set @PlanSelect =  
			' select 
				sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
				sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
				sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME,'

		--IF (@TheLevel = 2)
		--	set @TheQuery1 = @TheQuery1 +''''' NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
		--		sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
		--		sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME,'  

		--IF (@TheLevel = 3)
		--	set @TheQuery1 = @TheQuery1 +'  
		--		'''' NAME_5, '''' NAME_4,sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
		--		sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME,' 

		--IF (@TheLevel = 4)
		--	set @TheQuery1 = @TheQuery1 +''''' NAME_5, '''' NAME_4, '''' NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
		--		sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME,' 
   
		--IF (@TheLevel = 5)
		--	set @TheQuery1 = @TheQuery1 +''''' NAME_5, '''' NAME_4,''''NAME_3, '''' NAME_2,
		--		sc.SectionID_1+'':''+sc.Name_1 NAME_1, sc.SectionID+'':''+sc.Name NAME,' 

		--IF (@TheLevel = 6)
		--	set @TheQuery1 = @TheQuery1 + ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1,
		--		sc.SectionID+'':''+sc.Name NAME,' 

		--IF (@TheLevel = 7)
		--	set @TheQuery1  = @TheQuery1 +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, ''Total Mine'' NAME_2,
		--		sc.Name NAME_1,cs.Name NAME, '    

		--IF (@TheLevel = 8)
		--	 set @TheQuery1  = @TheQuery1 +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,scc.Name NAME_1, sc.Name NAME, '    

		--IF (@TheLevel = 9)
		--	set @TheQuery1  = @TheQuery1 +  ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1, sc.Name NAME, ' 

		set @PlanSelect = @PlanSelect + '
			pm.Prodmonth, pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
			Plan_Stope = case when pm.Activity = 0 then 1 else 0 end, 
			Plan_Dev = case when pm.Activity = 1 then 1 else 0 end,
			Stp_Plan_FL = max(case when pm.Activity = 0 then isnull(pm.FL,0) else 0 end),
			Stp_Plan_FLReef = max(case when pm.Activity = 0 then isnull(pm.ReefFL,0) else 0 end),
			Stp_Plan_FLWaste = max(case when pm.Activity = 0 then isnull(pm.WasteFL,0) else 0 end),
			Stp_Plan_Sqm = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) else 0 end),
			Stp_Plan_SqmReef = max(case when pm.Activity = 0 then isnull(pm.ReefSqm,0) else 0 end),
			Stp_Plan_SqmWaste = max(case when pm.Activity = 0 then isnull(pm.wasteSqm,0) else 0 end), 
			Stp_Plan_Adv = sum(case when pm.Activity = 0 then isnull(pm.MetresAdvance,0) else 0 end),
			Stp_Plan_AdvReef = sum(case when pm.Activity = 0 then isnull(pm.ReefAdv,0) else 0 end), 
			Stp_Plan_AdvWaste = sum(case when pm.Activity = 0 then isnull(pm.WasteAdv,0) else 0 end),
			Stp_Plan_SqmSW = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.SW,0) else 0 end),
			Stp_Plan_SqmCW = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.CW,0) else 0 end),
			Stp_Plan_SqmCmgt = max(case when pm.Activity = 0 then isnull(pm.ReefSqm,0) * isnull(pm.cmgt,0) else 0 end),
			Stp_Plan_SqmCmgtTotal = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.cmgt,0) else 0 end),
			Stp_Plan_Tons = max(case when pm.Activity = 0 then isnull(pm.Tons,0) else 0 end), 
			Stp_Plan_TonsReef = max(case when pm.Activity = 0 then isnull(pm.ReefTons,0) else 0 end),
			Stp_Plan_TonsWaste = max(case when pm.Activity = 0 then isnull(pm.WasteTons,0) else 0 end),
			Stp_Plan_Kg = max(case when pm.Activity = 0 then isnull(pm.KG,0) / 1000 else 0 end), 
			Stp_Plan_Cubics = max(case when pm.Activity = 0 then isnull(pm.Cubics,0) else 0 end), 
			Stp_Plan_CubicTons = max(case when pm.Activity = 0 then isnull(pm.CubicsTons,0) else 0 end), 
			Stp_Plan_CubicGrams = max(case when pm.Activity = 0 then isnull(pm.CubicGrams,0) else 0 end), 

			Dev_Plan_AdvReef = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) else 0 end),
			Dev_Plan_AdvWaste = max(case when pm.Activity = 1 then isnull(pm.WasteAdv,0) else 0 end),
			Dev_Plan_Primm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
			Dev_Plan_Secm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end), 
			Dev_Plan_Adv = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
			Dev_Plan_TonsReef = max(case when pm.Activity = 1 then isnull(pm.ReefTons,0) else 0 end),
			Dev_Plan_TonsWaste = max(case when pm.Activity = 1 then isnull(pm.WasteTons,0) else 0 end),
			Dev_Plan_Tons = max(case when pm.Activity = 1 then isnull(pm.Tons,0) else 0 end), 
			Dev_Plan_KG = max(case when pm.Activity = 1 then isnull(pm.KG,0) / 1000 else 0 end),
			Dev_Plan_AdvEH = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DHeight,0) else 0 end),
			Dev_Plan_AdvEW = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DWidth,0) else 0 end), 
			Dev_Plan_AdvCmgt = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) * isnull(pm.CMGT,0) else 0 end), 
			Dev_Plan_AdvCmgtTotal = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(pm.CMGT,0) else 0 end),
			Dev_Plan_Cubics = max(case when pm.Activity = 1 then isnull(pm.Cubics,0) else 0 end), 
			Dev_Plan_CubicTons = max(case when pm.Activity = 1 then isnull(pm.CubicsTons,0) else 0 end), 
			Dev_Plan_CubicGrams = max(case when pm.Activity = 1 then isnull(pm.CubicGrams,0) / 1000 else 0 end), 
			Dev_Plan_Labour = max(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
			Dev_Plan_ShftInfo = max(0), 
			Dev_Plan_DrillRig = max(case when pm.Activity = 1 then isnull(pm.DrillRig,0) else 0 end), 

			-- DPLAN - Progessive Plan
			Stp_PPlan_FL = sum(case when p.Activity = 0 and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
			Stp_PPlan_FLReef = sum(case when p.Activity = 0 and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
			Stp_PPlan_FLWaste = sum(case when p.Activity = 0 and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
			Stp_PPlan_Sqm = sum(case when p.Activity = 0 then isnull(p.Sqm,0) else 0 end),
			Stp_PPlan_SqmReef = sum(case when p.Activity = 0 then isnull(p.ReefSqm,0) else 0 end), 
			Stp_PPlan_SqmWaste = sum(case when p.Activity = 0 then isnull(p.WasteSqm,0) else 0 end),
			Stp_PPlan_Adv = sum(case when p.Activity = 0 then isnull(p.MetresAdvance,0) else 0 end),
			Stp_PPlan_AdvReef = sum(case when p.Activity = 0 then isnull(p.ReefAdv,0) else 0 end), 
			Stp_PPlan_AdvWaste = sum(case when p.Activity = 0 then isnull(p.WasteAdv,0) else 0 end),
			Stp_PPlan_SqmSW = sum(case when p.Activity = 0 then isnull(pm.SW,0) * isnull(p.Sqm,0) else 0 end), 
			Stp_PPlan_SqmCW = sum(case when p.Activity = 0 then isnull(pm.CW,0) * isnull(p.Sqm,0) else 0 end),
			Stp_PPlan_SqmCmgt = sum(case when p.Activity = 0 then isnull(pm.cmgt,0) * isnull(p.ReefSqm,0) else 0 end), 
			Stp_PPlan_SqmCmgtTotal = sum(case when p.Activity = 0 then isnull(pm.cmgt,0) * isnull(p.Sqm,0) else 0 end),
			Stp_PPlan_Tons = sum(case when p.Activity = 0 then isnull(p.Tons,0) else 0 end),
			Stp_PPlan_TonsReef = sum(case when p.Activity = 0 then isnull(p.ReefTons,0) else 0 end), 
			Stp_PPlan_TonsWaste = sum(case when p.Activity = 0 then isnull(p.WasteTons,0) else 0 end),
			Stp_PPlan_Kg = sum(case when p.Activity = 0 then isnull(p.Grams,0) / 1000 else 0 end), 
			Stp_PPlan_Cubics = max(case when p.Activity = 0 then isnull(p.Cubics,0) else 0 end), 
			Stp_PPlan_CubicTons = max(case when p.Activity = 0 then isnull(p.CubicTons,0) else 0 end), 
			Stp_PPlan_CubicGrams = max(case when p.Activity = 0 then isnull(p.CubicGrams,0) else 0 end), '

		set @PlanSelect1 = '
			Dev_PPlan_AdvReef = sum(case when p.Activity = 1 then p.ReefAdv else 0 end), 
			Dev_PPlan_AdvWaste = sum(case when p.Activity = 1 then p.WasteAdv else 0 end), 
			Dev_PPlan_Primm = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_Secm = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_Adv = sum(case when p.Activity = 1 then p.MetresAdvance else 0 end), 
			Dev_PPlan_TonsReef = sum(case when p.Activity = 1 then p.ReefTons else 0 end), 
			Dev_PPlan_TonsWaste = sum(case when p.Activity = 1 then p.WasteTons else 0 end), 
			Dev_PPlan_Tons = sum(case when p.Activity = 1 then p.Tons else 0 end), 
			Dev_PPlan_KG = sum(case when p.Activity = 1 then p.Grams / 1000 else 0 end), 
			Dev_PPlan_AdvEH = sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * pm.DHeight else 0 end), 
			Dev_PPlan_AdvEW= sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * pm.DWidth else 0 end), 
			Dev_PPlan_AdvCmgt = sum(case when p.Activity = 1 and p.ReefAdv > 0 then p.ReefAdv * p.Cmgt else 0 end), 
			Dev_PPlan_AdvCmgtTotal = sum(case when p.Activity = 1 and p.MetresAdvance > 0 then p.MetresAdvance * p.Cmgt else 0 end), 
			Dev_PPlan_Cubics = Sum(case when p.Activity = 1 then p.Cubics else 0 end), 
			Dev_PPlan_CubicTons = Sum(case when p.Activity = 1 then p.CubicTons else 0 end), 
			Dev_PPlan_CubicGrams = Sum(case when p.Activity = 1 then p.CubicGrams / 1000 else 0 end), 
			Dev_PPlan_Labour = Sum(case when p.Activity = 1 then pm.LabourStrength else 0 end), 
			Dev_PPlan_ShftInfo = Sum(0), 
			Dev_PPlan_DrillRig = Sum(0), 

			Stp_Book_FL = max(case when p.Activity = 0 then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_FLReef = max(case when p.Activity = 0 then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_FLWaste = max(case when p.Activity = 0 then isnull(p.BookFL,0) else 0 end),  
			Stp_Book_Sqm = sum(case when p.Activity = 0 then isnull(p.BookSqm,0) else 0 end),
			Stp_Book_SqmReef = sum(case when p.Activity = 0 then isnull(p.BookReefSqm,0) else 0 end), 
			Stp_Book_SqmWaste = sum(case when p.Activity = 0 then isnull(p.BookWasteSqm,0) else 0 end),
			Stp_Book_Adv = sum(case when p.Activity = 0 then isnull(p.BookMetresAdvance,0) else 0 end),
			Stp_Book_AdvReef = sum(case when p.Activity = 0 then isnull(p.BookReefAdv,0) else 0 end), 
			Stp_Book_AdvWaste = sum(case when p.Activity = 0 then isnull(p.BookWasteAdv,0) else 0 end),
			Stp_Book_SqmSW = sum(case when p.Activity = 0 then isnull(p.BookSW,0) * isnull(p.BookSqm,0) else 0 end),
			Stp_Book_SqmCW = sum(case when p.Activity = 0 then isnull(p.BookCW,0) * isnull(p.BookSqm,0) else 0 end), 
			Stp_Book_SqmCmgt = sum(case when p.Activity = 0 then isnull(p.BookCmgt,0) * isnull(p.BookReefSqm,0) else 0 end),
			Stp_Book_SqmCmgtTotal = sum(case when p.Activity = 0 then isnull(p.BookCmgt,0) * isnull(p.BookSqm,0) else 0 end),
			Stp_Book_Tons = sum(case when p.Activity = 0 then isnull(p.BookTons,0) else 0 end), 
			Stp_Book_TonsReef = sum(case when p.Activity = 0 then isnull(p.BookReefTons,0) else 0 end),
			Stp_Book_TonsWaste = sum(case when p.Activity = 0 then isnull(p.BookWasteTons,0) else 0 end), 
			Stp_Book_KG = sum(case when p.Activity = 0 then isnull(p.BookGrams,0) / 1000 else 0 end),
			Stp_Book_Cubics = sum(case when pm.Activity = 0 then isnull(p.BookCubics,0) else 0 end), 
			Stp_Book_CubicTons = sum(case when pm.Activity = 0 then isnull(p.BookCubicTons,0) else 0 end), 
			Stp_Book_CubicGrams = sum(case when pm.Activity = 0 then isnull(p.BookCubicGrams,0) else 0 end), 

			Dev_Book_AdvReef = sum(case when p.Activity = 1 then isnull(p.BookReefAdv,0) else 0 end), 
			Dev_Book_AdvWaste = sum(case when p.Activity = 1 then isnull(p.BookWasteAdv,0) else 0 end), 
			Dev_Book_Primm = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_Secm = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_Adv = sum(case when p.Activity = 1 then isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_TonsReef = sum(case when p.Activity = 1 then isnull(p.BookReefTons,0) else 0 end),
			Dev_Book_TonsWaste = sum(case when p.Activity = 1 then isnull(p.BookWasteTons,0) else 0 end), 
			Dev_Book_Tons = sum(case when p.Activity = 1 then isnull(p.BookTons,0) else 0 end), 
			Dev_Book_KG = sum(case when p.Activity = 1 then isnull(p.BookGrams,0) / 1000 else 0 end), 
			Dev_Book_AdvEH = sum(case when p.Activity = 1 and isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(pm.DHeight,0) * isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_AdvEW = sum(case when p.Activity = 1 and isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(pm.DWidth,0) * isnull(p.BookMetresAdvance,0) else 0 end), 
			Dev_Book_AdvCmgt = sum(case when isnull(p.BookReefAdv,0) > 0  then 
						isnull(p.Bookcmgt,0) * isnull(p.BookReefAdv,0) else 0 end),
			Dev_Book_AdvCmgtTotal = sum(case when isnull(p.BookMetresAdvance,0) > 0 then 
						isnull(p.BookMetresAdvance,0) * isnull(p.Cmgt,0) else 0 end), 
			Dev_Book_Cubics = Sum(case when p.Activity = 1 then isnull(p.BookCubicMetres,0) else 0 end), 
			Dev_Book_CubicTons = Sum(case when p.Activity = 1 then isnull(p.BookCubicTons,0) else 0 end), 
			Dev_Book_CubicGrams = Sum(case when p.Activity = 1 then isnull(p.BookCubicGrams,0) / 1000 else 0 end), 
			--Dev_Book_Labour = Sum(pm.LabourStrength), 
			--Dev_Book_ShftInfo = Sum(p.ShftInfo), 

			Stp_LPlan_FL = sum(0),
			Stp_LPlan_FLReef = sum(0),
			Stp_LPlan_FLWaste = sum(0),
			Stp_LPlan_Sqm = sum(0),
			Stp_LPlan_SqmReef = sum(0),
			Stp_LPlan_SqmWaste = sum(0), 
			Stp_LPlan_Adv = sum(0),
			Stp_LPlan_AdvReef = sum(0),
			Stp_LPlan_AdvWaste = sum(0),
			Stp_LPlan_SqmSW = sum(0),
			Stp_LPlan_SqmCW = sum(0),
			Stp_LPlan_SqmCmgt = sum(0),
			Stp_LPlan_SqmCmgtTotal = sum(0),
			Stp_LPlan_Tons = sum(0),
			Stp_LPlan_TonsReef = sum(0),
			Stp_LPlan_TonsWaste = sum(0),
			Stp_LPlan_Kg = sum(0),
			Stp_LPlan_Cubics = sum(0),
			Stp_LPlan_CubicTons = sum(0), 
			Stp_LPlan_CubicGrams = sum(0), '

		set @PlanSelect2 = '
			Dev_LPlan_AdvReef = sum(0),
			Dev_LPlan_AdvWaste = sum(0),
			Dev_LPlan_Primm = sum(0),
			Dev_LPlan_Secm = sum(0),
			Dev_LPlan_Adv = sum(0),
			Dev_LPlan_TonsReef = sum(0),
			Dev_LPlan_TonsWaste = sum(0),
			Dev_LPlan_Tons = sum(0),
			Dev_LPlan_KG = sum(0),
			Dev_LPlan_AdvEH = sum(0),
			Dev_LPlan_AdvEW = sum(0), 
			Dev_LPlan_AdvCmgt = sum(0),
			Dev_LPlan_AdvCmgtTotal = sum(0),
			Dev_LPlan_Cubics = sum(0),
			Dev_LPlan_CubicTons = sum(0),
			Dev_LPlan_CubicGrams = sum(0),
			Dev_LPlan_Labour = sum(0),
			Dev_LPlan_ShftInfo = sum(0), 
			Dev_LPlan_DrillRig = sum(0),

			Stp_LPPlan_FL = sum(0),
			Stp_LPPlan_FLReef = sum(0),
			Stp_LPPlan_FLWaste = sum(0),
			Stp_LPPlan_Sqm = sum(0),
			Stp_LPPlan_SqmReef = sum(0),
			Stp_LPPlan_SqmWaste = sum(0), 
			Stp_LPPlan_Adv = sum(0),
			Stp_LPPlan_AdvReef = sum(0), 
			Stp_LPPlan_AdvWaste = sum(0),
			Stp_LPPlan_SqmSW = sum(0),
			Stp_LPPlan_SqmCW = sum(0),
			Stp_LPPlan_SqmCmgt = sum(0),
			Stp_LPPlan_SqmCmgtTotal = sum(0),
			Stp_LPPlan_Tons = sum(0),
			Stp_LPPlan_TonsReef = sum(0),
			Stp_LPPlan_TonsWaste = sum(0),
			Stp_LPPlan_Kg = sum(0),
			Stp_LPPlan_Cubics = sum(0),
			Stp_LPPlan_CubicTons = sum(0), 
			Stp_LPPlan_CubicGrams = sum(0),

			Dev_LPPlan_AdvReef = sum(0),
			Dev_LPPlan_AdvWaste = sum(0),
			Dev_LPPlan_Primm = sum(0),
			Dev_LPPlan_Secm = sum(0),
			Dev_LPPlan_Adv = sum(0), 
			Dev_LPPlan_TonsReef = sum(0),
			Dev_LPPlan_TonsWaste = sum(0),
			Dev_LPPlan_Tons = sum(0),
			Dev_LPPlan_KG = sum(0),
			Dev_LPPlan_AdvEH = sum(0),
			Dev_LPPlan_AdvEW = sum(0),
			Dev_LPPlan_AdvCmgt = sum(0),
			Dev_LPPlan_AdvCmgtTotal = sum(0),
			Dev_LPPlan_Cubics = sum(0),
			Dev_LPPlan_CubicTons = sum(0),
			Dev_LPPlan_CubicGrams = sum(0),
			Dev_LPPlan_Labour = sum(0),
			Dev_LPPlan_ShftInfo = sum(0),
			Dev_LPPlan_DrillRig = sum(0),

			Stp_Meas_FL = sum(0),
			Stp_Meas_FLReef = sum(0),
			Stp_Meas_FLWaste = sum(0),
			Stp_Meas_Sqm = sum(0),
			Stp_Meas_SqmReef = sum(0),
			Stp_Meas_SqmWaste = sum(0),
			Stp_Meas_Adv = sum(0),
			Stp_Meas_AdvReef = sum(0),
			Stp_Meas_Advwaste = sum(0),
			Stp_Meas_SqmSW = sum(0),
			Stp_Meas_SqmCW = sum(0),
			Stp_Meas_SqmCMGT = sum(0 ),
			Stp_Meas_SqmCMGTTotal = sum(0 ),				
			Stp_Meas_Tons = sum(0), 
			Stp_Meas_TonsReef = sum(0.0), 
			Stp_Meas_TonsWaste = sum(0.0),
			Stp_Meas_Kg = sum(0), 
			Stp_Meas_Cubics = sum(0),
			Stp_Meas_CubicTons = sum(0), 
			Stp_Meas_CubicGrams = sum(0),

			Dev_Meas_AdvReef = sum(0),
			Dev_Meas_AdvWaste = sum(0),
			Dev_Meas_Primm = sum(0),
			Dev_Meas_Secm = sum(0),
			Dev_Meas_Adv = sum(0),
			Dev_Meas_TonsReef = sum(0),
			Dev_Meas_TonsWaste = sum(0), 
			Dev_Meas_Tons = sum(0),
			Dev_Meas_KG = sum(0),
			Dev_Meas_AdvEH = sum(0),
			Dev_Meas_AdvEW = sum(0),
			Dev_Meas_AdvCmgt = sum(0),
			Dev_Meas_AdvCmgtTotal = sum(0),
			Dev_Meas_Cubics = sum(0), 
			Dev_Meas_CubicTons = sum(0),
			Dev_Meas_CubicGrams = sum(0),
				
			BusFlag = max(''N''), 
			Stp_BPlan_FL = sum(0.000) ,
			Stp_BPlan_Sqm = sum(0.0) ,
			Stp_BPlan_SqmReef = sum(0.0) ,
			Stp_BPlan_SqmWaste = sum(0.0) ,
			Stp_BPlan_SqmSW = sum(0.0) ,
			Stp_BPlan_SqmCW = sum(0.0) ,
			Stp_BPlan_SqmCmgt = sum(0.0) , 
			Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
			Stp_BPlan_Tons = sum(0) ,
			Stp_BPlan_TonsReef = sum(0.0) ,
			Stp_BPlan_TonsWaste = sum(0.0),
			Stp_BPlan_Kg = sum(0),

			Stp_BPlan_OSSqm = sum(0.0),
			Stp_BPlan_OSFSqm = sum(0.0),
			Stp_BPlan_REEFFL = sum(0.0),
			Stp_BPlan_OSFL = sum(0.0),     
			Stp_BPlan_CUBICS = sum(0),
			Stp_BPlan_SqmSWFAULT = sum(0.0),
			Stp_BPlan_FaultTons = sum(0),
			Stp_BPlan_OSTons = sum(0),
			Stp_BPlan_CUBICTons = sum(0), 

			Dev_BPlan_AdvReef = sum(0),
			Dev_BPlan_AdvWaste = sum(0),
			Dev_BPlan_Primm = sum(0),
			Dev_BPlan_Secm = sum(0), 
			Dev_BPlan_Adv = sum(0),
			Dev_BPlan_TonsReef = sum(0),
			Dev_BPlan_TonsWaste = sum(0),
			Dev_BPlan_Tons = sum(0),
			Dev_BPlan_KG = sum(0),
			Dev_BPlan_AdvEH = sum(0),
			Dev_BPlan_AdvEW = sum(0),
			Dev_BPlan_AdvCmgt = sum(0),
			Dev_BPlan_AdvCmgtTotal = sum(0),
			Dev_BPlan_Cubics = sum(0),
			Dev_BPlan_CubicTons = sum(0),
			Dev_BPlan_TonsCmgt = sum(0) ,
			
			ForeCast_SQM = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							sum(p.BookSQM) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end, 
			ForeCast_SQMDens = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							sum(p.BookSQM) * max(pm.Density) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end, 
			ForeCast_Grams = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							sum(p.BookGrams) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end,
			ForeCast_KG = case when pm.Activity = 0 and max(p.ShiftDay) > 0 then 
							(sum(p.BookGrams) / 1000) / max(p.ShiftDay) * max(s.TotalShifts) else 0 end '
		set @PlanFrom = '
		    from Planmonth pm   
			inner join Planning p on 
				p.Prodmonth = pm.Prodmonth and 
				p.SectionID = pm.Sectionid and 
				p.WorkplaceID = pm.Workplaceid and 
				p.Activity = pm.Activity and
				p.PlanCode = pm.PlanCode and
				p.IsCubics = pm.IsCubics 
			inner join section_complete sc on  
				 sc.prodmonth = pm.prodmonth and  
				 sc.sectionid  = pm.sectionid
			inner join  (select distinct(ProdMonth) ProdMonth , SectionID,TotalShifts from seccal ) s on
			   s.ProdMonth = sc.prodmonth and
			   s.SectionID = sc.SectionID_1  
			inner join Workplace w on  
				 w.WorkplaceID = pm.WorkplaceID 
			inner join CommonAreaSubSections CS on
				 cs.Id = w.SubSection 
			inner join CommonAreas c on
				 c.Id = cs.CommonArea '

		SET @PlanWhere = 
			' where pm.PlanCode = ''MP'' and pm.IsCubics = ''N'' and'
		IF (@ReportType = 'P')
		BEGIN
			set @PlanWhere = @PlanWhere + ' pm.prodmonth = '''+@ProdMonth+''' and p. CalendarDate <= '''+@Calendardate+''' ' 
		END
		IF (@ReportType = 'M')
		BEGIN
			set @PlanWhere = @PlanWhere + 
				' pm.prodmonth >= '''+@FromMonth+''' and 
					pm.prodmonth <= '''+@ToMonth+''' ' 
		END
			
		--IF (@TheStopeLedge = 0)
		--BEGIN
		--	set @PlanWhere = @PlanWhere + ' and pm.Activity IN(0,3) '
		--END
		--IF (@TheStopeLedge = 1)
		--BEGIN
		--	set @PlanWhere = @PlanWhere + ' and pm.Activity = 0 '
		--END
		--IF (@TheStopeLedge = 2)
		--BEGIN
		--	set @PlanWhere = @PlanWhere + ' and pm.Activity = 3 '
		--END

		set @PlanWhere = @PlanWhere + '
			and '+@ReadSection+' = '''+@SectionID+'''
			group by  
				pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.Name_4, sc.SectionID_4, 
				sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,
				sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name, 
				pm.workplaceid, w.Description, pm.Activity '
		
			
	END
	IF (@Meas = 'Y') --or ()   --) and (( @DailyPlan= '1') or (@ProgPlan = '1') or (@Book = '1')) 
	BEGIN
		set @SurveyUnion = 'union all '
		set @SurveySelect = 
			' select 
				sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
				sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
				sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME,
				pm.Prodmonth, pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
				Plan_Stope = case when pm.Activity in (0,3) then 1 else 0 end, 
				Plan_Dev = case when pm.Activity in (1) then 1 else 0 end,
				Stp_Plan_FL = sum(0), 
				Stp_Plan_FLReef = sum(0), 
				Stp_Plan_FLWaste = sum(0), 
				Stp_Plan_Sqm = sum(0),
				Stp_Plan_SqmReef = sum(0),
				Stp_Plan_SqmWaste = sum(0),
				Stp_Plan_Adv = sum(0),
				Stp_Plan_AdvReef = sum(0),
				Stp_Plan_AdvWaste = sum(0),
				Stp_Plan_SqmSW = sum(0),
				Stp_Plan_SqmCW = sum(0),
				Stp_Plan_SqmCmgt = sum(0),
				Stp_Plan_SqmCmgtTotal = sum(0),
				Stp_Plan_Tons = sum(0),
				Stp_Plan_TonsReef = sum(0),
				Stp_Plan_TonsWaste = sum(0),
				Stp_Plan_Kg = sum(0),
				Stp_Plan_Cubics = sum(0),
				Stp_Plan_CubicTons = sum(0),
				Stp_Plan_CubicGrams = sum(0),

				Dev_Plan_AdvReef = sum(0),
				Dev_Plan_AdvWaste = sum(0),
				Dev_Plan_Primm = sum(0),
				Dev_Plan_Secm = sum(0),
				Dev_Plan_Adv = sum(0),
				Dev_Plan_TonsReef = sum(0),
				Dev_Plan_TonsWaste = sum(0),
				Dev_Plan_Tons = sum(0),
				Dev_Plan_KG = sum(0),
				Dev_Plan_AdvEH = sum(0),
				Dev_Plan_AdvEW = sum(0),
				Dev_Plan_AdvCmgt = sum(0),
				Dev_Plan_AdvCmgtTotal = sum(0),
				Dev_Plan_Cubics = sum(0), 
				Dev_Plan_CubicTons = sum(0),
				Dev_Plan_CubicGrams = sum(0),
				Dev_Plan_Labour = sum(0),
				Dev_Plan_ShftInfo = sum(0),
				Dev_Plan_DrillRig = sum(0),

				Stp_PPlan_FL = sum(0), 
				Stp_PPlan_FLReef = sum(0), 
				Stp_PPlan_FLWaste = sum(0), 
				Stp_PPlan_Sqm = sum(0),
				Stp_PPlan_SqmReef = sum(0),
				Stp_PPlan_SqmWaste = sum(0),
				Stp_PPlan_Adv = sum(0),
				Stp_PPlan_AdvReef = sum(0),
				Stp_PPlan_AdvWaste = sum(0),
				Stp_PPlan_SqmSW = sum(0),
				Stp_PPlan_SqmCW = sum(0),
				Stp_PPlan_SqmCmgt = sum(0),
				Stp_PPlan_SqmCmgtTotal = sum(0),
				Stp_PPlan_Tons = sum(0),
				Stp_PPlan_TonsReef = sum(0),
				Stp_PPlan_TonsWaste = sum(0),
				Stp_PPlan_Kg = sum(0),
				Stp_PPlan_Cubics = sum(0),
				Stp_PPlan_CubicTons = sum(0),
				Stp_PPlan_CubicGrams = sum(0),

				Dev_PPlan_AdvReef = sum(0),
				Dev_PPlan_AdvWaste = sum(0), 
				Dev_PPlan_Primm = sum(0),
				Dev_PPlan_Secm = sum(0),
				Dev_PPlan_Adv = sum(0),
				Dev_PPlan_TonsReef = sum(0), 
				Dev_PPlan_TonsWaste = sum(0), 
				Dev_PPlan_Tons = sum(0), 
				Dev_PPlan_KG = sum(0),
				Dev_PPlan_AdvEH = sum(0),
				Dev_PPlan_AdvEW = sum(0),
				Dev_PPlan_AdvCmgt = sum(0),
				Dev_PPlan_AdvCmgtTotal = sum(0), 
				Dev_PPlan_Cubics = sum(0),
				Dev_PPlan_CubicTons = sum(0),
				Dev_PPlan_CubicGrams = sum(0),
				Dev_PPlan_Labour = sum(0),
				Dev_PPlan_ShftInfo = sum(0), 
				Dev_PPlan_DrillRig = sum(0), 

				Stp_Book_FL = sum(0),
				Stp_Book_FLReef = sum(0),
				Stp_Book_FLWaste = sum(0),
				Stp_Book_Sqm = sum(0),
				Stp_Book_SqmReef = sum(0), 
				Stp_Book_SqmWaste = sum(0),
				Stp_Book_Adv = sum(0),
				Stp_Book_AdvReef = sum(0), 
				Stp_Book_AdvWaste = sum(0),
				Stp_Book_SqmSW = sum(0),
				Stp_Book_SqmCW = sum(0), 
				Stp_Book_SqmCmgt = sum(0), 
				Stp_Book_SqmCmgtTotal = sum(0),
				Stp_Book_Tons = sum(0),
				Stp_Book_TonsReef = sum(0),
				Stp_Book_TonsWaste = sum(0),
				Stp_Book_KG = sum(0),
				Stp_Book_Cubics = sum(0),
				Stp_Book_CubicTons = sum(0),
				Stp_Book_CubicGrams = sum(0),

				Dev_Book_AdvReef = sum(0),
				Dev_Book_AdvWaste = sum(0),
				Dev_Book_Primm = sum(0),
				Dev_Book_Secm = sum(0), 
				Dev_Book_Adv = sum(0), 
				Dev_Book_TonsReef = sum(0),
				Dev_Book_TonsWaste = sum(0),
				Dev_Book_Tons = sum(0), 
				Dev_Book_KG = sum(0), 
				Dev_Book_AdvEH = sum(0), 
				Dev_Book_AdvEW = sum(0),
				Dev_Book_AdvCmgt = sum(0),
				Dev_Book_AdvCmgtTotal = sum(0),
				Dev_Book_Cubics = sum(0),
				Dev_Book_CubicTons = sum(0),
				Dev_Book_CubicGrams = sum(0),
				--Dev_Book_Labour = sum(0), 
				--Dev_Book_ShftInfo = sum(0),
				
				Stp_LPlan_FL = sum(0),
				Stp_LPlan_FLReef = sum(0),
				Stp_LPlan_FLWaste = sum(0),
				Stp_LPlan_Sqm = sum(0),
				Stp_LPlan_SqmReef = sum(0),
				Stp_LPlan_SqmWaste = sum(0), 
				Stp_LPlan_Adv = sum(0),
				Stp_LPlan_AdvReef = sum(0),
				Stp_LPlan_AdvWaste = sum(0),
				Stp_LPlan_SqmSW = sum(0),
				Stp_LPlan_SqmCW = sum(0),
				Stp_LPlan_SqmCmgt = sum(0),
				Stp_LPlan_SqmCmgtTotal = sum(0),
				Stp_LPlan_Tons = sum(0),
				Stp_LPlan_TonsReef = sum(0),
				Stp_LPlan_TonsWaste = sum(0),
				Stp_LPlan_Kg = sum(0),
				Stp_LPlan_Cubics = sum(0),
				Stp_LPlan_CubicTons = sum(0),
				Stp_LPlan_CubicGrams = sum(0),

				Dev_LPlan_AdvReef = sum(0),
				Dev_LPlan_AdvWaste = sum(0),
				Dev_LPlan_Primm = sum(0),
				Dev_LPlan_Secm = sum(0),
				Dev_LPlan_Adv = sum(0),
				Dev_LPlan_TonsReef = sum(0),
				Dev_LPlan_TonsWaste = sum(0),
				Dev_LPlan_Tons = sum(0),
				Dev_LPlan_KG = sum(0),
				Dev_LPlan_AdvEH = sum(0),
				Dev_LPlan_AdvEW = sum(0),
				Dev_LPlan_AdvCmgt = sum(0),
				Dev_LPlan_AdvCmgtTotal = sum(0),
				Dev_LPlan_Cubics = sum(0),
				Dev_LPlan_CubicTons = sum(0),
				Dev_LPlan_CubicGrams = sum(0),
				Dev_LPlan_Labour = sum(0),
				Dev_LPlan_ShftInfo = sum(0),
				Dev_LPlan_DrillRig = sum(0),

				Stp_LPPlan_FL = sum(0),
				Stp_LPPlan_FLReef = sum(0),
				Stp_LPPlan_FLWaste = sum(0),
				Stp_LPPlan_Sqm = sum(0),
				Stp_LPPlan_SqmReef = sum(0),
				Stp_LPPlan_SqmWaste = sum(0), 
				Stp_LPPlan_Adv = sum(0),
				Stp_LPPlan_AdvReef = sum(0), 
				Stp_LPPlan_AdvWaste = sum(0),
				Stp_LPPlan_SqmSW = sum(0),
				Stp_LPPlan_SqmCW = sum(0),
				Stp_LPPlan_SqmCmgt = sum(0),
				Stp_LPPlan_SqmCmgtTotal = sum(0),
				Stp_LPPlan_Tons = sum(0),
				Stp_LPPlan_TonsReef = sum(0),
				Stp_LPPlan_TonsWaste = sum(0),
				Stp_LPPlan_Kg = sum(0),
				Stp_LPPlan_Cubics = sum(0),
				Stp_LPPlan_CubicTons = sum(0),
				Stp_LPPlan_CubicGrams = sum(0), 
				
				Dev_LPPlan_AdvReef = sum(0),
				Dev_LPPlan_AdvWaste = sum(0),
				Dev_LPPlan_Primm = sum(0),
				Dev_LPPlan_Secm = sum(0),
				Dev_LPPlan_Adv = sum(0),
				Dev_LPPlan_TonsReef = sum(0),
				Dev_LPPlan_TonsWaste = sum(0), 
				Dev_LPPlan_Tons = sum(0),
				Dev_LPPlan_KG = sum(0),
				Dev_LPPlan_AdvEH = sum(0),
				Dev_LPPlan_AdvEW= sum(0),
				Dev_LPPlan_AdvCmgt = sum(0), 
				Dev_LPPlan_AdvCmgtTotal = sum(0), 
				Dev_LPPlan_Cubics = sum(0),
				Dev_LPPlan_CubicTons = sum(0),
				Dev_LPPlan_CubicGrams = sum(0),
				Dev_LPPlan_Labour =sum(0),
				Dev_LPPlan_ShftInfo = sum(0),
				Dev_LPPlan_DrillRig = sum(0), ' 

		set @SurveySelect1 = ''		
		IF (@TheStopeLedge = 0) --Stoping and Ledging
			set @SurveySelect1 = @SurveySelect1 + ' 
				Stp_Meas_FL = sum(case when pm.Activity = 0 then isnull(pm.FLTotal,0) else 0 end),
				Stp_Meas_FLReef = sum(case when pm.Activity = 0 then isnull(pm.StopeFl,0) + isnull(pm.LedgeFl,0) else 0 end),
				Stp_Meas_FLWaste = sum(case when pm.Activity = 0 then isnull(pm.FLTotal,0) else 0 end),
				Stp_Meas_Sqm = sum(case when pm.Activity = 0 then isnull(pm.SqmTotal,0) else 0 end), 
				--sum(pm.StopeSqm+pm.LedgeSqm+pm.SqmConvTotal + pm.SqmOSTotal + pm.SqmOSFTotal),--
				Stp_Meas_SqmReef =  sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) else 0 end),
				--sum(pm.StopeSqm+pm.LedgeSqm+pm.SqmConvTotal),--
				Stp_Meas_SqmWaste = sum(case when pm.Activity = 0 then isnull(pm.SqmOSFTotal,0) + isnull(pm.SqmOSTotal,0) else 0 end),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef =  sum(0),
				Stp_Meas_AdvWaste = sum(0),
				Stp_Meas_SqmSW = sum(case when pm.Activity = 0 then isnull(pm.SqmTotal,0) * isnull(pm.SWSqm,0) else 0 end),
				Stp_Meas_SqmCW = sum(case when pm.Activity = 0 then 
						(isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) + isnull(pm.SqmConvTotal,0)) * isnull(pm.CW,0) else 0 end),
				--,sum(pm.SqmTotal * pm.CW),
				Stp_Meas_SqmCMGT = sum(case when pm.Activity = 0 then 
						(isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) + isnull(pm.SqmConvTotal,0)) * isnull(pm.cmgt,0) else 0 end),
				--sum((pm.StopeSqm + pm.LedgeSqm)*pm.cmgt), 
				Stp_Meas_SqmCMGTTotal = sum(case when pm.Activity = 0 then 
						(isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0) + isnull(pm.SqmConvTotal,0)) * isnull(pm.cmgt,0) else 0 end),
				--sum((pm.StopeSqm + pm.LedgeSqm)*pm.cmgt), 
				Stp_Meas_Tons = sum(case when pm.Activity = 0 then isnull(pm.TonsTotal,0) else 0 end),
				--sum((SqmTotal) * pm.SWSqm / 100 * pm.Density),
				Stp_Meas_TonsReef = sum(case when pm.Activity = 0 then isnull(pm.TonsReef,0) else 0 end),
				--sum((pm.StopeSqm + pm.LedgeSqm) * pm.SWSqm / 100 * pm.Density),
				Stp_Meas_TonsWaste = sum(case when pm.Activity = 0 then 
							isnull(pm.TonsOS,0) + isnull(pm.TonsOSF,0) + isnull(pm.TonsWaste,0) else 0 end),
				--sum((pm.SqmOSFTotal + pm.SqmOSTotal) * pm.SWSqm / 100 * pm.Density),
				Stp_Meas_Kg = sum(case when pm.Activity = 0 and isnull(pm.SWSqm,0) > 0 then 
					(
						((isnull(pm.StopeSqm,0) + isnull(pm.LedgeSqm,0)) * isnull(pm.cmgt,0) * isnull(pm.Density,0) / 100) + 
						(isnull(pm.CubicsReef,0) * isnull(pm.Cmgt,0) / isnull(pm.SWSqm,0) * isnull(pm.Density,0))
					) / 1000 else 0 end), '

		IF (@TheStopeLedge = 1)
			set @SurveySelect1 = @SurveySelect1 + ' 
				Stp_Meas_FL = sum(case when pm.Activity = 0 then isnull(pm.StopeFL,0) + isnull(pm.StopeFLOS,0) else 0 end),
				Stp_Meas_FLReef = sum(case when pm.Activity = 0 then isnull(pm.StopeFl,0) else 0 end),
				Stp_Meas_FLWaste = sum(case when pm.Activity = 0 then isnull(pm.StopeFLOS,0) else 0 end),
				Stp_Meas_Sqm = sum(case when pm.Activity = 0 then isnull(pm.StopeSqmTotal,0) else 0 end), 
				Stp_Meas_SqmReef = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) else 0 end),
				Stp_Meas_SqmWaste = sum(case when pm.Activity = 0 then isnull(pm.StopeSqmOSF,0) + isnull(pm.StopeSqmOS,0) else 0 end),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef =  sum(0),
				Stp_Meas_AdvWaste = sum(0),
				Stp_Meas_SqmSW = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.SWSqm,0) else 0 end),
				Stp_Meas_SqmCW = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.CW,0) else 0 end),
				Stp_Meas_SqmCMGT = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_SqmCMGTTotal = sum(case when pm.Activity = 0 then isnull(pm.StopeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_Tons = sum(case when pm.Activity = 0 then 
							isnull(StopeSqmTotal,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_TonsReef = sum(case when pm.Activity = 0 then 
							isnull(StopeSqm,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Sensity,0) else 0 end),
				Stp_Meas_TonsWaste = sum(case when pm.Activity = 0 then 
							(isnull(pm.StopeSqmOSF,0) + isnull(pm.StopeSqmOS,0)) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_Kg = sum(case when pm.Activity = 0 and isnull(pm.SWSqm,0) > 0 then 
						(
							(isnull(pm.StopeSqm,0) * isnull(pm.cmgt,0) * isnull(pm.Density,0) / 100) + 
							(isnull(pm.CubicsReef,0) * isnull(pm.Cmgt,0) / isnull(pm.SWSqm,0) * isnull(pm.Density,0))
						) / 1000 else 0 end),'
		IF (@TheStopeLedge = 2)	
			set @SurveySelect1 = @SurveySelect1 + ' 
				Stp_Meas_FL = sum(case when pm.Activity = 0 then isnull(pm.LedgeFL,0) + isnull(pm.LedgeFLOS,0) else 0 end),
				Stp_Meas_FLReef = sum(case when pm.Activity = 0 then isnull(pm.LedgeFl,0) else 0 end),
				Stp_Meas_FLWaste = sum(case when pm.Activity = 0 then isnull(pm.LedgeFLOS,0) else 0 end),
				Stp_Meas_Sqm = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqmTotal,0) else 0 end), 
				Stp_Meas_SqmReef = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) else 0 end),
				Stp_Meas_SqmWaste = sum(case when pm.Activity = 0 then 
							isnull(pm.LedgeSqmOSF,0) + isnull(pm.LedgeSqmOS,0) else 0 end),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef =  sum(0),
				Stp_Meas_AdvWaste = sum(0), 
				Stp_Meas_SqmSW = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.SWSqm,0) else 0 end),
				Stp_Meas_SqmCW = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.CW,0) else 0 end),
				Stp_Meas_SqmCMGT = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_SqmCMGTTotal = sum(case when pm.Activity = 0 then isnull(pm.LedgeSqm,0) * isnull(pm.cmgt,0) else 0 end), 
				Stp_Meas_Tons = sum(case when pm.Activity = 0 then 
							isnull(LedgeSqmTotal,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_OnTons = sum(case when pm.Activity = 0 then 
							isnull(LedgeSqm,0) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_OffTons = sum(case when pm.Activity = 0 then 
							(isnull(pm.LedgeSqmOSF,0) + isnull(pm.LedgeSqmOS,0)) * isnull(pm.SWSqm,0) / 100 * isnull(pm.Density,0) else 0 end),
				Stp_Meas_Kg = sum(case when pm.Activity = 0 and isnull(pm.SWSqm,0) > 0 then 
					(
						(isnull(pm.LedgeSqm,0) * isnull(pm.cmgt,0) * isnull(pm.Density,0) / 100) + 
						(isnull(pm.CubicsReef,0) * isnull(pm.Cmgt,0) / isnull(pm.SWSqm,0) * isnull(pm.StpDensity,0))
					) / 1000 else 0 end),'

		set @SurveySelect1 = @SurveySelect1 + '
				Stp_Meas_Cubics = sum(0), 
				Stp_Meas_CubicTons = sum(0),
				Stp_Meas_CubicGrams = sum(0),

				Dev_Meas_AdvReef = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres,0) else 0 end), 
				Dev_Meas_AdvWaste = sum(case when pm.Activity = 1 then isnull(pm.WasteMetres,0) else 0 end), 
				Dev_Meas_Primm = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres + pm.WasteMetres,0) else 0 end), 
				Dev_Meas_Secm = sum(0.0), 
				Dev_Meas_Adv = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres,0) + isnull(pm.WasteMetres,0) else 0 end), 
				Dev_Meas_TonsReef = sum(case when pm.Activity = 1 then isnull(TonsReef,0) else 0 end), 
				Dev_Meas_TonsWaste = sum(case when pm.Activity = 1 then isnull(TonsWaste,0) else 0 end), 
				Dev_Meas_Tons = sum(case when pm.Activity = 1 then isnull(TonsWaste,0) + isnull(TonsReef,0) else 0 end), 
				Dev_Meas_KG = sum(case when pm.Activity = 1 then isnull(pm.TotalContent,0) / 1000 else 0 end), 
				Dev_Meas_AdvEH = sum(case when pm.Activity = 1 then isnull(pm.MainMetres,0) * isnull(pm.MeasHeight,0) else 0 end), 
				Dev_Meas_AdvEW = sum(case when pm.Activity = 1 then isnull(pm.MainMetres,0) * isnull(pm.MeasWidth,0) else 0 end), 
				Dev_Meas_AdvCmgt = sum(case when pm.Activity = 1 then isnull(pm.ReefMetres,0) * isnull(pm.cmgt,0) else 0 end),
				Dev_Meas_AdvCmgtTotal = sum(0),
				Dev_Meas_Cubics = sum(0), 
				Dev_Meas_CubicTons = sum(0),
				Dev_Meas_CubicGrams = sum(0),

				BusFlag = max(''N''), 
				Stp_BPlan_FL = sum(0.000) ,
				Stp_BPlan_Sqm = sum(0.0) ,
				Stp_BPlan_SqmReef = sum(0.0) ,
				Stp_BPlan_SqmWaste = sum(0.0) ,
				Stp_BPlan_SqmSW = sum(0.0) ,
				Stp_BPlan_SqmCW = sum(0.0) ,
				Stp_BPlan_SqmCmgt = sum(0.0) , 
				Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
				Stp_BPlan_Tons = sum(0) ,
				Stp_BPlan_TonsReef = sum(0.0) ,
				Stp_BPlan_TonsWaste = sum(0.0),
				Stp_BPlan_Kg = sum(0),

				Stp_BPlan_OSSqm = sum(0.0),
				Stp_BPlan_OSFSqm = sum(0.0),
				Stp_BPlan_REEFFL = sum(0.0),
				Stp_BPlan_OSFL = sum(0.0),     
				Stp_BPlan_CUBICS = sum(0),
				Stp_BPlan_SqmSWFAULT = sum(0.0),
				Stp_BPlan_FaultTons = sum(0),
				Stp_BPlan_OSTons = sum(0),
				Stp_BPlan_CUBICTons = sum(0),

				Dev_BPlan_AdvReef = sum(0),
				Dev_BPlan_AdvWaste = sum(0),
				Dev_BPlan_Primm = sum(0),
				Dev_BPlan_Secm = sum(0), 
				Dev_BPlan_Adv = sum(0),
				Dev_BPlan_TonsReef = sum(0),
				Dev_BPlan_TonsWaste = sum(0),
				Dev_BPlan_Tons = sum(0),
				Dev_BPlan_KG = sum(0),
				Dev_BPlan_AdvEH = sum(0),
				Dev_BPlan_AdvEW =sum(0),
				Dev_BPlan_AdvCmgt = sum(0),
				Dev_BPlan_AdvCmgtTotal = sum(0),
				Dev_BPlan_Cubics = sum(0),
				Dev_BPlan_CubicTons = sum(0),
				Dev_BPlan_TonsCmgt = sum(0) ,
				
				ForeCast_SQM = sum(0) ,
				ForeCast_SQMDens = sum(0) ,
				ForeCast_Grams = sum(0) ,
				ForeCast_KG = sum(0) '
		set @SurveyFrom = '
			from Survey pm 
			inner join section_complete sc on 
				pm.PRODMONTH = sc.PRODMONTH and 
				pm.SECTIONID = sc.sectionID 
			inner join workplace w on 
				pm.WorkplaceID = w.WorkplaceID  
			inner join CommonAreaSubSections CS on
				cs.Id = w.SubSection 
			inner join CommonAreas c on
				c.Id = cs.CommonArea '
		SET @SurveyWhere = ' where '
				  
		IF (@ReportType = 'P')
		BEGIN
			set @SurveyWhere = @SurveyWhere + ' pm.prodmonth = '''+@FromMonth+''' and ' 
		END
		IF (@ReportType = 'M')
		BEGIN
			set @SurveyWhere = @SurveyWhere + 
				' pm.prodmonth >= '''+@FromMonth+''' and 
				  pm.prodmonth <= '''+@ToMonth+''' and ' 
		END
		set @SurveyWhere = @SurveyWhere + '
				'+@ReadSection+' = '''+@SectionID+''' '

		--IF (@TheStopeLedge = 0)
		--BEGIN
		--	set @SurveyWhere = @SurveyWhere + ' pm.Activity IN (0,3) '
		--END
		--IF (@TheStopeLedge = 1)
		--BEGIN
		--	set @SurveyWhere = @SurveyWhere + ' pm.Activity = 0 '
		--END
		--IF (@TheStopeLedge = 2)
		--BEGIN
		--	set @SurveyWhere = @SurveyWhere + ' pm.Activity IN (0,3) and  LedgeSqmTotal <>0 '
		--END

		set @SurveyWhere = @SurveyWhere + '
			group by  
				pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.Name_4, sc.SectionID_4, 
				sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,
				sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name, 
				pm.workplaceid, w.Description, pm.Activity '

	
	END
	IF (@PlanMonthLock = 'Y') or (@PlanProgLock = 'Y')
	BEGIN
		IF (@PlanMonth = 'Y') or (@PlanProg = 'Y') or (@Book = 'Y')
		BEGIN
			set @LPlanUnion = 'union all '
			set @LPlanSelect =  
				' select 
					sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
					sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,
					sc.SectionID_1+'':''+sc.Name_1 NAME_1,sc.SectionID+'':''+sc.Name NAME, 
					pm.Prodmonth, pm.Workplaceid+'':''+w.Description workplace,   pm.Activity,
					Plan_Stope = case when pm.Activity in (0,3) then 1 else 0 end, 
					Plan_Dev = case when pm.Activity in (1) then 1 else 0 end,
					Stp_Plan_FL = sum(0),
					Stp_Plan_FLReef = sum(0),
					Stp_Plan_FLWaste = sum(0),
					Stp_Plan_Sqm = sum(0),
					Stp_Plan_SqmReef = sum(0),
					Stp_Plan_SqmWaste = sum(0), 
					Stp_Plan_Adv = sum(0),
					Stp_Plan_AdvReef = sum(0),
					Stp_Plan_AdvWaste = sum(0),
					Stp_Plan_SqmSW = sum(0),
					Stp_Plan_SqmCW = sum(0),
					Stp_Plan_SqmCmgt = sum(0),
					Stp_Plan_SqmCmgtTotal = sum(0),
					Stp_Plan_Tons = sum(0),
					Stp_Plan_TonsReef = sum(0),
					Stp_Plan_TonsWaste = sum(0),
					Stp_Plan_Kg = sum(0),
					Stp_Plan_Cubics = sum(0), 
					Stp_Plan_CubicTons = sum(0),
					Stp_Plan_CubicGrams = sum(0),

					Dev_Plan_AdvReef = sum(0),
					Dev_Plan_AdvWaste = sum(0),
					Dev_Plan_Primm = sum(0),
					Dev_Plan_Secm = sum(0),
					Dev_Plan_Adv = sum(0),
					Dev_Plan_TonsReef = sum(0),
					Dev_Plan_TonsWaste = sum(0),
					Dev_Plan_Tons = sum(0),
					Dev_Plan_KG = sum(0),
					Dev_Plan_AdvEH = sum(0),
					Dev_Plan_AdvEW = sum(0),
					Dev_Plan_AdvCmgt = sum(0),
					Dev_Plan_AdvCmgtTotal = sum(0),
					Dev_Plan_Cubics = sum(0), 
					Dev_Plan_CubicTons = sum(0),
					Dev_Plan_CubicGrams = sum(0),
					Dev_Plan_Labour = sum(0),
					Dev_Plan_ShftInfo = sum(0),
					Dev_Plan_DrillRig = sum(0),

					Stp_PPlan_FL = sum(0),
					Stp_PPlan_FLReef = sum(0),
					Stp_PPlan_FLWaste = sum(0),
					Stp_PPlan_Sqm = sum(0),
					Stp_PPlan_SqmReef = sum(0),
					Stp_PPlan_SqmWaste = sum(0), 
					Stp_PPlan_Adv = sum(0),
					Stp_PPlan_AdvReef = sum(0), 
					Stp_PPlan_AdvWaste = sum(0),
					Stp_PPlan_SqmSW = sum(0),
					Stp_PPlan_SqmCW = sum(0),
					Stp_PPlan_SqmCmgt = sum(0),
					Stp_PPlan_SqmCmgtTotal = sum(0),
					Stp_PPlan_Tons = sum(0),
					Stp_PPlan_TonsReef = sum(0),
					Stp_PPlan_TonsWaste = sum(0),
					Stp_PPlan_Kg = sum(0),
					Stp_PPlan_Cubics = sum(0), 
					Stp_PPlan_CubicTons = sum(0),
					Stp_PPlan_CubicGrams = sum(0),

					Dev_PPlan_AdvReef = sum(0),
					Dev_PPlan_AdvWaste = sum(0), 
					Dev_PPlan_Primm = sum(0),
					Dev_PPlan_Secm = sum(0),
					Dev_PPlan_Adv = sum(0),
					Dev_PPlan_TonsReef = sum(0), 
					Dev_PPlan_TonsWaste = sum(0), 
					Dev_PPlan_Tons = sum(0), 
					Dev_PPlan_KG = sum(0),
					Dev_PPlan_AdvEH = sum(0),
					Dev_PPlan_AdvEW = sum(0),
					Dev_PPlan_AdvCmgt = sum(0),
					Dev_PPlan_AdvCmgtTotal = sum(0),
					Dev_PPlan_Cubics = sum(0),
					Dev_PPlan_CubicTons = sum(0),
					Dev_PPlan_CubicGrams = sum(0),
					Dev_PPlan_Labour = sum(0),
					Dev_PPlan_ShftInfo = sum(0), 
					Dev_PPlan_DrillRig = sum(0), 

					Stp_Book_FL = sum(0),
					Stp_Book_FLReef = sum(0),
					Stp_Book_FLWaste = sum(0),
					Stp_Book_Sqm = sum(0),
					Stp_Book_SqmReef = sum(0),
					Stp_Book_SqmWaste = sum(0),
					Stp_Book_Adv = sum(0),
					Stp_Book_AdvReef = sum(0),
					Stp_Book_AdvWaste = sum(0),
					Stp_Book_SqmSW = sum(0),
					Stp_Book_SqmCW = sum(0), 
					Stp_Book_SqmCmgt = sum(0),
					Stp_Book_SqmCmgtTotal = sum(0),
					Stp_Book_Tons = sum(0),
					Stp_Book_TonsReef = sum(0),
					Stp_Book_TonsWaste = sum(0), 
					Stp_Book_KG = sum(0),
					Stp_Book_Cubics = sum(0), 
					Stp_Book_CubicTons = sum(0),
					Stp_Book_CubicGrams = sum(0),

					Dev_Book_AdvReef = sum(0),
					Dev_Book_AdvWaste = sum(0),
					Dev_Book_Primm = sum(0),
					Dev_Book_Secm = sum(0), 
					Dev_Book_Adv = sum(0), 
					Dev_Book_TonsReef = sum(0),
					Dev_Book_TonsWaste = sum(0),
					Dev_Book_Tons = sum(0), 
					Dev_Book_KG = sum(0), 
					Dev_Book_AdvEH = sum(0), 
					Dev_Book_AdvEW = sum(0),
					Dev_Book_AdvCmgt = sum(0),
					Dev_Book_AdvCmgtTotal = sum(0),
					Dev_Book_Cubics = sum(0),
					Dev_Book_CubicTons = sum(0),
					Dev_Book_CubicGrams = sum(0),
					--Dev_Book_Labour = sum(0), 
					--Dev_Book_ShftInfo = sum(0),

					Stp_LPlan_FL = max(case when pm.Activity = 0 then isnull(pm.FL,0) else 0 end),
					Stp_LPlan_FLReef = max(case when pm.Activity = 0 then isnull(pm.ReefFL,0) else 0 end),
					Stp_LPlan_FLWaste = max(case when pm.Activity = 0 then isnull(pm.WasteFL,0) else 0 end),
					Stp_LPlan_Sqm = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) else 0 end),
					Stp_LPlan_SqmReef = max(case when pm.Activity = 0 then isnull(pm.ReefSqm,0) else 0 end),
					Stp_LPlan_SqmWaste = max(case when pm.Activity = 0 then isnull(pm.wasteSqm,0) else 0 end), 
					Stp_LPlan_Adv = sum(case when pm.Activity = 0 then isnull(pm.MetresAdvance,0) else 0 end),
					Stp_LPlan_AdvReef = sum(case when pm.Activity = 0 then isnull(pm.ReefAdv,0) else 0 end), 
					Stp_LPlan_AdvWaste = sum(case when pm.Activity = 0 then isnull(pm.WasteAdv,0) else 0 end),
					Stp_LPlan_SqmSW = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.SW,0) else 0 end),
					Stp_LPlan_SqmCW = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.CW,0) else 0 end),
					Stp_LPlan_SqmCmgt = max(case when pm.Activity = 0 then isnull(pm.ReefSqm,0) * isnull(pm.cmgt,0) else 0 end),
					Stp_LPlan_SqmCmgtTotal = max(case when pm.Activity = 0 then isnull(pm.Sqm,0) * isnull(pm.cmgt,0) else 0 end),
					Stp_LPlan_Tons = max(case when pm.Activity = 0 then isnull(pm.Tons,0) else 0 end), 
					Stp_LPlan_TonsReef = max(case when pm.Activity = 0 then isnull(pm.ReefTons,0) else 0 end),
					Stp_LPlan_TonsWaste = max(case when pm.Activity = 0 then isnull(pm.WasteTons,0) else 0 end),
					Stp_LPlan_Kg = sum(case when pm.Activity = 0 then isnull(pm.KG,0) / 1000 else 0 end),
					Stp_LPlan_Cubics = max(case when pm.Activity = 0 then isnull(pm.Cubics,0) else 0 end), 
					Stp_LPlan_CubicTons = max(case when pm.Activity = 0 then isnull(pm.CubicsTons,0) else 0 end), 
					Stp_LPlan_CubicGrams = max(case when pm.Activity = 0 then isnull(pm.CubicGrams,0) else 0 end), ' 

				set @LPlanSelect1 = '
					Dev_LPlan_AdvReef = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) else 0 end),
					Dev_LPlan_AdvWaste = max(case when pm.Activity = 1 then isnull(pm.WasteAdv,0) else 0 end),
					Dev_LPlan_Primm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
					Dev_LPlan_Secm = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end), 
					Dev_LPlan_Adv = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) else 0 end),
					Dev_LPlan_TonsReef = max(case when pm.Activity = 1 then isnull(pm.ReefTons,0) else 0 end),
					Dev_LPlan_TonsWaste = max(case when pm.Activity = 1 then isnull(pm.WasteTons,0) else 0 end),
					Dev_LPlan_Tons = max(case when pm.Activity = 1 then isnull(pm.Tons,0) else 0 end), 
					Dev_LPlan_KG = max(case when pm.Activity = 1 then isnull(pm.KG,0) / 1000 else 0 end),
					Dev_LPlan_AdvEH = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DHeight,0) else 0 end),
					Dev_LPlan_AdvEW = max(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(DWidth,0) else 0 end), 
					Dev_LPlan_AdvCmgt = max(case when pm.Activity = 1 then isnull(pm.ReefAdv,0) * isnull(pm.CMGT,0) else 0 end), 
					Dev_LPlan_AdvCmgtTotal = sum(case when pm.Activity = 1 then isnull(pm.MetresAdvance,0) * isnull(pm.CMGT,0) else 0 end),
					Dev_LPlan_Cubics = Sum(case when pm.Activity = 1 then isnull(pm.Cubics,0) else 0 end), 
					Dev_LPlan_CubicTons = Sum(case when pm.Activity = 1 then isnull(pm.CubicsTons,0) else 0 end), 
					Dev_LPlan_CubicGrams = Sum(case when pm.Activity = 1 then isnull(pm.CubicGrams,0) / 1000 else 0 end), 
					Dev_LPlan_Labour = Sum(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
					Dev_LPlan_ShftInfo = Sum(0), 
					Dev_LPlan_DrillRig = Sum(0), 
				
					-- DPLAN - Progessive Plan
					Stp_LPPlan_FL = sum(case when pm.Activity = 0 and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
					Stp_LPPlan_FLReef = sum(case when pm.Activity = 0 and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
					Stp_LPPlan_FLWaste = sum(case when pm.Activity = 0 and isnull(p.Sqm,0) > 0 then isnull(p.FL,0) else 0 end),
					Stp_LPPlan_Sqm = sum(case when pm.Activity = 0 then isnull(p.Sqm,0) else 0 end),
					Stp_LPPlan_SqmReef = sum(case when pm.Activity = 0 then isnull(p.ReefSqm,0) else 0 end), 
					Stp_LPPlan_SqmWaste = sum(case when pm.Activity = 0 then isnull(p.WasteSqm,0) else 0 end),
					Stp_LPPlan_Adv = sum(case when pm.Activity = 0 then isnull(p.MetresAdvance,0) else 0 end),
					Stp_LPPlan_AdvReef = sum(case when pm.Activity = 0 then isnull(p.ReefAdv,0) else 0 end), 
					Stp_LPPlan_AdvWaste = sum(case when pm.Activity = 0 then isnull(p.WasteAdv,0) else 0 end),
					Stp_LPPlan_SqmSW = sum(case when pm.Activity = 0 then isnull(pm.SW,0) * isnull(p.Sqm,0) else 0 end), 
					Stp_LPPlan_SqmCW = sum(case when pm.Activity = 0 then isnull(pm.CW,0) * isnull(p.Sqm,0) else 0 end),
					Stp_LPPlan_SqmCmgt = sum(case when pm.Activity = 0 then isnull(pm.cmgt,0) * isnull(p.ReefSqm,0) else 0 end), 
					Stp_LPPlan_SqmCmgtTotal = sum(case when pm.Activity = 0 then isnull(pm.cmgt,0) * isnull(p.Sqm,0) else 0 end),
					Stp_LPPlan_Tons = sum(case when pm.Activity = 0 then isnull(p.Tons,0) else 0 end),
					Stp_LPPlan_TonsReef = sum(case when pm.Activity = 0 then isnull(p.ReefTons,0) else 0 end), 
					Stp_LPPlan_TonsWaste = sum(case when pm.Activity = 0 then isnull(p.WasteTons,0) else 0 end),
					Stp_LPPlan_Kg = sum(case when pm.Activity = 0 then isnull(p.Grams,0) / 1000 else 0 end),
					Stp_LPPlan_Cubics = max(case when p.Activity = 0 then isnull(p.Cubics,0) else 0 end), 
					Stp_LPPlan_CubicTons = max(case when p.Activity = 0 then isnull(p.CubicTons,0) else 0 end), 
					Stp_LPPlan_CubicGrams = max(case when p.Activity = 0 then isnull(p.CubicGrams,0) else 0 end), 
					
					Dev_LPPlan_AdvReef = sum(case when pm.Activity = 1 then isnull(p.ReefAdv,0) else 0 end), 
					Dev_LPPlan_AdvWaste = sum(case when pm.Activity = 1 then isnull(p.WasteAdv,0) else 0 end), 
					Dev_LPPlan_Primm = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
					Dev_LPPlan_Secm = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
					Dev_LPPlan_Adv = sum(case when pm.Activity = 1 then isnull(p.MetresAdvance,0) else 0 end), 
					Dev_LPPlan_TonsReef = sum(case when pm.Activity = 1 then isnull(p.ReefTons,0) else 0 end), 
					Dev_LPPlan_TonsWaste = sum(case when pm.Activity = 1 then isnull(p.WasteTons,0) else 0 end), 
					Dev_LPPlan_Tons = sum(case when pm.Activity = 1 then isnull(p.Tons,0) else 0 end), 
					Dev_LPPlan_KG = sum(case when pm.Activity = 1 then isnull(p.Grams,0) / 1000 else 0 end), 
					Dev_LPPlan_AdvEH = sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
								isnull(p.MetresAdvance,0) * isnull(pm.DHeight,0) else 0 end), 
					Dev_LPPlan_AdvEW= sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
								isnull(p.MetresAdvance,0) * isnull(pm.DWidth,0) else 0 end), 
					Dev_LPPlan_AdvCmgt = sum(case when pm.Activity = 1 and isnull(p.ReefAdv,0) > 0 then 
								isnull(p.ReefAdv,0) * isnull(p.Cmgt,0) else 0 end), 
					Dev_LPPlan_AdvCmgtTotal = sum(case when pm.Activity = 1 and isnull(p.MetresAdvance,0) > 0 then 
								isnull(p.MetresAdvance,0) * isnull(p.Cmgt,0) else 0 end), 
					Dev_LPPlan_Cubics = Sum(case when pm.Activity = 1 then isnull(p.Cubics,0) else 0 end), 
					Dev_LPPlan_CubicTons = Sum(case when pm.Activity = 1 then isnull(p.CubicTons,0) else 0 end), 
					Dev_LPPlan_CubicGrams = Sum(case when pm.Activity = 1 then isnull(p.CubicGrams,0) / 1000 else 0 end), 
					Dev_LPPlan_Labour = Sum(case when pm.Activity = 1 then isnull(pm.LabourStrength,0) else 0 end), 
					Dev_LPPlan_ShftInfo = Sum(0), 
					Dev_LPPlan_DrillRig = Sum(0), '

				set @LPlanSelect2 = '
					Stp_Meas_FL = sum(0),
					Stp_Meas_FLReef = sum(0),
					Stp_Meas_FLWaste = sum(0),
					Stp_Meas_Sqm = sum(0),
					Stp_Meas_SqmReef = sum(0),
					Stp_Meas_SqmWaste = sum(0),
					Stp_Meas_Adv = sum(0),
					Stp_Meas_AdvReef = sum(0),
					Stp_Meas_Advwaste = sum(0),
					Stp_Meas_SqmSW = sum(0),
					Stp_Meas_SqmCW = sum(0),
					Stp_Meas_SqmCMGT = sum(0 ),
					Stp_Meas_SqmCMGTTotal = sum(0 ),				
					Stp_Meas_Tons = sum(0), 
					Stp_Meas_TonsReef = sum(0.0), 
					Stp_Meas_TonsWaste = sum(0.0),
					Stp_Meas_Kg = sum(0), 
					Stp_Meas_Cubics = sum(0),
					Stp_Meas_CubicTons = sum(0), 
					Stp_Meas_CubicGrams = sum(0),

					Dev_Meas_AdvReef = sum(0),
					Dev_Meas_AdvWaste = sum(0),
					Dev_Meas_Primm = sum(0),
					Dev_Meas_Secm = sum(0),
					Dev_Meas_Adv = sum(0),
					Dev_Meas_TonsReef = sum(0),
					Dev_Meas_TonsWaste = sum(0), 
					Dev_Meas_Tons = sum(0),
					Dev_Meas_KG = sum(0),
					Dev_Meas_AdvEH = sum(0),
					Dev_Meas_AdvEW = sum(0),
					Dev_Meas_AdvCmgt = sum(0),
					Dev_Meas_AdvCmgtTotal = sum(0),
					Dev_Meas_Cubics = sum(0), 
					Dev_Meas_CubicTons = sum(0),
					Dev_Meas_CubicGrams = sum(0),
				
					BusFlag = max(''N''), 
					Stp_BPlan_FL = sum(0.000) ,
					Stp_BPlan_Sqm = sum(0.0) ,
					Stp_BPlan_SqmReef = sum(0.0) ,
					Stp_BPlan_SqmWaste = sum(0.0) ,
					Stp_BPlan_SqmSW = sum(0.0) ,
					Stp_BPlan_SqmCW = sum(0.0) ,
					Stp_BPlan_SqmCmgt = sum(0.0) , 
					Stp_BPlan_SqmCmgtTotal = sum(0.0) ,
					Stp_BPlan_Tons = sum(0) ,
					Stp_BPlan_TonsReef = sum(0.0) ,
					Stp_BPlan_TonsWaste = sum(0.0),
					Stp_BPlan_Kg = sum(0),

					Stp_BPlan_OSSqm = sum(0.0),
					Stp_BPlan_OSFSqm = sum(0.0),
					Stp_BPlan_REEFFL = sum(0.0),
					Stp_BPlan_OSFL = sum(0.0),     
					Stp_BPlan_CUBICS = sum(0),
					Stp_BPlan_SqmSWFAULT = sum(0.0),
					Stp_BPlan_FaultTons = sum(0),
					Stp_BPlan_OSTons = sum(0),
					Stp_BPlan_CUBICTons = sum(0),
					 
					Dev_BPlan_AdvReef = sum(0),
					Dev_BPlan_AdvWaste = sum(0),
					Dev_BPlan_Primm = sum(0),
					Dev_BPlan_Secm = sum(0), 
					Dev_BPlan_Adv = sum(0),
					Dev_BPlan_TonsReef = sum(0),
					Dev_BPlan_TonsWaste = sum(0),
					Dev_BPlan_Tons = sum(0),
					Dev_BPlan_KG = sum(0),
					Dev_BPlan_AdvEH = sum(0),
					Dev_BPlan_AdvEW =sum(0),
					Dev_BPlan_AdvCmgt = sum(0),
					Dev_BPlan_AdvCmgtTotal = sum(0),
					Dev_BPlan_Cubics = sum(0),
					Dev_BPlan_CubicTons = sum(0),
					Dev_BPlan_TonsCmgt = sum(0),
					
					ForeCast_SQM = sum(0) ,
					ForeCast_SQMDens = sum(0) ,
					ForeCast_Grams = sum(0) ,
					ForeCast_KG = sum(0) '
			set @LPlanFrom = '
				from Planmonth pm '
			IF (@PlanProgLock = 'Y')
			BEGIN
				set @LPlanFrom = @LPlanFrom + '
					left outer join Planning p on 
						p.Prodmonth = pm.Prodmonth and 
						p.SectionID = pm.Sectionid and 
						p.WorkplaceID = pm.Workplaceid and 
						p.Activity = pm.Activity and
						p.PlanCode = pm.PlanCode and
						p.IsCubics = pm.IsCubics and
						p. CalendarDate <= '''+@Calendardate+''''
			END
			set @LPlanFrom = @LPlanFrom + '
				inner join section_complete sc on  
					 sc.prodmonth = pm.prodmonth and  
					 sc.sectionid  = pm.sectionid  
				inner join Workplace w on  
					 w.WorkplaceID = pm.WorkplaceID 
				inner join CommonAreaSubSections CS on
					 cs.Id = w.SubSection 
				inner join CommonAreas c on
					 c.Id = cs.CommonArea '

			SET @LPlanWhere = 
				' where pm.PlanCode = ''LP'' and pm.IsCubics = ''N'' and'
			IF (@ReportType = 'P')
			BEGIN
				set @LPlanWhere = @LPlanWhere + ' pm.prodmonth = '''+@ProdMonth+''' ' 
			END
			
			IF (@ReportType = 'M')
			BEGIN
				set @LPlanWhere = @LPlanWhere + 
					' pm.prodmonth >= '''+@FromMonth+''' and 
						pm.prodmonth <= '''+@ToMonth+''' ' 
			END
			
			--IF (@TheStopeLedge = 0)
			--BEGIN
			--	set @LPlanWhere = @LPlanWhere + ' and pm.Activity IN(0,3) '
			--END
			--IF (@TheStopeLedge = 1)
			--BEGIN
			--	set @LPlanWhere = @LPlanWhere + ' and pm.Activity = 0 '
			--END
			--IF (@TheStopeLedge = 2)
			--BEGIN
			--	set @LPlanWhere = @LPlanWhere + ' and pm.Activity = 3 '
			--END

			set @LPlanWhere = @LPlanWhere + '
				and '+@ReadSection+' = '''+@SectionID+'''
				group by  
					pm.Prodmonth, sc.SectionID_5, sc.Name_5, sc.Name_4, sc.SectionID_4, 
					sc.SectionID_3, sc.Name_3, sc.SectionID_2, sc.Name_2,
					sc.SectionID_1, sc.Name_1, sc.SectionID, sc.Name, 
					pm.workplaceid, w.Description, pm.Activity '
		END
	END
	IF ( @BussPlan= 'Y') -- and ( (@Meas = '1') or (@DailyPlan= '1') or (@ProgPlan = '1') or (@Book = '1'))
	BEGIN
		set @BusUnion = ' union all '
		set @BusSelect = '	 
			select 
				NAME_5,NAME_4,NAME_3,NAME_2, NAME_1,  NAME, 
				0 prodmonth, workplaceid,  Activity,
				Plan_Stope = sum(0),  
				Plan_Dev = sum(0), 
				Stp_Plan_FL = sum(0), 
				Stp_Plan_FLReef = sum(0), 
				Stp_Plan_FLWaste = sum(0), 
				Stp_Plan_Sqm = sum(0),
				Stp_Plan_SqmReef = sum(0),
				Stp_Plan_SqmWaste = sum(0),
				Stp_Plan_Adv = sum(0),
				Stp_Plan_AdvReef = sum(0),
				Stp_Plan_AdvWaste = sum(0),
				Stp_Plan_SqmSW = sum(0),
				Stp_Plan_SqmCW = sum(0),
				Stp_Plan_SqmCmgt = sum(0),
				Stp_Plan_SqmCmgtTotal = sum(0),
				Stp_Plan_Tons = sum(0),
				Stp_Plan_TonsReef = sum(0),
				Stp_Plan_TonsWaste = sum(0),
				Stp_Plan_Kg = sum(0),
				Stp_Plan_Cubics = sum(0), 
				Stp_Plan_CubicTons = sum(0),
				Stp_Plan_CubicGrams = sum(0),

				Dev_Plan_AdvReef = sum(0),
				Dev_Plan_AdvWaste = sum(0),
				Dev_Plan_Primm = sum(0),
				Dev_Plan_Secm = sum(0),
				Dev_Plan_Adv = sum(0),
				Dev_Plan_TonsReef = sum(0),
				Dev_Plan_TonsWaste = sum(0),
				Dev_Plan_Tons = sum(0),
				Dev_Plan_KG = sum(0),
				Dev_Plan_AdvEH = sum(0),
				Dev_Plan_AdvEW = sum(0),
				Dev_Plan_AdvCmgt = sum(0),
				Dev_Plan_AdvCmgtTotal = sum(0),
				Dev_Plan_Cubics = sum(0), 
				Dev_Plan_CubicTons = sum(0),
				Dev_Plan_CubicGrams = sum(0),
				Dev_Plan_Labour = sum(0),
				Dev_Plan_ShftInfo = sum(0),
				Dev_Plan_DrillRig = sum(0),

				Stp_PPlan_FL = sum(0), 
				Stp_PPlan_FLReef = sum(0), 
				Stp_PPlan_FLWaste = sum(0), 
				Stp_PPlan_Sqm = sum(0),
				Stp_PPlan_SqmReef = sum(0),
				Stp_PPlan_SqmWaste = sum(0),
				Stp_PPlan_Adv = sum(0),
				Stp_PPlan_AdvReef = sum(0),
				Stp_PPlan_AdvWaste = sum(0),
				Stp_PPlan_SqmSW = sum(0),
				Stp_PPlan_SqmCW = sum(0),
				Stp_PPlan_SqmCmgt = sum(0),
				Stp_PPlan_SqmCmgtTotal = sum(0),
				Stp_PPlan_Tons = sum(0),
				Stp_PPlan_TonsReef = sum(0),
				Stp_PPlan_TonsWaste = sum(0),
				Stp_PPlan_Kg = sum(0),
				Stp_PPlan_Cubics = sum(0), 
				Stp_PPlan_CubicTons = sum(0),
				Stp_PPlan_CubicGrams = sum(0),

				Dev_PPlan_AdvReef = sum(0),
				Dev_PPlan_AdvWaste = sum(0), 
				Dev_PPlan_Primm = sum(0),
				Dev_PPlan_Secm = sum(0),
				Dev_PPlan_Adv = sum(0),
				Dev_PPlan_TonsReef = sum(0), 
				Dev_PPlan_TonsWaste = sum(0), 
				Dev_PPlan_Tons = sum(0), 
				Dev_PPlan_KG = sum(0),
				Dev_PPlan_AdvEH = sum(0),
				Dev_PPlan_AdvEW = sum(0),
				Dev_PPlan_AdvCmgt = sum(0),
				Dev_PPlan_AdvCmgtTotal = sum(0), 
				Dev_PPlan_Cubics = sum(0),
				Dev_PPlan_CubicTons = sum(0),
				Dev_PPlan_CubicGrams = sum(0),
				Dev_PPlan_Labour = sum(0),
				Dev_PPlan_ShftInfo = sum(0), 
				Dev_PPlan_DrillRig = sum(0), 
			
				Stp_Book_FL = sum(0),
				Stp_Book_FLReef = sum(0),
				Stp_Book_FLWaste = sum(0),
				Stp_Book_Sqm = sum(0),
				Stp_Book_SqmReef = sum(0), 
				Stp_Book_SqmWaste = sum(0),
				Stp_Book_Adv = sum(0),
				Stp_Book_AdvReef = sum(0), 
				Stp_Book_AdvWaste = sum(0),
				Stp_Book_SqmSW = sum(0),
				Stp_Book_SqmCW = sum(0), 
				Stp_Book_SqmCmgt = sum(0), 
				Stp_Book_SqmCmgtTotal = sum(0),
				Stp_Book_Tons = sum(0),
				Stp_Book_TonsReef = sum(0),
				Stp_Book_TonsWaste = sum(0),
				Stp_Book_KG = sum(0),
				Stp_Book_Cubics = sum(0), 
				Stp_Book_CubicTons = sum(0),
				Stp_Book_CubicGrams = sum(0),

				Dev_Book_AdvReef = sum(0),
				Dev_Book_AdvWaste = sum(0),
				Dev_Book_Primm = sum(0),
				Dev_Book_Secm = sum(0), 
				Dev_Book_Adv = sum(0), 
				Dev_Book_TonsReef = sum(0),
				Dev_Book_TonsWaste = sum(0),
				Dev_Book_Tons = sum(0), 
				Dev_Book_KG = sum(0), 
				Dev_Book_AdvEH = sum(0), 
				Dev_Book_AdvEW = sum(0),
				Dev_Book_AdvCmgt = sum(0),
				Dev_Book_AdvCmgtTotal = sum(0),
				Dev_Book_Cubics = sum(0),
				Dev_Book_CubicTons = sum(0),
				Dev_Book_CubicGrams = sum(0),
				--Dev_Book_Labour = sum(0), 
				--Dev_Book_ShftInfo = sum(0),
				
				Stp_LPlan_FL = sum(0),
				Stp_LPlan_FLReef = sum(0),
				Stp_LPlan_FLWaste = sum(0),
				Stp_LPlan_Sqm = sum(0),
				Stp_LPlan_SqmReef = sum(0),
				Stp_LPlan_SqmWaste = sum(0), 
				Stp_LPlan_Adv = sum(0),
				Stp_LPlan_AdvReef = sum(0),
				Stp_LPlan_AdvWaste = sum(0),
				Stp_LPlan_SqmSW = sum(0),
				Stp_LPlan_SqmCW = sum(0),
				Stp_LPlan_SqmCmgt = sum(0),
				Stp_LPlan_SqmCmgtTotal = sum(0),
				Stp_LPlan_Tons = sum(0),
				Stp_LPlan_TonsReef = sum(0),
				Stp_LPlan_TonsWaste = sum(0),
				Stp_LPlan_Kg = sum(0),
				Stp_LPlan_Cubics = sum(0), 
				Stp_LPlan_CubicTons = sum(0),
				Stp_LPlan_CubicGrams = sum(0),

				Dev_LPlan_AdvReef = sum(0),
				Dev_LPlan_AdvWaste = sum(0),
				Dev_LPlan_Primm = sum(0),
				Dev_LPlan_Secm = sum(0),
				Dev_LPlan_Adv = sum(0),
				Dev_LPlan_TonsReef = sum(0),
				Dev_LPlan_TonsWaste = sum(0),
				Dev_LPlan_Tons = sum(0),
				Dev_LPlan_KG = sum(0),
				Dev_LPlan_AdvEH = sum(0),
				Dev_LPlan_AdvEW = sum(0),
				Dev_LPlan_AdvCmgt = sum(0),
				Dev_LPlan_AdvCmgtTotal = sum(0),
				Dev_LPlan_Cubics = sum(0),
				Dev_LPlan_CubicTons = sum(0),
				Dev_LPlan_CubicGrams = sum(0),
				Dev_LPlan_Labour = sum(0),
				Dev_LPlan_ShftInfo = sum(0),
				Dev_LPlan_DrillRig = sum(0),

				Stp_LPPlan_FL = sum(0),
				Stp_LPPlan_FLReef = sum(0),
				Stp_LPPlan_FLWaste = sum(0),
				Stp_LPPlan_Sqm = sum(0),
				Stp_LPPlan_SqmReef = sum(0),
				Stp_LPPlan_SqmWaste = sum(0), 
				Stp_LPPlan_Adv = sum(0),
				Stp_LPPlan_AdvReef = sum(0), 
				Stp_LPPlan_AdvWaste = sum(0),
				Stp_LPPlan_SqmSW = sum(0),
				Stp_LPPlan_SqmCW = sum(0),
				Stp_LPPlan_SqmCmgt = sum(0),
				Stp_LPPlan_SqmCmgtTotal = sum(0),
				Stp_LPPlan_Tons = sum(0),
				Stp_LPPlan_TonsReef = sum(0),
				Stp_LPPlan_TonsWaste = sum(0),
				Stp_LPPlan_Kg = sum(0),
				Stp_LPPlan_Cubics = sum(0), 
				Stp_LPPlan_CubicTons = sum(0),
				Stp_LPPlan_CubicGrams = sum(0),

				Dev_LPPlan_AdvReef = sum(0),
				Dev_LPPlan_AdvWaste = sum(0),
				Dev_LPPlan_Primm = sum(0),
				Dev_LPPlan_Secm = sum(0),
				Dev_LPPlan_Adv = sum(0),
				Dev_LPPlan_TonsReef = sum(0),
				Dev_LPPlan_TonsWaste = sum(0), 
				Dev_LPPlan_Tons = sum(0),
				Dev_LPPlan_KG = sum(0),
				Dev_LPPlan_AdvEH = sum(0),
				Dev_LPPlan_AdvEW= sum(0),
				Dev_LPPlan_AdvCmgt = sum(0), 
				Dev_LPPlan_AdvCmgtTotal = sum(0), 
				Dev_LPPlan_Cubics = sum(0),
				Dev_LPPlan_CubicTons = sum(0),
				Dev_LPPlan_CubicGrams = sum(0),
				Dev_LPPlan_Labour =sum(0),
				Dev_LPPlan_ShftInfo = sum(0),
				Dev_LPPlan_DrillRig = sum(0), 
				
				Stp_Meas_FL = sum(0),
				Stp_Meas_FLReef = sum(0),
				Stp_Meas_FLWaste = sum(0),
				Stp_Meas_Sqm = sum(0),
				Stp_Meas_SqmReef = sum(0),
				Stp_Meas_SqmWaste = sum(0),
				Stp_Meas_Adv = sum(0),
				Stp_Meas_AdvReef = sum(0),
				Stp_Meas_Advwaste = sum(0),
				Stp_Meas_SqmSW = sum(0),
				Stp_Meas_SqmCW = sum(0),
				Stp_Meas_SqmCMGT = sum(0 ),
				Stp_Meas_SqmCMGTTotal = sum(0 ),				
				Stp_Meas_Tons = sum(0), 
				Stp_Meas_TonsReef = sum(0.0), 
				Stp_Meas_TonsWaste = sum(0.0),
				Stp_Meas_Kg = sum(0), 
				Stp_Meas_Cubics = sum(0),
				Stp_Meas_CubicTons = sum(0), 
				Stp_Meas_CubicGrams = sum(0), '
				
			set @BusSelect1 = '
				Dev_Meas_AdvReef = sum(0),
				Dev_Meas_AdvWaste = sum(0),
				Dev_Meas_Primm = sum(0),
				Dev_Meas_Secm = sum(0),
				Dev_Meas_Adv = sum(0),
				Dev_Meas_TonsReef = sum(0),
				Dev_Meas_TonsWaste = sum(0), 
				Dev_Meas_Tons = sum(0),
				Dev_Meas_KG = sum(0),
				Dev_Meas_AdvEH = sum(0),
				Dev_Meas_AdvEW = sum(0),
				Dev_Meas_AdvCmgt = sum(0),
				Dev_Meas_AdvCmgtTotal = sum(0),
				Dev_Meas_Cubics = sum(0), 
				Dev_Meas_CubicTons = sum(0),
				Dev_Meas_CubicGrams = sum(0),
				 
				BusFlag = max(''Y''), '
			
			IF (@TheSTopeLedge = 0) 
			BEGIN
				set @BusSelect1 = @BusSelect1 + '
				  Stp_BPlan_FL = max(StopeFL),
				  Stp_BPlan_Sqm = max(StpSqm), 
    			  Stp_BPlan_SqmReef = max(StpSqmReef),
				  Stp_BPlan_SqmWaste = max(StpSqmWaste),
				  Stp_BPlan_SqmSW = max(StpSqmSW), 
    			  Stp_BPlan_SqmCW = sum(0.0),
				  Stp_BPlan_SqmCmgt = max(StpSqmCmgt),
				  Stp_BPlan_SqmCmgtTotal = max(0),
				  Stp_BPlan_Tons = max(StpTons), 
    			  Stp_BPlan_ReefTons = max(StpReefTons),
				  Stp_BPlan_WasteTons = max(StpReefTons),
				  Stp_BPlan_Kg = max(StpContent),
				  Stp_BPlan_OSSqm = sum(0.0),
				  Stp_BPlan_OSFSqm = sum(0.0),
				  Stp_BPlan_REEFFL = sum(0.0), 
				  Stp_BPlan_OSFL = sum(0.0),
				  Stp_BPlan_CUBICS = sum(0),
				  Stp_BPlan_SqmSWFAULT = sum(0.0),       
				  Stp_BPlan_FaultTons = sum(0),
				  Stp_BPlan_OSTons = sum(0),
				  Stp_BPlan_CUBICTons = sum(0), '
			END
			IF (@TheSTopeLedge = 1) 
			BEGIN
				set @BusSelect1 = @BusSelect1 + '
				  Stp_BPlan_FL = max(StopeFL),
				  Stp_BPlan_Sqm = max(StpSqm), 
    			  Stp_BPlan_SqmReef = max(StpSqmReef),
				  Stp_BPlan_SqmWaste = max(StpSqmWaste),
				  Stp_BPlan_SqmSW = max(StpSqmSW), 
    			  Stp_BPlan_SqmCW = sum(0.0),
				  Stp_BPlan_SqmCmgt = max(StpSqmCmgt),
				  Stp_BPlan_SqmCmgtTotal = max(StpSqmCmgtTotal),
				  Stp_BPlan_Tons = max(StpTons), 
    			  Stp_BPlan_ReefTons = max(StpTonsReef),
				  Stp_BPlan_WasteTons = max(StpTonsReef),
				  Stp_BPlan_Kg = max(StpContent),
				  Stp_BPlan_OSSqm = sum(0.0),
				  Stp_BPlan_OSFSqm = sum(0.0),
				  Stp_BPlan_REEFFL = sum(0.0), 
				  Stp_BPlan_OSFL = sum(0.0),
				  Stp_BPlan_CUBICS = sum(0),
				  Stp_BPlan_SqmSWFAULT = sum(0.0),       
				  Stp_BPlan_FaultTons = sum(0),
				  Stp_BPlan_OSTons = sum(0),
				  Stp_BPlan_CUBICTons = sum(0), '
			END
			IF (@TheSTopeLedge = 2) 
			BEGIN
				set @BusSelect1 = @BusSelect1 + '
				  --Stp_BPlan_FL = max(StopeFL),
				  --Stp_BPlan_Sqm = max(StpSqm), 
    		--	  Stp_BPlan_OnSqm = max(StpReefSqm),
				  --Stp_BPlan_OffSqm = max(StpWasteSqm),
				  --Stp_BPlan_SqmSW = max(StpSqmSW), 
    		--	  Stp_BPlan_SqmCW = sum(0.0),
				  --Stp_BPlan_SqmCmgt = max(StpSqmCmgt),
				  --Stp_BPlan_Tons = max(StpTons), 
    		--	  Stp_BPlan_OnTons = max(StpOnReefTons),
				  --Stp_BPlan_OffTons = max(StpOffReefTons),
				  --Stp_BPlan_Kg = max(StpContent),

				   Stp_BPlan_FL = sum(0.0),
				  Stp_BPlan_Sqm = sum(0.0), 
    			  Stp_BPlan_SqmReef = sum(0.0),
				  Stp_BPlan_SqmWaste = sum(0.0),
				  Stp_BPlan_SqmSW = sum(0.0), 
    			  Stp_BPlan_SqmCW = sum(0.0),
				  Stp_BPlan_SqmCmgt = sum(0.0),
				  Stp_BPlan_SqmCmgtTotal = sum(0.0),
				  Stp_BPlan_Tons = sum(0), 
    			  Stp_BPlan_ReefTons = sum(0.0),
				  Stp_BPlan_WasteTons = sum(0.0),
				  Stp_BPlan_Kg = sum(0),
				  Stp_BPlan_OSSqm = sum(0.0),
				  Stp_BPlan_OSFSqm = sum(0.0),
				  Stp_BPlan_REEFFL = sum(0.0), 
				  Stp_BPlan_OSFL = sum(0.0),
				  Stp_BPlan_CUBICS = sum(0),
				  Stp_BPlan_SqmSWFAULT = sum(0.0),       
				  Stp_BPlan_FaultTons = sum(0),
				  Stp_BPlan_OSTons = sum(0),
				  Stp_BPlan_CUBICTons = sum(0), '
			END
			set @BusSelect1 = @BusSelect1 + '				  				 
				Dev_BPlan_AdvReef = max(ReefMadv),
				Dev_BPlan_AdvWaste = max(WasteMAdv),
				Dev_BPlan_Primm = sum(0),
				Dev_BPlan_Secm = sum(0), 
				Dev_BPlan_Adv = max(MAdv),
				Dev_BPlan_TonsReef = max(DevOnTons),
				Dev_BPlan_TonsWaste = max(devOffTons),
				Dev_BPlan_Tons = max(DevTons), 
				Dev_BPlan_KG = max(DevGrams),
				Dev_BPlan_AdvEH = max(DevRAdvHeight),
				Dev_BPlan_AdvEW = max(DevRAdvWidth),
				Dev_BPlan_AdvCmgt = max(DevRAdvCmgt),  
				Dev_BPlan_AdvCmgtTotal = max(DevRAdvCmgt),  
				Dev_BPlan_Cubics = sum(0),
				Dev_BPlan_CubicTons = sum(0), 
				Dev_BPlan_TonsCmgt = max(DevTonsCmgt),
				
				ForeCast_SQM = sum(0) ,
				ForeCast_SQMDens = sum(0) ,
				ForeCast_Grams = sum(0) ,
				ForeCast_KG = sum(0) '			
			set @BusSelect1 = @BusSelect1 + ' from ( 
				select  NAME_5,NAME_4,NAME_3,
					NAME_2,'''' NAME_1, '''' NAME, 
					'''' workplaceid, 1 Activity,	
					StpSqm = sum(StpSqmReef) + sum(StpSqmWaste),	
					StpSqmReef = sum(StpSqmReef),	
					StpSqmWaste = sum(StpSqmWaste),
					StopeFL = sum(StopeFL),	
					StopeSW = sum(StopeSW),	
					StpReefTons = sum(StpTonsReef),	
					StpWasteTons = sum(StpTonswaste),		
					StpTons = sum(StpTons),	
					StpContent = sum(StpContent),
					StpSqmSW = sum(StpSqmSW),
					StpSqmCmgt = sum(StpSqmCmgt),
					StpSqmCmgtTotal = sum(StpSqmCmgtTotal),
					
					ReefMadv=sum(ReefMadv),
					WasteMAdv=sum(WasteMAdv),
					MAdv=sum(ReefMadv)+sum(WasteMAdv),
					DevOnTons = sum(DevOnTons),	 
					DevOffTons = sum(DevOffTons),
					DevTons=sum(DevTons),
					DevGrams=sum(DevGrams),					
					DevDensity=max(DevDensity),
					DevTonsCmgt = sum(DevTonsCmgt),
					DevRAdvCmgt = sum(DevRAdvCmgt),
					DevRAdvWidth=sum(DevRAdvWidth)+sum(DevWAdvWidth),
					DevRAdvHeight=sum(DevRAdvHeight)+sum(DevWAdvHeight),
					EquivalentMetres=sum(0)	'
			set @BusFrom = '		
				from
				(

					select '
					IF (@RunLevel = 1)
						set @BusFrom =  @BusFrom +'sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
							sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,' 

					IF (@RunLevel = 2)
						set @BusFrom = @BusFrom +''''' NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
							sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,' 

					IF (@RunLevel = 3)
						set @BusFrom = @BusFrom +'  
							'''' NAME_5, '''' NAME_4,sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,' 

					IF (@RunLevel = 4)
						set @BusFrom = @BusFrom +''''' NAME_5, '''' NAME_4, '''' NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,'
					set @BusFrom = @BusFrom +'
						fiscal_period,bus.prodmonth,Project_Task,Act,
						StpSqmReef = sum(ReefSqm),	
						StpSqmWaste = sum(WasteSqm)	+ sum(FaultSqm),
						StopeFL = sum(FL),	
						StopeSW = sum(Stopewidth),	
						StpTonsReef = sum(ReefSqm) * sum(Stopewidth)/100 * sum(StpDensity),		
						StpTonsWaste = (sum(WasteSqm) + sum(FaultSqm)) * sum(Stopewidth)/100 * sum(StpDensity),		
						StpTons = (sum(ReefSqm) + sum(WasteSqm) + sum(FaultSqm)) * sum(Stopewidth)/100 * sum(StpDensity),	
						StpContent = sum(ReefSqm) / 100 * sum(StpDensity) * sum(CMGT),
						StpSqmSW = sum(ReefSqm) * sum(Stopewidth),
						StpSqmCmgt = sum(ReefSqm) * sum(cmgt),
						StpSqmCmgtTotal = (sum(ReefSqm) + sum(WasteSqm)) * sum(cmgt),
						StpDensity = sum(StpDensity),
						
						ReefMadv=sum(Convert(numeric(10,5),ReefMadv)) + sum(ReefCapMAdv),
						WasteMAdv=sum(WasteMAdv) + sum(WasteCapMAdv),
						MAdv=sum(ReefMadv)+sum(WasteMAdv) + sum(ReefCapMAdv) + sum(WasteCapMAdv),
						DevOnTons = (sum(ReefMadv)+SUM(ReefCapMadv )) * sum(DevHeight) * sum(DevWidth) * (select top 1 density from workplace), 				   	 
						DevOffTons =case when sum(WasteMAdv) >0 or sum(WasteCapMAdv)>0 then sum(DevTons) else 0 end,
						DevTons=sum(DevTons),
						DevGrams=sum(DevGrams),					
						DevDensity=(select top 1 density from workplace),
						DevWidth=sum(DevWidth),
						DevHeight=sum(devHeight),
						DevTonsCmgt = sum(DevTons) * sum(Devcmgt),
						DevRAdvCmgt = sum(Convert(numeric(10,5),ReefMadv)) * sum(Convert(numeric(10,5),Devcmgt)),
						DevRAdvWidth=sum(Convert(numeric(10,5),ReefMadv)) * sum(Convert(numeric(10,5),DevWidth)),
						DevRAdvHeight=sum(Convert(numeric(10,5),ReefMadv)) * sum(Convert(numeric(10,5),DevHeight)),
						DevWAdvWidth=sum(Convert(numeric(10,5),WasteMAdv)) * sum(Convert(numeric(10,5),DevWidth)),
						DevWAdvHeight=sum(Convert(numeric(10,5),WasteMAdv)) * sum(Convert(numeric(10,5),DevHeight)),
						DevRCapAdvWidth=sum(Convert(numeric(10,5),ReefCapMadv)) * sum(Convert(numeric(10,5),DevWidth)),
						DevRCapAdvHeight=sum(Convert(numeric(10,5),ReefCapMadv)) * sum(Convert(numeric(10,5),DevHeight)),
						DevWCapAdvWidth=sum(Convert(numeric(10,5),WasteCapMAdv)) * sum(Convert(numeric(10,5),DevWidth)),
						DevWCapAdvHeight=sum(Convert(numeric(10,5),WasteCapMAdv)) * sum(Convert(numeric(10,5),DevHeight)),
						EquivalentMetres=sum(0) 	'
				set @BusWhere = '  
					from 
					(
						select project_no MOID, fiscal_period,left(fiscal_period,4) + 
							(case when right(fiscal_period,3) = ''Sep'' then ''09''  
								when right(fiscal_period,3) = ''Oct'' then ''10'' 
								when right(fiscal_period,3) = ''Nov'' then ''11''
								when right(fiscal_period,3) = ''Dec'' then ''12'' 
								when right(fiscal_period,3) = ''Jan'' then ''01''
								when right(fiscal_period,3) = ''Feb'' then ''02''
								when right(fiscal_period,3) = ''Mar'' then ''03'' 
								when right(fiscal_period,3) = ''Apr'' then ''04''
								when right(fiscal_period,3) = ''Aug'' then ''08''
								when right(fiscal_period,3) = ''Jul'' then ''07''
								when right(fiscal_period,3) = ''May'' then ''05''
								when right(fiscal_period,3) = ''Jun'' then ''06''   end)  prodmonth,
							Act = 0,
							BUD_VAL_TYPE ,
							PROJECT_TASK ,
							ReefSqm = case when Bud_Val_Type = ''Reef M2'' then Convert(numeric(10,5), Bud_val) else 0 end,
							WasteSqm = case when Bud_Val_Type = ''Off Reef M2'' then Convert(numeric(10,5), Bud_val) else 0 end,
							FaultSqm = case when Bud_Val_Type = ''Fault M2'' then Convert(numeric(10,5), Bud_val) else 0 end,
							FL = case when Bud_Val_Type = ''Face Length'' then  Bud_Val else 0 end,
							Stopewidth = case when Bud_Val_Type = ''Stoping WIdth'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							StpDensity = case when Bud_Val_Type = ''Relative Density'' then Convert(numeric(10,5), Bud_Val) else 0 end,				
							cmgt = case when Bud_Val_Type = ''Stoping - Block Val (Conv On Reef cmg/t)'' then Convert(numeric(10,5), Bud_Val) else 0 end,

							ReefMadv = case when Bud_Val_Type = ''Reef Ongoing Metres'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							WasteMAdv = case when Bud_Val_Type = ''Waste Ongoing Metres'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							ReefCapMadv = case when Bud_Val_Type = ''Reef Capital Metres'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							WasteCapMAdv = case when Bud_Val_Type = ''Waste Capital Metres'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							DevTons = case when Bud_Val_Type = ''Tonnes'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							Devcmgt = case when Bud_Val_Type = ''Dev Cmg/t'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							DevGrams = case when Bud_Val_Type = ''Kg'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							DevWidth = case when Bud_Val_Type = ''OP - On Reef Dev Width (m)'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							DevHeight = case when Bud_Val_Type = ''OP - On Reef Dev Height (m)'' then Convert(numeric(10,5), Bud_Val) else 0 end,
							DevDensity = case when Bud_Val_Type = ''Relative Density'' then Convert(numeric(10,5), Bud_Val) else 0 end
						from TM1Import '
						IF (@ReportType = 'M')
						BEGIN
							set @BusWhere = @BusWhere + 
								' where left(fiscal_period,4) + 
								(case when right(fiscal_period,3) = ''Sep'' then ''09''
									when right(fiscal_period,3) = ''Oct'' then ''10'' 
									when right(fiscal_period,3) = ''Nov'' then ''11''
									when right(fiscal_period,3) = ''Dec'' then ''12''
									when right(fiscal_period,3) = ''Jan'' then ''01'' 
									when right(fiscal_period,3) = ''Feb'' then ''02''
									when right(fiscal_period,3) = ''Mar'' then ''03''
									when right(fiscal_period,3) = ''Apr'' then ''04''
									when right(fiscal_period,3) = ''May'' then ''05''
									when right(fiscal_period,3) = ''Jun'' then ''06''
									when right(fiscal_period,3) = ''Jul'' then ''07''
									else ''08'' end)  >= '''+ @FromMonth +''' and
									left(fiscal_period,4) + 
								(case when right(fiscal_period,3) = ''Sep'' then ''09''
									when right(fiscal_period,3) = ''Oct'' then ''10'' 
									when right(fiscal_period,3) = ''Nov'' then ''11''
									when right(fiscal_period,3) = ''Dec'' then ''12''
									when right(fiscal_period,3) = ''Jan'' then ''01'' 
									when right(fiscal_period,3) = ''Feb'' then ''02''
									when right(fiscal_period,3) = ''Mar'' then ''03''
									when right(fiscal_period,3) = ''Apr'' then ''04''
									when right(fiscal_period,3) = ''May'' then ''05''
									when right(fiscal_period,3) = ''Jun'' then ''06''
									when right(fiscal_period,3) = ''Jul'' then ''07''
									else ''08'' end)  <= '''+ @ToMonth +''' '
						END
						ELSE
						BEGIN
							set @BusWhere = @BusWhere + 
								' where left(fiscal_period,4) + 
								(case when right(fiscal_period,3) = ''Sep'' then ''09''
									when right(fiscal_period,3) = ''Oct'' then ''10'' 
									when right(fiscal_period,3) = ''Nov'' then ''11''
									when right(fiscal_period,3) = ''Dec'' then ''12''
									when right(fiscal_period,3) = ''Jan'' then ''01'' 
									when right(fiscal_period,3) = ''Feb'' then ''02''
									when right(fiscal_period,3) = ''Mar'' then ''03''
									when right(fiscal_period,3) = ''Apr'' then ''04''
									when right(fiscal_period,3) = ''May'' then ''05''
									when right(fiscal_period,3) = ''Jun'' then ''06''
									when right(fiscal_period,3) = ''Jul'' then ''07''
									else ''08'' end)  = '''+ @FromMonth +''' '
						END 
						set @BusWhere = @BusWhere + 
					') bus
					inner join section s on
						s.prodmonth=bus.prodmonth and
						s.OpsPlanLink =bus.MOID 
					inner join Section_Complete_MO  sc on
						sc.SectionID_2 =s.SectionID and
						sc.Prodmonth = bus.prodmonth '
					IF (@RunLevel = 1)
						set @BusWhere = @BusWhere + ' where sc.SectionID_5 = '''+@SectionID+''' '  

					IF (@RunLevel = 2)
						set @BusWhere = @BusWhere + ' where sc.SectionID_4 = '''+@SectionID+''' '

					IF (@RunLevel = 3)
						set @BusWhere = @BusWhere + ' where sc.SectionID_3 = '''+@SectionID+''' '

					IF (@RunLevel = 4)
						set @BusWhere = @BusWhere + ' where sc.SectionID_2 = '''+@SectionID+''' '

					set @BusWhere = @BusWhere + '
					group by sc.SectionID_5,sc.Name_5,sc.SectionID_4,sc.Name_4,sc.SectionID_3,sc.Name_3,
							 sc.SectionID_2,sc.Name_2, fiscal_period,bus.prodmonth,Project_Task,Act
				) bus1
				group by  Name_5, Name_4, name_3, Name_2,prodmonth
			) bus2 
		group by NAME_5,NAME_4,NAME_3,NAME_2, NAME_1,  NAME,  workplaceid,  Activity '
	END
END
set @GroupBy = '
		) a group by  
			a.Prodmonth,  a.Name_5, a.Name_4, 
				a.Name_3, a.Name_2,
			a.Name_1, a.Name, 
			a.workplace with rollup '--) b'


--IF (@TheReportType = 'StpDaytoDay') 
--BEGIN

--	set @TheQuery4 =  ' select  '

--IF (@TheLevel = 1)
--		set @TheQuery4 = 'select sc.SectionID_5+'':''+sc.Name_5 NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
--		sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2,  sc.SectionID_1+'':''+sc.Name_1 NAME_1,
--		sc.SectionID+'':''+sc.Name NAME, '   

--	IF (@TheLevel = 2)
--		set @TheQuery4 =  'select '''' NAME_5,sc.SectionID_4+'':''+sc.Name_4 NAME_4,
--			sc.SectionID_3+'':''+sc.Name_3 NAME_3,sc.SectionID_2+'':''+sc.Name_2 NAME_2, '''' NAME_1,'''' NAME, '    

--	IF (@TheLevel = 3)
--		set @TheQuery4 =  'select   
--			'''' NAME_5, '''' NAME_4,sc.SectionID_3+'':''+sc.Name_3 NAME_3,
--			sc.SectionID_2+'':''+sc.Name_2 NAME_2, '''' NAME_1,'''' NAME, ' 

--	IF (@TheLevel = 4)
--		set @TheQuery4 = 'select '''' NAME_5, '''' NAME_4, '''' NAME_3,
--				sc.SectionID_2+'':''+sc.Name_2 NAME_2, '''' NAME_1,'''' NAME, ' 

--	IF (@TheLevel = 5)
--		set @TheQuery4 = 'select '''' NAME_5, '''' NAME_4,''''NAME_3, '''' NAME_2, '''' NAME_1,'''' NAME, '

--	IF (@TheLevel = 6)
--		set @TheQuery4 = 'select '''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1, '''' NAME,'         

--	IF (@TheLevel = 7)
--		set @TheQuery4  =  ''''' NAME_5, '''' NAME_4, '''' NAME_3, ''Total Mine'' NAME_2,c.Name NAME_1,cs.Name NAME, '    

--	IF (@TheLevel = 8)
--		set @TheQuery4  =  ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,c.Name NAME_1, cs.Name NAME, '    

--	IF (@TheLevel = 9)
--		set @TheQuery4  = ''''' NAME_5, '''' NAME_4, '''' NAME_3, '''' NAME_2,'''' NAME_1, cs.Name NAME, '   

--	set @TheQuery4 = @TheQuery4 + 'p.Workplaceid+'':''+w.Description workplaceid,  
--			Stp_PPlan_FL = max(case when p.Sqm > 0 then pm.FL else 0 end),
--			Stp_PPlan_Sqm = sum(p.Sqm), 
--			Stp_PPlan_OnSqm = sum(p.SqmReef), 
--			Stp_PPlan_OffSqm = sum(p.SqmWaste),  
--			Stp_PPlan_SqmSW = sum(case when p.Sqm > 0 then pm.SW*p.Sqm else 0 end), 
--			Stp_PPlan_SqmCW = sum(case when p.Sqm > 0 then pm.CW*p.Sqm else 0 end), 
--			Stp_PPlan_SqmCmgt = sum(case when p.Sqmreef > 0 then pm.gt*p.SqmReef else 0 end), 
--			cast(sum(p.Tons)  as decimal(18,4)) Stp_PPlan_Tons, 
--			cast(sum(p.TonsReef)  as decimal(18,4)) Stp_PPlan_OnTons, 
--			cast(sum(p.TonsWaste)  as decimal(18,4)) Stp_PPlan_OffTons, 
--			cast(sum(p.Content / 1000)  as decimal(18,4)) Stp_PPlan_KG, 
--			Stp_Book_FL = max(case when (p.BookSqm > 0) then (p.BookFL) else 0 end), 
--			Stp_Book_Sqm = sum(p.BookSqm), 
--			Stp_Book_OnSqm = sum(p.BookSqmReef), 
--			Stp_Book_OffSqm = sum(p.BookSqmWaste),
--			Stp_Book_SqmSW = sum(p.BookSW*p.BookSqm), 
--			Stp_Book_SqmCW = sum(pm.CW*p.BookSqm), 
--			Stp_Book_SqmCmgt = sum(p.BookCmgt*p.BookSqmReef), 
--			Stp_Book_Tons = sum(p.BookTons), 
--			Stp_Book_OnTons = sum(p.BookTonsReef), 
--			Stp_Book_OffTons = sum(p.BookTonsWaste), 
--			Stp_Book_KG = sum(p.BookGrams / 1000),'
--	set @TheQuery4 = @TheQuery4 + '    
--			CONVERT(decimal, 0) Stp_Plan_FL,CONVERT(decimal, 0) Stp_Plan_Sqm,CONVERT(decimal, 0) Stp_Plan_OnSqm, CONVERT(decimal, 0) Stp_Plan_OffSqm, 
--			CONVERT(decimal, 0) Stp_Plan_SqmSW,CONVERT(decimal, 0) Stp_Plan_SqmCW, CONVERT(decimal, 0) Stp_Plan_SqmCmgt,CONVERT(decimal, 0)Stp_Plan_Tons, 
--			CONVERT(decimal, 0)Stp_Plan_OnTons,CONVERT(decimal, 0)Stp_Plan_OffTons,CONVERT(decimal, 0)Stp_Plan_Kg,CONVERT(decimal, 0)Stp_Meas_FL,
--			CONVERT(decimal, 0)Stp_Meas_Sqm,CONVERT(decimal, 0)Stp_Meas_OnSqm,CONVERT(decimal, 0)Stp_Meas_OffSqm,CONVERT(decimal, 0)Stp_Meas_SqmSW,
--			CONVERT(decimal, 0)Stp_Meas_SqmCW,CONVERT(decimal, 0)Stp_Meas_SqmCMGT,CONVERT(decimal, 0)Stp_Meas_Tons,CONVERT(decimal, 6)Stp_Meas_OnTons, 
--			CONVERT(decimal, 0)Stp_Meas_OffTons,CONVERT(decimal, 0)Stp_Meas_KG,
--			CONVERT(decimal, 0) Stp_BPlan_FL,CONVERT(decimal, 0) Stp_BPlan_Sqm,CONVERT(decimal, 0) Stp_BPlan_OnSqm, 
--			CONVERT(decimal, 0) Stp_BPlan_OffSqm, CONVERT(decimal, 0) Stp_BPlan_SqmSW,CONVERT(decimal, 0) Stp_BPlan_SqmCW, 
--			CONVERT(decimal, 0) Stp_BPlan_SqmCmgt,CONVERT(decimal, 0)Stp_BPlan_Tons,CONVERT(decimal, 2) Stp_BPlan_OnTons, 
--			CONVERT(decimal, 2) Stp_BPlan_OffTons,CONVERT(decimal, 0) Stp_BPlan_Kg,CONVERT(decimal, 0) Stp_BPlan_OSSqm,   
--			CONVERT(decimal, 0) Stp_BPlan_OSFSqm,CONVERT(decimal, 0) Stp_BPlan_REEFFL,CONVERT(decimal, 0) Stp_BPlan_OSFL,     
--			CONVERT(decimal, 0) Stp_BPlan_CUBICS,CONVERT(decimal, 0) Stp_BPlan_SqmSWFAULT,CONVERT(decimal, 0)Stp_BPlan_FaultTons,    
--			CONVERT(decimal, 0) Stp_BPlan_OSTons,CONVERT(decimal, 0)Stp_BPlan_CUBICTons
--		from Planning p 
--		left outer join PlanMonth pm on  
--			pm.ProdMonth = p.ProdMonth and 
--			pm.SectionID = p.SectionID and 
--			pm.WorkplaceID = p.WorkplaceID and 
--			pm.Activity = p.Activity 
--		inner join Sections_Complete sc on 
--			sc.ProdMonth = p.ProdMonth and 
--			sc.SectionID = p.SectionID 
--		inner join Workplace w on  
--			w.WorkplaceID = p.WorkplaceID 
--		inner join CommonAreaSubSections CS on
--			cs.Id = w.SubSection 
--		inner join CommonAreas c on
--			c.Id = cs.CommonArea 
--		where p.CalendarDate >= '''+@TheFromDate+''' and 
--				p.CalendarDate <= '''+@TheToDate+''' and 
--			'	+@ReadSection+' = '''+@TheSectionID+''' and '
--	IF (@TheStopeLedge = 0)
--		set @TheQuery4 = @TheQuery4 + ' p.Activity IN(0,9) '

--	IF (@TheStopeLedge = 1)
--		set @TheQuery4 = @TheQuery4 + ' p.Activity = 0 '

--	IF (@TheStopeLedge = 2)
--		set @TheQuery4 = @TheQuery4 + ' p.Activity = 9 '

--	set @TheQuery4 = @TheQuery4 + 
--		' and w.reefid in (' + @TheReef + ') and
--		w.oreflowid in (' + @TheOreFlowLevel + ') 
--		group by  p.ProdMonth, '+@GroupBy+', p.Workplaceid, w.Description  '
--END  --if StpDaytoDay

----print @TheQuery
----print @TheQuery1
----print @TheQuery5
----print @SurveyUnion
----print @Survey
----print @Survey1
----print @BusPlanUnion
----print @BusPlan
--print @BusPlan1

IF (@ReportType = 'P')
BEGIN
	--print(@SelectPart)
	--print(@SelectPart1)
	--print(@SelectPart2)
	--print(@SelectPart3)
	--print(@PlanSelect)
	--print(@PlanSelect1)
	--print(@PlanSelect2)
	--print(@PlanFrom)
	--print(@PlanWhere)
	--print(@SurveyUnion)
	--print(@SurveySelect)
	--print(@SurveySelect1)
	--print(@SurveyFrom)
	--print(@SurveyWhere)
	--print(@BusUnion)
	--print(@BusSelect)
	--print(@BusSelect1)
	--print(@BusFrom)
	--print(@BusWhere)
	--print(@LPlanUnion)
	--print(@LPlanSelect)
	--print(@LPlanSelect1)
	--print(@LPlanSelect2)
	--print(@LPlanFrom)
	--print(@LPlanWhere)
	--print(@GroupBy)
	exec (@SelectPart+@SelectPart1+@SelectPart2+@SelectPart3+
			@PlanSelect+@PlanSelect1+@PlanSelect2+@PlanFrom+@PlanWhere+
			@SurveyUnion+@SurveySelect+@SurveySelect1+@SurveyFrom+@SurveyWhere+
			@BusUnion+@BusSelect+@BusSelect1+@BusFrom+@BusWhere+
			@LPlanUnion+@LPlanSelect+@LPlanSelect1+@LPlanSelect2+@LPlanFrom+@LPlanWhere+
			@GroupBy)
END
--ELSE
--BEGIN
	--select(@TheQuery + @TheQuery1 + @TheQuery5 + @SurveyUnion + @Survey + @Survey1
	-- + @BusPlanUnion + @BusPlan + @BusPlan1)
	--exec (@TheQuery+ @TheQuery1 + @TheQuery5+ @SurveyUnion + @Survey + @Survey1+ @BusPlanUnion + @BusPlan + @BusPlan1)
--END

go

CREATE Procedure [dbo].[sp_GenericReport_CheckBoxes] 
--Input Parametres
--declare
  @Section varchar(100),
  @Banner varchar(30),
  @theReportHeadings  VARCHAR(100), 
  @theTypeOfReport  VARCHAR(100), 
  @RunLevel  int, 

  @PlanDyn  VARCHAR(2), 
  @PlanLock  VARCHAR(2), 
  @PlanDynProg  VARCHAR(2), 
  @PlanLockProg  VARCHAR(2), 
  @Book  VARCHAR(2), 
  @Meas  VARCHAR(2), 
  @BussPlan  VARCHAR(2), 
  @FC  VARCHAR(2), 
  @AbsVar  VARCHAR(2),

  @StpSqm  VARCHAR(2),
  @StpSqmOn  VARCHAR(2),
  @StpSqmOff  VARCHAR(2),
  @StpCmgt  VARCHAR(2),
  @StpCmgtTotal  VARCHAR(2),
  @StpGT  VARCHAR(2),
  @StpGTTotal  VARCHAR(2),
  @StpSW  VARCHAR(2),
  @StpSWIdeal  VARCHAR(2),
  @StpCW  VARCHAR(2),
  @StpKg  VARCHAR(2),
  @StpFL  VARCHAR(2),
  @StpFLOn  VARCHAR(2),
  @StpFLOff  VARCHAR(2),
  @StpAdv  VARCHAR(2),
  @StpAdvOn  VARCHAR(2),
  @StpAdvOff  VARCHAR(2),
  @StpTons  VARCHAR(2),
  @StpTonsOn  VARCHAR(2),
  @StpTonsOff  VARCHAR(2),
  @StpCub  VARCHAR(2),
  @StpCubGT  VARCHAR(2),
  @StpCubTons  VARCHAR(2),
  @StpCubKg  VARCHAR(2),
  @StpSweepsMeas  VARCHAR(2),
  @StpLabour  VARCHAR(2),
  @StpShftInfo  VARCHAR(2),

  @DevAdv  VARCHAR(2),
  @DevAdvOn  VARCHAR(2),
  @DevAdvOff  VARCHAR(2),
  @DevTons  VARCHAR(2),
  @DevTonsOn  VARCHAR(2),
  @DevTonsOff  VARCHAR(2),
  @DevCmgt  VARCHAR(2),
  @DevCmgtTotal  VARCHAR(2),
  @DevGT  VARCHAR(2),
  @DevGTTotal  VARCHAR(2),
  @DevKg  VARCHAR(2),
  @DevCub  VARCHAR(2),
  @DevCubGT  VARCHAR(2),
  @DevCubTons  VARCHAR(2),
  @DevCubKg  VARCHAR(2),
  @DevLabour  VARCHAR(2),
  @DevShftInfo  VARCHAR(2),
  @DevDrillRig  VARCHAR(2)
  
as


SELECT
  @Section Section,
  @Banner Banner,
  @theReportHeadings theReportHeadings, 
  @theTypeOfReport  theTypeOfReport, 
  @RunLevel  RunLevel, 

  @PlanDyn  PlanDyn, 
  @PlanLock  PlanLock, 
  @PlanDynProg  PlanDynProg, 
  @PlanLockProg  PlanLockProg, 
  @Book  Book, 
  @Meas  Meas, 
  @BussPlan  BussPlan, 
  @FC  FC, 
  @AbsVar  AbsVar,

  @StpSqm  StpSqm,
  @StpSqmOn  StpSqmOn,
  @StpSqmOff  StpSqmOff,
  @StpCmgt  StpCmgt,
  @StpCmgtTotal  StpCmgtTotal,
  @StpGT  StpGT,
  @StpGTTotal  StpGTTotal,
  @StpSW  StpSW,
  @StpSWIdeal  StpSWIdeal,
  @StpCW  StpCW,
  @StpKg  StpKg,
  @StpFL  StpFL,
  @StpFLOn  StpFLOn,
  @StpFLOff  StpFLOff,
  @StpAdv  StpAdv,
  @StpAdvOn  StpAdvOn,
  @StpAdvOff  StpAdvOff,
  @StpTons  StpTons,
  @StpTonsOn  StpTonsOn,
  @StpTonsOff  StpTonsOff,
  @StpCub  StpCub, 
  @StpCubGT  StpCubGT,
  @StpCubTons  StpCubTons,
  @StpCubKg  StpCubKg,
  @StpSweepsMeas  StpSweepsMeas,
  @StpLabour  StpLabour,
  @StpShftInfo  StpShftInfo,

  @DevAdv  DevAdv,
  @DevAdvOn  DevAdvOn,
  @DevAdvOff  DevAdvOff,
  @DevTons  DevTons,
  @DevTonsOn  DevTonsOn,
  @DevTonsOff  DevTonsOff,
  @DevCmgt  DevCmgt,
  @DevCmgtTotal  DevCmgtTotal,
  @DevGT  DevGT,
  @DevGTTotal  DevGTTotal,
  @DevKg  DevKg,
  @DevCub  DevCub,
  @DevCubGT  DevCubGT,
  @DevCubTons  DevCubTons,
  @DevCubKg  DevCubKg,
  @DevLabour  DevLabour,
  @DevShftInfo  DevShftInfo,
  @DevDrillRig  DevDrillRig


  go

go

ALTER Procedure [dbo].[sp_Load_BookABSStoping_Detail] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
		@BookDate varchar(10)
as

--set @Prodmonth = 201703
--set @SectionID = 'REAA'
--set @BookDate = '2017-03-14'


Declare @SQL VarChar(8000)

set @SQL = ' 
 select * from 
  ( 
     select pm.ProdMonth, pm.sectionid Sec, pm.sectionid SectionID, 
         pm.workplaceid WPID, w.Description, 
         pm.workplaceid + '':'' +w.Description WP, 
         pm.Activity, act.Description ActDesc, pd.ShiftDay, 
         isnull(pm.OrgUnitDay, '''') OrgUnitDS, 
         CalendarDate = convert(varchar(10), pd.CalendarDate, 120), 
         isnull(pd.ABSPicNo, '''') ABSPicNo, 
         ABSCodeDisplay = case when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                        when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = '''' then ''S'' 
                        when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                        when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = '''' then ''B'' 
                        when isnull(pd.ABSCode, '''') = ''A'' then ''A'' else '''' end, 
         isnull(pd.ABSCode, '''') ABSCode, 
         isnull(pd.ABSPrec, '''') ABSPrec, 
         isnull(pd.ABSNotes,'''') ABSNotes, 
         isnull(pm.FL, 0) FL, 
		 BookAdv = case when isnull(pd.BookMetresAdvance,0) = 0 then cast(1.2 as numeric(7,2))
		    else cast(isnull(pd.BookMetresAdvance, 0) as numeric(7,2)) end, 
		 DefaultAdv = cast(1.2 as numeric(7,2)), 
         BookCmgt = case when isnull(pd.BookCMGT, 0) = 0 then isnull(pd.CMGT, 0) else isnull(pd.BookCMGT, 0) end, 
		 BookGT = case when isnull(pd.BookGT, 0) = 0 then isnull(pd.GT, 0) else isnull(pd.BookGT, 0) end,
         isnull(pd.BookSqm, 0) BookSqm, 
         isnull(pd.BookReefSqm, 0) BookReefSqm, 
         isnull(pd.BookWasteSqm, 0) BookWasteSqm, 
         cast(isnull(pd.BookMetresAdvance, 0) as numeric(7,2)) BookMetresAdvance, 
         cast(isnull(pd.BookFL, 0) as numeric(7,0)) BookFL, 
		 isnull(pd.BookTons, 0) BookTons, 
		 isnull(pd.BookReefTons, 0) BookReefTons, 
		 isnull(pd.BookWasteTons, 0) BookWasteTons, 
		 BookKG = isnull(pd.BookGrams, 0) / 1000, 
         BookGrams = isnull(pd.BookGrams, 0), 
         isnull(ProgSum.AdjSqm, 0) PrevAdjSqm, 
         0 AdjSqm, 0 AdjTons, 0 AdjGrams, 
         isnull(pm.SW,0) BookSW, ss.RockDensity BookDens, 
         BookCodeStp = case when prbook.ProblemID = ''ST'' then prbook.ProblemID else isnull(pd.BookCode,'''') end, 
         isnull(pd.SBossNotes,'''') BookProb, 
         isnull(pd.ProblemID,'''') ProblemID, isnull(pd.SBossNotes,'''') SBossNotes, 
         isnull(pd.CheckSqm, 0) CheckSqm, isnull(pd.MOFC, 0) MOFC ,
		 isnull(pd.CausedLostBlast,'''') CausedLostBlast,
		 isnull(pd.BookCubics,0) BookCubics,
		 isnull(pd.BookCubicMetres,0) BookCubicMetres,
		 isnull(pd.BookCubicTons,0) BookCubicTons,
		 isnull(pd.BookCubicGrams,0) BookCubicGrams,
		 BookCubicKG = isnull(pd.BookCubicGrams,0) / 1000,
		 BookCubicGT = ''3.33''--case when isnull(pd.BookCubicGT,0) = 0 then 
				--isnull(pd.CubicGT,0) else isnull(pd.BookCubicGT,0) end
      from planmonth pm 
      inner 
      join planning pd on 
        pm.Prodmonth = pd.Prodmonth and 
        pm.SectionID = pd.SectionID and 
        pm.WorkplaceID = pd.WorkplaceID and 
        pm.Activity = pd.Activity and 
        pm.PlanCode = pd.PlanCode and
		pm.IsCubics = pd.IsCubics
      inner join SECTION_COMPLETE sc on 
        sc.ProdMonth = pm.ProdMonth and 
        sc.SectionID = pm.SectionID 
      inner join Seccal s on 
        s.ProdMonth = sc.ProdMonth and 
        s.SectionID = sc.SectionID_1 
      inner join Caltype ct on 
        ct.CalendarCode = s.CalendarCode and 
        ct.CalendarDate = pd.CalendarDate 
      left outer join Code_Activity act on 
        act.Activity = pm.Activity 
      inner join Workplace w on 
        pm.WorkplaceID = w.WorkplaceID 
      left outer join 
       ( 
             select b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics,
                max(CalendarDate) CalendarDate 
             from Planning b 
             where ProdMonth = '''+@Prodmonth+''' and 
                   Activity in (0, 9) and 
                   CalendarDate < '''+@BookDate+''' and 
                   PlanCode = ''MP'' 
             group by b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics 
       ) prbook1 on 
         pm.Prodmonth = prbook1.Prodmonth and 
         pm.SectionID = prbook1.SectionID and 
         pm.WorkplaceID = prbook1.WorkplaceID and 
         pm.Activity = prbook1.Activity and 
         pm.PlanCode = prbook1.PlanCode and
		 pm.IsCubics = prbook1.IsCubics
       left outer join Planning prbook on 
         prbook.Prodmonth = prbook1.Prodmonth and 
         prbook.SectionID = prbook1.SectionID and 
         prbook.WorkplaceID = prbook1.WorkplaceID and 
         prbook.Activity = prbook1.Activity and 
         prbook.PlanCode = prbook1.PlanCode and 
		 prbook.IsCubics = prbook1.IsCubics and
         prbook.Calendardate = prbook1.Calendardate 
      left outer join 
         (Select workplaceid, max(CalendarDate) CalendarDate from sampling group by workplaceid 
         ) a on a.WorkplaceID = pm.WorkplaceID 
      left outer join Sampling aa on 
        aa.WorkplaceID = pm.WorkplaceID and 
        aa.CalendarDate = a.calendarDate 
      left outer join 
          (select p.ProdMonth, WorkplaceID, p.SectionID, Activity,  
                  PlanCode, IsCubics, sum(BookSqm) ProgBookSqm, sum(AdjSqm) AdjSqm 
           from planning p 
           inner 
           join SECTION_COMPLETE sc on 
             p.ProdMonth = sc.ProdMonth and 
             p.SectionID = sc.SectionID 
           where p.ProdMonth = '''+@Prodmonth+''' and 
                 p.calendardate < '''+@BookDate+''' and 
                 sc.SectionID_1 = '''+@SectionID+''' and 
                 p.Activity = 1 and
				 p.PlanCode = ''MP'' and
				 p.IsCubics = ''N''
           group by p.ProdMonth, WorkplaceID, p.SectionID, Activity, PlanCode, IsCubics 
          ) ProgSum on 
             pm.Prodmonth = ProgSum.Prodmonth and 
             pm.SectionID = ProgSum.SectionID and 
             pm.WorkplaceID = ProgSum.WorkplaceID and 
             pm.Activity = ProgSum.Activity and 
             pm.PlanCode = ProgSum.PlanCode and
			 pm.IsCubics = ProgSum.IsCubics, SYSSET ss  
      where pm.ProdMonth = '''+@Prodmonth+''' and 
            pm.Activity in (0, 9) and 
            pm.PlanCode = ''MP'' and 
            pd.CalendarDate = '''+@BookDate+''' and 
            pm.SQM > 0 and 
            ct.WorkingDay = ''Y'' and 
            sc.SectionID_1 = '''+@SectionID+''' 
			and pm.IsCubics = ''N''
            --(p.Pumahola = ''Y'' or ct.WorkingDay = ''Y'')) a 
  ) a '

--print (@SQL)
exec (@SQL)
go
ALTER Procedure [dbo].[sp_Load_BookABSStoping]
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
        @DaysBackdate int,
        @Shift int
as

--set @Prodmonth = 201612
--set @SectionID = 'REAA'
--Set @DaysBackdate = 0
--set @Shift = 3


Declare @SQL VarChar(8000),
 @Backdate DateTime


 --Select @TheshiftTime

--@SQL1 VarChar(8000), -- Forecast and Cleaning Bookings
--@SQL2 VarChar(8000)
select @backdate = Rundate - @DaysBackdate  From sysset 

set @SQL = ' select * from 
	(select sc.Name_1 SBName, sc.SectionID_2, sc.Name Name, pm.SectionID, 
	pm.Workplaceid+'' : ''+w.Description Workplace,
	pm.Activity, 
	ShiftDay = case when ct.WorkingDay = ''Y'' then convert(varchar(3), p.ShiftDay) else ''N'' end,
	isnull(ct.WorkingDay,'''') WorkingDay,
	ct.CalendarDate CalendarDate,  
	''Book'' BookType,
	isnull(p.ProblemID,'''') ProblemID,
	Type = z.[Type],
	ABSCode = isnull(p.AbsCode,''''), 
	MonthSqm = case when z.[Type] = ''Plan'' then convert(varchar(10), cast(pm.SQM as numeric(7,0))) else '''' end,
	--case when isnull(ProgP.Prog_Plan_SQM,0) ProgPlan,
	ProgSqm =  case when z.[Type] = ''Plan'' then 
					convert(varchar(10), cast(pp.ProgPlanSQM as numeric(7,0))) else 
					convert(varchar(10), cast(pp.ProgBookSQM as numeric(7,0))) end,
	theVal = case 
  when z.[Type] = ''Plan'' and isnull(p.SQM,0) = 0 then ''''
  when z.[Type] = ''Plan'' and isnull(p.SQM,0) > 0 then convert(varchar(10), convert(Numeric(7,0),isnull(p.SQM,0)))
  when z.[Type] = ''Book'' and isnull(p.ProblemID, '''') <> '''' then isnull(p.ProblemID,'''')
  when z.[Type] = ''Book'' and isnull(p.BookSQM,0) = 0 then '''' 
  when z.[Type] = ''Book'' and isnull(p.BookSQM,0) > 0 then
		convert(varchar(10), convert(Numeric(7,0),isnull(p.BookSQM,0))) else ''''
	end,
	theValue = case 
  when z.[Type] = ''Plan'' then convert(Numeric(7,0),isnull(p.SQM,0))
  when z.[Type] = ''Book'' then convert(Numeric(7,0),isnull(p.BookSQM,0)) else 0 end
	from planmonth pm	
	inner join Section_Complete sc on 
	  pm.SectionID = sc.SectionID and
	  pm.ProdMonth = sc.ProdMonth
	inner join Seccal s on
	  sc.ProdMonth = s.ProdMonth and
	  sc.SectionID_1 = s.SectionID
	inner join CalType ct on
	  s.CalendarCode = ct.CalendarCode and
	  s.BeginDate <= ct.CalendarDate and
	  s.enddate >= ct.CalendarDate
	inner join Planning p on
	  p.ProdMonth = pm.ProdMonth and 
	  p.SectionID = pm.SectionID and
	  p.WorkplaceID = pm.WorkplaceID and
	  p.Activity = pm.Activity and
	  p.IsCubics = pm.IsCubics and
	  p.PlanCode = pm.PlanCode and
	  p.Calendardate = ct.CalendarDate
	left outer join
		(select ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode, sum(isnull(Booksqm,0)) ProgBookSQM,
		 sum(isnull(SQM,0)) ProgPlanSQM
		 from Planning, Sysset
		 where CalendarDate <= SYSSET.RUNDATE and
		 PlanCode = ''MP'' and
		 Activity <> 1 and
		 IsCubics = ''N''
		 group by ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode
		) pp on
	  pp.ProdMonth = pm.ProdMonth and 
	  pp.SectionID = pm.SectionID and
	  pp.WorkplaceID = pm.WorkplaceID and
	  pp.Activity = pm.Activity and
	  pp.IsCubics = pm.IsCubics and
	  pp.PlanCode = pm.PlanCode
	inner join Workplace W on
	  pm.WorkplaceID = w.WorkplaceID, SYSSET ss,
	(select [Order] = 1,
	Type = ''Plan''
	union
	select [Order] = 2,
	Type = ''Book''
	) z
	where pm.prodmonth = '''+ @Prodmonth +''' and sc.SectionID_1= '''+@SectionID+''' 
	and pm.Activity in (0,9) and pm.tons > 0 and pm.PlanCode = ''MP'' and pm.IsCubics = ''N'''

--if @DaysBackdate = 0
--begin  
--Set @SQL = @SQL+'and ct.calendardate = convert(Date, ss.rundate) ) a '
--end
--else
--begin
Set @SQL = @SQL+'and ct.calendardate <= ss.rundate ) a 
		order by SectionID_2, SectionID, Workplace,CalendarDate ' 
			--	and p.calendardate >= '''+Convert(Varchar(10),@backdate,120)+''' '
--end


--print (@SQL)
exec (@SQL)
go

 ALTER Procedure [dbo].[sp_Load_BookABSDevelopment_Detail] 
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
		@BookDate varchar(10)
as

--set @Prodmonth = 201612
--set @SectionID = 'REca'
--set @BookDate = '2016-12-10'


Declare @SQL VarChar(8000)
Declare @SQL1 VarChar(8000)

set @SQL = ' 
 select PegID = case when z.BookCodeDev = '''' and z.ThePPegID <> '''' then z.ThePPegID
					 --when z.BookCodeDev = '''' and z.ThePPegID = '''' and z.ThePegID <> '''' then z.ThePegID
					 else case when z.BookCodeDev <> '''' then z.ThePegID					 
					 else ''Start:0.0'' end end,
		PegToFace = case when z.BookCodeDev = '''' and z.ThePPegID <> '''' then z.ThePPegToFace
					 --when z.BookCodeDev = '''' and z.ThePPegID = '''' and z.ThePegID <> '''' then z.ThePegToFace
					 else case when z.BookCodeDev <> '''' then z.ThePegToFace
					 else ''0.0'' end end,
		PegToFaceA = case when z.BookCodeDev = '''' and z.ThePPegID <> '''' then z.ThePPegToFace
					 --when z.BookCodeDev = '''' and z.ThePPegID = '''' and z.ThePegID <> '''' then z.ThePegToFace
					 else case when z.BookCodeDev <> '''' then z.ThePegToFace
					 else ''0.0'' end end,
		PegDist = case when z.BookCodeDev = '''' and z.ThePPegID <> '''' then z.ThePPegToFace
					 --when z.BookCodeDev = '''' and z.ThePPegID = '''' and z.ThePegID <> '''' then z.ThePegToFace
					 else case when z.BookCodeDev <> '''' then z.ThePegToFace
					 else ''0.0'' end end,
		PegFrom = case when z.BookCodeDev = '''' and z.ThePPegID <> '''' then z.ThePPegDist
					 else case when z.BookCodeDev <> '''' and z.ThePPegID <> '''' then z.ThePPegDist
					 else ''0.0'' end  end,
		PegTo = case when z.BookCodeDev = '''' and z.ThePPegID <> '''' then z.ThePegDist
					 else case when z.BookCodeDev <> '''' then z.PegValue + z.ThePegToFace
					 else ''0.0'' end end,
		BookCodeDev = case when z.BookCodeDev = '''' and z.ThePPegID <> '''' and 
								(BookCodeDevPrev = ''ST'' or BookCodeDevPrev = ''NP'') then BookCodeDevPrev
						else BookCodeDev end,
		BookAdv	= case when z.BookCodeDev = '''' and z.ThePPegID = '''' then ''0.0'' else Convert(varchar(7),z.TheBookAdv) end,
	z.* from 
  ( 
     select pm.ProdMonth, pm.sectionid Sec, pm.sectionid SectionID, 
            pm.workplaceid WPID, w.Description, 
            pm.workplaceid + '':'' + w.Description WP, pm.ReefWaste,   
            1 Activity, ''Development'' ActDesc, pd.ShiftDay, 
            isnull(pm.OrgUnitDay, '''') OrgUnitDS, 
           CalendarDate = convert(varchar(10), pd.CalendarDate, 120), 
           isnull(pd.ABSPicNo, '''') ABSPicNo, 
           ABSCodeDisplay = case when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                                when isnull(pd.ABSCode, '''') = ''S'' and isnull(pd.ABSPrec, '''') = '''' then ''S'' 
                                when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = ''P'' then ''P'' 
                                when isnull(pd.ABSCode, '''') = ''B'' and isnull(pd.ABSPrec, '''') = '''' then ''B'' 
                                when isnull(pd.ABSCode, '''') = ''A'' then ''A'' else '''' end, 
           isnull(pd.ABSCode, '''') ABSCode, 
           isnull(pd.ABSPrec, '''') ABSPrec, 
           isnull(pd.ABSNotes, '''') ABSNotes, 
		   PegValue = isnull(pg.Value, 0), 
		   ThePegID = pd.PegID, -- +'':''+convert(varchar(10), cast(pg.Value as numeric(10,1))),
		   ThePegDist = isnull(pd.PegDist, 0), 		   
           ThePegToFace = cast(isnull(pd.PegToFace, 0) as numeric(10,1)), 
		   ThePPegID = case when isnull(c.ppeg,'''') = '''' then (case when isnull(sss.ppeg,'''') ='''' or (sss.cal < e.cal) then 
		                     isnull(e.ppeg,'''') else isnull(sss.ppeg,'''') end) else isnull(c.ppeg,'''') end ,  
		   ThePPegToFace = case when c.ppegtoface is null then (case when sss.ppegtoface is null or (sss.cal < e.cal) then 
							isnull(e.ppegtoface,0) else isnull(sss.ppegtoface,0) end) else isnull(c.ppegtoface,0) end,
		   ThePPegDist = case when c.ppegdist is null then (case when sss.ppegdist is null or (sss.cal < e.cal) then 
							isnull(e.ppegdist,0) else isnull(sss.ppegdist,0) end)  else isnull(c.ppegdist,0) end,
           isnull(pd.BookMetresAdvance, 0) TheBookAdv, 
		   isnull(pd.BookReefAdv, 0) BookReefAdv, 
		   isnull(pd.BookWasteAdv, 0) BookWasteAdv, 
		   isnull(pd.BookTons, 0) BookTons,
		   isnull(pd.BookReefTons, 0) BookReefTons,
		   isnull(pd.BookWasteTons, 0) BookWasteTons,
           isnull(pd.BookGrams, 0) BookGrams, BookKG = isnull(pd.BookGrams, 0) / 1000, 
          -- isnull(pd.BookCubics, 0) BookCubics, isnull(pd.BookSweeps, 0) BookSweeps, 
          -- isnull(pd.BookResweeps, 0) BookReSweeps, isnull(pd.BookVamps, 0) BookVamps, 
           --BookTotal = isnull(pd.BookMetresAdvance, 0) + isnull(pd.BookSecM, 0), 
          -- isnull(pd.BookOpenUp, 0) BookOpenUp, isnull(pd.BookSecM, 0) BookSecM, 
           DHeight = case when isnull(pm.DHeight, 0) = 0 then w.EndHeight else isnull(pm.DHeight, 0) end, 
		   DWidth =  case when isnull(pm.DWidth, 0) = 0 then w.EndWidth else isnull(pm.DWidth, 0) end, 
		   isnull(ss.RockDensity, 0) Density, 
           isnull(pm.GT,0) gt, 
		   isnull(pm.CMGT,0) cmgt, 
           BookCodeDev = isnull(pd.BookCode,''''), 
		   BookCodeDevPrev = isnull(prbook.ProblemID,''''), 
           isnull(pd.ProblemID, '''') ProblemID, isnull(pd.SBossNotes, '''') SBossNotes,
		    isnull(pd.CausedLostBlast, '''') CausedLostBlast,
			isnull(pd.BookCubics,0) BookCubics,
			isnull(pd.BookCubicMetres,0) BookCubicMetres,
			isnull(pd.BookCubicTons,0) BookCubicTons,
			isnull(pd.BookCubicGrams,0) BookCubicGrams,
			BookCubicKG = isnull(pd.BookCubicGrams,0) / 1000,
			BookCubicGT = case when isnull(pd.BookCubicGT,0) = 0 then 
									isnull(pd.CubicGT,0) else isnull(pd.BookCubicGT,0) end
      from planmonth pm 
      inner join planning pd on 
        pm.Prodmonth = pd.Prodmonth and 
        pm.SectionID = pd.SectionID and 
        pm.WorkplaceID = pd.WorkplaceID and 
        pm.Activity = pd.Activity and 
        pm.PlanCode = pd.PlanCode and
		pm.IsCubics = pd.IsCubics
	left outer join 

		(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, 
		ppegtoface = case when isnull(b.pegtoface,0) = 0 then 0 
					      when convert(varchar(10),b.pegtoface) = '''' then 0 else b.pegtoface end,
		ppegdist = case when isnull(b.pegdist,0) = 0 then 0 
					      when convert(varchar(10),b.pegdist) = '''' then 0 else b.pegdist end,
		cal 
		from 
			(select p.workplaceid wp1,  max(calendardate) cal 
			from planning p 
			where calendardate < '''+@BookDate+''' and prodmonth = '''+@Prodmonth+''' and isCubics = ''N'' and PlanCode=''MP''
			   and p.activity in (1) and bookmetresadvance is not null group by p.workplaceid
			) a 
			left outer join 
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist,  calendardate 
				from planning 
				where bookmetresadvance is not null and activity in (1) and isCubics = ''N'' and PlanCode=''MP''
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) c on pm.WorkplaceID = c.wp1 
 
		left outer join  
		 (select a.wp1,  b.pegid + '':'' + convert(varchar(10),b.pegvalue) ppeg, 
		  ppegtoface = case when isnull(b.pegtoface,0) = 0 then 0 
					      when convert(varchar(10),b.pegtoface) = '''' then 0 else b.pegtoface end,
		ppegdist = case when isnull(b.pegdist,0) = 0 then 0 
					      when convert(varchar(10),b.pegdist) = '''' then 0 else b.pegdist end,
		  --b.pegtoface ppegtoface, b.pegdist ppegdist, 
		  cal 
		 from    
			(select p.workplaceid wp1,  max(calendardate) cal	  
			from survey p 
			where prodmonth < '''+@Prodmonth+''' and p.activity in (1) and mainmetres is not null 
			group by p.workplaceid
			) a    
		 left outer join    
			(select workplaceid ,  max(pegno)  pegid, max(pegvalue) pegvalue, max(pegtoface) pegtoface, max(progto) pegdist, calendardate    
			from survey
			where mainmetres is not null and activity in (1) 
			group by workplaceid, calendardate
			) b  on a.wp1 = b.workplaceid and a.cal = b.calendardate
		) sss on pm.WorkplaceID = sss.wp1  '
set @SQL1 = ' 

		left outer join  
			(select a.wp1, b.bookcode prevcode,  b.pegid ppeg, 
			ppegtoface = case when isnull(b.pegtoface,0) = 0 then 0 
					      when convert(varchar(10),b.pegtoface) = '''' then 0 else b.pegtoface end,
		ppegdist = case when isnull(b.pegdist,0) = 0 then 0 
					      when convert(varchar(10),b.pegdist) = '''' then 0 else b.pegdist end,
			--b.pegtoface ppegtoface, b.pegdist ppegdist, 
			cal 
			from 
				(select p.workplaceid wp1,  max(calendardate) cal  
				 from planning p 
				 where calendardate < '''+@BookDate+''' and p.activity in (1) and (bookmetresadvance > 0 or bookmetresadvance < 0) 
				 group by p.workplaceid
				) a    
			left outer join    
				(select workplaceid , max(bookcode)  bookcode,  max(pegid)  pegid, max(pegtoface) pegtoface, max(pegdist) pegdist, calendardate    
				from planning 
				where bookcode is not null and activity in (1) 
				group by workplaceid, calendardate
				) b on a.wp1 = b.workplaceid and a.cal = b.calendardate 
		) e on pm.WorkplaceID = e.wp1  
      inner join SECTION_COMPLETE sc on 
        sc.ProdMonth = pm.ProdMonth and 
        sc.SectionID = pm.SectionID 
      inner join Seccal s on 
        s.ProdMonth = sc.ProdMonth and 
        s.SectionID = sc.SectionID_1 
      inner join Caltype ct on 
        ct.CalendarCode = s.CalendarCode and 
        ct.CalendarDate = pd.CalendarDate 
      left outer join Code_Activity act on 
        act.Activity = pm.Activity 
      inner join Workplace w on 
        pm.WorkplaceID = w.WorkplaceID and
		pm.Activity = w.Activity
	  left outer join Peg pg on
	    pg.PegID+'':''+Convert(varchar(10),Convert(numeric(10,1),pg.Value)) = pd.PegID and
		pg.WorkplaceID = pd.WorkplaceID
      left outer join 
       ( 
             select b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics, 
                max(CalendarDate) CalendarDate 
             from planning b 
             where ProdMonth = '''+@Prodmonth+''' and 
                   Activity = 1 and 
                   CalendarDate < '''+@BookDate+''' and 
                   PlanCode = ''MP'' and
				   IsCubics = ''N''
             group by b.ProdMonth, b.SectionID, b.workplaceID, b.Activity, PlanCode, IsCubics
       ) prbook1 on 
         pm.Prodmonth = prbook1.Prodmonth and 
         pm.SectionID = prbook1.SectionID and 
         pm.WorkplaceID = prbook1.WorkplaceID and 
         pm.Activity = prbook1.Activity and 
         pm.PlanCode = prbook1.PlanCode and
		 pm.IsCubics = prbook1.IsCubics
       left outer join Planning prbook on 
         prbook.Prodmonth = prbook1.Prodmonth and 
         prbook.SectionID = prbook1.SectionID and 
         prbook.WorkplaceID = prbook1.WorkplaceID and 
         prbook.Activity = prbook1.Activity and 
         prbook.PlanCode = prbook1.PlanCode and 
		 prbook.IsCubics = prbook1.IsCubics and
         prbook.Calendardate = prbook1.Calendardate 
      left outer join 
         (Select workplaceid, max(CalendarDate) CalendarDate from sampling group by workplaceid 
         ) a on a.WorkplaceID = pm.WorkplaceID 
      left outer join Sampling aa on 
        aa.WorkplaceID = pm.WorkplaceID and 
        aa.CalendarDate = a.calendarDate 
      left outer join 
          (select p.ProdMonth, WorkplaceID, p.SectionID, Activity, 
                  PlanCode, IsCubics, sum(BookSqm) progbook, sum(AdjSqm) AdjSqm 
           from planning p 
           inner 
           join SECTION_COMPLETE sc on 
             p.ProdMonth = sc.ProdMonth and 
             p.SectionID = sc.SectionID 
           where p.ProdMonth = '''+@Prodmonth+''' and 
                 p.calendardate < '''+@BookDate+''' and 
                 sc.SectionID_1 = '''+@SectionID+''' and 
                 p.Activity = 1 and
				 p.PlanCode = ''MP'' and
				 p.IsCubics = ''N'' 
           group by p.ProdMonth, WorkplaceID, p.SectionID, Activity, PlanCode, IsCubics
          ) ProgSum on 
             pm.Prodmonth = ProgSum.Prodmonth and 
             pm.SectionID = ProgSum.SectionID and 
             pm.WorkplaceID = ProgSum.WorkplaceID and 
             pm.Activity = ProgSum.Activity and 
             pm.PlanCode = ProgSum.PlanCode and
			 pm.IsCubics = ProgSum.IsCubics, Sysset ss
      where pm.ProdMonth = '''+@Prodmonth+''' and 
            pm.Activity = 1 and 
            pm.PlanCode = ''MP'' and 
			pm.IsCubics = ''N'' and 
            pd.CalendarDate = '''+@BookDate+''' and 
            pm.Metresadvance > 0 and 
            ct.WorkingDay = ''Y'' and 
            sc.SectionID_1 = '''+@SectionID+''' 
            --(p.Pumahola = ''Y'' or ct.WorkingDay = ''Y'')) a 
  ) z '

  --print (@SQL)
  -- print (@SQL1)
exec (@SQL+@SQL1)

go

ALTER Procedure [dbo].[sp_Load_BookABSDevelopment]
--Declare 
        @Prodmonth varchar(6),
        @SectionID VarChar(20),
        @DaysBackdate int,
        @Shift int
as

--set @Prodmonth = 201612
--set @SectionID = 'RECA'
--Set @DaysBackdate = 0
--set @Shift = 3


Declare @SQL VarChar(8000),
 @Backdate DateTime


 --Select @TheshiftTime

--@SQL1 VarChar(8000), -- Forecast and Cleaning Bookings
--@SQL2 VarChar(8000)
select @backdate = Rundate - @DaysBackdate  From sysset 

set @SQL = ' select * from 
	(select sc.Name_1 SBName, sc.SectionID_2, sc.Name Name, pm.SectionID, 
	pm.Workplaceid+'' : ''+w.Description Workplace,
	pm.Activity, 
	ShiftDay = case when ct.WorkingDay = ''Y'' then convert(varchar(3), p.ShiftDay) else ''N'' end,
	isnull(ct.WorkingDay,'''') WorkingDay,
	ct.CalendarDate CalendarDate,  
	''Book'' BookType,
	isnull(p.ProblemID,'''') ProblemID,
	Type = z.[Type],
	ABSCode = isnull(p.AbsCode,''''),
	MonthAdv = case when z.[Type] = ''Plan'' then convert(varchar(10), cast(pm.MetresAdvance as numeric(7,0))) else '''' end,
	ProgAdv = case when z.[Type] = ''Plan'' then 
					convert(varchar(10), cast(pp.ProgPlanAdv as numeric(7,0))) else 
					convert(varchar(10), cast(pp.ProgBookAdv as numeric(7,0))) end,
	theVal = case 
		  when z.[Type] = ''Plan'' and isnull(p.MetresAdvance,0) = 0 then ''''
		  when z.[Type] = ''Plan'' and isnull(p.MetresAdvance,0) > 0 then convert(varchar(10), convert(Numeric(7,1),isnull(p.MetresAdvance,0)))
		  when z.[Type] = ''Book'' and isnull(p.ProblemID, '''') <> '''' then isnull(p.ProblemID,'''')
		  when z.[Type] = ''Book'' and isnull(p.BookMetresAdvance,0) = 0 then '''' 
		  when z.[Type] = ''Book'' and isnull(p.BookMetresAdvance,0) > 0 then
		convert(varchar(10), convert(Numeric(7,1),isnull(p.BookMetresAdvance,0))) else ''''
	end,
	theValue = case 
		  when z.[Type] = ''Plan'' then convert(Numeric(7,1),isnull(p.MetresAdvance,0))
		  when z.[Type] = ''Book'' then convert(Numeric(7,1),isnull(p.BookMetresAdvance,0)) else 0 end 	
	from planmonth pm	
	inner join Section_Complete sc on 
	  pm.SectionID = sc.SectionID and
	  pm.ProdMonth = sc.ProdMonth
	inner join Seccal s on
	  sc.ProdMonth = s.ProdMonth and
	  sc.SectionID_1 = s.SectionID
	inner join CalType ct on
	  s.CalendarCode = ct.CalendarCode and
	  s.BeginDate <= ct.CalendarDate and
	  s.enddate >= ct.CalendarDate
	inner join Planning p on
	  p.ProdMonth = pm.ProdMonth and 
	  p.SectionID = pm.SectionID and
	  p.WorkplaceID = pm.WorkplaceID and
	  p.Activity = pm.Activity and
	  p.IsCubics = pm.IsCubics and
	  p.PlanCode = pm.PlanCode and
	  p.IsCubics = pm.IsCubics and
	  p.Calendardate = ct.CalendarDate
	left outer join
		(select ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode, sum(isnull(BookMetresAdvance,0)) ProgBookAdv,
		 sum(isnull(MetresAdvance,0)) ProgPlanAdv
		 from Planning, Sysset
		 where CalendarDate <= SYSSET.RUNDATE and
		 PlanCode = ''MP'' and
		 Activity = 1 and
		 IsCubics = ''N''
		 group by ProdMonth,SectionID,WorkplaceID,Activity,IsCubics,PlanCode
		) pp on
	  pp.ProdMonth = pm.ProdMonth and 
	  pp.SectionID = pm.SectionID and
	  pp.WorkplaceID = pm.WorkplaceID and
	  pp.Activity = pm.Activity and
	  pp.IsCubics = pm.IsCubics and
	  pp.PlanCode = pm.PlanCode
	inner join Workplace W on
	  pm.WorkplaceID = w.WorkplaceID, SYSSET ss,
	(select [Order] = 1,
	Type = ''Plan''
	union
	select [Order] = 2,
	Type = ''Book''
	) z
	where pm.prodmonth = '''+ @Prodmonth +''' and sc.SectionID_1= '''+@SectionID+''' 
	and pm.Activity = 1 and pm.tons > 0 and pm.PlanCode = ''MP'' and pm.IsCubics = ''N''
	and ct.calendardate <= ss.rundate ) a 
		order by SectionID_2, SectionID, Workplace,CalendarDate ' 


--print (@SQL)
exec (@SQL)
go



