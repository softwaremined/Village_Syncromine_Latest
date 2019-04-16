USE [PAS_DNK_Syncromine]
go
alter Procedure sp_Save_Stope_Planning
(
@PlanningData VARCHAR(MAX),
@CycleData VARCHAR(MAX)
)

as 

--declare @PlanningData VARCHAR(MAX)
--declare @CycleData VARCHAR(MAX)
--declare @TotalsDays int


--set @PlanningData =

--'<?xml version="1.0" standalone="yes"?>
--<DocumentElement>
--  <PlanningData>
--    <MonthlyReefSQM>230</MonthlyReefSQM>
--    <MonthlyWatseSQM>20</MonthlyWatseSQM>
--    <MonthlyTotalSQM>250</MonthlyTotalSQM>
--    <Activity>0</Activity>
--    <SW>0.000</SW>
--    <CW>0.000</CW>
--    <isApproved>0</isApproved>
--    <CMGT>0.000</CMGT>
--    <CMKGT>0</CMKGT>
--    <Kg>0</Kg>
--    <UraniumBrokenKg>0</UraniumBrokenKg>
--    <CubicMetres>25</CubicMetres>
--    <CubicsReef>20</CubicsReef>
--    <CubicsWaste>5</CubicsWaste>
--    <FaceAdvance>0</FaceAdvance>
--    <FaceBrokenKG>0</FaceBrokenKG>
--    <FaceCMGT>0.000</FaceCMGT>
--    <FL>30</FL>
--    <FaceTons>0.68</FaceTons>
--    <FaceValue>0</FaceValue>
--    <GoldBroken>0</GoldBroken>
--    <UraniumBroken>0</UraniumBroken>
--    <CubicGrams>0</CubicGrams>
--    <DynamicCubicGT>0</DynamicCubicGT>
--    <IdealSW>120</IdealSW>
--    <IsCubics>N</IsCubics>
--    <callValue>264</callValue>
--    <Reefadv>0.000</Reefadv>
--    <Wasteadv>0.000</Wasteadv>
--    <OrgUnitAfternoon />
--    <OrgUnitDay>REAAEANURC</OrgUnitDay>
--    <OrgUnitNight />
--    <Prodmonth>201905</Prodmonth>
--    <RomingCrew />
--    <SectionID>REIBLHA</SectionID>
--    <Sectionid_2>REI</Sectionid_2>
--    <Sectionid_1>2.2 - C Schroeder</Sectionid_1>
--    <SQM>0.000</SQM>
--    <TargetID>1</TargetID>
--    <TrammedLevel />
--    <TrammedTons>0.76</TrammedTons>
--    <TrammedValue>0</TrammedValue>
--    <RockType>ON</RockType>
--    <WasteSQM>21</WasteSQM>
--    <ReefSQM>243</ReefSQM>
--    <WorkplaceDesc>192 N3 W 3</WorkplaceDesc>
--    <Workplaceid>RE007900</Workplaceid>
--    <Locked>false</Locked>
--    <StartDate>2019-04-16T00:00:00+02:00</StartDate>
--    <EndDate>2019-05-17T00:00:00+02:00</EndDate>
--    <IsStopped>N</IsStopped>
--    <hasChanged>true</hasChanged>
--    <hasRevised>false</hasRevised>
--  </PlanningData>
--  <PlanningData>
--    <MonthlyReefSQM>0</MonthlyReefSQM>
--    <MonthlyWatseSQM>0</MonthlyWatseSQM>
--    <MonthlyTotalSQM>0</MonthlyTotalSQM>
--    <Activity>0</Activity>
--    <SW>0.000</SW>
--    <CW>0.000</CW>
--    <isApproved>0</isApproved>
--    <CMGT>0.000</CMGT>
--    <CMKGT>0</CMKGT>
--    <Kg>0</Kg>
--    <UraniumBrokenKg>0</UraniumBrokenKg>
--    <CubicMetres>0.000</CubicMetres>
--    <CubicsReef>0.000</CubicsReef>
--    <CubicsWaste>0.000</CubicsWaste>
--    <FaceAdvance>0</FaceAdvance>
--    <FaceBrokenKG>0.00000000000</FaceBrokenKG>
--    <FaceCMGT>0.000</FaceCMGT>
--    <FL>30.000</FL>
--    <FaceTons>0</FaceTons>
--    <FaceValue>0</FaceValue>
--    <GoldBroken>0</GoldBroken>
--    <UraniumBroken>0</UraniumBroken>
--    <CubicGrams>0</CubicGrams>
--    <DynamicCubicGT>0</DynamicCubicGT>
--    <IdealSW>120</IdealSW>
--    <IsCubics>N</IsCubics>
--    <callValue>0.000</callValue>
--    <Reefadv>0.000</Reefadv>
--    <Wasteadv>0.000</Wasteadv>
--    <OrgUnitAfternoon />
--    <OrgUnitDay>REAABANURE</OrgUnitDay>
--    <OrgUnitNight />
--    <Prodmonth>201905</Prodmonth>
--    <RomingCrew />
--    <SectionID>REIBLEA</SectionID>
--    <Sectionid_2>REI</Sectionid_2>
--    <Sectionid_1>2.2 - J Ras</Sectionid_1>
--    <SQM>0.000</SQM>
--    <TargetID>9</TargetID>
--    <TrammedLevel />
--    <TrammedTons>0</TrammedTons>
--    <TrammedValue>0</TrammedValue>
--    <RockType>ON</RockType>
--    <WasteSQM>0.000</WasteSQM>
--    <ReefSQM>0.000</ReefSQM>
--    <WorkplaceDesc>192 N3 W 5</WorkplaceDesc>
--    <Workplaceid>RE007919</Workplaceid>
--    <Locked>false</Locked>
--    <StartDate>2019-04-16T00:00:00+02:00</StartDate>
--    <EndDate>2019-05-17T00:00:00+02:00</EndDate>
--    <IsStopped>N</IsStopped>
--    <hasChanged>true</hasChanged>
--    <hasRevised>false</hasRevised>
--  </PlanningData>
--  <PlanningData>
--    <MonthlyReefSQM>0</MonthlyReefSQM>
--    <MonthlyWatseSQM>0</MonthlyWatseSQM>
--    <MonthlyTotalSQM>0</MonthlyTotalSQM>
--    <Activity>0</Activity>
--    <SW>0.000</SW>
--    <CW>0.000</CW>
--    <isApproved>0</isApproved>
--    <CMGT>0.000</CMGT>
--    <CMKGT>0</CMKGT>
--    <Kg>0</Kg>
--    <UraniumBrokenKg>0</UraniumBrokenKg>
--    <CubicMetres>0.000</CubicMetres>
--    <CubicsReef>0.000</CubicsReef>
--    <CubicsWaste>0.000</CubicsWaste>
--    <FaceAdvance>0</FaceAdvance>
--    <FaceBrokenKG>0.00000000000</FaceBrokenKG>
--    <FaceCMGT>0.000</FaceCMGT>
--    <FL>32.000</FL>
--    <FaceTons>0</FaceTons>
--    <FaceValue>0</FaceValue>
--    <GoldBroken>0</GoldBroken>
--    <UraniumBroken>0</UraniumBroken>
--    <CubicGrams>0</CubicGrams>
--    <DynamicCubicGT>0</DynamicCubicGT>
--    <IdealSW>120</IdealSW>
--    <IsCubics>N</IsCubics>
--    <callValue>0.000</callValue>
--    <Reefadv>0.000</Reefadv>
--    <Wasteadv>0.000</Wasteadv>
--    <OrgUnitAfternoon />
--    <OrgUnitDay>REAACANURB</OrgUnitDay>
--    <OrgUnitNight />
--    <Prodmonth>201905</Prodmonth>
--    <RomingCrew />
--    <SectionID>REIALJB</SectionID>
--    <Sectionid_2>REI</Sectionid_2>
--    <Sectionid_1>2.2 - I Van Schalkwyk</Sectionid_1>
--    <SQM>0.000</SQM>
--    <TargetID>1</TargetID>
--    <TrammedLevel />
--    <TrammedTons>0</TrammedTons>
--    <TrammedValue>0</TrammedValue>
--    <RockType>ON</RockType>
--    <WasteSQM>0.000</WasteSQM>
--    <ReefSQM>0.000</ReefSQM>
--    <WorkplaceDesc>192 N3 W 2</WorkplaceDesc>
--    <Workplaceid>RE007899</Workplaceid>
--    <Locked>false</Locked>
--    <StartDate>2019-04-16T00:00:00+02:00</StartDate>
--    <EndDate>2019-05-17T00:00:00+02:00</EndDate>
--    <IsStopped>N</IsStopped>
--    <hasChanged>true</hasChanged>
--    <hasRevised>false</hasRevised>
--  </PlanningData>
--  <PlanningData>
--    <MonthlyReefSQM>0</MonthlyReefSQM>
--    <MonthlyWatseSQM>0</MonthlyWatseSQM>
--    <MonthlyTotalSQM>0</MonthlyTotalSQM>
--    <Activity>0</Activity>
--    <SW>0.000</SW>
--    <CW>0.000</CW>
--    <isApproved>0</isApproved>
--    <CMGT>0.000</CMGT>
--    <CMKGT>0</CMKGT>
--    <Kg>0</Kg>
--    <UraniumBrokenKg>0</UraniumBrokenKg>
--    <CubicMetres>0.000</CubicMetres>
--    <CubicsReef>0.000</CubicsReef>
--    <CubicsWaste>0.000</CubicsWaste>
--    <FaceAdvance>0</FaceAdvance>
--    <FaceBrokenKG>0.00000000000</FaceBrokenKG>
--    <FaceCMGT>0.000</FaceCMGT>
--    <FL>44.000</FL>
--    <FaceTons>0</FaceTons>
--    <FaceValue>0</FaceValue>
--    <GoldBroken>0</GoldBroken>
--    <UraniumBroken>0</UraniumBroken>
--    <CubicGrams>0</CubicGrams>
--    <DynamicCubicGT>0</DynamicCubicGT>
--    <IdealSW>120</IdealSW>
--    <IsCubics>N</IsCubics>
--    <callValue>0.000</callValue>
--    <Reefadv>0.000</Reefadv>
--    <Wasteadv>0.000</Wasteadv>
--    <OrgUnitAfternoon />
--    <OrgUnitDay>REAACANURP</OrgUnitDay>
--    <OrgUnitNight />
--    <Prodmonth>201905</Prodmonth>
--    <RomingCrew />
--    <SectionID>REIDLIA</SectionID>
--    <Sectionid_2>REI</Sectionid_2>
--    <Sectionid_1>2.2 - H Sithebe</Sectionid_1>
--    <SQM>0.000</SQM>
--    <TargetID>9</TargetID>
--    <TrammedLevel />
--    <TrammedTons>0</TrammedTons>
--    <TrammedValue>0</TrammedValue>
--    <RockType>ON</RockType>
--    <WasteSQM>0.000</WasteSQM>
--    <ReefSQM>0.000</ReefSQM>
--    <WorkplaceDesc>192 N3 W 4</WorkplaceDesc>
--    <Workplaceid>RE007901</Workplaceid>
--    <Locked>false</Locked>
--    <StartDate>2019-04-16T00:00:00+02:00</StartDate>
--    <EndDate>2019-05-17T00:00:00+02:00</EndDate>
--    <IsStopped>N</IsStopped>
--    <hasChanged>true</hasChanged>
--    <hasRevised>false</hasRevised>
--  </PlanningData>
--</DocumentElement>'

