Create Procedure sp_InsertUpdate_Calendars
		 @data CalendarsType ReadOnly
As
Begin
    Merge Calendars d
    Using @Data s ON d.Mine = s.Mine
			 And d.CalendarDate = s.CalendarDate
			 And d.SectionId = s.SectionId
			 And d.CalendarCode = s.CalendarCode
			 And d.ProdMonth = s.ProdMonth
    When Matched Then Update
	   Set WorkingDay = s.WorkingDay,
		  BeginDate = s.BeginDate,
		  EndDate = s.EndDate,
		  TotalShifts = s.TotalShifts
    When Not Matched Then Insert 
	   Values (
		  s.CalendarDate, 
		  s.WorkingDay, 
		  s.ProdMonth, 
		  s.SectionId, 
		  s.CalendarCode, 
		  s.BeginDate, 
		  s.EndDate, 
		  s.TotalShifts, 
		  s.Mine
	   );
End