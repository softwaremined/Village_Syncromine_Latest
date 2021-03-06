
ALTER Function [dbo].[RequestType_MessageBody] 
( 
   @ChnagrequestID INT
) 
RETURNS VARCHAR(MAX)
AS 
BEGIN
--DECLARE @ChnagrequestID INT
--SET @ChnagrequestID=2570
DECLARE @tableHTML  VARCHAR(MAX), @changeID INT, @daycrew varchar(50),@AfternoonCrew varchar(50),@NightCrew varchar(50),
@RovingCrew varchar(50),@ReefSQM numeric(7,0),@WasteSQM numeric(7,0),@Meters numeric(7,0),@MetersWaste numeric(7,0),
@CubicMeters numeric(7,0),@MiningMethod varchar(20),@FL NUMERIC(13,3),@ReefSQMOLD  numeric(7,0),@WasteSQMOLD numeric(7,0),
@MetersOLD numeric(7,0),@MetersWasteOLD numeric(7,0),
@CubicMetersOLD numeric(7,0),@Workplaceid varchar(20),@prodmonth numeric(7,0),@sectionid varchar(20), @sectionid_2 varchar(20),
@activity int,@FLOLD NUMERIC(13,3),@WPDesc varchar(50),@SectName varchar(50),@daycrewOLD varchar(50),@AfternoonCrewOLD varchar(50),@NightCrewOLD varchar(50),
@RovingCrewOLD varchar(50),@sectionidOLD varchar(20),@sectionNameOLD VARCHAR(50),@SectionName varchar(50),@stopdateOLD varchar(50),@stopdate varchar(50),
@startdateOLD VARCHAR(50),@startdate varchar(50),@OldWP VARCHAR(50),@MiningMethodOLD varchar(20),@miningmethodMOVEPLAN VARCHAR(50),@FLMOVEPLAN VARCHAR(50),
@DrillRig  VARCHAR(50)