--set @CycleData = '<?xml version="1.0" standalone="yes"?>
--<DocumentElement>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-16T00:00:00+02:00</Calendardate>
--    <Shift>1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-17T00:00:00+02:00</Calendardate>
--    <Shift>2</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-18T00:00:00+02:00</Calendardate>
--    <Shift>3</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-19T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-20T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-21T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-22T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-23T00:00:00+02:00</Calendardate>
--    <Shift>4</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-24T00:00:00+02:00</Calendardate>
--    <Shift>5</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-25T00:00:00+02:00</Calendardate>
--    <Shift>6</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-26T00:00:00+02:00</Calendardate>
--    <Shift>7</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-27T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-28T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-29T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-04-30T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-01T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-02T00:00:00+02:00</Calendardate>
--    <Shift>8</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-03T00:00:00+02:00</Calendardate>
--    <Shift>9</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-04T00:00:00+02:00</Calendardate>
--    <Shift>10</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-05T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-06T00:00:00+02:00</Calendardate>
--    <Shift>11</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-07T00:00:00+02:00</Calendardate>
--    <Shift>12</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-08T00:00:00+02:00</Calendardate>
--    <Shift>13</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-09T00:00:00+02:00</Calendardate>
--    <Shift>14</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-10T00:00:00+02:00</Calendardate>
--    <Shift>15</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-11T00:00:00+02:00</Calendardate>
--    <Shift>16</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-12T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-13T00:00:00+02:00</Calendardate>
--    <Shift>17</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-14T00:00:00+02:00</Calendardate>
--    <Shift>18</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-15T00:00:00+02:00</Calendardate>
--    <Shift>19</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-16T00:00:00+02:00</Calendardate>
--    <Shift>20</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007899</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIALJB</sectionID>
--    <Calendardate>2019-05-17T00:00:00+02:00</Calendardate>
--    <Shift>21</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>32</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-16T00:00:00+02:00</Calendardate>
--    <Shift>1</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-17T00:00:00+02:00</Calendardate>
--    <Shift>2</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-18T00:00:00+02:00</Calendardate>
--    <Shift>3</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>SU</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-19T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-20T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-21T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-22T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-23T00:00:00+02:00</Calendardate>
--    <Shift>4</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-24T00:00:00+02:00</Calendardate>
--    <Shift>5</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-25T00:00:00+02:00</Calendardate>
--    <Shift>6</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-26T00:00:00+02:00</Calendardate>
--    <Shift>7</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-27T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-28T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-29T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-04-30T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-01T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-02T00:00:00+02:00</Calendardate>
--    <Shift>8</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-03T00:00:00+02:00</Calendardate>
--    <Shift>9</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-04T00:00:00+02:00</Calendardate>
--    <Shift>10</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-05T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-06T00:00:00+02:00</Calendardate>
--    <Shift>11</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-07T00:00:00+02:00</Calendardate>
--    <Shift>12</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-08T00:00:00+02:00</Calendardate>
--    <Shift>13</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-09T00:00:00+02:00</Calendardate>
--    <Shift>14</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-10T00:00:00+02:00</Calendardate>
--    <Shift>15</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-11T00:00:00+02:00</Calendardate>
--    <Shift>16</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-12T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-13T00:00:00+02:00</Calendardate>
--    <Shift>17</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-14T00:00:00+02:00</Calendardate>
--    <Shift>18</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>SUBL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-15T00:00:00+02:00</Calendardate>
--    <Shift>19</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-16T00:00:00+02:00</Calendardate>
--    <Shift>20</Shift>
--    <PlannedValue>24</PlannedValue>
--    <PlannedCode>BL</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-17T00:00:00+02:00</Calendardate>
--    <Shift>21</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-18T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-19T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-20T00:00:00+02:00</Calendardate>
--    <Shift>22</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-21T00:00:00+02:00</Calendardate>
--    <Shift>23</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-22T00:00:00+02:00</Calendardate>
--    <Shift>24</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-23T00:00:00+02:00</Calendardate>
--    <Shift>25</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-24T00:00:00+02:00</Calendardate>
--    <Shift>26</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-25T00:00:00+02:00</Calendardate>
--    <Shift>27</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-26T00:00:00+02:00</Calendardate>
--    <Shift>-1</Shift>
--    <PlannedValue>0</PlannedValue>
--    <PlannedCode>OFF</PlannedCode>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-27T00:00:00+02:00</Calendardate>
--    <Shift>28</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--  <PlanningCycleData>
--    <WorkplaceID>RE007900</WorkplaceID>
--    <prodmonth>201905</prodmonth>
--    <IsCubics>N</IsCubics>
--    <Activity>0</Activity>
--    <sectionID>REIBLHA</sectionID>
--    <Calendardate>2019-05-28T00:00:00+02:00</Calendardate>
--    <Shift>29</Shift>
--    <PlannedValue>0</PlannedValue>
--    <FL>30</FL>
--  </PlanningCycleData>
--</DocumentElement>'

