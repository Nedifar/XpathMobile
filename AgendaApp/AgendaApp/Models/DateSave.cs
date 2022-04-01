using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaApp.Models
{
    public class DateSave
    {
        public static Date date = new Date( );
        public static void dateGet()
        {
            date.SelectedDate = DateTime.Today;
            date.GetDate(DateTime.Today);
        }
    }
}