SET @changeID=(select changeid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @daycrew=(select DAYCREW from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @AfternoonCrew=(select AfternoonCrew from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @NightCrew=(select NightCrew from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @RovingCrew=(select RovingCrew from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @ReefSQM=(select ReefSQM from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @WasteSQM=(select WasteSQM from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @Meters=(select Meters from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @MetersWaste=(select MetersWaste from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @CubicMeters=(select CubicMeters from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
--SET @MiningMethod=(select MiningMethod from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @FL=(select FL from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @Workplaceid=(select Workplaceid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @prodmonth=(select prodmonth from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @sectionid=(select sectionid from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @sectionid_2=(select sectionid_2 from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @activity=(select activity from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
set @WPDesc=(select description from workplace where workplaceid=@Workplaceid)
set @SectName=(select distinct name from section where Prodmonth = @prodmonth and sectionid=@sectionid_2)
SET @SectionName=(select distinct Name from section where Prodmonth = @prodmonth and sectionid=@sectionid)
SET @stopdate=(select stopdate from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @startdate=(select startdate from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)
SET @MiningMethod=(select Description from PrePlanning_ChangeRequest PPCR  LEFT JOIN Code_Methods BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PPCR.MiningMethod=BPD.TargetID where changerequestid=@ChnagrequestID)
SET @OldWP=(select OldWorkplaceID  from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)


SET @ReefSQMOLD=(select ReefSQM from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @WasteSQMOLD=(select WasteSQM from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @CubicMetersOLD=(select CubicMetres from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @MetersOLD=(select Reefadv from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @MetersWasteOLD=(select Wasteadv from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @FLOLD=(select FL from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @sectionidOLD=(select sectionid from planmonth where prodmonth=@prodmonth and workplaceid=@Workplaceid and plancode='LP')
SET @daycrewOLD=(select orgunitday from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD  and workplaceid=@Workplaceid and plancode='LP')
SET @AfternoonCrewOLD=(select [OrgUnitAfternoon] from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD and workplaceid=@Workplaceid and plancode='LP')
SET @NightCrewOLD=(select [OrgUnitNight] from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD and workplaceid=@Workplaceid and plancode='LP')
SET @RovingCrewOLD=(select [RomingCrew] from planmonth where prodmonth=@prodmonth and sectionid=@sectionidOLD and workplaceid=@Workplaceid and plancode='LP')
SET @SectionNameOLD=(select distinct Name from section where Prodmonth = @prodmonth and sectionid=@sectionidOLD)
SET @stopdateOLD =(select StoppedDate from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @startdateOLD =(select StartDate from planmonth where prodmonth=@prodmonth and sectionid=@sectionid and workplaceid=@Workplaceid and plancode='LP')
SET @MiningMethodOLD=(select Description from planmonth PP LEFT JOIN Code_Methods BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PP.TargetID=BPD.TargetID where prodmonth=@prodmonth and sectionid=@sectionid and PP.workplaceid=@Workplaceid and plancode='LP')
SET @miningmethodMOVEPLAN=(select Description from planmonth PP LEFT JOIN Code_Methods BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PP.TargetID=BPD.TargetID where prodmonth=@prodmonth and sectionid=@sectionid and PP.Workplaceid=@OldWP and plancode='LP')
SET @FLMOVEPLAN=(select FL from planmonth PP LEFT JOIN Code_Methods BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PP.TargetID=BPD.TargetID where prodmonth=@prodmonth and sectionid=@sectionid and PP.WORKPLACEID=@OldWP and plancode='LP')

SET @DrillRig=(select DrillRig from PrePlanning_ChangeRequest where changerequestid=@ChnagrequestID)


if @changeID=4 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th></tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLOLD as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
			
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWasteOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML
 if @changeID=4 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Facelength  </th>'+
				N'<td>' + ISNULL(cast(@FLOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML

 if @changeID=3
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Miner  </th>'+
				N'<td>' + ISNULL(cast(@SectionNameOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Day Crew </th>'+
				N'<td>' + ISNULL(cast(@daycrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@daycrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Afternoon Crew </th>'+
				N'<td>' + ISNULL(cast(@AfternoonCrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@AfternoonCrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Night Crew </th>'+
				N'<td>' + ISNULL(cast(@NightCrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@NightCrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Roming Crew </th>'+
				N'<td>' + ISNULL(cast(@RovingCrewOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@RovingCrew as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML
 if @changeID=1 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Stop Date  </th>'+
				N'<td>' + ISNULL(cast(@stopdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@stopdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML
if @changeID=1 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Stop Date  </th>'+
				N'<td>' + ISNULL(cast(@stopdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@stopdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWasteOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML

 if @changeID=6 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			
				--N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQMOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML

 if @changeID=6 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th> <th> Lock Plan </th> <th> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdateOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWasteOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMetersOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML

  if @changeID=2 and @activity=1
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th><th width="70%"> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			N'<tr>'+
				N'<th> Miner  </th>'+
				N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@Meters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>' +
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef Meters </th>'+
				N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@MetersWaste as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML
  if @changeID=2 and @activity=0
begin
SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border=1> '+
				N'<tr>'+
				N'<th>  </th><th width="70%"> New Plan </th></tr>'+
				N'<tr>'+
				N'<th> Start Date  </th>'+
				N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@startdate as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
			N'<tr>'+
				N'<th> Miner  </th>'+
				N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@SectionName as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Facelength  </th>'+
				N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> On Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@ReefSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Off Reef SQM </th>'+
				N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WasteSQM as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Cubic Meters </th>'+
				N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@CubicMeters as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'</table>'+
				N'</body>'+
				N'</html>'
end
 --PRINT @tableHTML
  if @changeID=5
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				N'<tr>'+
				N'<th> Workplace  </th>'+
				N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@miningmethodMOVEPLAN as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				N'<tr>'+
				N'<th> Facelength  </th>'+
				N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML

 if @changeID=7
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				--N'<tr>'+
				--N'<th> Workplace  </th>'+
				--N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				N'<tr>'+
				N'<th> Mining Method  </th>'+
				N'<td>' + ISNULL(cast(@MiningMethodOLD as varchar(20)),'')  + '</td>'+
				N'<td>' + ISNULL(cast(@MiningMethod as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end

 if @changeID=8
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				--N'<tr>'+
				--N'<th> Workplace  </th>'+
				--N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				N'<tr>'+
				N'<th> Drill Rig  </th>'+
				N'<td>' + ISNULL(cast(@DrillRig as varchar(50)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end

 if @changeID=9
begin
 SET @tableHTML =N'<html>'+

				N'<body>'+	
				
				N'<table border = 1> '+
				N'<tr>'+
				N'<th>  </th> <th width="70%"> Lock Plan </th> <th width="70%"> New Plan </th> </tr>'+
				--N'<tr>'+
				--N'<th> Workplace  </th>'+
				--N'<td>' + ISNULL(cast(@OldWP as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@WPDesc as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				N'<tr>'+
				N'<th> Delete Workplace  </th>'+
				N'<td>' + ISNULL(cast(@WPDesc as varchar(50)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				N'</tr>'+
				--N'<tr>'+
				--N'<th> Facelength  </th>'+
				--N'<td>' + ISNULL(cast(@FLMOVEPLAN as varchar(20)),'')  + '</td>'+
				--N'<td>' + ISNULL(cast(@FL as varchar(20)),'')  + '</td>'+
				----N'<td>' + ISNULL(cast(@activity as varchar(20)),'')  + '</td>'+
				--N'</tr>'+
				
				N'</table>'+
				N'</body>'+
				N'</html>'
end
-- PRINT @tableHTML
RETURN @tableHTML  
END	
GO
ALTER Procedure [dbo].[sp_PrePlanning_MoveData]
@ChangeRequestID INT
AS
SELECT DISTINCT SC.Name_2 Section,SC.name_2,SC.Name,PPCR.ProdMonth,PPCR.OldWorkplaceID,PPCR.StartDate,PPCR.WorkplaceID,PPCR.DayCrew,
                                   PPCR.SectionID,PPCR.AfternoonCrew,PPCR.NightCrew,PPCR.RovingCrew,WP.DESCRIPTION WPDesc,
                                   PPCR.StopDate,PPCR.MiningMethod,BPD.Description,BPD.TargetID,PPCR.Comments,PPCR.ReefSQM,PPCR.WasteSQM,PPCR.Meters,
								   PPCR.MetersWaste,PPCR.CubicMeters,
                                   PPCR.SectionID,PPCR.SectionID_2 FROM PrePlanning_ChangeRequest PPCR
                                   INNER JOIN SECTION_COMPLETE SC on
                                   PPCR.SectionID_2 = SC.SECTIONID_2 and
                                   PPCR.ProdMonth = SC.PRODMONTH and 
                                   PPCR.SectionID = SC.SECTIONID 
                                   INNER JOIN WORKPLACE WP on 
                                   PPCR.WorkplaceID = WP.WORKPLACEID 
                                    INNER JOIN Code_Methods BPD ON --" + TSystemSettings.Bonus_Database + " dbo.Bonus_PoolDefaults BPD on 
                                   PPCR.MiningMethod=BPD.TargetID 
                                   WHERE PPCR.ChangeRequestID = @ChangeRequestID
GO

