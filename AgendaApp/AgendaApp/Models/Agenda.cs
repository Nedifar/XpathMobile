using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace AgendaApp.Models
{
    public class Agenda
    {
        public string DayWeek { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<DayWeek> DayWeeks { get; set; }
        public string Color { get; set; }

        public ObservableCollection<Agenda> MyAgenda(DateTime DdownDay, List<DayWeek> shed) 
        {
            shed = AddTimeShedule(shed);
            return GetAgenda(DdownDay, shed); 
        }


        private ObservableCollection<Agenda> GetAgenda(DateTime DdownDay, List<DayWeek> shed)
        {
            return new ObservableCollection<Agenda>
            {
                new Agenda { DayWeek = "Понедельник", Color = "#B96CBD", Date = DdownDay,
                    DayWeeks = new ObservableCollection<DayWeek>(shed.Take(6)) },

                new Agenda { DayWeek = "Вторник", Color = "#49A24D", Date = DdownDay.AddDays(1),
                    DayWeeks = new ObservableCollection<DayWeek>(shed.Skip(6).Take(6)) },

                new Agenda { DayWeek = "Среда", Color = "#FDA838", Date = DdownDay.AddDays(2),
                    DayWeeks = new ObservableCollection<DayWeek>(shed.Skip(12).Take(6)) },

                new Agenda { DayWeek = "Четверг", Color = "#F75355", Date = DdownDay.AddDays(3),
                    DayWeeks = new ObservableCollection<DayWeek>(shed.Skip(18).Take(7)) },

                new Agenda { DayWeek = "Пятница", Color = "#00C6AE", Date = DdownDay.AddDays(4),
                    DayWeeks = new ObservableCollection<DayWeek>(shed.Skip(25).Take(6)) },

                new Agenda { DayWeek = "Суббота", Color = "#455399", Date = DdownDay.AddDays(5),
                    DayWeeks = new ObservableCollection<DayWeek>(shed.Skip(31).Take(6)) }
            };
        }
        private List<DayWeek> AddTimeShedule(List<DayWeek> shedule)
        {
            for(int i = 0; i<=shedule.Count-6;)
            {
                if (i != 18)
                {
                    shedule[i].Date = "\n08:30 - 10:00";
                    shedule[i + 1].Date = "\n10:10 - 11:40";
                    shedule[i + 2].Date = "\n12:00 - 13:30";
                    shedule[i + 3].Date = "\n14:00 - 15:30";
                    shedule[i + 4].Date = "\n15:50 - 17:10";
                    shedule[i + 5].Date = "\n17:15 - 18:45";
                    i += 6;
                }
                else
                {
                    shedule[i].Date = "\n08:30 - 10:00";
                    shedule[i + 1].Date = "\n10:10 - 11:40";
                    shedule[i + 2].Date = "\n12:00 - 13:30";
                    shedule[i + 3].Date = "\n13:40 - 14:10";
                    shedule[i + 4].Date = "\n14:20 - 15:50";
                    shedule[i + 5].Date = "\n16:00 - 17:30";
                    shedule[i + 6].Date = "\n17:35 - 19:05";
                    i += 7;
                }
            }
            for (int i = 0; i < shedule.Count; i++)
                shedule[i].Day = "\n" + shedule[i].Day;
            return shedule;
        }
    }
}