DECLARE @xmlPlanningData XML, @xmlCycleData XML
SELECT @xmlPlanningData = @PlanningData
SELECT @xmlCycleData = @CycleData

CREATE TABLE #TempPLANMONTH (
	[Prodmonth] [numeric](7, 0)  NULL,
	[Sectionid] [varchar](20)  NULL,
	[Activity] [numeric](7, 0)  NULL,
	[IsCubics] [varchar](5)  NULL,
	[PlanCode] [char](2)  NULL,
	[StartDate] [datetime]  NULL,
	[Workplaceid] [varchar](12)  NULL,
	[TargetID] [numeric](10, 0) NULL,
	[OrgUnitDay] [varchar](50) NULL,
	[OrgUnitAfternoon] [varchar](50) NULL,
	[OrgUnitNight] [varchar](50) NULL,
	[RomingCrew] [varchar](50) NULL,
	[Locked] [bit] NULL,
	[Auth] [char](1) NULL,
	[SQM] [numeric](10, 3) NULL,
	[ReefSQM] [numeric](10, 3) NULL,
	[WasteSQM] [numeric](10, 3) NULL,
	[FL] [numeric](10, 3) NULL,
	[ReefFL] [numeric](10, 3) NULL,
	[WasteFL] [numeric](10, 3) NULL,
	[FaceAdvance] [numeric](10, 3) NULL,
	[IdealSW] [numeric](10, 3) NULL,
	[SW] [numeric](10, 3) NULL,
	[CW] [numeric](10, 3) NULL,
	[CMGT] [numeric](10, 3) NULL,
	[GT] [numeric](10, 3) NULL,
	[Kg] [numeric](10, 3) NULL,
	[FaceCMGT] [numeric](10, 3) NULL,
	[FaceKG] [numeric](10, 3) NULL,
	[Tons] [numeric](10, 3) NULL,
	[ReefTons] [numeric](10, 3) NULL,
	[WasteTons] [numeric](10, 3) NULL,
	[FaceValue] [numeric](10, 3) NULL,
	[CubicMetres] [numeric](10, 3) NULL,
	[Cubics] [numeric](10, 3) NULL,
	[CubicsReef] [numeric](10, 3) NULL,
	[CubicsWaste] [numeric](10, 3) NULL,
	[CubicsTons] [numeric](10, 3) NULL,
	[CubicsReefTons] [numeric](10, 3) NULL,
	[CubicsWasteTons] [numeric](10, 3) NULL,
	[CubicGrams] [numeric](10, 3) NULL,
	[CubicDepth] [numeric](10, 3) NULL,
	[ActualDepth] [numeric](10, 3) NULL,
	[CubicGT] [numeric](10, 3) NULL,
	[TrammedTons] [numeric](10, 3) NULL,
	[TrammedValue] [numeric](10, 3) NULL,
	[TrammedLevel] [varchar](10) NULL,
	[Metresadvance] [numeric](10, 3) NULL,
	[ReefAdv] [numeric](10, 3) NULL,
	[WasteAdv] [numeric](10, 3) NULL,
	[DevMain] [numeric](10, 3) NULL,
	[DevSec] [numeric](10, 3) NULL,
	[DevSecReef] [numeric](10, 3) NULL,
	[DevCap] [numeric](10, 3) NULL,
	[LockedDate] [datetime] NULL,
	[LockedBY] [varchar](20) NULL,
	[DrillRig] [varchar](20) NULL,
	[StoppedDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[IsStopped] [char](1) NULL,
	[TopEnd] [char](1) NULL,
	[AutoUnPlan] [char](1) NULL,
	[LabourStrength] [numeric](7, 0) NULL,
	[BoxHoleID] [varchar](20) NULL,
	[MOCycle] [varchar](250) NULL,
	[Vac] [varchar](2) NULL,
	[DC] [varchar](2) NULL,
	[MOCycleNum] [varchar](250) NULL,
	[DevFlag] [varchar](2) NULL,
	[CMKGT] [numeric](7, 0) NULL,
	[UraniumBrokenKG] [numeric](10, 3) NULL,
	[DHeight] [numeric](7, 1) NULL,
	[DWidth] [numeric](7, 1) NULL,
	[Density] [decimal](18, 2) NULL,
	[ReefWaste] [varchar](1) NULL,
	[SurveyAdded] [varchar](1) NULL,
	[DefaultCycle] [varchar](250) NULL,
	[MonthlyReefSQM] [numeric](10, 0) NULL,
	[MonthlyWatseSQM] [numeric](10, 0) NULL,
	[MonthlyTotalSQM] [numeric](10, 0) NULL)




