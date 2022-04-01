using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaApp.Models
{
    public class Date
    {
        public int downDay { get; set; }
        public int upDay { get; set; }
        public DateTime DupDay { get; set; }
        public DateTime DdownDay { get; set; }
        public DateTime SelectedDate { get; set; }
        public void GetDate(DateTime dpDateSchedule)
        {
            switch (dpDateSchedule.Date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    downDay = dpDateSchedule.Date.AddDays(1).Day;
                    upDay = dpDateSchedule.Date.AddDays(6).Day;
                    DupDay = dpDateSchedule.Date.AddDays(6);
                    DdownDay = dpDateSchedule.Date.AddDays(1);
                    break;
                case DayOfWeek.Monday:
                    downDay = dpDateSchedule.Date.Day;
                    upDay = dpDateSchedule.Date.AddDays(5).Day;
                    DupDay = dpDateSchedule.Date.AddDays(5);
                    DdownDay = dpDateSchedule.Date;
                    break;
                case DayOfWeek.Tuesday:
                    downDay = dpDateSchedule.Date.AddDays(-1).Day;
                    upDay = dpDateSchedule.Date.AddDays(4).Day;
                    DupDay = dpDateSchedule.Date.AddDays(4);
                    DdownDay = dpDateSchedule.Date.AddDays(-1);
                    break;
                case DayOfWeek.Wednesday:
                    downDay = dpDateSchedule.Date.AddDays(-2).Day;
                    upDay = dpDateSchedule.Date.AddDays(3).Day;
                    DupDay = dpDateSchedule.Date.AddDays(3);
                    DdownDay = dpDateSchedule.Date.AddDays(-2);
                    break;
                case DayOfWeek.Thursday:
                    downDay = dpDateSchedule.Date.AddDays(-3).Day;
                    upDay = dpDateSchedule.Date.AddDays(2).Day;
                    DupDay = dpDateSchedule.Date.AddDays(2);
                    DdownDay = dpDateSchedule.Date.AddDays(-3);
                    break;
                case DayOfWeek.Friday:
                    downDay = dpDateSchedule.Date.AddDays(-4).Day;
                    upDay = dpDateSchedule.Date.AddDays(1).Day;
                    DupDay = dpDateSchedule.Date.AddDays(1);
                    DdownDay = dpDateSchedule.Date.AddDays(-4);
                    break;
                case DayOfWeek.Saturday:
                    downDay = dpDateSchedule.Date.AddDays(-5).Day;
                    upDay = dpDateSchedule.Date.Day;
                    DupDay = dpDateSchedule.Date;
                    DdownDay = dpDateSchedule.Date.AddDays(-5);
                    break;
            }
        }
    }

}
