/****** Object:  Table [dbo].[PLANPROT_FIELDS]    Script Date: 2017/05/18 9:18:31 AM ******/
DROP TABLE [dbo].[PLANPROT_FIELDS]
GO

CREATE TABLE [dbo].[PLANPROT_FIELDS](
	[FieldID] [int] NOT NULL,
	[TemplateID] [int] NOT NULL,
	[FieldName] [varchar](max) NOT NULL,
	[FieldType] [varchar](10) NOT NULL,
	[CalcField] [bit] NULL,
	[ThePos] [varchar](4) NULL,
	[NoCharacters] [varchar](max) NULL,
	[Nolines] [varchar](max) NULL,
	[FieldRequired] [bit] NULL,
	[ParentID] [int] NULL,
 CONSTRAINT [PK_PlanPlot_Fields] PRIMARY KEY CLUSTERED 
(
	[FieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE Procedure [dbo].[sp_Planprot_WorkplaceListToBeApproved] 

 @ProdMonth Numeric(7,0),@SectionID VARCHAR(10), @TemplateID INT,  @ActivityType INT
AS

--DECLARE @ProdMonth Numeric(7,0),@SectionID VARCHAR(10), @TemplateID INT,  @ActivityType INT
--SET @ProdMonth = 201506
--SET @SectionID = '188470'
------SET @WorkplaceID = '19157'
--SET @TemplateID = 16
--SET @ActivityType = 0



SELECT * FROM(
select PRODMONTH,SECTIONID,WORKPLACEID,TemplateID,FieldID,FieldName,Max(CaptureDate)  CaptureDate,DESCRIPTION  from
 (
					SELECT PPD.PRODMONTH,PPD.ActivityType , PPD.SECTIONID ,PPD.WORKPLACEID ,WP.DESCRIPTION,
					PPF.TemplateID, PPF.FieldID  ,PPF.FieldName ,PPD.TheValue ,PPF.FieldRequired,PPD.CaptureDate   FROM [dbo].[PlanProt_Fields] PPF 
					LEFT JOIN 
					 [dbo].[PlanProt_Data] PPD ON PPD.FieldID =PPF.FieldID inner JOIN 
					 dbo .WORKPLACE WP on WP.WORKPLACEID = PPD.WORKPLACEID 
					  inner join PLANMONTH pm on 
					  pm.Activity = PPD.ActivityType and
					  pm.Prodmonth = ppd.Prodmonth and
					  pm.Workplaceid = ppd.Workplaceid
					 where PPD.PRODMONTH = @ProdMonth and  pm.plancode = 'MP') main where main .PRODMONTH =@ProdMonth 
					 and main .TemplateID =@TemplateID and main.SECTIONID  =@SectionID  and main.FieldRequired =1 AND main.ActivityType =@ActivityType
					 group by PRODMONTH,SECTIONID,WORKPLACEID,TemplateID,FieldID,FieldName,DESCRIPTION ) MainData
					 INNER JOIN PlanProt_Data PPD on
					 MainData.PRODMONTH = PPD.PRODMONTH and
					 MainData.SECTIONID = PPD.SECTIONID and
					 MainData.WORKPLACEID = PPD.WORKPLACEID and
					 
					 MainData.FieldID = PPD.FieldID  and
					 MainData.CaptureDate = PPD.CaptureDate where TheValue ='' and PPD.PRODMONTH = @ProdMonth



GO

ALTER Procedure [dbo].[sp_PlanProt_ApproveData]

--DECLARE 
@Prodmonth NUMERIC(7,0), @SectionID VARCHAR(10), @WorkplaceID VARCHAR(30), @TemplateID INT,@ActivityType INT,
@ApprovedBy VARCHAR(30), @ApproveItem VARCHAR(10)

-- spPlanProt_ApproveData 201108,'25884','789955',1,0,'DOLF'

AS

--DECLARE 
--@Prodmonth NUMERIC(7,0), @SectionID VARCHAR(10), @WorkplaceID VARCHAR(30), @TemplateID INT,@ActivityType INT,
--@ApprovedBy VARCHAR(30), @ApproveItem VARCHAR(10)

--SET @ActivityType = 0
--SET @ApprovedBy = 'MINEWARE_dolf'
--SET @Prodmonth = 201706
--SET @SectionID = 'REA'
--SET @TemplateID = 16
--SET @WorkplaceID = 'RE007619'
--SET @ApproveItem = 'YES'



DECLARE @ApprovedDate DATETIME
DECLARE @ApprovedID INT
SET @ApprovedDate = GETDATE()
Declare @MinerSectionID VARCHAR(10)

IF @ApproveItem = 'NO'
BEGIN
-- test if has data
SET @ApprovedID = (SELECT MAX(ApproveID) FROM dbo.PlanProt_DataApproved 
WHERE ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID)
   
      
IF  @ApprovedID > 0 
BEGIN
-- get the ID
SET @ApprovedID = (SELECT ApproveID FROM dbo.PlanProt_DataApproved
WHERE ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID)

      
DELETE from a  FROM PlanProt_DataLocked a Inner join PLANPROT_FIELDS b on
a.FieldID = b.FieldID
Where ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID 

DELETE FROM dbo.PlanProt_DataApproved
WHERE ActivityType = @ActivityType AND
      PRODMONTH = @Prodmonth AND
      WORKPLACEID = @WorkplaceID AND
      SECTIONID = @SectionID AND
      TemplateID = @TemplateID       
END     
  
END

IF @ApproveItem = 'YES'
BEGIN


Select @MinerSectionID = Sectionid from Planmonth where PRODMONTH = @Prodmonth and Workplaceid = @WorkplaceID

INSERT INTO dbo.PlanProt_DataApproved
        ( PRODMONTH ,
          SECTIONID ,
          WORKPLACEID ,
          ActivityType ,
          TemplateID ,
          Approved ,
          ApprovedBy ,
          ApprovedDate
        )
VALUES  ( @Prodmonth , -- PRODMONTH - numeric
          @SectionID , -- SECTIONID - varchar(10)
          @WorkplaceID , -- WORKPLACEID - varbinary(30)
          @ActivityType , -- ActivityType - int
          @TemplateID , -- TemplateID - int
          1 , -- Approved - bit
          @ApprovedBy , -- ApprovedBy - varchar(30)
          @ApprovedDate  -- ApprovedDate - datetime
        )


SET @ApprovedID = (
SELECT ApproveID FROM dbo.PlanProt_DataApproved
WHERE PRODMONTH = @Prodmonth AND
      SECTIONID = @SectionID AND
      WORKPLACEID = @WorkplaceID AND
      ActivityType = @ActivityType AND
      TemplateID = @TemplateID)
      
INSERT INTO dbo.PlanProt_DataLocked
        ( ApprovedID ,
          PRODMONTH ,
          SECTIONID ,
          WORKPLACEID ,
          FieldID ,
          TheValue ,
          ActivityType ,
          CaptureDate
        )   


SELECT @ApprovedID,@Prodmonth PRODMONTH,@SectionID SECTIONID,PPD.WORKPLACEID,PPD.FieldID,
(SELECT CASE WHEN FieldName = 'Signature Date' Then cast(@ApprovedDate as varchar(max)) else PPD.TheValue end TheValue FROM PlanProt_Fields WHERE FieldID = PPD.FieldID)
,PPD.ActivityType,PPD.CaptureDate FROM dbo.PlanProt_Data PPD
INNER JOIN dbo.PlanProt_Fields PPF ON
PPD.FieldID = PPF.FieldID
WHERE PPF.TemplateID = @TemplateID AND
      PPD.ActivityType = @ActivityType AND
      PPD.PRODMONTH = @Prodmonth AND
      PPD.SECTIONID = @SectionID AND
      PPD.WORKPLACEID = @WorkplaceID 

END                                                                             

DECLARE @CMGT numeric(10,3),@CW numeric(10,3),@SW numeric(10,3),@KG numeric(10,3),@IdealSW numeric(10,3),@CMKGT numeric(10,3)

IF @TemplateID= 16 AND @ActivityType = 0
BEGIN
SELECT @CMGT = theUpdate.CMGT,
	   @CMKGT= theUpdate.CMKGT,	
                        @CW = theUpdate.ChannelW,
                        @SW = theUpdate.ActualSW,
						@KG = dbo.CalcGoldBrokenSQM(theUpdate.SQM ,theUpdate.CMGT,theUpdate.WORKPLACEID)  + 
						dbo.CalcGoldBrokenCUB(theUpdate.CUBICMETRES,theUpdate.CMGT,theUpdate.ActualSW,theUpdate.WORKPLACEID),
						@IdealSW = theUpdate.IdealSW

FROM( SELECT pp.Prodmonth,WORKPLACEID,pp.Sectionid,Sectionid_2,SQM,CUBICMETRES,
      CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMGT END CMGT,
	  CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCMKGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMKGT END CMKGT,
      CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CW END ChannelW,
      CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE SW END ActualSW,
	  CASE WHEN dbo.IsEvalApproved(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
	        THEN dbo.[GetApprovedIdealSW](PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE IdealSW END IdealSW           
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
WHERE Activity IN (0)  AND
      b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and Workplaceid = @WorkplaceID) theUpdate


DECLARE @SampCountCM INT


SET @SampCountCM = 
(SELECT COUNT(Prodmonth) FROM dbo.vw_Kriging_Latest 
WHERE Prodmonth = @Prodmonth AND
      WORKPLACEID = @WorkplaceID and WeekNo = 1)

IF @SampCountCM = 1      
BEGIN
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end

WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth and WeekNo  =1


Update PP set 
--Select
CMGT = isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0), 
CW = isnull(dbo.GetApprovedCW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
SW = isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
Kg = (ReefSQM * isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0) / 100 * c.Density)/1000,
Tons = (SQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * c.Density),
ReefTons = (ReefSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * c.Density),
WasteTons = (WasteSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * c.Density) 
                  
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
		 inner join WORKPLACE c on
		 pp.Workplaceid = c.Workplaceid
WHERE pp.Activity IN (0)  AND
      --b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and pp.Workplaceid = @WorkplaceID
END

IF @SampCountCM = 0
BEGIN
INSERT INTO dbo.KRIGING
        ( Prodmonth ,
		  SectionID,Activity,
          WORKPLACEID ,
          WeekNo           
        )
VALUES  ( @Prodmonth , -- Prodmonth - numeric
          @MinerSectionID,
		  @ActivityType,
          @WorkplaceID , -- WORKPLACEID - varchar(12)
          1  -- SAMPLINGDATE - datetime
         
        )
               
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end
                          
WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth  and WeekNo  =1


Update PP set 
--Select
CMGT = isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0), 
CW = isnull(dbo.GetApprovedCW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
SW = isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0),
Kg = (ReefSQM * isnull(dbo.GetApprovedCMGT(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2),0) / 100 * c.Density)/1000,
Tons = (SQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * c.Density),
ReefTons = (ReefSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * c.Density),
WasteTons = (WasteSQM * isnull(dbo.GetApprovedSW(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2)/100,0) * c.Density) 
                  
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
		 inner join WORKPLACE c on
		 pp.Workplaceid = c.Workplaceid
WHERE pp.Activity IN (0)  AND
      --b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and pp.Workplaceid = @WorkplaceID
      
END

END

IF @TemplateID= 14 AND @ActivityType = 1
BEGIN
SELECT @CMGT = theUpdate.CMGT,
	   @CMKGT= theUpdate.CMKGT,	
                        @CW = theUpdate.ChannelW--,


FROM( SELECT pp.Prodmonth,WORKPLACEID,pp.Sectionid,Sectionid_2,SQM,CUBICMETRES,
      CASE WHEN dbo.IsEvalApproved_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.[GetApprovedCMGT_Dev](PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMGT END CMGT,
	  CASE WHEN dbo.IsEvalApproved_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.[GetApprovedCMKGT_Dev](PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CMKGT END CMKGT,
      CASE WHEN dbo.IsEvalApproved_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) = 1 
           THEN dbo.GetApprovedCW_Dev(PP.Prodmonth,PP.WORKPLACEID,b.SECTIONID_2) ELSE CW END ChannelW 
       
         FROM Planmonth PP inner join SECTION_COMPLETE b on
		 pp.Prodmonth = b.Prodmonth and
		 pp.Sectionid = b.SectionID
WHERE Activity IN (1)  AND
      b.Sectionid_2 = @SectionID AND
      PP.Prodmonth = @Prodmonth AND Plancode = 'MP' and Workplaceid = @WorkplaceID) theUpdate


--DECLARE @SampCountCM INT


SET @SampCountCM = 
(SELECT COUNT(Prodmonth) FROM dbo.vw_Kriging_Latest 
WHERE Prodmonth = @Prodmonth AND
      WORKPLACEID = @WorkplaceID)

IF @SampCountCM = 1      
BEGIN
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end

WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth and WeekNo  =1

END

IF @SampCountCM = 0
BEGIN
INSERT INTO dbo.KRIGING
        ( Prodmonth ,
		  SectionID,Activity,
          WORKPLACEID ,
          WeekNo           
        )
VALUES  ( @Prodmonth , -- Prodmonth - numeric
          @MinerSectionID,
		  @ActivityType,
          @WorkplaceID , -- WORKPLACEID - varchar(12)
          1  -- SAMPLINGDATE - datetime
         
        )


               
UPDATE dbo.KRIGING SET  StopeWidth = @SW, 
                        ChannelWidth = @CW,
                        CMGT = @CMGT,
                        gt = case when (@SW = 0) or (@SW is null) then 0 else @CMGT / @SW end

WHERE WORKPLACEID = @WorkplaceID AND
      Prodmonth = @Prodmonth and WeekNo  =1
end
end

GO
INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('1','Latest Safety Report Number','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('2','Engineering/Equipping','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('3','Latest RME Report Number','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('4','Latest RME Recommendation Number','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('5','Latest Ventilation Report Number','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('6','Latest Geology Report Number','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('7','Latest Survey Note Number','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('8','Human Resources','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('9','Finance','0','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('11','Latest Safety Report Number','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('14','Valuation','1','','','1','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('16','Valuation','0','','','1','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('22','Engineering/Equipping','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('33','Latest RME Report Number','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('44','Latest RME Recommendation Number','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('55','Latest Ventilation Report Number','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('66','Latest Geology Report Number','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('77','Latest Survey Note Number','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('88','Human Resources','1','','','0','0')
GO

INSERT INTO PLANPROT_TEMPLATE([TemplateID],[TemplateName],[Activity],[User1],[User2],[ApprovalRequired],[Human_Resources])VALUES('99','Finance','1','','','0','0')
GO
INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1401','14','Channel Width (cm)','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1402','14','Raise on Reef','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1403','14','Progressive Value (cmg/t)','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1404','14','Comments/Actions','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1405','14','Valuation Officer','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1406','14','Signature date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1407','14','Progressive Value(cmkg/t)','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1601','16','Ideal stope width (cm)','2','1',NULL,NULL,NULL,'1',NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1602','16','Channel Width (cm)','2','0',NULL,NULL,NULL,'1',NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1603','16','Barricades','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1604','16','Value (cm.g/t)','3','0',NULL,NULL,NULL,'1',NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1605','16','Plan Stope Width (cm)','2','0',NULL,NULL,NULL,'1',NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1606','16','Last Visit Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1607','16','EVALUATION OFFICER: Name','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1608','16','Signature date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1609','16','Comments','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1610','16','Planning Stage','6','0',NULL,NULL,NULL,'0',NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1611','16','Ore Reserves Status','6','0',NULL,NULL,NULL,'0',NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('1612','16','Value(cmkg/t)','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10001','1','Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10002','1','Last Accident','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10003','1','A` Hazards','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10004','1','B` Hazards','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10005','1','C` Hazards','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10006','1','Dressing Cases','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10007','1','Lost time','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10008','1','Serious','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10009','1','White Flag Days','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10010','1','Physical Condition Rating','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10011','1','Standards Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10012','1','Explosives Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10013','1','Pre Conditioning Drilled To Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10014','1','Temp & Permanent Support & Resin Bolts To Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10015','1','Stop & Fixes Conceded','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10016','1','Explosives Stored & Controlled As Per Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10017','1','Winches , Rigging , Grizzlies , Tip Barricades, Signalling Devices To Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10018','1','Appointments Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10019','1','Early Shift','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10020','1','Late Shift','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10021','1','Declaration','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10022','1','Other Safety Concerns in Panel','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10023','1','Risk Assessments Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10024','1','PTO''s Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10025','1','Planned Inspections Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10026','1','On the Job Traininng','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10027','1','In-Stope Nets Installed Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10028','1','In-Stope Nets Installed  Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10029','1','In-Stope Resin Bolts Installed Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10030','1','In-Stope Resin Bolts Installed Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10031','1','Heading To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10032','1','Heading To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10033','1','Escape gully To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10034','1','Escape gully To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10035','1','Welded Mesh in Both Gullies Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10036','1','Welded Mesh in Both Gullies Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10037','1','Tip Area To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10038','1','Tip Area To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10039','1','Slusher Gully Utilizes Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10040','1','Slusher Gully Utilizes Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10041','1','Water Controls Utilized Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10042','1','Water Controls Utilized Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10043','1','Proto Valve Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10044','1','Proto Valve Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10045','1','Face Winch Distance From Face Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10046','1','Face Winch Distance From Face Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10047','1','Services Distance from Face Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10048','1','Services Distance from Face Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10049','1','Any Services in Back Areas Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10050','1','Any Services in Back Areas Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10051','1','Box Front To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10052','1','Box Front To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10053','1','Old Areas Baricaded off Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10054','1','Old Areas Baricaded off Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10055','1','Secondary Waiting Place Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10056','1','Secondary Waiting Place Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10057','1','Travelling Way Equipped Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10058','1','Travelling Way Equipped Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10059','1','Pre-Developed Gullies (Ledging Phase) Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10060','1','Pre-Developed Gullies (Ledging Phase) Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10061','1','Mono Which Route Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('10062','1','Mono Which Route Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11001','11','Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11002','11','Last Accident','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11003','11','A` Hazards','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11004','11','B` Hazards','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11005','11','C` Hazards','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11006','11','Dressing Cases','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11007','11','Lost time','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11008','11','Serious','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11009','11','White Flag Days','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11010','11','Physical Condition Rating','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11011','11','Standards Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11012','11','Explosives Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11013','11','Pre Conditioning Drilled To Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11014','11','Temp & Permanent Support & Resin Bolts To Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11015','11','Stop & Fixes Conceded','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11016','11','Explosives Stored & Controlled As Per Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11017','11','Winches , Rigging , Grizzlies , Tip Barricades, Signalling Devices To Std','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11018','11','Appointments Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11019','11','Early Shift','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11020','11','Late Shift','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11021','11','Declaration','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11022','11','Other Safety Concerns in Panel','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11023','11','Risk Assessments Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11024','11','PTO''s Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11025','11','Planned Inspections Done','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11026','11','On the Job Traininng','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11027','11','In-Stope Nets Installed Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11028','11','In-Stope Nets Installed  Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11029','11','In-Stope Resin Bolts Installed Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11030','11','In-Stope Resin Bolts Installed Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11031','11','Heading To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11032','11','Heading To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11033','11','Escape gully To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11034','11','Escape gully To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11035','11','Welded Mesh in Both Gullies Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11036','11','Welded Mesh in Both Gullies Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11037','11','Tip Area To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11038','11','Tip Area To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11039','11','Slusher Gully Utilizes Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11040','11','Slusher Gully Utilizes Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11041','11','Water Controls Utilized Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11042','11','Water Controls Utilized Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11043','11','Proto Valve Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11044','11','Proto Valve Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11045','11','Face Winch Distance From Face Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11046','11','Face Winch Distance From Face Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11047','11','Services Distance from Face Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11048','11','Services Distance from Face Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11049','11','Any Services in Back Areas Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11050','11','Any Services in Back Areas Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11051','11','Box Front To Std. Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11052','11','Box Front To Std. Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11053','11','Old Areas Baricaded off Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11054','11','Old Areas Baricaded off Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11055','11','Secondary Waiting Place Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11056','11','Secondary Waiting Place Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11057','11','Travelling Way Equipped Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11058','11','Travelling Way Equipped Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11059','11','Pre-Developed Gullies (Ledging Phase) Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11060','11','Pre-Developed Gullies (Ledging Phase) Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11061','11','Mono Which Route Y/N','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('11062','11','Mono Which Route Action Date','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20001','2','Centre Gully Winch','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20002','2','Centre Gully Winch','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20003','2','Face Winch','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20004','2','Face Winch','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20005','2','Winch Cubbies Fire Proofed','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20006','2','Winch Cubbies Fire Proofed','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20007','2','Lights installed','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20008','2','Lights installed','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20009','2','Winch Move Planned','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20010','2','Winch Move Planned','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20011','2','Mono Wich Move Planned','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20012','2','Mono Wich Move Planned','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20013','2','Power Extensions Required','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20014','2','Power Extensions Required','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20015','2','Machinery Maintenace Program','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20016','2','Machinery Maintenace Program','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('20017','2','Equipment Required for new Month','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22001','22','Centre Gully Winch','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22002','22','Centre Gully Winch','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22003','22','Face Winch','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22004','22','Face Winch','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22005','22','Winch Cubbies Fire Proofed','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22006','22','Winch Cubbies Fire Proofed','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22007','22','Lights installed','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22008','22','Lights installed','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22009','22','Winch Move Planned','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22010','22','Winch Move Planned','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22011','22','Mono Wich Move Planned','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22012','22','Mono Wich Move Planned','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22013','22','Power Extensions Required','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22014','22','Power Extensions Required','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22015','22','Machinery Maintenace Program','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22016','22','Machinery Maintenace Program','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('22017','22','Equipment Required for new Month','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30001','3','RMR','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30002','3','Energy Release Rate','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30003','3','Seismic Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30004','3','Lead & Lag','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30005','3','FOG Seismic','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30006','3','Brows present','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30007','3','Face Shape','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30008','3','Crush Pillar Req','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30009','3','Secondary Support Req','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30010','3','Additional Support Req','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('30011','3','Special Area','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33001','33','RMR','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33002','33','Energy Release Rate','4','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33003','33','Seismic Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33004','33','Lead & Lag','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33005','33','FOG Seismic','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33006','33','Brows present','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33007','33','Face Shape','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33008','33','Crush Pillar Req','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33009','33','Secondary Support Req','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33010','33','Additional Support Req','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('33011','33','Special Area','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('40001','4','Latest RME Recommendation Number','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('40002','4','Support Compliance %','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('40003','4','Panel Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('44001','44','Latest RME Recommendation Number','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('44002','44','Support Compliance %','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('44003','44','Panel Rating','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50001','5','Velocity','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50002','5','Wet Bulb ','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50003','5','Dry Bulb ','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50004','5','V/Controls','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50005','5','Noise','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50006','5','AU','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50007','5','GDI','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50008','5','Drills','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50009','5','Dust','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50010','5','CH4','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50011','5','Leackages','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50012','5','Heat Sickness Cases','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50013','5','Refuge Bay No','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50014','5','Life Sustainable','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50015','5','Distance from Workplace','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50016','5','Hearing Protection','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('50017','5','Rating','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55001','55','Velocity','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55002','55','Wet Bulb ','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55003','55','Dry Bulb ','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55004','55','V/Controls','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55005','55','Noise','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55006','55','AU','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55007','55','GDI','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55008','55','Drills','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55009','55','Dust','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55010','55','CH4','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55011','55','Leackages','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55012','55','Heat Sickness Cases','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55013','55','Refuge Bay No','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55014','55','Life Sustainable','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55015','55','Distance from Workplace','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55016','55','Hearing Protection','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('55017','55','Rating','2','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60001','6','Dykes (Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60002','6','Faults > 0.5m','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60003','6','Faults < 0.5m','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60004','6','Faults/Dykes < 400','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60005','6','Quartz veins/Joints','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60006','6','Dip of Reef','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60007','6','Argillite exposure (Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60008','6','FOG Gravity(Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60009','6','RIH (Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60010','6','RIF(Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60011','6','Rolling(Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60012','6','Extent of roll (high-Low)','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60013','6','SW Recommended','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('60014','6','Reef Detector no.','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66001','66','Dykes (Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66002','66','Faults > 0.5m','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66003','66','Faults < 0.5m','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66004','66','Faults/Dykes < 400','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66005','66','Quartz veins/Joints','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66006','66','Dip of Reef','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66007','66','Argillite exposure (Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66008','66','FOG Gravity(Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66009','66','RIH (Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66010','66','RIF(Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66011','66','Rolling(Yes/No)','6','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66012','66','Extent of roll (high-Low)','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66013','66','SW Recommended','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('66014','66','Reef Detector no.','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('70001','7','Stop Note No.','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('70002','7','Holing Note No.','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('70003','7','30m Rule','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('70004','7','Stopping Distance To Pillar','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('70005','7','Adhering To Gully Lines','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('70006','7','Adhering To Limit Lines','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('70007','7','Actual Stoping Width','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('77001','77','Stop Note No.','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('77002','77','Holing Note No.','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('77003','77','30m Rule','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('77004','77','Stopping Distance To Pillar','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('77005','77','Adhering To Gully Lines','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('77006','77','Adhering To Limit Lines','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('77007','77','Actual Stoping Width','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80001','8','Labour Compl','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80002','8','Plan','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80003','8','Actual','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80004','8','Employee''s Using Medication','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80005','8','Chronic Medication','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80006','8','TB Treatment','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80007','8','Efficiencies','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80008','8','m²/Crew','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('80009','8','m²/Man','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88001','88','Labour Compl','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88002','88','Plan','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88003','88','Actual','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88004','88','Employee''s Using Medication','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88005','88','Chronic Medication','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88006','88','TB Treatment','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88007','88','Efficiencies','5','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88008','88','m²/Crew','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('88009','88','m²/Man','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90001','9','Prev Mth Labour Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90002','9','Prev Mth Explosives Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90003','9','Prev Mth Drilling Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90004','9','Prev Mth Support Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90005','9','Prev Mth Cleaining Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90006','9','Prev Mth Other Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90007','9','Current Mth Labour Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90008','9','Current Mth Explosives Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90009','9','Current Mth Drilling Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90010','9','Current Mth Support Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90011','9','Current Mth Cleaining Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90012','9','Current Mth Other Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90013','9','Planned Labour Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90014','9','Planned Explosives Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90015','9','Planned Drilling Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90016','9','Planned Support Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90017','9','Planned Cleaining Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('90018','9','Planned Other Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99001','99','Prev Mth Labour Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99002','99','Prev Mth Explosives Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99003','99','Prev Mth Drilling Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99004','99','Prev Mth Support Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99005','99','Prev Mth Cleaining Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99006','99','Prev Mth Other Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99007','99','Current Mth Labour Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99008','99','Current Mth Explosives Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99009','99','Current Mth Drilling Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99010','99','Current Mth Support Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99011','99','Current Mth Cleaining Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99012','99','Current Mth Other Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99013','99','Planned Labour Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99014','99','Planned Explosives Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99015','99','Planned Drilling Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99016','99','Planned Support Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99017','99','Planned Cleaining Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

INSERT INTO PLANPROT_Fields([FieldID],[TemplateID],[FieldName],[FieldType],[CalcField],[ThePos],[NoCharacters],[Nolines],[FieldRequired],[ParentID])VALUES('99018','99','Planned Other Cost/m2','3','0',NULL,NULL,NULL,NULL,NULL)
GO

Create Procedure dbo.sp_PlanProd_WPList
--Declare
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Numeric(7)
as

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'
--SET @Activity = 0


			SELECT pp.Prodmonth,pp.Workplaceid,w.Description WorkplaceDesc,SC.NAME_2 MO
  
			FROM PLANMONTH  PP 
			INNER JOIN dbo.SECTION_COMPLETE SC ON
			pp.Prodmonth = SC.PRODMONTH AND
			pp.Sectionid = SC.SECTIONID 
			inner join Workplace W on
			pp.Workplaceid = w.Workplaceid
			WHERE 
			    PP.Prodmonth = @Prodmonth AND
				sc.Sectionid_2 = @SectionID_2 AND
				pp.Activity = @Activity  and
				pp.PlanCode = 'MP'       --done
Go 
				 
Create Procedure dbo.sp_PlanProd_Saftey_STP	  --01			
--Declare																					
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																						

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													

SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 1) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10001,SC.Sectionid_2)[Date],
	   dbo.GetPlanProdFieldName(10001) [Date Name],
       dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10002,SC.Sectionid_2)[Last Accident],
	   dbo.GetPlanProdFieldName(10002) [Last Accident Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10003,SC.Sectionid_2)[A` Hazards],
	   dbo.GetPlanProdFieldName(10003) [A` Hazards Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10004,SC.Sectionid_2)[B` Hazards],
	   dbo.GetPlanProdFieldName(10004) [B` Hazards Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10005,SC.Sectionid_2)[C` Hazards],
	   dbo.GetPlanProdFieldName(10005) [C` Hazards Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10006,SC.Sectionid_2)[Dressing Cases],
	   dbo.GetPlanProdFieldName(10006) [Dressing Cases Name], 
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10007,SC.Sectionid_2)[Lost time],
	   dbo.GetPlanProdFieldName(10007) [Lost time Name],  
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10008,SC.Sectionid_2)[Serious],
	   dbo.GetPlanProdFieldName(10008) [Serious Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10009,SC.Sectionid_2)[White Flag Days],
	   dbo.GetPlanProdFieldName(10009) [White Flag Days Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10010,SC.Sectionid_2)[Physical Condition Rating],
	   dbo.GetPlanProdFieldName(10010) [Physical Condition Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10011,SC.Sectionid_2)[Standards Rating  Rating],
	   dbo.GetPlanProdFieldName(10011) [Standards Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10012,SC.Sectionid_2)[Explosives Rating],
	   dbo.GetPlanProdFieldName(10012) [Explosives Rating Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10013,SC.Sectionid_2)[Pre Conditioning Drilled To Std],
	   dbo.GetPlanProdFieldName(10013) [Pre Conditioning Drilled To Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10014,SC.Sectionid_2)[Temp & Permanent Support & Resin Bolts To Std],
	   dbo.GetPlanProdFieldName(10014) [Temp & Permanent Support & Resin Bolts To Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10015,SC.Sectionid_2)[Stop & Fixes Conceded],
	   dbo.GetPlanProdFieldName(10015) [Stop & Fixes Conceded Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10016,SC.Sectionid_2)[Explosives Stored & Controlled As Per Std],
	   dbo.GetPlanProdFieldName(10016) [Explosives Stored & Controlled As Per Std Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10017,SC.Sectionid_2)[Winches , Rigging, ect. ],
	   dbo.GetPlanProdFieldName(10017) [Winches , Rigging, ect.  Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10018,SC.Sectionid_2)[Appointments Done],
	   dbo.GetPlanProdFieldName(10018) [Appointments Done Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10019,SC.Sectionid_2)[Early Shift],
	   dbo.GetPlanProdFieldName(10019) [Early Shift Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10020,SC.Sectionid_2)[Late Shift],
	   dbo.GetPlanProdFieldName(10020) [Late Shift Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10021,SC.Sectionid_2)[Declaration],
	   dbo.GetPlanProdFieldName(10021) [Declaration Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10022,SC.Sectionid_2)[Other Safety Concerns in Panel],
	   dbo.GetPlanProdFieldName(10022) [Other Safety Concerns in Panel Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10023,SC.Sectionid_2)[Risk Assessments Done],
	   dbo.GetPlanProdFieldName(10023) [Risk Assessments Done Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10024,SC.Sectionid_2)[PTO's Done],
	   dbo.GetPlanProdFieldName(10024) [PTO's Done Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10025,SC.Sectionid_2)[Planned Inspections Done],
	   dbo.GetPlanProdFieldName(10025) [Planned Inspections Done Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10026,SC.Sectionid_2)[On the Job Traininng],
	   dbo.GetPlanProdFieldName(10026) [On the Job Traininng Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10027,SC.Sectionid_2)[In-Stope Nets Installed],
	   dbo.GetPlanProdFieldName(10027) [In-Stope Nets Installed Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10028,SC.Sectionid_2)[In-Stope Nets Installed  Action Date],
	   dbo.GetPlanProdFieldName(10028) [In-Stope Nets Installed  Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10029,SC.Sectionid_2)[In-Stope Resin Bolts Installed],
	   dbo.GetPlanProdFieldName(10029) [In-Stope Resin Bolts Installed Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10030,SC.Sectionid_2)[In-Stope Resin Bolts Installed Action Date],
	   dbo.GetPlanProdFieldName(10030) [In-Stope Resin Bolts Installed Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10031,SC.Sectionid_2)[Heading To Std.],
	   dbo.GetPlanProdFieldName(10031) [Heading To Std. Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10032,SC.Sectionid_2)[Heading To Std  Action Date],
	   dbo.GetPlanProdFieldName(10032) [Heading To Std  Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10033,SC.Sectionid_2)[Escape gully To Std. ],
	   dbo.GetPlanProdFieldName(10033) [Escape gully To Std.  Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10034,SC.Sectionid_2)[Escape gully To Std. Action Date],
	   dbo.GetPlanProdFieldName(10034) [In-Stope Nets Installed  Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10035,SC.Sectionid_2)[Welded Mesh in Both Gullies],
	   dbo.GetPlanProdFieldName(10035) [Welded Mesh in Both Gullies Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10036,SC.Sectionid_2)[Welded Mesh in Both Gullies Action Date],
	   dbo.GetPlanProdFieldName(10036) [Welded Mesh in Both Gullies Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10037,SC.Sectionid_2)[Tip Area To Std.],
	   dbo.GetPlanProdFieldName(10037) [Tip Area To Std. Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10038,SC.Sectionid_2)[Tip Area To Std. Action Date],
	   dbo.GetPlanProdFieldName(10038) [Tip Area To Std. Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10039,SC.Sectionid_2)[Slusher Gully Utilizes ],
	   dbo.GetPlanProdFieldName(10039) [Slusher Gully Utilizes Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10040,SC.Sectionid_2)[Slusher Gully Utilizes Action Date],
	   dbo.GetPlanProdFieldName(10040) [Slusher Gully Utilizes Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10041,SC.Sectionid_2)[Water Controls Utilized],
	   dbo.GetPlanProdFieldName(10041) [Water Controls Utilized Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10042,SC.Sectionid_2)[Water Controls Utilized Action Date],
	   dbo.GetPlanProdFieldName(10042) [Water Controls Utilized Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10043,SC.Sectionid_2)[Proto Valve],
	   dbo.GetPlanProdFieldName(10043) [Proto Valve Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10044,SC.Sectionid_2)[Proto Valve Action Date],
	   dbo.GetPlanProdFieldName(10044) [Proto Valve Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10045,SC.Sectionid_2)[Face Winch Distance From Face],
	   dbo.GetPlanProdFieldName(10045) [Face Winch Distance From Face Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10046,SC.Sectionid_2)[Face Winch Distance From Face Action Date],
	   dbo.GetPlanProdFieldName(10046) [Face Winch Distance From Face Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10047,SC.Sectionid_2)[Services Distance from Face],
	   dbo.GetPlanProdFieldName(10047) [Services Distance from Face Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10048,SC.Sectionid_2)[Services Distance from Face Action Date],
	   dbo.GetPlanProdFieldName(10048) [Services Distance from Face Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10049,SC.Sectionid_2)[Any Services in Back Areas],
	   dbo.GetPlanProdFieldName(10049) [Any Services in Back Areas Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10050,SC.Sectionid_2)[Any Services in Back Areas Action Date],
	   dbo.GetPlanProdFieldName(10050) [Any Services in Back Areas Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10051,SC.Sectionid_2)[Box Front To Std.],
	   dbo.GetPlanProdFieldName(10051) [Box Front To Std. Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10052,SC.Sectionid_2)[Box Front To Std. Action Date],
	   dbo.GetPlanProdFieldName(10052) [Box Front To Std. Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10053,SC.Sectionid_2)[Old Areas Baricaded off],
	   dbo.GetPlanProdFieldName(10053) [Old Areas Baricaded off Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10054,SC.Sectionid_2)[Old Areas Baricaded off Action Date],
	   dbo.GetPlanProdFieldName(10054) [Old Areas Baricaded off Action DateName],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10055,SC.Sectionid_2)[Secondary Waiting Place],
	   dbo.GetPlanProdFieldName(10055) [Secondary Waiting Place Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10056,SC.Sectionid_2)[Secondary Waiting Place Action Date],
	   dbo.GetPlanProdFieldName(10056) [Secondary Waiting Place Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10057,SC.Sectionid_2)[Travelling Way Equipped],
	   dbo.GetPlanProdFieldName(10057) [Travelling Way Equipped Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10058,SC.Sectionid_2)[Travelling Way Equipped Action Date],
	   dbo.GetPlanProdFieldName(10058) [Travelling Way Equipped Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10059,SC.Sectionid_2)[Pre-Developed Gullies (Ledging Phase)],
	   dbo.GetPlanProdFieldName(10059) [Pre-Developed Gullies (Ledging Phase) Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10060,SC.Sectionid_2)[Pre-Developed Gullies (Ledging Phase) Action Date],
	   dbo.GetPlanProdFieldName(10060) [Pre-Developed Gullies (Ledging Phase) Action Date Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10061,SC.Sectionid_2)[Mono Which Route ],
	   dbo.GetPlanProdFieldName(10061) [Mono Which Route Name],
	   dbo.GetPlanProdData(1,pp.Workplaceid,@PRODMONTH,10062,SC.Sectionid_2)[Mono Which Route Action Date],
	   dbo.GetPlanProdFieldName(10062) [Mono Which Route Action Date Name],

CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 1
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP' --done
Go

Create Procedure dbo.sp_PlanProd_Saftey_DEV		--11		
--Declare																					
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																					

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'
--SET @Activity Int = 1													


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 11) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,

 dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11001,SC.Sectionid_2)[Date],
	   dbo.GetPlanProdFieldName(11001) [Date Name],
       dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11002,SC.Sectionid_2)[Last Accident],
	   dbo.GetPlanProdFieldName(11002) [Last Accident Name],  
	    dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11003,SC.Sectionid_2)[A` Hazards],
	   dbo.GetPlanProdFieldName(11003) [A` Hazards Name],  
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11004,SC.Sectionid_2)[B` Hazards],
	   dbo.GetPlanProdFieldName(11004) [B` Hazards Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11005,SC.Sectionid_2)[C` Hazards],
	   dbo.GetPlanProdFieldName(11005) [C` Hazards Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11006,SC.Sectionid_2)[Dressing Cases],
	   dbo.GetPlanProdFieldName(11006) [Dressing Cases Name], 
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11007,SC.Sectionid_2)[Lost time],
	   dbo.GetPlanProdFieldName(11007) [Lost time Name],  
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11008,SC.Sectionid_2)[Serious],
	   dbo.GetPlanProdFieldName(11008) [Serious Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11009,SC.Sectionid_2)[White Flag Days],
	   dbo.GetPlanProdFieldName(11009) [White Flag Days Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11010,SC.Sectionid_2)[Physical Condition Rating],
	   dbo.GetPlanProdFieldName(11010) [Physical Condition Rating Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11011,SC.Sectionid_2)[Standards Rating  Rating],
	   dbo.GetPlanProdFieldName(11011) [Standards Rating Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11012,SC.Sectionid_2)[Explosives Rating],
	   dbo.GetPlanProdFieldName(11012) [Explosives Rating Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11013,SC.Sectionid_2)[Pre Conditioning Drilled To Std],
	   dbo.GetPlanProdFieldName(11013) [Pre Conditioning Drilled To Std Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11014,SC.Sectionid_2)[Temp & Permanent Support & Resin Bolts To Std],
	   dbo.GetPlanProdFieldName(11014) [Temp & Permanent Support & Resin Bolts To Std Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11015,SC.Sectionid_2)[Stop & Fixes Conceded],
	   dbo.GetPlanProdFieldName(11015) [Stop & Fixes Conceded Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11016,SC.Sectionid_2)[Explosives Stored & Controlled As Per Std],
	   dbo.GetPlanProdFieldName(11016) [Explosives Stored & Controlled As Per Std Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11017,SC.Sectionid_2)[Winches , Rigging, ect. ],
	   dbo.GetPlanProdFieldName(11017) [Winches , Rigging, ect.  Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11018,SC.Sectionid_2)[Appointments Done],
	   dbo.GetPlanProdFieldName(11018) [Appointments Done Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11019,SC.Sectionid_2)[Early Shift],
	   dbo.GetPlanProdFieldName(11019) [Early Shift Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11020,SC.Sectionid_2)[Late Shift],
	   dbo.GetPlanProdFieldName(11020) [Late Shift Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11021,SC.Sectionid_2)[Declaration],
	   dbo.GetPlanProdFieldName(11021) [Declaration Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11022,SC.Sectionid_2)[Other Safety Concerns in Panel],
	   dbo.GetPlanProdFieldName(11022) [Other Safety Concerns in Panel Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11023,SC.Sectionid_2)[Risk Assessments Done],
	   dbo.GetPlanProdFieldName(11023) [Risk Assessments Done Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11024,SC.Sectionid_2)[PTO's Done],
	   dbo.GetPlanProdFieldName(11024) [PTO's Done Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11025,SC.Sectionid_2)[Planned Inspections Done],
	   dbo.GetPlanProdFieldName(11025) [Planned Inspections Done Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11026,SC.Sectionid_2)[On the Job Traininng],
	   dbo.GetPlanProdFieldName(11026) [On the Job Traininng Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11027,SC.Sectionid_2)[In-Stope Nets Installed],
	   dbo.GetPlanProdFieldName(11027) [In-Stope Nets Installed Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11028,SC.Sectionid_2)[In-Stope Nets Installed  Action Date],
	   dbo.GetPlanProdFieldName(11028) [In-Stope Nets Installed  Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11029,SC.Sectionid_2)[In-Stope Resin Bolts Installed],
	   dbo.GetPlanProdFieldName(11029) [In-Stope Resin Bolts Installed Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11030,SC.Sectionid_2)[In-Stope Resin Bolts Installed Action Date],
	   dbo.GetPlanProdFieldName(11030) [In-Stope Resin Bolts Installed Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11031,SC.Sectionid_2)[Heading To Std.],
	   dbo.GetPlanProdFieldName(11031) [Heading To Std. Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11032,SC.Sectionid_2)[Heading To Std  Action Date],
	   dbo.GetPlanProdFieldName(11032) [Heading To Std  Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11033,SC.Sectionid_2)[Escape gully To Std. ],
	   dbo.GetPlanProdFieldName(11033) [Escape gully To Std.  Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11034,SC.Sectionid_2)[Escape gully To Std. Action Date],
	   dbo.GetPlanProdFieldName(11034) [In-Stope Nets Installed  Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11035,SC.Sectionid_2)[Welded Mesh in Both Gullies],
	   dbo.GetPlanProdFieldName(11035) [Welded Mesh in Both Gullies Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11036,SC.Sectionid_2)[Welded Mesh in Both Gullies Action Date],
	   dbo.GetPlanProdFieldName(11036) [Welded Mesh in Both Gullies Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11037,SC.Sectionid_2)[Tip Area To Std.],
	   dbo.GetPlanProdFieldName(11037) [Tip Area To Std. Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11038,SC.Sectionid_2)[Tip Area To Std. Action Date],
	   dbo.GetPlanProdFieldName(11038) [Tip Area To Std. Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11039,SC.Sectionid_2)[Slusher Gully Utilizes ],
	   dbo.GetPlanProdFieldName(11039) [Slusher Gully Utilizes Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11040,SC.Sectionid_2)[Slusher Gully Utilizes Action Date],
	   dbo.GetPlanProdFieldName(11040) [Slusher Gully Utilizes Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11041,SC.Sectionid_2)[Water Controls Utilized],
	   dbo.GetPlanProdFieldName(11041) [Water Controls Utilized Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11042,SC.Sectionid_2)[Water Controls Utilized Action Date],
	   dbo.GetPlanProdFieldName(11042) [Water Controls Utilized Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11043,SC.Sectionid_2)[Proto Valve],
	   dbo.GetPlanProdFieldName(11043) [Proto Valve Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11044,SC.Sectionid_2)[Proto Valve Action Date],
	   dbo.GetPlanProdFieldName(11044) [Proto Valve Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11045,SC.Sectionid_2)[Face Winch Distance From Face],
	   dbo.GetPlanProdFieldName(11045) [Face Winch Distance From Face Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11046,SC.Sectionid_2)[Face Winch Distance From Face Action Date],
	   dbo.GetPlanProdFieldName(11046) [Face Winch Distance From Face Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11047,SC.Sectionid_2)[Services Distance from Face],
	   dbo.GetPlanProdFieldName(11047) [Services Distance from Face Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11048,SC.Sectionid_2)[Services Distance from Face Action Date],
	   dbo.GetPlanProdFieldName(11048) [Services Distance from Face Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11049,SC.Sectionid_2)[Any Services in Back Areas],
	   dbo.GetPlanProdFieldName(11049) [Any Services in Back Areas Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11050,SC.Sectionid_2)[Any Services in Back Areas Action Date],
	   dbo.GetPlanProdFieldName(11050) [Any Services in Back Areas Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11051,SC.Sectionid_2)[Box Front To Std.],
	   dbo.GetPlanProdFieldName(11051) [Box Front To Std. Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11052,SC.Sectionid_2)[Box Front To Std. Action Date],
	   dbo.GetPlanProdFieldName(11052) [Box Front To Std. Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11053,SC.Sectionid_2)[Old Areas Baricaded off],
	   dbo.GetPlanProdFieldName(11053) [Old Areas Baricaded off Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11054,SC.Sectionid_2)[Old Areas Baricaded off Action Date],
	   dbo.GetPlanProdFieldName(11054) [Old Areas Baricaded off Action DateName],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11055,SC.Sectionid_2)[Secondary Waiting Place],
	   dbo.GetPlanProdFieldName(11055) [Secondary Waiting Place Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11056,SC.Sectionid_2)[Secondary Waiting Place Action Date],
	   dbo.GetPlanProdFieldName(11056) [Secondary Waiting Place Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11057,SC.Sectionid_2)[Travelling Way Equipped],
	   dbo.GetPlanProdFieldName(11057) [Travelling Way Equipped Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11058,SC.Sectionid_2)[Travelling Way Equipped Action Date],
	   dbo.GetPlanProdFieldName(11058) [Travelling Way Equipped Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11059,SC.Sectionid_2)[Pre-Developed Gullies (Ledging Phase)],
	   dbo.GetPlanProdFieldName(11059) [Pre-Developed Gullies (Ledging Phase) Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11060,SC.Sectionid_2)[Pre-Developed Gullies (Ledging Phase) Action Date],
	   dbo.GetPlanProdFieldName(11060) [Pre-Developed Gullies (Ledging Phase) Action Date Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11061,SC.Sectionid_2)[Mono Which Route ],
	   dbo.GetPlanProdFieldName(11061) [Mono Which Route Name],
	   dbo.GetPlanProdData(11,pp.Workplaceid,@PRODMONTH,11062,SC.Sectionid_2)[Mono Which Route Action Date],
	   dbo.GetPlanProdFieldName(11062) [Mono Which Route Action Date Name],

CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 11
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'      --done
Go

Create Procedure dbo.sp_Engineering_Equipping_STP			--02	
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																				

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--o


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 2) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20001,SC.Sectionid_2)[Centre Gully Winch],
	   dbo.GetPlanProdFieldName(20001) [Centre Gully Winch Name],
       dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20002,SC.Sectionid_2)[Centre Gully Winch Number],
	   dbo.GetPlanProdFieldName(20002) [Centre Gully Winch Number Name],  
	    dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20003,SC.Sectionid_2)[Face Winch],
	   dbo.GetPlanProdFieldName(20003) [Face Winch Name],  
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20004,SC.Sectionid_2)[Face Winch Number],
	   dbo.GetPlanProdFieldName(20004) [Face Winch Number Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20005,SC.Sectionid_2)[Winch Cubbies Fire Proofed],
	   dbo.GetPlanProdFieldName(20005) [Winch Cubbies Fire Proofed Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20006,SC.Sectionid_2)[Winch Cubbies Fire Proofed Number],
	   dbo.GetPlanProdFieldName(20006) [Winch Cubbies Fire Proofed Number Name], 
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20007,SC.Sectionid_2)[Lights installed],
	   dbo.GetPlanProdFieldName(20007) [Lights installed Name],  
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20008,SC.Sectionid_2)[Lights installed Number],
	   dbo.GetPlanProdFieldName(20008) [Lights installed Number Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20009,SC.Sectionid_2)[Winch Move Planned],
	   dbo.GetPlanProdFieldName(20009) [Winch Move Planned Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20010,SC.Sectionid_2)[Winch Move Planned Date],
	   dbo.GetPlanProdFieldName(20010) [Winch Move Planned Date Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20011,SC.Sectionid_2)[Mono Wich Move Planned],
	   dbo.GetPlanProdFieldName(20011) [Mono Wich Move Planned Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20012,SC.Sectionid_2)[Mono Wich Move Planned Date],
	   dbo.GetPlanProdFieldName(20012) [Mono Wich Move Planned Date Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20013,SC.Sectionid_2)[Power Extensions Required],
	   dbo.GetPlanProdFieldName(20013) [Power Extensions Required Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20014,SC.Sectionid_2)[Power Extensions Required Date],
	   dbo.GetPlanProdFieldName(20014) [Power Extensions Required Date Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20015,SC.Sectionid_2)[Machinery Maintenace Program ],
	   dbo.GetPlanProdFieldName(20015) [Machinery Maintenace Program Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20016,SC.Sectionid_2)[Machinery Maintenace Program Date],
	   dbo.GetPlanProdFieldName(20016) [Machinery Maintenace Program Date Name],
	   dbo.GetPlanProdData(2,pp.Workplaceid,@PRODMONTH,20017,SC.Sectionid_2)[Equipment Required for new Month],
	   dbo.GetPlanProdFieldName(20017) [Equipment Required for new Month Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 2
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'     --done 
Go

Create Procedure dbo.sp_Engineering_Equipping_DEV		--22
--Declare																					
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																				

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 22) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22001,SC.Sectionid_2)[Centre Gully Winch],
	   dbo.GetPlanProdFieldName(22001) [Centre Gully Winch Name],
       dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22002,SC.Sectionid_2)[Centre Gully Winch Number],
	   dbo.GetPlanProdFieldName(22002) [Centre Gully Winch Number Name],  
	    dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22003,SC.Sectionid_2)[Face Winch],
	   dbo.GetPlanProdFieldName(22003) [Face Winch Name],  
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22004,SC.Sectionid_2)[Face Winch Number],
	   dbo.GetPlanProdFieldName(22004) [Face Winch Number Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22005,SC.Sectionid_2)[Winch Cubbies Fire Proofed],
	   dbo.GetPlanProdFieldName(22005) [Winch Cubbies Fire Proofed Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22006,SC.Sectionid_2)[Winch Cubbies Fire Proofed Number],
	   dbo.GetPlanProdFieldName(22006) [Winch Cubbies Fire Proofed Number Name], 
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22007,SC.Sectionid_2)[Lights installed],
	   dbo.GetPlanProdFieldName(22007) [Lights installed Name],  
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22008,SC.Sectionid_2)[Lights installed Number],
	   dbo.GetPlanProdFieldName(22008) [Lights installed Number Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22009,SC.Sectionid_2)[Winch Move Planned],
	   dbo.GetPlanProdFieldName(22009) [Winch Move Planned Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22010,SC.Sectionid_2)[Winch Move Planned Date],
	   dbo.GetPlanProdFieldName(22010) [Winch Move Planned Date Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22011,SC.Sectionid_2)[Mono Wich Move Planned],
	   dbo.GetPlanProdFieldName(22011) [Mono Wich Move Planned Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22012,SC.Sectionid_2)[Mono Wich Move Planned Date],
	   dbo.GetPlanProdFieldName(22012) [Mono Wich Move Planned Date Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22013,SC.Sectionid_2)[Power Extensions Required],
	   dbo.GetPlanProdFieldName(22013) [Power Extensions Required Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22014,SC.Sectionid_2)[Power Extensions Required Date],
	   dbo.GetPlanProdFieldName(22014) [Power Extensions Required Date Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22015,SC.Sectionid_2)[Machinery Maintenace Program ],
	   dbo.GetPlanProdFieldName(22015) [Machinery Maintenace Program Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22016,SC.Sectionid_2)[Machinery Maintenace Program Date],
	   dbo.GetPlanProdFieldName(22016) [Machinery Maintenace Program Date Name],
	   dbo.GetPlanProdData(22,pp.Workplaceid,@PRODMONTH,22017,SC.Sectionid_2)[Equipment Required for new Month],
	   dbo.GetPlanProdFieldName(22017) [Equipment Required for new Month Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 22
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Latest_RME_Report_Number_STP			--03	
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 3) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30001,SC.Sectionid_2)[RMR],
	   dbo.GetPlanProdFieldName(30001) [RMR Name],
       dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30002,SC.Sectionid_2)[Energy Release Rate],
	   dbo.GetPlanProdFieldName(30002) [Energy Release Rate Name],  
	    dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30003,SC.Sectionid_2)[Seismic Rating],
	   dbo.GetPlanProdFieldName(30003) [Seismic Rating Name],  
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30004,SC.Sectionid_2)[Lead & Lag],
	   dbo.GetPlanProdFieldName(30004) [Lead & Lag Name],
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30005,SC.Sectionid_2)[FOG Seismic],
	   dbo.GetPlanProdFieldName(30005) [FOG Seismic Name],
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30006,SC.Sectionid_2)[Brows present],
	   dbo.GetPlanProdFieldName(30006) [Brows presentName], 
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30007,SC.Sectionid_2)[Face Shape],
	   dbo.GetPlanProdFieldName(30007) [Face Shape Name],  
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30008,SC.Sectionid_2)[Crush Pillar Req],
	   dbo.GetPlanProdFieldName(30008) [Crush Pillar Req Name],
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30009,SC.Sectionid_2)[Secondary Support Req],
	   dbo.GetPlanProdFieldName(30009) [Secondary Support Req Name],
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30010,SC.Sectionid_2)[Additional Support Req],
	   dbo.GetPlanProdFieldName(30010) [Additional Support Req Name],
	   dbo.GetPlanProdData(3,pp.Workplaceid,@PRODMONTH,30011,SC.Sectionid_2)[Special Area],
	   dbo.GetPlanProdFieldName(30011) [Special Area Name],

CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 3
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP' --Done  
Go

Create Procedure dbo.sp_Latest_RME_Report_Number_DEV	
	--33	
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																						--in
	
--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 33) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33001,SC.Sectionid_2)[RMR],
	   dbo.GetPlanProdFieldName(33001) [RMR Name],
       dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33002,SC.Sectionid_2)[Energy Release Rate],
	   dbo.GetPlanProdFieldName(33002) [Energy Release Rate Name],  
	    dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33003,SC.Sectionid_2)[Seismic Rating],
	   dbo.GetPlanProdFieldName(33003) [Seismic Rating Name],  
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33004,SC.Sectionid_2)[Lead & Lag],
	   dbo.GetPlanProdFieldName(33004) [Lead & Lag Name],
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33005,SC.Sectionid_2)[FOG Seismic],
	   dbo.GetPlanProdFieldName(33005) [FOG Seismic Name],
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33006,SC.Sectionid_2)[Brows present],
	   dbo.GetPlanProdFieldName(33006) [Brows presentName], 
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33007,SC.Sectionid_2)[Face Shape],
	   dbo.GetPlanProdFieldName(33007) [Face Shape Name],  
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33008,SC.Sectionid_2)[Crush Pillar Req],
	   dbo.GetPlanProdFieldName(33008) [Crush Pillar Req Name],
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33009,SC.Sectionid_2)[Secondary Support Req],
	   dbo.GetPlanProdFieldName(33009) [Secondary Support Req Name],
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33010,SC.Sectionid_2)[Additional Support Req],
	   dbo.GetPlanProdFieldName(33010) [Additional Support Req Name],
	   dbo.GetPlanProdData(33,pp.Workplaceid,@PRODMONTH,33011,SC.Sectionid_2)[Special Area],
	   dbo.GetPlanProdFieldName(33011) [Special Area Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 33
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Latest_RME_Recommendation_Number_STP	--04
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																					--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 4) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,
	   
      dbo.GetPlanProdData(4,pp.Workplaceid,@PRODMONTH,40001,SC.Sectionid_2)[Latest RME Recommendation Number],
	  dbo.GetPlanProdFieldName(40001) [Latest RME Recommendation Number Name],
      dbo.GetPlanProdData(4,pp.Workplaceid,@PRODMONTH,40002,SC.Sectionid_2)[Support Compliance %],
	  dbo.GetPlanProdFieldName(40002) [Support Compliance % Name],  
	  dbo.GetPlanProdData(4,pp.Workplaceid,@PRODMONTH,40003,SC.Sectionid_2)[Panel Rating],
	  dbo.GetPlanProdFieldName(40003) [Panel Rating Name],  
	  

CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 4
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'        --Done 
Go

Create Procedure dbo.sp_Latest_RME_Recommendation_Number_DEV	 --44		
--Declare																					
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																						

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 44) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(44,pp.Workplaceid,@PRODMONTH,44001,SC.Sectionid_2)[Latest RME Recommendation Number],
	   dbo.GetPlanProdFieldName(44001) [Latest RME Recommendation Number Name],
       dbo.GetPlanProdData(44,pp.Workplaceid,@PRODMONTH,44002,SC.Sectionid_2)[Support Compliance %],
	   dbo.GetPlanProdFieldName(44002) [Support Compliance % Name],  
	   dbo.GetPlanProdData(44,pp.Workplaceid,@PRODMONTH,44003,SC.Sectionid_2)[Panel Rating],
	   dbo.GetPlanProdFieldName(44003) [Panel Rating Name],  
	  


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 44
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Latest_Ventilation_Report_Number_STP	--05  
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 5) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50001,SC.Sectionid_2)[Velocity],
	   dbo.GetPlanProdFieldName(50001) [Velocity Name],
       dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50002,SC.Sectionid_2)[Wet Bulb ],
	   dbo.GetPlanProdFieldName(50002) [Wet Bulb Name],  
	    dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50003,SC.Sectionid_2)[Dry Bulb],
	   dbo.GetPlanProdFieldName(50003) [Dry Bulb Name],  
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50004,SC.Sectionid_2)[V/Controls],
	   dbo.GetPlanProdFieldName(50004) [V/Controls Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50005,SC.Sectionid_2)[Noise],
	   dbo.GetPlanProdFieldName(50005) [Noise Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50006,SC.Sectionid_2)[AU],
	   dbo.GetPlanProdFieldName(50006) [AU Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50007,SC.Sectionid_2)[GDI],
	   dbo.GetPlanProdFieldName(50007) [GDI Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50008,SC.Sectionid_2)[Drills],
	   dbo.GetPlanProdFieldName(50008) [Drills Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50009,SC.Sectionid_2)[Dust],
	   dbo.GetPlanProdFieldName(50009) [Dust Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50010,SC.Sectionid_2)[CH4],
	   dbo.GetPlanProdFieldName(50010) [CH4 Name], 
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50011,SC.Sectionid_2)[Leackages],
	   dbo.GetPlanProdFieldName(50011) [Leackages Name],  
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50012,SC.Sectionid_2)[Heat Sickness Casesq],
	   dbo.GetPlanProdFieldName(50012) [Heat Sickness Cases Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50013,SC.Sectionid_2)[Refuge Bay No],
	   dbo.GetPlanProdFieldName(50013) [Refuge Bay No Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50014,SC.Sectionid_2)[Life Sustainable],
	   dbo.GetPlanProdFieldName(50014) [Life Sustainable Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50015,SC.Sectionid_2)[Distance from Workplace],
	   dbo.GetPlanProdFieldName(50015) [Distance from Workplace Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50016,SC.Sectionid_2)[Hearing Protection],
	   dbo.GetPlanProdFieldName(50016) [Hearing Protection Name],
	   dbo.GetPlanProdData(5,pp.Workplaceid,@PRODMONTH,50017,SC.Sectionid_2)[Rating],
	   dbo.GetPlanProdFieldName(50017) [Rating Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 5
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'        --done  
Go

Create Procedure dbo.sp_Latest_Ventilation_Report_Number_DEV	--55  
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																					--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 55) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55001,SC.Sectionid_2)[Velocity],
	   dbo.GetPlanProdFieldName(55001) [Velocity Name],
       dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55002,SC.Sectionid_2)[Wet Bulb ],
	   dbo.GetPlanProdFieldName(55002) [Wet Bulb Name],  
	    dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55003,SC.Sectionid_2)[Dry Bulb],
	   dbo.GetPlanProdFieldName(55003) [Dry Bulb Name],  
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55004,SC.Sectionid_2)[V/Controls],
	   dbo.GetPlanProdFieldName(55004) [V/Controls Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55005,SC.Sectionid_2)[Noise],
	   dbo.GetPlanProdFieldName(55005) [Noise Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55006,SC.Sectionid_2)[AU],
	   dbo.GetPlanProdFieldName(55006) [AU Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55007,SC.Sectionid_2)[GDI],
	   dbo.GetPlanProdFieldName(55007) [GDI Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55008,SC.Sectionid_2)[Drills],
	   dbo.GetPlanProdFieldName(55008) [Drills Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55009,SC.Sectionid_2)[Dust],
	   dbo.GetPlanProdFieldName(55009) [Dust Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55010,SC.Sectionid_2)[CH4],
	   dbo.GetPlanProdFieldName(55010) [CH4 Name], 
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55011,SC.Sectionid_2)[Leackages],
	   dbo.GetPlanProdFieldName(55011) [Leackages Name],  
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55012,SC.Sectionid_2)[Heat Sickness Casesq],
	   dbo.GetPlanProdFieldName(55012) [Heat Sickness Cases Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55013,SC.Sectionid_2)[Refuge Bay No],
	   dbo.GetPlanProdFieldName(55013) [Refuge Bay No Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55014,SC.Sectionid_2)[Life Sustainable],
	   dbo.GetPlanProdFieldName(55014) [Life Sustainable Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55015,SC.Sectionid_2)[Distance from Workplace],
	   dbo.GetPlanProdFieldName(55015) [Distance from Workplace Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55016,SC.Sectionid_2)[Hearing Protection],
	   dbo.GetPlanProdFieldName(55016) [Hearing Protection Name],
	   dbo.GetPlanProdData(55,pp.Workplaceid,@PRODMONTH,55017,SC.Sectionid_2)[Rating],
	   dbo.GetPlanProdFieldName(55017) [Rating Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 55
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Latest_Geology_Report_Number_STP		--06	
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 6) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60001,SC.Sectionid_2)[Dykes],
	   dbo.GetPlanProdFieldName(60001) [DykesName],
       dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60002,SC.Sectionid_2)[Faults > 0.5m],
	   dbo.GetPlanProdFieldName(60002) [Faults > 0.5m Name],  
	    dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60003,SC.Sectionid_2)[Faults < 0.5m],
	   dbo.GetPlanProdFieldName(60003) [Faults < 0.5m Name],  
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60004,SC.Sectionid_2)[Faults/Dykes < 400],
	   dbo.GetPlanProdFieldName(60004) [Faults/Dykes < 400 Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60005,SC.Sectionid_2)[Quartz veins/Joints],
	   dbo.GetPlanProdFieldName(60005) [Quartz veins/Joints Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60006,SC.Sectionid_2)[Dip of Reef],
	   dbo.GetPlanProdFieldName(60006) [Dip of Reef Name], 
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60007,SC.Sectionid_2)[Argillite exposure ],
	   dbo.GetPlanProdFieldName(60007) [Argillite exposure  Name],  
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60008,SC.Sectionid_2)[FOG Gravity],
	   dbo.GetPlanProdFieldName(60008) [FOG Gravity Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60009,SC.Sectionid_2)[RIH],
	   dbo.GetPlanProdFieldName(60009) [RIH Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60010,SC.Sectionid_2)[RIF],
	   dbo.GetPlanProdFieldName(60010) [RIF Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60011,SC.Sectionid_2)[Rolling],
	   dbo.GetPlanProdFieldName(60011) [Rolling Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60012,SC.Sectionid_2)[Extent of roll],
	   dbo.GetPlanProdFieldName(60012) [Extent of roll Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60013,SC.Sectionid_2)[SW Recommended],
	   dbo.GetPlanProdFieldName(60013) [SW Recommended Name],
	   dbo.GetPlanProdData(6,pp.Workplaceid,@PRODMONTH,60014,SC.Sectionid_2)[Reef Detector no.],
	   dbo.GetPlanProdFieldName(60014) [Reef Detector no.Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 6
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'     --done
Go

Create Procedure dbo.sp_Latest_Geology_Report_Number_DEV		--66  
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int =1
as																				--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 66) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66001,SC.Sectionid_2)[Dykes],
	   dbo.GetPlanProdFieldName(66001) [DykesName],
       dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66002,SC.Sectionid_2)[Faults > 0.5m],
	   dbo.GetPlanProdFieldName(66002) [Faults > 0.5m Name],  
	    dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66003,SC.Sectionid_2)[Faults < 0.5m],
	   dbo.GetPlanProdFieldName(66003) [Faults < 0.5m Name],  
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66004,SC.Sectionid_2)[Faults/Dykes < 400],
	   dbo.GetPlanProdFieldName(66004) [Faults/Dykes < 400 Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66005,SC.Sectionid_2)[Quartz veins/Joints],
	   dbo.GetPlanProdFieldName(66005) [Quartz veins/Joints Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66006,SC.Sectionid_2)[Dip of Reef],
	   dbo.GetPlanProdFieldName(66006) [Dip of Reef Name], 
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66007,SC.Sectionid_2)[Argillite exposure ],
	   dbo.GetPlanProdFieldName(66007) [Argillite exposure  Name],  
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66008,SC.Sectionid_2)[FOG Gravity],
	   dbo.GetPlanProdFieldName(66008) [FOG Gravity Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66009,SC.Sectionid_2)[RIH],
	   dbo.GetPlanProdFieldName(66009) [RIH Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66010,SC.Sectionid_2)[RIF],
	   dbo.GetPlanProdFieldName(66010) [RIF Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66011,SC.Sectionid_2)[Rolling],
	   dbo.GetPlanProdFieldName(66011) [Rolling Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66012,SC.Sectionid_2)[Hearing Protection],
	   dbo.GetPlanProdFieldName(66012) [Hearing Protection Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66013,SC.Sectionid_2)[Extent of roll],
	   dbo.GetPlanProdFieldName(66013) [Extent of roll Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66014,SC.Sectionid_2)[SW Recommended],
	   dbo.GetPlanProdFieldName(66014) [SW Recommended Name],
	   dbo.GetPlanProdData(66,pp.Workplaceid,@PRODMONTH,66015,SC.Sectionid_2)[Reef Detector no.],
	   dbo.GetPlanProdFieldName(66015) [Reef Detector no.Name],


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 66
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Latest_Survey_Note_Number_STP	 --07 	
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																							--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 7) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(7,pp.Workplaceid,@PRODMONTH,70001,SC.Sectionid_2)[Stop Note No.],
	   dbo.GetPlanProdFieldName(70001) [Stop Note No. Name],
       dbo.GetPlanProdData(7,pp.Workplaceid,@PRODMONTH,70002,SC.Sectionid_2)[Holing Note No.],
	   dbo.GetPlanProdFieldName(70002) [Holing Note No. Name],  
	    dbo.GetPlanProdData(7,pp.Workplaceid,@PRODMONTH,70003,SC.Sectionid_2)[30m Rule],
	   dbo.GetPlanProdFieldName(70003) [30m Rule Name],  
	   dbo.GetPlanProdData(7,pp.Workplaceid,@PRODMONTH,70004,SC.Sectionid_2)[Stopping Distance To Pillar],
	   dbo.GetPlanProdFieldName(70004) [Stopping Distance To Pillar Name],
	   dbo.GetPlanProdData(7,pp.Workplaceid,@PRODMONTH,70005,SC.Sectionid_2)[Adhering To Gully Lines],
	   dbo.GetPlanProdFieldName(70005) [Adhering To Gully Lines Name],
	   dbo.GetPlanProdData(7,pp.Workplaceid,@PRODMONTH,70006,SC.Sectionid_2)[Ahering To Limit Lines],
	   dbo.GetPlanProdFieldName(70006) [Ahering To Limit Lines Name], 
	   dbo.GetPlanProdData(7,pp.Workplaceid,@PRODMONTH,70007,SC.Sectionid_2)[Actual Stoping Width],
	   dbo.GetPlanProdFieldName(70007) [Actual Stoping Width Name], 


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 7
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Latest_Survey_Note_Number_DEV	--77		
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 77) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(77,pp.Workplaceid,@PRODMONTH,77001,SC.Sectionid_2)[Stop Note No.],
	   dbo.GetPlanProdFieldName(77001) [Stop Note No. Name],
       dbo.GetPlanProdData(77,pp.Workplaceid,@PRODMONTH,77002,SC.Sectionid_2)[Holing Note No.],
	   dbo.GetPlanProdFieldName(77002) [Holing Note No. Name],  
	    dbo.GetPlanProdData(77,pp.Workplaceid,@PRODMONTH,77003,SC.Sectionid_2)[30m Rule],
	   dbo.GetPlanProdFieldName(77003) [30m Rule Name],  
	   dbo.GetPlanProdData(77,pp.Workplaceid,@PRODMONTH,77004,SC.Sectionid_2)[Stopping Distance To Pillar],
	   dbo.GetPlanProdFieldName(77004) [Stopping Distance To Pillar Name],
	   dbo.GetPlanProdData(77,pp.Workplaceid,@PRODMONTH,77005,SC.Sectionid_2)[Adhering To Gully Lines],
	   dbo.GetPlanProdFieldName(77005) [Adhering To Gully Lines Name],
	   dbo.GetPlanProdData(77,pp.Workplaceid,@PRODMONTH,77006,SC.Sectionid_2)[dhering To Limit Lines],
	   dbo.GetPlanProdFieldName(77006) [dhering To Limit Lines Name], 
	   dbo.GetPlanProdData(77,pp.Workplaceid,@PRODMONTH,77007,SC.Sectionid_2)[Actual Stoping Width],
	   dbo.GetPlanProdFieldName(77007) [Actual Stoping Width Name], 


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 77
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Human_Resources_STP	--08			
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																							--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 8) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


       dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80001,SC.Sectionid_2)[Labour Compl],
	   dbo.GetPlanProdFieldName(80001) [Labour Compl Name],
       dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80002,SC.Sectionid_2)[Plan],
	   dbo.GetPlanProdFieldName(80002) [Plan Name],  
	    dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80003,SC.Sectionid_2)[Actual],
	   dbo.GetPlanProdFieldName(80003) [Actual Name],  
	   dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80004,SC.Sectionid_2)[Employee's Using Medication],
	   dbo.GetPlanProdFieldName(80004) [Employee's Using Medication Name],
	   dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80005,SC.Sectionid_2)[Chronic Medication],
	   dbo.GetPlanProdFieldName(80005) [Chronic Medication Name],
	   dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80006,SC.Sectionid_2)[TB Treatment],
	   dbo.GetPlanProdFieldName(80006) [TB Treatment Name], 
	   dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80007,SC.Sectionid_2)[Efficiencies],
	   dbo.GetPlanProdFieldName(80007) [Efficiencies Name], 
	   dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80008,SC.Sectionid_2)[m²/Crew],
	   dbo.GetPlanProdFieldName(80008) [m²/Crew Name], 
	   dbo.GetPlanProdData(8,pp.Workplaceid,@PRODMONTH,80009,SC.Sectionid_2)[m²/Man],
	   dbo.GetPlanProdFieldName(80009) [m²/Man Name], 


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 8
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Human_Resources_DEV		--88	
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 88) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88001,SC.Sectionid_2)[Labour Compl],
	   dbo.GetPlanProdFieldName(88001) [Labour Compl Name],
       dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88002,SC.Sectionid_2)[Plan],
	   dbo.GetPlanProdFieldName(88002) [Plan Name],  
	    dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88003,SC.Sectionid_2)[Actual],
	   dbo.GetPlanProdFieldName(88003) [Actual Name],  
	   dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88004,SC.Sectionid_2)[Employee's Using Medication],
	   dbo.GetPlanProdFieldName(88004) [Employee's Using Medication Name],
	   dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88005,SC.Sectionid_2)[Chronic Medication],
	   dbo.GetPlanProdFieldName(88005) [Chronic Medication Name],
	   dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88006,SC.Sectionid_2)[TB Treatment],
	   dbo.GetPlanProdFieldName(88006) [TB Treatment Name], 
	   dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88007,SC.Sectionid_2)[Efficiencies],
	   dbo.GetPlanProdFieldName(88007) [Efficiencies Name], 
	   dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88007,SC.Sectionid_2)[m²/Crew],
	   dbo.GetPlanProdFieldName(88007) [m²/Crew Name], 
	   dbo.GetPlanProdData(88,pp.Workplaceid,@PRODMONTH,88007,SC.Sectionid_2)[m²/Man],
	   dbo.GetPlanProdFieldName(88007) [m²/Man Name], 

CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 88
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Finance_STP	--09		
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 0
as																					--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 9) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,

		dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90001,SC.Sectionid_2)[Prev Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(90001) [Prev Mth Labour Cost/m2Name],
       dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90002,SC.Sectionid_2)[Prev Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(90002) [Prev Mth Explosives Cost/m2 Name],  
	    dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90003,SC.Sectionid_2)[Pev Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(90003) [Pev Mth Drilling Cost/m2 Name],  
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90004,SC.Sectionid_2)[Prev Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(90004) [Prev Mth Support Cost/m2 Name],
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90005,SC.Sectionid_2)[Prev Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(90005) [Prev Mth Cleaining Cost/m2 Name],
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90006,SC.Sectionid_2)[Prev Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(90006) [Prev Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90007,SC.Sectionid_2)[Current Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(90007) [Current Mth Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90008,SC.Sectionid_2)[Current Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(90008) [Current Mth Explosives Cost/m2Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90009,SC.Sectionid_2)[Current Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(90009) [Current Mth Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90010,SC.Sectionid_2)[Current Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(90010) [Current Mth Support Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90011,SC.Sectionid_2)[Current Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(90011) [Current Mth Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90012,SC.Sectionid_2)[Current Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(90012) [Current Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90013,SC.Sectionid_2)[Planned Labour Cost/m2],
	   dbo.GetPlanProdFieldName(90013) [Planned Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90014,SC.Sectionid_2)[Planned Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(90014) [Planned Explosives Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90015,SC.Sectionid_2)[Planned Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(90015) [Planned Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90016,SC.Sectionid_2)[Planned Support Cost/m2],
	   dbo.GetPlanProdFieldName(90016) [Planned Support Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90017,SC.Sectionid_2)[Planned Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(90017) [Planned Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(9,pp.Workplaceid,@PRODMONTH,90018,SC.Sectionid_2)[Planned Other Cost/m2],
	   dbo.GetPlanProdFieldName(90018) [Planned Other Cost/m2 Name],  


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 9
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 0 AND 
						PP.PlanCode ='MP'
Go

Create Procedure dbo.sp_Finance_DEV	--99				
--Declare																					--out
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Int = 1
as																						--in

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'													--out 


SELECT (SELECT TemplateName FROM dbo.PlanProt_Template WHERE TemplateID = 99) TemplateName,
       pp.Workplaceid,w.Description WorkplaceDesc,


        dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99001,SC.Sectionid_2)[Prev Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(99001) [Prev Mth Labour Cost/m2Name],
       dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99002,SC.Sectionid_2)[Prev Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(99002) [Prev Mth Explosives Cost/m2 Name],  
	    dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99003,SC.Sectionid_2)[Pev Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(99003) [Pev Mth Drilling Cost/m2 Name],  
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99004,SC.Sectionid_2)[Prev Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(99004) [Prev Mth Support Cost/m2 Name],
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99005,SC.Sectionid_2)[Prev Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(99005) [Prev Mth Cleaining Cost/m2 Name],
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99006,SC.Sectionid_2)[Prev Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(99006) [Prev Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99007,SC.Sectionid_2)[Current Mth Labour Cost/m2],
	   dbo.GetPlanProdFieldName(99007) [Current Mth Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99008,SC.Sectionid_2)[Current Mth Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(99008) [Current Mth Explosives Cost/m2Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99009,SC.Sectionid_2)[Current Mth Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(99009) [Current Mth Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99010,SC.Sectionid_2)[Current Mth Support Cost/m2],
	   dbo.GetPlanProdFieldName(99010) [Current Mth Support Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99011,SC.Sectionid_2)[Current Mth Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(99011) [Current Mth Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99012,SC.Sectionid_2)[Current Mth Other Cost/m2],
	   dbo.GetPlanProdFieldName(99012) [Current Mth Other Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99013,SC.Sectionid_2)[Planned Labour Cost/m2],
	   dbo.GetPlanProdFieldName(99013) [Planned Labour Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99014,SC.Sectionid_2)[Planned Explosives Cost/m2],
	   dbo.GetPlanProdFieldName(99014) [Planned Explosives Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99015,SC.Sectionid_2)[Planned Drilling Cost/m2],
	   dbo.GetPlanProdFieldName(99015) [Planned Drilling Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99016,SC.Sectionid_2)[Planned Support Cost/m2],
	   dbo.GetPlanProdFieldName(99016) [Planned Support Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99017,SC.Sectionid_2)[Planned Cleaining Cost/m2],
	   dbo.GetPlanProdFieldName(99017) [Planned Cleaining Cost/m2 Name], 
	   dbo.GetPlanProdData(99,pp.Workplaceid,@PRODMONTH,99018,SC.Sectionid_2)[Planned Other Cost/m2],
	   dbo.GetPlanProdFieldName(99018) [Planned Other Cost/m2 Name],  


CASE WHEN PPDA.Approved IS NULL THEN CAST(0 AS BIT) ELSE PPDA.Approved END Approved,PPDA.ApprovedBy,PPDA.ApprovedDate 
FROM PLANMONTH PP
INNER JOIN dbo.SECTION_COMPLETE SC ON
						pp.Prodmonth = SC.PRODMONTH AND
						pp.Sectionid = SC.SECTIONID
LEFT JOIN dbo.PlanProt_DataApproved PPDA ON
						PPDA.PRODMONTH = @PRODMONTH AND 
						sc.Sectionid_2 = PPDA.SECTIONID AND
						pp.Workplaceid = PPDA.WORKPLACEID AND
						pp.Activity = PPDA.ActivityType AND
						PPDA.TemplateID = 99
INNER JOIN Workplace W on
						pp.Workplaceid = w.Workplaceid
						WHERE 
						PP.Prodmonth = @PRODMONTH AND
						sc.Sectionid_2 = @SectionID_2 AND
						pp.Activity = 1 AND 
						PP.PlanCode ='MP'
Go

Create Table dbo.Code_Methods (TargetID Int,
Description VarChar(30),
Activity Int,
Primary Key (TargetID)
)
GO

INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('1','Breast','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('2','Double Cut','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('3','Downdip','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('4','High SW','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('5','No Night Shift as per RME','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('6','Re-establishing of panel','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('7','Special Area as per RME','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('8','Updip','0')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('10','Boxhole','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('11','Flat','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('12','Flat Mech','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('13','H\W Drives','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('14','Orepass','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('15','Raises','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('16','Sec. Sup Development','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('17','T\Ways','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('18','Winze','1')
go
INSERT INTO Code_Methods([TargetID],[Description],[Activity])VALUES('9','Ledging','3')
go

CREATE procedure [dbo].[sp_Revised_Audit_Summary]
--Declare 
@Prodmonth numeric(7),
@ToProdmonth numeric(7),
@Section Varchar(30),
@between varchar(5)
as

--set @Prodmonth = 201606
--set @ToProdmonth = 201608
--set @Section = '1999'
--set @between='0'

Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)
Declare @SyncroDB VarChar(50),@bonus varchar(50)
select @TheLevel = HIERARCHICALID from section 
where PRODMONTH = @Prodmonth 
and
sectionid = @Section

--select @TheLevel

If @TheLevel = 1 
    set @GroupLevel = 'NAME_6'

  If @TheLevel = 2 
    set @GroupLevel = 'NAME_5'
    
  If @TheLevel = 3 
    set @GroupLevel = 'NAME_4'  
    
  If @TheLevel = 4 
    set @GroupLevel = 'NAME_3'  
    
  If @TheLevel = 5 
    set @GroupLevel = 'NAME_2'  
    
  If @TheLevel = 6 
    set @GroupLevel = 'NAME_1'


  If @TheLevel = 1 
    set @SectionLevel = 'SECTIONID_6'

  If @TheLevel = 2 
    set @SectionLevel = 'SECTIONID_5'
    
  If @TheLevel = 3 
    set @SectionLevel = 'SECTIONID_4'  
    
   If @TheLevel = 4 
    set @SectionLevel = 'SECTIONID_3'  
     
   If @TheLevel = 5 
    set @SectionLevel = 'SECTIONID_2'  
    
   If @TheLevel = 6 
    set @SectionLevel = 'SECTIONID_1'

   If @TheLevel = 7 
    set @SectionLevel = 'SECTIONID'
Declare 
        @MINDATE varchar(50),
		@MAXDATE varchar(50),
		@SQL4 Varchar(MAX),
		@SQL5 Varchar(MAX)


 SET @SQL4='SELECT MIN(STARTDATE) FROM PLANMONTH PP INNER JOIN SECTION_COMPLETE SC on 
            			PP.SectionID = sc.SECTIONID and 
            			PP.ProdMonth = SC.PRODMONTH 
						WHERE 
						--PP.PRODMONTH=' + Convert(Varchar(7),@Prodmonth) +' 

						PP.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and PP.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'


						AND SC.'+@SectionLevel+'=''' + @Section + ''' 
						AND PP.PLANCODE=''MP'''

	CREATE TABLE #TheMinDate(TheMinDate VARCHAR(50))
	INSERT #TheMinDate EXEC(@SQL4)
	  
	SET @MINDATE = (SELECT TheMinDate FROM  #TheMinDate)
	--select @MINDATE
	DROP TABLE #TheMinDate
SET @SQL5='SELECT MAX(ENDDATE) FROM PLANMONTH PP INNER JOIN SECTION_COMPLETE SC on 
            			PP.SectionID = sc.SECTIONID and 
            			PP.ProdMonth = SC.PRODMONTH 
						WHERE 
						--PP.PRODMONTH=' + Convert(Varchar(7),@Prodmonth) +' 

						PP.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and PP.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'

						AND  SC.'+@SectionLevel+'=''' + @Section + '''  
						AND PP.PLANCODE=''MP'''
	CREATE TABLE #TheMaxDate(TheMinDate VARCHAR(50))
	INSERT #TheMaxDate EXEC(@SQL5)
	  
	SET @MAXDATE = (SELECT TheMinDate FROM  #TheMaxDate)
	--select @MAXDATE
	DROP TABLE #TheMaxDate

if @between='0'
begin
 Set @SQL1 = 'select distinct SC.NAME_2,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate, sd.ChangeType,
	SUM(case when	sd.ChangeType=''New Workplace'' THEN 1 else 0 END) [New Workplace], 
	SUM(case when	sd.ChangeType=''Call Changes'' THEN 1 else 0 end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
	SUM(				case when	sd.ChangeType=''Move Planning'' THEN 1 else 0 end) [Move Planning],
	SUM(				case when	sd.ChangeType=''Stop Workplace'' THEN 1 else 0 end) [Stop Workplace],
	SUM(				case when	sd.ChangeType=''Crew Miner Changes'' THEN 1 else 0 end) [Crew Miner Changes],
	SUM(				case when	sd.ChangeType=''Start Workplace'' THEN 1 else 0 end) [Start Workplace],
	SUM(				case when	sd.ChangeType=''Mining Method Change'' THEN 1 else 0 end) [Mining Method Change],
	SUM(				case when	sd.ChangeType=''Drill Rig Change'' THEN 1 else 0 end) [Drill Rig Change],
	SUM(				case when	sd.ChangeType=''Delete Planning'' THEN 1 else 0 end) [Delete Planning]
   from PREPLANNING_CHANGEREQUEST PPCR 
   left join PLANMONTH pp on
  --pp.sectionid=PPCR.sectionid AND
  PP.PRODMONTH=PPCR.PRODMONTH and
  --pp.sectionid=ppcr.sectionid and
  pp.workplaceid=ppcr.workplaceid 
  INNER JOIN SECTION_COMPLETE SC on 
	PPCR.SectionID = sc.SECTIONID and 
	PPCR.ProdMonth = SC.PRODMONTH 
	INNER Join Seccal sec on
	sc.prodmonth = sec.Prodmonth AND
	sc.SectionID_1 = sec.SectionID
			LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
			SD.ChangeRequestID=PPCR.ChangeRequestID 
			inner join PREPLANNING_CHANGEREQUEST_APPROVAL App on
			PPCR.ChangeRequestID = APP.ChangeRequestID
			WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
			--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

			and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
			and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'

			AND PP.PLANCODE=''MP'' 
			AND (Case When pp.Prodmonth = sec.Prodmonth and sec.enddate < app.requestDate then 1 else 0 end) = 0
			GROUP BY Sc.NAME_2,sc.'+@GroupLevel+', sd.ChangeType
--union 
--select distinct S.NAME,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate,
-- COUNT(case when	sd.ChangeType=''New Workplace'' THEN sd.ChangeType END) [New Workplace], 
--	COUNT(case when	sd.ChangeType=''Call Changes'' THEN sd.ChangeType end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
--	COUNT(				case when	sd.ChangeType=''Move Planning'' THEN sd.ChangeType end) [Move Planning],
--	COUNT(				case when	sd.ChangeType=''Stop Workplace'' THEN sd.ChangeType end) [Stop Workplace],
--	COUNT(				case when	sd.ChangeType=''Crew Miner Changes'' THEN sd.ChangeType end) [Crew Miner Changes],
--	COUNT(				case when	sd.ChangeType=''Start Workplace'' THEN sd.ChangeType end) [Start Workplace],
--	COUNT(				case when	sd.ChangeType=''Mining Method Change'' THEN sd.ChangeType end) [Mining Method Change],
--	COUNT(				case when	sd.ChangeType=''Drill Rig Change'' THEN sd.ChangeType end) [Drill Rig Change],
--	COUNT(				case when	sd.ChangeType=''Delete Planning'' THEN sd.ChangeType end) [Delete Planning]
--   from PREPLANNING_CHANGEREQUEST PPCR left join PLANMONTH pp on
--  --pp.sectionid=PPCR.sectionid AND
--  PP.PRODMONTH=PPCR.PRODMONTH and
--  pp.sectionid=ppcr.sectionid and
--  pp.workplaceid=ppcr.workplaceid INNER JOIN SECTION_COMPLETE SC on 
--        PPCR.SectionID = sc.SECTIONID and 
--        PPCR.ProdMonth = SC.PRODMONTH 
--		INNER Join Seccal sec on
--        sc.prodmonth = sec.Prodmonth AND
--        sc.SectionID_1 = sec.SectionID
--		LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
--		SD.ChangeRequestID=PPCR.ChangeRequestID WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
--		--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

--		and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
--		and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'		
		
--		AND PP.PLANCODE=''MP'' 
--		AND pp.startDATE  BETWEEN '''+ @MINDATE+''' AND  '''+ @MAXDATE+''' and pp.enddate  is null
--		GROUP BY S.NAME,sc.'+@GroupLevel+',sd.ChangeType						'
 exec (@SQL1)
-- SELECT @SQL1
 end

  declare @SQL3 VARCHAR(MAX)
  if @between='1'
  begin
  Set @SQL3 = 'select distinct SC.NAME_2,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate, sd.ChangeType,
	SUM(case when	sd.ChangeType=''New Workplace'' THEN 1 else 0 END) [New Workplace], 
	SUM(case when	sd.ChangeType=''Call Changes'' THEN 1 else 0 end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
	SUM(				case when	sd.ChangeType=''Move Planning'' THEN 1 else 0 end) [Move Planning],
	SUM(				case when	sd.ChangeType=''Stop Workplace'' THEN 1 else 0 end) [Stop Workplace],
	SUM(				case when	sd.ChangeType=''Crew Miner Changes'' THEN 1 else 0 end) [Crew Miner Changes],
	SUM(				case when	sd.ChangeType=''Start Workplace'' THEN 1 else 0 end) [Start Workplace],
	SUM(				case when	sd.ChangeType=''Mining Method Change'' THEN 1 else 0 end) [Mining Method Change],
	SUM(				case when	sd.ChangeType=''Drill Rig Change'' THEN 1 else 0 end) [Drill Rig Change],
	SUM(				case when	sd.ChangeType=''Delete Planning'' THEN 1 else 0 end) [Delete Planning]
   from PREPLANNING_CHANGEREQUEST PPCR 
   left join PLANMONTH pp on
  --pp.sectionid=PPCR.sectionid AND
  PP.PRODMONTH=PPCR.PRODMONTH and
  --pp.sectionid=ppcr.sectionid and
  pp.workplaceid=ppcr.workplaceid 
  INNER JOIN SECTION_COMPLETE SC on 
	PPCR.SectionID = sc.SECTIONID and 
	PPCR.ProdMonth = SC.PRODMONTH 
	INNER Join Seccal sec on
	sc.prodmonth = sec.Prodmonth AND
	sc.SectionID_1 = sec.SectionID
			LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
			SD.ChangeRequestID=PPCR.ChangeRequestID 
			inner join PREPLANNING_CHANGEREQUEST_APPROVAL App on
			PPCR.ChangeRequestID = APP.ChangeRequestID
			WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
			--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

			and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
			and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'

			AND PP.PLANCODE=''MP'' 
			AND (Case When pp.Prodmonth = sec.Prodmonth and sec.enddate < app.requestDate then 1 else 0 end) = 1
			GROUP BY Sc.NAME_2,sc.'+@GroupLevel+', sd.ChangeType
--union 
--select distinct S.NAME,sc.'+@GroupLevel+' SectName,'''+ @MAXDATE+''' maxdate,'''+ @MINDATE+''' Mindate,
-- COUNT(case when	sd.ChangeType=''New Workplace'' THEN sd.ChangeType END) [New Workplace], 
--	COUNT(case when	sd.ChangeType=''Call Changes'' THEN sd.ChangeType end) [Call Changes],--,ppcr.changeid,ppcr.workplaceid
--	COUNT(				case when	sd.ChangeType=''Move Planning'' THEN sd.ChangeType end) [Move Planning],
--	COUNT(				case when	sd.ChangeType=''Stop Workplace'' THEN sd.ChangeType end) [Stop Workplace],
--	COUNT(				case when	sd.ChangeType=''Crew Miner Changes'' THEN sd.ChangeType end) [Crew Miner Changes],
--	COUNT(				case when	sd.ChangeType=''Start Workplace'' THEN sd.ChangeType end) [Start Workplace],
--	COUNT(				case when	sd.ChangeType=''Mining Method Change'' THEN sd.ChangeType end) [Mining Method Change],
--	COUNT(				case when	sd.ChangeType=''Drill Rig Change'' THEN sd.ChangeType end) [Drill Rig Change],
--	COUNT(				case when	sd.ChangeType=''Delete Planning'' THEN sd.ChangeType end) [Delete Planning]
--   from PREPLANNING_CHANGEREQUEST PPCR left join PLANMONTH pp on
--  --pp.sectionid=PPCR.sectionid AND
--  PP.PRODMONTH=PPCR.PRODMONTH and
--  pp.sectionid=ppcr.sectionid and
--  pp.workplaceid=ppcr.workplaceid INNER JOIN SECTION_COMPLETE SC on 
--        PPCR.SectionID = sc.SECTIONID and 
--        PPCR.ProdMonth = SC.PRODMONTH 
--		INNER Join Seccal sec on
--        sc.prodmonth = sec.Prodmonth AND
--        sc.SectionID_1 = sec.SectionID
--		LEFT JOIN (select distinct ChangeRequestID,ChangeType from StatusDetails) sd ON
--		SD.ChangeRequestID=PPCR.ChangeRequestID WHERE  SC.'+@SectionLevel+'=''' + @Section + ''' 
--		--and ppcr.prodmonth=' + Convert(Varchar(7),@Prodmonth) +' 

--		and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
--		and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'		
		
--		AND PP.PLANCODE=''MP'' 
--		AND pp.startDATE  BETWEEN '''+ @MINDATE+''' AND  '''+ @MAXDATE+''' and pp.enddate  is null
--		GROUP BY S.NAME,sc.'+@GroupLevel+',sd.ChangeType	'
 exec (@SQL3)
 --SELECT @SQL3
 end

GO

CREATE Procedure [dbo].[sp_RevisedPlanning_AddWorkPlace] 
		@RequestID INT,
@UserID VARCHAR(10)

AS

DECLARE
@Prodmonth NUMERIC(7,0),
@Cubes NUMERIC(7,3),
@SQM NUMERIC(7,3),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@ActivityCode NUMERIC(7,0), 
@SQL Varchar(1000),
@WPDesc Varchar(50),
@OrgDay Varchar(20),
@OrgNight Varchar(20),
@OrgAfternoon Varchar(20),
@Roming Varchar(20),
@ReefSQM NUMERIC(7,3),
@WasteSQM NUMERIC(7,3),
@ReefMeters NUMERIC(7,3),
@WasteMeters NUMERIC(7,3),
@Startdate datetime,
@IsCubic Varchar(20),
@MiningMethod varchar(20),
@FL numeric(10,3)


--SET @RequestID = 1110
--SET @UserID = 'MINEWARE'

SET @SQM = (SELECT CR.ReefSQM + CR.WasteSQM FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Cubes = (SELECT CR.CubicMeters FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Prodmonth =(SELECT CR.ProdMonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Sectionid_2 =(SELECT CR.SectionID_2  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @SectionID =(SELECT CR.SectionID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID = (SELECT CR.WorkplaceID FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @MiningMethod =(SELECT CR.MiningMethod  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

--IF @WorkplaceID  NOT IN (SELECT pp.Workplaceid    FROM PrePlanning pp WHERE Prodmonth =@Prodmonth   and Sectionid_2 =@Sectionid_2  )
--BEGIN
--SET @ActivityCode =  0
--END
--ELSE BEGIN
--SET @ActivityCode =1
--END
--SET @ActivityCode =(SELECT TOP 1 Activity    FROM PLANMONTH pp WHERE Prodmonth =@Prodmonth   and Sectionid_2 =@Sectionid_2  )
SET @ActivityCode =(SELECT Activity    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID  )

SET @IsCubic ='N'

DELETE FROM dbo.PLANMONTH WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND AutoUnPlan = 'Y' AND Activity = @ActivityCode
DELETE FROM dbo.PLANNING WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND Activity = @ActivityCode


--(SELECT DESCRIPTION  FROM WORKPLCE WHERE WORKPLACEID =@WorkplaceID )
SET @WPDesc =(SELECT DESCRIPTION  FROM WORKPLACE WHERE WORKPLACEID =@WorkplaceID )
SET @OrgDay =(SELECT DayCrew  FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @OrgNight =(SELECT NightCrew   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @OrgAfternoon =(SELECT AfternoonCrew   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @Roming =(SELECT RovingCrew   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @ReefSQM =(SELECT ReefSQM    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @WasteSQM =(SELECT WasteSQM    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @ReefMeters =(SELECT Meters    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @WasteMeters =(SELECT MetersWaste    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @SectionID =(SELECT SectionID    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @Sectionid_2 =(SELECT SectionID_2    FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
SET @Startdate =(SELECT StartDate   FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID )
set @FL =(SELECT FL  FROM PREPLANNING_CHANGEREQUEST CR WHERE CR.ChangeRequestID =@RequestID )

IF @ActivityCode =0
BEGIN
INSERT INTO PLANMONTH (Prodmonth , Sectionid ,Activity ,IsCubics ,PlanCode, Workplaceid ,TargetID ,[SQM], ReefSQM,WasteSQM ,FL, CubicMetres ,StartDate ,OrgUnitDay ,OrgUnitNight ,OrgUnitAfternoon ,RomingCrew ) VALUES
(@Prodmonth , @SectionID ,@ActivityCode ,@IsCubic ,'MP',@WorkplaceID ,@MiningMethod,@ReefSQM + @WasteSQM , @ReefSQM ,@WasteSQM ,@FL,@Cubes , @Startdate ,@OrgDay ,@OrgNight ,@OrgAfternoon ,@Roming )
END

ELSE BEGIN
IF @ActivityCode =1
BEGIN
INSERT INTO PLANMONTH (Prodmonth  , Sectionid  ,Activity ,IsCubics ,PlanCode,  Workplaceid ,TargetID , [Metresadvance], ReefAdv ,WasteAdv ,FL , CubicMetres ,StartDate ,OrgUnitDay ,OrgUnitNight ,OrgUnitAfternoon ,RomingCrew ) VALUES
(@Prodmonth , @SectionID ,@ActivityCode ,@IsCubic ,'MP',@WorkplaceID ,@MiningMethod ,  @ReefMeters + @WasteMeters, @ReefMeters  ,@WasteMeters  ,@FL ,@Cubes, @Startdate ,@OrgDay ,@OrgNight ,@OrgAfternoon ,@Roming )
END
END









GO

CREATE Procedure [dbo].[sp_RevisedPlanning_ApproveDecline]
--Declare
@ApproveRequestID INT, @RequestStauts INT ,@UserID VARCHAR(50), @Comments varchar(MAX)

AS
 --Request Status
 --1 = Approved
 --2 = Decline
--SET @ApproveRequestID = 2721
--SET @RequestStauts = 1
--SET @UserID = 'MINEWARE_Rushabh'
--SET @Comments = 'YES'

DECLARE @ChangeRequestID INT, @RequestCount INT, @ChnageID INT,@SQL Varchar(1000),@SQL1 VARCHAR(100),@AppRequired int,@ReqdApproval int,@department varchar(5)

set @department=(SELECT department FROM PREPLANNING_CHANGEREQUEST_APPROVAL
WHERE ApproveRequestID = @ApproveRequestID)

IF @RequestStauts = 2
BEGIN
SET @ChangeRequestID = (
SELECT ChangeRequestID FROM PREPLANNING_CHANGEREQUEST_APPROVAL
WHERE ApproveRequestID = @ApproveRequestID)

SET @SQL = '[sp_PrePlanning_ChangeMail_Decline] ' + Cast(@ChangeRequestID as Varchar(30)) + ',''' + Cast(@ApproveRequestID as Varchar(30)) +  '''';
 exec (@SQL)

 UPDATE PREPLANNING_CHANGEREQUEST_APPROVAL SET Declined = 1,Approved = 0, ApprovedDeclinedByUser = @UserID, ApprovedDeclinedDate = GetDate(), Comments = @Comments
  WHERE ChangeRequestID = @ChangeRequestID

END

IF @RequestStauts = 1
BEGIN
SET @ChangeRequestID = (
SELECT ChangeRequestID FROM PREPLANNING_CHANGEREQUEST_APPROVAL
WHERE ApproveRequestID = @ApproveRequestID)

set @ReqdApproval=( SELECT ApprovalRequired FROM PREPLANNING_CHANGEREQUEST_APPROVAL
WHERE ApproveRequestID = @ApproveRequestID)
if @ReqdApproval =0
begin
--UPDATE PREPLANNING_CHANGEREQUEST_APPROVAL SET Approved = 1,Declined =0, ApprovedDeclinedByUser = @UserID, ApprovedDeclinedDate = GetDate(), Comments = @Comments
--WHERE ApproveRequestID = @ApproveRequestID and ApprovalRequired=0 and department=@department

UPDATE PREPLANNING_CHANGEREQUEST_APPROVAL SET Approved = 1,Declined =0, ApprovedDeclinedByUser = @UserID, ApprovedDeclinedDate = GetDate(), Comments = @Comments
WHERE ChangeRequestID = @ChangeRequestID and ApprovalRequired=0 and department=@department
end

SET @RequestCount = (SELECT COUNT(ChangeRequestID) FROM  PREPLANNING_CHANGEREQUEST_APPROVAL
WHERE ChangeRequestID = @ChangeRequestID and
      Approved = 0 and
      Declined = 0 and ApprovalRequired=0)

if @ReqdApproval =1
begin
--UPDATE PREPLANNING_CHANGEREQUEST_APPROVAL SET Approved = 1,Declined =0, ApprovedDeclinedByUser = @UserID, ApprovedDeclinedDate = GetDate(), Comments = @Comments
--WHERE ApproveRequestID = @ApproveRequestID and ApprovalRequired=1 and department=@department

UPDATE PREPLANNING_CHANGEREQUEST_APPROVAL SET Approved = 1,Declined =0, ApprovedDeclinedByUser = @UserID, ApprovedDeclinedDate = GetDate(), Comments = @Comments
WHERE ChangeRequestID = @ChangeRequestID and ApprovalRequired=1 and department=@department
end
set @AppRequired=(SELECT COUNT(ApprovalRequired) FROM  PREPLANNING_CHANGEREQUEST_APPROVAL
WHERE ChangeRequestID = @ChangeRequestID and
      Approved = 0 and
      Declined = 0 and ApprovalRequired=1)

      
IF @RequestCount = 0 
BEGIN
if @AppRequired =0
begin
SET @ChnageID = (SELECT ChangeID FROM  PrePlanning_ChangeRequest
WHERE ChangeRequestID = @ChangeRequestID)


IF @ChnageID = 1 -- Stop Workplace
BEGIN
 SET @SQL = '[sp_RevisedPlanning_Stop_Workplace] ' + Cast(@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

 SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)
END

IF @ChnageID =3 --  Miner Crew Changes
BEGIN
SET @SQL = '[sp_PrePlanning_CrewChanges] ' + Cast(@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

 SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)
 END

 IF @ChnageID =4 -- CALL VALUE CHANGES
 BEGIN
 SET @SQL = '[sp_PrePlanning_CallValueChanges] ' + Cast(@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

 SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)
 END

 IF @ChnageID =2 -- ADD WORKPLACE
 BEGIN
 SET @SQL='[sp_RevisedPlanning_AddWorkPlace] ' + Cast(@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

 SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)
 END

 IF @ChnageID=5 -- MOVE WORKPLACE
 BEGIN
 SET @SQL='[sp_RevisedPlanning_MovePlanning] ' + Cast (@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

 SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)
 END


 IF @ChnageID=6 -- Start WORKPLACE
 BEGIN
 SET @SQL='[sp_RevisedPlanning_StartWorkplace] ' + Cast (@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

 
 SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)
 END


 IF @ChnageID=7 -- Mining Method Change
 BEGIN
 SET @SQL='[sp_RevisedPlanning_MiningMethodChange] ' + Cast (@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

  SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)

 SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)
 END


 IF @ChnageID=8 -- Mining Method Change
 BEGIN
 SET @SQL='[sp_RevisedPlanning_DrillRigChange] ' + Cast (@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';
 exec (@SQL)

  SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)

 end


 IF @ChnageID=9 -- Mining Method Change
 BEGIN


 SET @SQL='[sp_RevisedPlanning_DeleteChange] ' + Cast (@ChangeRequestID as Varchar(30)) + ',''' + @UserID +  '''';

 exec (@SQL)

  SET @SQL1='[sp_PrePlanning_ChangeMail_Approve] ' + Cast(@ChangeRequestID as Varchar(30)) + '';
 EXEC (@SQL1)

 end


--IF @RequestCount > 0 
--BEGIN
--SELECT 'LOTS'
--END     
END
END
END



GO

CREATE procedure [dbo].[sp_RevisedPlanning_DeleteChange]
--Declare
@RequestID INT,
@UserID VARCHAR(10)
as

--select 
--@RequestID = '4474',
--@UserID = 'MINEWARE_IGGY'

declare 
@Prodmonth NUMERIC(7,0),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20),
@ActivityCode NUMERIC(7,0) 


SET @Prodmonth =(SELECT CR.ProdMonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Sectionid_2 =(SELECT CR.SectionID_2  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @SectionID =(SELECT CR.SectionID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID = (SELECT CR.WorkplaceID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @ActivityCode=(SELECT Activity  FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID)

--Select 
--@Prodmonth ,
--@SectionID , 
--@Sectionid_2 , 
--@WorkplaceID ,
--@ActivityCode

Delete from planmonth
WHERE Prodmonth=@Prodmonth and Sectionid=@SectionID 
and WorkplaceID=@WorkplaceID AND Activity=@ActivityCode 


Delete from Planning
WHERE Prodmonth=@Prodmonth and Sectionid=@SectionID 
and WorkplaceID=@WorkplaceID AND Activity=@ActivityCode 

GO

create procedure [dbo].[sp_RevisedPlanning_DrillRigChange]
@RequestID INT,
@UserID VARCHAR(10)
as

declare 
@Prodmonth NUMERIC(7,0),
@DrillRig varchar(150),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20),
@ActivityCode NUMERIC(7,0) 

SET @DrillRig =(SELECT CR.DrillRig  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Prodmonth =(SELECT CR.ProdMonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Sectionid_2 =(SELECT CR.SectionID_2  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @SectionID =(SELECT CR.SectionID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID = (SELECT CR.WorkplaceID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @ActivityCode=(SELECT Activity  FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID)

Update PLANMONTH set DrillRig=@DrillRig 
WHERE Prodmonth=@Prodmonth  and Sectionid=@SectionID 
and WorkplaceID=@WorkplaceID AND Activity=@ActivityCode and plancode='MP'



GO

create procedure [dbo].[sp_RevisedPlanning_MiningMethodChange]
@RequestID INT,
@UserID VARCHAR(10)
as

declare 
@Prodmonth NUMERIC(7,0),
@MiningMethod varchar(20),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20),
@ActivityCode NUMERIC(7,0) 

SET @MiningMethod =(SELECT CR.MiningMethod  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Prodmonth =(SELECT CR.ProdMonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Sectionid_2 =(SELECT CR.SectionID_2  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @SectionID =(SELECT CR.SectionID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID = (SELECT CR.WorkplaceID   FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @ActivityCode=(SELECT Activity  FROM PrePlanning_ChangeRequest CR WHERE CR.ChangeRequestID =@RequestID)

Update PLANMONTH set TargetID=@MiningMethod 
WHERE Prodmonth=@Prodmonth  and Sectionid=@SectionID 
and WorkplaceID=@WorkplaceID AND Activity=@ActivityCode and plancode='MP'

GO

CREATE Procedure [dbo].[sp_RevisedPlanning_MovePlan_Check]
  @Workplaceid VARCHAR(20),
  @Prodmonth numeric(7,0)
  AS
 DECLARE  @WorkplaceDesc varchar(20)
 SET @WorkplaceDesc=(select DESCRIPTION FROM WORKPLACE  WHERE WORKPLACEID=@Workplaceid)
 
 SELECT * FROM PrePlanning_ChangeRequest PPRC 
            INNER JOIN PREPLANNING_CHANGEREQUEST_APPROVAL PPRA ON 
            PPRC.ChangeRequestID = PPRA.ChangeRequestID 
            WHERE PPRC.OldWorkplaceID=@WorkplaceDesc and 
           PPRC.Prodmonth = @Prodmonth and PPRC.ChangeID=5 AND
            PPRA.Approved = 0 and PPRA.Declined = 0







GO

CREATE Procedure [dbo].[sp_RevisedPlanning_MovePlanning]
--DECLARE
@RequestID INT,
@UserID VARCHAR(10)

AS

DECLARE


@Prodmonth NUMERIC(7,0),
@WorkplaceID VARCHAR(20), 
@MiningMethod varchar(20),
@NewWorkplaceID VARCHAR(20),
@WPDESCRIPTION VARCHAR(50),
@Facelength numeric(13,3),
@ActivityCode Numeric(7)



--SET @RequestID = 1839
--SET @UserID = 'MINEWARE_Dolf'


SET @WorkplaceID = (select WP.WORKPLACEID FROM WORKPLACE WP INNER JOIN PrePlanning_ChangeRequest CR ON
WP.DESCRIPTION= CR.OldWorkplaceID WHERE CR.ChangeRequestID = @RequestID)
SET @NewWorkplaceID=(SELECT CR.WorkplaceID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
SET @MiningMethod =(SELECT CR.MiningMethod  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
SET @Prodmonth =(SELECT CR.Prodmonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
SET @WPDESCRIPTION=(SELECT DESCRIPTION  FROM WORKPLACE WHERE WORKPLACEID =@NewWorkplaceID)
SET  @Facelength=(SELECT CR.FL  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
SET  @ActivityCode=(SELECT CR.Activity  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

DELETE FROM dbo.PLANMONTH WHERE Workplaceid = @NewWorkplaceID AND Prodmonth = @Prodmonth AND AutoUnPlan = 'Y' AND Activity = @ActivityCode
DELETE FROM dbo.PLANNING WHERE Workplaceid = @NewWorkplaceID AND Prodmonth = @Prodmonth AND Activity = @ActivityCode

Update PlanMonth set [Workplaceid] = @NewWorkplaceID 
      ,[TargetID] = @MiningMethod 
      ,FL = @Facelength 
 where workplaceID = @WorkplaceID and Prodmonth = @Prodmonth and plancode = 'MP'

GO

CREATE Procedure [dbo].[sp_RevisedPLanning_Request]


 @requestType int,        
       @ProdMonth NUMERIC(7,0), 
       @WorkplaceID varchar(20),
       @SectionID varchar(20),
       @SectionID_2 varchar(20),
       @StopDate DateTime, 
       @UserComments varchar(150), 
       @RequestBy varchar(50),
       @SQMOn numeric(7,3),
       @SQMOff numeric(7,3),
       @Cube numeric(7,3),
       @MeterOn numeric(7,3),
       @MeterOff numeric(7,3),
	   @DayCrew varchar(50), 
	   @NightCrew varchar(50),
	   @AfternoonCrew varchar(50),
	   @RovingCrew varchar(50),
	   @StartDate datetime,
	   @MiningMethod varchar(20),
	   @OldWorkplaceID varchar(20),
	   @activity int,
	   @Facelength numeric(13,3),
	   @DrillRig  varchar(150),
	   @DeleteBookings bit = 0


--| Request Types
--| 1 = Stop Workplace

AS

--DECLARE
-- @requestType int,        
--       @ProdMonth NUMERIC(7,0), 
--       @WorkplaceID varchar(20),
--       @SectionID varchar(20),
--       @SectionID_2 varchar(20),
--       @StopDate DateTime, 
--       @UserComments varchar(150), 
--       @RequestBy varchar(50),
--       @SQMOn numeric(7,3),
--       @SQMOff numeric(7,3),
--       @Cube numeric(7,3),
--       @MeterOn numeric(7,3),
--       @MeterOff numeric(7,3),
--	   @DayCrew varchar(10), 
--	   @NightCrew varchar(10),
--	   @AfternoonCrew varchar(10),
--	   @RovingCrew varchar(10),
--	   @StartDate datetime,
--	   @MiningMethod varchar(20),
--	   @OldWorkplaceID varchar(20),
--	   @activity int,
--	   @Facelength numeric(13,3)

--SET @requestType =5 
--SET @ProdMonth = 201506
--SET @WorkplaceID = 18657
--SET @SectionID = '2563460'
--SET @SectionID_2 ='2433460'
--SET @StopDate = '2012/05/25'
--SET @StartDate = '2015/05/13'
--SET @UserComments = 'done'
--SET @RequestBy = 'MINEWARE_DOLF'
--SET @SQMOn = 400
--SET @SQMOff  = 0
--SET @Cube  = 0
--SET @MeterOn  = 0
--SET @MeterOff  = 0
--set @DayCrew='031IM3TBA'
--set @NightCrew=''
--set @AfternoonCrew=''
--set @RovingCrew=''
--set @OldWorkplaceID ='BE23B N41 04W'
--set @activity = 0

DECLARE @theShaft varchar(50),
@ucPlanningValueChanges INT,
@ucCrewMinerChange int,
@ucAddWorkplace INT, 
@StopWorkplace int,
@MovePlanning INT,
@StartWorkplace int,
@MiningMethodChange int, 
@ChnagrequestID int,
@theSection varchar(50),
@AppReqd int,
@DrillRigChange int,
@DeleteWorkplace int
SET @StopWorkplace = 1
SET @ucCrewMinerChange=3
SET @ucAddWorkplace=2
SET @ucPlanningValueChanges=4
SET @MovePlanning=5
SET @StartWorkplace=6
SET @MiningMethodChange=7
set @DrillRigChange=8
set @DeleteWorkplace=9

IF @requestType = @StopWorkplace 
BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST 
		  ([ProdMonth]
		  ,[RequestBy]
		  ,[SectionID]
		  ,[SectionID_2]
		  ,[WorkplaceID]
		  ,[ChangeID]
		  ,[DayCrew]
		  ,[NightCrew]
		  ,[AfternoonCrew]
		  ,[RovingCrew]
		  ,[StartDate]
		  ,[StopDate]
		  ,[Comments]
		  ,[ReefSQM]
		  ,[WasteSQM]
		  ,[CubicMeters]
		  ,[Meters]
		  ,[MetersWaste]
		  ,[MiningMethod]
		  ,[OldWorkplaceID]
		  ,[Activity]
		  ,[FL] 
		  ,[DrillRig]
		  ,[DeleteBookings])

	VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@StopWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
			@SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig,@DeleteBookings)
			SELECT @ChnagrequestID = SCOPE_IDENTITY()

			--select * from PREPLANNING_CHANGEREQUEST 

	SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
	WHERE PRODMONTH = @ProdMonth and
		  SECTIONID = @SectionID_2 ) 

	SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
	WHERE SC.PRODMONTH = @ProdMonth and
		  SC.Name_2 = @theSection)
      
     

	-- Get all the users that needs to approve tha request
	DECLARE @User1 varchar(50), @User2 varchar(50), @Department varchar(50),@approvalReqd int
	DECLARE db_Approval CURSOR FOR  
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		--  rs.Department  =ppns.Department AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--rs.Section = SC.SECTIONID_3 AND
	RS.SECTION=SC.SECTIONID_3
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		 -- PPNS.Section = '2' and
		  PPNS.ChangeID = 1   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null
		  UNION
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		  --rs.UserID =ppns.UserID AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--PPNS.Section = SC.SECTIONID_2 AND
	RS.SECTION=SC.SECTIONID_2
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		  --PPNS.Section = '318460' and
		  PPNS.ChangeID = 1   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null

	OPEN db_Approval   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval INTO @User1,@User2,@Department,@approvalReqd
WHILE @@FETCH_STATUS = 0   
BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
				(ChangeRequestID,
				 User1,
				 User2,
				 Department,
				 RequestDate,
				 ApprovalRequired	         )
	VALUES (@ChnagrequestID,@User1,@User2,@Department,GETDATE(),@approvalReqd )
	         
	FETCH NEXT FROM db_Approval INTO @User1,@User2,@Department,@approvalReqd
	END 

	CLOSE db_Approval   
	DEALLOCATE db_Approval   

	-- This mail
	EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

	-- End Mail       

	END  -- IF @requestType = @StopWorkplace   

IF @requestType = @ucCrewMinerChange 
BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST 
		  ([ProdMonth]
		  ,[RequestBy]
		  ,[SectionID]
		  ,[SectionID_2]
		  ,[WorkplaceID]
		  ,[ChangeID]
		  ,[DayCrew]
		  ,[NightCrew]
		  ,[AfternoonCrew]
		  ,[RovingCrew]
		  ,[StartDate]
		  ,[StopDate]
		  ,[Comments]
		  ,[ReefSQM]
		  ,[WasteSQM]
		  ,[CubicMeters]
		  ,[Meters]
		  ,[MetersWaste]
		  ,[MiningMethod]
		  ,[OldWorkplaceID]
		  ,[Activity]
		  ,[FL] 
		  ,[DrillRig])

	VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@ucCrewMinerChange,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
			@SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
			SELECT @ChnagrequestID = SCOPE_IDENTITY()

	SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
	WHERE PRODMONTH = @ProdMonth and
		  SECTIONID = @SectionID_2 ) 

	SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
	WHERE SC.PRODMONTH = @ProdMonth and
		  SC.Name_2 = @theSection)
      
     

	-- Get all the users that needs to approve tha request
	DECLARE @User11 varchar(50), @User22 varchar(50), @Department1 varchar(50),@approvalReqd1 int
	DECLARE db_Approval1 CURSOR FOR  
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		--  rs.Department  =ppns.Department AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--rs.Section = SC.SECTIONID_3 AND
	RS.SECTION=SC.SECTIONID_3
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		 -- PPNS.Section = '2' and
		  PPNS.ChangeID = 3   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null
		  UNION
			Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
		  --rs.UserID =ppns.UserID AND
		  RS.DEPARTMENT=PPNS.DEPARTMENT
	INNER JOIN SECTION_COMPLETE SC ON
	--PPNS.Section = SC.SECTIONID_2 AND
	RS.SECTION=SC.SECTIONID_2
	WHERE SC.PRODMONTH = @ProdMonth and 
		  SC.SECTIONID_2 = @SectionID_2 and 
		  --PPNS.Section = '318460' and
		  PPNS.ChangeID = 3   and active=1 and RS.SecurityType =1 and
		  rs.UserID > '' and rs.UserID is not null

	OPEN db_Approval1   
	-- end

	-- Add a approval record for each department that needs to approve the request.
	FETCH NEXT FROM db_Approval1 INTO @User11,@User22,@Department1,@approvalReqd1
	WHILE @@FETCH_STATUS = 0   
	BEGIN

	INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
				(ChangeRequestID,
				 User1,
				 User2,
				 Department,
				 RequestDate	      ,
				 ApprovalRequired	         )
	VALUES (@ChnagrequestID,@User11,@User22,@Department1,GETDATE(),@approvalReqd1 )
	         
	FETCH NEXT FROM db_Approval1 INTO @User11,@User22,@Department1,@approvalReqd1
END 

CLOSE db_Approval1   
DEALLOCATE db_Approval1   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

-- End Mail      
end 

IF @requestType = @ucAddWorkplace 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@ucAddWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User111 varchar(50), @User222 varchar(50), @Department11 varchar(50),@approvalReqd2 int
DECLARE db_Approval11 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 2   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 2   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval11   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval11 INTO @User111,@User222,@Department11,@approvalReqd2
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User111,@User222,@Department11,GETDATE(), @approvalReqd2)
	         
FETCH NEXT FROM db_Approval11 INTO @User111,@User222,@Department11,@approvalReqd2
END 

CLOSE db_Approval11   
DEALLOCATE db_Approval11   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

-- End Mail  
END

IF @requestType = @ucPlanningValueChanges 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@ucPlanningValueChanges,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User1111 varchar(50), @User2222 varchar(50), @Department111 varchar(50),@approvalReqd3 int
DECLARE db_Approval111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 4   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 4   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval111 INTO @User1111,@User2222,@Department111,@approvalReqd3
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	          ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User1111,@User2222,@Department111,GETDATE(),@approvalReqd3)
	         
FETCH NEXT FROM db_Approval111 INTO @User1111,@User2222,@Department111,@approvalReqd3
END 

CLOSE db_Approval111   
DEALLOCATE db_Approval111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 

-- End Mail  
end

IF @requestType = @MovePlanning 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

SELECT [ProdMonth]
      ,@RequestBy [RequestBy]
      ,[SectionID]
      ,@WorkplaceID [WorkplaceID]
      ,@MovePlanning [ChangeID]
      ,OrgUnitDay
      ,OrgUnitNight
      ,OrgUnitAfternoon
      ,RomingCrew
      ,[StartDate]
      ,StoppedDate
      ,@UserComments [Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,CubicMetres
      ,ReefAdv
      ,WasteAdv
	  ,@MiningMethod [MiningMethod]
	  ,@OldWorkplaceID [OldWorkplaceID]
	  ,[Activity]
	   ,@Facelength,@DrillRig FROM planmonth where WorkplaceID = @OldWorkplaceID and prodmonth = @ProdMonth and plancode = 'MP' and [Activity] = @activity
	   SELECT @ChnagrequestID = SCOPE_IDENTITY()
--VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@MovePlanning,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
--        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength)
		

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User11111 varchar(50), @User22222 varchar(50), @Department1111 varchar(50),@approvalReqd4 int
DECLARE db_Approval11 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 5   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 5   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval11    
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval11 INTO @User11111,@User22222,@Department1111,@approvalReqd4
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	          ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User11111,@User22222,@Department1111,GETDATE(),@approvalReqd4 )
	         
FETCH NEXT FROM db_Approval11 INTO @User11111,@User22222,@Department1111,@approvalReqd4
END 

CLOSE db_Approval11   
DEALLOCATE db_Approval11   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @StartWorkplace 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@StartWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User111111 varchar(50), @User222222 varchar(50), @Department11111 varchar(50),@approvalReqd5 int
DECLARE db_Approval111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 6   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 6   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval111 INTO @User111111,@User222222,@Department11111,@approvalReqd5
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	        ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User111111,@User222222,@Department11111,GETDATE(),@approvalReqd5 )
	         
FETCH NEXT FROM db_Approval111 INTO @User111111,@User222222,@Department11111,@approvalReqd5
END 

CLOSE db_Approval111   
DEALLOCATE db_Approval111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @MiningMethodChange 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@MiningMethodChange,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User1111111 varchar(50), @User2222222 varchar(50), @Department111111 varchar(50),@approvalReqd6 int
DECLARE db_Approval1111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 7   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 7   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval1111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval1111 INTO @User1111111,@User2222222,@Department111111,@approvalReqd6
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User1111111,@User2222222,@Department111111,GETDATE(),@approvalReqd6 )

	         
FETCH NEXT FROM db_Approval1111 INTO @User1111111,@User2222222,@Department111111,@approvalReqd6
END 

CLOSE db_Approval1111   
DEALLOCATE db_Approval1111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @DrillRigChange 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@DrillRigChange,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User11111111 varchar(50), @User22222222 varchar(50), @Department1111111 varchar(50),@approvalReqd7 int
DECLARE db_Approval11111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 8   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 8   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval11111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval11111 INTO @User11111111,@User22222222,@Department1111111,@approvalReqd7
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User11111111,@User22222222,@Department1111111,GETDATE(),@approvalReqd7 )

	         
FETCH NEXT FROM db_Approval11111 INTO @User1111111,@User2222222,@Department111111,@approvalReqd7
END 

CLOSE db_Approval11111   
DEALLOCATE db_Approval11111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end

IF @requestType = @DeleteWorkplace 
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST 
      ([ProdMonth]
      ,[RequestBy]
      ,[SectionID]
      ,[SectionID_2]
      ,[WorkplaceID]
      ,[ChangeID]
      ,[DayCrew]
      ,[NightCrew]
      ,[AfternoonCrew]
      ,[RovingCrew]
      ,[StartDate]
      ,[StopDate]
      ,[Comments]
      ,[ReefSQM]
      ,[WasteSQM]
      ,[CubicMeters]
      ,[Meters]
      ,[MetersWaste]
	  ,[MiningMethod]
	  ,[OldWorkplaceID]
	  ,[Activity]
	  ,[FL] 
	  ,[DrillRig])

VALUES (@ProdMonth,@RequestBy,@SectionID,@SectionID_2,@WorkplaceID,@DeleteWorkplace,@DayCrew,@NightCrew,@AfternoonCrew ,@RovingCrew ,@StartDate , @StopDate,@UserComments,
        @SQMOn,@SQMOff,@Cube,@MeterOn,@MeterOff,@MiningMethod,@OldWorkplaceID,@activity,@Facelength,@DrillRig)
		SELECT @ChnagrequestID = SCOPE_IDENTITY()

SET @theSection = (SELECT Name_2 FROM SECTION_COMPLETE
WHERE PRODMONTH = @ProdMonth and
      SECTIONID = @SectionID_2 ) 

SET @theShaft = (SELECT DISTINCT Name_3 FROM SECTION_COMPLETE SC 
WHERE SC.PRODMONTH = @ProdMonth and
      SC.Name_2 = @theSection)
      
     

-- Get all the users that needs to approve tha request
DECLARE @User111111111 varchar(50), @User222222222 varchar(50), @Department11111111 varchar(50),@approvalReqd8 int
DECLARE db_Approval111111 CURSOR FOR  
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	--  rs.Department  =ppns.Department AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--rs.Section = SC.SECTIONID_3 AND
RS.SECTION=SC.SECTIONID_3
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
     -- PPNS.Section = '2' and
      PPNS.ChangeID = 9   and active=1 and RS.SecurityType =1 and
	  rs.UserID > '' and rs.UserID is not null
	  UNION
	    Select DISTINCT rs.UserID,'' CPM_UserID2,PPNS.Department,ApprovalRequired  from REVISEDPLANNING_USERSECURITY_ACTIONS PPNS INNER join REVISEDPLANNING_SECURITY rs on
	  --rs.UserID =ppns.UserID AND
	  RS.DEPARTMENT=PPNS.DEPARTMENT
INNER JOIN SECTION_COMPLETE SC ON
--PPNS.Section = SC.SECTIONID_2 AND
RS.SECTION=SC.SECTIONID_2
WHERE SC.PRODMONTH = @ProdMonth and 
      SC.SECTIONID_2 = @SectionID_2 and 
      --PPNS.Section = '318460' and
      PPNS.ChangeID = 9   and active=1 and RS.SecurityType = 1 and
	  rs.UserID > '' and rs.UserID is not null

OPEN db_Approval111111   
-- end

-- Add a approval record for each department that needs to approve the request.
FETCH NEXT FROM db_Approval111111 INTO @User111111111,@User222222222,@Department11111111,@approvalReqd8
WHILE @@FETCH_STATUS = 0   
BEGIN

INSERT INTO PREPLANNING_CHANGEREQUEST_APPROVAL
            (ChangeRequestID,
	         User1,
	         User2,
	         Department,
	         RequestDate	         ,
			 ApprovalRequired	         )
VALUES (@ChnagrequestID,@User111111111,@User222222222,@Department11111111,GETDATE(),@approvalReqd8 )

	         
FETCH NEXT FROM db_Approval111111 INTO @User11111111,@User22222222,@Department1111111,@approvalReqd8
END 

CLOSE db_Approval111111   
DEALLOCATE db_Approval111111   

-- This mail
EXEC sp_PrePlanning_ChangeMail @ChnagrequestID 
end


GO

CREATE Procedure [dbo].[sp_RevisedPlanning_StartWorkplace]
@RequestID INT,
@UserID VARCHAR(10)

AS

DECLARE
@Prodmonth NUMERIC(7,0),
@Cubes NUMERIC(7,3),
@SQM NUMERIC(7,3),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@ActivityCode NUMERIC(7,0), 
@SQL Varchar(1000),
@NewWorkplaceID VARCHAR(20),
@NewSectionID VARCHAR(20),
@Meters NUMERIC(7,3)





--SET @RequestID = 26
--SET @UserID = 'MINEWARE'

SET @SQM = (SELECT CR.ReefSQM + CR.WasteSQM FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Cubes = (SELECT CR.CubicMeters FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @ActivityCode =  (SELECT PP.Activity FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.PLANCODE='MP')

SET @Prodmonth = (SELECT CR.ProdMonth FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.PLANCODE='MP')

SET @SectionID = (SELECT CR.SectionID FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.PLANCODE='MP')


SET @WorkplaceID = (SELECT CR.WorkplaceID FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.PLANCODE='MP')


IF  @ActivityCode = 0
BEGIN

IF @SQM >= 0  or @Cubes >= 0-- Stoping SQM
BEGIN 




UPDATE PLANMONTH SET SQM  = CR.ReefSQM + CR.WasteSQM,
						ReefSQM  = CR.ReefSQM,
                       WasteSQM  = CR.WasteSQM,
                       StoppedDate = null,
					   StartDate=CR.StartDate,
					   CubicMetres = cr.CubicMeters,
					   IsStopped = ''
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH  PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and  PP.PLANCODE='MP'

--UPDATE PlanMonth SET Squaremetres  = CR.OnReefSQM + CR.OffReefSQM,
--                     --OnReefCall = CR.OnReefSQM,
--                     --OffReefCall = CR.OffReefSQM,
--                     IsStopped = '',
--                     SQUAREMETRES = CR.OnReefSQM + CR.OffReefSQM,
--                     OffReefSQM = CR.OffReefSQM,
--                     OnReefSQM = CR.OnReefSQM,
--                     StoppedDate = null,
--					 StartDate =CR.StartDate
--					 --DateTimeStopped = null,
--					 --StoppedUserID = null 
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PLANMONTH  PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN PlanMonth PM on
--PM.PRODMONTH = pp.Prodmonth and
--PM.WORKPLACEID = PP.Workplaceid and
--PM.SECTIONID = pp.Sectionid
--WHERE CR.ChangeRequestID = @RequestID and PM.IsCubics = 'N'



      set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+',''' + 'N' + ''''
      
      exec (@SQL)
END  -- End SQM    

--IF @Cubes >= 0  -- Stoping  QUBES
--BEGIN 






--UPDATE PLANMONTH SET StoppedDate = null,
--					   StartDate=CR.StartDate,
--                       CubicMetres = cr.CubicMeters,
--					   RomingCrew =CR.RovingCrew ,
--					   --IsStopped = 0,
--					   IsStopped = '',
--                     SQM = CR.ReefSQM + CR.WasteSQM,
--                     --CUBICMETRES = CR.CubicMeters,
--                     --StoppedDate = null,
--					 --StartDate=CR.StartDate,
--					 OrgunitDay =CR.DayCrew ,
--					 OrgunitAfterNoon =CR.AfternoonCrew ,
--					 OrgunitNight =CR.NightCrew
-- FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--WHERE CR.ChangeRequestID = @RequestID AND IsCubics ='Y'

----UPDATE PLANMONTH SET IsStopped = '',
----                     SQUAREMETRES = CR.OnReefSQM + CR.OffReefSQM,
----                     CUBICMETRES = CR.CubicMeters,
----                     StoppedDate = null,
----					 StartDate=CR.StartDate,
----					 OrgunitDay =CR.DayCrew ,
----					 OrgunitAfterNoon =CR.AfternoonCrew ,
----					 OrgunitNight =CR.NightCrew ,
----					 --RovingCrew =CR.RovingCrew,
----					 --DateTimeStopped = null,
----					 --StoppedUserID = null 
------SELECT * 
----FROM PrePlanning_ChangeRequest CR 
----INNER JOIN PLANMONTH PP on
----CR.SectionID = PP.Sectionid and
----CR.SectionID_2 = pp.Sectionid_2 and
----CR.WorkplaceID = PP.Workplaceid and
----CR.ProdMonth = PP.Prodmonth
----INNER JOIN PlanMonth PM on
----PM.PRODMONTH = pp.Prodmonth and
----PM.WORKPLACEID = PP.Workplaceid and
----PM.SECTIONID = pp.Sectionid
----WHERE CR.ChangeRequestID = @RequestID and PM.IsCubics = 'Y'



--      --set @SQL = '[SP_Save_StopeCubics_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+','''+'Y'+''''
      
--      --exec (@SQL)

--	   set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+','''+'Y'+''''
      
--      exec (@SQL)
--END -- end Cubes

END  -- end Stoping      

SET @Meters = (SELECT CR.Meters + CR.MetersWaste FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)



IF  @ActivityCode = 1
BEGIN



IF @Meters >= 0  or @Cubes >= 0-- Development Meters
BEGIN 




					   UPDATE PLANMONTH SET ReefAdv = CR.Meters ,
                       WasteAdv  = CR.MetersWaste,
                       StoppedDate = null,
					   StartDate=CR.StartDate,
					   IsStopped = '',
					   [CubicMetres] = CR.[CubicMeters],
					   MetresAdvance  =  CR.Meters + CR.MetersWaste
                     --[METRESADVANCE] =  CR.Meters, --+CR.MetersWaste,
                     --OnReefAdv   = CR.Meters,
                     --OffReefAdv   = CR.MetersWaste
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.WorkplaceID and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID  and PP.PLANCODE='MP'


--UPDATE PLANMONTH SET Squaremetres  =  CR.Meters +CR.MetersWaste,
--                     [METRESADVANCE] =  CR.Meters, --+CR.MetersWaste,
--                     OnReefAdv   = CR.Meters,
--                     OffReefAdv   = CR.MetersWaste,
--                     --OnReefCall   = CR.Meters,
--                     --OffReefCall   = CR.MetersWaste,
--                     IsStopped = '',
--                     StoppedDate = null,
--					 StartDate=CR.StartDate,
--					 --DateTimeStopped = null,
--					 --StoppedUserID = null
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PLANMONTH PP on
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.WorkplaceID  and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN PlanMonth PM on
--PM.PRODMONTH = pp.Prodmonth and
--PM.WORKPLACEID = PP.Workplaceid and
--PM.SECTIONID = pp.Sectionid
--WHERE CR.ChangeRequestID = @RequestID and PM.IsCubics = 'N'


      set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+',''' + 'N' + ''''
      
      exec (@SQL)
END  -- End SQM    

--IF @Cubes >= 0  -- development  QUBES
--BEGIN 



--					   UPDATE PLANMONTH SET ReefAdv = CR.Meters ,					   
--                       WasteAdv = CR.MetersWaste,
--					   [CubicMetres] = CR.[CubicMeters],
--                       StoppedDate = null,
--					   StartDate=CR.StartDate,
--					   IsStopped = ''
-- FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PLANMONTH PP on
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.WorkplaceID and
--CR.ProdMonth = PP.Prodmonth
--WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'Y'

--UPDATE PlanMonth SET [CubicMetres] = CR.[CubicMeters],
--                     IsStopped = '',
--                     StoppedDate = null,
--					 StartDate=CR.StartDate,
--					 --DateTimeStopped = null,
--					 --StoppedUserID = null
----SELECT * 
--FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PrePlanning PP on
----CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.WorkplaceID  and
--CR.ProdMonth = PP.Prodmonth
--INNER JOIN PlanMonth PM on
--PM.PRODMONTH = pp.Prodmonth and
--PM.WORKPLACEID = PP.Workplaceid and
--PM.SECTIONID = pp.Sectionid
--WHERE CR.ChangeRequestID = @RequestID and PM.IsCubics = 'Y'



--      set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+','''+'Y'+''''
      
--      exec (@SQL)
--END -- end Cubes

END  -- end Development 







GO

CREATE Procedure [dbo].[sp_RevisedPlanning_Status]

as 

--SELECT DISTINCT 
--PPCRA .Approved ,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate ,PPCR .DayCrew ,PPCR .NightCrew ,PPCR.AfternoonCrew ,PPCR .RovingCrew 
--,PPCR .Meters ,PPCR .MetersWaste ,PPCR .CubicMeters ,PPCR.ReefSQM ,PPCR .WasteSQM 
--,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.NAME RequestedBy,PPRT.ChangeID,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
--FROM PrePlanning_ChangeRequestApproval PPCRA
--INNER JOIN PrePlanning_ChangeRequest PPCR ON
--PPCR.ChangeRequestID = PPCRA.ChangeRequestID
--INNER JOIN PrePlanning_ReplanningTypes PPRT ON
--PPCR.ChangeID = PPRT.ChangeID
--INNER JOIN USERS U on 
--U.USERID = PPCR.RequestBy
--INNER JOIN WORKPLCE WP on
--WP.WORKPLACEID = PPCR.WorkplaceID
--INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTIONS_COMPLETE) SECTION ON
--Section.SECTIONID_2 = PPCR.SectionID_2 and
--Section.PRODMONTH = PPCR.ProdMonth 
--INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTIONS_COMPLETE) MINER ON
--MINER.SECTIONID = PPCR.SectionID and
--MINER.PRODMONTH = PPCR.ProdMonth 


SELECT DISTINCT 
--*
PPCR.Comments ,PPCR .DayCrew ,PPCR .NightCrew ,PPCR.AfternoonCrew ,PPCR .RovingCrew ,
PPCR .Meters ,PPCR .MetersWaste ,PPCR .CubicMeters ,PPCR.ReefSQM ,PPCR .WasteSQM ,
PPCR.ChangeRequestID , U.UserID  RequestedBy,PPRT.ChangeID,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PrePlanning_ChangeRequest PPCR 

INNER JOIN CODE_PREPLANNINGTYPES PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth
ORDER BY ChangeRequestID DESC








GO

CREATE procedure [dbo].[sp_RevisedPlanning_Stop_Workplace] 
--DECLARE 

@ChangeRequestID INT, @UserID varchar(20)
AS

DECLARE
@Prodmonth NUMERIC(7,0),
@Cubes NUMERIC(7,3),
@SQM NUMERIC(7,3),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@Activity NUMERIC(7,0), 
@SQL Varchar(1000),
@Meters NUMERIC(7,3),
@DeleteBookings Bit

--SET @ChangeRequestID = 1343
--SET @UserID = 'MINEWARE_Iggy'

SELECT 
@SQM = (CR.ReefSQM + CR.WasteSQM), @Cubes = CR.CubicMeters,
@Activity = PP.Activity,
@Prodmonth = pp.prodmonth,
@SectionID = pp.SectionID,
@WorkplaceID = pp.WorkplaceID,
@DeleteBookings = CR.DeleteBookings,
@Meters = CR.Meters + CR.MetersWaste
FROM PrePlanning_ChangeRequest CR
INNER JOIN Planmonth PP on
CR.SectionID = PP.Sectionid and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth 
WHERE cr.ChangerequestID = @ChangeRequestID and pp.Plancode = 'MP'

--select @Meters = (SELECT CR.Meters + CR.MetersWaste), 
--@Activity = PP.Activity,
--@Prodmonth = pp.prodmonth,
--@SectionID = pp.SectionID,
--@Sectionid_2 = pp.SectionID_2,
--@WorkplaceID = pp.WorkplaceID FROM PrePlanning_ChangeRequest CR
--INNER JOIN Planmonth PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth  
--WHERE CR.ChangeRequestID = @ChangeRequestID

IF  @Activity = 0
BEGIN

	IF @SQM  >= 0  -- Stoping SQM
	BEGIN 

		UPDATE PLANMONTH SET ReefSQM = CR.ReefSQM,
							   WasteSQM = CR.WasteSQM,
							   SQM = CR.ReefSQM + CR.WasteSQM,
							   StoppedDate = CR.StopDate,
							   KG = Round((CR.ReefSQM*(CMGT/100)*b.density)/1000,3),
							   tons = Round((CR.ReefSQM + CR.WasteSQM)*(SW/100)*b.density,0),
							   Reeftons = Round(CR.ReefSQM*(SW/100)*b.density,0),
							   Wastetons = Round(CR.WasteSQM*(SW/100)*b.density,0),
							   IsStopped ='Y'
		FROM PrePlanning_ChangeRequest CR 
		INNER JOIN Planmonth PP on
		CR.SectionID = PP.Sectionid and
		CR.WorkplaceID = PP.Workplaceid and
		CR.ProdMonth = PP.Prodmonth inner join 
			 WORKPLACE b on
			 PP.Workplaceid = b.workplaceid 
		WHERE cr.ChangerequestID = @ChangerequestID and pp.Plancode = 'MP' and pp.IsCubics = 'N'



		If @DeleteBookings = 1
		begin
		  UPDATE Planning set BOOKTONS=0,
					BookReefTons =0,
					BOOKWASTETONS=0,
					BOOKGRAMS=0.000,
					BOOKMETRESADVANCE=0.000,
					BOOKREEFADV=0.000,
					BOOKWASTEADV=0.000,
					BOOKSQM=0,
					BOOKREEFSQM=0,
					BOOKWASTESQM=0,
					BOOKVOLUME=0,
					BOOKREEFVOLUME=0,
					BookWasteVolume=0,
					BOOKFL=0.000,
					BOOKSW=0,
					BOOKCMGT=0,
					BOOKGT=0,
					BOOKCW=0 
		   Where Prodmonth = @Prodmonth and
				 Sectionid = @SectionID and
				 Activity = @Activity and
				 WorkplaceID = @WorkplaceID

			--Delete from book_problem
			--Where Prodmonth = @Prodmonth and
			--	 Sectionid = @SectionID and
			--	 Activity = @Activity and
			--	 WorkplaceID = @WorkplaceID
		end

		--set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+',''' + 'N' + ''''
      
		--exec (@SQL)
	END  -- End SQM    

	IF @Cubes >= 0  -- Stoping  QUBES
	BEGIN 


		UPDATE Planmonth SET IsStopped = 'Y',
							 SQM = CR.ReefSQM + CR.WasteSQM,
							 CUBICMETRES = CR.CubicMeters,
							 StoppedDate = CR.StopDate
		 FROM PrePlanning_ChangeRequest CR 
		INNER JOIN Planmonth PP on
		CR.SectionID = PP.Sectionid and
		CR.WorkplaceID = PP.Workplaceid and
		CR.ProdMonth = PP.Prodmonth
		WHERE cr.ChangerequestID = @ChangerequestID and pp.Plancode = 'MP' and pp.IsCubics = 'N'

		If @DeleteBookings = 1
		begin
		  UPDATE Planning set BOOKTONS=0,
					BookReefTons =0,
					BOOKWASTETONS=0,
					BOOKGRAMS=0.000,
					BOOKMETRESADVANCE=0.000,
					BOOKREEFADV=0.000,
					BOOKWASTEADV=0.000,
					BOOKSQM=0,
					BOOKREEFSQM=0,
					BOOKWASTESQM=0,
					BOOKVOLUME=0,
					BOOKREEFVOLUME=0,
					BookWasteVolume=0,
					BOOKFL=0.000,
					BOOKSW=0,
					BOOKCMGT=0,
					BOOKGT=0,
					BOOKCW=0 
		   Where Prodmonth = @Prodmonth and
				 Sectionid = @SectionID and
				 Activity = @Activity and
				 WorkplaceID = @WorkplaceID

   --         Delete from book_problem
			--Where Prodmonth = @Prodmonth and
			--	 Sectionid = @SectionID and
			--	 Activity = @Activity and
			--	 WorkplaceID = @WorkplaceID
		end

		set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
		exec (@SQL)

			   --set @SQL = '[SP_Save_StopeCubics_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'Y'+''''
      
		--  exec (@SQL)
	END -- end Cubes

END  -- end Stoping      


IF  @Activity = 1
BEGIN

	IF @Meters >= 0  -- dev meters
	BEGIN 

		UPDATE PLANMONTH SET  ReefAdv  = CR.Meters ,
							   WasteAdv  = CR.MetersWaste,
							   StoppedDate = CR.StopDate,
								Metresadvance  = CR.Meters  + CR.MetersWaste ,
							   IsStopped = 'Y'
		 FROM PrePlanning_ChangeRequest CR 
		INNER JOIN Planmonth PP on
		CR.SectionID = PP.Sectionid and
		CR.WorkplaceID = PP.Workplaceid and
		CR.ProdMonth = PP.Prodmonth
		WHERE cr.ChangerequestID = @ChangerequestID and pp.Plancode = 'MP' and pp.IsCubics = 'N'

		If @DeleteBookings = 1
		begin
		  UPDATE Planning set BOOKTONS=0,
					BookReefTons =0,
					BOOKWASTETONS=0,
					BOOKGRAMS=0.000,
					BOOKMETRESADVANCE=0.000,
					BOOKREEFADV=0.000,
					BOOKWASTEADV=0.000,
					BOOKSQM=0,
					BOOKREEFSQM=0,
					BOOKWASTESQM=0,
					BOOKVOLUME=0,
					BOOKREEFVOLUME=0,
					BookWasteVolume=0,
					BOOKFL=0.000,
					BOOKSW=0,
					BOOKCMGT=0,
					BOOKGT=0,
					BOOKCW=0 
		   Where Prodmonth = @Prodmonth and
				 Sectionid = @SectionID and
				 Activity = @Activity and
				 WorkplaceID = @WorkplaceID

   --         Delete from book_problem
			--Where Prodmonth = @Prodmonth and
			--	 Sectionid = @SectionID and
			--	 Activity = @Activity and
			--	 WorkplaceID = @WorkplaceID
		end

		set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+',''' + 'N' + ''''
      
		exec (@SQL)
	END  -- End SQM    

	IF @Cubes >= 0  -- Stoping  QUBES
	BEGIN 


		UPDATE Planmonth SET IsStopped = 'Y',
							 Metresadvance  = CR.Meters  + CR.MetersWaste ,
							 CUBICMETRES = CR.CubicMeters,
							 StoppedDate = CR.StopDate
		 FROM PrePlanning_ChangeRequest CR 
		INNER JOIN Planmonth PP on
		CR.SectionID = PP.Sectionid and
		CR.WorkplaceID = PP.Workplaceid and
		CR.ProdMonth = PP.Prodmonth
		WHERE cr.ChangerequestID = @ChangerequestID and pp.Plancode = 'MP' and pp.IsCubics = 'N'

		If @DeleteBookings = 1
		begin
		  UPDATE Planning set BOOKTONS=0,
					BookReefTons =0,
					BOOKWASTETONS=0,
					BOOKGRAMS=0.000,
					BOOKMETRESADVANCE=0.000,
					BOOKREEFADV=0.000,
					BOOKWASTEADV=0.000,
					BOOKSQM=0,
					BOOKREEFSQM=0,
					BOOKWASTESQM=0,
					BOOKVOLUME=0,
					BOOKREEFVOLUME=0,
					BookWasteVolume=0,
					BOOKFL=0.000,
					BOOKSW=0,
					BOOKCMGT=0,
					BOOKGT=0,
					BOOKCW=0 
		   Where Prodmonth = @Prodmonth and
				 Sectionid = @SectionID and
				 Activity = @Activity and
				 WorkplaceID = @WorkplaceID

   --         Delete from book_problem
			--Where Prodmonth = @Prodmonth and
			--	 Sectionid = @SectionID and
			--	 Activity = @Activity and
			--	 WorkplaceID = @WorkplaceID
		end


		set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@Activity as Varchar(30))+','''+'N'+''''
      
		exec (@SQL)
	END -- end Cubes

END  -- end dev      


GO

CREATE Procedure [dbo].[sp_RevisedPlanning_StopWorkplace_Approve] 
--DECLARE 

@RequestID INT,
@UserID VARCHAR(10)

AS

DECLARE
@Prodmonth NUMERIC(7,0),
@Cubes NUMERIC(7,3),
@SQM NUMERIC(7,3),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@ActivityCode NUMERIC(7,0), 
@SQL Varchar(1000),
@NewWorkplaceID VARCHAR(20),
@NewSectionID VARCHAR(20),
@Meters NUMERIC(7,3),
@OldWorkplaceDesc VARCHAR(20)

--SET @RequestID = 1518
--SET @UserID = 'MINEWARE_DOLF'

SET @SQM = (SELECT CR.ReefSQM + CR.WasteSQM FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Cubes = (SELECT CR.CubicMeters FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

--SET @ActivityCode =  (SELECT PP.Activitycode FROM PrePlanning_ChangeRequest CR 
--INNER JOIN PrePlanning PP on
--CR.SectionID = PP.Sectionid and
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--WHERE CR.ChangeRequestID = @RequestID)

SET @Prodmonth=(SELECT CR.Prodmonth FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

set @Sectionid_2 =(SELECT CR.Sectionid_2 FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @ActivityCode =(SELECT CR.Activity FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
SET @NewWorkplaceID=(SELECT CR.WorkplaceID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

IF  @ActivityCode = 0
BEGIN



IF @SQM > 0  -- Stoping SQM
BEGIN 



SET @NewSectionID=(SELECT CR.SectionID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
--SELECT @NewSectionID

SET @OldWorkplaceDesc =  (SELECT CR.OldWorkplaceID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID =  (SELECT  WorkplaceID from WORKPLACE
WHERE Description = @OldWorkplaceDesc)

DECLARE @countPlan int
 SET @countPlan = (SELECT COUNT(WorkplaceID) FROM dbo.PLANNING WHERE Prodmonth = @Prodmonth AND WorkplaceID = @NewWorkplaceID)
IF(@countPlan > 0)
BEGIN
DELETE FROM dbo.PLANNING WHERE Prodmonth = @Prodmonth AND WorkplaceID = @NewWorkplaceID
END


UPDATE [dbo].[PLANNING] SET  WORKPLACEID=@NewWorkplaceID,SECTIONID=@NewSectionID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth AND PLANCODE='MP' and Activity = @ActivityCode
--update [dbo].[BOOK_PROBLEM] set WORKPLACEID=@NewWorkplaceID,SECTIONID=@NewSectionID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth and Activity = @ActivityCode
--UPDATE [dbo].[PLANNING] SET  SECTIONID=@NewSectionID WHERE WORKPLACEID=@NewWorkplaceID  AND PRODMONTH=@Prodmonth AND PLANCODE='MP'

					 UPDATE PLANMONTH SET 
                     WasteSQM = 0,
                     ReefSQM = 0,
					 SQM = 0,
					 Kg=0,
					 FL=0,
					 Tons=0,
					 ReefTons=0,
					 WasteTons=0,
					 CubicMetres=0,
					 CubicsTons=0,
					 CubicGrams=0, 
                     StoppedDate = CR.StartDate
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.OldWorkplaceID = PP.WorkplaceID and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'N' AND PP.PLANCODE='MP'




      set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+',''' + 'N' + ''''
      
      exec (@SQL)
END  -- End SQM    

IF @Cubes > 0  -- Stoping  QUBES
BEGIN 


SET @Prodmonth=(SELECT CR.Prodmonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @SectionID = (SELECT CR.sectionid  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @OldWorkplaceDesc =  (SELECT CR.OldWorkplaceID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID =  (SELECT  WorkplaceID from WORKPLACE
WHERE Description = @OldWorkplaceDesc)

UPDATE PLANMONTH SET IsStopped = 'Y',
					WasteSQM = 0,
                     ReefSQM = 0,
                     SQM = 0,
                     StoppedDate = CR.StartDate,
					 Kg=0,
					 FL=0,
					 Tons=0,
					 ReefTons=0,
					 WasteTons=0,
					 CubicMetres=0,
					 CubicsTons=0,
					 CubicGrams=0
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.OldWorkplaceID = PP.WorkplaceID and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'Y' AND PP.PLANCODE='MP'


      set @SQL = '[SP_Save_Stope_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)
END -- end Cubes

END  -- end Stoping      






SET @Meters = (SELECT CR.Meters + CR.MetersWaste FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)



IF  @ActivityCode = 1
BEGIN



IF @Meters > 0  -- Development Meters
BEGIN 


SET @NewSectionID=(SELECT CR.SectionID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)
--SELECT @NewSectionID

--SET @WorkplaceID =  (SELECT wp.[WorkplaceID]  FROM PrePlanning_ChangeRequest CR 
--INNER JOIN [dbo].[WORKPLACE] WP on
--CR.OldWorkplaceID = wp.[Description]
--WHERE CR.ChangeRequestID = @RequestID)

SET @OldWorkplaceDesc =  (SELECT CR.OldWorkplaceID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID =  (SELECT  WorkplaceID from WORKPLACE
WHERE Description = @OldWorkplaceDesc)


 SET @countPlan = (SELECT COUNT(WorkplaceID) FROM dbo.PLANNING WHERE Prodmonth = @Prodmonth AND WorkplaceID = @NewWorkplaceID)
IF(@countPlan > 0)
BEGIN
DELETE FROM dbo.PLANNING WHERE Prodmonth = @Prodmonth AND WorkplaceID = @NewWorkplaceID
END

UPDATE [dbo].[PLANNING] SET WORKPLACEID=@NewWorkplaceID,SECTIONID=@NewSectionID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth AND PLANCODE='MP' and Activity = @ActivityCode
--UPDATE [dbo].[BOOK_PROBLEM] SET  WORKPLACEID=@NewWorkplaceID,SECTIONID=@NewSectionID WHERE WORKPLACEID=@WorkplaceID  AND PRODMONTH=@Prodmonth and Activity = @ActivityCode

					   UPDATE PLANMONTH SET  ReefAdv   = 0,
                    --WasteAdv   = 0,
					 WasteAdv =0,
                     IsStopped = 'Y',
                     SQM =  CR.Meters +CR.MetersWaste,
                     Metresadvance =0 ,					 
                     StoppedDate = CR.StartDate,
					 Kg=0,
					 FL=0,
					 Tons=0,
					 ReefTons=0,
					 WasteTons=0,					 
					 CubicMetres=0,
					 CubicsTons=0,
					 CubicGrams=0
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and

CR.OldWorkplaceID = PP.WorkplaceID and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'N' AND PP.PLANCODE='MP'



      set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+',''' + 'N' + ''''
      
      exec (@SQL)
END  -- End SQM    

IF @Cubes > 0  -- Stoping  QUBES
BEGIN 


SET @Prodmonth=(SELECT CR.Prodmonth  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @SectionID = (SELECT CR.Sectionid  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)


--SET @WorkplaceID =  (SELECT CR.OldWorkplaceID  FROM PrePlanning_ChangeRequest CR 
--WHERE CR.ChangeRequestID = @RequestID)

SET @OldWorkplaceDesc =  (SELECT CR.OldWorkplaceID  FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID =  (SELECT  WorkplaceID from WORKPLACE
WHERE Description = @OldWorkplaceDesc)

UPDATE PLANMONTH SET IsStopped = 'Y',
					ReefAdv   = 0,
                    --WasteAdv   = 0,
					 WasteAdv =0,
                     SQM = 0,
                     StoppedDate = CR.StartDate,
					  Kg=0,
					 FL=0,
					 Tons=0,
					 ReefTons=0,
					 WasteTons=0,					 
					 CubicMetres=0,
					 CubicsTons=0,
					 CubicGrams=0
 FROM PrePlanning_ChangeRequest CR 
INNER JOIN PLANMONTH PP on
--CR.SectionID = PP.Sectionid and
CR.OldWorkplaceID = PP.WorkplaceID and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and PP.IsCubics = 'Y' AND PP.PLANCODE='MP'



      set @SQL = '[SP_Save_Dev_CyclePlan] '''+@UserID+''','+Cast(@Prodmonth as Varchar(30))+','''+@WorkplaceID+''','''+@SectionID+''','+Cast(@ActivityCode as Varchar(30))+','''+'N'+''''
      
      exec (@SQL)
END -- end Cubes

END  -- end Development 


GO

ALter Procedure [dbo].[sp_RevisedPlanning_Update] --201111
--DECLARE 
@Prodmonth Numeric(7,0)

AS

DECLARE @theShaft varchar(60),@theDepartment varchar(60),@theSection varchar(60)
--SET @Prodmonth = 201110 

-- Get all the shafts that currenlty is not in security table and add them
DECLARE db_Shafts CURSOR FOR  
SELECT Name_4 theShaft FROM vw_Section_from_shaft SFS
LEFT JOIN PrePlanning_Notification_Security PPNS ON
SFS.Name_4 = PPNS.Shaft
WHERE Prodmonth = @Prodmonth and 
      PPNS.Shaft is null
      
OPEN db_Shafts   
FETCH NEXT FROM db_Shafts INTO @theShaft  

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT @theShaft Shaft,'NONE' Section,DESCRIPTION Department FROM tblProfiles
FETCH NEXT FROM db_Shafts INTO @theShaft    
END  

CLOSE db_Shafts   
DEALLOCATE db_Shafts   

-- Get all the userprofiles that is not avalibal in security table and add them
DECLARE db_Departments CURSOR FOR
SELECT Description USERPROFILEID FROM tblProfiles UP
LEFT JOIN PrePlanning_Notification_Security PPNS ON
UP.Description = PPNS.Department and 
Section = 'NONE' 
WHERE PPNS.Shaft is null

OPEN db_Departments   
FETCH NEXT FROM db_Departments INTO @theDepartment 
WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT Name_4 theShaft,'NONE',@theDepartment FROM vw_SECTION_FROM_Shaft SFS
WHERE Prodmonth = @Prodmonth
FETCH NEXT FROM db_Departments INTO @theDepartment
END

CLOSE db_Departments   
DEALLOCATE db_Departments  

DECLARE db_Sections CURSOR FOR
SELECT Name_4 Shaft,Name_3 Section FROM vw_SECTION_FROM_UM SFUM
LEFT JOIN PrePlanning_Notification_Security PPNS ON
SFUM.Name_4 = PPNS.Shaft and
SFUM.Name_3 = PPNS.Section 
WHERE SFUM.Prodmonth = @Prodmonth and
PPNS.Shaft is null
Order by SFUM.Name_4,SFUM.Name_3 

OPEN db_Sections   
FETCH NEXT FROM db_Sections INTO @theShaft,@theSection 

WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT @theShaft Shaft,@theSection Section,Description Department FROM tblProfiles
FETCH NEXT FROM db_Sections INTO @theShaft,@theSection 
END

CLOSE db_Sections   
DEALLOCATE db_Sections

DECLARE db_Departments CURSOR FOR
SELECT Description USERPROFILEID FROM tblProfiles UP
LEFT JOIN PrePlanning_Notification_Security PPNS ON
UP.description = PPNS.Department and 
Section <> 'NONE' 
WHERE PPNS.Shaft is null

OPEN db_Departments 

FETCH NEXT FROM db_Departments INTO @theDepartment 
WHILE @@FETCH_STATUS = 0   
BEGIN
INSERT INTO PrePlanning_Notification_Security (Shaft,Section,Department)
SELECT Name_4 theShaft,Name_3 theSection,@theDepartment FROM vw_SECTION_FROM_UM SFUM
WHERE Prodmonth = @Prodmonth
FETCH NEXT FROM db_Departments INTO @theDepartment
END 

CLOSE db_Departments   
DEALLOCATE db_Departments 

--SELECT * FROM PrePlanning_Notification_Security

GO

CREATE procedure [dbo].[sp_RevisedPlanningAudit]
--Declare 
@Prodmonth numeric(7),
@ToProdmonth numeric(7),
@Section Varchar(30),
@RevisedType varchar(50)
as

--set @Prodmonth = 201507
--set @Section = '2818460'
--set @RevisedType='New Workplace'

--set @Prodmonth = 201502
--set @ToProdmonth = 201603
--set @Section = '1999'
--set @RevisedType='All'


Declare @TheLevel Int,
        @SQL1 Varchar(MAX),
        @GroupLevel Varchar(20),
        @SectionLevel Varchar(20)
 Declare @SyncroDB VarChar(50),@bonus varchar(50)
select @TheLevel = HIERARCHICALID from section where PRODMONTH = @ToProdmonth and
sectionid = @Section

--select @TheLevel

If @TheLevel = 1 
    set @GroupLevel = 'NAME_6'

  If @TheLevel = 2 
    set @GroupLevel = 'NAME_5'
    
  If @TheLevel = 3 
    set @GroupLevel = 'NAME_4'  
    
  If @TheLevel = 4 
    set @GroupLevel = 'NAME_3'  
    
  If @TheLevel = 5 
    set @GroupLevel = 'NAME_2'  
    
  If @TheLevel = 6 
    set @GroupLevel = 'NAME_1'

  If @TheLevel = 1 
    set @SectionLevel = 'SECTIONID_6'

  If @TheLevel = 2 
    set @SectionLevel = 'SECTIONID_5'
    
  If @TheLevel = 3 
    set @SectionLevel = 'SECTIONID_4'  
    
   If @TheLevel = 4 
    set @SectionLevel = 'SECTIONID_3'  
     
   If @TheLevel = 5 
    set @SectionLevel = 'SECTIONID_2'  
    
   If @TheLevel = 6 
    set @SectionLevel = 'SECTIONID_1'

   If @TheLevel = 7 
    set @SectionLevel = 'SECTIONID'
    
 if @RevisedType<>'All'
 begin
  Set @SQL1 = 'SELECT DISTINCT sd.ChangeRequestID, sc.'+@GroupLevel+',sc.Name,
                            sd.WorkplaceID, RU.Name + '' '' + RU.LastName FullName,
            			sd.ChangeType, 
            			sd.ProdMonth, 
            			sd.[Workplace Name],
            			sd.Name,sd.Comments,ChangeID , PPCR.DAYCREW,PPCR.NIGHTCREW,
						PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,
						PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description LockMethod,PPCR.FL,
            			CONVERT(varchar(50), ppcra.RequestDate,103) RequestDate, PPCR.DrillRig, p.DrillRig LockDrillRig
            			,max(Status) Status,s.name Section from StatusDetails sd  
            			left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on  
            			ppcra.ChangeRequestID =sd.ChangeRequestID   
            			INNER JOIN 				 
            			[dbo].[PREPLANNING_CHANGEREQUEST] PPCR on 
            			ppcra.ChangeRequestID = PPCR.ChangeRequestID 
            			INNER JOIN SECTION_COMPLETE SC on 
            			PPCR.SectionID = sc.SECTIONID and 
            			PPCR.ProdMonth = SC.PRODMONTH 
						INNER JOIN (SELECT * FROM '+@SyncroDB+'dbo.tblUsers WHERE  UserID <> '''') RU On
						PPCR.RequestBy = RU.UserID inner join section s on
						s.sectionid=ppcr.sectionid_2 and
						s.prodmonth=ppcr.prodmonth left join '+@bonus+'[dbo].[Bonus_PoolDefaults] BPD on
						PPCR.MiningMethod=BPD.TargetID 
						left join Planmonth P on
						PPCR.ProdMonth = p.PRODMONTH and
						PPCR.SectionID = p.SECTIONID and 
						PPCR.WorkplaceID = p.WorkplaceID and 
						p.PLancode = ''LP''
						left join '+@bonus+'[dbo].[Bonus_PoolDefaults] LBPD on
						p.TargetID =LBPD.TargetID  
                        	WHERE  CHANGETYPE=''' + @RevisedType + ''' AND SC.'+@SectionLevel+'=''' + @Section + ''' 

						and	ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'	
						--AND SC.'+@SectionLevel+' in (SELECT SectionID from SECTION WHERE SectionID IN (SELECT SectionID FROM USERS_SECTION where LinkType = ''P'')) --or
            			 
            			group by sd.ChangeRequestID,sc.'+@GroupLevel+',sc.Name,sd.ProdMonth,RU.Name,RU.LastName,sd.[Workplace Name],sd.Name,sd.Comments,sd.WorkplaceID,sd.ChangeType,ppcra.RequestDate,ChangeID,
						PPCR.DAYCREW,PPCR.NIGHTCREW,PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description,PPCR.FL,s.name, PPCR.DrillRig, p.DrillRig
            			order by sd.ProdMonth desc, sd.ChangeRequestID desc'
  Exec(@SQL1)
  --select @SQL1
  end
  else
  begin
   Set @SQL1 = 'SELECT DISTINCT sd.ChangeRequestID, sc.'+@GroupLevel+',sc.Name,
                            sd.WorkplaceID, RU.Name + '' '' + RU.LastName FullName,
            			sd.ChangeType, 
            			sd.ProdMonth, 
            			sd.[Workplace Name],
            			sd.Name,sd.Comments,ChangeID , PPCR.DAYCREW,PPCR.NIGHTCREW,
						PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,
						PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description LockMethod,PPCR.FL,
            			CONVERT(varchar(50), ppcra.RequestDate,103) RequestDate, PPCR.DrillRig, p.DrillRig LockDrillRig  
            			,max(Status) Status,s.name Section from StatusDetails sd  
            			left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on  
            			ppcra.ChangeRequestID =sd.ChangeRequestID   
            			INNER JOIN 				 
            			[dbo].[PREPLANNING_CHANGEREQUEST] PPCR on 
            			ppcra.ChangeRequestID = PPCR.ChangeRequestID 
            			INNER JOIN SECTION_COMPLETE SC on 
            			PPCR.SectionID = sc.SECTIONID and 
            			PPCR.ProdMonth = SC.PRODMONTH 
						INNER JOIN (SELECT * FROM '+@SyncroDB+'dbo.tblUsers WHERE  UserID <> '''') RU On
						PPCR.RequestBy = RU.UserID inner join section s on
						s.sectionid=ppcr.sectionid_2 and
						s.prodmonth=ppcr.prodmonth left join '+@bonus+'[dbo].[Bonus_PoolDefaults] BPD on
						PPCR.MiningMethod=BPD.TargetID
						left join Planmonth P on
						PPCR.ProdMonth = p.PRODMONTH and
						PPCR.SectionID = p.SECTIONID and 
						PPCR.WorkplaceID = p.WorkplaceID and 
						p.PLancode = ''LP''
						left join '+@bonus+'[dbo].[Bonus_PoolDefaults] LBPD on
						p.TargetID =LBPD.TargetID  
                        	WHERE   SC.'+@SectionLevel+'=''' + @Section + ''' 

						and ppcr.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
						and ppcr.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'
						--AND SC.'+@SectionLevel+' in (SELECT SectionID from SECTION WHERE SectionID IN (SELECT SectionID FROM USERS_SECTION where LinkType = ''P'')) --or
            			 
            			group by sd.ChangeRequestID,sc.'+@GroupLevel+',sc.Name,sd.ProdMonth,RU.Name,RU.LastName,sd.[Workplace Name],sd.Name,sd.Comments,sd.WorkplaceID,sd.ChangeType,ppcra.RequestDate,ChangeID,
						PPCR.DAYCREW,PPCR.NIGHTCREW,PPCR.AFTERNOONCREW,PPCR.ROVINGCREW,PPCR.REEFSQM,PPCR.WASTESQM,PPCR.METERS,PPCR.METERSWASTE,PPCR.CUBICMETERS,PPCR.STOPDATE,PPCR.STARTDATE,PPCR.OLDWORKPLACEID,BPD.Description,LBPD.Description,PPCR.FL,s.name, PPCR.DrillRig, p.DrillRig
            			order by sd.ProdMonth desc, sd.ChangeRequestID desc'
  Exec(@SQL1)
  --select @SQL1
  end


GO

CREATE procedure [dbo].[sp_RevisedPlanningAudit_StatusDetail]
--Declare
	@Prodmonth numeric(7),
	@ToProdmonth numeric(7)
as
Declare @SyncroDB VarChar(50)

--set @Prodmonth = 201601
--set @ToProdmonth = 201602


Declare 
        @SQL1 Varchar(MAX)
 Set @SQL1 = 'SELECT Distinct max(ppcra.ApprovedDeclinedDate) ApprovedDeclinedDate, ppcra.ApprovedDeclinedByUser, RU.Name + '' '' + 
 RU.LastName FullName, sd.ChangeRequestID, Dep.Description Department, sd.ProdMonth, sd.[Workplace Name], ppcra.Comments, Status 
 from StatusDetails sd 
 left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on 
 ppcra.ChangeRequestID = sd.ChangeRequestID 
 and sd.Department = ppcra.Department  
 inner join '+@SyncroDB+'[dbo].[tblDepartments] Dep on 	
 sd.Department = dep.[DepartmentID] 
 INNER JOIN 
 (SELECT * FROM '+@SyncroDB+'dbo.tblUsers WHERE  UserID <> '''') RU On
 PPCRA.ApprovedDeclinedByUser = RU.UserID 
 --and sd.prodmonth = ' + Convert(Varchar(7),@Prodmonth) +'  

 and sd.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
 and sd.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'	

 group by ppcra.ApprovedDeclinedByUser, RU.Name, RU.LastName, sd.ChangeRequestID, Dep.Description, sd.ProdMonth,sd.[Workplace Name], ppcra.Comments, Status
 union
 SELECT Distinct ppcra.ApprovedDeclinedDate, ppcra.ApprovedDeclinedByUser, RU.Name + '' '' + RU.LastName FullName, sd.ChangeRequestID, Dep.Description Department, 
 sd.ProdMonth, sd.[Workplace Name], ppcra.Comments, Status 
 from StatusDetails sd 
 left join PREPLANNING_CHANGEREQUEST_APPROVAL ppcra on 
 ppcra.ChangeRequestID = sd.ChangeRequestID 
 and sd.Department = ppcra.Department  
 inner join 
 '+@SyncroDB+'[dbo].[tblDepartments] Dep on 
 sd.Department = dep.[DepartmentID] 
 INNER JOIN 
 (SELECT * FROM '+@SyncroDB+'dbo.tblUsers WHERE  UserID <> '''') RU On
 PPCRA.User1 = RU.UserID 
 and status = 2 
 --and sd.prodmonth = ' + Convert(Varchar(7),@Prodmonth) + ' 
 and sd.prodmonth >= '+convert(Varchar(7),@Prodmonth)+'
 and sd.prodmonth <= '+convert(Varchar(7),@ToProdmonth)+'	
 group by 
 ppcra.ApprovedDeclinedDate, ppcra.ApprovedDeclinedByUser, RU.Name, RU.LastName, sd.ChangeRequestID, Dep.Description, 
 sd.ProdMonth, sd.[Workplace Name], ppcra.Comments, Status'
 exec (@SQL1)
 --select @SQL1
 

GO

Create Procedure [dbo].[sp_PrePlanning_data]
 @ChangeRequestID INT
AS
SELECT DISTINCT SC.Name_2 Section,SC.name_2,SC.Name,PPCR.DrillRig,PPCR.ProdMonth,PPCR.OldWorkplaceID,PPCR.StartDate,PPCR.WorkplaceID,PPCR.DayCrew,
PPCR.SectionID,PPCR.AfternoonCrew,PPCR.NightCrew,PPCR.RovingCrew,WP.DESCRIPTION WPDesc,
PPCR.StopDate,PPCR.MiningMethod,'' Description,'-1'TargetID,PPCR.Comments,PPCR.ReefSQM,PPCR.WasteSQM,PPCR.Meters,PPCR.MetersWaste,PPCR.CubicMeters,
PPCR.SectionID,PPCR.SectionID_2,PPCR.FL,PPCR .Activity, DeleteBookings  FROM PrePlanning_ChangeRequest PPCR
INNER JOIN SECTION_COMPLETE SC on
--PPCR.SectionID_2 = SC.SECTIONID_2 and
PPCR.ProdMonth = SC.PRODMONTH and 
PPCR.SectionID = SC.SECTIONID 
INNER JOIN WORKPLACE WP on 
PPCR.WorkplaceID = WP.WORKPLACEID 

WHERE PPCR.ChangeRequestID = @ChangeRequestID

GO


ALTER Procedure [dbo].[SP_Save_Stope_CyclePlan]
--declare
@Username VARCHAR(50), 
@ProdMonth VARCHAR(20), 
@WorkplaceID VARCHAR(50),
@SectionID VARCHAR(50), 
@Activity VARCHAR(20), 
@IsCubics Varchar(1)

as

Declare
@SB VARCHAR(50), 
@Sqm Numeric(18,10), 
@ManualSqm Numeric(18,10), 
@CMGT NUMERIC(18,2), 
@Tons NUMERIC(18,2), 
@StartShift NUMERIC(18),
@EndShift NUMERIC(18),
@STOPEWIDTH NUMERIC(18),
@Cubics NUMERIC(18),
@ManualCubics NUMERIC(18),
@ChannelWIDTH NUMERIC(18),
@Facelength NUMERIC(18),
@GoldGramsPerTon NUMERIC(18,3),
@OnReefSQM NUMERIC(18),
@OffreefPer Numeric(18,3),
@BeginDate DateTime,
@EndDate DateTime,
@Locked int,
@PLanCode Varchar(2),
@Workingday Varchar(1),
@RemainderSQM Numeric(10),
@RemainderCubics Numeric(10),
@TotalShifts int,
@ShiftNo int,
@SaveShiftNo int,
@TotalBlasts int,
@BlastNo int,
@SQMPerBlast Numeric(10),
@CubicsPerBlast Numeric(10),
@Remaining_Shifts Numeric(10),
@Remaining_Blasts Numeric(10),
@TheDate DateTime,
@Density Numeric(18,3),
@MOCycle Varchar(5),
@CycleInput Varchar(10)



--Select @Username = N'MINEWARE_IGGY',
--		@ProdMonth = N'201706',
--		@WorkplaceID = N'RE007667',
--		@SectionID = N'REAAHDA',
--		@Activity = N'0',
--		@IsCubics = N'N'


	--	Select * from planmonth where workplaceid = 54432 and prodmonth = 201611
		


select @SB = ReportToSectionid from SECTION where 
PRODMONTH = @ProdMonth
and SECTIONID = @SectionID

select @BeginDate = BeginDate, @EndDate = EndDate from SECCAL where 
PRODMONTH = @ProdMonth
and SECTIONID = @SB

--select @BeginDate, @EndDate from SECCAL where 
--PRODMONTH = @ProdMonth
--and SECTIONID = @SB

--select 
--@BeginDate = StartDate, @EndDate = StoppedDate
--from PLANMONTH
--  WHERE Prodmonth = @Prodmonth AND
--      Sectionid = @SectionID AND
--      Workplaceid = @WorkplaceID AND
--      Activitycode = @Activity and
--      IsCubics = @IsCubics 





select 
--*
@StartShift = COUNT(CALENDARDATE)
 from (select * from SECCAL) a inner join 
 (select * from CalType) b on
a.CalendarCode = b.CalendarCode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @BeginDate
and b.WORKINGDAY = 'Y'

select 
--*
@EndShift = COUNT(CALENDARDATE)
 from (select * from SECCAL) a inner join 
 (select * from CalType) b on
a.CalendarCode = b.CalendarCode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @EndDate
and b.WORKINGDAY = 'Y'


--select @SB, @BeginDate, @EndDate, @StartShift, @EndShift

--Drop table #Planning

CREATE TABLE #PLANNING(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[PlanCode] [char](2) NOT NULL,
	[IsCubics] [char](1) NOT NULL,
	[ShiftDay] [numeric](5, 0) NULL,
	[SQM] [numeric](5, 0) NULL,
	[ReefSQM] [int] NULL,
	[WasteSQM] [int] NULL,
	[Metresadvance] [numeric](9, 3) NULL,
	[ReefAdv] [numeric](10, 3) NULL,
	[WasteAdv] [numeric](10, 3) NULL,
	[Tons] [numeric](5, 0) NULL,
	[ReefTons] [int] NULL,
	[WasteTons] [int] NULL,
	[Grams] [numeric](9, 3) NULL,
	[FL] [numeric](9, 3) NULL,
	[ReefFL] [int] NULL,
	[WasteFL] [int] NULL,
	[SW] [numeric](5, 0) NULL,
	[CW] [numeric](5, 0) NULL,
	[CMGT] [numeric](5, 0) NULL,
	[GT] [numeric](8, 2) NULL,
	[CubicMetres] [numeric](13, 3) NULL,
	[Cubics] [numeric](10, 3) NULL,
	[ReefCubics] [numeric](10, 3) NULL,
	[WasteCubics] [numeric](10, 3) NULL,
	[CubicTons] [numeric](10, 3) NULL,
	[CubicGrams] [numeric](10, 3) NULL,
	[CubicDepth] [numeric](10, 3) NULL,
	[CubicGT] [numeric](10, 3) NULL,
	[MOCycle] [varchar](5) Null,
	[CycleInput] [varchar](10) Null
PRIMARY KEY CLUSTERED 
(
	[Prodmonth] ASC,
	[SectionID] ASC,
	[WorkplaceID] ASC,
	[Activity] ASC,
	[Calendardate] ASC,
	[PlanCode] ASC,
	[IsCubics] ASC
))

CREATE TABLE #MOCycle(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[MOCycle] [varchar](5) Null,
	[CycleInput] [varchar](10) Null)


insert into #MOCycle
select 
b.ProdMonth,
b.SectionID,
w.WorkplaceID,
@Activity Activity,
d.CalendarDate,
MOCycle = Case 
When d.Workingday = 'N' then 'OFF'
When d.Workingday = 'Y' And p.MOCycle is null then 'BL'
else p.MOCycle end,
CycleInput = Case 
When CycleInput is null then 'CAL' else CycleInput end
from 

 SECTION_COMPLETE b 
inner join seccal c on 
b.Prodmonth = c.prodmonth and
b.SectionID_1 = c.sectionID 
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CalendarDate and
c.EndDate >= d.CalendarDate 
left join planning p on
b.Prodmonth = p.Prodmonth AND
      b.Sectionid = p.SectionID AND
	  d.CalendarDate = p.CalendarDate 
	  and p.Workplaceid = @WorkplaceID AND
	  p.Activity = @Activity and
      p.IsCubics = @IsCubics and
	  p.PlanCode = 'MP',
	  (Select * from Workplace where WorkplaceID = @WorkplaceID) w
 WHERE b.Prodmonth = @Prodmonth AND
      b.Sectionid = @SectionID 
      
--Select * From #MOCycle

select 
--*
@TotalBlasts = COUNT(CALENDARDATE)
 from #MOCycle a
where a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
	  a.MOCycle = 'BL' and
	  a.CycleInput = 'CAL'

SELECT 
--*
@Sqm = SQM,
@Tons = Tons,
@STOPEWIDTH = SW,
@ChannelWIDTH = CW,
@Facelength = FL,
@GoldGramsPerTon = GT,
@CMGT = cmgt,
@OnReefSQM = ReefSQM,
@PLanCode = PlanCode,
@Density = b.density,
@Cubics = CubicMetres
FROM Planmonth a inner join Workplace b on
a.Workplaceid = b.workplaceid
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP' 

select 
--*
@ManualSqm = isnull(sum(SQM),0),
@ManualCubics = isnull(sum(Cubics),0)
 from Planning a
where a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
	  --a.MOCycle = 'BL' and
	  a.CycleInput = 'MAN'

--select @Sqm,
--@STOPEWIDTH,
--@ChannelWIDTH,
--@Facelength,
--@GoldGramsPerTon,
--@CMGT,
--@OnReefSQM,
--@PLanCode,
--@Density,
--@Cubics

    DECLARE Shaft_Cursor CURSOR FOR
	select CALENDARDATE, WORKINGDAY, TOTALSHIFTS
	 from SECCAL a inner join 
	 CALTYPE b on
	a.CALENDARCODE = b.CALENDARCODE and
	a.BeginDate <= b.CALENDARDATE and
	a.ENDDATE >= b.CALENDARDATE 
	where a.PRODMONTH = @ProdMonth and
	a.SECTIONID = @SB
	--and b.Workingday = 'Y'
	OPEN Shaft_Cursor;
	FETCH NEXT FROM Shaft_Cursor
	into @TheDate, @Workingday, @TotalShifts;

	Set @ShiftNo = 0
	Set @BlastNo = 0
	 Set @RemainderSQM = @Sqm-@ManualSqm
	 Set @RemainderCubics = @Cubics-@ManualCubics
	 Set @Remaining_Blasts = @TotalBlasts
	 WHILE @@FETCH_STATUS = 0
	   BEGIN
		 --set @SQMPerBlast = round(@Sqm/@TotalShifts,0)
		 --set @CubicsPerBlast = round(@Cubics/@TotalShifts,0)

		 Select @MOCycle = mocycle, @CycleInput = CycleInput from #MOCycle where Calendardate = @TheDate

		 if @Workingday = 'Y'
		 begin

		   set @Remaining_Shifts = @TotalShifts - @ShiftNo
		   
		   if @MOCycle = 'BL' and @CycleInput = 'CAL'
		   begin
		     set @Remaining_Blasts = @TotalBlasts - @BlastNo

		     set @SQMPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderSQM/@Remaining_Blasts,0)) end
		     set @CubicsPerBlast = case when @Remaining_Shifts = 0 then 0 else Convert(Int,round(@RemainderCubics/@Remaining_Blasts,0)) end

			 set @BlastNo = @BlastNo + 1

			 set @RemainderSQM = @RemainderSQM - @SQMPerBlast
		     set @RemainderCubics = @RemainderCubics - @CubicsPerBlast

		   end 
		   else
		   begin
		     set @SQMPerBlast = 0
		     set @CubicsPerBlast = 0
		   end		   

		   set @ShiftNo = @ShiftNo + 1
		   Set @SaveShiftNo = @ShiftNo
		   
		 end 
		 else
		 begin
		    set @SQMPerBlast = 0
		    set @CubicsPerBlast = 0
			Set @SaveShiftNo = 0
		 end

		 --Select @SQMPerBlast, @CubicsPerBlast, @SaveShiftNo, @Remaining_Shifts, @ShiftNo

		 if @CycleInput = 'CAL'
		 begin
			 insert into #PLANNING 
				(Prodmonth,
				SectionID,
				WorkplaceID,
				Activity,
				Calendardate,
				PlanCode,
				IsCubics,
				ShiftDay,
				SQM,
				ReefSQM,
				WasteSQM,
				Metresadvance,
				ReefAdv,
				WasteAdv,
				Tons,
				ReefTons,
				WasteTons,
				Grams,
				FL,
				ReefFL,
				WasteFL,
				SW,
				CW,
				CMGT,
				GT,
				CubicMetres,
				Cubics,
				ReefCubics,
				WasteCubics,
				CubicTons,
				CubicGrams,
				CubicDepth,
				CubicGT,
				[MOCycle],
				CycleInput)

				select
				@ProdMonth ,
				@SectionID SECTIONID,
				@WorkplaceID WORKPLACEID,
				@Activity ACTIVITY,
				@TheDate CALENDARDATE,
				'MP' PLanCode,
				@IsCubics,
				@SaveShiftNo SHIFTDAY,
				SQUAREMETRES = @SQMPerBlast,
				0 ReefSQM,
				0 WasteSQM,
				0 METRESADVANCE,
				0 ReefAdv,
				0 WasteAdv,
				TONS = @SQMPerBlast*@STOPEWIDTH/100*@Density,
				0 ReefTons,
				0 WasteTons,
				0 GRAMS, 
				@Facelength FL,
				0 ReefFL,
				0 WasteFL,
				@STOPEWIDTH SW,
				@ChannelWIDTH CW,
				@CMGT cmgt,
				@GoldGramsPerTon gt,
				CubicMetres = @CubicsPerBlast,
				Cubics = 0,
				ReefCubics = 0,
				WasteCubics = 0,
				CubicTons = @CubicsPerBlast*@Density,
				CubicGrams = 0,
				CubicDepth = 0,
				CubicGT = 0,
				MOCycle =  @MOCycle,
				CycleInput = @CycleInput
            end
			else
			begin
			  	insert into #PLANNING 
				(Prodmonth,
				SectionID,
				WorkplaceID,
				Activity,
				Calendardate,
				PlanCode,
				IsCubics,
				ShiftDay,
				SQM,
				ReefSQM,
				WasteSQM,
				Metresadvance,
				ReefAdv,
				WasteAdv,
				Tons,
				ReefTons,
				WasteTons,
				Grams,
				FL,
				ReefFL,
				WasteFL,
				SW,
				CW,
				CMGT,
				GT,
				CubicMetres,
				Cubics,
				ReefCubics,
				WasteCubics,
				CubicTons,
				CubicGrams,
				CubicDepth,
				CubicGT,
				[MOCycle],
				CycleInput)

				select
				@ProdMonth ,
				@SectionID SECTIONID,
				@WorkplaceID WORKPLACEID,
				@Activity ACTIVITY,
				@TheDate CALENDARDATE,
				'MP' PLanCode,
				@IsCubics,
				@SaveShiftNo SHIFTDAY,
				SQUAREMETRES = SQM,
				0 ReefSQM,
				0 WasteSQM,
				0 METRESADVANCE,
				0 ReefAdv,
				0 WasteAdv,
				TONS = SQM*@STOPEWIDTH/100*@Density,
				0 ReefTons,
				0 WasteTons,
				0 GRAMS, 
				@Facelength FL,
				0 ReefFL,
				0 WasteFL,
				@STOPEWIDTH SW,
				@ChannelWIDTH CW,
				@CMGT cmgt,
				@GoldGramsPerTon gt,
				CubicMetres = CubicMetres,
				Cubics = 0,
				ReefCubics = 0,
				WasteCubics = 0,
				CubicTons = CubicMetres*@Density,
				CubicGrams = 0,
				CubicDepth = 0,
				CubicGT = 0,
				MOCycle =  @MOCycle,
				CycleInput = @CycleInput
				from Planning where Prodmonth = @ProdMonth and sectionid = @SectionID and WorkplaceID = @WorkplaceID and Calendardate = @TheDate and Plancode = 'MP'
			end

		 FETCH NEXT FROM Shaft_Cursor
		   into @TheDate, @Workingday, @TotalShifts;
	   end

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;




select @OffreefPer = case when @Sqm = 0 then 0 else @OnReefSQM/@Sqm end

--select @OffreefPer

--select * from #PLANNING

--insert into #PLANNING 
--(Prodmonth,
--SectionID,
--WorkplaceID,
--Activity,
--Calendardate,
--PlanCode,
--IsCubics,
--ShiftDay,
--SQM,
--ReefSQM,
--WasteSQM,
--Metresadvance,
--ReefAdv,
--WasteAdv,
--Tons,
--ReefTons,
--WasteTons,
--Grams,
--FL,
--ReefFL,
--WasteFL,
--SW,
--CW,
--CMGT,
--GT,
--CubicMetres,
--Cubics,
--ReefCubics,
--WasteCubics,
--CubicTons,
--CubicGrams,
--CubicDepth,
--CubicGT,
--MOCycle)

--select 

--@ProdMonth ,
--@SectionID SECTIONID,
--@WorkplaceID WORKPLACEID,
--@Activity ACTIVITY,
--CALENDARDATE,
--@PLanCode,
--@IsCubics,
--0 SHIFTDAY,
--SQUAREMETRES = 0,
--0 ReefSQM,
--0 WasteSQM,
--0 METRESADVANCE,
--0 ReefAdv,
--0 WasteAdv,
--TONS = 0,
--0 ReefTons,
--0 WasteTons,
--0 GRAMS, 
--@Facelength FL,
--0 ReefFL,
--0 WasteFL,
--0 SW,
--0 CW,
--0 cmgt,
--0 gt,
--CubicMetres = 0,
--Cubics = 0,
--ReefCubics = 0,
--WasteCubics = 0,
--CubicTons = 0,
--CubicGrams = 0,
--CubicDepth = 0,
--CubicGT = 0,
--MOCycle = CASE WHEN workingday = 'N' THEN 'OFF' ELSE 'BL' END

-- from (select * from SECCAL) a inner join 
-- (select * from CalType) b on
--a.CalendarCode = b.CalendarCode and
--a.BeginDate <= b.CALENDARDATE and
--a.ENDDATE >= b.CALENDARDATE 
--where a.PRODMONTH = @ProdMonth and
--a.SECTIONID = @SB and 
--b.CALENDARDATE <= @EndDate
--and b.WORKINGDAY = 'N'

Update #PLANNING set 
--Select
GRAMS = Round(SQM*@OffreefPer,0)*cmgt/100*(vw_WP_density.density),
METRESADVANCE = case when FL = 0 then 0 else SQM/FL end,
ReefFL = Round(FL*@OffreefPer,0),
WasteFL = FL-Round(FL*@OffreefPer,0),
ReefTons = Round(TONS*@OffreefPer,0),
WasteTons = TONS - Round(TONS*@OffreefPer,0),
ReefSQM = Round(SQM*@OffreefPer,0),
WasteSQM = SQM-Round(SQM*@OffreefPer,0),
ReefAdv = case when FL = 0 then 0 else (SQM/FL)*@OffreefPer end,
WasteAdv = case when FL = 0 then 0 else (SQM/FL)-((SQM/FL)*@OffreefPer) end
from
 #PLANNING PLANNING inner join WORKPLACE vw_WP_density  on
 PLANNING.WORKPLACEID Collate SQL_Latin1_General_CP1_CI_AS = vw_wp_density.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      PLANNING.Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'

--select * from #PLANNING

Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate > @EndDate
 
Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate < @BeginDate

Update PLANMONTH set 
ReefFL = Round(FL*@OffreefPer,0),
WasteFL = FL- Round(FL*@OffreefPer,0)
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      Workplaceid = @WorkplaceID AND
      Activity = @Activity and
	  PlanCode = 'MP' and
      IsCubics = @IsCubics

insert into PLANNING 
(Prodmonth,
SectionID,
WorkplaceID,
Activity,
Calendardate,
PlanCode,
IsCubics,
ShiftDay,
SQM,
ReefSQM,
WasteSQM,
Metresadvance,
ReefAdv,
WasteAdv,
Tons,
ReefTons,
WasteTons,
Grams,
FL,
ReefFL,
WasteFL,
SW,
CW,
CMGT,
GT,
CubicMetres,
Cubics,
ReefCubics,
WasteCubics,
CubicTons,
CubicGrams,
CubicDepth,
CubicGT,
MOCycle,
CycleInput)
Select a.* from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS
Where b.Prodmonth is null


Update PLANNING set
ShiftDay = a.ShiftDay,
SQM = a.SQM,
ReefSQM = a.ReefSQM,
WasteSQM = a.WasteSQM,
Metresadvance = a.Metresadvance,
ReefAdv = a.ReefAdv,
WasteAdv = a.WasteAdv,
Tons = a.Tons,
ReefTons = a.ReefTons,
WasteTons = a.WasteTons,
Grams = a.Grams,
FL = a.FL,
ReefFL = a.ReefFL,
WasteFL = a.WasteFL,
SW = a.SW,
CW = a.CW,
CMGT = a.CMGT,
GT = a.GT,
CubicMetres = a.CubicMetres,
Cubics = a.Cubics,
ReefCubics = a.ReefCubics,
WasteCubics = a.WasteCubics,
CubicTons = a.CubicTons,
CubicGrams = a.CubicGrams,
CubicDepth = a.CubicDepth,
CubicGT = a.CubicGT
from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS


Drop Table #PLANNING
DROP Table #MOCycle


GO

ALTER Procedure [dbo].[SP_Save_Dev_CyclePlan]
--Declare
@Username VARCHAR(50), 
@ProdMonth VARCHAR(20), 
@WorkplaceID VARCHAR(50),
@SectionID VARCHAR(50), 
@Activity VARCHAR(20), 
@IsCubics Varchar(1)

AS

--Select
--@Username = 'MINEWARE_D',
--@ProdMonth = 201706,
--@WorkplaceID = 'RE009814',
--@SectionID = 'REHAKAA',
--@Activity = 1,
--@IsCubics = 'N'

--select * from planmonth where prodmonth = 201505 and activity = 1
--and plancode = 'MP'
Declare
@SB VARCHAR(50), 
@Adv Numeric(18,10), 
@ManualAdv Numeric(18,10), 
@CMGT NUMERIC(18,2), 
@Tons NUMERIC(18,2), 
@StartShift NUMERIC(18),
@EndShift NUMERIC(18),
@STOPEWIDTH NUMERIC(18),
@Facelength NUMERIC(18),
@GoldGramsPerTon NUMERIC(18,3),
@OnReefADV NUMERIC(18),  
@OffreefPer Numeric(18,3),
@BeginDate DateTime,
@EndDate DateTime,
@TheDate DateTime,
@Locked int,
@MVar Numeric(10,1),
@PerOnReef Numeric(18,3),
@Workingday Varchar(1),
@RemainderAdv Numeric(10,3),
@RemainderCubics Numeric(10),
@TotalShifts int,
@ShiftNo int,
@SaveShiftNo int,
@TotalBlasts int,
@BlastNo int,
@AdvPerBlast Numeric(10,1),
@CubicsPerBlast Numeric(10),
@Remaining_Shifts Numeric(10,1),
@Remaining_Blasts Numeric(10),
@CubicMetres NUMERIC(18),
@MOCycle Varchar(5),
@CycleInput Varchar(10),
@Density Numeric(10,3)


--select @Username = 'Mineware', 
--@ProdMonth = '201703', 
--@WorkplaceID = '35033',
--@SectionID  = '3555400', 
--@Activity  = '1', 
--@IsCubics  = 'N'


      
Create Table #Planning (
			[Prodmonth] [numeric](7, 0) NOT NULL,
			[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
			[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
			ACTIVITY  [numeric](7, 0),
			IsCubics [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
			SHIFTDAY  [numeric](7, 0),
			Calendardate DateTime,
			PlanCode [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS, 
			SQUAREMETRES [numeric](7, 0),
			METRESADVANCE [numeric](7, 2),
			CUBICMETRES [numeric](7, 0),
			TONS [numeric](7, 3),
			GRAMS [numeric](7, 0),
			FL [numeric](7, 0),
			SW  [numeric](7, 0),
			GT [numeric](7, 0),
			cmgt [numeric](7, 0),
			ReefFL[numeric](7, 0),
			WasteFL [numeric](7, 0),
			ReefTons [numeric](7,3),
			WasteTons [numeric](7,3),
			ReefSQM [numeric](7, 0),
			WasteSQM [numeric](7, 0),
			CubicTons [numeric](7, 0),
			CubicGrams [numeric](7, 0),
			CubicDepth [numeric](7, 0),
			CubicGT [numeric](7, 2), 
			OnReefAdv [numeric](7, 2),
			OffReefAdv [numeric](7, 2),
			MOCycle [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
			CycleInput [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS)


select @SB = ReportToSectionid from section where 
PRODMONTH = @ProdMonth
and SECTIONID = @SectionID


select @BeginDate = BeginDate, @EndDate = ENDDATE from SECCAL where 
PRODMONTH = @ProdMonth
and SECTIONID = @SB

select @Density = Density from WORKPLACE where 
Workplaceid = @Workplaceid

 
select 
--*
@StartShift = COUNT(CALENDARDATE)
 from SECCAL  a inner join 
 CALTYPE b on
a.CALENDARcode = b.CALENDARcode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @BeginDate
and b.WORKINGDAY = 'Y'

select 
--*     
@EndShift = COUNT(CALENDARDATE)
 from SECCAL a inner join 
CALTYPE b on
a.CALENDARcode = b.CALENDARcode and
a.BeginDate <= b.CALENDARDATE and
a.ENDDATE >= b.CALENDARDATE 
where a.PRODMONTH = @ProdMonth and
a.SECTIONID = @SB and 
b.CALENDARDATE <= @EndDate
and b.WORKINGDAY = 'Y'

CREATE TABLE #MOCycle(
	[Prodmonth] [numeric](7, 0) NOT NULL,
	[SectionID] [varchar](10) Collate SQL_Latin1_General_CP1_CI_AS,
	[WorkplaceID] [varchar](12) Collate SQL_Latin1_General_CP1_CI_AS,
	[Activity] [numeric](7, 0) NOT NULL,
	[Calendardate] [datetime] NOT NULL,
	[MOCycle] [varchar](5) Null,
	[CycleInput] [varchar](10) Null)


insert into #MOCycle
select 
a.ProdMonth,
a.SectionID,
a.WorkplaceID,
a.Activity,
d.CalendarDate,
MOCycle = Case 
When d.Workingday = 'N' then 'OFF'
When d.Workingday = 'Y' And p.MOCycle is null then 'BL'
else p.MOCycle end,
CycleInput = Case 
When CycleInput is NUll then 'CAL' else CycleInput end
from 

Planmonth a inner join SECTION_COMPLETE b on
a.Prodmonth = b.prodmonth and
a.SectionID = b.sectionID 
inner join seccal c on 
b.Prodmonth = c.prodmonth and
b.SectionID_1 = c.sectionID 
inner join CALTYPE d on
c.CalendarCode = d.CalendarCode and
c.BeginDate <= d.CalendarDate and
c.EndDate >= d.CalendarDate 
left join planning p on
a.Prodmonth = p.Prodmonth AND
      a.Sectionid = p.SectionID AND
      a.Workplaceid = p.WorkplaceID AND
      a.Activity = p.Activity and
      a.IsCubics = p.IsCubics and
	  a.PlanCode = p.PlanCode and
	  d.CalendarDate = p.CalendarDate 
 WHERE a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      a.IsCubics = @IsCubics and
	  a.PlanCode = 'MP'

--select * from  #MOCycle


select 
--*
@TotalBlasts = COUNT(CALENDARDATE)
 from #MOCycle a
where a.Prodmonth = @Prodmonth AND
      a.Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
	  a.MOCycle = 'BL'
	  and a.CycleInput = 'CAL'


--select @SB, @BeginDate, @EndDate, @StartShift, @EndShift

SELECT 
--*
@Adv = MetresAdvance,
@Tons = Tons,
@STOPEWIDTH = SW,
@Facelength = FL,
@GoldGramsPerTon = GT,
@CMGT = cmgt,
@OnReefADV = ReefADV,
@CubicMetres = isnull(Cubicmetres,0)--,
--@OffReefSQM = OffReefSQM
FROM Planmonth
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  plancode = 'MP'

SELECT 
--*
@ManualAdv = isnull(Sum(MetresAdvance),0)
FROM Planning
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  plancode = 'MP' and
	  CycleInput = 'MAN'
	  
  DECLARE Shaft_Cursor CURSOR FOR
	select CALENDARDATE, WORKINGDAY, TOTALSHIFTS
	 from SECCAL a inner join 
	 CALTYPE b on
	a.CALENDARCODE = b.CALENDARCODE and
	a.BeginDate <= b.CALENDARDATE and
	a.ENDDATE >= b.CALENDARDATE 
	where a.PRODMONTH = @ProdMonth and
	a.SECTIONID = @SB
	--and Workingday = 'Y'
	OPEN Shaft_Cursor;
	FETCH NEXT FROM Shaft_Cursor
	into @TheDate, @Workingday, @TotalShifts;

	Set @ShiftNo = 0
	Set @BlastNo = 0
	 Set @RemainderAdv = @Adv-@ManualAdv
	 Set @RemainderCubics = @CubicMetres
	 Set @Remaining_Blasts = @TotalBlasts
	 WHILE @@FETCH_STATUS = 0
	   BEGIN
		 --set @AdvPerBlast = round(@Adv/@TotalShifts,0)
		 --set @CubicsPerBlast = round(@CubicMetres/@TotalShifts,0)

		 Select @MOCycle = mocycle, @CycleInput = CycleInput from #MOCycle where Calendardate = @TheDate

		 if @Workingday = 'Y'
		 begin

		   set @Remaining_Shifts = @TotalShifts - @ShiftNo

		   if @MOCycle = 'BL' and @CycleInput = 'CAL'
		   begin
		       set @Remaining_Blasts = @TotalBlasts - @BlastNo

			   set @AdvPerBlast = case when @Remaining_Blasts = 0 then 0 else @RemainderAdv/@Remaining_Blasts end
			   set @CubicsPerBlast = case when @Remaining_Blasts = 0 then 0 else @RemainderCubics/@Remaining_Blasts end 
	   
	           set @RemainderAdv = @RemainderAdv - @AdvPerBlast
			   set @RemainderCubics = @RemainderCubics - @CubicsPerBlast

			   set @BlastNo = @BlastNo + 1
			   

		   end 
		   else
		   begin
		     set @AdvPerBlast = 0
		     set @CubicsPerBlast = 0
		   end	

		   set @ShiftNo = @ShiftNo + 1
		   Set @SaveShiftNo = @ShiftNo

		   
		 end 
		 else
		 begin
		    set @AdvPerBlast = 0
		    set @CubicsPerBlast = 0
			Set @SaveShiftNo = 0
		 end

		 if @CycleInput = 'CAL'
		 begin
			 insert into #Planning (
				[Prodmonth] ,
				[SectionID],
				[WorkplaceID],
				ACTIVITY,
				IsCubics,
				SHIFTDAY,
				Calendardate,
				PlanCode,
				SQUAREMETRES,
				METRESADVANCE,
				CUBICMETRES,
				TONS,
				GRAMS,
				FL,
				SW,
				GT,
				cmgt,
				ReefFL,
				WasteFL,
				ReefTons,
				WasteTons,
				ReefSQM,
				WasteSQM,
				CubicTons,
				CubicGrams,
				CubicDepth,
				CubicGT, 
				OnReefAdv,
				OffReefAdv,
				MOCycle,
				CycleInput)

				select
				@ProdMonth PRODMONTH ,
				@SectionID SECTIONID,
				@WorkplaceID WORKPLACEID,
				@Activity ACTIVITYCODE,
				@IsCubics IsCubics ,
				@SaveShiftNo SHIFTDAY,
				@TheDate TEMPDATE,
				'MP',
				SQUAREMETRES = 0,
				METRESADVANCE = @AdvPerBlast,
				CUBICMETRES = @CubicsPerBlast,
				TONS = 0,
				0 GRAMS,
				@Facelength,
				@STOPEWIDTH StopeWidth,
				@GoldGramsPerTon GoldGramsPerTon,
				@CMGT cmgt,
				0 OnReefFL,
				0 OffReefFL,
				0 OnReefTons,
				0 OffReefTons,
				0 OnReefSQM,
				0 OffReefSQM,
				@CubicsPerBlast*@Density CubicTons,
				0 CubicGrams, 
				0 CubicDepth,
				0 CubicGT,
				0 OnReefAdv,
				0 OffReefAdv,
				@MOCycle, @CycleInput
			END
			else
			begin
			 insert into #Planning (
				[Prodmonth] ,
				[SectionID],
				[WorkplaceID],
				ACTIVITY,
				IsCubics,
				SHIFTDAY,
				Calendardate,
				PlanCode,
				SQUAREMETRES,
				METRESADVANCE,
				CUBICMETRES,
				TONS,
				GRAMS,
				FL,
				SW,
				GT,
				cmgt,
				ReefFL,
				WasteFL,
				ReefTons,
				WasteTons,
				ReefSQM,
				WasteSQM,
				CubicTons,
				CubicGrams,
				CubicDepth,
				CubicGT, 
				OnReefAdv,
				OffReefAdv,
				MOCycle,
				CycleInput)

				select
				@ProdMonth PRODMONTH ,
				@SectionID SECTIONID,
				@WorkplaceID WORKPLACEID,
				@Activity ACTIVITYCODE,
				@IsCubics IsCubics ,
				@SaveShiftNo SHIFTDAY,
				@TheDate TEMPDATE,
				'MP',
				SQUAREMETRES = 0,
				METRESADVANCE = METRESADVANCE,
				CUBICMETRES = CUBICMETRES,
				TONS = 0,
				0 GRAMS,
				@Facelength,
				@STOPEWIDTH StopeWidth,
				@GoldGramsPerTon GoldGramsPerTon,
				@CMGT cmgt,
				0 OnReefFL,
				0 OffReefFL,
				0 OnReefTons,
				0 OffReefTons,
				0 OnReefSQM,
				0 OffReefSQM,
				CUBICMETRES*@Density CubicTons,
				0 CubicGrams, 
				0 CubicDepth,
				0 CubicGT,
				0 OnReefAdv,
				0 OffReefAdv,
				@MOCycle, @CycleInput
				from Planning where Prodmonth = @ProdMonth and sectionid = @SectionID and WorkplaceID = @WorkplaceID and Calendardate = @TheDate and Plancode = 'MP'
			END
     

		 FETCH NEXT FROM Shaft_Cursor
		   into @TheDate, @Workingday, @TotalShifts;
	   end

CLOSE Shaft_Cursor;
DEALLOCATE Shaft_Cursor;

--select * from #Planning

select @PerOnReef = case when @Adv = 0 then 0 else @OnReefADV/@Adv end

Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate > @EndDate
 
Delete from planning  
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      PLANNING.Workplaceid = @WorkplaceID AND
      Activity = @Activity and
      IsCubics = @IsCubics and
	  PlanCode = 'MP'
	  and Calendardate < @BeginDate

Update #Planning set 
OnReefAdv = METRESADVANCE * @PerOnReef,
OffReefAdv = METRESADVANCE - Round(METRESADVANCE * @PerOnReef,1)
 from #Planning a inner join WORKPLACE wp_Density on
 a.WORKPLACEID Collate SQL_Latin1_General_CP1_CI_AS = wp_density.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      IsCubics = @IsCubics

Update #Planning set 
ReefTons = (wp_Density.EndHeight * wp_Density.[EndWidth]) * wp_Density.Density * OnReefAdv,
WasteTons = (wp_Density.[EndHeight] * wp_Density.[EndWidth]) * wp_Density.Density * OffReefAdv,
Tons = (wp_Density.[EndHeight] * wp_Density.[EndWidth]) * wp_Density.Density * METRESADVANCE
 from #Planning a inner join WORKPLACE wp_Density on
 a.WORKPLACEID Collate SQL_Latin1_General_CP1_CI_AS = wp_density.workplaceid Collate SQL_Latin1_General_CP1_CI_AS
  inner join [dbo].[WORKPLACE] wp on
 a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = wp.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS
WHERE Prodmonth = @Prodmonth AND
      Sectionid = @SectionID AND
      a.Workplaceid = @WorkplaceID AND
      a.Activity = @Activity and
      IsCubics = @IsCubics

--Select * from #Planning
 insert into PLANNING 
([Prodmonth] ,
			[SectionID],
			[WorkplaceID],
			ACTIVITY,
			IsCubics,
			SHIFTDAY,
			Calendardate,
			PlanCode,
			SQM,
			METRESADVANCE,
			CUBICMETRES,
			TONS,
			GRAMS,
			FL,
			SW,
			GT,
			cmgt,
			ReefFL,
			WasteFL,
			ReefTons,
			WasteTons,
			ReefSQM,
			WasteSQM,
			CubicTons,
			CubicGrams,
			CubicDepth,
			CubicGT, 
			ReefAdv,
			WasteAdv,
			MOCycle,
			CycleInput)
Select a.* from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS
Where b.Prodmonth is null


Update PLANNING set
SHIFTDAY = a.SHIFTDAY,
			SQM = a.SQUAREMETRES,
			METRESADVANCE = a.METRESADVANCE,
			CUBICMETRES = a.CUBICMETRES,
			TONS = a.TONS,
			GRAMS = a.GRAMS,
			FL = a.FL,
			SW = a.SW,
			GT = a.GT,
			cmgt = a.cmgt,
			ReefFL = a.ReefFL,
			WasteFL = a.WasteFL,
			ReefTons = a.ReefTons,
			WasteTons = a.WasteTons,
			ReefSQM = a.ReefSQM,
			WasteSQM = a.WasteSQM,
			CubicTons = a.CubicTons,
			CubicGrams = a.CubicGrams,
			CubicDepth = a.CubicDepth,
			CubicGT = a.CubicGT, 
			ReefAdv = a.OnReefAdv,
			WasteAdv = a.OffReefAdv,
			MOCycle = a.MOCycle

from 
#PLANNING a 
left join 
PLANNING b on
      a.Prodmonth = b.Prodmonth AND
      a.Sectionid Collate SQL_Latin1_General_CP1_CI_AS = b.SectionID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Workplaceid Collate SQL_Latin1_General_CP1_CI_AS = b.WorkplaceID Collate SQL_Latin1_General_CP1_CI_AS AND
      a.Activity = b.Activity and
	  a.Calendardate = b.Calendardate and
	  a.PlanCode Collate SQL_Latin1_General_CP1_CI_AS = b.PlanCode Collate SQL_Latin1_General_CP1_CI_AS and
      a.IsCubics Collate SQL_Latin1_General_CP1_CI_AS = b.IsCubics Collate SQL_Latin1_General_CP1_CI_AS


 drop table #Planning
 drop table #MOCycle

  

GO

ALTER Procedure [dbo].[sp_Get_Cycle]
--Declare
@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)

--DECLARE
--@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)
--SET @theProdmonth = 201706
--SET @theWorkplaceID = 'RE007667'
--SET @theSectionID = 'REAAHDA'



AS
CREATE TABLE #cycleData
( 
    Prodmonth VARCHAR(40)
    ,WorkplaceID VARCHAR(40)
	,RowType varchar(20)
	,Name varchar(20)
	,SQM numeric(10,3)
	,Metresadvance numeric(10,3)
	,Cubes numeric(10,3)
	,Day1 VARCHAR(40)
	,Day2 VARCHAR(40)
	,Day3 VARCHAR(40)
	,Day4 VARCHAR(40)
	,Day5 VARCHAR(40)
	,Day6 VARCHAR(40)
	,Day7 VARCHAR(40)
	,Day8 VARCHAR(40)
	,Day9 VARCHAR(40)
	,Day10 VARCHAR(40)
	,Day11 VARCHAR(40)
	,Day12 VARCHAR(40)
	,Day13 VARCHAR(40)
	,Day14 VARCHAR(40)
	,Day15 VARCHAR(40)
	,Day16 VARCHAR(40)
	,Day17 VARCHAR(40)
	,Day18 VARCHAR(40)
	,Day19 VARCHAR(40)
	,Day20 VARCHAR(40)
	,Day21 VARCHAR(40)
	,Day22 VARCHAR(40)
	,Day23 VARCHAR(40)
    ,Day24 VARCHAR(40)
	,Day25 VARCHAR(40)
	,Day26 VARCHAR(40)
	,Day27 VARCHAR(40)
	,Day28 VARCHAR(40)
	,Day29 VARCHAR(40)
	,Day30 VARCHAR(40)
	,Day31 VARCHAR(40)
	,Day32 VARCHAR(40)
	,Day33 VARCHAR(40)
	,Day34 VARCHAR(40)
	,Day35 VARCHAR(40)
	,Day36 VARCHAR(40)
	,Day37 VARCHAR(40)
	,Day38 VARCHAR(40)
	,Day39 VARCHAR(40)
	,Day40 VARCHAR(40)
	,Day41 VARCHAR(40)
	,Day42 VARCHAR(40)
	,Day43 VARCHAR(40)
	,Day44 VARCHAR(40)
	,Day45 VARCHAR(40)
	,Day46 VARCHAR(40)
	,Day47 VARCHAR(40)
	,Day48 VARCHAR(40)
	,Day49 VARCHAR(40)
	,Day50 VARCHAR(40)
)

DECLARE
@Prodmonth VARCHAR(40),
@WorkplaceID VARCHAR(40), 
@CycleValue VARCHAR(40), 
@CycleValueCude VARCHAR(40),
@Calendardate VARCHAR(40), 
@MOCycle VARCHAR(40),
@MOCycleCube VARCHAR(40), 
@WorkingDay VARCHAR(40), 
@theSQLCalDate varchar(max),
@name VARCHAR(40),
@CycleInput VARCHAR(40),
@theInsert varchar(max), 
@dayCount int, 
@theSQLCycleVal varchar(max),
@theSQLInputVal varchar(max),
@theSQLCycleValCube varchar(max),
@theSQLMOCycle varchar(max),
@theSQLMOCycleCube varchar(max),
@theSQLWorkingDay varchar(max),
@SQM numeric(10,3),
@Metresadvance numeric(10,3),
@Cubes numeric(10,3)



SET @theInsert = 'INSERT INTO #cycleData (Prodmonth,WorkplaceID,RowType,Name,SQM,Metresadvance,Cubes'


SET @dayCount = 1;
DECLARE db_Items CURSOR FOR 
SELECT PM.Prodmonth,PM.WorkplaceID
      ,CASE WHEN  PM.Activity in (0) and PM.SQM > 0 and PLAND.SQM IS NOT NULL  THEN PLAND.SQM 
	        WHEN  PM.Activity in (1) and PM.Metresadvance  >0 and PLAND.Metresadvance  IS NOT NULL  THEN PLAND.Metresadvance 
	        ELSE 0 END CycleValue
      ,CASE WHEN  PM.Activity in (0,1) and PM.[CubicMetres] > 0  and PLAND.CubicMetres IS NOT NULL THEN PLAND.CubicMetres ELSE 0 END CycleValueCube
	  ,CT.Calendardate
      ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
	        WHEN PLAND.MOCycle is null then 'BL' else PLAND.MOCycle
	  end MOCycle
	  ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
	        WHEN PLAND.MOCycleCube is null then 'BL' else PLAND.MOCycleCube
	  end MOCycleCude
      ,CASE WHEN ct.Workingday = 'Y' THEN 'WD' ELSE 'NWD' END WorkingDay
	  ,CASE WHEN PM.SQM IS NULL then 0 ELSE PM.SQM END SQM
	  ,CASE WHEN PM.Metresadvance IS NULL then 0 ELSE PM.Metresadvance END Metresadvance
	  ,CASE WHEN PM.[CubicMetres] IS NULL THEN 0 ELSE PM.[CubicMetres] END [CubicMetres],
	  CASE WHEN PLAND.CycleInput IS NULL THEN 'CAL' ELSE CycleInput END CycleInput
  FROM PLANMONTH PM 
    Inner join 
  [dbo].[SECTION_COMPLETE] SCOM ON
  PM.SectionID = SCOM.sectionID and
  PM.Prodmonth = SCOM.PRODMONTH 
  INNER JOIN
  [dbo].[SECCAL] SC on
  SCOM.SectionID_1 = SC.sectionID and
  SCOM.Prodmonth = SC.PRODMONTH 
  Inner join 
   CalType CT on
 SC.CalendarCode = CT.CalendarCode and
 SC.BeginDate <= CT.CALENDARDATE and
 SC.ENDDATE >= CT.CALENDARDATE 
  LEFT JOIN [dbo].[PLANNING] PLAND on
  PM.Prodmonth = PLAND.PRODMONTH and
  PM.[SectionID] = PLAND.[SectionID] and
  PM.Workplaceid = PLAND.WorkplaceID and
  PM.Activity = PLAND.Activity and
  CT.Calendardate = PLAND.CalendarDate
  WHERE PM.Prodmonth = @theProdmonth
        and PM.SECTIONID = @theSectionID
		and PM.WorkplaceID = @theWorkplaceID
		and PM.PlanCode = 'MP'
		
		ORDER BY CT.Calendardate

OPEN db_Items   
FETCH NEXT FROM db_Items INTO @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput


SET @theSQLCalDate = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sDate'',''Date'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
SET @theSQLInputVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sInput'',''Input'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))

IF @SQM > 0
BEGIN
SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''SQM'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
END
IF @Metresadvance > 0
BEGIN
SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''Meters'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
END
SET @theSQLCycleValCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValueCube'',''Cubes'','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
SET @theSQLMOCycle = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCode'','''',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
SET @theSQLMOCycleCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCodeCube'','''','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
WHILE @@FETCH_STATUS = 0   
BEGIN
SET @theInsert = @theInsert + ',Day' + Cast(@dayCount as varchar(10))
SET @theSQLCalDate = @theSQLCalDate  + ',''' + Cast([dbo].[FormatDateTime](@Calendardate,'YYYY-MM-DD') as varchar(MAX)) + ''''
SET @theSQLInputVal = @theSQLInputVal  + ',''' +@CycleInput + ''''
SET @theSQLCycleVal = @theSQLCycleVal  + ',''' + Cast(@CycleValue as varchar(MAX)) + ''''
SET @theSQLCycleValCube = @theSQLCycleValCube  + ',''' + Cast(@CycleValueCude as varchar(MAX)) + ''''
SET @theSQLMOCycle = @theSQLMOCycle  + ',''' + Cast(@MOCycle as varchar(MAX)) + ''''
SET @theSQLMOCycleCube = @theSQLMOCycleCube  + ',''' + Cast(@MOCycleCube as varchar(MAX)) + ''''
--SET @theSQL = @theSQL + Cast(@Prodmonth as varchar(MAX))  + ',' + Cast(@WorkplaceID as varchar(MAX)) + ',' + Cast(@CycleValue as varchar(MAX))+ ',' + Cast(@Calendardate as varchar(MAX)) + ',' + Cast(@MOCycle as varchar(MAX)) + ',' + Cast(@WorkingDay as varchar(MAX))
SET @dayCount = @dayCount + 1
FETCH NEXT FROM db_Items INTO  @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput 
END

SET @theSQLCalDate = @theSQLCalDate + ')'
SET @theSQLInputVal = @theSQLInputVal + ')'
SET @theSQLCycleVal = @theSQLCycleVal + ')'
SET @theSQLMOCycle = @theSQLMOCycle + ')'
SET @theSQLMOCycleCube = @theSQLMOCycleCube + ')'
SET @theSQLCycleValCube = @theSQLCycleValCube + ')'
SET @theInsert = @theInsert + ') VALUES '
--select (@theInsert + @theSQLCalDate)
--select (@theInsert + @theSQLMOCycle)
--select (@theInsert + @theSQLCycleVal)
IF @theSQLCalDate is not null
begin
exec (@theInsert + @theSQLCalDate)
exec (@theInsert + @theSQLInputVal)
IF @SQM + @Metresadvance > 0
BEGIN
exec (@theInsert + @theSQLMOCycle)
exec (@theInsert + @theSQLCycleVal)
END
IF @Cubes > 0
BEGIN
exec (@theInsert + @theSQLMOCycleCube)
exec (@theInsert + @theSQLCycleValCube)
END
end

SELECT * FROM #cycleData

DROP TABLE #cycleData

CLOSE db_Items   
DEALLOCATE db_Items


GO


CREATE TABLE [dbo].[STATUSDETAILS](
	[Approved] [bit] NULL,
	[Department] [varchar](80) NULL,
	[Comments] [varchar](150) NULL,
	[Declined] [bit] NULL,
	[ApprovedDeclinedDate] [datetime] NULL,
	[ChangeRequestID] [int] NULL,
	[ApproveRequestID] [int] NULL,
	[Name] [varchar](50) NULL,
	[ChangeType] [varchar](50) NULL,
	[Workplace Name] [varchar](50) NOT NULL,
	[WorkplaceID] [varchar](50) NULL,
	[ProdMonth] [numeric](7, 0) NULL,
	[Status] [int] NULL,
	[RequestDate] [datetime] NULL
) ON [PRIMARY]

GO

CREATE Procedure [dbo].[sp_StatusDetails]
as

DELETE FROM StatusDetails
INSERT INTO StatusDetails (Approved,RequestDate, Department , Comments , Declined , ApprovedDeclinedDate ,
Status,ChangeRequestID , ApproveRequestID , Name ,ChangeType, [Workplace Name] ,WorkplaceID, ProdMonth )

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Approved=1 and PPCRA.Declined=0 then 0 end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Approved=1 and PPCRA.Declined=0 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL 

union

--INSERT INTO StatusDetails (Approved, Department , Comments , Declined , ApprovedDeclinedDate ,
--ChangeRequestID , ApproveRequestID , Name ,ChangeType, [Workplace Name] ,WorkplaceID, ProdMonth ,Status)

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Declined=1 and PPCRA.Approved=0 then 1  end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Declined=1 and PPCRA.Approved=0 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL 

union
--INSERT INTO StatusDetails (Approved, Department , Comments , Declined , ApprovedDeclinedDate ,
--ChangeRequestID , ApproveRequestID , Name ,ChangeType, [Workplace Name] ,WorkplaceID, ProdMonth ,Status)

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Approved=0 and PPCRA.Declined=0  then 2  end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Approved=0 and PPCRA.Declined=0 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL --order by PPCRA.ChangeRequestID

union

SELECT DISTINCT 
PPCRA .Approved ,PPCRA.RequestDate,PPCRA.Department,PPCR.Comments ,PPCRA .Declined ,PPCRA .ApprovedDeclinedDate , 
 case when  PPCRA .Approved=1 and PPCRA.Declined=1  then 1  end status
,PPCR.ChangeRequestID, PPCRA.ApproveRequestID , U.USERID RequestedBy,PPRT.Description ChangeType,WP.DESCRIPTION WPName, WP.WORKPLACEID,PPCR.ProdMonth 
FROM PREPLANNING_CHANGEREQUEST_APPROVAL PPCRA
INNER JOIN PrePlanning_ChangeRequest PPCR ON
PPCR.ChangeRequestID = PPCRA.ChangeRequestID
INNER JOIN [CODE_PREPLANNINGTYPES] PPRT ON
PPCR.ChangeID = PPRT.ChangeID
INNER JOIN USERS U on 
U.USERID = PPCR.RequestBy
INNER JOIN WORKPLACE WP on
WP.WORKPLACEID = PPCR.WorkplaceID
INNER JOIN (SELECT Name_2,SECTIONID_2,PRODMONTH FROM SECTION_COMPLETE) SECTION ON
Section.SECTIONID_2 = PPCR.SectionID_2 and
Section.PRODMONTH = PPCR.ProdMonth 
INNER JOIN (SELECT Name,SECTIONID,PRODMONTH FROM SECTION_COMPLETE) MINER ON
MINER.SECTIONID = PPCR.SectionID and
MINER.PRODMONTH = PPCR.ProdMonth left join [dbo].[StatusDetails] SD on
PPCRA.ApproveRequestID=SD.ApproveRequestID AND
PPCR.ChangeRequestID=SD.ChangeRequestID where PPCRA.Approved=1 and PPCRA.Declined=1 AND SD.ApproveRequestID IS NULL
AND SD.ChangeRequestID IS NULL --order by PPCRA.ChangeRequestID
--select * from StatusDetails

GO

ALTER Procedure [dbo].[sp_PrePlanning_CrewChanges] 
--DECLARE 
	@RequestID INT,
   @UserID VARCHAR(10)

AS

DECLARE
@Prodmonth NUMERIC(7,0),
@Cubes NUMERIC(7,3),
@SQM NUMERIC(7,3),
@SectionID VARCHAR(20), 
@Sectionid_2 VARCHAR(20), 
@WorkplaceID VARCHAR(20), 
@Activity NUMERIC(7,0), 
@SQL Varchar(1000),
@HasAutoPlan INT,
@NewSectionID_2  VARCHAR(20),
@StartDate DateTime,
@EndDate DateTime

--SET @RequestID = 16
--SET @UserID = 'MINEWARE_IGGY'

--SET @SQM = (SELECT CR.OnReefSQM + CR.OffReefSQM FROM PrePlanning_ChangeRequest CR 
--WHERE CR.ChangeRequestID = @RequestID)

--SET @Cubes = (SELECT CR.CubicMeters FROM PrePlanning_ChangeRequest CR 
--WHERE CR.ChangeRequestID = @RequestID)
Begin Transaction
SET @SectionID =  (SELECT CR.SectionID FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Activity =  (SELECT CR.Activity FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @WorkplaceID = (SELECT CR.Workplaceid FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

SET @Prodmonth = (SELECT CR.Prodmonth FROM PrePlanning_ChangeRequest CR 
WHERE CR.ChangeRequestID = @RequestID)

Select @NewSectionID_2 = SectionID_2, @StartDate = BeginDate, @EndDate = Enddate 
--Select SectionID_2
from Section_complete a inner join seccal b on
a.prodmonth = b.Prodmonth and
a.sectionID_1 = b.SectionID 
where a.Prodmonth = @Prodmonth and
a.Sectionid = @SectionID

--Select @NewSectionID_2, @StartDate, @EndDate, @Prodmonth, @SectionID, @Activity


SET @HasAutoPlan = (SELECT COUNT(Workplaceid) FROM PLANMONTH WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND AutoUnPlan = 'Y')

--SELECT @HasAutoPlan, @WorkplaceID

IF @HasAutoPlan > 0
BEGIN
	DELETE FROM P
	FROM dbo.PLANNING P 
	INNER JOIN dbo.PLANMONTH PM ON
	PM.Activity = P.Activity AND
	PM.Prodmonth = P.Prodmonth AND
	PM.Workplaceid = P.WorkplaceID  AND
	pm.Sectionid = p.SectionID
	WHERE p.Workplaceid = @WorkplaceID AND p.Prodmonth = @Prodmonth AND pm.AutoUnPlan = 'Y'

	DELETE 
	FROM PLANMONTH WHERE Workplaceid = @WorkplaceID AND Prodmonth = @Prodmonth AND AutoUnPlan = 'Y'
END



UPDATE PM SET
		OrgunitDay =CR.DayCrew ,
		OrgunitAfterNoon =CR.AfternoonCrew ,
		OrgunitNight =CR.NightCrew ,
		RomingCrew =CR.RovingCrew,
		SECTIONID =CR.SectionID,
		--SectionID_2 = @NewSectionID_2,
		StartDate = @Startdate,
		Enddate = @enddate    					  

--SELECT PP.SectionID ,CR.DayCrew,CR.AfternoonCrew,CR.NightCrew,CR.RovingCrew,CR.SectionID 
--SELECT *  
 FROM PlanMonth PM  
INNER JOIN PrePlanning_ChangeRequest CR  ON
--CR.SECTIONID = pm.SECTIONID and
CR.WorkplaceID = pm.Workplaceid and
CR.ProdMonth = pm.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and plancode = 'MP'

UPDATE planmonth SET

		SECTIONID =CR.SectionID,
		--SectionID_2 = @NewSectionID_2,
		StartDate = @Startdate,
		Enddate = @enddate    					  
--SELECT *  
FROM planmonth LP 
INNER JOIN PrePlanning_ChangeRequest CR  ON
--CR.SectionID_2 = lp.Sectionid_2 and
CR.WorkplaceID = lp.Workplaceid and
CR.ProdMonth = lp.Prodmonth
WHERE CR.ChangeRequestID = @RequestID and plancode = 'LP' 

UPDATE planning SET 
  Sectionid =CR.SectionID 
 --SELECT *  					  
FROM planning LPD 
INNER JOIN planmonth PP ON
LPD.PRODMONTH = pp.Prodmonth and
LPD.WORKPLACEID = PP.Workplaceid and
lpd.plancode = pp.plancode
INNER JOIN PrePlanning_ChangeRequest CR  ON
--pp.Sectionid = CR.SectionID and
CR.WorkplaceID = PP.Workplaceid and
CR.ProdMonth = PP.Prodmonth
WHERE CR.ChangeRequestID = @RequestID  


--UPDATE BP SET SectionID = CR.SectionID
----SELECT *  
-- FROM dbo.BOOK_PROBLEM BP
--INNER JOIN planmonth PP ON
--BP.PRODMONTH = pp.Prodmonth and
--BP.WORKPLACEID = PP.Workplaceid 
--INNER JOIN PrePlanning_ChangeRequest CR  ON
--CR.SectionID_2 = pp.Sectionid_2 and
--CR.WorkplaceID = PP.Workplaceid and
--CR.ProdMonth = PP.Prodmonth
--WHERE PP.PlanCode = 'MP' AND PP.Prodmonth = @Prodmonth AND PP.Workplaceid = @WorkplaceID  AND PP.AutoUnPlan IS NULL AND CR.ChangeRequestID = @RequestID  


if @NewSectionID_2 <> @SectionID_2
BEGIN
	UPDATE PLANPROT_DATA SET SectionID = @NewSectionID_2
	WHERE Prodmonth = @Prodmonth AND 
	Workplaceid = @WorkplaceID 

	UPDATE PLANPROT_DATALOCKED SET SectionID = @NewSectionID_2
	WHERE Prodmonth = @Prodmonth AND 
	Workplaceid = @WorkplaceID 
END

COMMIT TRANSACTION

Declare @CycleWorkplaceid VarChar(20),
@CycleSectionid VarChar(20),
@CycleSQL VarChar(8000),
@CycleActivitycode Int,
@CycleIscubics Varchar(1),
@CycleTheProdmonth Numeric(7)

DECLARE _Cursor CURSOR FOR

  select a.Prodmonth, a.Sectionid, a.Workplaceid, a.Activity, a.Iscubics from planmonth a inner join Section_Complete b on
  a.prodmonth = b.prodmonth and
  a.sectionid = b.sectionid and
  PlanCode = 'MP'
  where a.Prodmonth = @Prodmonth 
  and Workplaceid = @WorkplaceID



 OPEN _Cursor;
 FETCH NEXT FROM _Cursor
 into @CycleTheProdmonth, @CycleSectionid, @CycleWorkplaceid, @CycleActivitycode, @CycleIscubics;

 WHILE @@FETCH_STATUS = 0
   BEGIN

     If @CycleActivitycode = 0 

       Set @CycleSQL = 'exec [SP_Save_Stope_CyclePlan] '
     else 
	   Set @CycleSQL = 'exec [SP_Save_Dev_CyclePlan] '

     Set @CycleSQL = @CycleSQL+' @Username = ''MINEWARE'', 
     @ProdMonth = '+cast(@CycleTheProdmonth as Varchar(7))+', 
     @WorkplaceID = '''+@CycleWorkplaceid+''',
     @SectionID = '''+@CycleSectionid+''', 
     @Activity = '+cast(@CycleActivitycode as Varchar(1))+', 
     @IsCubics = '''+@CycleIscubics+''''

   exec (@CycleSQL)

 FETCH NEXT FROM _Cursor
 into @CycleTheProdmonth,@CycleSectionid, @CycleWorkplaceid, @CycleActivitycode, @CycleIscubics;

END

CLOSE _Cursor;
DEALLOCATE _Cursor;


GO

ALTER Procedure [dbo].[sp_PlanProd_WPList]
--Declare
@Prodmonth Numeric(7),
@SectionID_2 VarChar(20),
@Activity Numeric(7)
as

--SET @Prodmonth = 201703
--SET @SectionID_2 = 'REG'
--SET @Activity = 0


			SELECT pp.Prodmonth,pp.Workplaceid,w.Description WorkplaceDesc,SC.NAME_2 MO, pp.SQM, isnull(PPP.SQM,0) PrevSQM, pp.ReefSQM, pp.WasteSQM, pp.SW,pp.IdealSW,pp.cw,pp.CMGT,gt = case when pp.ReefTons = 0 then 0 else pp.kg*1000/pp.reeftons end
			,pp.OrgUnitDay,pp.OrgUnitAfternoon,pp.OrgUnitNight, pp.DHeight, pp.DWidth, SCA.BeginDate, SCA.EndDate
            , pp.Metresadvance, ppp.Metresadvance PrevAdv, pp.ReefAdv, pp.WasteAdv, pp.FL, pp.Kg, Name_2, Name_1,Name
			,meas.SQM MeasSQM, meas.ADV MeasAdv
			FROM PLANMONTH  PP 
			INNER JOIN dbo.SECTION_COMPLETE SC ON
			pp.Prodmonth = SC.PRODMONTH AND
			pp.Sectionid = SC.SECTIONID 
			INNER JOIN dbo.SECCAL SCA ON
			SC.Prodmonth = SCA.PRODMONTH AND
			SC.Sectionid_1 = SCA.SECTIONID 
			inner join Workplace W on
			pp.Workplaceid = w.Workplaceid
			LEFT JOIN PLANMONTH  PPP ON
			PPP.Prodmonth = dbo.GetPrevProdMonth(pp.Prodmonth) and
			ppp.Workplaceid = pp.Workplaceid
			left join (
			Select WorkplaceID, Sum(SqmTotal) SQM, SUM(TotalMetres) ADV from SURVEY
			where ProdMonth = dbo.GetPrevProdMonth(@Prodmonth)
			group by WorkplaceID 
			) Meas on
			pp.Workplaceid = meas.WorkplaceID
			WHERE 
			    PP.Prodmonth = @Prodmonth AND
				sc.Sectionid_2 = @SectionID_2 AND
				pp.Activity = @Activity  and
				pp.PlanCode = 'MP'       --done
GO

Create Procedure [dbo].[sp_Get_Cycle_Protocol]
--Declare
@Prodmonth numeric(7,0),@SectionID_2 varchar(10), @Activity Int

--DECLARE
--@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)
--SET @theProdmonth = 201706
--SET @theSectionID = 'REH'
--Set @TheActivity = 1

--Select * from planmonth where prodmonth = 201601 and workplaceid = '21924'


AS
CREATE TABLE #cycleData
( 
    Prodmonth VARCHAR(40)
    ,WorkplaceID VARCHAR(40)
	,RowType varchar(20)
	,Name varchar(20)
	,SQM numeric(10,3)
	,Metresadvance numeric(10,3)
	,Cubes numeric(10,3)
	,Day1 VARCHAR(40)
	,Day2 VARCHAR(40)
	,Day3 VARCHAR(40)
	,Day4 VARCHAR(40)
	,Day5 VARCHAR(40)
	,Day6 VARCHAR(40)
	,Day7 VARCHAR(40)
	,Day8 VARCHAR(40)
	,Day9 VARCHAR(40)
	,Day10 VARCHAR(40)
	,Day11 VARCHAR(40)
	,Day12 VARCHAR(40)
	,Day13 VARCHAR(40)
	,Day14 VARCHAR(40)
	,Day15 VARCHAR(40)
	,Day16 VARCHAR(40)
	,Day17 VARCHAR(40)
	,Day18 VARCHAR(40)
	,Day19 VARCHAR(40)
	,Day20 VARCHAR(40)
	,Day21 VARCHAR(40)
	,Day22 VARCHAR(40)
	,Day23 VARCHAR(40)
    ,Day24 VARCHAR(40)
	,Day25 VARCHAR(40)
	,Day26 VARCHAR(40)
	,Day27 VARCHAR(40)
	,Day28 VARCHAR(40)
	,Day29 VARCHAR(40)
	,Day30 VARCHAR(40)
	,Day31 VARCHAR(40)
	,Day32 VARCHAR(40)
	,Day33 VARCHAR(40)
	,Day34 VARCHAR(40)
	,Day35 VARCHAR(40)
	,Day36 VARCHAR(40)
	,Day37 VARCHAR(40)
	,Day38 VARCHAR(40)
	,Day39 VARCHAR(40)
	,Day40 VARCHAR(40)
	,Day41 VARCHAR(40)
	,Day42 VARCHAR(40)
	,Day43 VARCHAR(40)
	,Day44 VARCHAR(40)
	,Day45 VARCHAR(40)
	,Day46 VARCHAR(40)
	,Day47 VARCHAR(40)
	,Day48 VARCHAR(40)
	,Day49 VARCHAR(40)
	,Day50 VARCHAR(40)
)

DECLARE
@WorkplaceID VARCHAR(40), 
@CycleValue VARCHAR(40), 
@CycleValueCude VARCHAR(40),
@SectionID VARCHAR(40),
@Calendardate VARCHAR(40), 
@MOCycle VARCHAR(40),
@MOCycleCube VARCHAR(40), 
@WorkingDay VARCHAR(40), 
@theSQLCalDate varchar(max),
@name VARCHAR(40),
@CycleInput VARCHAR(40),
@theInsert varchar(max), 
@dayCount int, 
@theSQLCycleVal varchar(max),
@theSQLInputVal varchar(max),
@theSQLCycleValCube varchar(max),
@theSQLMOCycle varchar(max),
@theSQLMOCycleCube varchar(max),
@theSQLWorkingDay varchar(max),
@SQM numeric(10,3),
@Metresadvance numeric(10,3),
@Cubes numeric(10,3)


DECLARE _Cursor1 CURSOR FOR

  select a.Sectionid, a.Workplaceid from planmonth a inner join Section_Complete b on
  a.prodmonth = b.prodmonth and
  a.sectionid = b.sectionid and
  PlanCode = 'MP'
  where a.Prodmonth = @Prodmonth
  and b.SectionID_2 = @SectionID_2
  and activity in (@Activity)

 OPEN _Cursor1;
 FETCH NEXT FROM _Cursor1
 into  @Sectionid, @Workplaceid;

 WHILE @@FETCH_STATUS = 0
   BEGIN


		SET @theInsert = 'INSERT INTO #cycleData (Prodmonth,WorkplaceID,RowType,Name,SQM,Metresadvance,Cubes'


		SET @dayCount = 1;
		DECLARE db_Items CURSOR FOR 
		SELECT PM.Prodmonth,PM.WorkplaceID
			  ,CASE WHEN  PM.Activity in (0) and PM.SQM > 0 and PLAND.SQM IS NOT NULL  THEN PLAND.SQM 
					WHEN  PM.Activity in (1) and PM.Metresadvance  >0 and PLAND.Metresadvance  IS NOT NULL  THEN PLAND.Metresadvance 
					ELSE 0 END CycleValue
			  ,CASE WHEN  PM.Activity in (0,1) and PM.[CubicMetres] > 0  and PLAND.CubicMetres IS NOT NULL THEN PLAND.CubicMetres ELSE 0 END CycleValueCube
			  ,CT.Calendardate
			  ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
					WHEN PLAND.MOCycle is null then 'BL' else PLAND.MOCycle
			  end MOCycle
			  ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
					WHEN PLAND.MOCycleCube is null then 'BL' else PLAND.MOCycleCube
			  end MOCycleCude
			  ,CASE WHEN ct.Workingday = 'Y' THEN 'WD' ELSE 'NWD' END WorkingDay
			  ,CASE WHEN PM.SQM IS NULL then 0 ELSE PM.SQM END SQM
			  ,CASE WHEN PM.Metresadvance IS NULL then 0 ELSE PM.Metresadvance END Metresadvance
			  ,CASE WHEN PM.[CubicMetres] IS NULL THEN 0 ELSE PM.[CubicMetres] END [CubicMetres],
			  CASE WHEN PLAND.CycleInput IS NULL THEN 'CAL' ELSE CycleInput END CycleInput
		  FROM PLANMONTH PM 
			Inner join 
		  [dbo].[SECTION_COMPLETE] SCOM ON
		  PM.SectionID = SCOM.sectionID and
		  PM.Prodmonth = SCOM.PRODMONTH 
		  INNER JOIN
		  [dbo].[SECCAL] SC on
		  SCOM.SectionID_1 = SC.sectionID and
		  SCOM.Prodmonth = SC.PRODMONTH 
		  Inner join 
		   CalType CT on
		 SC.CalendarCode = CT.CalendarCode and
		 SC.BeginDate <= CT.CALENDARDATE and
		 SC.ENDDATE >= CT.CALENDARDATE 
		  LEFT JOIN [dbo].[PLANNING] PLAND on
		  PM.Prodmonth = PLAND.PRODMONTH and
		  PM.[SectionID] = PLAND.[SectionID] and
		  PM.Workplaceid = PLAND.WorkplaceID and
		  PM.Activity = PLAND.Activity and
		  CT.Calendardate = PLAND.CalendarDate
		  WHERE PM.Prodmonth = @Prodmonth
				and PM.SECTIONID = @SectionID
				and PM.WorkplaceID = @WorkplaceID
				and PM.PlanCode = 'MP'
				and ct.Workingday = 'Y'
		
				ORDER BY CT.Calendardate

		OPEN db_Items   
		FETCH NEXT FROM db_Items INTO @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput


		SET @theSQLCalDate = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sDate'',''Date'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLInputVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sInput'',''Input'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))

		IF @SQM > 0
		BEGIN
		SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''SQM'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		END
		IF @Metresadvance > 0
		BEGIN
		SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''Meters'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		END
		SET @theSQLCycleValCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValueCube'',''Cubes'','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLMOCycle = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCode'','''',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLMOCycleCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCodeCube'','''','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		WHILE @@FETCH_STATUS = 0   
		BEGIN
		SET @theInsert = @theInsert + ',Day' + Cast(@dayCount as varchar(10))
		SET @theSQLCalDate = @theSQLCalDate  + ',''' + Cast([dbo].[FormatDateTime](@Calendardate,'YYYY-MM-DD') as varchar(MAX)) + ''''
		SET @theSQLInputVal = @theSQLInputVal  + ',''' +@CycleInput + ''''
		SET @theSQLCycleVal = @theSQLCycleVal  + ',''' + Cast(@CycleValue as varchar(MAX)) + ''''
		SET @theSQLCycleValCube = @theSQLCycleValCube  + ',''' + Cast(@CycleValueCude as varchar(MAX)) + ''''
		SET @theSQLMOCycle = @theSQLMOCycle  + ',''' + Cast(@MOCycle as varchar(MAX)) + ''''
		SET @theSQLMOCycleCube = @theSQLMOCycleCube  + ',''' + Cast(@MOCycleCube as varchar(MAX)) + ''''
		--SET @theSQL = @theSQL + Cast(@Prodmonth as varchar(MAX))  + ',' + Cast(@WorkplaceID as varchar(MAX)) + ',' + Cast(@CycleValue as varchar(MAX))+ ',' + Cast(@Calendardate as varchar(MAX)) + ',' + Cast(@MOCycle as varchar(MAX)) + ',' + Cast(@WorkingDay as varchar(MAX))
		SET @dayCount = @dayCount + 1
		FETCH NEXT FROM db_Items INTO  @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput 
		END

		CLOSE db_Items;
        DEALLOCATE db_Items;

		--SET @theSQLCalDate = @theSQLCalDate + ')'
		--SET @theSQLInputVal = @theSQLInputVal + ')'
		SET @theSQLCycleVal = @theSQLCycleVal + ')'
		SET @theSQLMOCycle = @theSQLMOCycle + ')'
		SET @theSQLMOCycleCube = @theSQLMOCycleCube + ')'
		SET @theSQLCycleValCube = @theSQLCycleValCube + ')'
		SET @theInsert = @theInsert + ') VALUES '
		--select (@theInsert + @theSQLCalDate)
		--select (@theInsert + @theSQLMOCycle)
		--select (@theInsert + @theSQLCycleVal)
		IF @theSQLCalDate is not null
		begin
		--exec (@theInsert + @theSQLCalDate)
		--exec (@theInsert + @theSQLInputVal)
		IF @SQM + @Metresadvance > 0
		BEGIN
		exec (@theInsert + @theSQLMOCycle)
		exec (@theInsert + @theSQLCycleVal)
		END
		IF @Cubes > 0
		BEGIN
		exec (@theInsert + @theSQLMOCycleCube)
		exec (@theInsert + @theSQLCycleValCube)
		END
		end

		 FETCH NEXT FROM _Cursor1
       into  @Sectionid, @Workplaceid;

END

CLOSE _Cursor1;
DEALLOCATE _Cursor1;

SELECT 
    Prodmonth
    ,WorkplaceID
	,RowType
	,Name
	,SQM
	,Metresadvance
	,Cubes
	,Isnull(Day1,0) Day1
	,Isnull(Day2,0) Day2
	,Isnull(Day3,0) Day3
	,Isnull(Day4,0) Day4
	,Isnull(Day5,0) Day5
	,Isnull(Day6,0) Day6
	,Isnull(Day7,0) Day7
	,Isnull(Day8,0) Day8
	,Isnull(Day9,0) Day9
	,Isnull(Day10,0) Day10
	,Isnull(Day11,0) Day11
	,Isnull(Day12,0) Day12
	,Isnull(Day13,0) Day13
	,Isnull(Day14,0) Day14
	,Isnull(Day15,0) Day15
	,Isnull(Day16,0) Day16
	,Isnull(Day17,0) Day17
	,Isnull(Day18,0) Day18
	,Isnull(Day19,0) Day19
	,Isnull(Day20,0) Day20
	,Isnull(Day21,0) Day21
	,Isnull(Day22,0) Day22 
	,Isnull(Day23,0) Day23
    ,Isnull(Day24,0) Day24
	,Isnull(Day25,0) Day25
	,Isnull(Day26,0) Day26
	,Isnull(Day27,0) Day27
	,Isnull(Day28,0) Day28
	,Isnull(Day29,0) Day29
	,Isnull(Day30,0) Day30
	,Isnull(Day31,0) Day31
	,Isnull(Day32,0) Day32
	,Isnull(Day33,0) Day33
	,Isnull(Day34,0) Day34
	,Isnull(Day35,0) Day35
	,Isnull(Day36,0) Day36
	,Isnull(Day37,0) Day37
	,Isnull(Day38,0) Day38
	,Isnull(Day39,0) Day39
	,Isnull(Day40,0) Day40
	,Isnull(Day41,0) Day41
	,Isnull(Day42,0) Day42
	,Isnull(Day43,0) Day43
	,Isnull(Day44,0) Day44
	,Isnull(Day45,0) Day45
	,Isnull(Day46,0) Day46
	,Isnull(Day47,0) Day47
	,Isnull(Day48,0) Day48
	,Isnull(Day49,0) Day49
	,Isnull(Day50,0) Day50
 FROM #cycleData

DROP TABLE #cycleData


GO

Create Procedure [dbo].[sp_Get_Cycle_Protocol_Dev]
--Declare
@Prodmonth numeric(7,0),@SectionID_2 varchar(10), @Activity Int

--DECLARE
--@theProdmonth numeric(7,0),@theWorkplaceID varchar(12),@theSectionID varchar(10)
--SET @theProdmonth = 201706
--SET @theSectionID = 'REH'
--Set @TheActivity = 1

--Select * from planmonth where prodmonth = 201601 and workplaceid = '21924'


AS
CREATE TABLE #cycleData
( 
    Prodmonth VARCHAR(40)
    ,WorkplaceID VARCHAR(40)
	,RowType varchar(20)
	,Name varchar(20)
	,SQM numeric(10,3)
	,Metresadvance numeric(10,3)
	,Cubes numeric(10,3)
	,Day1 VARCHAR(40)
	,Day2 VARCHAR(40)
	,Day3 VARCHAR(40)
	,Day4 VARCHAR(40)
	,Day5 VARCHAR(40)
	,Day6 VARCHAR(40)
	,Day7 VARCHAR(40)
	,Day8 VARCHAR(40)
	,Day9 VARCHAR(40)
	,Day10 VARCHAR(40)
	,Day11 VARCHAR(40)
	,Day12 VARCHAR(40)
	,Day13 VARCHAR(40)
	,Day14 VARCHAR(40)
	,Day15 VARCHAR(40)
	,Day16 VARCHAR(40)
	,Day17 VARCHAR(40)
	,Day18 VARCHAR(40)
	,Day19 VARCHAR(40)
	,Day20 VARCHAR(40)
	,Day21 VARCHAR(40)
	,Day22 VARCHAR(40)
	,Day23 VARCHAR(40)
    ,Day24 VARCHAR(40)
	,Day25 VARCHAR(40)
	,Day26 VARCHAR(40)
	,Day27 VARCHAR(40)
	,Day28 VARCHAR(40)
	,Day29 VARCHAR(40)
	,Day30 VARCHAR(40)
	,Day31 VARCHAR(40)
	,Day32 VARCHAR(40)
	,Day33 VARCHAR(40)
	,Day34 VARCHAR(40)
	,Day35 VARCHAR(40)
	,Day36 VARCHAR(40)
	,Day37 VARCHAR(40)
	,Day38 VARCHAR(40)
	,Day39 VARCHAR(40)
	,Day40 VARCHAR(40)
	,Day41 VARCHAR(40)
	,Day42 VARCHAR(40)
	,Day43 VARCHAR(40)
	,Day44 VARCHAR(40)
	,Day45 VARCHAR(40)
	,Day46 VARCHAR(40)
	,Day47 VARCHAR(40)
	,Day48 VARCHAR(40)
	,Day49 VARCHAR(40)
	,Day50 VARCHAR(40)
)

DECLARE
@WorkplaceID VARCHAR(40), 
@CycleValue VARCHAR(40), 
@CycleValueCude VARCHAR(40),
@SectionID VARCHAR(40),
@Calendardate VARCHAR(40), 
@MOCycle VARCHAR(40),
@MOCycleCube VARCHAR(40), 
@WorkingDay VARCHAR(40), 
@theSQLCalDate varchar(max),
@name VARCHAR(40),
@CycleInput VARCHAR(40),
@theInsert varchar(max), 
@dayCount int, 
@theSQLCycleVal varchar(max),
@theSQLInputVal varchar(max),
@theSQLCycleValCube varchar(max),
@theSQLMOCycle varchar(max),
@theSQLMOCycleCube varchar(max),
@theSQLWorkingDay varchar(max),
@SQM numeric(10,3),
@Metresadvance numeric(10,3),
@Cubes numeric(10,3)


DECLARE _Cursor1 CURSOR FOR

  select a.Sectionid, a.Workplaceid from planmonth a inner join Section_Complete b on
  a.prodmonth = b.prodmonth and
  a.sectionid = b.sectionid and
  PlanCode = 'MP'
  where a.Prodmonth = @Prodmonth
  and b.SectionID_2 = @SectionID_2
  and activity in (1)

 OPEN _Cursor1;
 FETCH NEXT FROM _Cursor1
 into  @Sectionid, @Workplaceid;

 WHILE @@FETCH_STATUS = 0
   BEGIN


		SET @theInsert = 'INSERT INTO #cycleData (Prodmonth,WorkplaceID,RowType,Name,SQM,Metresadvance,Cubes'


		SET @dayCount = 1;
		DECLARE db_Items CURSOR FOR 
		SELECT PM.Prodmonth,PM.WorkplaceID
			  ,CASE WHEN  PM.Activity in (0) and PM.SQM > 0 and PLAND.SQM IS NOT NULL  THEN PLAND.SQM 
					WHEN  PM.Activity in (1) and PM.Metresadvance  >0 and PLAND.Metresadvance  IS NOT NULL  THEN PLAND.Metresadvance 
					ELSE 0 END CycleValue
			  ,CASE WHEN  PM.Activity in (0,1) and PM.[CubicMetres] > 0  and PLAND.CubicMetres IS NOT NULL THEN PLAND.CubicMetres ELSE 0 END CycleValueCube
			  ,CT.Calendardate
			  ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
					WHEN PLAND.MOCycle is null then 'BL' else PLAND.MOCycle
			  end MOCycle
			  ,CASE WHEN ct.Workingday = 'N' then 'OFF' 
					WHEN PLAND.MOCycleCube is null then 'BL' else PLAND.MOCycleCube
			  end MOCycleCude
			  ,CASE WHEN ct.Workingday = 'Y' THEN 'WD' ELSE 'NWD' END WorkingDay
			  ,CASE WHEN PM.SQM IS NULL then 0 ELSE PM.SQM END SQM
			  ,CASE WHEN PM.Metresadvance IS NULL then 0 ELSE PM.Metresadvance END Metresadvance
			  ,CASE WHEN PM.[CubicMetres] IS NULL THEN 0 ELSE PM.[CubicMetres] END [CubicMetres],
			  CASE WHEN PLAND.CycleInput IS NULL THEN 'CAL' ELSE CycleInput END CycleInput
		  FROM PLANMONTH PM 
			Inner join 
		  [dbo].[SECTION_COMPLETE] SCOM ON
		  PM.SectionID = SCOM.sectionID and
		  PM.Prodmonth = SCOM.PRODMONTH 
		  INNER JOIN
		  [dbo].[SECCAL] SC on
		  SCOM.SectionID_1 = SC.sectionID and
		  SCOM.Prodmonth = SC.PRODMONTH 
		  Inner join 
		   CalType CT on
		 SC.CalendarCode = CT.CalendarCode and
		 SC.BeginDate <= CT.CALENDARDATE and
		 SC.ENDDATE >= CT.CALENDARDATE 
		  LEFT JOIN [dbo].[PLANNING] PLAND on
		  PM.Prodmonth = PLAND.PRODMONTH and
		  PM.[SectionID] = PLAND.[SectionID] and
		  PM.Workplaceid = PLAND.WorkplaceID and
		  PM.Activity = PLAND.Activity and
		  CT.Calendardate = PLAND.CalendarDate
		  WHERE PM.Prodmonth = @Prodmonth
				and PM.SECTIONID = @SectionID
				and PM.WorkplaceID = @WorkplaceID
				and PM.PlanCode = 'MP'
				and ct.Workingday = 'Y'
		
				ORDER BY CT.Calendardate

		OPEN db_Items   
		FETCH NEXT FROM db_Items INTO @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput


		SET @theSQLCalDate = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sDate'',''Date'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLInputVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sInput'',''Input'',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))

		IF @SQM > 0
		BEGIN
		SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''SQM'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		END
		IF @Metresadvance > 0
		BEGIN
		SET @theSQLCycleVal = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValue'',''Meters'','  + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		END
		SET @theSQLCycleValCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sValueCube'',''Cubes'','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLMOCycle = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCode'','''',' + CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		SET @theSQLMOCycleCube = '(''' + Cast(@Prodmonth as varchar(MAX))  + ''',''' + Cast(@WorkplaceID as varchar(MAX)) + ''',''sCycleCodeCube'','''','+ CAST(@SQM as varchar(20)) + ',' + CAST(@Metresadvance as varchar(20)) + ',' + CAST(@Cubes as varchar(20))
		WHILE @@FETCH_STATUS = 0   
		BEGIN
		SET @theInsert = @theInsert + ',Day' + Cast(@dayCount as varchar(10))
		SET @theSQLCalDate = @theSQLCalDate  + ',''' + Cast([dbo].[FormatDateTime](@Calendardate,'YYYY-MM-DD') as varchar(MAX)) + ''''
		SET @theSQLInputVal = @theSQLInputVal  + ',''' +@CycleInput + ''''
		SET @theSQLCycleVal = @theSQLCycleVal  + ',''' + Cast(@CycleValue as varchar(MAX)) + ''''
		SET @theSQLCycleValCube = @theSQLCycleValCube  + ',''' + Cast(@CycleValueCude as varchar(MAX)) + ''''
		SET @theSQLMOCycle = @theSQLMOCycle  + ',''' + Cast(@MOCycle as varchar(MAX)) + ''''
		SET @theSQLMOCycleCube = @theSQLMOCycleCube  + ',''' + Cast(@MOCycleCube as varchar(MAX)) + ''''
		--SET @theSQL = @theSQL + Cast(@Prodmonth as varchar(MAX))  + ',' + Cast(@WorkplaceID as varchar(MAX)) + ',' + Cast(@CycleValue as varchar(MAX))+ ',' + Cast(@Calendardate as varchar(MAX)) + ',' + Cast(@MOCycle as varchar(MAX)) + ',' + Cast(@WorkingDay as varchar(MAX))
		SET @dayCount = @dayCount + 1
		FETCH NEXT FROM db_Items INTO  @Prodmonth,@WorkplaceID,@CycleValue,@CycleValueCude,@Calendardate,@MOCycle,@MOCycleCube,@WorkingDay,@SQM,@Metresadvance,@Cubes,@CycleInput 
		END

		CLOSE db_Items;
        DEALLOCATE db_Items;

		--SET @theSQLCalDate = @theSQLCalDate + ')'
		--SET @theSQLInputVal = @theSQLInputVal + ')'
		SET @theSQLCycleVal = @theSQLCycleVal + ')'
		SET @theSQLMOCycle = @theSQLMOCycle + ')'
		SET @theSQLMOCycleCube = @theSQLMOCycleCube + ')'
		SET @theSQLCycleValCube = @theSQLCycleValCube + ')'
		SET @theInsert = @theInsert + ') VALUES '
		--select (@theInsert + @theSQLCalDate)
		--select (@theInsert + @theSQLMOCycle)
		--select (@theInsert + @theSQLCycleVal)
		IF @theSQLCalDate is not null
		begin
		--exec (@theInsert + @theSQLCalDate)
		--exec (@theInsert + @theSQLInputVal)
		IF @SQM + @Metresadvance > 0
		BEGIN
		exec (@theInsert + @theSQLMOCycle)
		exec (@theInsert + @theSQLCycleVal)
		END
		IF @Cubes > 0
		BEGIN
		exec (@theInsert + @theSQLMOCycleCube)
		exec (@theInsert + @theSQLCycleValCube)
		END
		end

		 FETCH NEXT FROM _Cursor1
       into  @Sectionid, @Workplaceid;

END

CLOSE _Cursor1;
DEALLOCATE _Cursor1;

SELECT 
    Prodmonth
    ,WorkplaceID
	,RowType
	,Name
	,SQM
	,Metresadvance
	,Cubes
	,Isnull(Day1,0) Day1
	,Isnull(Day2,0) Day2
	,Isnull(Day3,0) Day3
	,Isnull(Day4,0) Day4
	,Isnull(Day5,0) Day5
	,Isnull(Day6,0) Day6
	,Isnull(Day7,0) Day7
	,Isnull(Day8,0) Day8
	,Isnull(Day9,0) Day9
	,Isnull(Day10,0) Day10
	,Isnull(Day11,0) Day11
	,Isnull(Day12,0) Day12
	,Isnull(Day13,0) Day13
	,Isnull(Day14,0) Day14
	,Isnull(Day15,0) Day15
	,Isnull(Day16,0) Day16
	,Isnull(Day17,0) Day17
	,Isnull(Day18,0) Day18
	,Isnull(Day19,0) Day19
	,Isnull(Day20,0) Day20
	,Isnull(Day21,0) Day21
	,Isnull(Day22,0) Day22 
	,Isnull(Day23,0) Day23
    ,Isnull(Day24,0) Day24
	,Isnull(Day25,0) Day25
	,Isnull(Day26,0) Day26
	,Isnull(Day27,0) Day27
	,Isnull(Day28,0) Day28
	,Isnull(Day29,0) Day29
	,Isnull(Day30,0) Day30
	,Isnull(Day31,0) Day31
	,Isnull(Day32,0) Day32
	,Isnull(Day33,0) Day33
	,Isnull(Day34,0) Day34
	,Isnull(Day35,0) Day35
	,Isnull(Day36,0) Day36
	,Isnull(Day37,0) Day37
	,Isnull(Day38,0) Day38
	,Isnull(Day39,0) Day39
	,Isnull(Day40,0) Day40
	,Isnull(Day41,0) Day41
	,Isnull(Day42,0) Day42
	,Isnull(Day43,0) Day43
	,Isnull(Day44,0) Day44
	,Isnull(Day45,0) Day45
	,Isnull(Day46,0) Day46
	,Isnull(Day47,0) Day47
	,Isnull(Day48,0) Day48
	,Isnull(Day49,0) Day49
	,Isnull(Day50,0) Day50
 FROM #cycleData

DROP TABLE #cycleData


GO


insert into users_section values ('MINEWARE', 'GM', 'P')
insert into users_section values ('MINEWARE', 'GM', 'R')