insert into #TempPLANMONTH
(
 Prodmonth,
 Sectionid,
 Activity,
 Workplaceid,
 OrgUnitDay,
 MonthlyReefSQM,
 MonthlyWatseSQM,
 MonthlyTotalSQM,
 SQM ,
 FL,
 TargetID,
 IsCubics,
 CMGT,
 SW,
 IdealSW,
 CW,
 WasteSQM,
 ReefSQM,
 	[CubicMetres],
	[Cubics] ,
	[CubicsReef] ,
	[CubicsWaste] ,
	[CubicsTons] ,
	[CubicsReefTons] ,
	[CubicsWasteTons] ,
	[CubicGrams] ,
	[CubicDepth] ,
	[StartDate],
	[EndDate])
SELECT 
    T.C.value('(Prodmonth)[1]', '[numeric](7,0)') AS Prodmonth,
	T.C.value('(SectionID)[1]', '[varchar](20)') AS SectionID,
	 T.C.value('(Activity)[1]', '[numeric](7,0)') AS Activity,
    T.C.value('(Workplaceid)[1]', '[varchar](100)') AS Workplaceid,
	T.C.value('(OrgUnitDay)[1]', '[varchar](50)') AS OrgUnitDay,
	T.C.value('(MonthlyReefSQM)[1]', '[numeric](10,3)') AS MonthlyReefSQM,
	T.C.value('(MonthlyWatseSQM)[1]', '[numeric](10,3)') AS MonthlyWatseSQM,
	T.C.value('(MonthlyTotalSQM)[1]', '[numeric](10,3)') AS MonthlyTotalSQM,
	T.C.value('(callValue)[1]', '[numeric](10,3)') AS SQM,
	T.C.value('(FL)[1]', '[numeric](10,3)') AS FL,
	T.C.value('(TargetID)[1]', '[numeric](10,3)') AS TargetID,
	T.C.value('(IsCubics)[1]', '[varchar](100)') AS IsCubics,
	T.C.value('(CMGT)[1]', '[numeric](10,3)') AS CMGT,
	T.C.value('(SW)[1]', '[numeric](10,3)') AS SW,
	T.C.value('(IdealSW)[1]', '[numeric](10,3)') AS IdealSW,
	T.C.value('(CW)[1]', '[numeric](10,3)') AS CW,
	T.C.value('(WasteSQM)[1]', '[numeric](10,3)') AS WasteSQM,
	T.C.value('(ReefSQM)[1]', '[numeric](10,3)') AS ReefSQM,
	T.C.value('(CubicMetres)[1]', '[numeric](10,3)') AS CubicMetres,
	T.C.value('(Cubics)[1]', '[numeric](10,3)') AS Cubics,
	T.C.value('(CubicsReef)[1]', '[numeric](10,3)') AS CubicsReef,
	T.C.value('(CubicsWaste)[1]', '[numeric](10,3)') AS CubicsWaste,
	T.C.value('(CubicsTons)[1]', '[numeric](10,3)') AS CubicsTons,
	T.C.value('(CubicsReefTons)[1]', '[numeric](10,3)') AS CubicsReefTons,
	T.C.value('(CubicsWasteTons)[1]', '[numeric](10,3)') AS CubicsWasteTons,
	T.C.value('(CubicGrams)[1]', '[numeric](10,3)') AS CubicGrams,
	T.C.value('(CubicDepth)[1]', '[numeric](10,3)') AS CubicDepth,
	T.C.value('(StartDate)[1]', '[datetime]') AS StartDate,
	T.C.value('(EndDate)[1]', '[datetime]') AS EndDate


	
