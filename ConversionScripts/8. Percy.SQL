insert into code_hoppers values ('Hopper', 3.50)
go
-- [sp_Load_Tramming_Booking] '201705', 'REA', '2017-05-16'
ALTER Procedure [dbo].[sp_Load_Tramming_Booking] 
@Prodmonth varchar(6),
@Section Varchar(50),
@Bookdate DateTime

AS
select (select Name from OREFLOWENTITIES where OreFlowID = [PLAN].BoxholeID) BH,
[PLAN].BoxHoleID Workplaceid
, [Plan].Tons, isnull(Book.Night, 0)  + isnull(Book.Morning, 0) + isnull(Book.Afternoon, 0) 
* (select theFactor from Code_Hoppers) BookedDailyTons, 
Convert(decimal(10,2),isnull((select sum(Morning + Afternoon + Night) 
from Book_Tramming 
where Prodmonth = @Prodmonth and bookdate <= @Bookdate), 0))
Progressive,
[Plan].HoppersPerShift, Units.Units,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Night, 0)) else isnull(Book_Comments.Night, '') end Night,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Morning, 0)) else isnull(Book_Comments.Morning, '') end Morning,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Afternoon, 0)) else isnull(Book_Comments.Afternoon, '') end Afternoon,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Night, 0)  + isnull(Book.Morning, 0) + isnull(Book.Afternoon, 0)) else '' end  Total
from (select a.Prodmonth, b.sectionid_2 SectionID, 
w.BoxholeID BoxHoleID, Sum(Tons)Tons, 
Convert(int, round(Sum(Tons/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from planmonth a inner join WORKPLACE w on a.Workplaceid = w.WorkplaceID inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid and a.PlanCode = 'MP'
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid, Code_Hoppers h
where a.prodmonth = @Prodmonth and b.sectionid_2 = @Section and (w.BoxholeID is not null or w.BoxholeID <> '')
group by a.prodmonth, b.sectionid_2, w.BoxholeID
union
select a.Prodmonth, sectionid_2 SectionID
, w.BoxholeID BoxHoleID, 
Sum(units*(ActualDepth/100)*1.67) Tons, 
Convert(int, round(Sum((units*(ActualDepth/100)*1.67)/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from planmonth_oldgold a inner join WORKPLACE w on a.WorkplaceID = w.WorkplaceID inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid, Code_Hoppers h
where a.prodmonth = @Prodmonth and b.sectionid_2 = @Section and (w.BoxholeID is not null or w.BoxholeID <> '')
group by a.prodmonth, sectionid_2, w.BoxholeID
union
select a.Prodmonth, sectionid_2 SectionID, BoxHoleID Workplaceid,
 Sum(units) Tons, 
Convert(int, round(Sum((units)/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from PLANMONTH_SUNDRYMINING a inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid
inner join SUNDRYMINING_TYPE s on a.SMID = s.SMID and s.UnitBase = 5, Code_Hoppers h
where a.prodmonth = @Prodmonth and sectionid_2 = @Section and (BoxHoleID is not null or BoxHoleID <> '')
group by a.prodmonth, sectionid_2, BoxHoleID) [Plan] inner join 
(select a.PRODMONTH, SECTIONID_2, NAME_2, CALENDARDATE, min(BeginDate) BEGINDATE, max(ENDDATE) ENDDATE, max(WORKINGDAY) WORKINGDAY
from seccal a inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionID_1
inner join CALTYPE c on a.CalendarCode = c.CalendarCode and a.BeginDate <= c.CALENDARDATE and a.ENDDATE >= c.CALENDARDATE
Where a.prodmonth = @Prodmonth and SECTIONID_2 = @Section group by a.PRODMONTH, SECTIONID_2, NAME_2, CALENDARDATE) Cal on
[Plan].Prodmonth = Cal.PRODMONTH and [Plan].Sectionid = Cal.SECTIONID_2
left join Book_Tramming Book on [Plan].prodmonth = Book.Prodmonth and [Plan].Sectionid = Book.sectionid and
[Plan].BoxHoleID = Book.Workplaceid and cal.CALENDARDATE = Book.Bookdate
left join Book_Tramming_Comments Book_Comments on [Plan].prodmonth = Book_Comments.Prodmonth and [Plan].Sectionid = Book_Comments.sectionid and
[Plan].BoxHoleID = Book_Comments.Workplaceid and cal.CALENDARDATE = Book_Comments.Bookdate
left join WORKPLACE wp on [Plan].BoxHoleID = wp.WORKPLACEID,
(select 1 Orderby, 'Hoppers' Units) Units
where [Plan].prodmonth = @Prodmonth and Cal.SECTIONID_2 = @Section and Cal.CALENDARDATE = @Bookdate
and [Plan].BoxHoleID != ''
order by Orderby
GO

-- [sp_PlannedVsBooked_Daily] 'P', '201701', '1.5 - T Cwati', '2017/01/31', '7', '1', '', '1', 'S', 'Tons'
CREATE procedure [dbo].[sp_PlannedVsBooked_Daily]
@TypeMonth varchar(1),
@ProdMonth varchar(6),
@SectionName varchar(60),
@RunDate varchar(10),
@SumLevel varchar(1),
@Activity varchar(1),
@Account varchar(1),
@ReefWaste varchar(1),
@WorkCap varchar(1),
@Unit varchar(20)

AS   
declare 
@TheMinDate varchar(10),
@TheMaxDate varchar(10)

Declare @TheLevel Int,
@SQL1 Varchar(max),
@SQL4 Varchar(max),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20),
@GroupCrewLevel Varchar(20),
@GroupWPLevel Varchar(20),
@SectionWPLevel Varchar(20)

select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1' 
       set @GroupLevel = 'NAME_5'
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'    
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4'
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'      
If @SumLevel = '6' 
       set @GroupLevel = 'NAME'

If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'Description'  
	   set @GroupCrewLevel = 'Description' 
END
If @SumLevel = '8'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END
  

If @TheLevel = 1 
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  

DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)

IF (@TypeMonth = 'P')
BEGIN
       SET @SQL2 =
         ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                       convert(varchar(10), max(c.CalendarDate),120)      
             from planmonth p 
             inner join section_complete sc on 
                    p.prodmonth = sc.prodmonth and
                    p.sectionid = sc.sectionid
             inner join seccal s on
                    sc.prodmonth = s.prodmonth and
                    sc.sectionid_1 = s.sectionid
             inner join CalType c on
                    s.CalendarCode = c.CalendarCode and
                    s.BeginDate <= c.CalendarDate and
                    s.Enddate >= c.CalendarDate
             Where 
                    sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                    p.Prodmonth = '''+ @ProdMonth +''' and 
                    p.Activity = '+ @Activity +' and 
                    p.PlanCode = ''MP'' '

             CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
             INSERT #TheMinDate EXEC(@SQL2)
         
             SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
             SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
	
             DROP TABLE #TheMinDate

       if @TheMinDate is null 
             set @TheMinDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate is null
             set @TheMaxDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
				
			 print(@TheMaxDate)	 
END
ELSE
BEGIN
       select @TheMinDate = Convert(varchar(10),min(StartDate),120),
               @TheMaxDate = Convert(varchar(10),max(EndDate),120)
       from CALENDARMILL
       where MillMonth = @ProdMonth

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
END

Set @SQL1 = 'select * from (
       Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   
IF @SumLevel = '7'  
BEGIN
	Set @SQL1 = @SQL1 + ' w.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
END
ELSE
BEGIN  
	IF @SumLevel = '8'  
	BEGIN
		Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
	END       
	ELSE
		Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
END		


	   
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL1 = @SQL1 + '
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL1 = @SQL1 + '      
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)), 0) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = isnull(cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = isnull(cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0),    
             Booked = isnull(cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = isnull(cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0),     
             Booked = isnull(cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = isnull(cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0),    
             Booked = isnull(cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = isnull(cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0),
             Booked = isnull(cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
END    

Set @SQL1 = @SQL1 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID 
			 inner join workplace w on
			   w.workplaceid = pd.workplaceid
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
			IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL1 = @SQL1 + ' and w.AccountCode = '+ @Account +' '    
			END
SET @SQL1 = @SQL1 + ') pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate 
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
			 
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL1 = @SQL1 + ' and w.AccountCode = '+ @Account +' '    
END

IF @SumLevel = '7'  
BEGIN
	Set @SQL1 = @SQL1 + '
		   group by ct.CalendarDate, w.'+@GroupCrewLevel+' '
END
ELSE
BEGIN   
	IF @SumLevel = '8'  
	BEGIN
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, w.'+@GroupWPLevel+' '
	END   
	ELSE   
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, ss.'+@GroupLevel+' '
END


Set @SQL4 = ' union all'
Set @SQL4 = @SQL4+
       ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL4 = @SQL4 + '
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL4 = @SQL4 + '      
                    Planned = isnull(cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)), 0),
                    Booked = isnull(cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Metres Advance')
              BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = isnull(cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)), 0) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)), 0) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = isnull(cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = isnull(cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0),    
             Booked = isnull(cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = isnull(cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0),     
             Booked = isnull(cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = isnull(cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0),    
             Booked = isnull(cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = isnull(cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0),
             Booked = isnull(cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
END    

Set @SQL4 = @SQL4 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID
			 inner join Workplace w on
			   w.workplaceid=pd.workplaceid 
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL4 = @SQL4 + ' and w.AccountCode = '+ @Account +' '
     
			END

      Set @SQL4 = @SQL4 + ' ) pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate and
                    p.PlanCode = pd.PlanCode
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL4 = @SQL4 + ' and w.AccountCode = '+ @Account +' '     
END
             
Set @SQL4 = @SQL4 + '
       group by ct.CalendarDate ) a 
order by a.theSort, a.Section, a.CalendarDate'
--print(@SQL1)
--print(@SQL4)
EXEC (@SQL1+@SQL4)  

GO

-- [sp_PlannedVsBooked_Prog] 'P', '201701', '1.5 - T Cwati', '2017/01/31', '7', '1', '', '1', 'S', 'Tons'
CREATE procedure [dbo].[sp_PlannedVsBooked_Prog]

--Declare
    @TypeMonth varchar(1),
       @ProdMonth varchar(6),
       @SectionName varchar(60),
       @RunDate varchar(10),
       @SumLevel varchar(1),
       @Activity varchar(1),
       @Account varchar(1),
       @ReefWaste varchar(1),
       @WorkCap varchar(1),
       @Unit varchar(20)
as
       
declare @TheMinDate varchar(10),@TheMaxDate varchar(50)
Declare @TheLevel Int,
    @SQL1 Varchar(8000),
       @SQL4 Varchar(8000),
    @GroupLevel Varchar(20),
    @SectionLevel Varchar(20),
	@GroupWPLevel Varchar(20),
	@GroupCrewLevel Varchar(20)

select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1'
       set @GroupLevel = 'NAME_5'    
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'      
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4' 
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'

If @SumLevel = '6'
set @GroupLevel = 'NAME'

If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END 

If @TheLevel = 1 
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  

If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END 

DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)
IF (@TypeMonth = 'P')
BEGIN
    SET @SQL2 =
        ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                    convert(varchar(10), max(c.CalendarDate),120)    
            from planmonth p 
            inner join section_complete sc on 
                p.prodmonth = sc.prodmonth and
                p.sectionid = sc.sectionid
            inner join seccal s on
                sc.prodmonth = s.prodmonth and
                sc.sectionid_1 = s.sectionid
            inner join CalType c on
                s.CalendarCode = c.CalendarCode and
                s.BeginDate <= c.CalendarDate and
                s.Enddate >= c.CalendarDate
            Where 
                sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                p.Prodmonth = '''+ @ProdMonth +''' and 
                p.Activity = '+ @Activity +' and 
                p.PlanCode = ''MP'' '
             
            CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
            INSERT #TheMinDate EXEC(@SQL2)
         
            SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
            SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
            DROP TABLE #TheMinDate

    if @TheMinDate is null 
            set @TheMinDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate is null
            set @TheMaxDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END
ELSE
BEGIN
    select @TheMinDate = Convert(varchar(10),min(StartDate),120),
            @TheMaxDate = Convert(varchar(10),max(EndDate),120)
    from CalendarMill
    where MillMonth = @ProdMonth

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END

IF (@TypeMonth = 'P')
BEGIN
       Set @SQL1 = 'select * from ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '

	   --ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
	    IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' p.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
		END
		ELSE
		BEGIN 
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
			END
			ELSE        
				Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
		END	
                
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END                     
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END                
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0),     
                            Booked = isnull(cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
               Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
       END    

       Set @SQL1 = @SQL1 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                              w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
					IF (@Activity = '1')
					   BEGIN
								IF (@Account <> '')
										   Set @SQL1 = @SQL1 + ' and w.AccountCode = '+ @Account +' '     
					   END 
                    Set @SQL1 = @SQL1 + ' ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        p.PlanCode = pd.PlanCode and
                                        ct.CalendarDate >= pd.Calendardate
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                            p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ' and w.AccountCode = '+ @Account +' '    
       END
             
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, p.'+ @GroupCrewLevel
		END
		ELSE
		BEGIN     
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, w.'+ @GroupWPLevel
			END
			ELSE    
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, ss.'+ @GroupLevel
		END	

              --  group by ct.CalendarDate, ss.'+@GroupLevel+' 


       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END                       
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)), 0)'
                           END              
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0)'
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0),     
                            Booked = isnull(cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
       END    

       Set @SQL4 = @SQL4 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                             inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                            w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
				IF (@Activity = '1')
				   BEGIN
							IF (@Account <> '')
									   Set @SQL4 = @SQL4 + ' and w.AccountCode = '+ @Account +' '     
				   END
              Set @SQL4 = @SQL4 + '  ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        ct.CalendarDate >= pd.Calendardate and
                                        p.PlanCode = pd.PlanCode
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                           p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ' and w.AccountCode = '+ @Account +' '    
       END
             
       Set @SQL4 = @SQL4 + '
                group by ct.CalendarDate ) a 
       order by a.theSort, a.Section, a.CalendarDate'
END




IF (@TypeMonth = 'M')
BEGIN 
       --declare @TheMinDate datetime
       --declare @TheMaxDate datetime
       declare @thedate datetime

       --set @TheMinDate = '2015-05-01'
       --set @TheMaxDate = '2015-05-29'

       create table #daterange (theDate varchar(10))

    set @thedate = @TheMinDate
    while @thedate <= @TheMaxDate
    begin
             insert into #daterange values (convert(varchar(10),@thedate,120))
             set @thedate = dateadd(day, 1, @thedate)
    end


Set @SQL1 = '
       select * from 
       ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   --'+ @GroupLevel +' Section, theDate CalendarDate, '
		
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + @GroupCrewLevel +' Section, theDate CalendarDate, '
		END
		ELSE
		BEGIN   
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + @GroupWPLevel +' Section, theDate CalendarDate, '
			END
			ELSE      
				Set @SQL1 = @SQL1 +  @GroupLevel +' Section, theDate CalendarDate, '
		END

       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END                     
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END               
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0),     
                            Booked = isnull(cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
       END    

       Set @SQL1 = @SQL1 + '
             from #daterange 
                LEFT outer join '

				IF @SumLevel = '8'  
	   			BEGIN
					Set @SQL1 = @SQL1 + '(select p.'+ @GroupCrewLevel +' , pd.CalendarDate, '
				END
				ELSE
				BEGIN  
					IF @SumLevel = '9'  
	   				BEGIN
						Set @SQL1 = @SQL1 + '(select w.'+ @GroupWPLevel +' , pd.CalendarDate, '
					END
					ELSE       
						Set @SQL1 = @SQL1 + '(select sc.'+ @GroupLevel +' , pd.Calendardate , '
				END
                        --    (select sc.'+ @GroupLevel +' , pd.Calendardate ,
                    Set @SQL1 = @SQL1 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
										pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                            from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid 
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ' and w.AccountCode = '+ @Account +' '   
       END 
             Set @SQL1 = @SQL1 + ' ) pd on theDate >= pd.Calendardate '
                --    group by pd.'+ @GroupLevel +', theDate '
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by p.'+ @GroupCrewLevel +' , theDate'
			END
			ELSE
			BEGIN   
				IF @SumLevel = '9'  
	   			BEGIN
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupWPLevel +' , theDate'
				END
				ELSE      
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupLevel +' , theDate '
			END

       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, theDate CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = isnull(cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                        Booked = isnull(cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END                      
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)), 0) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = isnull(cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)), 0),
                                                  Booked = isnull(cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)), 0) '
                           END                
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)), 0) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                          Planned = isnull(cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0),     
                            Booked = isnull(cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)), 0) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0),    
                            Booked = isnull(cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)), 0) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = isnull(cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0),
                           Booked = isnull(cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)), 0) ' 
       END    

       Set @SQL4 = @SQL4 + '
                    from #daterange d
                LEFT outer join '
                           -- (select sc.'+ @GroupLevel +', pd.Calendardate , 
						    IF @SumLevel = '8'  
	   						BEGIN
								Set @SQL4 = @SQL4 + ' (select p.'+ @GroupCrewLevel +' , pd.Calendardate , '
							END
							ELSE
							BEGIN   
								IF @SumLevel = ''  
	   							BEGIN
									Set @SQL4 = @SQL4 + ' (select p.'+ @GroupWPLevel +' , pd.Calendardate , '
								END
								ELSE      
									Set @SQL4 = @SQL4 + ' (select sc.'+ @GroupLevel +', pd.Calendardate , '
							END
                           Set @SQL4 = @SQL4 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

							from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid  
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                       pd.PlanCode = ''MP'' '
	IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ' and w.AccountCode = '+ @Account +' '     
       END 
          Set @SQL4 = @SQL4 + '   ) pd on theDate >= pd.Calendardate
             group by ' --pd.'+ @GroupLevel +', theDate
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL4 = @SQL4 + ' p.'+ @GroupCrewLevel +' , theDate '
			END
			ELSE
			BEGIN 
				IF @SumLevel = '9'  
	   		BEGIN
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupWPLevel +' , theDate '
				END
				ELSE      
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupLevel +', theDate '
			END
      Set @SQL4 = @SQL4 + '  ) z order by theSort, MaxDate, Section, CalendarDate '

END


EXEC (@SQL1+@SQL4)  

--print @SQL1 
--print @SQL4

IF (@TypeMonth = 'M')
BEGIN
	drop table #daterange
END

GO

-- [sp_DailyGradeReport] '2017-02-02', 2, 'Y', 1330, 860
CREATE Procedure [dbo].[sp_DailyGradeReport]
--Declare
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int

as 
--Delete from [Temp_DailyGrade] where USERID = Host_Name()

--Insert Into [Temp_DailyGrade]
Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then '      '+Description
when isnull(MO,'') <> '' and isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 1
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 'Total '+MO
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(

--declare @theDate datetime
--declare @Shaft varchar(50)
--set @theDate = '2016-06-21'
--set @Shaft = '3#'
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
--declare @theDate datetime
--declare @Shaft varchar(50)
--set @theDate = '2016-06-21'
--set @Shaft = '3#'

Select 
b.sectionid_2 MO,
e.Description,
ShiftNo = Sum(Case when d.WorkingDay = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*w.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * w.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * w.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*w.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * w.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * w.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * w.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * w.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
inner join workplace w on
a.workplaceid = w.workplaceid

Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by b.sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
GO

-- [sp_DailyGradeReport_Development] '2017-02-02', 2, 'Y', 1330, 860
CREATE Procedure [dbo].[sp_DailyGradeReport_Development]
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int

AS
Select 'AA' TheTop,
	'MO '+MO MO,
	[Description],
	ShiftNo = Min(ShiftNo),
	TotalShifts = Min(TotalShifts),
	LockPlan_Adv  = Sum(LockPlan_Adv),
	LockPlan_ReefAdv  = Sum(LockPlan_ReefAdv),
	LockPlan_WasteAdv  = Sum(LockPlan_WasteAdv),
	LockPlan_Tonnes = Sum(LockPlan_Tonnes),
	LockPlan_ReefTons = Sum(convert(decimal(10,5),LockPlan_ReefTons)),
	LockPlan_WasteTons = Sum(convert(decimal(10,5),LockPlan_WasteTons)),
	LockPlan_gt = Case When Sum(LockPlan_ReefTons) = 0 then 0 else Sum(LockPlan_Grams)*1000/Sum(LockPlan_ReefTons) end,
	LockPlan_Kg = Sum(LockPlan_Grams),

	DynPlan_Adv  = Sum(DynPlan_Adv),
	DynPlan_ReefAdv  = Sum(DynPlan_ReefAdv),
	DynPlan_WasteAdv  = Sum(DynPlan_WasteAdv),
	DynPlan_Tonnes = Sum(DynPlan_Tonnes),
	DynPlan_ReefTons = Sum(convert(decimal(10,5),DynPlan_ReefTons)),
	DynPlan_WasteTons = Sum(convert(decimal(10,5),DynPlan_WasteTons)),
	DynPlan_gt = Case When Sum(DynPlan_ReefTons) = 0 then 0 else Sum(DynPlan_Grams)*1000/Sum(DynPlan_ReefTons) end,
	DynPlan_Kg = Sum(DynPlan_Grams),

	DynDayPlan_Adv  = Sum(DynDayPlan_Adv),
	DynDayPlan_ReefAdv  = Sum(DynDayPlan_ReefAdv),
	DynDayPlan_WasteAdv  = Sum(DynDayPlan_WasteAdv),
	DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
	DynDayPlan_ReefTons = Sum(convert(decimal(10,5),DynDayPlan_ReefTons)),
	DynDayPlan_WasteTons = Sum(convert(decimal(10,5),DynDayPlan_wasteTons)),
	DynDayPlan_gt = Case When Sum(DynDayPlan_ReefTons) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_ReefTons) end,
	DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

	DayBook_Adv  = Sum(DayBook_Adv),
	DayBook_ReefAdv  = Sum(DayBook_ReefAdv),
	DayBook_WasteAdv  = Sum(DayBook_WasteAdv),
	DayBook_Tonnes = Sum(DayBook_Tonnes),
	DayBook_ReefTons = Sum(convert(decimal(10,5),DayBook_ReefTons)),
	DayBook_WasteTons = Sum(convert(decimal(10,5),DayBook_WasteTons)),
	DayBook_cmgt = 0,
	DayBook_gt = Case When Sum(DayBook_ReefTons) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_ReefTons) end,
	DayBook_Kg = Sum(DayBook_Grams)/1000,

	DynProgPlan_Adv  = Sum(DynProgPlan_Adv),
	DynProgPlan_ReefAdv  = Sum(DynProgPlan_ReefAdv),
	DynProgPlan_WasteAdv  = Sum(DynProgPlan_WasteAdv),
	DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
	DynProgPlan_ReefTons = Sum(convert(decimal(10,5),DynProgPlan_ReefTons)),
	DynProgPlan_WasteTons = Sum(convert(decimal(10,5),DynProgPlan_WasteTons)),
	DynProgPlan_gt = Case When Sum(DynProgPlan_ReefTons) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_ReefTons) end,
	DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

	ProgBook_Adv  = Sum(ProgBook_Adv),
	ProgBook_ReefAdv  = Sum(ProgBook_ReefAdv),
	ProgBook_WasteAdv  = Sum(ProgBook_WasteAdv),
	ProgBook_Tonnes = Sum(ProgBook_Tonnes),
	ProgBook_ReefTons = Sum(convert(decimal(10,5),ProgBook_ReefTons)),
	ProgBook_WasteTons = Sum(convert(decimal(10,5),ProgBook_WasteTons)),
	ProgBook_gt = Case When Sum(ProgBook_ReefTons) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_ReefTons) end,
	ProgBook_Kg = Sum(ProgBook_Grams)/1000,

	ProgVar_Adv = sum(ProgBook_Adv - DynProgPlan_Adv),
	ProgVar_ReefAdv = sum(ProgBook_ReefAdv - DynProgPlan_ReefAdv),
	ProgVar_WasteAdv = sum(ProgBook_Adv - DynProgPlan_WasteAdv),
	ProgVar_Tonnes = sum(ProgBook_Tonnes - DynProgPlan_Tonnes),
	ProgVar_ReefTons = sum(ProgBook_ReefTons - DynProgPlan_ReefTons),
	ProgVar_WasteTons = sum(ProgBook_wasteTons - DynProgPlan_WasteTons),
	ProgVar_gt = case when sum(ProgBook_Tonnes - DynProgPlan_Tonnes) > 0 then
					 sum(ProgBook_Grams - DynProgPlan_Grams) / 
					 sum(ProgBook_Tonnes - DynProgPlan_Tonnes) end,
	ProgVar_Kg = sum(ProgBook_Grams - DynProgPlan_Grams) / 1000,

	ForeCast_WasteAdv = sum(case when @DefaultShift = 'Y' then ProgBook_WasteAdv/ShiftNo * TotalShifts
					else ProgBook_WasteAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefAdv = sum(case when @DefaultShift = 'Y' then ProgBook_ReefAdv/ShiftNo * TotalShifts
					else ProgBook_ReefAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Adv = sum(case when @DefaultShift = 'Y' then ProgBook_Adv/ShiftNo * TotalShifts
					else ProgBook_Adv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Tonnes = sum(case when @DefaultShift = 'Y' then ProgBook_Tonnes / ShiftNo * TotalShifts
							else ProgBook_Tonnes / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefTons = sum(case when @DefaultShift = 'Y' then ProgBook_ReefTons / ShiftNo * TotalShifts
							else ProgBook_ReefTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_WasteTons = sum(case when @DefaultShift = 'Y' then ProgBook_WasteTons / ShiftNo * TotalShifts
							else ProgBook_WasteTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Kg = sum(case when @DefaultShift = 'Y' then (ProgBook_Grams / 1000) / ShiftNo * TotalShifts
							else (ProgBook_Grams / 1000) / ShiftNo * (TotalShifts-@ShiftsNo) end),

	[Top10] = Sum([Top10]),
	Colour = cast((case when  sum(ProgBook_Adv) = 0  then 1   
						 else 0  end) as varchar(1))


from 
(

	Select 
		sc.SectionID_2 MO,
		w.[Description],
		ShiftNo = Sum(Case when ct.WorkingDay = 'Y' then 1 else 0 end),
		TotalShifts = Avg(convert(numeric(10,0), TotalShifts)),

		LockPlan_Adv = max(Isnull(lp.MetresAdvance,0)),
		LockPlan_ReefAdv = max(Isnull(lp.ReefAdv,0)),
		LockPlan_WasteAdv = max(Isnull(lp.WasteAdv,0)),
		LockPlan_Tonnes = max(Isnull(lp.Tons,0)),
		LockPlan_ReefTons = max(Isnull(lp.ReefTons,0)),
		LockPlan_WasteTons = max(Isnull(lp.WasteTons,0)),
		LockPlan_Grams = max(Isnull(lp.Kg,0)),--kg

		DynPlan_Adv = isnull(max(pm.MetresAdvance),0),
		DynPlan_ReefAdv = isnull(max(pm.ReefAdv),0),
		DynPlan_WasteAdv = isnull(max(pm.WasteAdv),0),
		DynPlan_Tonnes = max(Isnull(pm.Tons,0)),
		DynPlan_ReefTons = max(Isnull(pm.ReefTons,0)),
		DynPlan_WasteTons = max(Isnull(pm.WasteTons,0)),
		DynPlan_Grams = Isnull(max(pm.Kg),0),--kg


		DynProgPlan_Adv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynProgPlan_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynProgPlan_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynProgPlan_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Tons,0) end),
		DynProgPlan_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefTons,0) end),
		DynProgPlan_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteTons,0) end),
		DynProgPlan_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Grams,0) end),

		ProgBook_Adv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookMetresAdvance,0) end),
		ProgBook_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefAdv,0) end),
		ProgBook_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteAdv,0) end),
		ProgBook_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookTons,0) end),
		ProgBook_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefTons,0) end),
		ProgBook_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteTons,0) end),
		ProgBook_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.BookGrams,0) end),


		DynDayPlan_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynDayPlan_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynDayPlan_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynDayPlan_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.Tons,0) end),
		DynDayPlan_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.ReefTons,0) end),
		DynDayPlan_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.WasteTons,0) end),
		DynDayPlan_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.Grams,0) end),

		DayBook_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookMetresAdvance,0) end),
		DayBook_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookReefAdv,0) end),
		DayBook_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookWasteAdv,0) end),
		DayBook_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookTons,0) end),
		DayBook_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookReefTons,0) end),
		DayBook_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookWasteTons,0) end),
		DayBook_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookGrams,0) end),

		TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

	from planmonth pm 
	inner join section_complete sc on
		pm.prodmonth = sc.prodmonth and
		pm.sectionid = sc.sectionID and
		pm.Plancode = 'MP'
	left join planmonth lp on
		pm.prodmonth = lp.prodmonth and
		pm.sectionid = lp.sectionID and
		pm.Workplaceid = lp.Workplaceid and
		pm.Activity = lp.Activity and
		lp.PlanCode = 'LP' and
		lp.Locked = 1
	inner join Seccal s on
		sc.prodmonth = s.prodmonth and
		sc.sectionid_1 = s.Sectionid  and
		s.BeginDate <= @TheDate and
		s.enddate >= @TheDate 
	inner join caltype ct on 
		s.Calendarcode = ct.Calendarcode and
		s.BeginDate <= ct.Calendardate and
		s.enddate >= ct.Calendardate
	Inner join Workplace w on
		pm.WorkplaceID = w.WorkplaceID
	left join Planning pd on
		pm.prodmonth = pd.prodmonth and
		pm.sectionid = pd.sectionID and
		pm.Workplaceid = pd.Workplaceid and
		pm.Activity = pd.Activity and
		pm.PLancode = pd.PLancode and
		ct.CalendarDate = pd.Calendardate
	--inner join vw_wp_density de on
	--	pm.workplaceid = de.workplaceid

	Left Join 
	(
		Select top 2000 a.* from PLanmonth a inner join section_complete b on
		a.prodmonth = b.prodmonth and
		a.sectionid = b.sectionID and
		a.Plancode = 'MP'
		inner join 
		Seccal c on
		b.prodmonth = c.prodmonth and
		b.sectionid_1 = c.Sectionid  and
		c.BeginDate <= @TheDate and
		c.enddate >= @TheDate
		inner join Workplace w on
		w.WorkplaceID = a.WorkplaceID
		where a.Activity = 1
		and a.Locked = 1 and
		auth = 'Y'
		and w.Priority = 0
		order by KG desc
	) [Top] on
	pm.prodmonth = [Top].prodmonth and
	pm.sectionid = [Top].sectionID and
	pm.Workplaceid = [Top].Workplaceid and
	pm.Activity = [Top].Activity and
	pm.PLancode = [Top].PLancode 

	where pm.Activity in (1)
	and ct.Calendardate <= @TheDate
	group by sc.SectionID_2, w.[Description]
) Main
Group by MO, [Description]
GO

-- [SP_Planning_Stope_Summary] '201701', '1.1 - A Dube', 2
CREATE Procedure [dbo].[SP_Planning_Stope_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(max),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID 
 from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'
    
  If @TheLevel = 1 
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  
    

Set @SQL1 = 'Select GroupSec,
Sum(SquareMetres) SquareMetres,
Sum(OnReefSQM) OnReefSQM,
Sum(OffReefSQM) OffReefSQM,
Sum(TargetSQM) TargetSQM,
convert(int,round(Sum(FaceLength),0)) FaceLength,
convert(int,round(Sum(OnReefFL),0)) OnReefFL,
convert(int,round(Sum(OffReefFL),0)) OffReefFL,
FaceAdv = Case when Sum(FaceLength) = 0 then 0 else
convert(Numeric(10,1),Sum(SquareMetres)/Sum(FaceLength)) end,
OnreefAdv = Case when Sum(OnreefFl) = 0 then 0 else
convert(Numeric(10,1),Sum(OnreefSQM)/Sum(OnreefFl)) end,
OffreefAdv = Case when Sum(OffreefFl) = 0 then 0 else
convert(Numeric(10,1),Sum(OffreefSQM)/Sum(OffreefFl)) end,
CW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))
end,
CTons = convert(int,round(Sum(Ctons),0)),
SW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))
end,
ISW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))
end,
Tons = convert(int,round(Sum(tons),0)),
OnReefTons = convert(int,round(Sum(OnReeftons),0)),
OffReefTons = convert(int,round(Sum(OffReeftons),0)),
Volume = convert(int,round(Sum(Volume),0)),
OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),
OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),
[g/t] =
case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else
Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))
end,
convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,
Cmgt =
Case when
  (Sum(OnreefSQM) = 0) or (Sum(OnreefSQM) is null) then 0 else
convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
end,
PaySQM = Sum(PaySQM),
UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),
PayTons = Sum(PayTons),
UnPayTons = Sum(Tons)-Sum(PayTons),
convert(int,Round(avg(PayLimit),0)) ShaftPayLimit, 
cubicmetres = Sum(cubicmetres),
CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),
CubicGrams = Convert(Numeric(7),Round(Sum(CubicGrams),0)),
CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
  
  SUM(Bus_Plan_SQM) Bus_Plan_SQM,
  convert(Numeric(10,3),round(Sum(Bus_Plan_Grams)/1000,3)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_Grams)*100)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end 
  from 
  (select  
    SelectSection, 
    GroupSec,
    SUM(SquareMetres) SquareMetres,
	SUM(OnReefSQM) OnReefSQM,  
    SUM(OffReefSQM) OffReefSQM,  
    SUM(TargetSQM) TargetSQM, 
    SUM(MetresAdvance) MetresAdvance,   
    SUM(OnReefAdv) OnReefAdv,   
    SUM(OffReefAdv) OffReefAdv,   
    SUM(FL) FaceLength,   
    SUM(FL) OnReefFL,   
    SUM(OffReefFL) OffReefFL,   
    SUM(CWSQM) CWSQM,  
    SUM(CTons) CTons,  
    SUM(SWSQM) SWSQM,  
    SUM(ISWSQM) ISWSQM,  
    SUM(Volume) Volume,  
    SUM(OnReefVolume) OnReefVolume,  
    SUM(OffReefVolume) OffReefVolume,  
    SUM(Tons) Tons,  
    SUM(ReefTons) ReefTons,  
    SUM(OnReefTons) OnReefTons,  
    SUM(OffReefTons) OffReefTons,  
    SUM(OnreefSQMDens) OnreefSQMDens,  
    SUM(Grams) Grams,  
    SUM(Kg) Kg,  
    SUM(PaySQM) PaySQM,  
    SUM(PayTons) PayTons,  
    --SUM(OnreefSQM) OnreefSQM,  
    avg(RockDensity) RockDensity,
    avg(PayLimit) PayLimit,  
    SUM(cubicmetres) cubicmetres,  
    SUM(CubicTons) CubicTons,
    SUM(CubicGrams) CubicGrams,   
    SUM(CubicGT) CubicGT,
    
    AVG(Bus_Plan_SQM) Bus_Plan_SQM,
    AVG(Bus_Plan_Grams) Bus_Plan_Grams
 from
(Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection, 
Convert(Numeric(7),a.SQM+a.WasteSQM) SquareMetres,
Convert(Numeric(7),a.SQM) OnReefSQM,
Convert(Numeric(7),a.WasteSQM) OffReefSQM,
0 TargetSQM,
a.FaceAdvance MetresAdvance, 
0 OnReefAdv, 
0 OffReefAdv, 
a.FL, 
a.FL OnReefFL, 
0 OffReefFL, 
CWSQM = a.SQM*a.CW,
CTons = a.SQM*a.CW*w.Density/100,
SWSQM = a.SQM*a.SW,
ISWSQM = a.SQM*sm.SWidth,
Volume = 0,
OnReefVolume = 0,
OffReefVolume = 0,
Tons = Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)),
ReefTons = Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)),
OnReefTons = Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)),
OffReefTons = 0,
OnreefSQMDens = a.SQM*w.Density,
Grams = a.cmgt*a.SQM*w.density/100,
Kg = convert(numeric(10,3),Round(a.cmgt*a.SQM*w.Density/100,0)/100),
0 PaySQM,
0 PayTons,
--Convert(Numeric(7),a.SQM) OnreefSQM,
s.RockDensity,
0 PayLimit,
Convert(Numeric(7),a.cubicmetres) cubicmetres,
Convert(Numeric(7),Round(a.cubicmetres*w.density,0)) CubicTons, 
0 CubicGrams, 
0 CubicGT,
0 Bus_Plan_SQM,
0 Bus_Plan_Grams
     
 from PLANMONTH a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on
--a.prodmonth = e.prodmonth and
--c.OreFlowID  = e.OreFlowID 
inner join workplace w on a.workplaceid = w.workplaceid
left join sampling sm on
--a.Prodmonth = sm.prodmonth and
a.workplaceid = sm.workplaceid

    --left join
    --(select * from MONTH_CORP_PLAN
    -- where PRODMONTH = '''+@Prodmonth+'''
    -- and Activity = 0) BusPlan on
    -- b.Name_2 = BusPlan.Name_2,

	 ,Sysset s
	where a.prodmonth = '''+@prodmonth+'''
	 and a.activity in (0,3)
	 and b.'+@SectionLevel+' = '''+@Section+''' and a.PlanCode = ''MP''
	) DetailCorrection
    Group by GroupSec, SelectSection 
    
    ) LastSum
    group by GroupSec' 

Exec(@SQL1)
 --PRINT(@SQL1)


 GO

-- [SP_Planning_Stope] '201701', '1.1 - A Dube'
CREATE Procedure [dbo].[SP_Planning_Stope]
--Declare 
@Prodmonth varchar(6),
@Section Varchar(30)

as

--set @Prodmonth = 201303
--set @Section = 'Shaft Manager 1# 1SUB'

Select Name_1, Name,Name_2, a.isStopped, Workplace,a.Workplaceid ,Description, min(BeginDate) BeginDate, max(EndDate) EndDate,
convert(int, Avg(TotalShifts)) TotalShifts,
Sum(SquareMetres) SquareMetres,
Sum(OnReefSQM) OnReefSQM,
Sum(OffReefSQM) OffReefSQM,
Sum(TargetSQM) TargetSQM,
convert(int,round(Sum(FL),0)) FaceLength,
convert(int,round(Sum(OnReefFL),0)) OnReefFL,
convert(int,round(Sum(OffReefFL),0)) OffReefFL,
FaceAdv =
  Case When (Sum(a.FL) = 0) or (Sum(a.FL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.SquareMetres)/Sum(a.FL))
end,
OnReefAdv =
  Case When (Sum(a.OnReefFL) = 0) or (Sum(a.OnReefFL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.OnReefSQM)/Sum(a.OnReefFL))
end,
OffReefAdv =
  Case When (Sum(a.OffReefFL) = 0) or (Sum(a.OffReefFL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.OffReefSQM)/Sum(a.OffReefFL))
end,
CW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))
end,
CTons = convert(int,round(Sum(Ctons),0)),
SW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))
end,
ISW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))
end,
Tons = convert(int,round(Sum(tons),0)),
OnReefTons = convert(int,round(Sum(OnReefTons),0)),
OffReefTons = convert(int,round(Sum(OffReefTons),0)),
Volume = convert(int,round(Sum(Volume),0)),
OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),
OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),
[g/t] =
case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else
Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))
end,
convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,
Cmgt =
Case when
  (Sum(OnreefSQM) = 0) or (Sum(OnreefSQM) is null) then 0 else
convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
end,
PaySQM = Sum(PaySQM),
UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),
PayTons = Sum(PayTons),
UnPayTons = Sum(Tons)-Sum(PayTons),
0 ShaftPayLimit,
Min(OrgUnitday) OrgUnitday,
Min(OrgUnitAfternoon) OrgUnitAfternoon,
Min(OrgUnitNight) OrgUnitNight,
Min(OrgUnitRoving) OrgUnitRoving,
Min(Convert(Varchar(10),TargetID)+':'+MineMet) MineMet,
Min(Remarks) Remarks,
Convert(numeric(6,2),Sum(LabourStrength)) CrewStrength,
Cubicmetres = Sum(a.Cubicmetres),
CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),
CubicGrams = Convert(Numeric(7),Round(Sum(CubicGrams),0)),
CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
Avg(Bus_Plan_SQM) Bus_Plan_SQM,
convert(Numeric(10,3),Avg(Bus_Plan_KG)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_KG)*100000)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end ,
Sum(SQMDens) SQMDens,
Sum(SWSQM) SWSQM,
Sum(CWSQM) CWSQM,
Sum(ISWSQM) ISWSQM,
Sum(Grams) Grams
 from
(Select Name_2, Name_1, Name, c.Description+':'+a.Workplaceid Workplace,a.Workplaceid,c.Description, d.BeginDate , d.enddate, d.TotalShifts,
Convert(Numeric(7),a.SQM+a.WasteSQM) SquareMetres,'' isStopped,
Convert(Numeric(7),a.ReefSQM) OnreefSQM,
Convert(Numeric(7),a.WasteSQM) OffreefSQm,
0 TargetSQM,
a.FaceAdvance MetresAdvance,
a.FaceAdvance OnreefAdv,
0 OffreefAdv,
OrgUnitday = Case when (a.OrgUnitday is null) then '' else a.OrgUnitday end,
OrgUnitAfternoon = Case when (a.OrgUnitAfternoon is null) then '' else a.OrgUnitAfternoon end,
OrgUnitNight = Case when (a.OrgUnitday is null) then '' else a.OrgUnitNight end,
OrgUnitRoving = Case when (a.RomingCrew is null) then '' else a.RomingCrew end,
TargetID = a.TargetID, '' MineMet,
'' remarks,
a.FL,
a.FL OnReefFL,
0 OffReefFL,
CTons = case when w.Density is null then 0 else 
a.SQM*a.CW*w.Density/100 end,
CWSQM = a.SQM*a.CW,
SWSQM = a.SQM*a.SW,
ISWSQM = a.SQM*sm.SWidth,
SQMDens = reefSQM*w.density,
ReefTons = case when w.Density is null then 0 else
a.SQM*a.SW*w.Density/100 end,
Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)) Tons, 
Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)) OnReefTons, 
0 OffReefTons, 
0 Volume, 
0 OnReefVolume, 
0 OffReefVolume, 
s.ROCKDENSITY,
Grams = a.cmgt*a.SQM*w.density/100, 
Kg = case when w.Density is null then 0 else
convert(numeric(10,3),Round(a.cmgt*a.SQM*w.Density/100,0)/100) end,
0 PaySQM,
0 PayTons,
--SQMDens = case when rc.density is null then 0 else
--a.SQM*rc.density end,
OnReefSQMDens = case when w.density is null then 0 else
a.SQM*w.density end,
OffreefSQMDens = case when w.density is null then 0 else
0 end,
0 PayLimit,
a.LabourStrength LabourStrength,
a.Cubicmetres,
Convert(Numeric(7),Round(a.Cubicmetres*w.density,0)) CubicTons, 
0 CubicGrams, 
0 CubicGT,
0 Bus_Plan_SQM,
0 Bus_Plan_KG  
 from PLANMONTH a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid left outer join WORKPLACE w on
a.workplaceid = w.workplaceid
inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on
--a.prodmonth = e.prodmonth and
--c.oreflowid = e.oreflowid
left join sampling sm on 
a.workplaceid = sm.workplaceid 
--left outer join Pers.dbo.Bonus_PoolDefaults bpd on 
-- a.TargetID = bpd.TargetID and
--bpd.Activity in (0,3)   
    --left join
    --(select * from planmonth_ops
    -- where PRODMONTH = @Prodmonth
    -- and Activity = 0) BusPlan on
    -- b.SECTIONID_2 = BusPlan.Sectionid_2
	 , SYSSET s
where a.prodmonth = @Prodmonth
and a.activity in (0,3)
and Name_2 = @Section) a
group by
Name_1, Name,Name_2,a.isStopped, Workplace, Workplaceid ,Description

GO

-- [SP_Dynamic_Planning_Stope_Summary] '201701', '1.1 - A Dube', 2, 'Y'
CREATE Procedure [dbo].[SP_Dynamic_Planning_Stope_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int,
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(max),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'
    
  If @TheLevel = 1
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  
    

Set @SQL1 = 'Select GroupSec,  Sum(SquareMetres) SquareMetres,  Sum(OnReefSQM) OnReefSQM,
  Sum(OffReefSQM) OffReefSQM,  Sum(TargetSQM) TargetSQM,  
  convert(int,round(Sum(FaceLength),0)) FaceLength,  
  convert(int,round(Sum(OnReefFL),0)) OnReefFL,  convert(int,round(Sum(OffReefFL),0)) OffReefFL,  
  FaceAdv = Case when Sum(FaceLength) = 0 then 0 else  convert(Numeric(10,1),Sum(SquareMetres)/Sum(FaceLength)) end,  
  OnreefAdv = Case when Sum(OnreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OnreefSQM)/Sum(OnreefFl)) end,  
  OffreefAdv = Case when Sum(OffreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OffreefSQM)/Sum(OffreefFl)) end,  
  CW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))  end,  
  CTons = convert(int,round(Sum(Ctons),0)),  SW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))  end,  
  ISW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))  end, 
  Tons = convert(int,round(Sum(tons),0)),  OnReefTons = convert(int,round(Sum(OnReeftons),0)),  
  OffReefTons = convert(int,round(Sum(OffReeftons),0)),  Volume = convert(int,round(Sum(Volume),0)),  
  OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),  OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),  
  [g/t] =  case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else  Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))  end,  
  convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,  
	Cmgt =
	Case when
	  (Sum(OnreefSQMDens) = 0) or (Sum(OnreefSQMDens) is null) then 0 else
	convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
	end,  
  PaySQM = Sum(PaySQM),  UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),  PayTons = Sum(PayTons),  UnPayTons = Sum(Tons)-Sum(PayTons),  
  convert(int,Round(avg(PayLimit),0)) ShaftPayLimit,   cubicmetres = Sum(cubicmetres),  CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),  
  CubicGrams = Convert(Numeric(7,3),Round(Sum(CubicGrams)/1000,3)),  
  CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
  
  SUM(Bus_Plan_SQM) Bus_Plan_SQM,
  convert(Numeric(10,3),round(Sum(Bus_Plan_Grams)/1000,3)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_Grams)*100)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end,
  Sum(UraniumKg) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*10000)/(Sum(UraniumReefSQMDens)))
	end 
  from 
  (select  
    SelectSection, 
    GroupSec,
    SUM(SquareMetres) SquareMetres,
    SUM(OffReefSQM) OffReefSQM,  
    SUM(TargetSQM) TargetSQM, 
    SUM(MetresAdvance) MetresAdvance,   
    SUM(OnReefAdv) OnReefAdv,   
    SUM(OffReefAdv) OffReefAdv,   
    SUM(FL) FaceLength,   
    SUM(FL) OnReefFL,   
    SUM(OffReefFL) OffReefFL,   
    SUM(CWSQM) CWSQM,  
    SUM(CTons) CTons,  
    SUM(SWSQM) SWSQM,  
    SUM(ISWSQM) ISWSQM,  
    SUM(Volume) Volume,  
    SUM(OnReefVolume) OnReefVolume,  
    SUM(OffReefVolume) OffReefVolume,  
    SUM(Tons) Tons,  
    SUM(ReefTons) ReefTons,  
    SUM(OnReefTons) OnReefTons,  
    SUM(OffReefTons) OffReefTons,  
    SUM(OnreefSQMDens) OnreefSQMDens,  
    SUM(Grams) Grams,  
    SUM(Kg) Kg,  
    SUM(PaySQM) PaySQM,  
    SUM(PayTons) PayTons,  
    SUM(OnreefSQM) OnreefSQM,  
    avg(RockDensity) RockDensity,
    avg(PayLimit) PayLimit,  
    SUM(cubicmetres) cubicmetres,  
    SUM(CubicTons) CubicTons,
    SUM(CubicGrams) CubicGrams,   
    SUM(CubicGT) CubicGT,
    SUM(UraniumKg) UraniumKg, 
    SUM(UraniumReefSQM) UraniumReefSQM,
    SUM(UraniumReefSQMDens) UraniumReefSQMDens,
    AVG(Bus_Plan_SQM) Bus_Plan_SQM,
    AVG(Bus_Plan_Grams) Bus_Plan_Grams
   FROM
  (Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection, 
    Convert(Numeric(7),a.SQM ) SquareMetres,
    Convert(Numeric(7),a.wastesqm) OffReefSQM,  
    0 TargetSQM, 
    a.MetresAdvance MetresAdvance,   
    0 OnReefAdv,   
    0 OffReefAdv,   
    a.FL,   
    a.FL OnReefFL,   
    0 OffReefFL,   
    CWSQM = a.SQM*a.CW,  
    CTons = a.SQM*a.CW*w.Density/100,  
    SWSQM = a.SQM*a.SW,  
    ISWSQM = a.SQM*sm.SWidth,  
    Volume = 0,  OnReefVolume = 0,  
    OffReefVolume = 0,  
    Tons = Convert(Numeric(7),Round(a.SQM*a.SW/100*w.density,0)),  
    ReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OnReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OffReefTons = Convert(Numeric(7),Round(a.WasteSQM*a.SW/100*w.density,0)), 
    OnreefSQMDens = a.ReefSQM*w.Density,  
    Grams = a.cmgt*a.ReefSQM*w.density/100,  
    Kg = convert(numeric(10,3),Round(a.cmgt*a.ReefSQM*w.Density/100,0)/100),  
    0 PaySQM,  
    0 PayTons, 
	UraniumKg = a.cmkgt*a.ReefSQM*w.density/10000, 
    UraniumReefSQM = Case when a.cmkgt > 0 then ReefSQM else 0 end,
    UraniumReefSQMDens = Case when a.cmkgt > 0 then ReefSQM*w.Density else 0 end, 
    Convert(Numeric(7),a.ReefSQM) OnreefSQM,  
    s.RockDensity,  0 PayLimit,  
    Convert(Numeric(7),a.cubicmetres) cubicmetres,  
    Convert(Numeric(7),Round(a.cubicmetres*w.density,0)) CubicTons,
    0 CubicGrams,   
    0 CubicGT,
    
    0 Bus_Plan_SQM,
    0 Bus_Plan_Grams
        
    from Planmonth a 
    inner join section_complete b 
    on  a.prodmonth = b.prodmonth and  
    a.sectionid = b.sectionid 
    inner join workplace c on  
    a.workplaceid = c.workplaceid 
    inner join seccal d on  
    b.prodmonth = d.prodmonth and  
    b.sectionid_1 = d.sectionid 
    --left outer join shaft_paylimits e on  
    --a.prodmonth = e.prodmonth and  c.OreFlowID  = e.OreFlowID 
    inner join  workplace w on a.workplaceid = w.workplaceid  
    left join sampling sm on -- a.Prodmonth = sm.prodmonth and  
    a.workplaceid = sm.workplaceid
    
    --left join
    --(select * from MONTH_CORP_PLAN
    -- where PRODMONTH =  '+@Prodmonth+' 
    -- and Activity = 0) BusPlan on
    -- b.Name_2 = BusPlan.Name_2

    , Sysset s  
    where a.prodmonth =  '+@Prodmonth+'   
    and a.activity in (0,3)   and b.'+@SectionLevel+' = '''+@Section+''' and a.PlanCode = ''MP'''
 

	if @ShowAuth <> 'Y'
	Begin
	Set @SQL1 = @SQL1+' 
	 and a.Locked = 1'
	END

	Set @SQL1 = @SQL1+' 

    ) DetailCorrection
    Group by GroupSec, SelectSection 
    
    ) LastSum
    group by GroupSec
	with rollup'

--Select @SQL1

Exec(@SQL1)



GO

-- [SP_Dynamic_Planning_Stope] '201701', '1.1 - B Ndlovu', 'Y'
CREATE Procedure [dbo].[SP_Dynamic_Planning_Stope]
--Declare 
@Prodmonth varchar(6),
@Section Varchar(40),
@ShowAuth VarChar(5) = 'Y'

AS
Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20),
		@BonusDB Varchar(20)

--SET @BonusDB = ( SELECT Bonus_Database FROM SYSSET)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @TheLevel = 1
    set @SectionLevel = 'Name_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'Name_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'Name_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'Name_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'Name_1'

   If @TheLevel = 6 
    set @SectionLevel = 'Name'

Set @SQL1 = 'Select 
* from 
(Select Name_2 Name_2, Name_1 Name_1, Name Name, Workplace,
Max(Description) Description,max(Workplaceid) Workplaceid, 
min(BeginDate) BeginDate, max(EndDate) EndDate, Max(a.isStopped) isStopped, 
convert(int, Avg(TotalShifts)) TotalShifts,
convert(int,round(Sum(SquareMetres),0)) SquareMetres,
convert(int,round(Sum(OnReefSQM),0))OnReefSQM,
convert(int,round(Sum(OffReefSQM),0) )OffReefSQM,
convert(int,round(Sum(TargetSQM),0)) TargetSQM,
convert(int,round(Sum(FL),0)) FaceLength,
convert(int,round(Sum(OnReefFL),0)) OnReefFL,
convert(int,round(Sum(OffReefFL),0)) OffReefFL,
FaceAdv =
  Case When (Sum(a.FL) = 0) or (Sum(a.FL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.SquareMetres)/Sum(a.FL))
end,
OnReefAdv =
  Case When (Sum(a.OnReefFL) = 0) or (Sum(a.OnReefFL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.SquareMetres)/Sum(a.OnReefFL))
end,
OffReefAdv =
  Case When (Sum(a.OffReefFL) = 0) or (Sum(a.OffReefFL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.OffReefSQM)/Sum(a.OffReefFL))
end,
CW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))
end,
CTons = convert(int,round(Sum(Ctons),0)),
SW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))
end,
ISW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))
end,
Tons = convert(int,round(Sum(tons),0)),
OnReefTons = convert(int,round(Sum(OnReefTons),0)),
OffReefTons = convert(int,round(Sum(OffReefTons),0)),
Volume = convert(int,round(Sum(Volume),0)),
OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),
OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),
[g/t] =
case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else
Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))
end,
convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,
Cmgt =
Case when
  (Sum(OnreefSQMDens) = 0) or (Sum(OnreefSQMDens) is null) then 0 else
convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
end,
PaySQM = Sum(PaySQM),
UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),
PayTons = Sum(PayTons),
UnPayTons = Sum(Tons)-Sum(PayTons),
convert(int,Round(avg(PayLimit),0)) ShaftPayLimit,
Max(OrgUnitday) OrgUnitday,
Max(OrgUnitAfternoon) OrgUnitAfternoon,
Max(OrgUnitNight) OrgUnitNight,
Max(OrgUnitRoving) OrgUnitRoving,
Min(Convert(Varchar(10),TargetID)+'':''+MineMet) MineMet,
Min(Remarks) Remarks,
Convert(numeric(6,2),Sum(LabourStrength)) CrewStrength,
Cubicmetres = Sum(a.Cubicmetres),
CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),
CubicGrams = Convert(Numeric(7,3),Round(Sum(CubicGrams)/1000,3)),
CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
convert(Numeric(10,3),Avg(Bus_Plan_SQM)) Bus_Plan_SQM,
convert(Numeric(10,3),Avg(Bus_Plan_KG)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_KG)*100000)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end ,
Sum(SQMDens) SQMDens,
Sum(SWSQM) SWSQM,
Sum(CWSQM) CWSQM,
Sum(ISWSQM) ISWSQM,
Sum(Grams) Grams,
Sum(UraniumKg) UraniumKG,
Cmkgt =
Case when
  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
convert(int,(Sum(UraniumKg)*10000)/(Sum(UraniumReefSQMDens)))
end
 from
(Select b.Name_2, Name_1, Name, c.Description+'':''+a.Workplaceid Workplace,c.Description ,a.Workplaceid , d.BeginDate, d.enddate, d.TotalShifts,
Convert(Numeric(7),a.sqm) SquareMetres,'''' isStopped,
ReefSQM
OnreefSQm,
WasteSQM OffreefSQm,
0 TargetSQM,
a.MetresAdvance MetresAdvance,
a.MetresAdvance OnreefAdv,
0 OffreefAdv,
OrgUnitday = Case when (a.OrgUnitday is null) then '''' else a.OrgUnitday end,
OrgUnitAfternoon = Case when (a.OrgUnitAfternoon is null) then '''' else a.OrgUnitAfternoon end,
OrgUnitNight = Case when (a.OrgUnitday is null) then '''' else a.OrgUnitNight end,
OrgUnitRoving = Case when (a.RomingCrew  is null) then '''' else a.RomingCrew  end,
TargetID = a.TargetID,'''' MineMet,
'''' remarks,
a.FL,
a.FL OnReefFL,
0 OffReefFL,
CTons = case when w.Density is null then 0 else 
a.SQM*a.CW*w.Density/100 end,
CWSQM = a.SQM*a.CW,
SWSQM = a.SQM*a.SW,
ISWSQM = a.SQM*sm.SWidth,
ReefTons = case when w.Density is null then 0 else
a.ReefSQM*a.SW*w.Density/100 end,
Convert(Numeric(7),Round(a.SQM*a.SW/100*w.density,0)) Tons, 
Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)) OnReefTons, 
Convert(Numeric(7),Round(a.WasteSQM*a.SW/100*w.density,0)) OffReefTons, 
0 Volume, 
0 OnReefVolume, 
0 OffReefVolume,
s.ROCKdensity,
Grams = a.cmgt*a.ReefSQM*w.density/100, 
Kg = case when w.Density is null then 0 else
convert(numeric(10,3),Round(a.cmgt*a.ReefSQM*w.Density/100,0)/100) end,
0 PaySQM,
0 PayTons,
UraniumKg = a.cmkgt*a.ReefSQM*w.density/10000, 
UraniumReefSQM = Case when a.cmkgt > 0 then ReefSQM else 0 end,
UraniumReefSQMDens = Case when a.cmkgt > 0 then ReefSQM*w.Density else 0 end,
SQMDens = case when w.density is null then 0 else
a.SQM*w.density end,
OnReefSQMDens = case when w.density is null then 0 else
a.ReefSQM*w.density end,
OffreefSQMDens = case when w.density is null then 0 else
a.WASTESQM end,
0 PayLimit,
0 LabourStrength,
a.Cubicmetres,
Convert(Numeric(7),Round(a.Cubicmetres*w.density,0)) CubicTons, 
0 CubicGrams, 
0 CubicGT,
0 Bus_Plan_SQM,
0 Bus_Plan_KG
 from PLANMONTH a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid left outer join workplace w on
a.workplaceid = w.workplaceid
inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on
--a.prodmonth = e.prodmonth and
--c.oreflowid = e.oreflowid
--left join vw_CrewInfo x on
--a.prodmonth = x.prodmonth and
--a.sectionid = x.sectionid and
--a.workplaceid = x.workplaceid and 
--a.activity = x.activity and
--a.iscubics = x.Iscubics
left join sampling sm on 
--a.prodmonth = sm.prodmonth and 
a.workplaceid = sm.workplaceid 
--left outer join Bonus_PoolDefaults bpd on 
-- a.TargetID = bpd.TargetID and
--bpd.Activity in (0,3)    
    --left join
    --(select * from PLANMONTH_OPS 
    -- where PRODMONTH ='+@Prodmonth+'
    -- and Activity = 0) BusPlan on
    -- b.SECTIONID_2 = BusPlan.Sectionid_2
	 , SYSSET s
where a.prodmonth ='+@Prodmonth+'
and a.activity in (0,3)
and b.'+@SectionLevel+' ='''+@Section+''' and a.plancode=''MP'''
 

if @ShowAuth <> 'Y'
Begin
Set @SQL1 = @SQL1+' 
 and a.Locked = 1'
END

Set @SQL1 = @SQL1+' ) a
group by
Name_2,Name_1, Name,a.isStopped, Workplace
with rollup) a
where 
(case when Workplace is not Null then 0
	  when (Name is not Null) and (Workplace is null) then 1
	  when (Name_1 is Not null) and (Name is Null) and (Workplace is null) then 2 
	  when (Name_2 is Not null) and (Name_1 is null) and (Name is Null) and (Workplace is null) then 3 end) in (0,3)'

Exec(@SQL1)

--PRINT(@SQL1)


GO

-- [SP_Lock_Planning_Stope_Summary] '201701', '1.1 - A Dube', 2, 'Y'
CREATE Procedure [dbo].[SP_Lock_Planning_Stope_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int,
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(8000),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'
    
  If @TheLevel = 1 
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  
    

Set @SQL1 = 'Select GroupSec,  Sum(SquareMetres) SquareMetres,  Sum(OnReefSQM) OnReefSQM,
  Sum(OffReefSQM) OffReefSQM,  Sum(TargetSQM) TargetSQM,  
  convert(int,round(Sum(FaceLength),0)) FaceLength,  
  convert(int,round(Sum(OnReefFL),0)) OnReefFL,  convert(int,round(Sum(OffReefFL),0)) OffReefFL,  
  FaceAdv = Case when Sum(FaceLength) = 0 then 0 else  convert(Numeric(10,1),Sum(SquareMetres)/Sum(FaceLength)) end,  
  OnreefAdv = Case when Sum(OnreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OnreefSQM)/Sum(OnreefFl)) end,  
  OffreefAdv = Case when Sum(OffreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OffreefSQM)/Sum(OffreefFl)) end,  
  CW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))  end,  
  CTons = convert(int,round(Sum(Ctons),0)),  SW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))  end,  
  ISW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))  end, 
  Tons = convert(int,round(Sum(tons),0)),  OnReefTons = convert(int,round(Sum(OnReeftons),0)),  
  OffReefTons = convert(int,round(Sum(OffReeftons),0)),  Volume = convert(int,round(Sum(Volume),0)),  
  OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),  OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),  
  [g/t] =  case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else  Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))  end,  
  convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,  
	Cmgt =
	Case when
	  (Sum(OnreefSQM) = 0) or (Sum(OnreefSQM) is null) then 0 else
	convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
	end,  
  PaySQM = Sum(PaySQM),  UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),  PayTons = Sum(PayTons),  UnPayTons = Sum(Tons)-Sum(PayTons),  
  convert(int,Round(avg(PayLimit),0)) ShaftPayLimit,   cubicmetres = Sum(cubicmetres),  CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),  
  CubicGrams = Convert(Numeric(7,3),Round(Sum(CubicGrams)/1000,3)),  
  CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
  
  SUM(Bus_Plan_SQM) Bus_Plan_SQM,
  convert(Numeric(10,3),round(Sum(Bus_Plan_Grams)/1000,3)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_Grams)*100)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end ,
  Sum(UraniumKg) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*10000)/(Sum(UraniumReefSQMDens)))
	end 
  from 
  (select  
    SelectSection, 
    GroupSec,
    SUM(SquareMetres) SquareMetres,
    SUM(OffReefSQM) OffReefSQM,  
    SUM(TargetSQM) TargetSQM, 
    SUM(MetresAdvance) MetresAdvance,   
    SUM(OnReefAdv) OnReefAdv,   
    SUM(OffReefAdv) OffReefAdv,   
    SUM(FL) FaceLength,   
    SUM(FL) OnReefFL,   
    SUM(OffReefFL) OffReefFL,   
    SUM(CWSQM) CWSQM,  
    SUM(CTons) CTons,  
    SUM(SWSQM) SWSQM,  
    SUM(ISWSQM) ISWSQM,  
    SUM(Volume) Volume,  
    SUM(OnReefVolume) OnReefVolume,  
    SUM(OffReefVolume) OffReefVolume,  
    SUM(Tons) Tons,  
    SUM(ReefTons) ReefTons,  
    SUM(OnReefTons) OnReefTons,  
    SUM(OffReefTons) OffReefTons,  
    SUM(OnreefSQMDens) OnreefSQMDens,  
    SUM(Grams) Grams,  
    SUM(Kg) Kg,  
    SUM(PaySQM) PaySQM,  
    SUM(PayTons) PayTons,  
    SUM(OnreefSQM) OnreefSQM,  
    avg(RockDensity) RockDensity,
    avg(PayLimit) PayLimit,  
    SUM(cubicmetres) cubicmetres,  
    SUM(CubicTons) CubicTons,
    SUM(CubicGrams) CubicGrams,   
    SUM(CubicGT) CubicGT,
    SUM(UraniumKg) UraniumKg, 
    SUM(UraniumReefSQM) UraniumReefSQM,
    SUM(UraniumReefSQMDens) UraniumReefSQMDens,
    AVG(Bus_Plan_SQM) Bus_Plan_SQM,
    AVG(Bus_Plan_Grams) Bus_Plan_Grams
   FROM
  (Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection, 
    Convert(Numeric(7),a.SQM ) SquareMetres,
    Convert(Numeric(7),a.wastesqm) OffReefSQM,  
    0 TargetSQM, 
    a.MetresAdvance MetresAdvance,   
    0 OnReefAdv,   
    0 OffReefAdv,   
    a.FL,   
    a.FL OnReefFL,   
    0 OffReefFL,   
    CWSQM = a.SQM*a.CW,  
    CTons = a.SQM*a.CW*w.Density/100,  
    SWSQM = a.SQM*a.SW,  
    ISWSQM = a.SQM*sm.SWidth,  
    Volume = 0,  OnReefVolume = 0,  
    OffReefVolume = 0,  
    Tons = Convert(Numeric(7),Round(a.SQM*a.SW/100*w.density,0)),  
    ReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OnReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OffReefTons = Convert(Numeric(7),Round(a.WasteSQM*a.SW/100*w.density,0)), 
    OnreefSQMDens = a.ReefSQM*w.Density,  
    Grams = a.cmgt*a.ReefSQM*w.density/100,  
    Kg = convert(numeric(10,3),Round(a.cmgt*a.ReefSQM*w.Density/100,0)/100),  
    0 PaySQM,  
    0 PayTons,  
    Convert(Numeric(7),a.ReefSQM) OnreefSQM,  
    s.RockDensity,  0 PayLimit,  
    Convert(Numeric(7),a.cubicmetres) cubicmetres,  
    Convert(Numeric(7),Round(a.cubicmetres*w.density,0)) CubicTons,
    0 CubicGrams,   
    0 CubicGT,
    UraniumKg = a.cmkgt*a.ReefSQM*w.density/100000, 
    UraniumReefSQM = Case when a.cmkgt > 0 then ReefSQM else 0 end,
    UraniumReefSQMDens = Case when a.cmkgt > 0 then ReefSQM*w.Density else 0 end, 
    0 Bus_Plan_SQM,
    0 Bus_Plan_Grams
        
    from Planmonth a 
    inner join section_complete b 
    on  a.prodmonth = b.prodmonth and  
    a.sectionid = b.sectionid 
    inner join workplace c on  
    a.workplaceid = c.workplaceid 
    inner join seccal d on  
    b.prodmonth = d.prodmonth and  
    b.sectionid_1 = d.sectionid 
    --left outer join shaft_paylimits e on  
    --a.prodmonth = e.prodmonth and  c.OreFlowID  = e.OreFlowID 
    inner join  workplace w on a.workplaceid = w.workplaceid  
    left join sampling sm on -- a.Prodmonth = sm.prodmonth and  
    a.workplaceid = sm.workplaceid
    
    --left join
    --(select * from MONTH_CORP_PLAN
    -- where PRODMONTH =  '+@Prodmonth+' 
    -- and Activity = 0) BusPlan on
    -- b.Name_2 = BusPlan.Name_2

    , Sysset s  
    where a.prodmonth =  '+@Prodmonth+'   
    and a.activity in (0,3)   and b.'+@SectionLevel+' = '''+@Section+''' and a.PlanCode = ''LP''  and a.Locked = 1
 
) DetailCorrection
    Group by GroupSec, SelectSection 
    
    ) LastSum
    group by GroupSec
	with rollup'

--Select @SQL1

Exec(@SQL1)




GO

-- [SP_Lock_Planning_Stope] '201701', '1.1 - B Ndlovu', 'Y'
CREATE Procedure [dbo].[SP_Lock_Planning_Stope]
--Declare 
@Prodmonth varchar(6),
@Section Varchar(40),
@ShowAuth VarChar(5) = 'Y'

AS
Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20),
		@BonusDB Varchar(20)

--SET @BonusDB = ( SELECT Bonus_Database FROM SYSSET)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @TheLevel = 1
    set @SectionLevel = 'Name_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'Name_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'Name_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'Name_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'Name_1'

   If @TheLevel = 6 
    set @SectionLevel = 'Name'

Set @SQL1 = 'Select 
* from 
(Select Name_2 Name_2, Name_1 Name_1, Name Name, Workplace,Max(Description) Description,max(Workplaceid) Workplaceid, min(BeginDate) BeginDate, max(EndDate) EndDate, Max(a.isStopped) isStopped, 
convert(int, Avg(TotalShifts)) TotalShifts,
convert(int,round(Sum(SquareMetres),0)) SquareMetres,
convert(int,round(Sum(OnReefSQM),0))OnReefSQM,
convert(int,round(Sum(OffReefSQM),0) )OffReefSQM,
convert(int,round(Sum(TargetSQM),0)) TargetSQM,
convert(int,round(Sum(FL),0)) FaceLength,
convert(int,round(Sum(OnReefFL),0)) OnReefFL,
convert(int,round(Sum(OffReefFL),0)) OffReefFL,
FaceAdv =
  Case When (Sum(a.FL) = 0) or (Sum(a.FL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.SquareMetres)/Sum(a.FL))
end,
OnReefAdv =
  Case When (Sum(a.OnReefFL) = 0) or (Sum(a.OnReefFL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.SquareMetres)/Sum(a.OnReefFL))
end,
OffReefAdv =
  Case When (Sum(a.OffReefFL) = 0) or (Sum(a.OffReefFL) is null) then 0 else
  Convert(numeric(4,1),Sum(a.OffReefSQM)/Sum(a.OffReefFL))
end,
CW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))
end,
CTons = convert(int,round(Sum(Ctons),0)),
SW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))
end,
ISW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))
end,
Tons = convert(int,round(Sum(tons),0)),
OnReefTons = convert(int,round(Sum(OnReefTons),0)),
OffReefTons = convert(int,round(Sum(OffReefTons),0)),
Volume = convert(int,round(Sum(Volume),0)),
OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),
OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),
[g/t] =
case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else
Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))
end,
convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,
Cmgt =
Case when
  (Sum(OnreefSQMDens) = 0) or (Sum(OnreefSQMDens) is null) then 0 else
convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
end,
PaySQM = Sum(PaySQM),
UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),
PayTons = Sum(PayTons),
UnPayTons = Sum(Tons)-Sum(PayTons),
convert(int,Round(avg(PayLimit),0)) ShaftPayLimit,
Max(OrgUnitday) OrgUnitday,
Max(OrgUnitAfternoon) OrgUnitAfternoon,
Max(OrgUnitNight) OrgUnitNight,
Max(OrgUnitRoving) OrgUnitRoving,
Min(Convert(Varchar(10),TargetID)+'':''+MineMet) MineMet,
Min(Remarks) Remarks,
Convert(numeric(6,2),Sum(LabourStrength)) CrewStrength,
Cubicmetres = Sum(a.Cubicmetres),
CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),
CubicGrams = Convert(Numeric(7,3),Round(Sum(CubicGrams)/1000,3)),
CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
convert(Numeric(10,3),Avg(Bus_Plan_SQM)) Bus_Plan_SQM,
convert(Numeric(10,3),Avg(Bus_Plan_KG)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_KG)*100000)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end ,
Sum(UraniumKg) UraniumKG,
Cmkgt =
Case when
  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
convert(int,(Sum(UraniumKg)*10000)/(Sum(UraniumReefSQMDens)))
end
 from
(Select b.Name_2, Name_1, Name, c.Description+'':''+a.Workplaceid Workplace,c.Description ,a.Workplaceid , d.BeginDate, d.enddate, d.TotalShifts,
Convert(Numeric(7),a.sqm) SquareMetres,'''' isStopped,
ReefSQM
OnreefSQm,
WasteSQM OffreefSQm,
0 TargetSQM,
a.MetresAdvance MetresAdvance,
a.MetresAdvance OnreefAdv,
0 OffreefAdv,
OrgUnitday = Case when (a.OrgUnitday is null) then '''' else a.OrgUnitday end,
OrgUnitAfternoon = Case when (a.OrgUnitAfternoon is null) then '''' else a.OrgUnitAfternoon end,
OrgUnitNight = Case when (a.OrgUnitday is null) then '''' else a.OrgUnitNight end,
OrgUnitRoving = Case when (a.RomingCrew  is null) then '''' else a.RomingCrew  end,
TargetID = a.TargetID, '''' MineMet,
'''' remarks,
a.FL,
a.FL OnReefFL,
0 OffReefFL,
CTons = case when w.Density is null then 0 else 
a.SQM*a.CW*w.Density/100 end,
CWSQM = a.SQM*a.CW,
SWSQM = a.SQM*a.SW,
ISWSQM = a.SQM*sm.SWidth,
ReefTons = case when w.Density is null then 0 else
a.ReefSQM*a.SW*w.Density/100 end,
Convert(Numeric(7),Round(a.SQM*a.SW/100*w.density,0)) Tons, 
Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)) OnReefTons, 
Convert(Numeric(7),Round(a.WasteSQM*a.SW/100*w.density,0)) OffReefTons, 
0 Volume, 
0 OnReefVolume, 
0 OffReefVolume,
s.ROCKdensity,
Grams = a.cmgt*a.ReefSQM*w.density/100, 
Kg = case when w.Density is null then 0 else
convert(numeric(10,3),Round(a.cmgt*a.ReefSQM*w.Density/100,0)/100) end,
0 PaySQM,
0 PayTons,
UraniumKg = a.cmkgt*a.ReefSQM*w.density/100000, 
UraniumReefSQM = Case when a.cmkgt > 0 then ReefSQM else 0 end,
UraniumReefSQMDens = Case when a.cmkgt > 0 then ReefSQM*w.Density else 0 end,
SQMDens = case when w.density is null then 0 else
a.SQM*w.density end,
OnReefSQMDens = case when w.density is null then 0 else
a.ReefSQM*w.density end,
OffreefSQMDens = case when w.density is null then 0 else
a.WASTESQM end,
0 PayLimit,
0 LabourStrength,
a.Cubicmetres,
Convert(Numeric(7),Round(a.Cubicmetres*w.density,0)) CubicTons, 
0 CubicGrams, 
0 CubicGT,
0 Bus_Plan_SQM,
0 Bus_Plan_KG
 from PLANMONTH a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid left outer join workplace w on
a.workplaceid = w.workplaceid
inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on
--a.prodmonth = e.prodmonth and
--c.oreflowid = e.oreflowid
--left join vw_CrewInfo x on
--a.prodmonth = x.prodmonth and
--a.sectionid = x.sectionid and
--a.workplaceid = x.workplaceid and 
--a.activity = x.activity and
--a.iscubics = x.Iscubics
left join sampling sm on 
--a.prodmonth = sm.prodmonth and 
a.workplaceid = sm.workplaceid    
    --left join
    --(select * from PLANMONTH_OPS 
    -- where PRODMONTH ='+@Prodmonth+'
    -- and Activity = 0) BusPlan on
    -- b.SECTIONID_2 = BusPlan.Sectionid_2
	 , SYSSET s
where a.prodmonth ='+@Prodmonth+'
and a.activity in (0,3)
and b.'+@SectionLevel+' ='''+@Section+''' and a.plancode=''LP''  and a.Locked = 1
 
 ) a
group by
Name_2,Name_1, Name,a.isStopped, Workplace
with rollup) a
where 
(case when Workplace is not Null then 0
	  when (Name is not Null) and (Workplace is null) then 1
	  when (Name_1 is Not null) and (Name is Null) and (Workplace is null) then 2 
	  when (Name_2 is Not null) and (Name_1 is null) and (Name is Null) and (Workplace is null) then 3 end) in (0,3)'

Exec(@SQL1)



GO

-- [SP_Planning_Dev_Summary] '201701', '1.5 - T Cwati', 2

CREATE Procedure [dbo].[SP_Planning_Dev_Summary]
--Declare
  @Prodmonth varchar(6),
  @Section VarChar(50),
  @SumLevel int

AS
Declare @TheLevel Int,
        @SQL1 Varchar(max),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'
    
  If @TheLevel = 1
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  
    

Set @SQL1 = 'Select GroupSec,
Max(Prim) Prim,
Max(OrgUnitday) OrgUnitday,
Max(OrgUnitAfterNoon) OrgUnitAfterNoon,
Max(OrgUnitNight) OrgUnitNight,
'''' remarks,
Sum(metresAdvance+Converted_M) TotalAdvance,
Convert(Numeric(9,1),sum(MetresAdvance)) MetresAdvance,
Convert(Numeric(9,1),sum(OnReefAdv)) OnReefAdv,
Convert(Numeric(9,1),sum(OffReefAdv)) OffReefAdv,
Convert(Numeric(9,1),sum(SecTot_M)) SecTot_M,
Convert(Numeric(9,1),sum(SecOn_M)) SecOn_M,
Convert(Numeric(9,1),sum(SecOff_M)) SecOff_M,
Convert(Numeric(9,1),sum(MainTot_M)) MainTot_M,
Convert(Numeric(9,1),sum(MainOn_M)) MainOn_M,
Convert(Numeric(9,1),sum(MainOff_M)) MainOff_M,
Sum(Capital_M) Capital_M,
Sum(CapitalOn_M) CapitalOn_M,
Sum(CapitalOff_M) CapitalOff_M,
Sum(Converted_M) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
'''' DrillRig,
Convert(Numeric(9),sum(Tons)) Tons,
0 OnReefTons,
0 OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = sum(Grams),
0 cmgt,
kg = convert(Numeric(10,1),SUM(Grams)/1000),
Convert(Numeric(9,1),sum(OnReefSQM)) OnReefSQM,
Convert(Numeric(9,1),avg(Height)) Height,
0 gt,
avg(ShaftPayLimit) ShaftPayLimit,
Sum(CMGTGramsCalc) CMGTGramsCalc,
avg(convert(numeric(6,2), 0)) labourstrength,
Convert(Numeric(9),sum(Cubicmetres)) Cubicmetres,
Convert(Numeric(9),sum(CubicTons)) CubicTons,
Convert(Numeric(9),sum(CubicGrams)) CubicGrams,
sum(0) CubicGT,
sum(Bus_Plan_DevCubics/10)+sum(Bus_Plan_Adv)+sum(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
sum(Bus_Plan_Adv) Bus_Plan_Adv,
sum(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
sum(Bus_Plan_Adv)-sum(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
sum(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
Isnull(sum(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
from
(Select GroupSec, SelectSection,
Max(Prim) Prim,
Max(OrgUnitday) OrgUnitday,
Max(OrgUnitAfterNoon) OrgUnitAfterNoon,
Max(OrgUnitNight) OrgUnitNight,
'''' remarks,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
sum(MetresAdvance) MetresAdvance,
sum(OnReefAdv) OnReefAdv,
sum(OffReefAdv) OffReefAdv,
sum(SecTot_M) SecTot_M,
sum(SecOn_M) SecOn_M,
sum(SecOff_M) SecOff_M,
sum(MainTot_M) MainTot_M,
sum(MainOn_M) MainOn_M,
sum(MainOff_M) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
convert(Numeric(10,1), round(Sum(ConvertedOn_M),1)) ConvertedOn_M,
convert(Numeric(10,1), round(Sum(ConvertedOff_M),1)) ConvertedOff_M,
'''' DrillRig,
sum(Tons) Tons,
sum(OnReefTons) OnReefTons,
sum(OffReefTons) OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = sum(Grams),
sum(OnReefSQM) OnReefSQM,
avg(Height) Height,
avg(ShaftPayLimit) ShaftPayLimit,
Sum(CMGTGramsCalc) CMGTGramsCalc,
avg(convert(numeric(6,2), 0)) labourstrength, 
sum(Cubicmetres) Cubicmetres,
sum(CubicTons) CubicTons,
sum(CubicGrams) CubicGrams,
sum(0) CubicGT,
Avg(DevCubics/10)+Avg(Bus_Plan_Adv) Bus_Plan_TotalAdv,
Avg(Bus_Plan_Adv) Bus_Plan_Adv,
Avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
Avg(Bus_Plan_Adv)-Avg(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
Isnull(Avg(DevCubics),0) Bus_Plan_DevCubics,
Isnull(Avg(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
from
(select
B.'+@GroupLevel+' GroupSec, Name_2 SelectSection,
'''' Prim,
OrgUnitday = Case when ((a.OrgUnitday) is null) then '''' else (a.OrgUnitday) end,
OrgUnitAfterNoon = Case when ((a.OrgUnitAfterNoon) is null) then '''' else (a.OrgUnitAfterNoon) end,
OrgUnitNight = Case when ((a.OrgUnitNight) is null) then '''' else (a.OrgUnitNight) end,
'''' remarks,
(a.ReefAdv+a.WasteAdv) MetresAdvance,
(a.ReefAdv) OnReefAdv,
(a.WasteAdv ) OffReefAdv,
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  (a.CUBICMETRES/10),
ConvertedOn_M = case when c.ReefWaste in (0,3) then (a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.ReefWaste in (1,2) then (a.CUBICMETRES/10) else 0 end,
'''' DrillRig,
(a.Tons) Tons,
case when c.ReefWaste in (0,3) then (a.ReefTons) else 0 end OnReefTons,
case when c.ReefWaste in (1,2) then (a.WasteTons) else 0 end OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = (GT),
(a.ReefAdv*a.FL) OnReefSQM,
(a.SW) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when (c.Density) is null or (c.Density) = 0 then
(cmgt*(a.ReefAdv*a.FL)*s.RockDensity/100)
else
(cmgt*(a.ReefAdv*a.FL)*c.Density/100) end,
(convert(numeric(6,2), 0)) labourstrength, 
(a.Cubicmetres) Cubicmetres,
(Convert(Numeric(7),a.Cubicmetres*c.density)) CubicTons,
(Convert(Numeric(7),a.Cubicmetres*c.density)*0) CubicGrams,
(0) CubicGT,
0 Bus_Plan_Adv,
0  Bus_Plan_OnreefAdv,
0  DevCubics,
0 Bus_Plan_Cap_Adv

 from PLANMONTH a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.OreFlowID  = e.OreFlowID 

    --left join
    --(select * from planmonth_ops
    -- where PRODMONTH = '+@Prodmonth+'
    -- and Activity = 1) BusPlan on
    -- b.SECTIONID_2 = BusPlan.Sectionid_2,
     
 ,Sysset s
where a.prodmonth ='+@Prodmonth+'
and a.activity in (1,7)
and '+@SectionLevel+' = '''+@Section+''') correctedData
group by GroupSec, SelectSection) LastSum
group by GroupSec'


exec(@SQL1)

GO

-- [SP_Planning_Dev] '201701', '1.5 - T Cwati'
CREATE Procedure [dbo].[SP_Planning_Dev]
@Prodmonth varchar(6),
@Section Varchar(30)

AS
Select Name_2, Name_1,a.isStopped, Name+':'+Prim Name,Name Miner, Prim, Workplace, a.Workplaceid, 
Description, min(BeginDate) BeginDate, max(EndDate) EndDate,
convert(int, Avg(TotalShifts)) TotalShifts,
convert(int, Avg(ShaftPayLimit)) ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(metresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
Min(DrillRig) DrillRig,
convert(Numeric(10), round(Sum(Tons),1)) Tons,
0 OnReefTons,
0 OffReefTons,
convert(Numeric(10), round(Sum(Volume),1)) Volume,
convert(Numeric(10), round(Sum(OnReefVolume),1)) OnReefVolume,
convert(Numeric(10), round(Sum(OffReefVolume),1)) OffReefVolume,
0 gt,
0 cmgt,
Kg = convert(numeric(10,3),round(sum(grams)/1000,3)),
Min(OrgUnitday) OrgUnitday,
Min(OrgUnitAfterNoon) OrgUnitAfterNoon,
Min(OrgUnitNight) OrgUnitNight,
'' Remarks,
sum(Labourstrength) CrewStrength,
CubicMetres = Sum(CubicMetres), 
CubicTons = Sum(CubicTons), 
CubicGrams = Sum(CubicGrams), 
CubicGT = Sum(CubicGT),
0 Bus_Plan_TotalAdv,
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 Bus_Plan_OffreefAdv,
0 Bus_Plan_DevCubics,
0 Bus_Plan_Cap_Adv
 from
(Select Name_2,'' isStopped, Name_1, Name, c.Description+':'+a.Workplaceid Workplace,a.Workplaceid ,c.Description, 
d.BeginDate, d.enddate, d.TotalShifts,
'' Prim,
OrgUnitday = Case when (max(a.OrgUnitday) is null) then '' else max(a.OrgUnitday) end,
OrgUnitAfterNoon = Case when (max(a.OrgUnitAfterNoon) is null) then '' else max(a.OrgUnitAfterNoon) end,
OrgUnitNight = Case when (max(a.OrgUnitNight) is null) then '' else max(a.OrgUnitNight) end,
'' remarks,
sum(a.ReefAdv+a.WasteAdv) MetresAdvance,
sum(a.ReefAdv) OnReefAdv,
sum(a.WasteAdv) OffReefAdv,
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  sum(a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then sum(a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then sum(a.CUBICMETRES/10) else 0 end,
'' DrillRig,
sum(a.TONS) Tons,
case when c.REEFWASTE in (0,3) then sum(a.ReefTons) else 0 end OnReefTons,
case when c.REEFWASTE in (1,2) then sum(a.WasteTons) else 0 end OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = sum(gt),
sum(a.ReefAdv*a.FL) OnReefSQM,
avg(a.SW) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when avg(c.Density) is null or avg(c.Density) = 0 then
sum(cmgt*(a.ReefAdv*a.FL)*s.RockDensity/100)
else
sum(cmgt*(a.ReefAdv*a.FL)*c.Density/100) end, 
0 labourstrength, 
sum(a.Cubicmetres) Cubicmetres,
sum(Convert(Numeric(7),a.Cubicmetres*c.density)) CubicTons,
sum(Convert(Numeric(7),a.Cubicmetres*c.density)*0) CubicGrams,
0 CubicGT
 from Planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid
--left join vw_CrewInfo x on
--a.prodmonth = x.prodmonth and
--a.sectionid = x.sectionid and
--a.workplaceid = x.workplaceid and 
--a.activity = x.activity and
--a.iscubics = x.Iscubics
--left outer join WORKPLACE w on
--a.workplaceid = w.workplaceid
, Sysset s
where a.prodmonth = @Prodmonth
and a.activity in (1,7)
and Name_2 = @Section
group by Name_2, Name_1, Name, c.Description+':'+a.Workplaceid ,a.Workplaceid , c.Description, 
d.BeginDate, d.enddate, d.TotalShifts,
--c.GG025_TMS,
c.REEFWASTE
--, c.ACCOUNTCODE 
) a, sysset s 
group by 
Name_2,Name_1,a.isStopped, Name+':'+Prim,Name, Prim, Workplace, Workplaceid ,Description
GO

-- [SP_Dynamic_Planning_Dev_Summary] '201701', '1.5 - T Cwati', 4, 'Y'
CREATE Procedure [dbo].[SP_Dynamic_Planning_Dev_Summary]
--Declare
  @Prodmonth varchar(6),
  @Section VarChar(50),
  @SumLevel int,
  @ShowAuth VarChar(5) = 'Y'

AS
Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
		@SQL2 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'

  If @TheLevel = 1
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  
    

Set @SQL1 = 'Select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
'''' remarks,
Sum(metresAdvance+Converted_M) TotalAdvance,
sum(MetresAdvance) MetresAdvance,
sum(OnReefAdv) OnReefAdv,
sum(OffReefAdv) OffReefAdv,
sum(SecTot_M) SecTot_M,
sum(SecOn_M) SecOn_M,
sum(SecOff_M) SecOff_M,
sum(MainTot_M) MainTot_M,
sum(MainOn_M) MainOn_M,
sum(MainOff_M) MainOff_M,
Sum(Capital_M) Capital_M,
Sum(CapitalOn_M) CapitalOn_M,
Sum(CapitalOff_M) CapitalOff_M,
Sum(Converted_M) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
max(DrillRig) DrillRig,
sum(Tons) Tons,
sum(OnReefTons) OnReefTons,
sum(OffReefTons) OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = sum(Grams),
convert(numeric(10,3), sum(Grams)/1000) KG, 
sum(OnReefAdv*Facelength) OnReefSQM,
avg(stopewidth) Height,
0 ShaftPayLimit,
convert(Numeric(10,3),round(Sum(CMGTGramsCalc),1)) Grams,
CMGTGramsCalc = case when avg(Density) is null or avg(Density) = 0 then
sum(cmgt*(MetresAdvance*Facelength)*RockDensity/100)
else
sum(cmgt*(MetresAdvance*Facelength)*Density/100) end,
gt = case when Sum(OnReefTons) is null or Sum(OnReefTons) = 0 then
0
else
sum(Grams)/Sum(OnReefTons) end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
avg(convert(numeric(6,2), labourstrength)) labourstrength,
sum(Cubicmetres) Cubicmetres,
sum(CubicTons) CubicTons,
sum(CubicGrams) CubicGrams,
sum(CubicGT) CubicGT,
sum(Bus_Plan_DevCubics/10)+sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec)+sum(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv,
sum(Bus_Plan_OnreefAdv)+sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv,
sum(Bus_Plan_OffreefAdv)+sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv,
sum(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
sum(Bus_Plan_Cap_Adv) Bus_Plan_Cap_Adv,
sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
sum(Bus_Plan_Adv) Bus_Plan_Adv_Main,
sum(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv_Main,
sum(Bus_Plan_OffreefAdv) Bus_Plan_OffreefAdv_Main,
Sum(isnull(UraniumKg,0)) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*100)/(Sum(UraniumReefSQMDens)))
	end 
from
(select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
0 ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(MetresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
convert(Numeric(10,1), round(Sum(ConvertedOn_M),1)) ConvertedOn_M,
convert(Numeric(10,1), round(Sum(ConvertedOff_M),1)) ConvertedOff_M,
min(DrillRig) DrillRig,
Sum(Tons) Tons,
Sum(OnReefTons) OnReefTons,
Sum(OffReefTons) OffReefTons,
--Sum(Volume) Volume,
--Sum(OnReefVolume) OnReefVolume,
--Sum(OffReefVolume) OffReefVolume,
Grams = sum(Grams),
OnReefSQM = Sum(OnReefSQM),
CMGTGramsCalc = Sum(CMGTGramsCalc), 
Avg(FL) faceLength,
Avg(density) density,
Avg(ROCKDENSITY) ROCKDENSITY,
Avg(goldgramsperton) goldgramsperton,
Avg(stopewidth) stopewidth, 
0 PayLimit, 
Avg(labourstrength) labourstrength,
gt = Case when (Sum(OnReefTons) = 0) or (Sum(OnReefTons) is Null) then 0 else
  convert(Numeric(10,1), round(Sum(Grams)/Sum(OnReefTons),1))
end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
Kg = convert(numeric(10,3),round(sum(CMGTGramsCalc)/1000,3)),
CubicMetres = sum(CubicMetres), 
CubicTons = sum(CubicTons), 
CubicGrams = sum(CubicGrams), 
UraniumKg = SUM(UraniumKg), 
UraniumReefSQM = SUM(UraniumReefSQM),
UraniumReefSQMDens = SUM(UraniumReefSQMDens),
CubicGT = sum(CubicGT),
Avg(Bus_Plan_Adv) Bus_Plan_Adv,
Avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
Avg(Bus_Plan_Adv)-Avg(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
Avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
Avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
Avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
Isnull(Avg(DevCubics),0) Bus_Plan_DevCubics,
Isnull(Avg(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
 from
(Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection,  
'''' Prim,
a.OrgUnitday ,
a.OrgUnitAfterNoon ,
a.OrgUnitNight ,
'''' remarks,
a.MetresAdvance MetresAdvance,
a.ReefAdv OnReefAdv,
a.WasteAdv OffReefAdv,
a.FL,
w.density,
s.ROCKDENSITY,
gt goldgramsperton,
a.SW stopewidth, 
0 PayLimit, 
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  (a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then (a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then (a.CUBICMETRES/10) else 0 end,
(DrillRig) DrillRig,
(a.Tons) Tons,
(a.ReefTons) OnReefTons,
(a.WasteTons) OffReefTons,
--(a.Volume) Volume,
--(a.OnReefVolume) OnReefVolume,
--(a.OffReefVolume) OffReefVolume,
Grams = (a.gt*a.ReefTons),
(a.ReefAdv*a.fl) OnReefSQM,
(a.sw) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when (w.Density) is null or (w.Density) = 0 then
(a.cmgt*(a.MetresAdvance*c.endheight)*s.RockDensity/100)
else
(a.cmgt*(a.MetresAdvance*c.endheight)*w.Density/100) end,
(convert(numeric(6,2), labourstrength)) labourstrength, 
(a.Cubicmetres) Cubicmetres,
(a.CubicsTons) CubicTons,
(a.CubicGrams) CubicGrams,
(a.CubicGT) CubicGT,
	UraniumKg = (a.cmkgt*(a.ReefAdv*a.fl)*w.density/100), 
    UraniumReefSQM = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl) else 0 end),
    UraniumReefSQMDens = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl)*w.Density else 0 end), 
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 DevCubics,
0 Bus_Plan_Cap_Adv ,
0 Bus_Plan_Adv_Sec,
0 Bus_Plan_OnreefAdv_Sec,
0 Bus_Plan_OffreefAdv_Sec '


Set @SQL2 = ' from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid 
left outer join workplace w on 
a.workplaceid = w.workplaceid 
  --  left join
  --  (select 
		--Prodmonth,
		--Sectionid_2,
		--Sum(SQM) SQM,
		--Sum(Kg) Kg,
		--Sum(Cubics) Cubics,
		--Sum(Metresadvance) Metresadvance,
		--Sum(MetresReef) MetresReef,
		--Sum(MetresWaste) MetresWaste,
		--Sum(DevCap) DevCap,
		--Sum(DevSec) DevSec,
		--Sum(DevSecReef) DevSecReef
		-- from PLANMONTH_OPS
  --   where PRODMONTH ='+@Prodmonth+'
		--group by Prodmonth,
		--Sectionid_2) BusPlan on
  --   b.SECTIONID_2 = BusPlan.SECTIONID_2,

, Sysset s
where a.prodmonth = '+@Prodmonth+'
and a.activity in (1,7)  and a.PlanCode = ''MP'''
 

if @ShowAuth <> 'Y'
Begin
Set @SQL2 = @SQL2+' 
 and a.Locked = 1'
END

Set @SQL2 = @SQL2+' and '+@SectionLevel+' = '''+@Section+''') CorrectDetail
group by GroupSec, SelectSection ) LastSum
group by GroupSec
With Rollup'

Exec(@SQL1+@SQL2)

GO

-- [SP_Dynamic_Planning_Dev] '201701', '1.5 - T Cwati', 'Y'
CREATE Procedure [dbo].[SP_Dynamic_Planning_Dev]
@Prodmonth varchar(6),
@Section Varchar(40),
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(MAX),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @TheLevel = 1
    set @SectionLevel = 'Name_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'Name_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'Name_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'Name_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'Name_1'

   If @TheLevel = 6 
    set @SectionLevel = 'Name'

Set @SQL1 = 'Select 
* from 
(
Select distinct Name_2 Miner,Name_1,a.isStopped, Name Name, Min(Prim) Prim, Workplace,max(a.Workplaceid) Workplaceid ,max(Description) Description, MAX(MiningMethod) MiningMethod, min(BeginDate) BeginDate, max(EndDate) EndDate,
convert(int, Avg(TotalShifts)) TotalShifts,
convert(int, Avg(ShaftPayLimit)) ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(metresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
Min(DrillRig) DrillRig,
convert(Numeric(10), round(Sum(Tons),1)) Tons,
convert(Numeric(10), round(Sum(OnReefTons),1)) OnReefTons,
convert(Numeric(10), round(Sum(OffReefTons),1)) OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
gt = Case when (Sum(OnReefTons) = 0) or (Sum(OnReefTons)is Null) then 0 else
  convert(Numeric(10,1), round(Sum(Grams)/Sum(OnReefTons),1))
end,
cmgt = case when (Sum(OnReefTons) = 0) or (avg(Height) = 0) then 0 else
convert(Numeric(10), round(Sum(Grams)/Sum(OnReefTons)*(avg(Height)),0)) end,
Kg = convert(numeric(10,3),round(sum(grams)/1000,3)),
Min(OrgUnitday) OrgUnitday,
Min(OrgUnitAfterNoon) OrgUnitAfterNoon,
Min(OrgUnitNight) OrgUnitNight,
'''' Remarks,
sum(Labourstrength) CrewStrength,
CubicMetres = Sum(CubicMetres), 
CubicTons = Sum(CubicTons), 
CubicGrams = Sum(CubicGrams), 
CubicGT = Sum(CubicGT),
avg(Bus_Plan_DevCubics/10)+avg(Bus_Plan_Adv)+avg(Bus_Plan_Adv_Sec)+avg(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
avg(Bus_Plan_Adv)+avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv,
avg(Bus_Plan_OnreefAdv)+avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv,
avg(Bus_Plan_OffreefAdv)+avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv,
avg(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
avg(Bus_Plan_Cap_Adv) Bus_Plan_Cap_Adv,
avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
avg(Bus_Plan_Adv) Bus_Plan_Adv_Main,
avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv_Main,
avg(Bus_Plan_OffreefAdv) Bus_Plan_OffreefAdv_Main,
  Sum(isnull(UraniumKg,0)) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*100)/(Sum(UraniumReefSQMDens)))
	end 
 from
(Select name_2 Name_2 ,a.isStopped, name_1 Name_1,name Name, c.Description+'':''+a.Workplaceid Workplace,a.Workplaceid,c.Description, d.BeginDate, d.enddate, d.TotalShifts,
'''' Prim,
OrgUnitday = Case when (max(a.OrgUnitday) is null) then '''' else max(a.OrgUnitday) end,
OrgUnitAfterNoon = Case when (max(a.OrgUnitAfterNoon) is null) then '''' else max(a.OrgUnitAfterNoon) end,
OrgUnitNight = Case when (max(a.OrgUnitNight) is null) then '''' else max(a.OrgUnitNight) end,
'''' MiningMethod,
'''' remarks,
sum(a.MetresAdvance) MetresAdvance,
sum(a.ReefAdv) OnReefAdv,
sum(a.WasteAdv) OffReefAdv,
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  sum(a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then sum(a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then sum(a.CUBICMETRES/10) else 0 end,
max(DrillRig) DrillRig,
sum(a.Tons) Tons,
sum(a.ReefTons) OnReefTons,
sum(a.WasteTons) OffReefTons,
--sum(a.Volume) Volume,
--sum(a.OnReefVolume) OnReefVolume,
--sum(a.OffReefVolume) OffReefVolume,
Grams = sum(a.gt*a.ReefTons),
sum(a.ReefAdv*a.fl) OnReefSQM,
avg(a.sw) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when avg(w.Density) is null or avg(w.Density) = 0 then
sum(cmgt*(a.MetresAdvance*a.fl)*s.RockDensity/100)
else
sum(cmgt*(a.MetresAdvance*a.fl)*w.Density/100) end,
0 labourstrength, 
sum(a.Cubicmetres) Cubicmetres,
sum(a.CubicsTons) CubicTons,
sum(a.CubicGrams) CubicGrams,
sum(a.CubicGT) CubicGT,
	UraniumKg = Sum(a.cmkgt*(a.ReefAdv*a.fl)*w.density/100), 
    UraniumReefSQM = Sum(Case when a.cmkgt > 0 then (a.ReefAdv*a.fl) else 0 end),
    UraniumReefSQMDens = Sum(Case when a.cmkgt > 0 then (a.ReefAdv*a.fl)*w.Density else 0 end), 
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 Bus_Plan_DevCubics,
0 Bus_Plan_Cap_Adv,
0 Bus_Plan_OffreefAdv,
0 Bus_Plan_Adv_Sec,
0 Bus_Plan_OnreefAdv_Sec,
0 Bus_Plan_OffreefAdv_Sec
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid
--left join vw_CrewInfo x on
--a.prodmonth = x.prodmonth and
--a.sectionid = x.sectionid and
--a.workplaceid = x.workplaceid and 
--a.activity = x.activity and
--a.iscubics = x.Iscubics
left outer join workplace w on
a.workplaceid = w.workplaceid
--left Join Bonus_PoolDefaults i on
--a.TargetID = i.TargetID

    --left join
    --(select * from planmonth_ops
    -- where PRODMONTH = '+@Prodmonth+'
    -- and Activity = 1) BusPlan on
    -- b.SECTIONID_2 = BusPlan.SECTIONID_2,

, Sysset s
where a.prodmonth ='+@Prodmonth+'
and a.activity in (1,7)
and b.'+@SectionLevel+' ='''+@Section+'''
AND a.plancode=''MP''' 

if @ShowAuth <> 'Y'
Begin
Set @SQL1 = @SQL1+' 
 and a.Locked = 1'
END


Set @SQL1 = @SQL1+' group by Name_2,a.isStopped, Name_1, Name, c.Description+'':''+a.Workplaceid ,
a.Workplaceid,c.Description, d.BeginDate, d.enddate, d.TotalShifts,c.REEFWASTE) a, sysset s 
group by 
Name_2,Name_1,a.isStopped, Name, Workplace
with rollup) a
where 
(case when Workplace is not Null then 0
	  when (Name is not Null) and (Workplace is null) then 1
	  when (Name_1 is Not null) and (Name is Null) and (Workplace is null) then 2 
	  when (Miner is Not null) and (Name_1 is null) and (Name is Null) and (Workplace is null) then 3 end) in (0,3)'

--select @SQL1

Exec(@SQL1)
--PRINT(@SQL1)


GO

-- [SP_Dynamic_Planning_Dev] '201701', '1.5 - T Cwati', 'Y'
ALTER Procedure [dbo].[SP_Dynamic_Planning_Dev]
@Prodmonth varchar(6),
@Section Varchar(40),
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(MAX),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @TheLevel = 1
    set @SectionLevel = 'Name_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'Name_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'Name_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'Name_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'Name_1'

   If @TheLevel = 6 
    set @SectionLevel = 'Name'

Set @SQL1 = 'Select 
* from 
(
Select distinct Name_2 Miner,Name_1,a.isStopped, Name Name, Min(Prim) Prim, Workplace,max(a.Workplaceid) Workplaceid ,max(Description) Description, MAX(MiningMethod) MiningMethod, min(BeginDate) BeginDate, max(EndDate) EndDate,
convert(int, Avg(TotalShifts)) TotalShifts,
convert(int, Avg(ShaftPayLimit)) ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(metresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
Min(DrillRig) DrillRig,
convert(Numeric(10), round(Sum(Tons),1)) Tons,
convert(Numeric(10), round(Sum(OnReefTons),1)) OnReefTons,
convert(Numeric(10), round(Sum(OffReefTons),1)) OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
gt = Case when (Sum(OnReefTons) = 0) or (Sum(OnReefTons)is Null) then 0 else
  convert(Numeric(10,1), round(Sum(Grams)/Sum(OnReefTons),1))
end,
cmgt = case when (Sum(OnReefTons) = 0) or (avg(Height) = 0) then 0 else
convert(Numeric(10), round(Sum(Grams)/Sum(OnReefTons)*(avg(Height)),0)) end,
Kg = convert(numeric(10,3),round(sum(grams)/1000,3)),
Min(OrgUnitday) OrgUnitday,
Min(OrgUnitAfterNoon) OrgUnitAfterNoon,
Min(OrgUnitNight) OrgUnitNight,
'''' Remarks,
sum(Labourstrength) CrewStrength,
CubicMetres = Sum(CubicMetres), 
CubicTons = Sum(CubicTons), 
CubicGrams = Sum(CubicGrams), 
CubicGT = Sum(CubicGT),
avg(Bus_Plan_DevCubics/10)+avg(Bus_Plan_Adv)+avg(Bus_Plan_Adv_Sec)+avg(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
avg(Bus_Plan_Adv)+avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv,
avg(Bus_Plan_OnreefAdv)+avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv,
avg(Bus_Plan_OffreefAdv)+avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv,
avg(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
avg(Bus_Plan_Cap_Adv) Bus_Plan_Cap_Adv,
avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
avg(Bus_Plan_Adv) Bus_Plan_Adv_Main,
avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv_Main,
avg(Bus_Plan_OffreefAdv) Bus_Plan_OffreefAdv_Main,
  Sum(isnull(UraniumKg,0)) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*100)/(Sum(UraniumReefSQMDens)))
	end 
 from
(Select name_2 Name_2 ,a.isStopped, name_1 Name_1,name Name, c.Description+'':''+a.Workplaceid Workplace,a.Workplaceid,c.Description, d.BeginDate, d.enddate, d.TotalShifts,
'''' Prim,
OrgUnitday = Case when (max(a.OrgUnitday) is null) then '''' else max(a.OrgUnitday) end,
OrgUnitAfterNoon = Case when (max(a.OrgUnitAfterNoon) is null) then '''' else max(a.OrgUnitAfterNoon) end,
OrgUnitNight = Case when (max(a.OrgUnitNight) is null) then '''' else max(a.OrgUnitNight) end,
'''' MiningMethod,
'''' remarks,
sum(a.MetresAdvance) MetresAdvance,
sum(a.ReefAdv) OnReefAdv,
sum(a.WasteAdv) OffReefAdv,
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  sum(a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then sum(a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then sum(a.CUBICMETRES/10) else 0 end,
max(DrillRig) DrillRig,
sum(a.Tons) Tons,
sum(a.ReefTons) OnReefTons,
sum(a.WasteTons) OffReefTons,
--sum(a.Volume) Volume,
--sum(a.OnReefVolume) OnReefVolume,
--sum(a.OffReefVolume) OffReefVolume,
Grams = sum(a.gt*a.ReefTons),
sum(a.ReefAdv*a.fl) OnReefSQM,
avg(a.sw) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when avg(w.Density) is null or avg(w.Density) = 0 then
sum(cmgt*(a.MetresAdvance*a.fl)*s.RockDensity/100)
else
sum(cmgt*(a.MetresAdvance*a.fl)*w.Density/100) end,
0 labourstrength, 
sum(a.Cubicmetres) Cubicmetres,
sum(a.CubicsTons) CubicTons,
sum(a.CubicGrams) CubicGrams,
sum(a.CubicGT) CubicGT,
	UraniumKg = Sum(a.cmkgt*(a.ReefAdv*a.fl)*w.density/100), 
    UraniumReefSQM = Sum(Case when a.cmkgt > 0 then (a.ReefAdv*a.fl) else 0 end),
    UraniumReefSQMDens = Sum(Case when a.cmkgt > 0 then (a.ReefAdv*a.fl)*w.Density else 0 end), 
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 Bus_Plan_DevCubics,
0 Bus_Plan_Cap_Adv,
0 Bus_Plan_OffreefAdv,
0 Bus_Plan_Adv_Sec,
0 Bus_Plan_OnreefAdv_Sec,
0 Bus_Plan_OffreefAdv_Sec
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid
--left join vw_CrewInfo x on
--a.prodmonth = x.prodmonth and
--a.sectionid = x.sectionid and
--a.workplaceid = x.workplaceid and 
--a.activity = x.activity and
--a.iscubics = x.Iscubics
left outer join workplace w on
a.workplaceid = w.workplaceid
--left Join Bonus_PoolDefaults i on
--a.TargetID = i.TargetID

    --left join
    --(select * from planmonth_ops
    -- where PRODMONTH = '+@Prodmonth+'
    -- and Activity = 1) BusPlan on
    -- b.SECTIONID_2 = BusPlan.SECTIONID_2,

, Sysset s
where a.prodmonth ='+@Prodmonth+'
and a.activity in (1,7)
and b.'+@SectionLevel+' ='''+@Section+'''
AND a.plancode=''MP''' 

if @ShowAuth <> 'Y'
Begin
Set @SQL1 = @SQL1+' 
 and a.Locked = 1'
END


Set @SQL1 = @SQL1+' group by Name_2,a.isStopped, Name_1, Name, c.Description+'':''+a.Workplaceid ,
a.Workplaceid,c.Description, d.BeginDate, d.enddate, d.TotalShifts,c.REEFWASTE) a, sysset s 
group by 
Name_2,Name_1,a.isStopped, Name, Workplace
with rollup) a
where 
(case when Workplace is not Null then 0
	  when (Name is not Null) and (Workplace is null) then 1
	  when (Name_1 is Not null) and (Name is Null) and (Workplace is null) then 2 
	  when (Miner is Not null) and (Name_1 is null) and (Name is Null) and (Workplace is null) then 3 end) in (0,3)'

--select @SQL1

Exec(@SQL1)
--PRINT(@SQL1)


GO

-- [SP_Lock_Planning_Dev_Summary] '201701', '1.5 - T Cwati', 4, 'Y'
CREATE Procedure [dbo].[SP_Lock_Planning_Dev_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int,
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(8000),
@SQL2 Varchar(8000),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'
    
  If @TheLevel = 1 
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  
    

Set @SQL1 = 'Select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
'''' remarks,
Sum(metresAdvance+Converted_M) TotalAdvance,
sum(MetresAdvance) MetresAdvance,
sum(OnReefAdv) OnReefAdv,
sum(OffReefAdv) OffReefAdv,
sum(SecTot_M) SecTot_M,
sum(SecOn_M) SecOn_M,
sum(SecOff_M) SecOff_M,
sum(MainTot_M) MainTot_M,
sum(MainOn_M) MainOn_M,
sum(MainOff_M) MainOff_M,
Sum(Capital_M) Capital_M,
Sum(CapitalOn_M) CapitalOn_M,
Sum(CapitalOff_M) CapitalOff_M,
Sum(Converted_M) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
max(DrillRig) DrillRig,
sum(Tons) Tons,
sum(OnReefTons) OnReefTons,
sum(OffReefTons) OffReefTons,
--sum(Volume) Volume,
--sum(OnReefVolume) OnReefVolume,
--sum(OffReefVolume) OffReefVolume,
Grams = sum(Grams),
KG = convert(numeric(10,3), sum(Grams)/1000),
sum(OnReefAdv*Facelength) OnReefSQM,
avg(stopewidth) Height,
0 ShaftPayLimit,
convert(Numeric(10,3),round(Sum(CMGTGramsCalc),1)) Grams,
CMGTGramsCalc = case when avg(Density) is null or avg(Density) = 0 then
sum(cmgt*(MetresAdvance*Facelength)*RockDensity/100)
else
sum(cmgt*(MetresAdvance*Facelength)*Density/100) end,
gt = case when Sum(OnReefTons) is null or Sum(OnReefTons) = 0 then
0
else
sum(Grams)/Sum(OnReefTons) end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
avg(convert(numeric(6,2), labourstrength)) labourstrength, 
sum(Cubicmetres) Cubicmetres,
sum(CubicTons) CubicTons,
sum(CubicGrams) CubicGrams,
sum(CubicGT) CubicGT,
sum(Bus_Plan_DevCubics/10)+sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec)+sum(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv,
sum(Bus_Plan_OnreefAdv)+sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv,
sum(Bus_Plan_OffreefAdv)+sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv,
sum(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
sum(Bus_Plan_Cap_Adv) Bus_Plan_Cap_Adv,
sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
sum(Bus_Plan_Adv) Bus_Plan_Adv_Main,
sum(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv_Main,
sum(Bus_Plan_OffreefAdv) Bus_Plan_OffreefAdv_Main,
  Sum(isnull(UraniumKg,0)) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*100)/(Sum(UraniumReefSQMDens)))
	end 
from
(select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
0 ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(MetresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
convert(Numeric(10,1), round(Sum(ConvertedOn_M),1)) ConvertedOn_M,
convert(Numeric(10,1), round(Sum(ConvertedOff_M),1)) ConvertedOff_M,
min(DrillRig) DrillRig,
Sum(Tons) Tons,
Sum(OnReefTons) OnReefTons,
Sum(OffReefTons) OffReefTons,
--Sum(Volume) Volume,
--Sum(OnReefVolume) OnReefVolume,
--Sum(OffReefVolume) OffReefVolume,
Grams = sum(Grams),
OnReefSQM = Sum(OnReefSQM),
CMGTGramsCalc = Sum(CMGTGramsCalc), 
Avg(FL) faceLength,
Avg(density) density,
Avg(ROCKDENSITY) ROCKDENSITY,
Avg(goldgramsperton) goldgramsperton,
Avg(stopewidth) stopewidth, 
0 PayLimit, 
Avg(labourstrength) labourstrength,
gt = Case when (Sum(OnReefTons) = 0) or (Sum(OnReefTons) is Null) then 0 else
  convert(Numeric(10,1), round(Sum(Grams)/Sum(OnReefTons),1))
end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
Kg = convert(numeric(10,3),round(sum(CMGTGramsCalc)/1000,3)),
CubicMetres = sum(CubicMetres), 
CubicTons = sum(CubicTons), 
CubicGrams = sum(CubicGrams), 
UraniumKg = SUM(UraniumKg), 
UraniumReefSQM = SUM(UraniumReefSQM),
UraniumReefSQMDens = SUM(UraniumReefSQMDens),
CubicGT = sum(CubicGT),
Avg(Bus_Plan_Adv) Bus_Plan_Adv,
Avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
Avg(Bus_Plan_Adv)-Avg(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
Avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
Avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
Avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
Isnull(Avg(DevCubics),0) Bus_Plan_DevCubics,
Isnull(Avg(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
 from
(Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection,  
''''Prim,
a.OrgUnitday ,
a.OrgUnitAfterNoon ,
a.OrgUnitNight ,
'''' remarks,
a.MetresAdvance MetresAdvance,
a.ReefAdv OnReefAdv,
a.WasteAdv OffReefAdv,
a.FL,
w.density,
s.ROCKDENSITY,
gt goldgramsperton,
a.SW stopewidth, 
0 PayLimit, 
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  (a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then (a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then (a.CUBICMETRES/10) else 0 end,
(DrillRig) DrillRig,
(a.Tons) Tons,
(a.ReefTons) OnReefTons,
(a.WasteTons) OffReefTons,
--(a.Volume) Volume,
--(a.OnReefVolume) OnReefVolume,
--(a.OffReefVolume) OffReefVolume,
Grams = (a.gt*a.ReefTons),
(a.ReefAdv*a.fl) OnReefSQM,
(a.sw) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when (w.Density) is null or (w.Density) = 0 then
(a.cmgt*(a.MetresAdvance*c.endheight)*s.RockDensity/100)
else
(a.cmgt*(a.MetresAdvance*c.endheight)*w.Density/100) end,
(convert(numeric(6,2), labourstrength)) labourstrength, 
(a.Cubicmetres) Cubicmetres,
(a.CubicsTons) CubicTons,
(a.CubicGrams) CubicGrams,
(a.CubicGT) CubicGT,
	UraniumKg = (a.cmkgt*(a.ReefAdv*a.fl)*w.density/100), 
    UraniumReefSQM = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl) else 0 end),
    UraniumReefSQMDens = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl)*w.Density else 0 end), 
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 DevCubics,
0 Bus_Plan_Cap_Adv ,
0 Bus_Plan_Adv_Sec,
0 Bus_Plan_OnreefAdv_Sec,
0 Bus_Plan_OffreefAdv_Sec ' 
Set @SQL2 = ' from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid 
left outer join workplace w on 
a.workplaceid = w.workplaceid 
  --  left join
  --  (select 
		--Prodmonth,
		--Sectionid_2,
		--Sum(SQM) SQM,
		--Sum(Kg) Kg,
		--Sum(Cubics) Cubics,
		--Sum(Metresadvance) Metresadvance,
		--Sum(MetresReef) MetresReef,
		--Sum(MetresWaste) MetresWaste,
		--Sum(DevCap) DevCap,
		--Sum(DevSec) DevSec,
		--Sum(DevSecReef) DevSecReef
		-- from PLANMONTH_OPS
  --   where PRODMONTH ='+@Prodmonth+'
		--group by Prodmonth,
		--Sectionid_2) BusPlan on
  --   b.SECTIONID_2 = BusPlan.SECTIONID_2,

, Sysset s
where a.prodmonth = '+@Prodmonth+'
and a.activity in (1,7)  and a.PlanCode = ''LP''  and a.Locked = 1
and '+@SectionLevel+' = '''+@Section+''') CorrectDetail
group by GroupSec, SelectSection ) LastSum
group by GroupSec
with rollup'


--select 1, @SQL1
--union
--select 2, @SQL2

Exec(@SQL1+@SQL2)
GO

-- [SP_Lock_Planning_Dev] '201701', '1.5 - T Cwati', 'Y'
CREATE Procedure [dbo].[SP_Lock_Planning_Dev]
--Declare 
@Prodmonth varchar(6),
@Section Varchar(40),
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(MAX),
@SQL2 Varchar(MAX),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @TheLevel = 1
    set @SectionLevel = 'Name_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'Name_4'  
    
   If @TheLevel = 3 
    set @SectionLevel = 'Name_3'  
     
   If @TheLevel = 4 
    set @SectionLevel = 'Name_2'  
    
   If @TheLevel = 5 
    set @SectionLevel = 'Name_1'

   If @TheLevel = 6 
    set @SectionLevel = 'Name'

Set @SQL1 = 'Select 
* from 
(
Select distinct Name_2 Miner,Name_1,a.isStopped, Name Name, Min(Prim) Prim, Workplace,max(a.Workplaceid) Workplaceid ,max(Description) Description, MAX(MiningMethod) MiningMethod, min(BeginDate) BeginDate, max(EndDate) EndDate,
convert(int, Avg(TotalShifts)) TotalShifts,
0 ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(metresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
Min(DrillRig) DrillRig,
convert(Numeric(10), round(Sum(Tons),1)) Tons,
convert(Numeric(10), round(Sum(OnReefTons),1)) OnReefTons,
convert(Numeric(10), round(Sum(OffReefTons),1)) OffReefTons,
--convert(Numeric(10), round(Sum(Volume),1)) Volume,
--convert(Numeric(10), round(Sum(OnReefVolume),1)) OnReefVolume,
--convert(Numeric(10), round(Sum(OffReefVolume),1)) OffReefVolume,
gt = Case when (Sum(OnReefTons) = 0) or (Sum(OnReefTons)is Null) then 0 else
  convert(Numeric(10,1), round(Sum(Grams)/Sum(OnReefTons),1))
end,
cmgt = case when (Sum(OnReefTons) = 0) or (avg(Height) = 0) then 0 else
convert(Numeric(10), round(Sum(Grams)/Sum(OnReefTons)*(avg(Height)),0)) end,
Kg = convert(numeric(10,3),round(sum(grams)/1000,3)),
Min(OrgUnitday) OrgUnitday,
Min(OrgUnitAfterNoon) OrgUnitAfterNoon,
Min(OrgUnitNight) OrgUnitNight,
'''' Remarks,
sum(Labourstrength) CrewStrength,
CubicMetres = Sum(CubicMetres), 
CubicTons = Sum(CubicTons), 
CubicGrams = Sum(CubicGrams), 
CubicGT = Sum(CubicGT),
avg(Bus_Plan_DevCubics/10)+avg(Bus_Plan_Adv)+avg(Bus_Plan_Adv_Sec)+avg(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
avg(Bus_Plan_Adv)+avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv,
avg(Bus_Plan_OnreefAdv)+avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv,
avg(Bus_Plan_OffreefAdv)+avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv,
avg(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
avg(Bus_Plan_Cap_Adv) Bus_Plan_Cap_Adv,
avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
avg(Bus_Plan_Adv) Bus_Plan_Adv_Main,
avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv_Main,
avg(Bus_Plan_OffreefAdv) Bus_Plan_OffreefAdv_Main,
  Sum(isnull(UraniumKg,0)) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*100)/(Sum(UraniumReefSQMDens)))
	end 
 from
(Select name_2 Name_2 ,a.isStopped, name_1 Name_1,name Name, c.Description+'':''+a.Workplaceid Workplace,a.Workplaceid,c.Description, d.BeginDate, d.enddate, d.TotalShifts,
'''' Prim,
OrgUnitday = Case when (max(a.OrgUnitday) is null) then '''' else max(a.OrgUnitday) end,
OrgUnitAfterNoon = Case when (max(a.OrgUnitAfterNoon) is null) then '''' else max(a.OrgUnitAfterNoon) end,
OrgUnitNight = Case when (max(a.OrgUnitNight) is null) then '''' else max(a.OrgUnitNight) end,
'''' MiningMethod,
'''' remarks,
sum(a.MetresAdvance) MetresAdvance,
sum(a.ReefAdv) OnReefAdv,
sum(a.WasteAdv) OffReefAdv,
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  sum(a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then sum(a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then sum(a.CUBICMETRES/10) else 0 end,
max(DrillRig) DrillRig,
sum(a.Tons) Tons,
sum(a.ReefTons) OnReefTons,
sum(a.WasteTons) OffReefTons,
--sum(a.Volume) Volume,
--sum(a.OnReefVolume) OnReefVolume,
--sum(a.OffReefVolume) OffReefVolume,
Grams = sum(a.gt*a.ReefTons),
sum(a.ReefAdv*a.fl) OnReefSQM,
avg(a.sw) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when avg(w.Density) is null or avg(w.Density) = 0 then
sum(cmgt*(a.MetresAdvance*a.fl)*s.RockDensity/100)
else
sum(cmgt*(a.MetresAdvance*a.fl)*w.Density/100) end,
0 labourstrength, 
sum(a.Cubicmetres) Cubicmetres,
sum(a.CubicsTons) CubicTons,
sum(a.CubicGrams) CubicGrams,
sum(a.CubicGT) CubicGT,
	UraniumKg = Sum(a.cmkgt*(a.ReefAdv*a.fl)*w.density/100), 
    UraniumReefSQM = Sum(Case when a.cmkgt > 0 then (a.ReefAdv*a.fl) else 0 end),
    UraniumReefSQMDens = Sum(Case when a.cmkgt > 0 then (a.ReefAdv*a.fl)*w.Density else 0 end), 
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 Bus_Plan_OffreefAdv,
0 Bus_Plan_DevCubics,
0 Bus_Plan_Cap_Adv,
0 Bus_Plan_Adv_Sec,
0 Bus_Plan_OnreefAdv_Sec,
0 Bus_Plan_OffreefAdv_Sec '

Set @SQL2 = ' from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid
--left join vw_CrewInfo x on
--a.prodmonth = x.prodmonth and
--a.sectionid = x.sectionid and
--a.workplaceid = x.workplaceid and 
--a.activity = x.activity and
--a.iscubics = x.Iscubics
left outer join workplace w on
a.workplaceid = w.workplaceid
--left Join Bonus_PoolDefaults i on
--a.TargetID = i.TargetID

    --left join
    --(select * from planmonth_ops
    -- where PRODMONTH = '+@Prodmonth+'
    -- and Activity = 1) BusPlan on
    -- b.SECTIONID_2 = BusPlan.SECTIONID_2,

, Sysset s
where a.prodmonth ='+@Prodmonth+'
and a.activity in (1,7)
and b.'+@SectionLevel+' ='''+@Section+'''
AND a.plancode=''LP'' and a.Locked = 1


group by Name_2,a.isStopped, Name_1, Name, c.Description+'':''+a.Workplaceid ,a.Workplaceid,c.Description, d.BeginDate, d.enddate, d.TotalShifts,c.REEFWASTE) a, sysset s 
group by 
Name_2,Name_1,a.isStopped, Name, Workplace
with rollup) a
where 
(case when Workplace is not Null then 0
	  when (Name is not Null) and (Workplace is null) then 1
	  when (Name_1 is Not null) and (Name is Null) and (Workplace is null) then 2 
	  when (Miner is Not null) and (Name_1 is null) and (Name is Null) and (Workplace is null) then 3 end) in (0,3)'

--select @SQL1+@SQL2

Exec(@SQL1+@SQL2)
GO
-- [sp_PlannedVsBooked_Daily] 'P', '201701', 'S Mofokeng', '2017-01-18', '7', '1', '', '', '', 'Tons'
ALTER procedure [dbo].[sp_PlannedVsBooked_Daily]

--Declare
    @TypeMonth varchar(1),
       @ProdMonth varchar(6),
       @SectionName varchar(60),
       @RunDate varchar(10),
       @SumLevel varchar(1),
       @Activity varchar(1),
       @Account varchar(1),
       @ReefWaste varchar(1),
       @WorkCap varchar(1),
       @Unit varchar(20)
as
       
declare @TheMinDate varchar(10),@TheMaxDate varchar(10)
Declare @TheLevel Int,
    @SQL1 Varchar(8000),
       @SQL4 Varchar(8000),
    @GroupLevel Varchar(20),
    @SectionLevel Varchar(20),
	@GroupCrewLevel Varchar(20),
	 @GroupWPLevel Varchar(20),
    @SectionWPLevel Varchar(20)





select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1'
       set @GroupLevel = 'NAME_5'    
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'      
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4' 
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'
If @SumLevel = '6' 
       set @GroupLevel = 'NAME'

If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END
  
If @TheLevel = 1 
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  

DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)

IF (@TypeMonth = 'P')
BEGIN
       SET @SQL2 =
         ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                       convert(varchar(10), max(c.CalendarDate),120)      
             from planmonth p 
             inner join section_complete sc on 
                    p.prodmonth = sc.prodmonth and
                    p.sectionid = sc.sectionid
             inner join seccal s on
                    sc.prodmonth = s.prodmonth and
                    sc.sectionid_1 = s.sectionid
             inner join CalType c on
                    s.CalendarCode = c.CalendarCode and
                    s.BeginDate <= c.CalendarDate and
                    s.Enddate >= c.CalendarDate
             Where 
                    sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                    p.Prodmonth = '''+ @ProdMonth +''' and 
                    p.Activity = '+ @Activity +' and 
                    p.PlanCode = ''MP'' '
             
             CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
             INSERT #TheMinDate EXEC(@SQL2)
         
             SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
             SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
             DROP TABLE #TheMinDate

       if @TheMinDate is null 
             set @TheMinDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate is null
             set @TheMaxDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
END
ELSE
BEGIN
       select @TheMinDate = Convert(varchar(10),min(StartDate),120),
               @TheMaxDate = Convert(varchar(10),max(EndDate),120)
       from CalendarMill
       where MillMonth = @ProdMonth

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
END



Set @SQL1 = 'select * from (
       Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   
IF @SumLevel = '8'  
BEGIN
	Set @SQL1 = @SQL1 + ' p.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
END
ELSE
BEGIN  
	IF @SumLevel = '7'  
	BEGIN
		Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
	END       
	ELSE
		Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
END		


	   
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                    Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL1 = @SQL1 + '      
                    Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
             Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
             Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
             Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
             Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
END    

Set @SQL1 = @SQL1 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID 
			 inner join workplace w on
			   w.workplaceid = pd.workplaceid
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
			IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL1 = @SQL1 + ''
				   IF (@WorkCap <> '')
						 Set @SQL1 = @SQL1 + ''       
			END
SET @SQL1 = @SQL1 + ') pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate 
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
			 
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL1 = @SQL1 + ''
       IF (@WorkCap <> '')
             Set @SQL1 = @SQL1 + ''       
END

IF @SumLevel = '8'  
BEGIN
	Set @SQL1 = @SQL1 + '
		   group by ct.CalendarDate, p.'+@GroupCrewLevel+' '
END
ELSE
BEGIN   
	IF @SumLevel = '7'  
	BEGIN
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, w.'+@GroupWPLevel+' '
	END   
	ELSE   
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, ss.'+@GroupLevel+' '
END


Set @SQL4 = ' union all'
Set @SQL4 = @SQL4+
       ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                    Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL4 = @SQL4 + '      
                    Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
              BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
             Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
             Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
             Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
             Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
END    

Set @SQL4 = @SQL4 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID
			 inner join Workplace w on
			   w.workplaceid=pd.workplaceid 
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL4 = @SQL4 + ''
				   IF (@WorkCap <> '')
						 Set @SQL4 = @SQL4 + ''       
			END

      Set @SQL4 = @SQL4 + ' ) pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate and
                    p.PlanCode = pd.PlanCode
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL4 = @SQL4 + ''
       IF (@WorkCap <> '')
             Set @SQL4 = @SQL4 + ''       
END
             
Set @SQL4 = @SQL4 + '
       group by ct.CalendarDate ) a 
order by a.theSort, a.Section, a.CalendarDate'

EXEC (@SQL1+@SQL4)  
GO

-- [sp_PlannedVsBooked_Prog] 'P', '201701', 'S Mofokeng', '2017-01-18', '7', '1', '', '', '', 'Tons'
ALTER procedure [dbo].[sp_PlannedVsBooked_Prog]

--Declare
    @TypeMonth varchar(1),
       @ProdMonth varchar(6),
       @SectionName varchar(60),
       @RunDate varchar(10),
       @SumLevel varchar(1),
       @Activity varchar(1),
       @Account varchar(1),
       @ReefWaste varchar(1),
       @WorkCap varchar(1),
       @Unit varchar(20)
as
       
declare @TheMinDate varchar(10),@TheMaxDate varchar(50)

Declare @TheLevel Int,
    @SQL1 Varchar(8000),
       @SQL4 Varchar(8000),
    @GroupLevel Varchar(20),
    @SectionLevel Varchar(20),
	@GroupWPLevel Varchar(20),
	@GroupCrewLevel Varchar(20)

select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1'
       set @GroupLevel = 'NAME_5'    
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'      
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4' 
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'
If @SumLevel = '6' 
       set @GroupLevel = 'NAME'
If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END 


If @TheLevel = 1
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  


DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)
IF (@TypeMonth = 'P')
BEGIN
    SET @SQL2 =
        ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                    convert(varchar(10), max(c.CalendarDate),120)    
            from planmonth p 
            inner join section_complete sc on 
                p.prodmonth = sc.prodmonth and
                p.sectionid = sc.sectionid
            inner join seccal s on
                sc.prodmonth = s.prodmonth and
                sc.sectionid_1 = s.sectionid
            inner join CalType c on
                s.CalendarCode = c.CalendarCode and
                s.BeginDate <= c.CalendarDate and
                s.Enddate >= c.CalendarDate
            Where 
                sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                p.Prodmonth = '''+ @ProdMonth +''' and 
                p.Activity = '+ @Activity +' and 
                p.PlanCode = ''MP'' '
             
            CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
            INSERT #TheMinDate EXEC(@SQL2)
         
            SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
            SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
            DROP TABLE #TheMinDate

    if @TheMinDate is null 
            set @TheMinDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate is null
            set @TheMaxDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END
ELSE
BEGIN
    select @TheMinDate = Convert(varchar(10),min(StartDate),120),
            @TheMaxDate = Convert(varchar(10),max(EndDate),120)
    from CalendarMill
    where MillMonth = @ProdMonth

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END

IF (@TypeMonth = 'P')
BEGIN
       Set @SQL1 = 'select * from ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '

	   --ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
	    IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' p.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
		END
		ELSE
		BEGIN 
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
			END
			ELSE        
				Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
		END	
                
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL1 = @SQL1 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
               Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL1 = @SQL1 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                              w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
					IF (@Activity = '1')
					   BEGIN
								IF (@Account <> '')
										   Set @SQL1 = @SQL1 + ''
								IF (@WorkCap <> '')
										   Set @SQL1 = @SQL1 + ''       
					   END 
                    Set @SQL1 = @SQL1 + ' ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        p.PlanCode = pd.PlanCode and
                                        ct.CalendarDate >= pd.Calendardate
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                            p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ''
                IF (@WorkCap <> '')
                           Set @SQL1 = @SQL1 + ''       
       END
             
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, p.'+ @GroupCrewLevel
		END
		ELSE
		BEGIN     
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, w.'+ @GroupWPLevel
			END
			ELSE    
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, ss.'+ @GroupLevel
		END	

              --  group by ct.CalendarDate, ss.'+@GroupLevel+' 


       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL4 = @SQL4 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL4 = @SQL4 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                             inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                            w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
				IF (@Activity = '1')
				   BEGIN
							IF (@Account <> '')
									   Set @SQL4 = @SQL4 + ''
							IF (@WorkCap <> '')
									   Set @SQL4 = @SQL4 + ''       
				   END
              Set @SQL4 = @SQL4 + '  ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        ct.CalendarDate >= pd.Calendardate and
                                        p.PlanCode = pd.PlanCode
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                           p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ''
                IF (@WorkCap <> '')
                           Set @SQL4 = @SQL4 + ''       
       END
             
       Set @SQL4 = @SQL4 + '
                group by ct.CalendarDate ) a 
       order by a.theSort, a.Section, a.CalendarDate'
END




IF (@TypeMonth = 'M')
BEGIN 
       declare @thedate datetime

       create table #daterange (theDate varchar(10))

    set @thedate = @TheMinDate
    while @thedate <= @TheMaxDate
    begin
             insert into #daterange values (convert(varchar(10),@thedate,120))
             set @thedate = dateadd(day, 1, @thedate)
    end


Set @SQL1 = '
       select * from 
       ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   --'+ @GroupLevel +' Section, theDate CalendarDate, '
		
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + @GroupCrewLevel +' Section, theDate CalendarDate, '
		END
		ELSE
		BEGIN   
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + @GroupWPLevel +' Section, theDate CalendarDate, '
			END
			ELSE      
				Set @SQL1 = @SQL1 +  @GroupLevel +' Section, theDate CalendarDate, '
		END

       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL1 = @SQL1 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL1 = @SQL1 + '
             from #daterange 
                LEFT outer join '

				IF @SumLevel = '8'  
	   			BEGIN
					Set @SQL1 = @SQL1 + '(select p.'+ @GroupCrewLevel +' , pd.CalendarDate, '
				END
				ELSE
				BEGIN  
					IF @SumLevel = '9'  
	   				BEGIN
						Set @SQL1 = @SQL1 + '(select w.'+ @GroupWPLevel +' , pd.CalendarDate, '
					END
					ELSE       
						Set @SQL1 = @SQL1 + '(select sc.'+ @GroupLevel +' , pd.Calendardate , '
				END
                        --    (select sc.'+ @GroupLevel +' , pd.Calendardate ,
                    Set @SQL1 = @SQL1 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
										pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                            from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid 
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ''
                IF (@WorkCap <> '')
                           Set @SQL1 = @SQL1 + ''       
       END 
             Set @SQL1 = @SQL1 + ' ) pd on theDate >= pd.Calendardate '
                --    group by pd.'+ @GroupLevel +', theDate '
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by p.'+ @GroupCrewLevel +' , theDate'
			END
			ELSE
			BEGIN   
				IF @SumLevel = '9'  
	   			BEGIN
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupWPLevel +' , theDate'
				END
				ELSE      
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupLevel +' , theDate '
			END

       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, theDate CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL4 = @SQL4 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                          BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                          Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL4 = @SQL4 + '
                    from #daterange d
                LEFT outer join '
                           -- (select sc.'+ @GroupLevel +', pd.Calendardate , 
						    IF @SumLevel = '8'  
	   						BEGIN
								Set @SQL4 = @SQL4 + ' (select p.'+ @GroupCrewLevel +' , pd.Calendardate , '
							END
							ELSE
							BEGIN   
								IF @SumLevel = ''  
	   							BEGIN
									Set @SQL4 = @SQL4 + ' (select p.'+ @GroupWPLevel +' , pd.Calendardate , '
								END
								ELSE      
									Set @SQL4 = @SQL4 + ' (select sc.'+ @GroupLevel +', pd.Calendardate , '
							END
                           Set @SQL4 = @SQL4 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

							from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid  
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                       pd.PlanCode = ''MP'' '
	IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ''
                IF (@WorkCap <> '')
                           Set @SQL4 = @SQL4 + ''       
       END 
          Set @SQL4 = @SQL4 + '   ) pd on theDate >= pd.Calendardate
             group by ' --pd.'+ @GroupLevel +', theDate
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL4 = @SQL4 + ' p.'+ @GroupCrewLevel +' , theDate '
			END
			ELSE
			BEGIN 
				IF @SumLevel = '9'  
	   		BEGIN
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupWPLevel +' , theDate '
				END
				ELSE      
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupLevel +', theDate '
			END
      Set @SQL4 = @SQL4 + '  ) z order by theSort, MaxDate, Section, CalendarDate '

END


EXEC (@SQL1+@SQL4)  

--print @SQL1 
--print @SQL4

IF (@TypeMonth = 'M')
BEGIN
	drop table #daterange
END

GO

-- [sp_DailyGradeReport] '2017-02-02', 2, 'Y', 1330, 860, 'G'
ALTER Procedure [dbo].[sp_DailyGradeReport]
--Declare
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int,
@TopPanels varchar(1)

AS
Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then '      '+Description
when isnull(MO,'') <> '' and isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 1
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 'Total '+MO
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
Select 
MO = case when @TopPanels = 'T' then tp.SectionID else b.Sectionid_2 end,
e.Description,
ShiftNo = Sum(Case when d.WorkingDay = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*w.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * w.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * w.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*w.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * w.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * w.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * w.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * w.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
inner join workplace w on
a.workplaceid = w.workplaceid
inner join TOPPANELSSELECTED tp
on w.WorkplaceID = tp.WorkplaceID

Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by tp.SectionID, b.sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
GO

-- [sp_DailyGradeReport_Development] '2017-02-02', 2, 'Y', 1330, 860
ALTER Procedure [dbo].[sp_DailyGradeReport_Development]
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int

AS
Select 'AA' TheTop,
	'MO '+MO MO,
	[Description],
	ShiftNo = Min(ShiftNo),
	TotalShifts = Min(TotalShifts),
	LockPlan_Adv  = Sum(LockPlan_Adv),
	LockPlan_ReefAdv  = Sum(LockPlan_ReefAdv),
	LockPlan_WasteAdv  = Sum(LockPlan_WasteAdv),
	LockPlan_Tonnes = Sum(LockPlan_Tonnes),
	LockPlan_ReefTons = Sum(convert(decimal(10,5),LockPlan_ReefTons)),
	LockPlan_WasteTons = Sum(convert(decimal(10,5),LockPlan_WasteTons)),
	LockPlan_gt = Case When Sum(LockPlan_ReefTons) = 0 then 0 else Sum(LockPlan_Grams)*1000/Sum(LockPlan_ReefTons) end,
	LockPlan_Kg = Sum(LockPlan_Grams),

	DynPlan_Adv  = Sum(DynPlan_Adv),
	DynPlan_ReefAdv  = Sum(DynPlan_ReefAdv),
	DynPlan_WasteAdv  = Sum(DynPlan_WasteAdv),
	DynPlan_Tonnes = Sum(DynPlan_Tonnes),
	DynPlan_ReefTons = Sum(convert(decimal(10,5),DynPlan_ReefTons)),
	DynPlan_WasteTons = Sum(convert(decimal(10,5),DynPlan_WasteTons)),
	DynPlan_gt = Case When Sum(DynPlan_ReefTons) = 0 then 0 else Sum(DynPlan_Grams)*1000/Sum(DynPlan_ReefTons) end,
	DynPlan_Kg = Sum(DynPlan_Grams),

	DynDayPlan_Adv  = Sum(DynDayPlan_Adv),
	DynDayPlan_ReefAdv  = Sum(DynDayPlan_ReefAdv),
	DynDayPlan_WasteAdv  = Sum(DynDayPlan_WasteAdv),
	DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
	DynDayPlan_ReefTons = Sum(convert(decimal(10,5),DynDayPlan_ReefTons)),
	DynDayPlan_WasteTons = Sum(convert(decimal(10,5),DynDayPlan_wasteTons)),
	DynDayPlan_gt = Case When Sum(DynDayPlan_ReefTons) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_ReefTons) end,
	DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

	DayBook_Adv  = Sum(DayBook_Adv),
	DayBook_ReefAdv  = Sum(DayBook_ReefAdv),
	DayBook_WasteAdv  = Sum(DayBook_WasteAdv),
	DayBook_Tonnes = Sum(DayBook_Tonnes),
	DayBook_ReefTons = Sum(convert(decimal(10,5),DayBook_ReefTons)),
	DayBook_WasteTons = Sum(convert(decimal(10,5),DayBook_WasteTons)),
	DayBook_cmgt = 0,
	DayBook_gt = Case When Sum(DayBook_ReefTons) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_ReefTons) end,
	DayBook_Kg = Sum(DayBook_Grams)/1000,

	DynProgPlan_Adv  = Sum(DynProgPlan_Adv),
	DynProgPlan_ReefAdv  = Sum(DynProgPlan_ReefAdv),
	DynProgPlan_WasteAdv  = Sum(DynProgPlan_WasteAdv),
	DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
	DynProgPlan_ReefTons = Sum(convert(decimal(10,5),DynProgPlan_ReefTons)),
	DynProgPlan_WasteTons = Sum(convert(decimal(10,5),DynProgPlan_WasteTons)),
	DynProgPlan_gt = Case When Sum(DynProgPlan_ReefTons) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_ReefTons) end,
	DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

	ProgBook_Adv  = Sum(ProgBook_Adv),
	ProgBook_ReefAdv  = Sum(ProgBook_ReefAdv),
	ProgBook_WasteAdv  = Sum(ProgBook_WasteAdv),
	ProgBook_Tonnes = Sum(ProgBook_Tonnes),
	ProgBook_ReefTons = Sum(convert(decimal(10,5),ProgBook_ReefTons)),
	ProgBook_WasteTons = Sum(convert(decimal(10,5),ProgBook_WasteTons)),
	ProgBook_gt = Case When Sum(ProgBook_ReefTons) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_ReefTons) end,
	ProgBook_Kg = Sum(ProgBook_Grams)/1000,

	ProgVar_Adv = sum(ProgBook_Adv - DynProgPlan_Adv),
	ProgVar_ReefAdv = sum(ProgBook_ReefAdv - DynProgPlan_ReefAdv),
	ProgVar_WasteAdv = sum(ProgBook_Adv - DynProgPlan_WasteAdv),
	ProgVar_Tonnes = sum(ProgBook_Tonnes - DynProgPlan_Tonnes),
	ProgVar_ReefTons = sum(ProgBook_ReefTons - DynProgPlan_ReefTons),
	ProgVar_WasteTons = sum(ProgBook_wasteTons - DynProgPlan_WasteTons),
	ProgVar_gt = case when sum(ProgBook_Tonnes - DynProgPlan_Tonnes) > 0 then
					 sum(ProgBook_Grams - DynProgPlan_Grams) / 
					 sum(ProgBook_Tonnes - DynProgPlan_Tonnes) end,
	ProgVar_Kg = sum(ProgBook_Grams - DynProgPlan_Grams) / 1000,

	ForeCast_WasteAdv = sum(case when @DefaultShift = 'Y' then ProgBook_WasteAdv/ShiftNo * TotalShifts
					else ProgBook_WasteAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefAdv = sum(case when @DefaultShift = 'Y' then ProgBook_ReefAdv/ShiftNo * TotalShifts
					else ProgBook_ReefAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Adv = sum(case when @DefaultShift = 'Y' then ProgBook_Adv/ShiftNo * TotalShifts
					else ProgBook_Adv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Tonnes = sum(case when @DefaultShift = 'Y' then ProgBook_Tonnes / ShiftNo * TotalShifts
							else ProgBook_Tonnes / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefTons = sum(case when @DefaultShift = 'Y' then ProgBook_ReefTons / ShiftNo * TotalShifts
							else ProgBook_ReefTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_WasteTons = sum(case when @DefaultShift = 'Y' then ProgBook_WasteTons / ShiftNo * TotalShifts
							else ProgBook_WasteTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Kg = sum(case when @DefaultShift = 'Y' then (ProgBook_Grams / 1000) / ShiftNo * TotalShifts
							else (ProgBook_Grams / 1000) / ShiftNo * (TotalShifts-@ShiftsNo) end),

	[Top10] = Sum([Top10]),
	Colour = cast((case when  sum(ProgBook_Adv) = 0  then 1   
						 else 0  end) as varchar(1))


from 
(

	Select 
		sc.SectionID_2 MO,
		w.[Description],
		ShiftNo = Sum(Case when ct.WorkingDay = 'Y' then 1 else 0 end),
		TotalShifts = Avg(convert(numeric(10,0), TotalShifts)),

		LockPlan_Adv = max(Isnull(lp.MetresAdvance,0)),
		LockPlan_ReefAdv = max(Isnull(lp.ReefAdv,0)),
		LockPlan_WasteAdv = max(Isnull(lp.WasteAdv,0)),
		LockPlan_Tonnes = max(Isnull(lp.Tons,0)),
		LockPlan_ReefTons = max(Isnull(lp.ReefTons,0)),
		LockPlan_WasteTons = max(Isnull(lp.WasteTons,0)),
		LockPlan_Grams = max(Isnull(lp.Kg,0)),--kg

		DynPlan_Adv = isnull(max(pm.MetresAdvance),0),
		DynPlan_ReefAdv = isnull(max(pm.ReefAdv),0),
		DynPlan_WasteAdv = isnull(max(pm.WasteAdv),0),
		DynPlan_Tonnes = max(Isnull(pm.Tons,0)),
		DynPlan_ReefTons = max(Isnull(pm.ReefTons,0)),
		DynPlan_WasteTons = max(Isnull(pm.WasteTons,0)),
		DynPlan_Grams = Isnull(max(pm.Kg),0),--kg


		DynProgPlan_Adv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynProgPlan_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynProgPlan_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynProgPlan_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Tons,0) end),
		DynProgPlan_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefTons,0) end),
		DynProgPlan_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteTons,0) end),
		DynProgPlan_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Grams,0) end),

		ProgBook_Adv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookMetresAdvance,0) end),
		ProgBook_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefAdv,0) end),
		ProgBook_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteAdv,0) end),
		ProgBook_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookTons,0) end),
		ProgBook_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefTons,0) end),
		ProgBook_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteTons,0) end),
		ProgBook_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.BookGrams,0) end),


		DynDayPlan_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynDayPlan_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynDayPlan_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynDayPlan_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.Tons,0) end),
		DynDayPlan_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.ReefTons,0) end),
		DynDayPlan_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.WasteTons,0) end),
		DynDayPlan_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.Grams,0) end),

		DayBook_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookMetresAdvance,0) end),
		DayBook_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookReefAdv,0) end),
		DayBook_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookWasteAdv,0) end),
		DayBook_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookTons,0) end),
		DayBook_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookReefTons,0) end),
		DayBook_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookWasteTons,0) end),
		DayBook_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookGrams,0) end),

		TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

	from planmonth pm 
	inner join section_complete sc on
		pm.prodmonth = sc.prodmonth and
		pm.sectionid = sc.sectionID and
		pm.Plancode = 'MP'
	left join planmonth lp on
		pm.prodmonth = lp.prodmonth and
		pm.sectionid = lp.sectionID and
		pm.Workplaceid = lp.Workplaceid and
		pm.Activity = lp.Activity and
		lp.PlanCode = 'LP' and
		lp.Locked = 1
	inner join Seccal s on
		sc.prodmonth = s.prodmonth and
		sc.sectionid_1 = s.Sectionid  and
		s.BeginDate <= @TheDate and
		s.enddate >= @TheDate 
	inner join caltype ct on 
		s.Calendarcode = ct.Calendarcode and
		s.BeginDate <= ct.Calendardate and
		s.enddate >= ct.Calendardate
	Inner join Workplace w on
		pm.WorkplaceID = w.WorkplaceID
	left join Planning pd on
		pm.prodmonth = pd.prodmonth and
		pm.sectionid = pd.sectionID and
		pm.Workplaceid = pd.Workplaceid and
		pm.Activity = pd.Activity and
		pm.PLancode = pd.PLancode and
		ct.CalendarDate = pd.Calendardate
	--inner join vw_wp_density de on
	--	pm.workplaceid = de.workplaceid

	Left Join 
	(
		Select top 2000 a.* from PLanmonth a inner join section_complete b on
		a.prodmonth = b.prodmonth and
		a.sectionid = b.sectionID and
		a.Plancode = 'MP'
		inner join 
		Seccal c on
		b.prodmonth = c.prodmonth and
		b.sectionid_1 = c.Sectionid  and
		c.BeginDate <= @TheDate and
		c.enddate >= @TheDate
		inner join Workplace w on
		w.WorkplaceID = a.WorkplaceID
		where a.Activity = 1
		and a.Locked = 1 and
		auth = 'Y'
		and w.Priority = 0
		order by KG desc
	) [Top] on
	pm.prodmonth = [Top].prodmonth and
	pm.sectionid = [Top].sectionID and
	pm.Workplaceid = [Top].Workplaceid and
	pm.Activity = [Top].Activity and
	pm.PLancode = [Top].PLancode 

	where pm.Activity in (1)
	and ct.Calendardate <= @TheDate
	group by sc.SectionID_2, w.[Description]
) Main
Group by MO, [Description]
GO

-- [SP_MODailyReport] '201701', 'REA', '2016-12-21'
CREATE Procedure [dbo].[SP_MODailyReport]
@Prodmonth varchar(6),
@Section VarChar(50),
@TheDate DateTime

AS
Select  Name_2, Name, Description, 
ShiftDay,
Max(Calendardate) Calendardate,
Convert(Numeric(7),Avg(SQM)) SQM, 
Convert(Numeric(7),Avg(PlannedStrength)) PlannedStrength, 
Convert(Numeric(7),Avg(Strength)) Strength, 
Convert(Numeric(7),Avg(AvgAtWorkLabour)) AvgAtWorkLabour, 
Convert(Numeric(7),Avg(FL)) FL, 
Convert(Numeric(7),Avg(cmgt)) cmgt, 
Sum(PlanSQM) PlanSQM,
Sum(BookSQM) BookSQM,
Min(NoteID) NoteID,
Min(SBNotes) SBNotes,
Min(Problem) Problem,
Min(Note) Note,
Min(Explanation) Explanation,
Avg(ShiftProgBookSQM) ShiftProgBookSQM,
Avg(NonShiftProgBookSQM) NonShiftProgBookSQM,
Avg(ProgBookSQM) ProgBookSQM,
Avg(ProgPlanSQM) ProgPlanSQM,
Avg(VAriance) VAriance
 from
(
select Name_2 Name_2, Name Name, d.Description, 
Convert(Numeric(7),isnull(a.SQM,0)) SQM, 
0 PlannedStrength, 
0 Strength, 
0 AvgAtWorkLabour, 
Convert(Numeric(7),isnull(a.FL,0)) FL, 
Convert(Numeric(7),isnull(a.cmgt,0)) cmgt, 
Calendardate = Case when ShiftDay = 0 then s.EndDate+1 else c.Calendardate end,
c.ShiftDay,
Convert(Numeric(7),isnull(c.SQM,0)) PlanSQM,
Convert(Numeric(7),isnull(c.BookSQM,0)) BookSQM,
'' NoteID,
'' SBNotes,
'' Problem,
'' Note,
'' Explanation,
ShiftProgBookSQM,
NonShiftProgBookSQM,
ProgBookSQM,
ProgPlanSQM,
VAriance
 from planmonth a 
 inner join SECTION_COMPLETE b on
a.Prodmonth = b.PRODMONTH and
a.Sectionid = b.SECTIONID and
a.plancode = 'MP'
inner join SECCAL s on
b.Prodmonth = s.PRODMONTH and
b.SECTIONID_1 = s.SECTIONID
inner join PLANNING c on
a.Prodmonth = c.PRODMONTH and
a.Sectionid = c.SECTIONID and
a.Workplaceid = c.WorkplaceID and
a.Activity = c.Activity and
a.IsCubics = c.IsCubics and
a.PlanCode = c.PlanCode
Inner join WORKPLACE d on
a.Workplaceid = d.WorkplaceID
--left join vw_crewinfo e on
--a.Prodmonth = e.PRODMONTH and
--a.Sectionid = e.SECTIONID and
--a.Workplaceid = e.WorkplaceID and
--a.Activity = e.Activity and
--a.iscubics = e.Iscubics
--left join BOOK_PROBLEM pr on
--c.Prodmonth = pr.PRODMONTH and
--c.Sectionid = pr.SECTIONID and
--c.Workplaceid = pr.WorkplaceID and
--c.Activity = pr.Activity and
--c.iscubics = pr.Iscubics and
--c.Calendardate = pr.Calendardate
inner join (select 
p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity,
isnull(sum(SQM),0) ProgPlanSQM,
sum(case when ShiftDay <> 0 then isnull(bookSQM,0) else 0 end) ShiftProgBookSQM,
sum(case when ShiftDay = 0 then isnull(bookSQM,0) else 0 end) NonShiftProgBookSQM,
isnull(sum(BookSQM),0) ProgBookSQM,
VAriance = isnull(sum(BookSQM),0)- isnull(sum(SQM),0)
from PLanning p where
prodmonth = @Prodmonth
and Activity = 0 and
iscubics = 'N'
and Plancode = 'MP'
and Calendardate <= @Thedate
group by p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity) prog on
c.Prodmonth = prog.PRODMONTH and
c.Sectionid = prog.SECTIONID and
c.Workplaceid = prog.WorkplaceID and
c.Activity = prog.Activity
--left join vw_Problem_Complete prc on
--pr.NoteID = prc.noteid and
--pr.Activity = prc.activity
where a.Prodmonth = @Prodmonth
and Sectionid_2 = @Section
and a.PlanCode = 'MP'
and a.locked = 1
and a.Activity = 0
and c.Calendardate <= @Thedate
) a
group by Name_2, Name, Description, 
ShiftDay

GO

-- [SP_MODaily_Dev_Report] '201701', 'REH', '2017-01-12'
CREATE Procedure [dbo].[SP_MODaily_Dev_Report]
@Prodmonth varchar(6),
@Section VarChar(50),
@TheDate DateTime

AS
Select  Name_2, Name, Description, 
ShiftDay,
Max(Calendardate) Calendardate,
Convert(Numeric(7,1),Avg(Metresadvance)) Metresadvance, 
Convert(Numeric(7),Avg(PlannedStrength)) PlannedStrength, 
Convert(Numeric(7),Avg(Strength)) Strength, 
Convert(Numeric(7),Avg(AvgAtWorkLabour)) AvgAtWorkLabour, 
Convert(Numeric(7),Avg(FL)) FL, 
Convert(Numeric(7),Avg(cmgt)) cmgt, 
Sum(PlanSQM) PlanSQM,
Sum(BookSQM) BookSQM,
Min(NoteID) NoteID,
Min(SBNotes) SBNotes,
Min(Problem) Problem,
Min(Note) Note,
Min(Explanation) Explanation,
Avg(ShiftProgBookAdv) ShiftProgBookAdv,
Avg(NonShiftProgBookAdv) NonShiftProgBookAdv,
Avg(ProgBookAdv) ProgBookAdv,
Avg(ProgPlanAdv) ProgPlanAdv,
Avg(VAriance) VAriance
 from
(
select Name_2 Name_2, Name Name, d.Description, 
Convert(Numeric(7,1),isnull(a.Metresadvance,0)) Metresadvance, 
0 PlannedStrength, 
0 Strength, 
0 AvgAtWorkLabour, 
Convert(Numeric(7),isnull(a.FL,0)) FL, 
Convert(Numeric(7),isnull(a.cmgt,0)) cmgt, 
Calendardate = Case when ShiftDay = 0 then s.EndDate+1 else c.Calendardate end,
c.ShiftDay,
Convert(Numeric(7,1),isnull(c.MetresAdvance,0)) PlanSQM,
Convert(Numeric(7,1),isnull(c.BookMetresAdvance,0)) BookSQM,
'' NoteID,
'' SBNotes,
'' Problem,
'' Note,
'' Explanation,
ShiftProgBookAdv,
NonShiftProgBookAdv,
ProgBookAdv,
ProgPlanAdv,
VAriance
 from planmonth a 
 inner join SECTION_COMPLETE b on
a.Prodmonth = b.PRODMONTH and
a.Sectionid = b.SECTIONID
inner join SECCAL s on
b.Prodmonth = s.PRODMONTH and
b.SECTIONID_1 = s.SECTIONID
inner join PLANNING c on
a.Prodmonth = c.PRODMONTH and
a.Sectionid = c.SECTIONID and
a.Workplaceid = c.WorkplaceID and
a.Activity = c.Activity and
a.PlanCode = c.PlanCode
Inner join WORKPLACE d on
a.Workplaceid = d.WorkplaceID
--left join vw_crewinfo e on
--a.Prodmonth = e.PRODMONTH and
--a.Sectionid = e.SECTIONID and
--a.Workplaceid = e.WorkplaceID and
--a.Activity = e.Activity and
--a.iscubics = e.Iscubics
--left join BOOK_PROBLEM pr on
--c.Prodmonth = pr.PRODMONTH and
--c.Sectionid = pr.SECTIONID and
--c.Workplaceid = pr.WorkplaceID and
--c.Activity = pr.Activity and
--c.iscubics = pr.Iscubics and
--c.Calendardate = pr.Calendardate
inner join (select 
p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity,
isnull(sum(Metresadvance),0) ProgPlanAdv,
sum(case when ShiftDay <> 0 then isnull(bookMetresadvance,0) else 0 end) ShiftProgBookAdv,
sum(case when ShiftDay = 0 then isnull(bookMetresadvance,0) else 0 end) NonShiftProgBookAdv,
isnull(sum(BookMetresadvance),0) ProgBookAdv,
VAriance = isnull(sum(BookMetresadvance),0)- isnull(sum(Metresadvance),0)
from PLanning p where
prodmonth = @Prodmonth
and Activity = 1 and
iscubics = 'N' and 
PlanCode = 'MP'
and Calendardate <= @TheDate
group by p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity) prog on
c.Prodmonth = prog.PRODMONTH and
c.Sectionid = prog.SECTIONID and
c.Workplaceid = prog.WorkplaceID and
c.Activity = prog.Activity
--left join vw_Problem_Complete prc on
--pr.NoteID = prc.noteid and
--pr.Activity = prc.activity
where a.Prodmonth = @Prodmonth
and Sectionid_2 = @Section
and a.PlanCode = 'MP'
--and a.locked = 1
and a.Activity = 1
and c.Calendardate <= @TheDate
) a
group by Name_2, Name, Description, 
ShiftDay

GO


-- [sp_DailyGradeReport] '2017-02-02', 2, 'Y', 1330, 860, 'G'
ALTER Procedure [dbo].[sp_DailyGradeReport]
--Declare
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int,
@TopPanels varchar(1)

AS
Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then '      '+Description
when isnull(MO,'') <> '' and isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 1
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 'Total '+MO
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
Select 
MO = case when @TopPanels = 'T' then tp.SectionID else b.Sectionid_2 end,
e.Description,
ShiftNo = Sum(Case when d.WorkingDay = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*w.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * w.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * w.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*w.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * w.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * w.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * w.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * w.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
inner join workplace w on
a.workplaceid = w.workplaceid
inner join TOPPANELSSELECTED tp
on w.WorkplaceID = tp.WorkplaceID

Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by tp.SectionID, b.sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
GO

-- [sp_DailyGradeReport_Development] '2017-02-02', 2, 'Y', 1330, 860
ALTER Procedure [dbo].[sp_DailyGradeReport_Development]
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int

AS
Select 'AA' TheTop,
	'MO '+MO MO,
	[Description],
	ShiftNo = Min(ShiftNo),
	TotalShifts = Min(TotalShifts),
	LockPlan_Adv  = Sum(LockPlan_Adv),
	LockPlan_ReefAdv  = Sum(LockPlan_ReefAdv),
	LockPlan_WasteAdv  = Sum(LockPlan_WasteAdv),
	LockPlan_Tonnes = Sum(LockPlan_Tonnes),
	LockPlan_ReefTons = Sum(convert(decimal(10,5),LockPlan_ReefTons)),
	LockPlan_WasteTons = Sum(convert(decimal(10,5),LockPlan_WasteTons)),
	LockPlan_gt = Case When Sum(LockPlan_ReefTons) = 0 then 0 else Sum(LockPlan_Grams)*1000/Sum(LockPlan_ReefTons) end,
	LockPlan_Kg = Sum(LockPlan_Grams),

	DynPlan_Adv  = Sum(DynPlan_Adv),
	DynPlan_ReefAdv  = Sum(DynPlan_ReefAdv),
	DynPlan_WasteAdv  = Sum(DynPlan_WasteAdv),
	DynPlan_Tonnes = Sum(DynPlan_Tonnes),
	DynPlan_ReefTons = Sum(convert(decimal(10,5),DynPlan_ReefTons)),
	DynPlan_WasteTons = Sum(convert(decimal(10,5),DynPlan_WasteTons)),
	DynPlan_gt = Case When Sum(DynPlan_ReefTons) = 0 then 0 else Sum(DynPlan_Grams)*1000/Sum(DynPlan_ReefTons) end,
	DynPlan_Kg = Sum(DynPlan_Grams),

	DynDayPlan_Adv  = Sum(DynDayPlan_Adv),
	DynDayPlan_ReefAdv  = Sum(DynDayPlan_ReefAdv),
	DynDayPlan_WasteAdv  = Sum(DynDayPlan_WasteAdv),
	DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
	DynDayPlan_ReefTons = Sum(convert(decimal(10,5),DynDayPlan_ReefTons)),
	DynDayPlan_WasteTons = Sum(convert(decimal(10,5),DynDayPlan_wasteTons)),
	DynDayPlan_gt = Case When Sum(DynDayPlan_ReefTons) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_ReefTons) end,
	DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

	DayBook_Adv  = Sum(DayBook_Adv),
	DayBook_ReefAdv  = Sum(DayBook_ReefAdv),
	DayBook_WasteAdv  = Sum(DayBook_WasteAdv),
	DayBook_Tonnes = Sum(DayBook_Tonnes),
	DayBook_ReefTons = Sum(convert(decimal(10,5),DayBook_ReefTons)),
	DayBook_WasteTons = Sum(convert(decimal(10,5),DayBook_WasteTons)),
	DayBook_cmgt = 0,
	DayBook_gt = Case When Sum(DayBook_ReefTons) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_ReefTons) end,
	DayBook_Kg = Sum(DayBook_Grams)/1000,

	DynProgPlan_Adv  = Sum(DynProgPlan_Adv),
	DynProgPlan_ReefAdv  = Sum(DynProgPlan_ReefAdv),
	DynProgPlan_WasteAdv  = Sum(DynProgPlan_WasteAdv),
	DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
	DynProgPlan_ReefTons = Sum(convert(decimal(10,5),DynProgPlan_ReefTons)),
	DynProgPlan_WasteTons = Sum(convert(decimal(10,5),DynProgPlan_WasteTons)),
	DynProgPlan_gt = Case When Sum(DynProgPlan_ReefTons) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_ReefTons) end,
	DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

	ProgBook_Adv  = Sum(ProgBook_Adv),
	ProgBook_ReefAdv  = Sum(ProgBook_ReefAdv),
	ProgBook_WasteAdv  = Sum(ProgBook_WasteAdv),
	ProgBook_Tonnes = Sum(ProgBook_Tonnes),
	ProgBook_ReefTons = Sum(convert(decimal(10,5),ProgBook_ReefTons)),
	ProgBook_WasteTons = Sum(convert(decimal(10,5),ProgBook_WasteTons)),
	ProgBook_gt = Case When Sum(ProgBook_ReefTons) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_ReefTons) end,
	ProgBook_Kg = Sum(ProgBook_Grams)/1000,

	ProgVar_Adv = sum(ProgBook_Adv - DynProgPlan_Adv),
	ProgVar_ReefAdv = sum(ProgBook_ReefAdv - DynProgPlan_ReefAdv),
	ProgVar_WasteAdv = sum(ProgBook_Adv - DynProgPlan_WasteAdv),
	ProgVar_Tonnes = sum(ProgBook_Tonnes - DynProgPlan_Tonnes),
	ProgVar_ReefTons = sum(ProgBook_ReefTons - DynProgPlan_ReefTons),
	ProgVar_WasteTons = sum(ProgBook_wasteTons - DynProgPlan_WasteTons),
	ProgVar_gt = case when sum(ProgBook_Tonnes - DynProgPlan_Tonnes) > 0 then
					 sum(ProgBook_Grams - DynProgPlan_Grams) / 
					 sum(ProgBook_Tonnes - DynProgPlan_Tonnes) end,
	ProgVar_Kg = sum(ProgBook_Grams - DynProgPlan_Grams) / 1000,

	ForeCast_WasteAdv = sum(case when @DefaultShift = 'Y' then ProgBook_WasteAdv/ShiftNo * TotalShifts
					else ProgBook_WasteAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefAdv = sum(case when @DefaultShift = 'Y' then ProgBook_ReefAdv/ShiftNo * TotalShifts
					else ProgBook_ReefAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Adv = sum(case when @DefaultShift = 'Y' then ProgBook_Adv/ShiftNo * TotalShifts
					else ProgBook_Adv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Tonnes = sum(case when @DefaultShift = 'Y' then ProgBook_Tonnes / ShiftNo * TotalShifts
							else ProgBook_Tonnes / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefTons = sum(case when @DefaultShift = 'Y' then ProgBook_ReefTons / ShiftNo * TotalShifts
							else ProgBook_ReefTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_WasteTons = sum(case when @DefaultShift = 'Y' then ProgBook_WasteTons / ShiftNo * TotalShifts
							else ProgBook_WasteTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Kg = sum(case when @DefaultShift = 'Y' then (ProgBook_Grams / 1000) / ShiftNo * TotalShifts
							else (ProgBook_Grams / 1000) / ShiftNo * (TotalShifts-@ShiftsNo) end),

	[Top10] = Sum([Top10]),
	Colour = cast((case when  sum(ProgBook_Adv) = 0  then 1   
						 else 0  end) as varchar(1))


from 
(

	Select 
		sc.SectionID_2 MO,
		w.[Description],
		ShiftNo = Sum(Case when ct.WorkingDay = 'Y' then 1 else 0 end),
		TotalShifts = Avg(convert(numeric(10,0), TotalShifts)),

		LockPlan_Adv = max(Isnull(lp.MetresAdvance,0)),
		LockPlan_ReefAdv = max(Isnull(lp.ReefAdv,0)),
		LockPlan_WasteAdv = max(Isnull(lp.WasteAdv,0)),
		LockPlan_Tonnes = max(Isnull(lp.Tons,0)),
		LockPlan_ReefTons = max(Isnull(lp.ReefTons,0)),
		LockPlan_WasteTons = max(Isnull(lp.WasteTons,0)),
		LockPlan_Grams = max(Isnull(lp.Kg,0)),--kg

		DynPlan_Adv = isnull(max(pm.MetresAdvance),0),
		DynPlan_ReefAdv = isnull(max(pm.ReefAdv),0),
		DynPlan_WasteAdv = isnull(max(pm.WasteAdv),0),
		DynPlan_Tonnes = max(Isnull(pm.Tons,0)),
		DynPlan_ReefTons = max(Isnull(pm.ReefTons,0)),
		DynPlan_WasteTons = max(Isnull(pm.WasteTons,0)),
		DynPlan_Grams = Isnull(max(pm.Kg),0),--kg


		DynProgPlan_Adv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynProgPlan_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynProgPlan_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynProgPlan_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Tons,0) end),
		DynProgPlan_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefTons,0) end),
		DynProgPlan_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteTons,0) end),
		DynProgPlan_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Grams,0) end),

		ProgBook_Adv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookMetresAdvance,0) end),
		ProgBook_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefAdv,0) end),
		ProgBook_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteAdv,0) end),
		ProgBook_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookTons,0) end),
		ProgBook_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefTons,0) end),
		ProgBook_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteTons,0) end),
		ProgBook_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.BookGrams,0) end),


		DynDayPlan_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynDayPlan_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynDayPlan_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynDayPlan_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.Tons,0) end),
		DynDayPlan_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.ReefTons,0) end),
		DynDayPlan_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.WasteTons,0) end),
		DynDayPlan_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.Grams,0) end),

		DayBook_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookMetresAdvance,0) end),
		DayBook_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookReefAdv,0) end),
		DayBook_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookWasteAdv,0) end),
		DayBook_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookTons,0) end),
		DayBook_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookReefTons,0) end),
		DayBook_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookWasteTons,0) end),
		DayBook_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookGrams,0) end),

		TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

	from planmonth pm 
	inner join section_complete sc on
		pm.prodmonth = sc.prodmonth and
		pm.sectionid = sc.sectionID and
		pm.Plancode = 'MP'
	left join planmonth lp on
		pm.prodmonth = lp.prodmonth and
		pm.sectionid = lp.sectionID and
		pm.Workplaceid = lp.Workplaceid and
		pm.Activity = lp.Activity and
		lp.PlanCode = 'LP' and
		lp.Locked = 1
	inner join Seccal s on
		sc.prodmonth = s.prodmonth and
		sc.sectionid_1 = s.Sectionid  and
		s.BeginDate <= @TheDate and
		s.enddate >= @TheDate 
	inner join caltype ct on 
		s.Calendarcode = ct.Calendarcode and
		s.BeginDate <= ct.Calendardate and
		s.enddate >= ct.Calendardate
	Inner join Workplace w on
		pm.WorkplaceID = w.WorkplaceID
	left join Planning pd on
		pm.prodmonth = pd.prodmonth and
		pm.sectionid = pd.sectionID and
		pm.Workplaceid = pd.Workplaceid and
		pm.Activity = pd.Activity and
		pm.PLancode = pd.PLancode and
		ct.CalendarDate = pd.Calendardate
	--inner join vw_wp_density de on
	--	pm.workplaceid = de.workplaceid

	Left Join 
	(
		Select top 2000 a.* from PLanmonth a inner join section_complete b on
		a.prodmonth = b.prodmonth and
		a.sectionid = b.sectionID and
		a.Plancode = 'MP'
		inner join 
		Seccal c on
		b.prodmonth = c.prodmonth and
		b.sectionid_1 = c.Sectionid  and
		c.BeginDate <= @TheDate and
		c.enddate >= @TheDate
		inner join Workplace w on
		w.WorkplaceID = a.WorkplaceID
		where a.Activity = 1
		and a.Locked = 1 and
		auth = 'Y'
		and w.Priority = 0
		order by KG desc
	) [Top] on
	pm.prodmonth = [Top].prodmonth and
	pm.sectionid = [Top].sectionID and
	pm.Workplaceid = [Top].Workplaceid and
	pm.Activity = [Top].Activity and
	pm.PLancode = [Top].PLancode 

	where pm.Activity in (1)
	and ct.Calendardate <= @TheDate
	group by sc.SectionID_2, w.[Description]
) Main
Group by MO, [Description]
GO

-- [SP_MODaily_Dev_Report] '201701', 'REH', '2017-01-12'
ALTER Procedure [dbo].[SP_MODaily_Dev_Report]
@Prodmonth varchar(6),
@Section VarChar(50),
@TheDate DateTime

AS
Select  Name_2, Name, Description, 
ShiftDay,
Max(Calendardate) Calendardate,
Convert(Numeric(7,1),Avg(Metresadvance)) Metresadvance, 
Convert(Numeric(7),Avg(PlannedStrength)) PlannedStrength, 
Convert(Numeric(7),Avg(Strength)) Strength, 
Convert(Numeric(7),Avg(AvgAtWorkLabour)) AvgAtWorkLabour, 
Convert(Numeric(7),Avg(FL)) FL, 
Convert(Numeric(7),Avg(cmgt)) cmgt, 
Sum(PlanSQM) PlanSQM,
Sum(BookSQM) BookSQM,
Min(NoteID) NoteID,
Min(SBNotes) SBNotes,
Min(Problem) Problem,
Min(Note) Note,
Min(Explanation) Explanation,
Avg(ShiftProgBookAdv) ShiftProgBookAdv,
Avg(NonShiftProgBookAdv) NonShiftProgBookAdv,
Avg(ProgBookAdv) ProgBookAdv,
Avg(ProgPlanAdv) ProgPlanAdv,
Avg(VAriance) VAriance
 from
(
select Name_2 Name_2, Name Name, d.Description, 
Convert(Numeric(7,1),isnull(a.Metresadvance,0)) Metresadvance, 
0 PlannedStrength, 
0 Strength, 
0 AvgAtWorkLabour, 
Convert(Numeric(7),isnull(a.FL,0)) FL, 
Convert(Numeric(7),isnull(a.cmgt,0)) cmgt, 
Calendardate = Case when ShiftDay = 0 then s.EndDate+1 else c.Calendardate end,
c.ShiftDay,
Convert(Numeric(7,1),isnull(c.MetresAdvance,0)) PlanSQM,
Convert(Numeric(7,1),isnull(c.BookMetresAdvance,0)) BookSQM,
'' NoteID,
'' SBNotes,
'' Problem,
'' Note,
'' Explanation,
ShiftProgBookAdv,
NonShiftProgBookAdv,
ProgBookAdv,
ProgPlanAdv,
VAriance
 from planmonth a 
 inner join SECTION_COMPLETE b on
a.Prodmonth = b.PRODMONTH and
a.Sectionid = b.SECTIONID
inner join SECCAL s on
b.Prodmonth = s.PRODMONTH and
b.SECTIONID_1 = s.SECTIONID
inner join PLANNING c on
a.Prodmonth = c.PRODMONTH and
a.Sectionid = c.SECTIONID and
a.Workplaceid = c.WorkplaceID and
a.Activity = c.Activity and
a.PlanCode = c.PlanCode
Inner join WORKPLACE d on
a.Workplaceid = d.WorkplaceID
--left join vw_crewinfo e on
--a.Prodmonth = e.PRODMONTH and
--a.Sectionid = e.SECTIONID and
--a.Workplaceid = e.WorkplaceID and
--a.Activity = e.Activity and
--a.iscubics = e.Iscubics
--left join BOOK_PROBLEM pr on
--c.Prodmonth = pr.PRODMONTH and
--c.Sectionid = pr.SECTIONID and
--c.Workplaceid = pr.WorkplaceID and
--c.Activity = pr.Activity and
--c.iscubics = pr.Iscubics and
--c.Calendardate = pr.Calendardate
inner join (select 
p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity,
isnull(sum(Metresadvance),0) ProgPlanAdv,
sum(case when ShiftDay <> 0 then isnull(bookMetresadvance,0) else 0 end) ShiftProgBookAdv,
sum(case when ShiftDay = 0 then isnull(bookMetresadvance,0) else 0 end) NonShiftProgBookAdv,
isnull(sum(BookMetresadvance),0) ProgBookAdv,
VAriance = isnull(sum(BookMetresadvance),0)- isnull(sum(Metresadvance),0)
from PLanning p where
prodmonth = @Prodmonth
and Activity = 1 and
iscubics = 'N' and 
PlanCode = 'MP'
and Calendardate <= @TheDate
group by p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity) prog on
c.Prodmonth = prog.PRODMONTH and
c.Sectionid = prog.SECTIONID and
c.Workplaceid = prog.WorkplaceID and
c.Activity = prog.Activity
--left join vw_Problem_Complete prc on
--pr.NoteID = prc.noteid and
--pr.Activity = prc.activity
where a.Prodmonth = @Prodmonth
and Sectionid_2 = @Section
and a.PlanCode = 'MP'
--and a.locked = 1
and a.Activity = 1
and c.Calendardate <= @TheDate
) a
group by Name_2, Name, Description, 
ShiftDay

GO

-- [SP_MODailyReport] '201701', 'REA', '2016-12-21'
ALTER Procedure [dbo].[SP_MODailyReport]
@Prodmonth varchar(6),
@Section VarChar(50),
@TheDate DateTime

AS
Select  Name_2, Name, Description, 
ShiftDay,
Max(Calendardate) Calendardate,
Convert(Numeric(7),Avg(SQM)) SQM, 
Convert(Numeric(7),Avg(PlannedStrength)) PlannedStrength, 
Convert(Numeric(7),Avg(Strength)) Strength, 
Convert(Numeric(7),Avg(AvgAtWorkLabour)) AvgAtWorkLabour, 
Convert(Numeric(7),Avg(FL)) FL, 
Convert(Numeric(7),Avg(cmgt)) cmgt, 
Sum(PlanSQM) PlanSQM,
Sum(BookSQM) BookSQM,
Min(NoteID) NoteID,
Min(SBNotes) SBNotes,
Min(Problem) Problem,
Min(Note) Note,
Min(Explanation) Explanation,
Avg(ShiftProgBookSQM) ShiftProgBookSQM,
Avg(NonShiftProgBookSQM) NonShiftProgBookSQM,
Avg(ProgBookSQM) ProgBookSQM,
Avg(ProgPlanSQM) ProgPlanSQM,
Avg(VAriance) VAriance
 from
(
select Name_2 Name_2, Name Name, d.Description, 
Convert(Numeric(7),isnull(a.SQM,0)) SQM, 
0 PlannedStrength, 
0 Strength, 
0 AvgAtWorkLabour, 
Convert(Numeric(7),isnull(a.FL,0)) FL, 
Convert(Numeric(7),isnull(a.cmgt,0)) cmgt, 
Calendardate = Case when ShiftDay = 0 then s.EndDate+1 else c.Calendardate end,
c.ShiftDay,
Convert(Numeric(7),isnull(c.SQM,0)) PlanSQM,
Convert(Numeric(7),isnull(c.BookSQM,0)) BookSQM,
'' NoteID,
'' SBNotes,
'' Problem,
'' Note,
'' Explanation,
ShiftProgBookSQM,
NonShiftProgBookSQM,
ProgBookSQM,
ProgPlanSQM,
VAriance
 from planmonth a 
 inner join SECTION_COMPLETE b on
a.Prodmonth = b.PRODMONTH and
a.Sectionid = b.SECTIONID and
a.plancode = 'MP'
inner join SECCAL s on
b.Prodmonth = s.PRODMONTH and
b.SECTIONID_1 = s.SECTIONID
inner join PLANNING c on
a.Prodmonth = c.PRODMONTH and
a.Sectionid = c.SECTIONID and
a.Workplaceid = c.WorkplaceID and
a.Activity = c.Activity and
a.IsCubics = c.IsCubics and
a.PlanCode = c.PlanCode
Inner join WORKPLACE d on
a.Workplaceid = d.WorkplaceID
--left join vw_crewinfo e on
--a.Prodmonth = e.PRODMONTH and
--a.Sectionid = e.SECTIONID and
--a.Workplaceid = e.WorkplaceID and
--a.Activity = e.Activity and
--a.iscubics = e.Iscubics
--left join BOOK_PROBLEM pr on
--c.Prodmonth = pr.PRODMONTH and
--c.Sectionid = pr.SECTIONID and
--c.Workplaceid = pr.WorkplaceID and
--c.Activity = pr.Activity and
--c.iscubics = pr.Iscubics and
--c.Calendardate = pr.Calendardate
inner join (select 
p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity,
isnull(sum(SQM),0) ProgPlanSQM,
sum(case when ShiftDay <> 0 then isnull(bookSQM,0) else 0 end) ShiftProgBookSQM,
sum(case when ShiftDay = 0 then isnull(bookSQM,0) else 0 end) NonShiftProgBookSQM,
isnull(sum(BookSQM),0) ProgBookSQM,
VAriance = isnull(sum(BookSQM),0)- isnull(sum(SQM),0)
from PLanning p where
prodmonth = @Prodmonth
and Activity = 0 and
iscubics = 'N'
and Plancode = 'MP'
and Calendardate <= @Thedate
group by p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity) prog on
c.Prodmonth = prog.PRODMONTH and
c.Sectionid = prog.SECTIONID and
c.Workplaceid = prog.WorkplaceID and
c.Activity = prog.Activity
--left join vw_Problem_Complete prc on
--pr.NoteID = prc.noteid and
--pr.Activity = prc.activity
where a.Prodmonth = @Prodmonth
and Sectionid_2 = @Section
and a.PlanCode = 'MP'
and a.locked = 1
and a.Activity = 0
and c.Calendardate <= @Thedate
) a
group by Name_2, Name, Description, 
ShiftDay

GO

-- [sp_PlannedVsBooked_Daily] 'P', '201701', 'S Mofokeng', '2017-01-18', '7', '1', '', '', '', 'Tons'
ALTER procedure [dbo].[sp_PlannedVsBooked_Daily]

--Declare
    @TypeMonth varchar(1),
       @ProdMonth varchar(6),
       @SectionName varchar(60),
       @RunDate varchar(10),
       @SumLevel varchar(1),
       @Activity varchar(1),
       @Account varchar(1),
       @ReefWaste varchar(1),
       @WorkCap varchar(1),
       @Unit varchar(20)
as
       
declare @TheMinDate varchar(10),@TheMaxDate varchar(10)
Declare @TheLevel Int,
    @SQL1 Varchar(8000),
       @SQL4 Varchar(8000),
    @GroupLevel Varchar(20),
    @SectionLevel Varchar(20),
	@GroupCrewLevel Varchar(20),
	 @GroupWPLevel Varchar(20),
    @SectionWPLevel Varchar(20)





select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1'
       set @GroupLevel = 'NAME_5'    
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'      
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4' 
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'
If @SumLevel = '6' 
       set @GroupLevel = 'NAME'

If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END
  
If @TheLevel = 1 
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  

DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)

IF (@TypeMonth = 'P')
BEGIN
       SET @SQL2 =
         ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                       convert(varchar(10), max(c.CalendarDate),120)      
             from planmonth p 
             inner join section_complete sc on 
                    p.prodmonth = sc.prodmonth and
                    p.sectionid = sc.sectionid
             inner join seccal s on
                    sc.prodmonth = s.prodmonth and
                    sc.sectionid_1 = s.sectionid
             inner join CalType c on
                    s.CalendarCode = c.CalendarCode and
                    s.BeginDate <= c.CalendarDate and
                    s.Enddate >= c.CalendarDate
             Where 
                    sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                    p.Prodmonth = '''+ @ProdMonth +''' and 
                    p.Activity = '+ @Activity +' and 
                    p.PlanCode = ''MP'' '
             
             CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
             INSERT #TheMinDate EXEC(@SQL2)
         
             SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
             SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
             DROP TABLE #TheMinDate

       if @TheMinDate is null 
             set @TheMinDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate is null
             set @TheMaxDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
END
ELSE
BEGIN
       select @TheMinDate = Convert(varchar(10),min(StartDate),120),
               @TheMaxDate = Convert(varchar(10),max(EndDate),120)
       from CalendarMill
       where MillMonth = @ProdMonth

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
END



Set @SQL1 = 'select * from (
       Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   
IF @SumLevel = '8'  
BEGIN
	Set @SQL1 = @SQL1 + ' p.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
END
ELSE
BEGIN  
	IF @SumLevel = '7'  
	BEGIN
		Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
	END       
	ELSE
		Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
END		


	   
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                    Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL1 = @SQL1 + '      
                    Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
             Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
             Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
             Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
             Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
END    

Set @SQL1 = @SQL1 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID 
			 inner join workplace w on
			   w.workplaceid = pd.workplaceid
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
			IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL1 = @SQL1 + ''
				   IF (@WorkCap <> '')
						 Set @SQL1 = @SQL1 + ''       
			END
SET @SQL1 = @SQL1 + ') pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate 
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
			 
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL1 = @SQL1 + ''
       IF (@WorkCap <> '')
             Set @SQL1 = @SQL1 + ''       
END

IF @SumLevel = '8'  
BEGIN
	Set @SQL1 = @SQL1 + '
		   group by ct.CalendarDate, p.'+@GroupCrewLevel+' '
END
ELSE
BEGIN   
	IF @SumLevel = '7'  
	BEGIN
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, w.'+@GroupWPLevel+' '
	END   
	ELSE   
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, ss.'+@GroupLevel+' '
END


Set @SQL4 = ' union all'
Set @SQL4 = @SQL4+
       ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                    Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL4 = @SQL4 + '      
                    Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
              BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
             Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
             Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
             Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
             Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
END    

Set @SQL4 = @SQL4 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID
			 inner join Workplace w on
			   w.workplaceid=pd.workplaceid 
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL4 = @SQL4 + ''
				   IF (@WorkCap <> '')
						 Set @SQL4 = @SQL4 + ''       
			END

      Set @SQL4 = @SQL4 + ' ) pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate and
                    p.PlanCode = pd.PlanCode
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL4 = @SQL4 + ''
       IF (@WorkCap <> '')
             Set @SQL4 = @SQL4 + ''       
END
             
Set @SQL4 = @SQL4 + '
       group by ct.CalendarDate ) a 
order by a.theSort, a.Section, a.CalendarDate'


EXEC (@SQL1+@SQL4)  

--print @SQL1 
--print @SQL4

GO

-- [sp_PlannedVsBooked_Prog] 'P', '201701', 'S Mofokeng', '2017-01-18', '7', '1', '', '', '', 'Tons'
ALTER procedure [dbo].[sp_PlannedVsBooked_Prog]

--Declare
    @TypeMonth varchar(1),
       @ProdMonth varchar(6),
       @SectionName varchar(60),
       @RunDate varchar(10),
       @SumLevel varchar(1),
       @Activity varchar(1),
       @Account varchar(1),
       @ReefWaste varchar(1),
       @WorkCap varchar(1),
       @Unit varchar(20)
as
       
declare @TheMinDate varchar(10),@TheMaxDate varchar(50)

Declare @TheLevel Int,
    @SQL1 Varchar(8000),
       @SQL4 Varchar(8000),
    @GroupLevel Varchar(20),
    @SectionLevel Varchar(20),
	@GroupWPLevel Varchar(20),
	@GroupCrewLevel Varchar(20)

select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1'
       set @GroupLevel = 'NAME_5'    
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'      
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4' 
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'
If @SumLevel = '6' 
       set @GroupLevel = 'NAME'
If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END 


If @TheLevel = 1
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  


DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)
IF (@TypeMonth = 'P')
BEGIN
    SET @SQL2 =
        ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                    convert(varchar(10), max(c.CalendarDate),120)    
            from planmonth p 
            inner join section_complete sc on 
                p.prodmonth = sc.prodmonth and
                p.sectionid = sc.sectionid
            inner join seccal s on
                sc.prodmonth = s.prodmonth and
                sc.sectionid_1 = s.sectionid
            inner join CalType c on
                s.CalendarCode = c.CalendarCode and
                s.BeginDate <= c.CalendarDate and
                s.Enddate >= c.CalendarDate
            Where 
                sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                p.Prodmonth = '''+ @ProdMonth +''' and 
                p.Activity = '+ @Activity +' and 
                p.PlanCode = ''MP'' '
             
            CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
            INSERT #TheMinDate EXEC(@SQL2)
         
            SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
            SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
            DROP TABLE #TheMinDate

    if @TheMinDate is null 
            set @TheMinDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate is null
            set @TheMaxDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END
ELSE
BEGIN
    select @TheMinDate = Convert(varchar(10),min(StartDate),120),
            @TheMaxDate = Convert(varchar(10),max(EndDate),120)
    from CalendarMill
    where MillMonth = @ProdMonth

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END

IF (@TypeMonth = 'P')
BEGIN
       Set @SQL1 = 'select * from ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '

	   --ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
	    IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' p.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
		END
		ELSE
		BEGIN 
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
			END
			ELSE        
				Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
		END	
                
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL1 = @SQL1 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
               Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL1 = @SQL1 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                              w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
					IF (@Activity = '1')
					   BEGIN
								IF (@Account <> '')
										   Set @SQL1 = @SQL1 + ''
								IF (@WorkCap <> '')
										   Set @SQL1 = @SQL1 + ''       
					   END 
                    Set @SQL1 = @SQL1 + ' ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        p.PlanCode = pd.PlanCode and
                                        ct.CalendarDate >= pd.Calendardate
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                            p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ''
                IF (@WorkCap <> '')
                           Set @SQL1 = @SQL1 + ''       
       END
             
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, p.'+ @GroupCrewLevel
		END
		ELSE
		BEGIN     
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, w.'+ @GroupWPLevel
			END
			ELSE    
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, ss.'+ @GroupLevel
		END	

              --  group by ct.CalendarDate, ss.'+@GroupLevel+' 


       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL4 = @SQL4 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL4 = @SQL4 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                             inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                            w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
				IF (@Activity = '1')
				   BEGIN
							IF (@Account <> '')
									   Set @SQL4 = @SQL4 + ''
							IF (@WorkCap <> '')
									   Set @SQL4 = @SQL4 + ''       
				   END
              Set @SQL4 = @SQL4 + '  ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        ct.CalendarDate >= pd.Calendardate and
                                        p.PlanCode = pd.PlanCode
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                           p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ''
                IF (@WorkCap <> '')
                           Set @SQL4 = @SQL4 + ''       
       END
             
       Set @SQL4 = @SQL4 + '
                group by ct.CalendarDate ) a 
       order by a.theSort, a.Section, a.CalendarDate'
END




IF (@TypeMonth = 'M')
BEGIN 
       declare @thedate datetime

       create table #daterange (theDate varchar(10))

    set @thedate = @TheMinDate
    while @thedate <= @TheMaxDate
    begin
             insert into #daterange values (convert(varchar(10),@thedate,120))
             set @thedate = dateadd(day, 1, @thedate)
    end


Set @SQL1 = '
       select * from 
       ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   --'+ @GroupLevel +' Section, theDate CalendarDate, '
		
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + @GroupCrewLevel +' Section, theDate CalendarDate, '
		END
		ELSE
		BEGIN   
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + @GroupWPLevel +' Section, theDate CalendarDate, '
			END
			ELSE      
				Set @SQL1 = @SQL1 +  @GroupLevel +' Section, theDate CalendarDate, '
		END

       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL1 = @SQL1 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL1 = @SQL1 + '
             from #daterange 
                LEFT outer join '

				IF @SumLevel = '8'  
	   			BEGIN
					Set @SQL1 = @SQL1 + '(select p.'+ @GroupCrewLevel +' , pd.CalendarDate, '
				END
				ELSE
				BEGIN  
					IF @SumLevel = '9'  
	   				BEGIN
						Set @SQL1 = @SQL1 + '(select w.'+ @GroupWPLevel +' , pd.CalendarDate, '
					END
					ELSE       
						Set @SQL1 = @SQL1 + '(select sc.'+ @GroupLevel +' , pd.Calendardate , '
				END
                        --    (select sc.'+ @GroupLevel +' , pd.Calendardate ,
                    Set @SQL1 = @SQL1 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
										pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                            from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid 
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ''
                IF (@WorkCap <> '')
                           Set @SQL1 = @SQL1 + ''       
       END 
             Set @SQL1 = @SQL1 + ' ) pd on theDate >= pd.Calendardate '
                --    group by pd.'+ @GroupLevel +', theDate '
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by p.'+ @GroupCrewLevel +' , theDate'
			END
			ELSE
			BEGIN   
				IF @SumLevel = '9'  
	   			BEGIN
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupWPLevel +' , theDate'
				END
				ELSE      
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupLevel +' , theDate '
			END

       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, theDate CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL4 = @SQL4 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                          BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                          Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL4 = @SQL4 + '
                    from #daterange d
                LEFT outer join '
                           -- (select sc.'+ @GroupLevel +', pd.Calendardate , 
						    IF @SumLevel = '8'  
	   						BEGIN
								Set @SQL4 = @SQL4 + ' (select p.'+ @GroupCrewLevel +' , pd.Calendardate , '
							END
							ELSE
							BEGIN   
								IF @SumLevel = ''  
	   							BEGIN
									Set @SQL4 = @SQL4 + ' (select p.'+ @GroupWPLevel +' , pd.Calendardate , '
								END
								ELSE      
									Set @SQL4 = @SQL4 + ' (select sc.'+ @GroupLevel +', pd.Calendardate , '
							END
                           Set @SQL4 = @SQL4 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

							from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid  
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                       pd.PlanCode = ''MP'' '
	IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ''
                IF (@WorkCap <> '')
                           Set @SQL4 = @SQL4 + ''       
       END 
          Set @SQL4 = @SQL4 + '   ) pd on theDate >= pd.Calendardate
             group by ' --pd.'+ @GroupLevel +', theDate
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL4 = @SQL4 + ' p.'+ @GroupCrewLevel +' , theDate '
			END
			ELSE
			BEGIN 
				IF @SumLevel = '9'  
	   		BEGIN
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupWPLevel +' , theDate '
				END
				ELSE      
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupLevel +', theDate '
			END
      Set @SQL4 = @SQL4 + '  ) z order by theSort, MaxDate, Section, CalendarDate '

END


EXEC (@SQL1+@SQL4)  

--print @SQL1 
--print @SQL4

IF (@TypeMonth = 'M')
BEGIN
	drop table #daterange
END

GO

-- [sp_PlanningReportTotalLocked] 'xx', '201507'
CREATE PROCEDURE [dbo].[sp_PlanningReportTotalLocked]
	@Banner varchar(200), 
	@ProdMonth varchar(6)

AS
BEGIN
	 select 'Planning Report (Locked)' label1, 'xx' banner, 
	 '201507' Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 b.budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Sectionid_3, Name_3 
	from SECTION_COMPLETE where prodmonth = '201507') sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity, 
	 sum(ReefMeters) ReefMeters,   SUM(WasteMeters) WasteMeters,  SUM(TotalMeters) TotalMeters, 
	 SUM(DevOunces) DevOunces,  SUM(StopeOunces) StopeOunces,   SUM(TotalOunces) TotalOunces,  
	 SUM(DevContent) DevContent, SUM(StopeContent) StopeContent,   SUM(TotalContent) TotalContent,  
	 SUM(StopeTons) StopeTons,   SUM(DevTons) DevTons, SUM(TotalTons) TotalTons,    
	 SUM(Facelength) Facelength, SUM(ReefSQM) ReefSQM, SUM(WasteSQM) WasteSQM, SUM(Sqm) Sqm,   SUM(AveFAdv) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 
	  0 OldGoldTons
	 ,case when SUM(SQMReefForCalc) <> 0 then SUM(SQMReefForCalc * GT)/SUM(SQMReefForCalc) else 0 end CMGT
	 ,SUM(SW) SW
	 ,SUM(CW) CW
	 ,SUM(SQMReefForCalc * GT) GTForCalc
	 , SUM(SQMSW) SQMSW
	 , SUM(SQMCW) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then KG/1000/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then KG/1000/31.10348 else 0 end as StopeOunces,   
	 KG/1000/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then KG/1000 else 0 end as DevContent, case when pm.Activity in (0,9)   then KG/1000 else 0 end 
	 as StopeContent, KG/1000 as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM,case when pm.Activity in (0,9) then ReefSQM 
	 when pm.activity = 1 then (ReefAdv) else 0 end as SQMReefForCalc, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end 
	 as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
	 ,pm.Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = '201507'  and pm.PlanCode = 'LP') a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,Activity Act,
	 SUM(ReefSQM) budgetReefSQM, SUM(WasteSQM) budget_WasteSQM, SUM(SQM) budget_Sqm, 
 	SUM(StopeTons) budget_StopeTons,    
 	SUM(DevTons) budget_DevTons,  
 	SUM(Fl) budget_Fl,  
 	SUM(ReefMeters) budget_ReefMeters,  SUM(WasteMeters) budget_WasteMeters,   
 	SUM(StopeContent) budget_StopeContent,  
	SUM(DevContent) budget_DevContent,  
 	SUM(AveFAdv) BAveFAdv,SUM(SQMSW) budget_SQMSW,SUM(SQMCW) budget_SQMCW,SUM(SQMCMGT) budget_SQMCMGT
 	from   
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0  Activity, 
 	0 ReefSqm , 
 	0 WasteSqm ,0 SQM,0 StopeTons ,0.0 DevTons ,0 StopeContent , 
 	0 AveFAdv, 
 	0 FL,0.0 DevContent ,0.0 ReefMeters ,0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 
	0 SQMCW   
 	--from businessplan_stoping
 	--where prodmonth = '201507' 
	union 
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm , 
 	0.0 WasteSqm , 
 	0.0 SQM,        
 	0.0 StopeTons , 
 	0.0 DevTons ,           
 	0.0 StopeContent , 
 	0.0 AveFAdv, 
	 0.0 FL, 
 	0 DevContent , 
	0 ReefMeters , 
 	0 WasteMeters,0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW  
 	--from businessplan_development
 	--where prodmonth = '201507' 
	) a  
	 left outer join
	 (select distinct prodmonth, Sectionid_2,Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	 on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth   
	 group by Sectionid_2, Name_2, Sectionid_3,Name_3, Activity) b on b.MOID = sc.Sectionid_2  
	 where a.Activity is not null or b.ACt is not null) e   
	union
	 select 'Planning Report (Locked)' label1, 'xx' banner, 
	 '201507' Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from  (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Sectionid_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 b.budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Sectionid_3, Name_3 
	from Section_complete where prodmonth = '201507') sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity,  
	 sum(0) ReefMeters,   SUM(0) WasteMeters,  SUM(0) TotalMeters, 
	 SUM(0) DevOunces,  SUM(0) StopeOunces,   SUM(0) TotalOunces,  
	 SUM(0) DevContent, SUM(0) StopeContent,   SUM(0) TotalContent,  
	 SUM(0) StopeTons,   SUM(0) DevTons, SUM(0) TotalTons,    
	 SUM(0) Facelength, SUM(0) ReefSQM, SUM(0) WasteSQM, SUM(0) Sqm,   SUM(0) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 
	  0 OldGoldTons
	 ,'0' CMGT
	 ,SUM(0) SW
	 ,SUM(0) CW
	 ,SUM(0) GTForCalc
	 , SUM(0) SQMSW
	 , SUM(0) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then KG/1000/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then KG/1000/31.10348 else 0 end as StopeOunces,   
	 KG/1000/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then KG/1000 else 0 end as DevContent, case when pm.Activity in (0,9)   then KG/1000 else 0 end 
	 as StopeContent, KG/1000 as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end 
	 as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 , 0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
	 ,pm.Activity Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = '201507' and pm.PlanCode = 'LP') a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(
select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,
 		Activity Act, SUM(ReefSQM) budgetReefSQM, SUM(WasteSQM) budget_WasteSQM, SUM(SQM) budget_Sqm, 
 	SUM(StopeTons) budget_StopeTons,   
 	SUM(DevTons) budget_DevTons, 
 	SUM(Fl) budget_Fl, 
 	SUM(ReefMeters) budget_ReefMeters,  SUM(WasteMeters) budget_WasteMeters,  
 	SUM(StopeContent) budget_StopeContent, 
	SUM(DevContent) budget_DevContent, 
 	SUM(AveFAdv) BAveFAdv,SUM(SQMSW) budget_SQMSW,SUM(SQMCW) budget_SQMCW,SUM(SQMCMGT) budget_SQMCMGT 
 	from  
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0 Activity, 
 	0 ReefSqm ,      
 	0 WasteSqm , 
 	0 SQM,       
 	0 StopeTons ,
 	0.0 DevTons ,          
 	0 StopeContent ,
 	0 AveFAdv,
 	0 FL,     
 	0.0 DevContent ,
 	0.0 ReefMeters ,
 	0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 0 SQMCW  
 	--from businessplan_stoping     
 	--where prodmonth = '201507' 
	union
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm ,
 	0.0 WasteSqm ,
 	0.0 SQM,       
 	0.0 StopeTons ,
 	0.0 DevTons ,          
 	0.0 StopeContent ,
 	0.0 AveFAdv,
 	0.0 FL,
 	0 DevContent ,
 	0 ReefMeters ,
 	0 WasteMeters, 0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW 
 	--from businessplan_development     
 	--where prodmonth = '201507'
	) a 
 	left outer join     
 	(select distinct prodmonth, Sectionid_2, Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth  
 	group by Sectionid_2, Name_2, Sectionid_3,Name_3,  Activity ) b on b.MOID = sc.Sectionid_2  
	where a.Activity is not null or b.ACt is not null) e   
END

GO

-- [sp_PlanningReportTotalDynamic]'xx', '201610'
CREATE PROCEDURE [dbo].[sp_PlanningReportTotalDynamic]
@Banner varchar(200), 
@Prodmonth varchar(6)

AS
 select 'Planning Report (Dynamic)' label1, 'xx' banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 0 budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Sectionid_3, Name_3 
	from SECTION_COMPLETE where prodmonth = @ProdMonth) sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity, 
	 sum(ReefMeters) ReefMeters,   SUM(WasteMeters) WasteMeters,  SUM(TotalMeters) TotalMeters, 
	 SUM(DevOunces) DevOunces,  SUM(StopeOunces) StopeOunces,   SUM(TotalOunces) TotalOunces,  
	 SUM(DevContent) DevContent, SUM(StopeContent) StopeContent,   SUM(TotalContent) TotalContent,  
	 SUM(StopeTons) StopeTons,   SUM(DevTons) DevTons, SUM(TotalTons) TotalTons,    
	 SUM(Facelength) Facelength, SUM(ReefSQM) ReefSQM, SUM(WasteSQM) WasteSQM, SUM(Sqm) Sqm,   SUM(AveFAdv) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 
	  0 OldGoldGT, 0 OldGoldTons
	 ,case when SUM(SQMReefForCalc) <> 0 then SUM(SQMReefForCalc * GT)/SUM(SQMReefForCalc) else 0 end CMGT
	 ,SUM(SW) SW
	 ,SUM(CW) CW
	 ,SUM(SQMReefForCalc * GT) GTForCalc
	 , SUM(SQMSW) SQMSW
	 , SUM(SQMCW) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then Kg/1000/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then Kg/1000/31.10348 else 0 end as StopeOunces,   
	 Kg/1000/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then Kg/1000 else 0 end as DevContent, case when pm.Activity in (0,9)   then Kg/1000 else 0 end 
	 as StopeContent, Kg/1000 as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM,case when pm.Activity in (0,9) then ReefSQM 
	 when pm.activity = 1 then (ReefAdv) else 0 end as SQMReefForCalc, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end 
	 as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 , 0.00 OldGoldContents, 0.00 OldGoldCubics, 0.00 OldGoldGT, 0.00 OldGoldTons
	 ,pm.Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = @ProdMonth  --and (pm.OldGoldContents IS null or pm.OldGoldContents = 0)
	 ) a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,Activity Act,
	0 budget_WasteSQM, 0 budget_Sqm, 
 	0 budget_StopeTons,    
 	0 budget_DevTons,  
 	0 budget_Fl,  
 	0 budget_ReefMeters, 0 budget_WasteMeters,   
 	0 budget_StopeContent,  
	0 budget_DevContent,  
 	0 BAveFAdv, 0 budget_SQMSW, 0 budget_SQMCW, 0 budget_SQMCMGT
 	from   
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0  Activity, 
 	0 ReefSqm , 
 	0 WasteSqm ,0 SQM,0 StopeTons ,0.0 DevTons ,0 StopeContent , 
 	0 AveFAdv, 
 	0 FL,0.0 DevContent ,0.0 ReefMeters ,0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 
	0 SQMCW   
 	--from businessplan_stoping
 	--where prodmonth = '201507' 
	union 
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm , 
 	0.0 WasteSqm , 
 	0.0 SQM,        
 	0.0 StopeTons , 
 	0.0 DevTons ,           
 	0.0 StopeContent , 
 	0.0 AveFAdv, 
	0.0 FL, 
 	0.0 DevContent , 
	0.0 ReefMeters , 
 	0.0 WasteMeters,0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW  
 	--from businessplan_development
 	--where prodmonth = '201507' 
	) a  
	 left outer join
	 (select distinct prodmonth, Sectionid_2,Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	 on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth   
	 group by Sectionid_2, Name_2, Sectionid_3,Name_3, Activity) b on b.MOID = sc.Sectionid_2  
	 where a.Activity is not null or b.ACt is not null) e   
	union
	 select 'Planning Report (Dynamic)' label1, 'xx' banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from  (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 b.budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Name_3, Sectionid_3 
	from SECTION_COMPLETE where prodmonth = @ProdMonth) sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity,  
	 sum(0) ReefMeters,   SUM(0) WasteMeters,  SUM(0) TotalMeters, 
	 SUM(0) DevOunces,  SUM(0) StopeOunces,   SUM(0) TotalOunces,  
	 SUM(0) DevContent, SUM(0) StopeContent,   SUM(0) TotalContent,  
	 SUM(0) StopeTons,   SUM(0) DevTons, SUM(0) TotalTons,    
	 SUM(0) Facelength, SUM(0) ReefSQM, SUM(0) WasteSQM, SUM(0) Sqm,   SUM(0) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
	 ,'0' CMGT
	 ,SUM(0) SW
	 ,SUM(0) CW
	 ,SUM(0) GTForCalc
	 , SUM(0) SQMSW
	 , SUM(0) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then Grams/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then Grams/31.10348 else 0 end as StopeOunces,   
	 Grams/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then Grams else 0 end as DevContent, case when pm.Activity in (0,9)   then Grams else 0 end 
	 as StopeContent, Grams as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 , 0.00 OldGoldContents, 0.00 OldGoldCubics, 0.00 OldGoldGT, 0.00 OldGoldTons
	 , '' Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, PLANMONTH_OLDGOLD pmog, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = @ProdMonth -- and (pm.OldGoldContents IS not null or pm.OldGoldContents <> 0)
	 ) a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(
select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,
 		Activity Act, SUM(ReefSQM) budgetReefSQM, SUM(WasteSQM) budget_WasteSQM, SUM(SQM) budget_Sqm, 
 	SUM(StopeTons) budget_StopeTons,   
 	SUM(DevTons) budget_DevTons, 
 	SUM(Fl) budget_Fl, 
 	SUM(ReefMeters) budget_ReefMeters,  SUM(WasteMeters) budget_WasteMeters,  
 	SUM(StopeContent) budget_StopeContent, 
	SUM(DevContent) budget_DevContent, 
 	SUM(AveFAdv) BAveFAdv,SUM(SQMSW) budget_SQMSW,SUM(SQMCW) budget_SQMCW,SUM(SQMCMGT) budget_SQMCMGT 
 	from  
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0 Activity, 
 	0 ReefSqm ,      
 	0 WasteSqm , 
 	0 SQM,       
 	0 StopeTons ,
 	0.0 DevTons ,          
 	0 StopeContent ,
 	0 AveFAdv,
 	0 FL,     
 	0.0 DevContent ,
 	0.0 ReefMeters ,
 	0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 0 SQMCW  
 	--from businessplan_stoping     
 	--where prodmonth = '201507' 
	union
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm ,
 	0.0 WasteSqm ,
 	0.0 SQM,       
 	0.0 StopeTons ,
 	0.0 DevTons ,          
 	0.0 StopeContent ,
 	0.0 AveFAdv,
 	0.0 FL,
 	0 DevContent ,
 	0 ReefMeters ,
 	0 WasteMeters, 0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW 
 	--from businessplan_development     
 	--where prodmonth = '201507'
	) a 
 	left outer join     
 	(select distinct prodmonth, Sectionid_2, Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth  
 	group by Sectionid_2, Name_2, Sectionid_3,Name_3,  Activity ) b 
	on b.MOID = sc.sectionid_2  where a.Activity is not null or b.ACt is not null) e   



GO

 -- sp_PlanningReportLockedDetail '201612', 'REA'
CREATE Procedure [dbo].[sp_PlanningReportLockedDetail]
@Prodmonth varchar(6),
@SectionID varchar(10)

AS
 
 select 'Planning Detail Report (Locked)' label1, '' banner, 
 @Prodmonth Myprodmonth , 'Total Mine' thesection, Fl,  
 * from (select CONVERT(numeric(11, 0), CMGT) CMGT, CONVERT(numeric(15, 0), AdvCMGT) AdvCMGT, 
 CONVERT(numeric(15, 0), SqmCMGT) SqmCMGT, 
 Vamps, s2reptosecid, mosection, ReptoSecid, MoName, Fl, 
 Adv , 
 SW, CW, SQM*sw SQMSW, reefsqm*cw SQMCW, 
 Reefmetres,    Wastemetres,  ReefSQM, WasteSQM, OldGoldTons, OldGoldContents, Cubics, Dens, BrokenRockDensity, 
 OrgUnitDay, ShiftBossName, MinerName, 
 Totalmetres,   wpDesc, wpID,  SbSecID,MinerSecID, 
 DevOunces, StopeOunces,    TotalOunces,  
 CONVERT(numeric(15, 0), DevContent) DevContent, CONVERT(numeric(15, 0), StopeContent) StopeContent, 
 CONVERT(numeric(15, 0), TotalContent) TotalContent, 
 StopeTons,    DevTons, TotalTons,    
 Facelength,  SQM SQM, AveFAdv from (     
 select GT CMGT,0 Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName, SECTIONID_1 SbSecID, 
 sc.SECTIONID MinerSecID, NAME MinerName, OrgUnitDay, pm.FL Fl,
 Adv = case when pm.Activity IN( 0,9) and pm.FL > 0 then pm.SQM/pm.fl else 0 end, 
 pm.SW SW, w.BrokenRockDensity BrokenRockDensity,
 0 OldGoldTons, 0 OldGoldContents, 0 Cubics, case when SQM != 0 then pm.Density else 0 end as Dens, 
 sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid,  
 name_2 MoName,  
 case when pm.Activity = 1 then KG/1000/31.10348 else 0 end as DevOunces,     
 case when pm.Activity IN (0,9)   then KG/1000/31.10348 else 0 end as StopeOunces,   
 KG/1000/31.10348 TotalOunces,    

 case when pm.Activity = 1 then KG/1000 else 0 end as DevContent,     
 case when pm.Activity IN (0,9)   then KG/1000 else 0 end as StopeContent,   
 (KG/1000) TotalContent,    

 case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
 case when pm.activity IN (0,9)   then (ReefTons+WasteTons) else 0 END as StopeTons,     
 (ReefTons+WasteTons) TotalTons, FL Facelength, SQM,     
 case when pm.Activity IN( 0,9) and FL > 0  
  then SQM/FL  else 0 end as AveFAdv, 
case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end as ReefMetres,
 
case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end as WasteMetres,
 
 case when ReefSQM is not null then ReefSQM else 0 end ReefSQM, 
 case when WasteSQM is not null then WasteSQM else 0 end WasteSQM, 
 case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * GT else 0 end AdvCMGT, 
 case when ReefSQM is not null AND pm.Activity IN( 0,9) then ReefSQM * GT else 0 end SqmCMGT, 
 ISNULL(CW, 0) CW, 
 case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres 
 from planmonth pm, section_complete sc, workplace w   where  
 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
 pm.prodmonth = @Prodmonth and sc.SECTIONID_2 = @SectionID and pm.PlanCode = 'LP') a 
 )q  order by SbSecID, MinerSecID, OrgUnitDay, wpDesc 
GO

-- [sp_PlanningReportDynamicDetail] '201612', 'REA'
CREATE Procedure [dbo].[sp_PlanningReportDynamicDetail]
@Prodmonth varchar(6),
@SectionID varchar(10)

AS
select 'Planning Detail Report (Dynamic)' label1, '' banner, 
@ProdMonth Myprodmonth , 'Total Mine' thesection, Fl,  
* from(select BrokenRockDensity, CONVERT(numeric(11, 0), CMGT) CMGT, CONVERT(numeric(15, 0), AdvCMGT) AdvCMGT,
CONVERT(numeric(15, 0), SqmCMGT) SqmCMGT, 0 Vamps,
s2reptosecid, mosection, ReptoSecid, MoName, Fl, Adv, 0 SW, CW, 0 SQMSW, reefsqm * cw SQMCW,
Reefmetres, Wastemetres, ReefSQM, WasteSQM, OldGoldTons, OldGoldContents, Cubics, Dens, OrgUnitDay, ShiftBossName,
MinerName,
Totalmetres, wpDesc, wpID, SbSecID, MinerSecID, DevOunces, StopeOunces, TotalOunces,
isnull(DevContent, 0) DevContent, isnull(StopeContent, 0) StopeContent, isnull(TotalContent, 0) TotalContent,
StopeTons, DevTons, TotalTons,
Facelength, SQM, AveFAdv from(
select CMGT = case when pm.activity IN(0, 9) and isnull(pm.ReefSQM, 0) > 0 then pm.GT
when pm.activity = 1 and isnull(pm.ReefAdv, 0) > 0 then pm.GT else 0 end,
0 Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName,
SECTIONID_1 SbSecID, sc.SECTIONID MinerSecID, NAME MinerName, OrgUnitDay,
pm.FL Fl, Adv = case when pm.Activity IN(0, 9) and pm.FL > 0 then pm.SQM / pm.FL else 0 end,
0 BrokenRockDensity,
0 OldGoldTons, 0 OldGoldContents, 0 Cubics, case when SQM != 0
then pm.Density else 0 end as Dens, sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 MoName,
case when pm.Activity = 1 then Kg/1000 / 31.10348 else 0 end as DevOunces,
case when pm.Activity IN(0, 9)   then Kg/1000 / 31.10348 else 0 end as StopeOunces,
Kg/1000 / 31.10348 TotalOunces,
case when pm.Activity = 1 then Kg/1000 else 0 end as DevContent,
case when pm.Activity IN(0, 9)   then Kg/1000 else 0 end as StopeContent,
0 TotalContent,
case when pm.activity = 1   then(ReefTons + WasteTons) else 0 END as DevTons,
case when pm.activity IN(0, 9)   then  Tons else 0 END as StopeTons,
0 TotalTons, FL Facelength, SQM,     case when FL > 0
then SQM / FL  else 0 end as AveFAdv,
case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste = 'R' THEN DevSec ELSE 0 END else 0 end as ReefMetres,
case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste = 'W' THEN DevSec ELSE 0 END else 0 end as WasteMetres,
case when ReefSQM is not null then ReefSQM else 0 end ReefSQM,
case when WasteSQM is not null then WasteSQM else 0 end WasteSQM,
case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * GT else 0 end AdvCMGT,
case when ReefSQM is not null AND pm.Activity IN(0, 9) then ReefSQM * GT else 0 end SqmCMGT,
ISNULL(CW, 0) CW,
case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres
from planmonth pm, section_complete sc, workplace w   where
pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and
pm.prodmonth = @ProdMonth and sc.SECTIONID_2 = @SectionID) a
) q order by SbSecID, MinerSecID, wpDesc
GO

-- [SP_Planning_Stope_Summary] '201702', '1.1 - A Dube', 6
ALTER Procedure [dbo].[SP_Planning_Stope_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(max),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID 
 from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'

	  If @SumLevel = 6 
    set @GroupLevel = 'NAME'

  If @TheLevel = 1 
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  

If @SumLevel = 6
    set @GroupLevel = 'NAME'

Set @SQL1 = 'Select GroupSec,
Sum(SquareMetres) SquareMetres,
Sum(OnReefSQM) OnReefSQM,
Sum(OffReefSQM) OffReefSQM,
Sum(TargetSQM) TargetSQM,
convert(int,round(Sum(FaceLength),0)) FaceLength,
convert(int,round(Sum(OnReefFL),0)) OnReefFL,
convert(int,round(Sum(OffReefFL),0)) OffReefFL,
FaceAdv = Case when Sum(FaceLength) = 0 then 0 else
convert(Numeric(10,1),Sum(SquareMetres)/Sum(FaceLength)) end,
OnreefAdv = Case when Sum(OnreefFl) = 0 then 0 else
convert(Numeric(10,1),Sum(OnreefSQM)/Sum(OnreefFl)) end,
OffreefAdv = Case when Sum(OffreefFl) = 0 then 0 else
convert(Numeric(10,1),Sum(OffreefSQM)/Sum(OffreefFl)) end,
CW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))
end,
CTons = convert(int,round(Sum(Ctons),0)),
SW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))
end,
ISW =
  Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else
  convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))
end,
Tons = convert(int,round(Sum(tons),0)),
OnReefTons = convert(int,round(Sum(OnReeftons),0)),
OffReefTons = convert(int,round(Sum(OffReeftons),0)),
Volume = convert(int,round(Sum(Volume),0)),
OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),
OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),
[g/t] =
case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else
Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))
end,
convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,
Cmgt =
Case when
  (Sum(OnreefSQM) = 0) or (Sum(OnreefSQM) is null) then 0 else
convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
end,
PaySQM = Sum(PaySQM),
UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),
PayTons = Sum(PayTons),
UnPayTons = Sum(Tons)-Sum(PayTons),
convert(int,Round(avg(PayLimit),0)) ShaftPayLimit, 
cubicmetres = Sum(cubicmetres),
CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),
CubicGrams = Convert(Numeric(7),Round(Sum(CubicGrams),0)),
CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
  
  SUM(Bus_Plan_SQM) Bus_Plan_SQM,
  convert(Numeric(10,3),round(Sum(Bus_Plan_Grams)/1000,3)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_Grams)*100)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end 
  from 
  (select  
    SelectSection, 
    GroupSec,
    SUM(SquareMetres) SquareMetres,
	SUM(OnReefSQM) OnReefSQM,  
    SUM(OffReefSQM) OffReefSQM,  
    SUM(TargetSQM) TargetSQM, 
    SUM(MetresAdvance) MetresAdvance,   
    SUM(OnReefAdv) OnReefAdv,   
    SUM(OffReefAdv) OffReefAdv,   
    SUM(FL) FaceLength,   
    SUM(FL) OnReefFL,   
    SUM(OffReefFL) OffReefFL,   
    SUM(CWSQM) CWSQM,  
    SUM(CTons) CTons,  
    SUM(SWSQM) SWSQM,  
    SUM(ISWSQM) ISWSQM,  
    SUM(Volume) Volume,  
    SUM(OnReefVolume) OnReefVolume,  
    SUM(OffReefVolume) OffReefVolume,  
    SUM(Tons) Tons,  
    SUM(ReefTons) ReefTons,  
    SUM(OnReefTons) OnReefTons,  
    SUM(OffReefTons) OffReefTons,  
    SUM(OnreefSQMDens) OnreefSQMDens,  
    SUM(Grams) Grams,  
    SUM(Kg) Kg,  
    SUM(PaySQM) PaySQM,  
    SUM(PayTons) PayTons,  
    --SUM(OnreefSQM) OnreefSQM,  
    avg(RockDensity) RockDensity,
    avg(PayLimit) PayLimit,  
    SUM(cubicmetres) cubicmetres,  
    SUM(CubicTons) CubicTons,
    SUM(CubicGrams) CubicGrams,   
    SUM(CubicGT) CubicGT,
    
    AVG(Bus_Plan_SQM) Bus_Plan_SQM,
    AVG(Bus_Plan_Grams) Bus_Plan_Grams
 from
(Select 
B.'+@GroupLevel+' 

GroupSec, B.Name_2 SelectSection, 
Convert(Numeric(7),a.SQM+a.WasteSQM) SquareMetres,
Convert(Numeric(7),a.SQM) OnReefSQM,
Convert(Numeric(7),a.WasteSQM) OffReefSQM,
0 TargetSQM,
a.FaceAdvance MetresAdvance, 
0 OnReefAdv, 
0 OffReefAdv, 
a.FL, 
a.FL OnReefFL, 
0 OffReefFL, 
CWSQM = a.SQM*a.CW,
CTons = a.SQM*a.CW*w.Density/100,
SWSQM = a.SQM*a.SW,
ISWSQM = a.SQM*sm.SWidth,
Volume = 0,
OnReefVolume = 0,
OffReefVolume = 0,
Tons = Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)),
ReefTons = Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)),
OnReefTons = Convert(Numeric(7),Round(a.SQM*SW/100*w.density,0)),
OffReefTons = 0,
OnreefSQMDens = a.SQM*w.Density,
Grams = a.cmgt*a.SQM*w.density/100,
Kg = convert(numeric(10,3),Round(a.cmgt*a.SQM*w.Density/100,0)/100),
0 PaySQM,
0 PayTons,
--Convert(Numeric(7),a.SQM) OnreefSQM,
s.RockDensity,
0 PayLimit,
Convert(Numeric(7),a.cubicmetres) cubicmetres,
Convert(Numeric(7),Round(a.cubicmetres*w.density,0)) CubicTons, 
0 CubicGrams, 
0 CubicGT,
0 Bus_Plan_SQM,
0 Bus_Plan_Grams
     
 from PLANMONTH a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on
--a.prodmonth = e.prodmonth and
--c.OreFlowID  = e.OreFlowID 
inner join workplace w on a.workplaceid = w.workplaceid
left join sampling sm on
--a.Prodmonth = sm.prodmonth and
a.workplaceid = sm.workplaceid

    --left join
    --(select * from MONTH_CORP_PLAN
    -- where PRODMONTH = '''+@Prodmonth+'''
    -- and Activity = 0) BusPlan on
    -- b.Name_2 = BusPlan.Name_2,

	 ,Sysset s
	where a.prodmonth = '''+@prodmonth+'''
	 and a.activity in (0,3)
	 and b.'+@SectionLevel+' = '''+@Section+''' and a.PlanCode = ''MP''
	) DetailCorrection
    Group by GroupSec, SelectSection 
    
    ) LastSum
    group by GroupSec' 

Exec(@SQL1)
 --PRINT(@SQL1)
GO

-- [SP_Dynamic_Planning_Dev_Summary] '201701', '1.5 - T Cwati', 6, 'Y'
ALTER Procedure [dbo].[SP_Dynamic_Planning_Dev_Summary]
--Declare
  @Prodmonth varchar(6),
  @Section VarChar(50),
  @SumLevel int,
  @ShowAuth VarChar(5) = 'Y'

AS
Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
		@SQL2 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'

	  If @SumLevel = 6
    set @GroupLevel = 'NAME'

  If @TheLevel = 1
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  

	  If @TheLevel = 6
    set @SectionLevel = 'NAME'  
    

Set @SQL1 = 'Select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
'''' remarks,
Sum(metresAdvance+Converted_M) TotalAdvance,
sum(MetresAdvance) MetresAdvance,
sum(OnReefAdv) OnReefAdv,
sum(OffReefAdv) OffReefAdv,
sum(SecTot_M) SecTot_M,
sum(SecOn_M) SecOn_M,
sum(SecOff_M) SecOff_M,
sum(MainTot_M) MainTot_M,
sum(MainOn_M) MainOn_M,
sum(MainOff_M) MainOff_M,
Sum(Capital_M) Capital_M,
Sum(CapitalOn_M) CapitalOn_M,
Sum(CapitalOff_M) CapitalOff_M,
Sum(Converted_M) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
max(DrillRig) DrillRig,
sum(Tons) Tons,
sum(OnReefTons) OnReefTons,
sum(OffReefTons) OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = sum(Grams),
convert(numeric(10,3), sum(Grams)/1000) KG, 
sum(OnReefAdv*Facelength) OnReefSQM,
avg(stopewidth) Height,
0 ShaftPayLimit,
convert(Numeric(10,3),round(Sum(CMGTGramsCalc),1)) Grams,
CMGTGramsCalc = case when avg(Density) is null or avg(Density) = 0 then
sum(cmgt*(MetresAdvance*Facelength)*RockDensity/100)
else
sum(cmgt*(MetresAdvance*Facelength)*Density/100) end,
gt = case when Sum(OnReefTons) is null or Sum(OnReefTons) = 0 then
0
else
sum(Grams)/Sum(OnReefTons) end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
avg(convert(numeric(6,2), labourstrength)) labourstrength,
sum(Cubicmetres) Cubicmetres,
sum(CubicTons) CubicTons,
sum(CubicGrams) CubicGrams,
sum(CubicGT) CubicGT,
sum(Bus_Plan_DevCubics/10)+sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec)+sum(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv,
sum(Bus_Plan_OnreefAdv)+sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv,
sum(Bus_Plan_OffreefAdv)+sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv,
sum(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
sum(Bus_Plan_Cap_Adv) Bus_Plan_Cap_Adv,
sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
sum(Bus_Plan_Adv) Bus_Plan_Adv_Main,
sum(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv_Main,
sum(Bus_Plan_OffreefAdv) Bus_Plan_OffreefAdv_Main,
Sum(isnull(UraniumKg,0)) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*100)/(Sum(UraniumReefSQMDens)))
	end 
from
(select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
0 ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(MetresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
convert(Numeric(10,1), round(Sum(ConvertedOn_M),1)) ConvertedOn_M,
convert(Numeric(10,1), round(Sum(ConvertedOff_M),1)) ConvertedOff_M,
min(DrillRig) DrillRig,
Sum(Tons) Tons,
Sum(OnReefTons) OnReefTons,
Sum(OffReefTons) OffReefTons,
--Sum(Volume) Volume,
--Sum(OnReefVolume) OnReefVolume,
--Sum(OffReefVolume) OffReefVolume,
Grams = sum(Grams),
OnReefSQM = Sum(OnReefSQM),
CMGTGramsCalc = Sum(CMGTGramsCalc), 
Avg(FL) faceLength,
Avg(density) density,
Avg(ROCKDENSITY) ROCKDENSITY,
Avg(goldgramsperton) goldgramsperton,
Avg(stopewidth) stopewidth, 
0 PayLimit, 
Avg(labourstrength) labourstrength,
gt = Case when (Sum(OnReefTons) = 0) or (Sum(OnReefTons) is Null) then 0 else
  convert(Numeric(10,1), round(Sum(Grams)/Sum(OnReefTons),1))
end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
Kg = convert(numeric(10,3),round(sum(CMGTGramsCalc)/1000,3)),
CubicMetres = sum(CubicMetres), 
CubicTons = sum(CubicTons), 
CubicGrams = sum(CubicGrams), 
UraniumKg = SUM(UraniumKg), 
UraniumReefSQM = SUM(UraniumReefSQM),
UraniumReefSQMDens = SUM(UraniumReefSQMDens),
CubicGT = sum(CubicGT),
Avg(Bus_Plan_Adv) Bus_Plan_Adv,
Avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
Avg(Bus_Plan_Adv)-Avg(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
Avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
Avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
Avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
Isnull(Avg(DevCubics),0) Bus_Plan_DevCubics,
Isnull(Avg(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
 from
(Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection,  
'''' Prim,
a.OrgUnitday ,
a.OrgUnitAfterNoon ,
a.OrgUnitNight ,
'''' remarks,
a.MetresAdvance MetresAdvance,
a.ReefAdv OnReefAdv,
a.WasteAdv OffReefAdv,
a.FL,
w.density,
s.ROCKDENSITY,
gt goldgramsperton,
a.SW stopewidth, 
0 PayLimit, 
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  (a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then (a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then (a.CUBICMETRES/10) else 0 end,
(DrillRig) DrillRig,
(a.Tons) Tons,
(a.ReefTons) OnReefTons,
(a.WasteTons) OffReefTons,
--(a.Volume) Volume,
--(a.OnReefVolume) OnReefVolume,
--(a.OffReefVolume) OffReefVolume,
Grams = (a.gt*a.ReefTons),
(a.ReefAdv*a.fl) OnReefSQM,
(a.sw) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when (w.Density) is null or (w.Density) = 0 then
(a.cmgt*(a.MetresAdvance*c.endheight)*s.RockDensity/100)
else
(a.cmgt*(a.MetresAdvance*c.endheight)*w.Density/100) end,
(convert(numeric(6,2), labourstrength)) labourstrength, 
(a.Cubicmetres) Cubicmetres,
(a.CubicsTons) CubicTons,
(a.CubicGrams) CubicGrams,
(a.CubicGT) CubicGT,
	UraniumKg = (a.cmkgt*(a.ReefAdv*a.fl)*w.density/100), 
    UraniumReefSQM = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl) else 0 end),
    UraniumReefSQMDens = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl)*w.Density else 0 end), 
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 DevCubics,
0 Bus_Plan_Cap_Adv ,
0 Bus_Plan_Adv_Sec,
0 Bus_Plan_OnreefAdv_Sec,
0 Bus_Plan_OffreefAdv_Sec '


Set @SQL2 = ' from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid 
left outer join workplace w on 
a.workplaceid = w.workplaceid 
  --  left join
  --  (select 
		--Prodmonth,
		--Sectionid_2,
		--Sum(SQM) SQM,
		--Sum(Kg) Kg,
		--Sum(Cubics) Cubics,
		--Sum(Metresadvance) Metresadvance,
		--Sum(MetresReef) MetresReef,
		--Sum(MetresWaste) MetresWaste,
		--Sum(DevCap) DevCap,
		--Sum(DevSec) DevSec,
		--Sum(DevSecReef) DevSecReef
		-- from PLANMONTH_OPS
  --   where PRODMONTH ='+@Prodmonth+'
		--group by Prodmonth,
		--Sectionid_2) BusPlan on
  --   b.SECTIONID_2 = BusPlan.SECTIONID_2,

, Sysset s
where a.prodmonth = '+@Prodmonth+'
and a.activity in (1,7)  and a.PlanCode = ''MP'''
 

if @ShowAuth <> 'Y'
Begin
Set @SQL2 = @SQL2+' 
 and a.Locked = 1'
END

Set @SQL2 = @SQL2+' and '+@SectionLevel+' = '''+@Section+''') CorrectDetail
group by GroupSec, SelectSection ) LastSum
group by GroupSec
With Rollup'

Exec(@SQL1+@SQL2)

GO

-- [SP_Dynamic_Planning_Stope_Summary] '201701', '1.1 - A Dube', 6, 'Y'
ALTER Procedure [dbo].[SP_Dynamic_Planning_Stope_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int,
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(max),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'

	  If @SumLevel = 6
    set @GroupLevel = 'NAME'
    
  If @TheLevel = 1
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  

	  If @TheLevel = 6
    set @SectionLevel = 'NAME' 
    

Set @SQL1 = 'Select GroupSec,  Sum(SquareMetres) SquareMetres,  Sum(OnReefSQM) OnReefSQM,
  Sum(OffReefSQM) OffReefSQM,  Sum(TargetSQM) TargetSQM,  
  convert(int,round(Sum(FaceLength),0)) FaceLength,  
  convert(int,round(Sum(OnReefFL),0)) OnReefFL,  convert(int,round(Sum(OffReefFL),0)) OffReefFL,  
  FaceAdv = Case when Sum(FaceLength) = 0 then 0 else  convert(Numeric(10,1),Sum(SquareMetres)/Sum(FaceLength)) end,  
  OnreefAdv = Case when Sum(OnreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OnreefSQM)/Sum(OnreefFl)) end,  
  OffreefAdv = Case when Sum(OffreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OffreefSQM)/Sum(OffreefFl)) end,  
  CW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))  end,  
  CTons = convert(int,round(Sum(Ctons),0)),  SW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))  end,  
  ISW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))  end, 
  Tons = convert(int,round(Sum(tons),0)),  OnReefTons = convert(int,round(Sum(OnReeftons),0)),  
  OffReefTons = convert(int,round(Sum(OffReeftons),0)),  Volume = convert(int,round(Sum(Volume),0)),  
  OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),  OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),  
  [g/t] =  case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else  Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))  end,  
  convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,  
	Cmgt =
	Case when
	  (Sum(OnreefSQMDens) = 0) or (Sum(OnreefSQMDens) is null) then 0 else
	convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
	end,  
  PaySQM = Sum(PaySQM),  UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),  PayTons = Sum(PayTons),  UnPayTons = Sum(Tons)-Sum(PayTons),  
  convert(int,Round(avg(PayLimit),0)) ShaftPayLimit,   cubicmetres = Sum(cubicmetres),  CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),  
  CubicGrams = Convert(Numeric(7,3),Round(Sum(CubicGrams)/1000,3)),  
  CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
  
  SUM(Bus_Plan_SQM) Bus_Plan_SQM,
  convert(Numeric(10,3),round(Sum(Bus_Plan_Grams)/1000,3)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_Grams)*100)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end,
  Sum(UraniumKg) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*10000)/(Sum(UraniumReefSQMDens)))
	end 
  from 
  (select  
    SelectSection, 
    GroupSec,
    SUM(SquareMetres) SquareMetres,
    SUM(OffReefSQM) OffReefSQM,  
    SUM(TargetSQM) TargetSQM, 
    SUM(MetresAdvance) MetresAdvance,   
    SUM(OnReefAdv) OnReefAdv,   
    SUM(OffReefAdv) OffReefAdv,   
    SUM(FL) FaceLength,   
    SUM(FL) OnReefFL,   
    SUM(OffReefFL) OffReefFL,   
    SUM(CWSQM) CWSQM,  
    SUM(CTons) CTons,  
    SUM(SWSQM) SWSQM,  
    SUM(ISWSQM) ISWSQM,  
    SUM(Volume) Volume,  
    SUM(OnReefVolume) OnReefVolume,  
    SUM(OffReefVolume) OffReefVolume,  
    SUM(Tons) Tons,  
    SUM(ReefTons) ReefTons,  
    SUM(OnReefTons) OnReefTons,  
    SUM(OffReefTons) OffReefTons,  
    SUM(OnreefSQMDens) OnreefSQMDens,  
    SUM(Grams) Grams,  
    SUM(Kg) Kg,  
    SUM(PaySQM) PaySQM,  
    SUM(PayTons) PayTons,  
    SUM(OnreefSQM) OnreefSQM,  
    avg(RockDensity) RockDensity,
    avg(PayLimit) PayLimit,  
    SUM(cubicmetres) cubicmetres,  
    SUM(CubicTons) CubicTons,
    SUM(CubicGrams) CubicGrams,   
    SUM(CubicGT) CubicGT,
    SUM(UraniumKg) UraniumKg, 
    SUM(UraniumReefSQM) UraniumReefSQM,
    SUM(UraniumReefSQMDens) UraniumReefSQMDens,
    AVG(Bus_Plan_SQM) Bus_Plan_SQM,
    AVG(Bus_Plan_Grams) Bus_Plan_Grams
   FROM
  (Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection, 
    Convert(Numeric(7),a.SQM ) SquareMetres,
    Convert(Numeric(7),a.wastesqm) OffReefSQM,  
    0 TargetSQM, 
    a.MetresAdvance MetresAdvance,   
    0 OnReefAdv,   
    0 OffReefAdv,   
    a.FL,   
    a.FL OnReefFL,   
    0 OffReefFL,   
    CWSQM = a.SQM*a.CW,  
    CTons = a.SQM*a.CW*w.Density/100,  
    SWSQM = a.SQM*a.SW,  
    ISWSQM = a.SQM*sm.SWidth,  
    Volume = 0,  OnReefVolume = 0,  
    OffReefVolume = 0,  
    Tons = Convert(Numeric(7),Round(a.SQM*a.SW/100*w.density,0)),  
    ReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OnReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OffReefTons = Convert(Numeric(7),Round(a.WasteSQM*a.SW/100*w.density,0)), 
    OnreefSQMDens = a.ReefSQM*w.Density,  
    Grams = a.cmgt*a.ReefSQM*w.density/100,  
    Kg = convert(numeric(10,3),Round(a.cmgt*a.ReefSQM*w.Density/100,0)/100),  
    0 PaySQM,  
    0 PayTons, 
	UraniumKg = a.cmkgt*a.ReefSQM*w.density/10000, 
    UraniumReefSQM = Case when a.cmkgt > 0 then ReefSQM else 0 end,
    UraniumReefSQMDens = Case when a.cmkgt > 0 then ReefSQM*w.Density else 0 end, 
    Convert(Numeric(7),a.ReefSQM) OnreefSQM,  
    s.RockDensity,  0 PayLimit,  
    Convert(Numeric(7),a.cubicmetres) cubicmetres,  
    Convert(Numeric(7),Round(a.cubicmetres*w.density,0)) CubicTons,
    0 CubicGrams,   
    0 CubicGT,
    
    0 Bus_Plan_SQM,
    0 Bus_Plan_Grams
        
    from Planmonth a 
    inner join section_complete b 
    on  a.prodmonth = b.prodmonth and  
    a.sectionid = b.sectionid 
    inner join workplace c on  
    a.workplaceid = c.workplaceid 
    inner join seccal d on  
    b.prodmonth = d.prodmonth and  
    b.sectionid_1 = d.sectionid 
    --left outer join shaft_paylimits e on  
    --a.prodmonth = e.prodmonth and  c.OreFlowID  = e.OreFlowID 
    inner join  workplace w on a.workplaceid = w.workplaceid  
    left join sampling sm on -- a.Prodmonth = sm.prodmonth and  
    a.workplaceid = sm.workplaceid
    
    --left join
    --(select * from MONTH_CORP_PLAN
    -- where PRODMONTH =  '+@Prodmonth+' 
    -- and Activity = 0) BusPlan on
    -- b.Name_2 = BusPlan.Name_2

    , Sysset s  
    where a.prodmonth =  '+@Prodmonth+'   
    and a.activity in (0,3)   and b.'+@SectionLevel+' = '''+@Section+''' and a.PlanCode = ''MP'''
 

	if @ShowAuth <> 'Y'
	Begin
	Set @SQL1 = @SQL1+' 
	 and a.Locked = 1'
	END

	Set @SQL1 = @SQL1+' 

    ) DetailCorrection
    Group by GroupSec, SelectSection 
    
    ) LastSum
    group by GroupSec
	with rollup'

--Select @SQL1

Exec(@SQL1)

GO

-- [SP_Lock_Planning_Dev_Summary] '201701', '1.5 - T Cwati', 6, 'Y'
ALTER Procedure [dbo].[SP_Lock_Planning_Dev_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int,
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(8000),
@SQL2 Varchar(8000),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'

	  If @SumLevel = 6
    set @GroupLevel = 'NAME'
    
  If @TheLevel = 1 
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  

	  If @TheLevel = 6
    set @SectionLevel = 'NAME' 
    

Set @SQL1 = 'Select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
'''' remarks,
Sum(metresAdvance+Converted_M) TotalAdvance,
sum(MetresAdvance) MetresAdvance,
sum(OnReefAdv) OnReefAdv,
sum(OffReefAdv) OffReefAdv,
sum(SecTot_M) SecTot_M,
sum(SecOn_M) SecOn_M,
sum(SecOff_M) SecOff_M,
sum(MainTot_M) MainTot_M,
sum(MainOn_M) MainOn_M,
sum(MainOff_M) MainOff_M,
Sum(Capital_M) Capital_M,
Sum(CapitalOn_M) CapitalOn_M,
Sum(CapitalOff_M) CapitalOff_M,
Sum(Converted_M) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
max(DrillRig) DrillRig,
sum(Tons) Tons,
sum(OnReefTons) OnReefTons,
sum(OffReefTons) OffReefTons,
--sum(Volume) Volume,
--sum(OnReefVolume) OnReefVolume,
--sum(OffReefVolume) OffReefVolume,
Grams = sum(Grams),
KG = convert(numeric(10,3), sum(Grams)/1000),
sum(OnReefAdv*Facelength) OnReefSQM,
avg(stopewidth) Height,
0 ShaftPayLimit,
convert(Numeric(10,3),round(Sum(CMGTGramsCalc),1)) Grams,
CMGTGramsCalc = case when avg(Density) is null or avg(Density) = 0 then
sum(cmgt*(MetresAdvance*Facelength)*RockDensity/100)
else
sum(cmgt*(MetresAdvance*Facelength)*Density/100) end,
gt = case when Sum(OnReefTons) is null or Sum(OnReefTons) = 0 then
0
else
sum(Grams)/Sum(OnReefTons) end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
avg(convert(numeric(6,2), labourstrength)) labourstrength, 
sum(Cubicmetres) Cubicmetres,
sum(CubicTons) CubicTons,
sum(CubicGrams) CubicGrams,
sum(CubicGT) CubicGT,
sum(Bus_Plan_DevCubics/10)+sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec)+sum(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
sum(Bus_Plan_Adv)+sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv,
sum(Bus_Plan_OnreefAdv)+sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv,
sum(Bus_Plan_OffreefAdv)+sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv,
sum(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
sum(Bus_Plan_Cap_Adv) Bus_Plan_Cap_Adv,
sum(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
sum(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
sum(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
sum(Bus_Plan_Adv) Bus_Plan_Adv_Main,
sum(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv_Main,
sum(Bus_Plan_OffreefAdv) Bus_Plan_OffreefAdv_Main,
  Sum(isnull(UraniumKg,0)) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*100)/(Sum(UraniumReefSQMDens)))
	end 
from
(select GroupSec,
Max(Prim) Prim,
max(OrgUnitday) OrgUnitday,
max(OrgUnitAfterNoon) OrgUnitAfterNoon,
max(OrgUnitNight) OrgUnitNight,
0 ShaftPayLimit,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
convert(Numeric(10,1), round(Sum(MetresAdvance),1)) MetresAdvance,
convert(Numeric(10,1), round(Sum(OnReefAdv),1)) OnReefAdv,
convert(Numeric(10,1), round(Sum(OffReefAdv),1)) OffReefAdv,
convert(Numeric(10,1), round(Sum(SecTot_M),1)) SecTot_M,
convert(Numeric(10,1), round(Sum(SecOn_M),1)) SecOn_M,
convert(Numeric(10,1), round(Sum(SecOff_M),1)) SecOff_M,
convert(Numeric(10,1), round(Sum(MainTot_M),1)) MainTot_M,
convert(Numeric(10,1), round(Sum(MainOn_M),1)) MainOn_M,
convert(Numeric(10,1), round(Sum(MainOff_M),1)) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
convert(Numeric(10,1), round(Sum(ConvertedOn_M),1)) ConvertedOn_M,
convert(Numeric(10,1), round(Sum(ConvertedOff_M),1)) ConvertedOff_M,
min(DrillRig) DrillRig,
Sum(Tons) Tons,
Sum(OnReefTons) OnReefTons,
Sum(OffReefTons) OffReefTons,
--Sum(Volume) Volume,
--Sum(OnReefVolume) OnReefVolume,
--Sum(OffReefVolume) OffReefVolume,
Grams = sum(Grams),
OnReefSQM = Sum(OnReefSQM),
CMGTGramsCalc = Sum(CMGTGramsCalc), 
Avg(FL) faceLength,
Avg(density) density,
Avg(ROCKDENSITY) ROCKDENSITY,
Avg(goldgramsperton) goldgramsperton,
Avg(stopewidth) stopewidth, 
0 PayLimit, 
Avg(labourstrength) labourstrength,
gt = Case when (Sum(OnReefTons) = 0) or (Sum(OnReefTons) is Null) then 0 else
  convert(Numeric(10,1), round(Sum(Grams)/Sum(OnReefTons),1))
end,
cmgt = case when Sum(OnReefSQM) = 0 then 0 else
Sum(CMGTGramsCalc*100)/Sum(OnReefSQM) end,
Kg = convert(numeric(10,3),round(sum(CMGTGramsCalc)/1000,3)),
CubicMetres = sum(CubicMetres), 
CubicTons = sum(CubicTons), 
CubicGrams = sum(CubicGrams), 
UraniumKg = SUM(UraniumKg), 
UraniumReefSQM = SUM(UraniumReefSQM),
UraniumReefSQMDens = SUM(UraniumReefSQMDens),
CubicGT = sum(CubicGT),
Avg(Bus_Plan_Adv) Bus_Plan_Adv,
Avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
Avg(Bus_Plan_Adv)-Avg(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
Avg(Bus_Plan_Adv_Sec) Bus_Plan_Adv_Sec,
Avg(Bus_Plan_OnreefAdv_Sec) Bus_Plan_OnreefAdv_Sec,
Avg(Bus_Plan_OffreefAdv_Sec) Bus_Plan_OffreefAdv_Sec,
Isnull(Avg(DevCubics),0) Bus_Plan_DevCubics,
Isnull(Avg(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
 from
(Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection,  
''''Prim,
a.OrgUnitday ,
a.OrgUnitAfterNoon ,
a.OrgUnitNight ,
'''' remarks,
a.MetresAdvance MetresAdvance,
a.ReefAdv OnReefAdv,
a.WasteAdv OffReefAdv,
a.FL,
w.density,
s.ROCKDENSITY,
gt goldgramsperton,
a.SW stopewidth, 
0 PayLimit, 
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  (a.CUBICMETRES/10),
ConvertedOn_M = case when c.REEFWASTE in (0,3) then (a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.REEFWASTE in (1,2) then (a.CUBICMETRES/10) else 0 end,
(DrillRig) DrillRig,
(a.Tons) Tons,
(a.ReefTons) OnReefTons,
(a.WasteTons) OffReefTons,
--(a.Volume) Volume,
--(a.OnReefVolume) OnReefVolume,
--(a.OffReefVolume) OffReefVolume,
Grams = (a.gt*a.ReefTons),
(a.ReefAdv*a.fl) OnReefSQM,
(a.sw) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when (w.Density) is null or (w.Density) = 0 then
(a.cmgt*(a.MetresAdvance*c.endheight)*s.RockDensity/100)
else
(a.cmgt*(a.MetresAdvance*c.endheight)*w.Density/100) end,
(convert(numeric(6,2), labourstrength)) labourstrength, 
(a.Cubicmetres) Cubicmetres,
(a.CubicsTons) CubicTons,
(a.CubicGrams) CubicGrams,
(a.CubicGT) CubicGT,
	UraniumKg = (a.cmkgt*(a.ReefAdv*a.fl)*w.density/100), 
    UraniumReefSQM = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl) else 0 end),
    UraniumReefSQMDens = (Case when a.cmkgt > 0 then (a.ReefAdv*a.fl)*w.Density else 0 end), 
0 Bus_Plan_Adv,
0 Bus_Plan_OnreefAdv,
0 DevCubics,
0 Bus_Plan_Cap_Adv ,
0 Bus_Plan_Adv_Sec,
0 Bus_Plan_OnreefAdv_Sec,
0 Bus_Plan_OffreefAdv_Sec ' 
Set @SQL2 = ' from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.oreflowid = e.oreflowid 
left outer join workplace w on 
a.workplaceid = w.workplaceid 
  --  left join
  --  (select 
		--Prodmonth,
		--Sectionid_2,
		--Sum(SQM) SQM,
		--Sum(Kg) Kg,
		--Sum(Cubics) Cubics,
		--Sum(Metresadvance) Metresadvance,
		--Sum(MetresReef) MetresReef,
		--Sum(MetresWaste) MetresWaste,
		--Sum(DevCap) DevCap,
		--Sum(DevSec) DevSec,
		--Sum(DevSecReef) DevSecReef
		-- from PLANMONTH_OPS
  --   where PRODMONTH ='+@Prodmonth+'
		--group by Prodmonth,
		--Sectionid_2) BusPlan on
  --   b.SECTIONID_2 = BusPlan.SECTIONID_2,

, Sysset s
where a.prodmonth = '+@Prodmonth+'
and a.activity in (1,7)  and a.PlanCode = ''LP''  and a.Locked = 1
and '+@SectionLevel+' = '''+@Section+''') CorrectDetail
group by GroupSec, SelectSection ) LastSum
group by GroupSec
with rollup'


--select 1, @SQL1
--union
--select 2, @SQL2

Exec(@SQL1+@SQL2)
GO

-- [SP_Lock_Planning_Stope_Summary] '201701', '1.1 - A Dube', 6, 'Y'
ALTER Procedure [dbo].[SP_Lock_Planning_Stope_Summary]
@Prodmonth varchar(6),
@Section VarChar(50),
@SumLevel int,
@ShowAuth VarChar(5) = 'Y'

AS
Declare 
@TheLevel Int,
@SQL1 Varchar(8000),
@GroupLevel Varchar(20),
@SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'

	  If @SumLevel = 6
    set @GroupLevel = 'NAME'
    
  If @TheLevel = 1 
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  

	  If @TheLevel = 6
    set @SectionLevel = 'NAME'  
    

Set @SQL1 = 'Select GroupSec,  Sum(SquareMetres) SquareMetres,  Sum(OnReefSQM) OnReefSQM,
  Sum(OffReefSQM) OffReefSQM,  Sum(TargetSQM) TargetSQM,  
  convert(int,round(Sum(FaceLength),0)) FaceLength,  
  convert(int,round(Sum(OnReefFL),0)) OnReefFL,  convert(int,round(Sum(OffReefFL),0)) OffReefFL,  
  FaceAdv = Case when Sum(FaceLength) = 0 then 0 else  convert(Numeric(10,1),Sum(SquareMetres)/Sum(FaceLength)) end,  
  OnreefAdv = Case when Sum(OnreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OnreefSQM)/Sum(OnreefFl)) end,  
  OffreefAdv = Case when Sum(OffreefFl) = 0 then 0 else  convert(Numeric(10,1),Sum(OffreefSQM)/Sum(OffreefFl)) end,  
  CW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(CWSQM)/Sum(SquareMetres),0))  end,  
  CTons = convert(int,round(Sum(Ctons),0)),  SW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    
  convert(int,round(Sum(SWSQM)/Sum(SquareMetres),0))  end,  
  ISW =    Case When (Sum(SquareMetres) = 0) or (Sum(SquareMetres) is null) then 0 else    convert(int,round(Sum(ISWSQM)/Sum(SquareMetres),0))  end, 
  Tons = convert(int,round(Sum(tons),0)),  OnReefTons = convert(int,round(Sum(OnReeftons),0)),  
  OffReefTons = convert(int,round(Sum(OffReeftons),0)),  Volume = convert(int,round(Sum(Volume),0)),  
  OnReefVolume = convert(int,round(Sum(OnReefVolume),0)),  OffReefVolume = convert(int,round(Sum(OffReefVolume),0)),  
  [g/t] =  case when (Sum(Reeftons) = 0) or (Sum(Reeftons) is null) then 0 else  Convert(Numeric(7,1),Round(Sum(Grams)/Sum(Reeftons),1))  end,  
  convert(Numeric(10,3),round(Sum(Grams)/1000,3)) Kg,  
	Cmgt =
	Case when
	  (Sum(OnreefSQM) = 0) or (Sum(OnreefSQM) is null) then 0 else
	convert(int,(Sum(Grams)*100)/(Sum(OnreefSQMDens)))
	end,  
  PaySQM = Sum(PaySQM),  UnPaySQM = Sum(SquareMetres)-Sum(PaySQM),  PayTons = Sum(PayTons),  UnPayTons = Sum(Tons)-Sum(PayTons),  
  convert(int,Round(avg(PayLimit),0)) ShaftPayLimit,   cubicmetres = Sum(cubicmetres),  CubicTons = Convert(Numeric(7),Round(Sum(CubicTons),0)),  
  CubicGrams = Convert(Numeric(7,3),Round(Sum(CubicGrams)/1000,3)),  
  CubicGT = case when Sum(CubicTons) = 0 then 0  else Convert(Numeric(7,1),Round(Sum(CubicGrams)/Sum(CubicTons),1)) end,
  
  SUM(Bus_Plan_SQM) Bus_Plan_SQM,
  convert(Numeric(10,3),round(Sum(Bus_Plan_Grams)/1000,3)) Bus_Plan_KG,
  Bus_Plan_Cmgt =  Case when    (Sum(Bus_Plan_SQM) = 0) or (Sum(Bus_Plan_SQM) is null) then
   0 else  
   convert(int,(Sum(Bus_Plan_Grams)*100)/(Sum(Bus_Plan_SQM)*avg(RockDensity)))  end ,
  Sum(UraniumKg) UraniumKG,
	Cmkgt =
	Case when
	  (Sum(UraniumReefSQMDens) = 0) or (Sum(UraniumReefSQMDens) is null) then 0 else
	convert(int,(Sum(UraniumKg)*10000)/(Sum(UraniumReefSQMDens)))
	end 
  from 
  (select  
    SelectSection, 
    GroupSec,
    SUM(SquareMetres) SquareMetres,
    SUM(OffReefSQM) OffReefSQM,  
    SUM(TargetSQM) TargetSQM, 
    SUM(MetresAdvance) MetresAdvance,   
    SUM(OnReefAdv) OnReefAdv,   
    SUM(OffReefAdv) OffReefAdv,   
    SUM(FL) FaceLength,   
    SUM(FL) OnReefFL,   
    SUM(OffReefFL) OffReefFL,   
    SUM(CWSQM) CWSQM,  
    SUM(CTons) CTons,  
    SUM(SWSQM) SWSQM,  
    SUM(ISWSQM) ISWSQM,  
    SUM(Volume) Volume,  
    SUM(OnReefVolume) OnReefVolume,  
    SUM(OffReefVolume) OffReefVolume,  
    SUM(Tons) Tons,  
    SUM(ReefTons) ReefTons,  
    SUM(OnReefTons) OnReefTons,  
    SUM(OffReefTons) OffReefTons,  
    SUM(OnreefSQMDens) OnreefSQMDens,  
    SUM(Grams) Grams,  
    SUM(Kg) Kg,  
    SUM(PaySQM) PaySQM,  
    SUM(PayTons) PayTons,  
    SUM(OnreefSQM) OnreefSQM,  
    avg(RockDensity) RockDensity,
    avg(PayLimit) PayLimit,  
    SUM(cubicmetres) cubicmetres,  
    SUM(CubicTons) CubicTons,
    SUM(CubicGrams) CubicGrams,   
    SUM(CubicGT) CubicGT,
    SUM(UraniumKg) UraniumKg, 
    SUM(UraniumReefSQM) UraniumReefSQM,
    SUM(UraniumReefSQMDens) UraniumReefSQMDens,
    AVG(Bus_Plan_SQM) Bus_Plan_SQM,
    AVG(Bus_Plan_Grams) Bus_Plan_Grams
   FROM
  (Select B.'+@GroupLevel+' GroupSec, B.Name_2 SelectSection, 
    Convert(Numeric(7),a.SQM ) SquareMetres,
    Convert(Numeric(7),a.wastesqm) OffReefSQM,  
    0 TargetSQM, 
    a.MetresAdvance MetresAdvance,   
    0 OnReefAdv,   
    0 OffReefAdv,   
    a.FL,   
    a.FL OnReefFL,   
    0 OffReefFL,   
    CWSQM = a.SQM*a.CW,  
    CTons = a.SQM*a.CW*w.Density/100,  
    SWSQM = a.SQM*a.SW,  
    ISWSQM = a.SQM*sm.SWidth,  
    Volume = 0,  OnReefVolume = 0,  
    OffReefVolume = 0,  
    Tons = Convert(Numeric(7),Round(a.SQM*a.SW/100*w.density,0)),  
    ReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OnReefTons = Convert(Numeric(7),Round(a.ReefSQM*a.SW/100*w.density,0)),  
    OffReefTons = Convert(Numeric(7),Round(a.WasteSQM*a.SW/100*w.density,0)), 
    OnreefSQMDens = a.ReefSQM*w.Density,  
    Grams = a.cmgt*a.ReefSQM*w.density/100,  
    Kg = convert(numeric(10,3),Round(a.cmgt*a.ReefSQM*w.Density/100,0)/100),  
    0 PaySQM,  
    0 PayTons,  
    Convert(Numeric(7),a.ReefSQM) OnreefSQM,  
    s.RockDensity,  0 PayLimit,  
    Convert(Numeric(7),a.cubicmetres) cubicmetres,  
    Convert(Numeric(7),Round(a.cubicmetres*w.density,0)) CubicTons,
    0 CubicGrams,   
    0 CubicGT,
    UraniumKg = a.cmkgt*a.ReefSQM*w.density/100000, 
    UraniumReefSQM = Case when a.cmkgt > 0 then ReefSQM else 0 end,
    UraniumReefSQMDens = Case when a.cmkgt > 0 then ReefSQM*w.Density else 0 end, 
    0 Bus_Plan_SQM,
    0 Bus_Plan_Grams
        
    from Planmonth a 
    inner join section_complete b 
    on  a.prodmonth = b.prodmonth and  
    a.sectionid = b.sectionid 
    inner join workplace c on  
    a.workplaceid = c.workplaceid 
    inner join seccal d on  
    b.prodmonth = d.prodmonth and  
    b.sectionid_1 = d.sectionid 
    --left outer join shaft_paylimits e on  
    --a.prodmonth = e.prodmonth and  c.OreFlowID  = e.OreFlowID 
    inner join  workplace w on a.workplaceid = w.workplaceid  
    left join sampling sm on -- a.Prodmonth = sm.prodmonth and  
    a.workplaceid = sm.workplaceid
    
    --left join
    --(select * from MONTH_CORP_PLAN
    -- where PRODMONTH =  '+@Prodmonth+' 
    -- and Activity = 0) BusPlan on
    -- b.Name_2 = BusPlan.Name_2

    , Sysset s  
    where a.prodmonth =  '+@Prodmonth+'   
    and a.activity in (0,3)   and b.'+@SectionLevel+' = '''+@Section+''' and a.PlanCode = ''LP''  and a.Locked = 1
 
) DetailCorrection
    Group by GroupSec, SelectSection 
    
    ) LastSum
    group by GroupSec
	with rollup'

--Select @SQL1

Exec(@SQL1)

GO

-- [SP_Manage_CalcTypeItem] 'Percy', 'Percy'
ALTER PROCEDURE [dbo].[SP_Manage_CalcTypeItem]
@CalendarCode varchar(10),@CalType varchar(50)

as

declare @countItem int

set @countItem = ( SELECT count(CalendarCode) FROM [CODE_CALENDAR] WHERE CalendarCode = @CalendarCode)

if @countItem = 1
begin
UPDATE [dbo].[CODE_CALENDAR]
   SET [Description] = @CalType
      
 WHERE CalendarCode = @CalendarCode
end
else
begin
INSERT INTO [dbo].[CODE_CALENDAR]
           ([CalendarCode]
           ,[Description])
     VALUES
           (@CalendarCode 
           ,@CalType)
end
GO

-- [SP_MODaily_Dev_Report] '201701', 'REH', '2017-01-12'
ALTER Procedure [dbo].[SP_MODaily_Dev_Report]
@Prodmonth varchar(6),
@Section VarChar(50),
@TheDate DateTime

AS
Select  Name_2, Name, Description, 
ShiftDay,
Max(Calendardate) Calendardate,
Convert(Numeric(7,1),Avg(Metresadvance)) Metresadvance, 
Convert(Numeric(7),Avg(PlannedStrength)) PlannedStrength, 
Convert(Numeric(7),Avg(Strength)) Strength, 
Convert(Numeric(7),Avg(AvgAtWorkLabour)) AvgAtWorkLabour, 
Convert(Numeric(7),Avg(FL)) FL, 
Convert(Numeric(7),Avg(cmgt)) cmgt, 
Sum(PlanSQM) PlanSQM,
Sum(BookSQM) BookSQM,
Min(NoteID) NoteID,
Min(SBNotes) SBNotes,
Min(Problem) Problem,
Min(Note) Note,
Min(Explanation) Explanation,
Avg(ShiftProgBookAdv) ShiftProgBookAdv,
Avg(NonShiftProgBookAdv) NonShiftProgBookAdv,
Avg(ProgBookAdv) ProgBookAdv,
Avg(ProgPlanAdv) ProgPlanAdv,
Avg(VAriance) VAriance
 from
(
select Name_2 Name_2, Name Name, d.Description, 
Convert(Numeric(7,1),isnull(a.Metresadvance,0)) Metresadvance, 
0 PlannedStrength, 
0 Strength, 
0 AvgAtWorkLabour, 
Convert(Numeric(7),isnull(a.FL,0)) FL, 
Convert(Numeric(7),isnull(a.cmgt,0)) cmgt, 
Calendardate = Case when ShiftDay = 0 then s.EndDate+1 else c.Calendardate end,
c.ShiftDay,
Convert(Numeric(7,1),isnull(c.MetresAdvance,0)) PlanSQM,
Convert(Numeric(7,1),isnull(c.BookMetresAdvance,0)) BookSQM,
'' NoteID,
'' SBNotes,
'' Problem,
'' Note,
'' Explanation,
ShiftProgBookAdv,
NonShiftProgBookAdv,
ProgBookAdv,
ProgPlanAdv,
VAriance
 from planmonth a 
 inner join SECTION_COMPLETE b on
a.Prodmonth = b.PRODMONTH and
a.Sectionid = b.SECTIONID
inner join SECCAL s on
b.Prodmonth = s.PRODMONTH and
b.SECTIONID_1 = s.SECTIONID
inner join PLANNING c on
a.Prodmonth = c.PRODMONTH and
a.Sectionid = c.SECTIONID and
a.Workplaceid = c.WorkplaceID and
a.Activity = c.Activity and
a.PlanCode = c.PlanCode
Inner join WORKPLACE d on
a.Workplaceid = d.WorkplaceID
--left join vw_crewinfo e on
--a.Prodmonth = e.PRODMONTH and
--a.Sectionid = e.SECTIONID and
--a.Workplaceid = e.WorkplaceID and
--a.Activity = e.Activity and
--a.iscubics = e.Iscubics
--left join BOOK_PROBLEM pr on
--c.Prodmonth = pr.PRODMONTH and
--c.Sectionid = pr.SECTIONID and
--c.Workplaceid = pr.WorkplaceID and
--c.Activity = pr.Activity and
--c.iscubics = pr.Iscubics and
--c.Calendardate = pr.Calendardate
inner join (select 
p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity,
isnull(sum(Metresadvance),0) ProgPlanAdv,
sum(case when ShiftDay <> 0 then isnull(bookMetresadvance,0) else 0 end) ShiftProgBookAdv,
sum(case when ShiftDay = 0 then isnull(bookMetresadvance,0) else 0 end) NonShiftProgBookAdv,
isnull(sum(BookMetresadvance),0) ProgBookAdv,
VAriance = isnull(sum(BookMetresadvance),0)- isnull(sum(Metresadvance),0)
from PLanning p where
prodmonth = @Prodmonth
and Activity = 1 and
iscubics = 'N' and 
PlanCode = 'MP'
and Calendardate <= @TheDate
group by p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity) prog on
c.Prodmonth = prog.PRODMONTH and
c.Sectionid = prog.SECTIONID and
c.Workplaceid = prog.WorkplaceID and
c.Activity = prog.Activity
--left join vw_Problem_Complete prc on
--pr.NoteID = prc.noteid and
--pr.Activity = prc.activity
where a.Prodmonth = @Prodmonth
and Sectionid_2 = @Section
and a.PlanCode = 'MP'
--and a.locked = 1
and a.Activity = 1
and c.Calendardate <= @TheDate
) a
group by Name_2, Name, Description, 
ShiftDay

GO

-- [SP_MODailyReport] '201701', 'REA', '2016-12-21'
ALTER Procedure [dbo].[SP_MODailyReport]
@Prodmonth varchar(6),
@Section VarChar(50),
@TheDate DateTime

AS
Select  Name_2, Name, Description, 
ShiftDay,
Max(Calendardate) Calendardate,
Convert(Numeric(7),Avg(SQM)) SQM, 
Convert(Numeric(7),Avg(PlannedStrength)) PlannedStrength, 
Convert(Numeric(7),Avg(Strength)) Strength, 
Convert(Numeric(7),Avg(AvgAtWorkLabour)) AvgAtWorkLabour, 
Convert(Numeric(7),Avg(FL)) FL, 
Convert(Numeric(7),Avg(cmgt)) cmgt, 
Sum(PlanSQM) PlanSQM,
Sum(BookSQM) BookSQM,
Min(NoteID) NoteID,
Min(SBNotes) SBNotes,
Min(Problem) Problem,
Min(Note) Note,
Min(Explanation) Explanation,
Avg(ShiftProgBookSQM) ShiftProgBookSQM,
Avg(NonShiftProgBookSQM) NonShiftProgBookSQM,
Avg(ProgBookSQM) ProgBookSQM,
Avg(ProgPlanSQM) ProgPlanSQM,
Avg(VAriance) VAriance
 from
(
select Name_2 Name_2, Name Name, d.Description, 
Convert(Numeric(7),isnull(a.SQM,0)) SQM, 
0 PlannedStrength, 
0 Strength, 
0 AvgAtWorkLabour, 
Convert(Numeric(7),isnull(a.FL,0)) FL, 
Convert(Numeric(7),isnull(a.cmgt,0)) cmgt, 
Calendardate = Case when ShiftDay = 0 then s.EndDate+1 else c.Calendardate end,
c.ShiftDay,
Convert(Numeric(7),isnull(c.SQM,0)) PlanSQM,
Convert(Numeric(7),isnull(c.BookSQM,0)) BookSQM,
'' NoteID,
'' SBNotes,
'' Problem,
'' Note,
'' Explanation,
ShiftProgBookSQM,
NonShiftProgBookSQM,
ProgBookSQM,
ProgPlanSQM,
VAriance
 from planmonth a 
 inner join SECTION_COMPLETE b on
a.Prodmonth = b.PRODMONTH and
a.Sectionid = b.SECTIONID and
a.plancode = 'MP'
inner join SECCAL s on
b.Prodmonth = s.PRODMONTH and
b.SECTIONID_1 = s.SECTIONID
inner join PLANNING c on
a.Prodmonth = c.PRODMONTH and
a.Sectionid = c.SECTIONID and
a.Workplaceid = c.WorkplaceID and
a.Activity = c.Activity and
a.IsCubics = c.IsCubics and
a.PlanCode = c.PlanCode
Inner join WORKPLACE d on
a.Workplaceid = d.WorkplaceID
--left join vw_crewinfo e on
--a.Prodmonth = e.PRODMONTH and
--a.Sectionid = e.SECTIONID and
--a.Workplaceid = e.WorkplaceID and
--a.Activity = e.Activity and
--a.iscubics = e.Iscubics
--left join BOOK_PROBLEM pr on
--c.Prodmonth = pr.PRODMONTH and
--c.Sectionid = pr.SECTIONID and
--c.Workplaceid = pr.WorkplaceID and
--c.Activity = pr.Activity and
--c.iscubics = pr.Iscubics and
--c.Calendardate = pr.Calendardate
inner join (select 
p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity,
isnull(sum(SQM),0) ProgPlanSQM,
sum(case when ShiftDay <> 0 then isnull(bookSQM,0) else 0 end) ShiftProgBookSQM,
sum(case when ShiftDay = 0 then isnull(bookSQM,0) else 0 end) NonShiftProgBookSQM,
isnull(sum(BookSQM),0) ProgBookSQM,
VAriance = isnull(sum(BookSQM),0)- isnull(sum(SQM),0)
from PLanning p where
prodmonth = @Prodmonth
and Activity = 0 and
iscubics = 'N'
and Plancode = 'MP'
and Calendardate <= @Thedate
group by p.Prodmonth,
p.Sectionid,
p.Workplaceid,
p.Activity) prog on
c.Prodmonth = prog.PRODMONTH and
c.Sectionid = prog.SECTIONID and
c.Workplaceid = prog.WorkplaceID and
c.Activity = prog.Activity
--left join vw_Problem_Complete prc on
--pr.NoteID = prc.noteid and
--pr.Activity = prc.activity
where a.Prodmonth = @Prodmonth
and Sectionid_2 = @Section
and a.PlanCode = 'MP'
and a.locked = 1
and a.Activity = 0
and c.Calendardate <= @Thedate
) a
group by Name_2, Name, Description, 
ShiftDay

GO

-- [sp_PlannedVsBooked_Daily] 'P', '201701', 'S Mofokeng', '2017-01-18', '7', '1', '', '', '', 'Tons'
ALTER procedure [dbo].[sp_PlannedVsBooked_Daily]

--Declare
    @TypeMonth varchar(1),
       @ProdMonth varchar(6),
       @SectionName varchar(60),
       @RunDate varchar(10),
       @SumLevel varchar(1),
       @Activity varchar(1),
       @Account varchar(1),
       @ReefWaste varchar(1),
       @WorkCap varchar(1),
       @Unit varchar(20)
as
       
declare @TheMinDate varchar(10),@TheMaxDate varchar(10)
Declare @TheLevel Int,
    @SQL1 Varchar(8000),
       @SQL4 Varchar(8000),
    @GroupLevel Varchar(20),
    @SectionLevel Varchar(20),
	@GroupCrewLevel Varchar(20),
	 @GroupWPLevel Varchar(20),
    @SectionWPLevel Varchar(20)





select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1'
       set @GroupLevel = 'NAME_5'    
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'      
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4' 
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'
If @SumLevel = '6' 
       set @GroupLevel = 'NAME'

If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END
  
If @TheLevel = 1 
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  

DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)

IF (@TypeMonth = 'P')
BEGIN
       SET @SQL2 =
         ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                       convert(varchar(10), max(c.CalendarDate),120)      
             from planmonth p 
             inner join section_complete sc on 
                    p.prodmonth = sc.prodmonth and
                    p.sectionid = sc.sectionid
             inner join seccal s on
                    sc.prodmonth = s.prodmonth and
                    sc.sectionid_1 = s.sectionid
             inner join CalType c on
                    s.CalendarCode = c.CalendarCode and
                    s.BeginDate <= c.CalendarDate and
                    s.Enddate >= c.CalendarDate
             Where 
                    sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                    p.Prodmonth = '''+ @ProdMonth +''' and 
                    p.Activity = '+ @Activity +' and 
                    p.PlanCode = ''MP'' '
             
             CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
             INSERT #TheMinDate EXEC(@SQL2)
         
             SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
             SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
             DROP TABLE #TheMinDate

       if @TheMinDate is null 
             set @TheMinDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate is null
             set @TheMaxDate = convert(varchar(10), GETDATE (),120)

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
END
ELSE
BEGIN
       select @TheMinDate = Convert(varchar(10),min(StartDate),120),
               @TheMaxDate = Convert(varchar(10),max(EndDate),120)
       from CalendarMill
       where MillMonth = @ProdMonth

       IF @TheMaxDate > @RunDate
             set @TheMaxDate = @RunDate
END



Set @SQL1 = 'select * from (
       Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   
IF @SumLevel = '8'  
BEGIN
	Set @SQL1 = @SQL1 + ' p.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
END
ELSE
BEGIN  
	IF @SumLevel = '7'  
	BEGIN
		Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
	END       
	ELSE
		Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
END		


	   
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL1 = @SQL1 + '
                    Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                    Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL1 = @SQL1 + '      
                    Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL1 = @SQL1 + '      
                           Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
             Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
             Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
             Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL1 = @SQL1 + '
             Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
             Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
END    

Set @SQL1 = @SQL1 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID 
			 inner join workplace w on
			   w.workplaceid = pd.workplaceid
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
			IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL1 = @SQL1 + ''
				   IF (@WorkCap <> '')
						 Set @SQL1 = @SQL1 + ''       
			END
SET @SQL1 = @SQL1 + ') pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate 
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL1 = @SQL1 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL1 = @SQL1 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
			 
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL1 = @SQL1 + ''
       IF (@WorkCap <> '')
             Set @SQL1 = @SQL1 + ''       
END

IF @SumLevel = '8'  
BEGIN
	Set @SQL1 = @SQL1 + '
		   group by ct.CalendarDate, p.'+@GroupCrewLevel+' '
END
ELSE
BEGIN   
	IF @SumLevel = '7'  
	BEGIN
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, w.'+@GroupWPLevel+' '
	END   
	ELSE   
		Set @SQL1 = @SQL1 + '
			   group by ct.CalendarDate, ss.'+@GroupLevel+' '
END


Set @SQL4 = ' union all'
Set @SQL4 = @SQL4+
       ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
IF (@ReefWaste = '0')  --Reef
BEGIN
       IF (@Unit = 'Tons')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Square Metres')
       BEGIN
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
       END
       IF (@Unit = 'Metres Advance')
       BEGIN        
             Set @SQL4 = @SQL4 + '
                    Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                    Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
       END
       IF (@Unit = 'Volume')
       BEGIN        
             Set @SQL4 = @SQL4 + '      
                    Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                    Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
       END
END
ELSE
BEGIN
       IF (@ReefWaste = '1') --Waste
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
              BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                        
       END
       ELSE
       BEGIN
             IF (@Unit = 'Square Metres')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Metres Advance')
             BEGIN        
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                           Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
             END
             IF (@Unit = 'Tons')
             BEGIN
                    Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
             END
             IF (@Unit = 'Volume')
             BEGIN        
                    Set @SQL4 = @SQL4 + '      
                           Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
             END                 
       END
END
IF (@Unit = 'Kilograms')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
             Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Grade')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
             Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
END
IF (@Unit = 'Cubics')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
             Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
END
             
IF (@Unit = 'Blast')
BEGIN
       Set @SQL4 = @SQL4 + '
             Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
             Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
END    

Set @SQL4 = @SQL4 + '
       from planmonth p 
       inner join section_complete ss on
             p.prodmonth = ss.prodmonth and 
             p.sectionid = ss.sectionid
       inner join seccal sc on
             ss.prodmonth = sc.prodmonth and
             ss.sectionid_1 = sc.sectionid
       inner join CalType ct on
             sc.CalendarCode = ct.CalendarCode and
             sc.BeginDate  <= ct.CalendarDate and
             sc.Enddate >= ct.CalendarDate
       --INNER JOIN
       LEFT outer join 
             (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                    pd.ReefSQM Plan_ReefSQM,
                    pd.ReefAdv as Plan_ReefAdv, 
                    pd.ReefTons as Plan_ReefTons, 
                    pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                    pd.WasteSQM  Plan_WasteSQM,
                    pd.WasteAdv  as Plan_WasteAdv, 
                    pd.WasteTons as Plan_WasteTons, 
                    pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                    pd.SQM Plan_SQM,
                    pd.Metresadvance as Plan_Adv, 
                    pd.Tons as Plan_Tons, 
                    (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                    pd.Grams as Plan_Grams, 
                    pd.CubicMetres as Plan_CubicMetres, 
                    Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                    pd.BookReefSQM  as Book_ReefSQM, 
                    pd.BookReefAdv  as Book_ReefAdv, 
                    pd.BookReefTons  as Book_ReefTons, 
                    pd.BookReefVolume  as Book_eefVolume, 

                    pd.BookWasteSQM  as Book_WasteSQM, 
                    pd.BookWasteAdv  as Book_WasteAdv, 
                    pd.BookWasteTons  as Book_WasteTons, 
                    pd.BookWasteVolume as Book_WasteVolume, 

                    pd.BookSQM as Book_SQM, 
                    pd.BookMetresadvance  as Book_Adv, 
                    pd.BookTons as Book_Tons, 
                    pd.BookVolume as Book_Volume,

                    pd.BookGrams as Book_Grams, 
                    BookCubicMetres as Book_CubicMetres, 
                    Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

             from planning pd 
             inner join Section_Complete sc on 
                    sc.ProdMonth = pd.ProdMonth and 
                    sc.SectionID = pd.SectionID
			 inner join Workplace w on
			   w.workplaceid=pd.workplaceid 
             where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
                    sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                    pd.Activity = '+ @Activity +' and 
                    pd.calendardate >='''+@TheMinDate+''' and 
                    pd.calendardate <= '''+@TheMaxDate+''' AND
                    pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
			BEGIN
				   IF (@Account <> '')
						 Set @SQL4 = @SQL4 + ''
				   IF (@WorkCap <> '')
						 Set @SQL4 = @SQL4 + ''       
			END

      Set @SQL4 = @SQL4 + ' ) pd on 
                    p.ProdMonth = pd.ProdMonth and 
                    p.SectionID = pd.SectionID and 
                    p.WorkplaceID = pd.WorkplaceID and 
                    p.Activity = pd.Activity and 
                    ct.CalendarDate = pd.Calendardate and
                    p.PlanCode = pd.PlanCode
       inner join Workplace w on 
             w.WorkplaceID = p.WorkplaceID 
       where '
IF (@TypeMonth = 'P')
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth = '+ @ProdMonth +' and '
END
ELSE
BEGIN
       SET @SQL4 = @SQL4 + '
                    pd.ProdMonth >= '+ @ProdMonth +' and '
END
       SET @SQL4 = @SQL4 + '
             p.Activity = '+ @Activity +' and
             p.PlanCode = ''MP'' AND
             p.plancode <> ''NULL'' and              
             ct.CalendarDate >='''+@TheMinDate+''' and 
             ct.CalendarDate <= '''+@TheMaxDate+''' and
             ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
IF (@Activity = '1')
BEGIN
       IF (@Account <> '')
             Set @SQL4 = @SQL4 + ''
       IF (@WorkCap <> '')
             Set @SQL4 = @SQL4 + ''       
END
             
Set @SQL4 = @SQL4 + '
       group by ct.CalendarDate ) a 
order by a.theSort, a.Section, a.CalendarDate'


EXEC (@SQL1+@SQL4)  

--print @SQL1 
--print @SQL4

GO

-- [sp_PlannedVsBooked_Prog] 'P', '201701', 'S Mofokeng', '2017-01-18', '7', '1', '', '', '', 'Tons'
ALTER procedure [dbo].[sp_PlannedVsBooked_Prog]

--Declare
    @TypeMonth varchar(1),
       @ProdMonth varchar(6),
       @SectionName varchar(60),
       @RunDate varchar(10),
       @SumLevel varchar(1),
       @Activity varchar(1),
       @Account varchar(1),
       @ReefWaste varchar(1),
       @WorkCap varchar(1),
       @Unit varchar(20)
as
       
declare @TheMinDate varchar(10),@TheMaxDate varchar(50)

Declare @TheLevel Int,
    @SQL1 Varchar(8000),
       @SQL4 Varchar(8000),
    @GroupLevel Varchar(20),
    @SectionLevel Varchar(20),
	@GroupWPLevel Varchar(20),
	@GroupCrewLevel Varchar(20)

select @TheLevel = HIERARCHICALID 
from section where PRODMONTH = @ProdMonth and Name = @SectionName

If @SumLevel = '1'
       set @GroupLevel = 'NAME_5'    
If @SumLevel = '2'
       set @GroupLevel = 'NAME_4'      
If @SumLevel = '3'
       set @GroupLevel = 'NAME_3'      
If @SumLevel = '4' 
       set @GroupLevel = 'NAME_2'      
If @SumLevel = '5' 
       set @GroupLevel = 'NAME_1'
If @SumLevel = '6' 
       set @GroupLevel = 'NAME'
If @SumLevel = '7'
BEGIN
       set @GroupLevel = 'NAME'  
	   set @GroupWPLevel = 'Description' 
END 


If @TheLevel = 1
       set @SectionLevel = 'NAME_5'   
If @TheLevel = 2 
       set @SectionLevel = 'NAME_4'      
If @TheLevel = 3 
       set @SectionLevel = 'NAME_3'      
If @TheLevel = 4 
       set @SectionLevel = 'NAME_2'     
If @TheLevel = 5 
       set @SectionLevel = 'NAME_1'     
If @TheLevel = 6 
       set @SectionLevel = 'NAME'  


DECLARE @SQL2 VARCHAR(8000),
           @SQL3 VARCHAR(8000)
IF (@TypeMonth = 'P')
BEGIN
    SET @SQL2 =
        ' SELECT convert(varchar(10), min(c.CalendarDate),120),
                    convert(varchar(10), max(c.CalendarDate),120)    
            from planmonth p 
            inner join section_complete sc on 
                p.prodmonth = sc.prodmonth and
                p.sectionid = sc.sectionid
            inner join seccal s on
                sc.prodmonth = s.prodmonth and
                sc.sectionid_1 = s.sectionid
            inner join CalType c on
                s.CalendarCode = c.CalendarCode and
                s.BeginDate <= c.CalendarDate and
                s.Enddate >= c.CalendarDate
            Where 
                sc.'+ @SectionLevel +'   = '''+ @SectionName + ''' and 
                p.Prodmonth = '''+ @ProdMonth +''' and 
                p.Activity = '+ @Activity +' and 
                p.PlanCode = ''MP'' '
             
            CREATE TABLE #TheMinDate(TheMinDate VARCHAR(10), TheMaxDate VARCHAR(10))
            INSERT #TheMinDate EXEC(@SQL2)
         
            SET @TheMinDate = (SELECT TheMinDate FROM  #TheMinDate)
            SET @TheMaxDate = (SELECT TheMaxDate FROM  #TheMinDate)
            DROP TABLE #TheMinDate

    if @TheMinDate is null 
            set @TheMinDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate is null
            set @TheMaxDate = convert(varchar(10), GETDATE (),120)

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END
ELSE
BEGIN
    select @TheMinDate = Convert(varchar(10),min(StartDate),120),
            @TheMaxDate = Convert(varchar(10),max(EndDate),120)
    from CalendarMill
    where MillMonth = @ProdMonth

    IF @TheMaxDate > @RunDate
            set @TheMaxDate = @RunDate
END

IF (@TypeMonth = 'P')
BEGIN
       Set @SQL1 = 'select * from ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '

	   --ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
	    IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' p.'+ @GroupCrewLevel +' Section, ct.CalendarDate, '
		END
		ELSE
		BEGIN 
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' w.'+ @GroupWPLevel +' Section, ct.CalendarDate, '
			END
			ELSE        
				Set @SQL1 = @SQL1 + ' ss.'+ @GroupLevel +' Section, ct.CalendarDate, '
		END	
                
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL1 = @SQL1 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
               Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL1 = @SQL1 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode, 
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                              w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
					IF (@Activity = '1')
					   BEGIN
								IF (@Account <> '')
										   Set @SQL1 = @SQL1 + ''
								IF (@WorkCap <> '')
										   Set @SQL1 = @SQL1 + ''       
					   END 
                    Set @SQL1 = @SQL1 + ' ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        p.PlanCode = pd.PlanCode and
                                        ct.CalendarDate >= pd.Calendardate
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                            p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ''
                IF (@WorkCap <> '')
                           Set @SQL1 = @SQL1 + ''       
       END
             
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, p.'+ @GroupCrewLevel
		END
		ELSE
		BEGIN     
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, w.'+ @GroupWPLevel
			END
			ELSE    
				Set @SQL1 = @SQL1 + ' group by ct.CalendarDate, ss.'+ @GroupLevel
		END	

              --  group by ct.CalendarDate, ss.'+@GroupLevel+' 


       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, ct.CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL4 = @SQL4 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL4 = @SQL4 + '
                from planmonth p 
                inner join section_complete ss on
                           p.prodmonth = ss.prodmonth and 
                            p.sectionid = ss.sectionid
                inner join seccal sc on
                           ss.prodmonth = sc.prodmonth and
                           ss.sectionid_1 = sc.sectionid
                inner join CalType ct on
                           sc.CalendarCode = ct.CalendarCode and
                           sc.BeginDate  <= ct.CalendarDate and
                           sc.Enddate >= ct.CalendarDate
                --INNER JOIN
                LEFT outer join 
                            (select pd.ProdMonth, pd.SEctionID, pd.WorkplaceID, pd.Calendardate , pd.Activity, pd.PlanCode,
                                        pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *MetresAdvance*(SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *MetresAdvance*(SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *MetresAdvance*(SW/100)) + (pd.WasteFL *MetresAdvance*(SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                           from planning pd 
                             inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID 
							inner join Workplace w on 
                            w.WorkplaceID = pd.WorkplaceID 
                            where 
                                        pd.ProdMonth = '+ @ProdMonth +' and 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
				IF (@Activity = '1')
				   BEGIN
							IF (@Account <> '')
									   Set @SQL4 = @SQL4 + ''
							IF (@WorkCap <> '')
									   Set @SQL4 = @SQL4 + ''       
				   END
              Set @SQL4 = @SQL4 + '  ) pd on 
                                        p.ProdMonth = pd.ProdMonth and 
                                        p.SectionID = pd.SectionID and 
                                        p.WorkplaceID = pd.WorkplaceID and 
                                        p.Activity = pd.Activity and 
                                        ct.CalendarDate >= pd.Calendardate and
                                        p.PlanCode = pd.PlanCode
                inner join Workplace w on 
                            w.WorkplaceID = p.WorkplaceID 
                where 
                           p.ProdMonth = '+ @ProdMonth +' and 
                            p.Activity = '+ @Activity +' and
                           p.PlanCode = ''MP'' AND
                           p.plancode <> ''NULL'' and              
                            ct.CalendarDate >='''+@TheMinDate+''' and 
                            ct.CalendarDate <= '''+@TheMaxDate+''' and
                           ss.'+ @SectionLevel +'   = '''+@SectionName+''' '
       IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ''
                IF (@WorkCap <> '')
                           Set @SQL4 = @SQL4 + ''       
       END
             
       Set @SQL4 = @SQL4 + '
                group by ct.CalendarDate ) a 
       order by a.theSort, a.Section, a.CalendarDate'
END




IF (@TypeMonth = 'M')
BEGIN 
       declare @thedate datetime

       create table #daterange (theDate varchar(10))

    set @thedate = @TheMinDate
    while @thedate <= @TheMaxDate
    begin
             insert into #daterange values (convert(varchar(10),@thedate,120))
             set @thedate = dateadd(day, 1, @thedate)
    end


Set @SQL1 = '
       select * from 
       ( Select ''1'' theSort, '''+@TheMaxDate+''' MaxDate, '
	   --'+ @GroupLevel +' Section, theDate CalendarDate, '
		
		IF @SumLevel = '8'  
	   	BEGIN
			Set @SQL1 = @SQL1 + @GroupCrewLevel +' Section, theDate CalendarDate, '
		END
		ELSE
		BEGIN   
			IF @SumLevel = '9'  
	   		BEGIN
				Set @SQL1 = @SQL1 + @GroupWPLevel +' Section, theDate CalendarDate, '
			END
			ELSE      
				Set @SQL1 = @SQL1 +  @GroupLevel +' Section, theDate CalendarDate, '
		END

       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL1 = @SQL1 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL1 = @SQL1 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL1 = @SQL1 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL1 = @SQL1 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL1 = @SQL1 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL1 = @SQL1 + '
             from #daterange 
                LEFT outer join '

				IF @SumLevel = '8'  
	   			BEGIN
					Set @SQL1 = @SQL1 + '(select p.'+ @GroupCrewLevel +' , pd.CalendarDate, '
				END
				ELSE
				BEGIN  
					IF @SumLevel = '9'  
	   				BEGIN
						Set @SQL1 = @SQL1 + '(select w.'+ @GroupWPLevel +' , pd.CalendarDate, '
					END
					ELSE       
						Set @SQL1 = @SQL1 + '(select sc.'+ @GroupLevel +' , pd.Calendardate , '
				END
                        --    (select sc.'+ @GroupLevel +' , pd.Calendardate ,
                    Set @SQL1 = @SQL1 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
										pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

                            from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid 
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                        pd.PlanCode = ''MP'' '
		IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL1 = @SQL1 + ''
                IF (@WorkCap <> '')
                           Set @SQL1 = @SQL1 + ''       
       END 
             Set @SQL1 = @SQL1 + ' ) pd on theDate >= pd.Calendardate '
                --    group by pd.'+ @GroupLevel +', theDate '
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL1 = @SQL1 + ' group by p.'+ @GroupCrewLevel +' , theDate'
			END
			ELSE
			BEGIN   
				IF @SumLevel = '9'  
	   			BEGIN
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupWPLevel +' , theDate'
				END
				ELSE      
					Set @SQL1 = @SQL1 + ' group by pd.'+ @GroupLevel +' , theDate '
			END

       Set @SQL4 = ' union all'
       Set @SQL4 = @SQL4+
                ' Select ''2'' theSort, '''+@TheMaxDate+''' MaxDate, ''Total'' Section, theDate CalendarDate, '
       IF (@ReefWaste = '0')  --Reef
       BEGIN
                IF (@Unit = 'Tons')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefTons) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefTons) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Square Metres')
                BEGIN
                           Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefSQM) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefSQM) as decimal (10,3)),2) as decimal(10,0)) '
                END
                IF (@Unit = 'Metres Advance')
                BEGIN        
                            Set @SQL4 = @SQL4 + '
                                        Planned = cast(round(cast (Sum(Plan_ReefAdv) as decimal (10,3)),2) as decimal(10,1)),
                                        Booked = cast(round(cast (Sum(Book_ReefAdv) as decimal (10,3)),2) as decimal(10,1)) '
                END
                IF (@Unit = 'Volume')
                BEGIN        
                            Set @SQL4 = @SQL4 + '      
                                        Planned = cast(round(cast (Sum(Plan_ReefVolume) as decimal (10,3)),2) as decimal(10,0)),
                                        Booked = cast(round(cast (Sum(Book_ReefVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                END
       END
       ELSE
       BEGIN
                IF (@ReefWaste = '1') --Waste
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteSQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteSQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteAdv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_WasteAdv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_WasteTons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteTons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                          BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_WasteVolume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_WasteVolume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                        
                END
                ELSE
                BEGIN
                           IF (@Unit = 'Square Metres')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_SQM) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_SQM) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Metres Advance')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Adv) as decimal (10,3)),2) as decimal(10,1)),
                                                  Booked = cast(round(cast (Sum(Book_Adv) as decimal (10,3)),2) as decimal(10,1)) '
                           END
                           IF (@Unit = 'Tons')
                           BEGIN
                                        Set @SQL4 = @SQL4 + '
                                                  Planned = cast(round(cast (Sum(Plan_Tons) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Tons) as decimal (10,3)),2) as decimal(10,0)) '
                           END
                           IF (@Unit = 'Volume')
                           BEGIN        
                                        Set @SQL4 = @SQL4 + '      
                                                  Planned = cast(round(cast (Sum(Plan_Volume) as decimal (10,3)),2) as decimal(10,0)),
                                                  Booked = cast(round(cast (Sum(Book_Volume) as decimal (10,3)),2) as decimal(10,0)) ' 
                            END                 
                END
       END
       IF (@Unit = 'Kilograms')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)),    
                            Booked = cast(round(cast (Sum(Book_Grams)/1000 as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Grade')
       BEGIN
                Set @SQL4 = @SQL4 + '
                          Planned = cast(round(cast ((case when sum(Plan_ReefTons) > 0 then Sum(Plan_Grams) / sum(Plan_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)),     
                            Booked = cast(round(cast ((case when sum(Book_ReefTons) > 0 then Sum(Book_Grams) / sum(Book_ReefTons) else 0 end) as decimal (10,3)),2) as decimal(10,2)) '
       END
       IF (@Unit = 'Cubics')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_CubicMetres) as decimal (10,3)),2) as decimal(10,1)),    
                            Booked = cast(round(cast (Sum(Book_CubicMetres) as decimal (10,3)),2) as decimal(10,1)) '
       END
             
       IF (@Unit = 'Blast')
       BEGIN
                Set @SQL4 = @SQL4 + '
                           Planned = cast(round(cast (Sum(Plan_Blasts) as decimal (10,3)),2) as decimal(10,0)),
                           Booked = cast(round(cast (Sum(Book_Blasts) as decimal (10,3)),2) as decimal(10,0)) ' 
       END    

       Set @SQL4 = @SQL4 + '
                    from #daterange d
                LEFT outer join '
                           -- (select sc.'+ @GroupLevel +', pd.Calendardate , 
						    IF @SumLevel = '8'  
	   						BEGIN
								Set @SQL4 = @SQL4 + ' (select p.'+ @GroupCrewLevel +' , pd.Calendardate , '
							END
							ELSE
							BEGIN   
								IF @SumLevel = ''  
	   							BEGIN
									Set @SQL4 = @SQL4 + ' (select p.'+ @GroupWPLevel +' , pd.Calendardate , '
								END
								ELSE      
									Set @SQL4 = @SQL4 + ' (select sc.'+ @GroupLevel +', pd.Calendardate , '
							END
                           Set @SQL4 = @SQL4 + ' pd.ReefSQM Plan_ReefSQM,
                                        pd.ReefAdv as Plan_ReefAdv, 
                                        pd.ReefTons as Plan_ReefTons, 
                                        pd.ReefFL *pd.MetresAdvance*(p.SW/100) as Plan_ReefVolume, 

                                        pd.WasteSQM  Plan_WasteSQM,
                                        pd.WasteAdv  as Plan_WasteAdv, 
                                        pd.WasteTons as Plan_WasteTons, 
                                        pd.WasteFL *pd.MetresAdvance*(p.SW/100)  as Plan_WasteVolume,        
                                               
                                        pd.SQM Plan_SQM,
                                        pd.Metresadvance as Plan_Adv, 
                                        pd.Tons as Plan_Tons, 
                                        (pd.ReefFL *pd.MetresAdvance*(p.SW/100)) + (pd.WasteFL *pd.MetresAdvance*(p.SW/100)) as Plan_Volume,  

                                        pd.Grams as Plan_Grams, 
                                        pd.CubicMetres as Plan_CubicMetres, 
                                        Plan_Blasts = case when pd.Tons > 0 then 1 else 0 end,

                                        pd.BookReefSQM  as Book_ReefSQM, 
                                        pd.BookReefAdv  as Book_ReefAdv, 
                                        pd.BookReefTons  as Book_ReefTons, 
                                        pd.BookReefVolume  as Book_eefVolume, 

                                        pd.BookWasteSQM  as Book_WasteSQM, 
                                        pd.BookWasteAdv  as Book_WasteAdv, 
                                        pd.BookWasteTons  as Book_WasteTons, 
                                        pd.BookWasteVolume as Book_WasteVolume, 

                                        pd.BookSQM as Book_SQM, 
                                        pd.BookMetresadvance  as Book_Adv, 
                                        pd.BookTons as Book_Tons, 
                                        pd.BookVolume as Book_Volume,

                                        pd.BookGrams as Book_Grams, 
                                        BookCubicMetres as Book_CubicMetres, 
                                        Book_Blasts = case when pd.Tons > 0 then 1 else 0 end

							from planning pd 
                            inner join Section_Complete sc on 
                                        sc.ProdMonth = pd.ProdMonth and 
                                        sc.SectionID = pd.SectionID
							left outer join Planmonth p on
							  p.Prodmonth = pd.prodmonth and
							  p.SectionID = pd.SectionID and
							  p.WorkplaceID = pd.WorkplaceID and
							  p.Activity = pd.Activity and
							  p.PlanCode = pd.PlanCode
							inner join Workplace w on
							  w.workplaceid = pd.workplaceid  
                            where 
                                        sc.'+ @SectionLevel +'   = '''+@SectionName+''' and 
                                        pd.Activity = '+ @Activity +' and 
                                        pd.calendardate >='''+@TheMinDate+''' and 
                                        pd.calendardate <= '''+@TheMaxDate+''' AND
                                       pd.PlanCode = ''MP'' '
	IF (@Activity = '1')
       BEGIN
                IF (@Account <> '')
                           Set @SQL4 = @SQL4 + ''
                IF (@WorkCap <> '')
                           Set @SQL4 = @SQL4 + ''       
       END 
          Set @SQL4 = @SQL4 + '   ) pd on theDate >= pd.Calendardate
             group by ' --pd.'+ @GroupLevel +', theDate
			IF @SumLevel = '8'  
	   		BEGIN
				Set @SQL4 = @SQL4 + ' p.'+ @GroupCrewLevel +' , theDate '
			END
			ELSE
			BEGIN 
				IF @SumLevel = '9'  
	   		BEGIN
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupWPLevel +' , theDate '
				END
				ELSE      
					Set @SQL4 = @SQL4 + ' pd.'+ @GroupLevel +', theDate '
			END
      Set @SQL4 = @SQL4 + '  ) z order by theSort, MaxDate, Section, CalendarDate '

END


EXEC (@SQL1+@SQL4)  

--print @SQL1 
--print @SQL4

IF (@TypeMonth = 'M')
BEGIN
	drop table #daterange
END

GO

-- [SP_Planning_Dev_Summary] '201701', '1.5 - T Cwati', 6
ALTER Procedure [dbo].[SP_Planning_Dev_Summary]
--Declare
  @Prodmonth varchar(6),
  @Section VarChar(50),
  @SumLevel int

AS
Declare @TheLevel Int,
        @SQL1 Varchar(max),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)

select @TheLevel = HIERARCHICALID from section where PRODMONTH = @Prodmonth and
Name = @Section

  If @SumLevel = 1
    set @GroupLevel = 'NAME_5'
    
  If @SumLevel = 2 
    set @GroupLevel = 'NAME_4'  
    
  If @SumLevel = 3 
    set @GroupLevel = 'NAME_3'  
    
  If @SumLevel = 4 
    set @GroupLevel = 'NAME_2'  
    
  If @SumLevel = 5 
    set @GroupLevel = 'NAME_1'

	  If @SumLevel = 6
    set @GroupLevel = 'NAME'
    
  If @TheLevel = 1
    set @SectionLevel = 'NAME_5'
    
  If @TheLevel = 2 
    set @SectionLevel = 'NAME_4'  
    
  If @TheLevel = 3 
    set @SectionLevel = 'NAME_3'  
    
  If @TheLevel = 4 
    set @SectionLevel = 'NAME_2'  
    
  If @TheLevel = 5 
    set @SectionLevel = 'NAME_1'  

	  If @TheLevel = 6
    set @SectionLevel = 'NAME'  
    

Set @SQL1 = 'Select GroupSec,
Max(Prim) Prim,
Max(OrgUnitday) OrgUnitday,
Max(OrgUnitAfterNoon) OrgUnitAfterNoon,
Max(OrgUnitNight) OrgUnitNight,
'''' remarks,
Sum(metresAdvance+Converted_M) TotalAdvance,
Convert(Numeric(9,1),sum(MetresAdvance)) MetresAdvance,
Convert(Numeric(9,1),sum(OnReefAdv)) OnReefAdv,
Convert(Numeric(9,1),sum(OffReefAdv)) OffReefAdv,
Convert(Numeric(9,1),sum(SecTot_M)) SecTot_M,
Convert(Numeric(9,1),sum(SecOn_M)) SecOn_M,
Convert(Numeric(9,1),sum(SecOff_M)) SecOff_M,
Convert(Numeric(9,1),sum(MainTot_M)) MainTot_M,
Convert(Numeric(9,1),sum(MainOn_M)) MainOn_M,
Convert(Numeric(9,1),sum(MainOff_M)) MainOff_M,
Sum(Capital_M) Capital_M,
Sum(CapitalOn_M) CapitalOn_M,
Sum(CapitalOff_M) CapitalOff_M,
Sum(Converted_M) Converted_M,
0 ConvertedOn_M,
0 ConvertedOff_M,
'''' DrillRig,
Convert(Numeric(9),sum(Tons)) Tons,
0 OnReefTons,
0 OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = sum(Grams),
0 cmgt,
kg = convert(Numeric(10,1),SUM(Grams)/1000),
Convert(Numeric(9,1),sum(OnReefSQM)) OnReefSQM,
Convert(Numeric(9,1),avg(Height)) Height,
0 gt,
avg(ShaftPayLimit) ShaftPayLimit,
Sum(CMGTGramsCalc) CMGTGramsCalc,
avg(convert(numeric(6,2), 0)) labourstrength,
Convert(Numeric(9),sum(Cubicmetres)) Cubicmetres,
Convert(Numeric(9),sum(CubicTons)) CubicTons,
Convert(Numeric(9),sum(CubicGrams)) CubicGrams,
sum(0) CubicGT,
sum(Bus_Plan_DevCubics/10)+sum(Bus_Plan_Adv)+sum(Bus_Plan_Cap_Adv) Bus_Plan_TotalAdv,
sum(Bus_Plan_Adv) Bus_Plan_Adv,
sum(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
sum(Bus_Plan_Adv)-sum(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
sum(Bus_Plan_DevCubics/10) Bus_Plan_DevCubics,
Isnull(sum(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
from
(Select GroupSec, SelectSection,
Max(Prim) Prim,
Max(OrgUnitday) OrgUnitday,
Max(OrgUnitAfterNoon) OrgUnitAfterNoon,
Max(OrgUnitNight) OrgUnitNight,
'''' remarks,
convert(Numeric(10,1), round(Sum(metresAdvance+Converted_M),1)) TotalAdvance,
sum(MetresAdvance) MetresAdvance,
sum(OnReefAdv) OnReefAdv,
sum(OffReefAdv) OffReefAdv,
sum(SecTot_M) SecTot_M,
sum(SecOn_M) SecOn_M,
sum(SecOff_M) SecOff_M,
sum(MainTot_M) MainTot_M,
sum(MainOn_M) MainOn_M,
sum(MainOff_M) MainOff_M,
convert(Numeric(10,1), round(Sum(Capital_M),1)) Capital_M,
convert(Numeric(10,1), round(Sum(CapitalOn_M),1)) CapitalOn_M,
convert(Numeric(10,1), round(Sum(CapitalOff_M),1)) CapitalOff_M,
convert(Numeric(10,1), round(Sum(Converted_M),1)) Converted_M,
convert(Numeric(10,1), round(Sum(ConvertedOn_M),1)) ConvertedOn_M,
convert(Numeric(10,1), round(Sum(ConvertedOff_M),1)) ConvertedOff_M,
'''' DrillRig,
sum(Tons) Tons,
sum(OnReefTons) OnReefTons,
sum(OffReefTons) OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = sum(Grams),
sum(OnReefSQM) OnReefSQM,
avg(Height) Height,
avg(ShaftPayLimit) ShaftPayLimit,
Sum(CMGTGramsCalc) CMGTGramsCalc,
avg(convert(numeric(6,2), 0)) labourstrength, 
sum(Cubicmetres) Cubicmetres,
sum(CubicTons) CubicTons,
sum(CubicGrams) CubicGrams,
sum(0) CubicGT,
Avg(DevCubics/10)+Avg(Bus_Plan_Adv) Bus_Plan_TotalAdv,
Avg(Bus_Plan_Adv) Bus_Plan_Adv,
Avg(Bus_Plan_OnreefAdv) Bus_Plan_OnreefAdv,
Avg(Bus_Plan_Adv)-Avg(Bus_Plan_OnreefAdv) Bus_Plan_OffreefAdv,
Isnull(Avg(DevCubics),0) Bus_Plan_DevCubics,
Isnull(Avg(Bus_Plan_Cap_Adv),0) Bus_Plan_Cap_Adv
from
(select
B.'+@GroupLevel+' GroupSec, Name_2 SelectSection,
'''' Prim,
OrgUnitday = Case when ((a.OrgUnitday) is null) then '''' else (a.OrgUnitday) end,
OrgUnitAfterNoon = Case when ((a.OrgUnitAfterNoon) is null) then '''' else (a.OrgUnitAfterNoon) end,
OrgUnitNight = Case when ((a.OrgUnitNight) is null) then '''' else (a.OrgUnitNight) end,
'''' remarks,
(a.ReefAdv+a.WasteAdv) MetresAdvance,
(a.ReefAdv) OnReefAdv,
(a.WasteAdv ) OffReefAdv,
0 SecTot_M,
0 SecOn_M,
0 SecOff_M,
0 MainTot_M,
0 MainOn_M,
0 MainOff_M,
0 Capital_M,
0 CapitalOn_M,
0 CapitalOff_M,
Converted_M =  (a.CUBICMETRES/10),
ConvertedOn_M = case when c.ReefWaste in (0,3) then (a.CUBICMETRES/10) else 0 end,
ConvertedOff_M = case when c.ReefWaste in (1,2) then (a.CUBICMETRES/10) else 0 end,
'''' DrillRig,
(a.Tons) Tons,
case when c.ReefWaste in (0,3) then (a.ReefTons) else 0 end OnReefTons,
case when c.ReefWaste in (1,2) then (a.WasteTons) else 0 end OffReefTons,
0 Volume,
0 OnReefVolume,
0 OffReefVolume,
Grams = (GT),
(a.ReefAdv*a.FL) OnReefSQM,
(a.SW) Height,
0 ShaftPayLimit,
CMGTGramsCalc = case when (c.Density) is null or (c.Density) = 0 then
(cmgt*(a.ReefAdv*a.FL)*s.RockDensity/100)
else
(cmgt*(a.ReefAdv*a.FL)*c.Density/100) end,
(convert(numeric(6,2), 0)) labourstrength, 
(a.Cubicmetres) Cubicmetres,
(Convert(Numeric(7),a.Cubicmetres*c.density)) CubicTons,
(Convert(Numeric(7),a.Cubicmetres*c.density)*0) CubicGrams,
(0) CubicGT,
0 Bus_Plan_Adv,
0  Bus_Plan_OnreefAdv,
0  DevCubics,
0 Bus_Plan_Cap_Adv

 from PLANMONTH a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionid inner join workplace c on
a.workplaceid = c.workplaceid inner join seccal d on
b.prodmonth = d.prodmonth and
b.sectionid_1 = d.sectionid 
--left outer join shaft_paylimits e on 
--a.prodmonth = e.prodmonth and 
--c.OreFlowID  = e.OreFlowID 

    --left join
    --(select * from planmonth_ops
    -- where PRODMONTH = '+@Prodmonth+'
    -- and Activity = 1) BusPlan on
    -- b.SECTIONID_2 = BusPlan.Sectionid_2,
     
 ,Sysset s
where a.prodmonth ='+@Prodmonth+'
and a.activity in (1,7)
and '+@SectionLevel+' = '''+@Section+''') correctedData
group by GroupSec, SelectSection) LastSum
group by GroupSec'


exec(@SQL1)
GO

-- [sp_PlanningReportDynamicDetail] '201612', 'REA'
ALTER Procedure [dbo].[sp_PlanningReportDynamicDetail]
@Prodmonth varchar(6),
@SectionID varchar(10)

AS
select 'Planning Detail Report (Dynamic)' label1, '' banner, 
@ProdMonth Myprodmonth , 'Total Mine' thesection, Fl,  
* from(select BrokenRockDensity, CONVERT(numeric(11, 0), CMGT) CMGT, CONVERT(numeric(15, 0), AdvCMGT) AdvCMGT,
CONVERT(numeric(15, 0), SqmCMGT) SqmCMGT, 0 Vamps,
s2reptosecid, mosection, ReptoSecid, MoName, Fl, Adv, 0 SW, CW, 0 SQMSW, reefsqm * cw SQMCW,
Reefmetres, Wastemetres, ReefSQM, WasteSQM, OldGoldTons, OldGoldContents, Cubics, Dens, OrgUnitDay, ShiftBossName,
MinerName,
Totalmetres, wpDesc, wpID, SbSecID, MinerSecID, DevOunces, StopeOunces, TotalOunces,
isnull(DevContent, 0) DevContent, isnull(StopeContent, 0) StopeContent, isnull(TotalContent, 0) TotalContent,
StopeTons, DevTons, TotalTons,
Facelength, SQM, AveFAdv from(
select CMGT = case when pm.activity IN(0, 9) and isnull(pm.ReefSQM, 0) > 0 then pm.GT
when pm.activity = 1 and isnull(pm.ReefAdv, 0) > 0 then pm.GT else 0 end,
0 Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName,
SECTIONID_1 SbSecID, sc.SECTIONID MinerSecID, NAME MinerName, OrgUnitDay,
pm.FL Fl, Adv = case when pm.Activity IN(0, 9) and pm.FL > 0 then pm.SQM / pm.FL else 0 end,
0 BrokenRockDensity,
0 OldGoldTons, 0 OldGoldContents, 0 Cubics, case when SQM != 0
then pm.Density else 0 end as Dens, sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 MoName,
case when pm.Activity = 1 then Kg/1000 / 31.10348 else 0 end as DevOunces,
case when pm.Activity IN(0, 9)   then Kg/1000 / 31.10348 else 0 end as StopeOunces,
Kg/1000 / 31.10348 TotalOunces,
case when pm.Activity = 1 then Kg/1000 else 0 end as DevContent,
case when pm.Activity IN(0, 9)   then Kg/1000 else 0 end as StopeContent,
0 TotalContent,
case when pm.activity = 1   then(ReefTons + WasteTons) else 0 END as DevTons,
case when pm.activity IN(0, 9)   then  Tons else 0 END as StopeTons,
0 TotalTons, FL Facelength, SQM,     case when FL > 0
then SQM / FL  else 0 end as AveFAdv,
case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste = 'R' THEN DevSec ELSE 0 END else 0 end as ReefMetres,
case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste = 'W' THEN DevSec ELSE 0 END else 0 end as WasteMetres,
case when ReefSQM is not null then ReefSQM else 0 end ReefSQM,
case when WasteSQM is not null then WasteSQM else 0 end WasteSQM,
case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * GT else 0 end AdvCMGT,
case when ReefSQM is not null AND pm.Activity IN(0, 9) then ReefSQM * GT else 0 end SqmCMGT,
ISNULL(CW, 0) CW,
case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres
from planmonth pm, section_complete sc, workplace w   where
pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and
pm.prodmonth = @ProdMonth and sc.SECTIONID_2 = @SectionID) a
) q order by SbSecID, MinerSecID, wpDesc

GO

 -- sp_PlanningReportLockedDetail '201612', 'REA'
ALTER Procedure [dbo].[sp_PlanningReportLockedDetail]
@Prodmonth varchar(6),
@SectionID varchar(10)

AS
 
 select 'Planning Detail Report (Locked)' label1, '' banner, 
 @Prodmonth Myprodmonth , 'Total Mine' thesection, Fl,  
 * from (select CONVERT(numeric(11, 0), CMGT) CMGT, CONVERT(numeric(15, 0), AdvCMGT) AdvCMGT, 
 CONVERT(numeric(15, 0), SqmCMGT) SqmCMGT, 
 Vamps, s2reptosecid, mosection, ReptoSecid, MoName, Fl, 
 Adv , 
 SW, CW, SQM*sw SQMSW, reefsqm*cw SQMCW, 
 Reefmetres,    Wastemetres,  ReefSQM, WasteSQM, OldGoldTons, OldGoldContents, Cubics, Dens, BrokenRockDensity, 
 OrgUnitDay, ShiftBossName, MinerName, 
 Totalmetres,   wpDesc, wpID,  SbSecID,MinerSecID, 
 DevOunces, StopeOunces,    TotalOunces,  
 CONVERT(numeric(15, 0), DevContent) DevContent, CONVERT(numeric(15, 0), StopeContent) StopeContent, 
 CONVERT(numeric(15, 0), TotalContent) TotalContent, 
 StopeTons,    DevTons, TotalTons,    
 Facelength,  SQM SQM, AveFAdv from (     
 select GT CMGT,0 Vamps, w.Description wpDesc, w.workplaceid wpID, NAME_1 ShiftBossName, SECTIONID_1 SbSecID, 
 sc.SECTIONID MinerSecID, NAME MinerName, OrgUnitDay, pm.FL Fl,
 Adv = case when pm.Activity IN( 0,9) and pm.FL > 0 then pm.SQM/pm.fl else 0 end, 
 pm.SW SW, w.BrokenRockDensity BrokenRockDensity,
 0 OldGoldTons, 0 OldGoldContents, 0 Cubics, case when SQM != 0 then pm.Density else 0 end as Dens, 
 sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid,  
 name_2 MoName,  
 case when pm.Activity = 1 then KG/1000/31.10348 else 0 end as DevOunces,     
 case when pm.Activity IN (0,9)   then KG/1000/31.10348 else 0 end as StopeOunces,   
 KG/1000/31.10348 TotalOunces,    

 case when pm.Activity = 1 then KG/1000 else 0 end as DevContent,     
 case when pm.Activity IN (0,9)   then KG/1000 else 0 end as StopeContent,   
 (KG/1000) TotalContent,    

 case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
 case when pm.activity IN (0,9)   then (ReefTons+WasteTons) else 0 END as StopeTons,     
 (ReefTons+WasteTons) TotalTons, FL Facelength, SQM,     
 case when pm.Activity IN( 0,9) and FL > 0  
  then SQM/FL  else 0 end as AveFAdv, 
case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end as ReefMetres,
 
case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end as WasteMetres,
 
 case when ReefSQM is not null then ReefSQM else 0 end ReefSQM, 
 case when WasteSQM is not null then WasteSQM else 0 end WasteSQM, 
 case when ReefAdv is not null AND pm.Activity = 1 then ReefAdv * GT else 0 end AdvCMGT, 
 case when ReefSQM is not null AND pm.Activity IN( 0,9) then ReefSQM * GT else 0 end SqmCMGT, 
 ISNULL(CW, 0) CW, 
 case when  pm.activity = 1 then Metresadvance else 0 end as Totalmetres 
 from planmonth pm, section_complete sc, workplace w   where  
 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
 pm.prodmonth = @Prodmonth and sc.SECTIONID_2 = @SectionID and pm.PlanCode = 'LP') a 
 )q  order by SbSecID, MinerSecID, OrgUnitDay, wpDesc 

 
 GO

-- [sp_PlanningReportTotalDynamic]'Percy', '201612'
ALTER PROCEDURE [dbo].[sp_PlanningReportTotalDynamic]
@Banner varchar(200), 
@Prodmonth varchar(6)

AS
 select 'Planning Report (Dynamic)' label1, @Banner banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 0 budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Sectionid_3, Name_3 
	from SECTION_COMPLETE where prodmonth = @ProdMonth) sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity, 
	 sum(ReefMeters) ReefMeters,   SUM(WasteMeters) WasteMeters,  SUM(TotalMeters) TotalMeters, 
	 SUM(DevOunces) DevOunces,  SUM(StopeOunces) StopeOunces,   SUM(TotalOunces) TotalOunces,  
	 SUM(DevContent) DevContent, SUM(StopeContent) StopeContent,   SUM(TotalContent) TotalContent,  
	 SUM(StopeTons) StopeTons,   SUM(DevTons) DevTons, SUM(TotalTons) TotalTons,    
	 SUM(Facelength) Facelength, SUM(ReefSQM) ReefSQM, SUM(WasteSQM) WasteSQM, SUM(Sqm) Sqm,   SUM(AveFAdv) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 
	  0 OldGoldGT, 0 OldGoldTons
	 ,case when SUM(SQMReefForCalc) <> 0 then SUM(SQMReefForCalc * GT)/SUM(SQMReefForCalc) else 0 end CMGT
	 ,SUM(SW) SW
	 ,SUM(CW) CW
	 ,SUM(SQMReefForCalc * GT) GTForCalc
	 , SUM(SQMSW) SQMSW
	 , SUM(SQMCW) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then Kg/1000/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then Kg/1000/31.10348 else 0 end as StopeOunces,   
	 Kg/1000/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then Kg/1000 else 0 end as DevContent, case when pm.Activity in (0,9)   then Kg/1000 else 0 end 
	 as StopeContent, Kg/1000 as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM,case when pm.Activity in (0,9) then ReefSQM 
	 when pm.activity = 1 then (ReefAdv) else 0 end as SQMReefForCalc, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end 
	 as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 , 0.00 OldGoldContents, 0.00 OldGoldCubics, 0.00 OldGoldGT, 0.00 OldGoldTons
	 ,pm.Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = @ProdMonth  --and (pm.OldGoldContents IS null or pm.OldGoldContents = 0)
	 ) a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,Activity Act,
	0 budget_WasteSQM, 0 budget_Sqm, 
 	0 budget_StopeTons,    
 	0 budget_DevTons,  
 	0 budget_Fl,  
 	0 budget_ReefMeters, 0 budget_WasteMeters,   
 	0 budget_StopeContent,  
	0 budget_DevContent,  
 	0 BAveFAdv, 0 budget_SQMSW, 0 budget_SQMCW, 0 budget_SQMCMGT
 	from   
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0  Activity, 
 	0 ReefSqm , 
 	0 WasteSqm ,0 SQM,0 StopeTons ,0.0 DevTons ,0 StopeContent , 
 	0 AveFAdv, 
 	0 FL,0.0 DevContent ,0.0 ReefMeters ,0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 
	0 SQMCW   
 	--from businessplan_stoping
 	--where prodmonth = '201507' 
	union 
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm , 
 	0.0 WasteSqm , 
 	0.0 SQM,        
 	0.0 StopeTons , 
 	0.0 DevTons ,           
 	0.0 StopeContent , 
 	0.0 AveFAdv, 
	0.0 FL, 
 	0.0 DevContent , 
	0.0 ReefMeters , 
 	0.0 WasteMeters,0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW  
 	--from businessplan_development
 	--where prodmonth = '201507' 
	) a  
	 left outer join
	 (select distinct prodmonth, Sectionid_2,Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	 on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth   
	 group by Sectionid_2, Name_2, Sectionid_3,Name_3, Activity) b on b.MOID = sc.Sectionid_2  
	 where a.Activity is not null or b.ACt is not null) e   
	union
	 select 'Planning Report (Dynamic)' label1, @Banner banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from  (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 b.budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Name_3, Sectionid_3 
	from SECTION_COMPLETE where prodmonth = @ProdMonth) sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity,  
	 sum(0) ReefMeters,   SUM(0) WasteMeters,  SUM(0) TotalMeters, 
	 SUM(0) DevOunces,  SUM(0) StopeOunces,   SUM(0) TotalOunces,  
	 SUM(0) DevContent, SUM(0) StopeContent,   SUM(0) TotalContent,  
	 SUM(0) StopeTons,   SUM(0) DevTons, SUM(0) TotalTons,    
	 SUM(0) Facelength, SUM(0) ReefSQM, SUM(0) WasteSQM, SUM(0) Sqm,   SUM(0) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
	 ,'0' CMGT
	 ,SUM(0) SW
	 ,SUM(0) CW
	 ,SUM(0) GTForCalc
	 , SUM(0) SQMSW
	 , SUM(0) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then Grams/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then Grams/31.10348 else 0 end as StopeOunces,   
	 Grams/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then Grams else 0 end as DevContent, case when pm.Activity in (0,9)   then Grams else 0 end 
	 as StopeContent, Grams as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 , 0.00 OldGoldContents, 0.00 OldGoldCubics, 0.00 OldGoldGT, 0.00 OldGoldTons
	 , '' Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, PLANMONTH_OLDGOLD pmog, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = @ProdMonth -- and (pm.OldGoldContents IS not null or pm.OldGoldContents <> 0)
	 ) a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(
select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,
 		Activity Act, SUM(ReefSQM) budgetReefSQM, SUM(WasteSQM) budget_WasteSQM, SUM(SQM) budget_Sqm, 
 	SUM(StopeTons) budget_StopeTons,   
 	SUM(DevTons) budget_DevTons, 
 	SUM(Fl) budget_Fl, 
 	SUM(ReefMeters) budget_ReefMeters,  SUM(WasteMeters) budget_WasteMeters,  
 	SUM(StopeContent) budget_StopeContent, 
	SUM(DevContent) budget_DevContent, 
 	SUM(AveFAdv) BAveFAdv,SUM(SQMSW) budget_SQMSW,SUM(SQMCW) budget_SQMCW,SUM(SQMCMGT) budget_SQMCMGT 
 	from  
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0 Activity, 
 	0 ReefSqm ,      
 	0 WasteSqm , 
 	0 SQM,       
 	0 StopeTons ,
 	0.0 DevTons ,          
 	0 StopeContent ,
 	0 AveFAdv,
 	0 FL,     
 	0.0 DevContent ,
 	0.0 ReefMeters ,
 	0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 0 SQMCW  
 	--from businessplan_stoping     
 	--where prodmonth = '201507' 
	union
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm ,
 	0.0 WasteSqm ,
 	0.0 SQM,       
 	0.0 StopeTons ,
 	0.0 DevTons ,          
 	0.0 StopeContent ,
 	0.0 AveFAdv,
 	0.0 FL,
 	0 DevContent ,
 	0 ReefMeters ,
 	0 WasteMeters, 0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW 
 	--from businessplan_development     
 	--where prodmonth = '201507'
	) a 
 	left outer join     
 	(select distinct prodmonth, Sectionid_2, Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth  
 	group by Sectionid_2, Name_2, Sectionid_3,Name_3,  Activity ) b 
	on b.MOID = sc.sectionid_2  where a.Activity is not null or b.ACt is not null) e   
GO

-- [sp_PlanningReportTotalLocked] 'Percy', '201610'
ALTER PROCEDURE [dbo].[sp_PlanningReportTotalLocked]
	@Banner varchar(200), 
	@ProdMonth varchar(6)

AS
BEGIN
	 select 'Planning Report (Locked)' label1, @Banner banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Name_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 b.budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Sectionid_3, Name_3 
	from SECTION_COMPLETE where prodmonth = @ProdMonth) sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity, 
	 sum(ReefMeters) ReefMeters,   SUM(WasteMeters) WasteMeters,  SUM(TotalMeters) TotalMeters, 
	 SUM(DevOunces) DevOunces,  SUM(StopeOunces) StopeOunces,   SUM(TotalOunces) TotalOunces,  
	 SUM(DevContent) DevContent, SUM(StopeContent) StopeContent,   SUM(TotalContent) TotalContent,  
	 SUM(StopeTons) StopeTons,   SUM(DevTons) DevTons, SUM(TotalTons) TotalTons,    
	 SUM(Facelength) Facelength, SUM(ReefSQM) ReefSQM, SUM(WasteSQM) WasteSQM, SUM(Sqm) Sqm,   SUM(AveFAdv) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 
	  0 OldGoldTons
	 ,case when SUM(SQMReefForCalc) <> 0 then SUM(SQMReefForCalc * GT)/SUM(SQMReefForCalc) else 0 end CMGT
	 ,SUM(SW) SW
	 ,SUM(CW) CW
	 ,SUM(SQMReefForCalc * GT) GTForCalc
	 , SUM(SQMSW) SQMSW
	 , SUM(SQMCW) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then KG/1000/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then KG/1000/31.10348 else 0 end as StopeOunces,   
	 KG/1000/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then KG/1000 else 0 end as DevContent, case when pm.Activity in (0,9)   then KG/1000 else 0 end 
	 as StopeContent, KG/1000 as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM,case when pm.Activity in (0,9) then ReefSQM 
	 when pm.activity = 1 then (ReefAdv) else 0 end as SQMReefForCalc, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv   + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end 
	 as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv  + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
	 ,pm.Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = @ProdMonth  and pm.PlanCode = 'LP') a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,Activity Act,
	 SUM(ReefSQM) budgetReefSQM, SUM(WasteSQM) budget_WasteSQM, SUM(SQM) budget_Sqm, 
 	SUM(StopeTons) budget_StopeTons,    
 	SUM(DevTons) budget_DevTons,  
 	SUM(Fl) budget_Fl,  
 	SUM(ReefMeters) budget_ReefMeters,  SUM(WasteMeters) budget_WasteMeters,   
 	SUM(StopeContent) budget_StopeContent,  
	SUM(DevContent) budget_DevContent,  
 	SUM(AveFAdv) BAveFAdv,SUM(SQMSW) budget_SQMSW,SUM(SQMCW) budget_SQMCW,SUM(SQMCMGT) budget_SQMCMGT
 	from   
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0  Activity, 
 	0 ReefSqm , 
 	0 WasteSqm ,0 SQM,0 StopeTons ,0.0 DevTons ,0 StopeContent , 
 	0 AveFAdv, 
 	0 FL,0.0 DevContent ,0.0 ReefMeters ,0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 
	0 SQMCW   
 	--from businessplan_stoping
 	--where prodmonth = '201507' 
	union 
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm , 
 	0.0 WasteSqm , 
 	0.0 SQM,        
 	0.0 StopeTons , 
 	0.0 DevTons ,           
 	0.0 StopeContent , 
 	0.0 AveFAdv, 
	 0.0 FL, 
 	0 DevContent , 
	0 ReefMeters , 
 	0 WasteMeters,0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW  
 	--from businessplan_development
 	--where prodmonth = '201507' 
	) a  
	 left outer join
	 (select distinct prodmonth, Sectionid_2,Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	 on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth   
	 group by Sectionid_2, Name_2, Sectionid_3,Name_3, Activity) b on b.MOID = sc.Sectionid_2  
	 where a.Activity is not null or b.ACt is not null) e   
	union
	 select 'Planning Report (Locked)' label1, @Banner banner, 
	 @ProdMonth Myprodmonth , 'Total Mine' thesection, *, BAveFAdv AveFAdv from  (
	  select sc.Sectionid_3 S2reptosecid, sc.Name_3 mosection, sc.Sectionid_2 reptosecid, sc.Sectionid_2 sectionname,
	 Activity = case when a.activity is null then b.act else a.Activity end, a.ReefMeters, a.wastemeters, a.totalmeters,
	 a.DevOunces,  a.StopeOunces,a.TotalOunces,a.DevContent,a.StopeContent,a.TotalContent,a.StopeTons,a.DevTons,a.TotalTons,    
	 a.Facelength,a.ReefSQM,a.WasteSQM,a.Sqm,a.AveFAdv 
	  ,a.OldGoldContents,a.OldGoldCubics,a.OldGoldGT,a.OldGoldTons
	 ,a.CMGT,a.SW,a.CW,a.GTForCalc,a.SQMSW,a.SQMCW,
	 b.budgetReefSQM,b.budget_WasteSQM,b.budget_Sqm,b.budget_StopeTons,    
 	b.budget_DevTons,b.budget_Fl,b.budget_ReefMeters, b.budget_WasteMeters,b.budget_StopeContent,b.budget_DevContent,  
 	b.BAveFAdv,b.budget_SQMSW,b.budget_SQMCW,b.budget_SQMCMGT from (select distinct Sectionid_2, Name_2, Sectionid_3, Name_3 
	from Section_complete where prodmonth = @ProdMonth) sc 
	 left outer join (select s2reptosecid, mosection, ReptoSecid, sectionname,convert(int, Activity) Activity,  
	 sum(0) ReefMeters,   SUM(0) WasteMeters,  SUM(0) TotalMeters, 
	 SUM(0) DevOunces,  SUM(0) StopeOunces,   SUM(0) TotalOunces,  
	 SUM(0) DevContent, SUM(0) StopeContent,   SUM(0) TotalContent,  
	 SUM(0) StopeTons,   SUM(0) DevTons, SUM(0) TotalTons,    
	 SUM(0) Facelength, SUM(0) ReefSQM, SUM(0) WasteSQM, SUM(0) Sqm,   SUM(0) AveFAdv 
	  ,0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 
	  0 OldGoldTons
	 ,'0' CMGT
	 ,SUM(0) SW
	 ,SUM(0) CW
	 ,SUM(0) GTForCalc
	 , SUM(0) SQMSW
	 , SUM(0) SQMCW
	 from (     
	 select sectionid_3 s2reptosecid, name_3 mosection, sectionid_2 ReptoSecid, name_2 sectionname,  
	 case when pm.Activity = 1 then KG/1000/31.10348 else 0 end as DevOunces,     
	 case when pm.Activity in (0,9)   then KG/1000/31.10348 else 0 end as StopeOunces,   
	 KG/1000/31.10348 TotalOunces,    case when pm.activity = 1   then (ReefTons+WasteTons) else 0 END as DevTons,   
	 case when pm.Activity = 1 then KG/1000 else 0 end as DevContent, case when pm.Activity in (0,9)   then KG/1000 else 0 end 
	 as StopeContent, KG/1000 as TotalContent,   
	 case when pm.activity = 0 then WasteTons when pm.activity = 9 then ReefTons else 0 END as StopeTons,     
	 (ReefTons+WasteTons) as TotalTons, FL Facelength, ReefSQM, WasteSQM, SQM Sqm,     case when FL > 0  
	 then SQM/FL  else 0 end as AveFAdv, 
	 case when ReefAdv is not null then ReefAdv + CASE WHEN pm.ReefWaste='R' THEN DevSec ELSE 0 END else 0 end 
	 as ReefMeters, 
	 case when WasteAdv is not null then WasteAdv + CASE WHEN pm.ReefWaste='W' THEN DevSec ELSE 0 END else 0 end 
	 as WasteMeters,  
	 case when  pm.activity = 1 then Metresadvance else 0 end as TotalMeters 
	 , 0 OldGoldContents, 0 OldGoldCubics, 0 OldGoldGT, 0 OldGoldTons
	 ,pm.Activity Activity
	 ,pm.SW, pm.CW, pm.GT
	 ,pm.SW * pm.SQM SQMSW
	 ,pm.CW * pm.ReefSQM SQMCW
	 from planmonth pm, section_complete sc, workplace w   where  
	 pm.sectionid = sc.sectionid and pm.prodmonth = sc.prodmonth and pm.workplaceid = w.workplaceid and   
	 pm.prodmonth = @ProdMonth and pm.PlanCode = 'LP') a 
	 group by s2reptosecid, mosection, ReptoSecid, sectionname, Activity) a on a.ReptoSecID = sc.Sectionid_2 
	 left outer join  
 	(
select Sectionid_3 UMID, Name_3 UMName, Sectionid_2 MOID, Name_2 MOName,
 		Activity Act, SUM(ReefSQM) budgetReefSQM, SUM(WasteSQM) budget_WasteSQM, SUM(SQM) budget_Sqm, 
 	SUM(StopeTons) budget_StopeTons,   
 	SUM(DevTons) budget_DevTons, 
 	SUM(Fl) budget_Fl, 
 	SUM(ReefMeters) budget_ReefMeters,  SUM(WasteMeters) budget_WasteMeters,  
 	SUM(StopeContent) budget_StopeContent, 
	SUM(DevContent) budget_DevContent, 
 	SUM(AveFAdv) BAveFAdv,SUM(SQMSW) budget_SQMSW,SUM(SQMCW) budget_SQMCW,SUM(SQMCMGT) budget_SQMCMGT 
 	from  
 	(select '' sectionid, '' prodmonth, '' workplaceid, 0 Activity, 
 	0 ReefSqm ,      
 	0 WasteSqm , 
 	0 SQM,       
 	0 StopeTons ,
 	0.0 DevTons ,          
 	0 StopeContent ,
 	0 AveFAdv,
 	0 FL,     
 	0.0 DevContent ,
 	0.0 ReefMeters ,
 	0.0 WasteMeters, 0 SQMCMGT, 0 SQMSW, 0 SQMCW  
 	--from businessplan_stoping     
 	--where prodmonth = '201507' 
	union
 	select '' sectionid, '' prodmonth, '' workplaceid, 1 Activity, 
 	0.0 ReefSqm ,
 	0.0 WasteSqm ,
 	0.0 SQM,       
 	0.0 StopeTons ,
 	0.0 DevTons ,          
 	0.0 StopeContent ,
 	0.0 AveFAdv,
 	0.0 FL,
 	0 DevContent ,
 	0 ReefMeters ,
 	0 WasteMeters, 0.0 SQMCMGT, 0.0 SQMSW, 0.0 SQMCW 
 	--from businessplan_development     
 	--where prodmonth = '201507'
	) a 
 	left outer join     
 	(select distinct prodmonth, Sectionid_2, Name_2, Sectionid_3, Name_3 from SECTION_COMPLETE) c  
	on a.Sectionid = c.Sectionid_2 and a.prodmonth = c.prodmonth  
 	group by Sectionid_2, Name_2, Sectionid_3,Name_3,  Activity ) b on b.MOID = sc.Sectionid_2  
	where a.Activity is not null or b.ACt is not null) e   
END

GO

-- [sp_DailyGradeReport] '2017-02-02', 2, 'Y', 1330, 860, 'G'
ALTER Procedure [dbo].[sp_DailyGradeReport]
--Declare
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int,
@TopPanels varchar(1)

AS
Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then '      '+Description
when isnull(MO,'') <> '' and isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 1
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 'Total '+MO
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
Select 
MO = case when @TopPanels = 'T' then tp.SectionID else b.Sectionid_2 end,
e.Description,
ShiftNo = Sum(Case when d.WorkingDay = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*w.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * w.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * w.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*w.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * w.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * w.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * w.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*w.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * w.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*w.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*w.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * w.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*w.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
inner join workplace w on
a.workplaceid = w.workplaceid
inner join TOPPANELSSELECTED tp
on w.WorkplaceID = tp.WorkplaceID

Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by tp.SectionID, b.sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
GO

-- [sp_DailyGradeReport_Development] '2017-02-02', 2, 'Y', 1330, 860
ALTER Procedure [dbo].[sp_DailyGradeReport_Development]
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int

AS
Select 'AA' TheTop,
	'MO '+MO MO,
	[Description],
	ShiftNo = Min(ShiftNo),
	TotalShifts = Min(TotalShifts),
	LockPlan_Adv  = Sum(LockPlan_Adv),
	LockPlan_ReefAdv  = Sum(LockPlan_ReefAdv),
	LockPlan_WasteAdv  = Sum(LockPlan_WasteAdv),
	LockPlan_Tonnes = Sum(LockPlan_Tonnes),
	LockPlan_ReefTons = Sum(convert(decimal(10,5),LockPlan_ReefTons)),
	LockPlan_WasteTons = Sum(convert(decimal(10,5),LockPlan_WasteTons)),
	LockPlan_gt = Case When Sum(LockPlan_ReefTons) = 0 then 0 else Sum(LockPlan_Grams)*1000/Sum(LockPlan_ReefTons) end,
	LockPlan_Kg = Sum(LockPlan_Grams),

	DynPlan_Adv  = Sum(DynPlan_Adv),
	DynPlan_ReefAdv  = Sum(DynPlan_ReefAdv),
	DynPlan_WasteAdv  = Sum(DynPlan_WasteAdv),
	DynPlan_Tonnes = Sum(DynPlan_Tonnes),
	DynPlan_ReefTons = Sum(convert(decimal(10,5),DynPlan_ReefTons)),
	DynPlan_WasteTons = Sum(convert(decimal(10,5),DynPlan_WasteTons)),
	DynPlan_gt = Case When Sum(DynPlan_ReefTons) = 0 then 0 else Sum(DynPlan_Grams)*1000/Sum(DynPlan_ReefTons) end,
	DynPlan_Kg = Sum(DynPlan_Grams),

	DynDayPlan_Adv  = Sum(DynDayPlan_Adv),
	DynDayPlan_ReefAdv  = Sum(DynDayPlan_ReefAdv),
	DynDayPlan_WasteAdv  = Sum(DynDayPlan_WasteAdv),
	DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
	DynDayPlan_ReefTons = Sum(convert(decimal(10,5),DynDayPlan_ReefTons)),
	DynDayPlan_WasteTons = Sum(convert(decimal(10,5),DynDayPlan_wasteTons)),
	DynDayPlan_gt = Case When Sum(DynDayPlan_ReefTons) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_ReefTons) end,
	DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

	DayBook_Adv  = Sum(DayBook_Adv),
	DayBook_ReefAdv  = Sum(DayBook_ReefAdv),
	DayBook_WasteAdv  = Sum(DayBook_WasteAdv),
	DayBook_Tonnes = Sum(DayBook_Tonnes),
	DayBook_ReefTons = Sum(convert(decimal(10,5),DayBook_ReefTons)),
	DayBook_WasteTons = Sum(convert(decimal(10,5),DayBook_WasteTons)),
	DayBook_cmgt = 0,
	DayBook_gt = Case When Sum(DayBook_ReefTons) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_ReefTons) end,
	DayBook_Kg = Sum(DayBook_Grams)/1000,

	DynProgPlan_Adv  = Sum(DynProgPlan_Adv),
	DynProgPlan_ReefAdv  = Sum(DynProgPlan_ReefAdv),
	DynProgPlan_WasteAdv  = Sum(DynProgPlan_WasteAdv),
	DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
	DynProgPlan_ReefTons = Sum(convert(decimal(10,5),DynProgPlan_ReefTons)),
	DynProgPlan_WasteTons = Sum(convert(decimal(10,5),DynProgPlan_WasteTons)),
	DynProgPlan_gt = Case When Sum(DynProgPlan_ReefTons) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_ReefTons) end,
	DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

	ProgBook_Adv  = Sum(ProgBook_Adv),
	ProgBook_ReefAdv  = Sum(ProgBook_ReefAdv),
	ProgBook_WasteAdv  = Sum(ProgBook_WasteAdv),
	ProgBook_Tonnes = Sum(ProgBook_Tonnes),
	ProgBook_ReefTons = Sum(convert(decimal(10,5),ProgBook_ReefTons)),
	ProgBook_WasteTons = Sum(convert(decimal(10,5),ProgBook_WasteTons)),
	ProgBook_gt = Case When Sum(ProgBook_ReefTons) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_ReefTons) end,
	ProgBook_Kg = Sum(ProgBook_Grams)/1000,

	ProgVar_Adv = sum(ProgBook_Adv - DynProgPlan_Adv),
	ProgVar_ReefAdv = sum(ProgBook_ReefAdv - DynProgPlan_ReefAdv),
	ProgVar_WasteAdv = sum(ProgBook_Adv - DynProgPlan_WasteAdv),
	ProgVar_Tonnes = sum(ProgBook_Tonnes - DynProgPlan_Tonnes),
	ProgVar_ReefTons = sum(ProgBook_ReefTons - DynProgPlan_ReefTons),
	ProgVar_WasteTons = sum(ProgBook_wasteTons - DynProgPlan_WasteTons),
	ProgVar_gt = case when sum(ProgBook_Tonnes - DynProgPlan_Tonnes) > 0 then
					 sum(ProgBook_Grams - DynProgPlan_Grams) / 
					 sum(ProgBook_Tonnes - DynProgPlan_Tonnes) end,
	ProgVar_Kg = sum(ProgBook_Grams - DynProgPlan_Grams) / 1000,

	ForeCast_WasteAdv = sum(case when @DefaultShift = 'Y' then ProgBook_WasteAdv/ShiftNo * TotalShifts
					else ProgBook_WasteAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefAdv = sum(case when @DefaultShift = 'Y' then ProgBook_ReefAdv/ShiftNo * TotalShifts
					else ProgBook_ReefAdv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Adv = sum(case when @DefaultShift = 'Y' then ProgBook_Adv/ShiftNo * TotalShifts
					else ProgBook_Adv/ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Tonnes = sum(case when @DefaultShift = 'Y' then ProgBook_Tonnes / ShiftNo * TotalShifts
							else ProgBook_Tonnes / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_ReefTons = sum(case when @DefaultShift = 'Y' then ProgBook_ReefTons / ShiftNo * TotalShifts
							else ProgBook_ReefTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_WasteTons = sum(case when @DefaultShift = 'Y' then ProgBook_WasteTons / ShiftNo * TotalShifts
							else ProgBook_WasteTons / ShiftNo * (TotalShifts-@ShiftsNo) end),
	ForeCast_Kg = sum(case when @DefaultShift = 'Y' then (ProgBook_Grams / 1000) / ShiftNo * TotalShifts
							else (ProgBook_Grams / 1000) / ShiftNo * (TotalShifts-@ShiftsNo) end),

	[Top10] = Sum([Top10]),
	Colour = cast((case when  sum(ProgBook_Adv) = 0  then 1   
						 else 0  end) as varchar(1))


from 
(

	Select 
		sc.SectionID_2 MO,
		w.[Description],
		ShiftNo = Sum(Case when ct.WorkingDay = 'Y' then 1 else 0 end),
		TotalShifts = Avg(convert(numeric(10,0), TotalShifts)),

		LockPlan_Adv = max(Isnull(lp.MetresAdvance,0)),
		LockPlan_ReefAdv = max(Isnull(lp.ReefAdv,0)),
		LockPlan_WasteAdv = max(Isnull(lp.WasteAdv,0)),
		LockPlan_Tonnes = max(Isnull(lp.Tons,0)),
		LockPlan_ReefTons = max(Isnull(lp.ReefTons,0)),
		LockPlan_WasteTons = max(Isnull(lp.WasteTons,0)),
		LockPlan_Grams = max(Isnull(lp.Kg,0)),--kg

		DynPlan_Adv = isnull(max(pm.MetresAdvance),0),
		DynPlan_ReefAdv = isnull(max(pm.ReefAdv),0),
		DynPlan_WasteAdv = isnull(max(pm.WasteAdv),0),
		DynPlan_Tonnes = max(Isnull(pm.Tons,0)),
		DynPlan_ReefTons = max(Isnull(pm.ReefTons,0)),
		DynPlan_WasteTons = max(Isnull(pm.WasteTons,0)),
		DynPlan_Grams = Isnull(max(pm.Kg),0),--kg


		DynProgPlan_Adv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynProgPlan_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynProgPlan_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynProgPlan_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Tons,0) end),
		DynProgPlan_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.ReefTons,0) end),
		DynProgPlan_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.WasteTons,0) end),
		DynProgPlan_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.Grams,0) end),

		ProgBook_Adv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookMetresAdvance,0) end),
		ProgBook_ReefAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefAdv,0) end),
		ProgBook_WasteAdv = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteAdv,0) end),
		ProgBook_Tonnes = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookTons,0) end),
		ProgBook_ReefTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookReefTons,0) end),
		ProgBook_WasteTons = Sum(Case when ct.Calendardate <= @TheDate then Isnull(pd.BookWasteTons,0) end),
		ProgBook_Grams = Sum(Case when ct.Calendardate <= @TheDate then  Isnull(pd.BookGrams,0) end),


		DynDayPlan_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.MetresAdvance,0) end),
		DynDayPlan_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.ReefAdv,0) end),
		DynDayPlan_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.WasteAdv,0) end),
		DynDayPlan_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.Tons,0) end),
		DynDayPlan_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.ReefTons,0) end),
		DynDayPlan_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.WasteTons,0) end),
		DynDayPlan_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.Grams,0) end),

		DayBook_Adv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookMetresAdvance,0) end),
		DayBook_ReefAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookReefAdv,0) end),
		DayBook_WasteAdv = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookWasteAdv,0) end),
		DayBook_Tonnes = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookTons,0) end),
		DayBook_ReefTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookReefTons,0) end),
		DayBook_WasteTons = Sum(Case when ct.Calendardate = @TheDate then Isnull(pd.BookWasteTons,0) end),
		DayBook_Grams = Sum(Case when ct.Calendardate = @TheDate then  Isnull(pd.BookGrams,0) end),

		TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

	from planmonth pm 
	inner join section_complete sc on
		pm.prodmonth = sc.prodmonth and
		pm.sectionid = sc.sectionID and
		pm.Plancode = 'MP'
	left join planmonth lp on
		pm.prodmonth = lp.prodmonth and
		pm.sectionid = lp.sectionID and
		pm.Workplaceid = lp.Workplaceid and
		pm.Activity = lp.Activity and
		lp.PlanCode = 'LP' and
		lp.Locked = 1
	inner join Seccal s on
		sc.prodmonth = s.prodmonth and
		sc.sectionid_1 = s.Sectionid  and
		s.BeginDate <= @TheDate and
		s.enddate >= @TheDate 
	inner join caltype ct on 
		s.Calendarcode = ct.Calendarcode and
		s.BeginDate <= ct.Calendardate and
		s.enddate >= ct.Calendardate
	Inner join Workplace w on
		pm.WorkplaceID = w.WorkplaceID
	left join Planning pd on
		pm.prodmonth = pd.prodmonth and
		pm.sectionid = pd.sectionID and
		pm.Workplaceid = pd.Workplaceid and
		pm.Activity = pd.Activity and
		pm.PLancode = pd.PLancode and
		ct.CalendarDate = pd.Calendardate
	--inner join vw_wp_density de on
	--	pm.workplaceid = de.workplaceid

	Left Join 
	(
		Select top 2000 a.* from PLanmonth a inner join section_complete b on
		a.prodmonth = b.prodmonth and
		a.sectionid = b.sectionID and
		a.Plancode = 'MP'
		inner join 
		Seccal c on
		b.prodmonth = c.prodmonth and
		b.sectionid_1 = c.Sectionid  and
		c.BeginDate <= @TheDate and
		c.enddate >= @TheDate
		inner join Workplace w on
		w.WorkplaceID = a.WorkplaceID
		where a.Activity = 1
		and a.Locked = 1 and
		auth = 'Y'
		and w.Priority = 0
		order by KG desc
	) [Top] on
	pm.prodmonth = [Top].prodmonth and
	pm.sectionid = [Top].sectionID and
	pm.Workplaceid = [Top].Workplaceid and
	pm.Activity = [Top].Activity and
	pm.PLancode = [Top].PLancode 

	where pm.Activity in (1)
	and ct.Calendardate <= @TheDate
	group by sc.SectionID_2, w.[Description]
) Main
Group by MO, [Description]
GO
-- [sp_Load_Tramming_Booking] '201705', 'REA', '2017-05-16'
ALTER Procedure [dbo].[sp_Load_Tramming_Booking] 
@Prodmonth varchar(6),
@Section Varchar(50),
@Bookdate DateTime

AS
select (select Name from OREFLOWENTITIES where OreFlowID = [PLAN].BoxholeID) BH,
[PLAN].BoxHoleID Workplaceid
, [Plan].Tons, isnull(Book.Night, 0)  + isnull(Book.Morning, 0) + isnull(Book.Afternoon, 0) 
* (select theFactor from Code_Hoppers) BookedDailyTons, 
Convert(decimal(10,2),isnull((select sum(Morning + Afternoon + Night) 
from Book_Tramming 
where Prodmonth = @Prodmonth and bookdate <= @Bookdate), 0))
Progressive,
[Plan].HoppersPerShift, Units.Units,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Night, 0)) else isnull(Book_Comments.Night, '') end Night,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Morning, 0)) else isnull(Book_Comments.Morning, '') end Morning,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Afternoon, 0)) else isnull(Book_Comments.Afternoon, '') end Afternoon,
Case when Units.Units = 'Hoppers' then 
Convert(VarChar(20),isnull(Book.Night, 0)  + isnull(Book.Morning, 0) + isnull(Book.Afternoon, 0)) else '' end  Total
from (select a.Prodmonth, b.sectionid_2 SectionID, 
w.BoxholeID BoxHoleID, Sum(Tons)Tons, 
Convert(int, round(Sum(Tons/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from planmonth a inner join WORKPLACE w on a.Workplaceid = w.WorkplaceID inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid and a.PlanCode = 'MP'
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid, Code_Hoppers h
where a.prodmonth = @Prodmonth and b.sectionid_2 = @Section and (w.BoxholeID is not null or w.BoxholeID <> '')
group by a.prodmonth, b.sectionid_2, w.BoxholeID
union
select a.Prodmonth, sectionid_2 SectionID
, w.BoxholeID BoxHoleID, 
Sum(units*(ActualDepth/100)*1.67) Tons, 
Convert(int, round(Sum((units*(ActualDepth/100)*1.67)/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from planmonth_oldgold a inner join WORKPLACE w on a.WorkplaceID = w.WorkplaceID inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid, Code_Hoppers h
where a.prodmonth = @Prodmonth and b.sectionid_2 = @Section and (w.BoxholeID is not null or w.BoxholeID <> '')
group by a.prodmonth, sectionid_2, w.BoxholeID
union
select a.Prodmonth, sectionid_2 SectionID, BoxHoleID Workplaceid,
 Sum(units) Tons, 
Convert(int, round(Sum((units)/totalshifts/3/H.TheFactor)+0.5,0)) Hopperspershift
from PLANMONTH_SUNDRYMINING a inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionid
inner join seccal c on b.prodmonth = c.prodmonth and b.Sectionid_1 = c.Sectionid
inner join SUNDRYMINING_TYPE s on a.SMID = s.SMID and s.UnitBase = 5, Code_Hoppers h
where a.prodmonth = @Prodmonth and sectionid_2 = @Section and (BoxHoleID is not null or BoxHoleID <> '')
group by a.prodmonth, sectionid_2, BoxHoleID) [Plan] inner join 
(select a.PRODMONTH, SECTIONID_2, NAME_2, CALENDARDATE, min(BeginDate) BEGINDATE, max(ENDDATE) ENDDATE, max(WORKINGDAY) WORKINGDAY
from seccal a inner join SECTION_COMPLETE b on a.prodmonth = b.prodmonth and a.sectionid = b.sectionID_1
inner join CALTYPE c on a.CalendarCode = c.CalendarCode and a.BeginDate <= c.CALENDARDATE and a.ENDDATE >= c.CALENDARDATE
Where a.prodmonth = @Prodmonth and SECTIONID_2 = @Section group by a.PRODMONTH, SECTIONID_2, NAME_2, CALENDARDATE) Cal on
[Plan].Prodmonth = Cal.PRODMONTH and [Plan].Sectionid = Cal.SECTIONID_2
left join Book_Tramming Book on [Plan].prodmonth = Book.Prodmonth and [Plan].Sectionid = Book.sectionid and
[Plan].BoxHoleID = Book.Workplaceid and cal.CALENDARDATE = Book.Bookdate
left join Book_Tramming_Comments Book_Comments on [Plan].prodmonth = Book_Comments.Prodmonth and [Plan].Sectionid = Book_Comments.sectionid and
[Plan].BoxHoleID = Book_Comments.Workplaceid and cal.CALENDARDATE = Book_Comments.Bookdate
left join WORKPLACE wp on [Plan].BoxHoleID = wp.WORKPLACEID,
(select 1 Orderby, 'Hoppers' Units) Units
where [Plan].prodmonth = @Prodmonth and Cal.SECTIONID_2 = @Section and Cal.CALENDARDATE = @Bookdate
and [Plan].BoxHoleID != ''
order by Orderby
GO

-- [sp_DailyGradeReport] '2017-07-10', 2, 'Y', 1330, 860, 'T'
ALTER Procedure [dbo].[sp_DailyGradeReport]
--Declare
@TheDate DateTime,
@ShiftsNo int,
@DefaultShift varchar(1),
@PayLimit Int,
@CutOffGrade Int,
@TopPanels varchar(1)

AS
if @TopPanels = 'G'
Begin

Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then '      '+Description
when isnull(MO,'') <> '' and isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 1
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(MO,'') <> '' and isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(MO,'') <> '' and isnull(Description,'') <> '' then 'Total '+MO
when isnull(MO,'') <> '' and isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
Select 
MO = tp.SectionID,
e.Description,
ShiftNo = Sum(Case when d.WorkingDay = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*e.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * e.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * e.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*e.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * e.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * e.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * e.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * e.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
inner join TOPPANELSSELECTED tp
on e.WorkplaceID = tp.WorkplaceID
Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by tp.SectionID, b.sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
end

if @TopPanels = 'T'
begin
Select 
Host_Name(), 
ROW_NUMBER() OVER(ORDER BY MO Desc, Description Desc) AS RowID,
Detail = Case 
when isnull(Description,'') <> '' then '      '+Description
when isnull(Description,'') <> '' then '      '+Description
when isnull(Description,'') = '' then '  Total '+MO
end,
Flag = 
Case 
when isnull(Description,'') <> '' then 1
when isnull(Description,'') = '' then 2
else 3
end,
Top10 = Case when isnull(Description,'') <> '' then cast(Top10 as Varchar(10)) else '' end,
ShiftNo = Case when isnull(Description,'') <> '' then cast(ShiftNo as Varchar(10)) else '' end,
TotalShifts = Case when isnull(Description,'') <> '' then cast(TotalShifts as Varchar(10)) else '' end,
LockPlan_SQM,
LockPlan_Tonnes,
LockPlan_cmgt,
LockPlan_gt,
LockPlan_Kg,

DynPlan_SQM,
DynPlan_Tonnes,
DynPlan_cmgt,
DynPlan_gt,
DynPlan_Kg,

DynDayPlan_SQM,
DynDayPlan_Tonnes,
DynDayPlan_cmgt,
DynDayPlan_gt,
DynDayPlan_Kg,

DayBook_SQM,
DayBook_Tonnes,
DayBook_cmgt,
DayBook_gt,
DayBook_Kg,

DynProgPlan_SQM,
DynProgPlan_Tonnes,
DynProgPlan_cmgt,
DynProgPlan_gt,
DynProgPlan_Kg,

ProgBook_SQM,
ProgBook_Tonnes,
ProgBook_cmgt,
ProgBook_gt,
ProgBook_Kg,

ProgVar_SQM = ProgBook_SQM-DynProgPlan_SQM,
ProgVar_Tonnes = ProgBook_Tonnes-DynProgPlan_Tonnes,
ProgVar_cmgt = ProgBook_cmgt-DynProgPlan_cmgt,
ProgVar_gt = ProgBook_gt-DynProgPlan_gt,
ProgVar_Kg = ProgBook_Kg-DynProgPlan_Kg,

ForeCast_SQM = case when @DefaultShift = 'Y' then ProgBook_SQM/ShiftNo * TotalShifts
					else ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo) end,
ForeCast_Tonnes = case when @DefaultShift = 'Y' then ProgBook_Tonnes/ShiftNo * TotalShifts
						else ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo) end,


ForeCast_cmgt = case when ProgBook_SQMDens = 0 then 0 
				 when @DefaultShift = 'Y' then
					((ProgBook_Kg * 1000) / ShiftNo * TotalShifts * 100) / (ProgBook_SQMDens / ShiftNo * TotalShifts * 100)
				else  ((ProgBook_Kg * 1000) / ShiftNo * (TotalShifts - @ShiftsNo) * 100) / 
					 (ProgBook_SQMDens / ShiftNo * (TotalShifts - @ShiftsNo) * 100)
				end,

ForeCast_gt = Case when ProgBook_Tonnes = 0 then 0 
				when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * TotalShifts)
				else ((ProgBook_SQM/ShiftNo * (TotalShifts - @ShiftsNo))*DynPlan_cmgt/100*2.75)/(ProgBook_Tonnes/ShiftNo * (TotalShifts-@ShiftsNo))
end,
ForeCast_Kg = case when @DefaultShift = 'Y' then ((ProgBook_SQM/ShiftNo * TotalShifts)*DynPlan_cmgt/100*2.75)/1000
					else ((ProgBook_SQM/ShiftNo * (TotalShifts-@ShiftsNo))*DynPlan_cmgt/100*2.75)/1000
				end
,@TheDate TheDate,
DynProgPlan_SW,
ProgBook_SW,
DynProgPlan_SQMSW,
ProgBook_SQMSW,
Colour = cast((case when DynPlan_cmgt > @PayLimit and ProgBook_SQM = 0  then 1   
						when ProgBook_SQM < DynProgPlan_SQM and DynPlan_cmgt > @PayLimit then 2  
						 when  ProgBook_SQM > DynProgPlan_SQM and DynPlan_cmgt < @CutOffGrade then 3  
						 else 0  end) as varchar(1)),
OrderBy = 
Case 
when isnull(Description,'') <> '' then 'Total '+MO
when isnull(Description,'') = '' then 'Total '+MO
else 'XXXXX'
end,
ProgBook_SQMDens
From
(
Select 
MO,
Description,
ShiftNo = Min(ShiftNo),
TotalShifts = Convert(Numeric(7,0), Min(TotalShifts)),
LockPlan_SQM  = Sum(LockPlan_SQM),
LockPlan_SQMDens  = Sum(LockPlan_SQMDens),
LockPlan_Tonnes = Sum(LockPlan_Tonnes),
LockPlan_cmgt = max(LockPlan_cmgt),
LockPlan_gt = Case When Sum(LockPlan_Tonnes) = 0 then 0 else Sum(LockPlan_Grams)/Sum(LockPlan_Tonnes) end,
LockPlan_Kg = Sum(LockPlan_Grams)/1000,

DynPlan_SQM  = Sum(DynPlan_SQM),
DynPlan_SQMDens  = Sum(DynPlan_SQMDens),
DynPlan_Tonnes = Sum(DynPlan_Tonnes),
DynPlan_cmgt = max(DynPlan_CMGT),
DynPlan_gt = Case When Sum(DynPlan_Tonnes) = 0 then 0 else Sum(DynPlan_Grams)/Sum(DynPlan_Tonnes) end,
DynPlan_Kg = Sum(DynPlan_Grams)/1000,

DynDayPlan_SQM  = Sum(DynDayPlan_SQM),
DynDayPlan_SQMDens  = Sum(DynDayPlan_SQMDens),
DynDayPlan_Tonnes = Sum(DynDayPlan_Tonnes),
DynDayPlan_cmgt = Case When Sum(DynDayPlan_SQMDens) = 0 then 0 else Sum(DynDayPlan_Grams*100)/Sum(DynDayPlan_SQMDens) end,
DynDayPlan_gt = Case When Sum(DynDayPlan_Tonnes) = 0 then 0 else Sum(DynDayPlan_Grams)/Sum(DynDayPlan_Tonnes) end,
DynDayPlan_Kg = Sum(DynDayPlan_Grams)/1000,

DayBook_SQM  = Sum(DayBook_SQM),
DayBook_Tonnes = Sum(DayBook_Tonnes),
DayBook_cmgt = Case When Sum(DayBook_SQMDens) = 0 then 0 else Sum(DayBook_Grams*100)/Sum(DayBook_SQMDens) end,
DayBook_gt = Case When Sum(DayBook_Tonnes) = 0 then 0 else Sum(DayBook_Grams)/Sum(DayBook_Tonnes) end,
DayBook_Kg = Sum(DayBook_Grams)/1000,

DynProgPlan_SQM  = Sum(DynProgPlan_SQM),
DynProgPlan_SQMDens  = Sum(DynProgPlan_SQMDens),
DynProgPlan_Tonnes = Sum(DynProgPlan_Tonnes),
DynProgPlan_cmgt = Case When Sum(DynProgPlan_SQMDens) = 0 then 0 else Sum(DynProgPlan_Grams*100)/Sum(DynProgPlan_SQMDens) end,
DynProgPlan_gt = Case When Sum(DynProgPlan_Tonnes) = 0 then 0 else Sum(DynProgPlan_Grams)/Sum(DynProgPlan_Tonnes) end,
DynProgPlan_Kg = Sum(DynProgPlan_Grams)/1000,

ProgBook_SQM  = Sum(ProgBook_SQM),
ProgBook_Tonnes = Sum(ProgBook_Tonnes),
ProgBook_cmgt = Case When Sum(ProgBook_SQMDens) = 0 then 0 else Sum(ProgBook_Grams*100)/Sum(ProgBook_SQMDens) end,
ProgBook_gt = Case When Sum(ProgBook_Tonnes) = 0 then 0 else Sum(ProgBook_Grams)/Sum(ProgBook_Tonnes) end,
ProgBook_Kg = Sum(ProgBook_Grams)/1000,
ProgBook_SQMDens = Sum(ProgBook_SQMDens),
[Top10] = Sum([Top10]),
DynProgPlan_SW = Case When Sum(DynProgPlan_SQM) = 0 then 0 else Sum(DynProgPlan_SQMSW)/Sum(DynProgPlan_SQM) end,
ProgBook_SW = Case When Sum(ProgBook_SQM) = 0 then 0 else Sum(ProgBook_SQMSW)/Sum(ProgBook_SQM) end,
DynProgPlan_SQMSW  = Sum(DynProgPlan_SQMSW),
ProgBook_SQMSW  = Sum(ProgBook_SQMSW)

from 
(
Select 
MO = tp.SectionID,
e.Description,
ShiftNo = Sum(Case when d.WorkingDay = 'Y' then 1 else 0 end),
TotalShifts = Avg(TotalShifts),

LockPlan_SQM = Isnull(max(Isnull(LP.SQM,0)),0),
LockPlan_CMGT =Isnull(max(LP.CMGT) ,0),
LockPlan_SQMDens = Isnull(max(Isnull(LP.ReefSQM*e.density,0)),0),
LockPlan_Tonnes = Isnull(max(LP.SQM * (LP.SW / 100) * e.Density) ,0),
LockPlan_Grams = Isnull(max(LP.CMGT * LP.ReefSQM / 100 * e.Density) ,0),

DynPlan_SQM = Isnull(max(a.SQM) ,0),
DynPlan_CMGT =Isnull(max(a.CMGT) ,0),
DynPlan_SQMDens = Isnull(max(Isnull(a.ReefSQM*e.density,0)),0),
DynPlan_Tonnes = Isnull(max(a.SQM * (a.SW / 100) * e.Density) ,0),
DynPlan_Grams = Isnull(max(a.CMGT * a.ReefSQM / 100 * e.Density) ,0),

DynProgPlan_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.SQM,0) end),
DynProgPlan_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynProgPlan_SQMSW = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.ReefSQM*CY.SW,0) end),
DynProgPlan_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				 Isnull(CY.SQM * (CY.SW/100) * e.density,0) end),
DynProgPlan_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

ProgBook_SQM = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookSQM,0) end),
ProgBook_SQMDens = Sum(Case when d.Calendardate <= @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
ProgBook_SQMSW = Sum(Case when d.Calendardate <= @TheDate then Isnull(CY.BookReefSQM*CY.BookSW,0) end),
ProgBook_Tonnes = Sum(Case when d.Calendardate <= @TheDate then 
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
ProgBook_Grams = Sum(Case when d.Calendardate <= @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

DynDayPlan_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.SQM,0) end),
DynDayPlan_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.ReefSQM*e.density,0) end),
DynDayPlan_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.Sqm * (CY.SW/100) * e.density,0) end),
DynDayPlan_Grams = Sum(Case when d.Calendardate = @TheDate then  
					Isnull(CY.ReefSQM*(CY.CMGT/100)*e.Density,0) end),

DayBook_SQM = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookSQM,0) end),
DayBook_SQMDens = Sum(Case when d.Calendardate = @TheDate then  Isnull(CY.BookReefSQM*e.density,0) end),
DayBook_Tonnes = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookSQM * (CY.SW/100) * e.Density ,0) end),
DayBook_Grams = Sum(Case when d.Calendardate = @TheDate then  
				Isnull(CY.BookReefSQM*(CY.CMGT/100)*e.Density,0) end),

TOP10 = Case when Sum([Top].Prodmonth) is null then 0 else 1 end

--,*
 from planmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
left join planmonth LP on
a.prodmonth = lp.prodmonth and
a.sectionid = lp.sectionID and
a.Workplaceid = lp.Workplaceid and
a.Activity = lp.Activity and
lp.PlanCode = 'LP' and
lp.Locked = 1
inner join 
Seccal c on
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
inner join caltype d on 
c.Calendarcode = d.Calendarcode and
c.BeginDate <= d.Calendardate and
c.enddate >= d.Calendardate
Inner join Workplace e on
a.WorkplaceID = e.WorkplaceID
left join Planning CY on
a.prodmonth = CY.prodmonth and
a.sectionid = CY.sectionID and
a.Workplaceid = CY.Workplaceid and
a.Activity = CY.Activity and
a.PLancode = CY.PLancode and
d.CalendarDate = CY.Calendardate
left outer join TOPPANELSSELECTED tp
on b.Prodmonth = tp.Prodmonth
and e.WorkplaceID = tp.WorkplaceID
Left Join 
(Select top 10 a.* from PLanmonth a inner join section_complete b on
a.prodmonth = b.prodmonth and
a.sectionid = b.sectionID and
a.Plancode = 'MP'
inner join 
Seccal c on 
--select * from section_complete
--select * from toppanelsselected
b.prodmonth = c.prodmonth and
b.sectionid_1 = c.Sectionid  and
c.BeginDate <= @TheDate and
c.enddate >= @TheDate 
where Activity = 0
and a.Locked = 1
and isnull(a.IsStopped,'N') ='N'
and auth = 'Y'
order by KG desc) [Top] on
a.prodmonth = [Top].prodmonth and
a.sectionid = [Top].sectionID and
a.Workplaceid = [Top].Workplaceid and
a.Activity = [Top].Activity and
a.PLancode = [Top].PLancode 

where a.Activity in (0,3)
and d.Calendardate <= @TheDate
--and isnull(a.IsStopped,'N') ='N'

group by tp.SectionID, b.Sectionid_2,e.Description) Main
Group by
MO,
Description
with rollup
) Final
end