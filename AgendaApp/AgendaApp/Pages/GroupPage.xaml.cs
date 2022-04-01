using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgendaApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        public GroupPage(List<DayWeek> shedGroup, DateTime date)
        {
            InitializeComponent();
            cvSchedule.IsVisible = false;
            Agenda agenda = new Agenda();
            cvSchedule.ItemsSource = agenda.MyAgenda(date, shedGroup);
            cvSchedule.IsVisible = true;
        }
    }
}