FROM 
    @xmlPlanningData.nodes('/DocumentElement/PlanningData') T(C)


	
	update pm set pm.SQM = tpm.SQM,
	              pm.Sectionid = tpm.Sectionid,
				  pm.TargetID = tpm.TargetID,
				  pm.MonthlyReefSQM = tpm.MonthlyReefSQM,
				  pm.MonthlyWatseSQM = tpm.MonthlyWatseSQM,
				  pm.MonthlyTotalSQM = tpm.MonthlyTotalSQM,
				  pm.FL = tpm.FL,
				  pm.OrgUnitDay = tpm.OrgUnitDay,
				  pm.CMGT = tpm.CMGT,
				  pm.SW = tpm.SW,
				  pm.IdealSW = tpm.IdealSW,
				  pm.CW = tpm.CW,
				  pm.WasteSQM = tpm.WasteSQM,
				  pm.ReefSQM = tpm.ReefSQM,
				  pm.[CubicMetres] = tpm.[CubicMetres],
	pm.[Cubics] = tpm.[Cubics],
	pm.[CubicsReef] = tpm.[CubicsReef],
	pm.[CubicsWaste] = tpm.[CubicsWaste],
	pm.[CubicsTons] =tpm.[CubicsTons],
	pm.[CubicsReefTons] = tpm.[CubicsReefTons],
	pm.[CubicsWasteTons] = tpm.[CubicsWasteTons],
	pm.[CubicGrams] = tpm.[CubicGrams],
	pm.[CubicDepth] = tpm.[CubicDepth],
	pm.StartDate = tpm.StartDate,
	pm.EndDate = tpm.EndDate

	--select *
				  	from PLANMONTH pm
	inner join #TempPLANMONTH tpm on 
	pm.Activity = tpm.Activity and
	pm.Workplaceid = tpm.Workplaceid and
	--pm.Sectionid = tpm.Sectionid and
	pm.PlanCode = 'MP' and
	pm.IsCubics = tpm.IsCubics and
	pm.Prodmonth = tpm.Prodmonth

	select * from #TempPLANMONTH

		drop table #TempPLANMONTH


	if(@CycleData <> 'NONE')
	BEGIN

	create table #TempCycleData (
	 WorkplaceID [varchar](100)  NULL,
	 prodmonth numeric(7,0),
     IsCubics varchar(5),
     Activity numeric(7,0),
     sectionID varchar(12),
	 Calendardate datetime  NULL,
	 [Shift] int  NULL,
	 PlannedValue numeric(5,0) null,
	 PlannedCode varchar(20)  NULL,
	 FL numeric(7,0) null,
)


	insert into #TempCycleData
	SELECT 
   
	T.C.value('(WorkplaceID)[1]', '[varchar](100)') AS WorkplaceID,
	T.C.value('(prodmonth)[1]', 'numeric(7,0)') AS prodmonth,
	T.C.value('(IsCubics)[1]', '[varchar](5)') AS IsCubics,
	T.C.value('(Activity)[1]', 'numeric(7,0)') AS Activity,
	T.C.value('(sectionID)[1]', '[varchar](12)') AS sectionID,
	 T.C.value('(Calendardate)[1]', '[datetime]') AS Calendardate,
	T.C.value('(Shift)[1]', '[varchar](100)') AS [Shift],
	T.C.value('(PlannedValue)[1]', 'numeric(5,0)') AS PlannedValue,
	T.C.value('(PlannedCode)[1]', '[varchar](20)') AS PlannedCode,
	T.C.value('(FL)[1]', '[varchar](20)') AS FL



	
FROM 
    @xmlCycleData.nodes('/DocumentElement/PlanningCycleData') T(C)

	


	update p set p.FL = tp.FL,
	             p.SectionID = tp.sectionID,
				 p.ShiftDay = tp.[Shift],
				 p.SQM = tp.PlannedValue,
				 p.ReefSQM = tp.PlannedValue,
				 p.MOCycle = tp.PlannedCode 

	from PLANNING p
	inner join #TempCycleData tp on
	p.WorkplaceID = tp.WorkplaceID and
	p.Prodmonth = tp.prodmonth and
	CONVERT(VARCHAR(10), p.Calendardate, 103)  = CONVERT(VARCHAR(10), tp.CalendarDate , 103) and
	p.IsCubics = tp.IsCubics and
	p.PlanCode = 'MP' and
	p.Activity = tp.Activity

	drop table #TempCycleData
	END

