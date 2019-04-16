Create View [dbo].Vw_Calendars_DW as 
(
    Select m.DivisionCode, m.Description, c.CalendarDate, c.WorkingDay, c.ProdMonth, c.SectionId, c.CalendarCode, c.BeginDate, c.EndDate, c.TotalShifts, c.Mine
    From Calendars c
    Inner Join Code_WpDivision m on m.Mine = c.Mine
)