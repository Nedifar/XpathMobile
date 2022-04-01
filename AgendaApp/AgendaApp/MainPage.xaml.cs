using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;
using AgendaApp.Models;
using Xamarin.Essentials;

namespace AgendaApp
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        HttpClient http = new HttpClient();
        Date dateSchedule = new Date();
        public MainPage()
        {
            InitializeComponent();
            tryingNewSchedule();
            dpDateSchedule.Date = DateSave.date.SelectedDate;
            dpDateSchedule_DateSelected(null, null);
            GetGroupList();
            this.BindingContext = this;
        }

        private async void dpDateSchedule_DateSelected(object sender, DateChangedEventArgs e)
        {
            while (5 > 3)
            {
                try
                {
                    loading.IsVisible = true;
                    loading.IsAnimationPlaying = true;
                    cvSchedule.IsVisible = false;
                    cvSchedule.ItemsSource = null;
                    dateSchedule.GetDate(dpDateSchedule.Date);
                    lbFirstDay.Text = dateSchedule.downDay.ToString();
                    lbSecondDay.Text = dateSchedule.upDay.ToString();
                    lbSecondMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateSchedule.DupDay.Month);
                    lbFirstMonth.Text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateSchedule.DdownDay.Month);
                    var resDate = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastdance/getdate/{dpDateSchedule.Date.ToShortDateString()}"));
                    resDate.EnsureSuccessStatusCode();
                    pGroup_SelectedIndexChanged(null, null);
                    resDate.EnsureSuccessStatusCode();
                    cvSchedule.IsVisible = true;
                    loading.IsAnimationPlaying = false;
                    loading.IsVisible = false;
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                    continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }

        }

        private async void pGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            while (5 > 3)
            {
                try
                {
                    if (pGroup.SelectedIndex != -1)
                    {
                        loading.IsVisible = true;
                        loading.IsAnimationPlaying = true;
                        cvSchedule.IsVisible = false;
                        var resGroup = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastdance/getgroup/{pGroup.SelectedItem.ToString().Replace("★", "")}"));
                        resGroup.EnsureSuccessStatusCode();
                        var groupShedule = resGroup.Content.ReadAsAsync<List<DayWeek>>();
                        List<DayWeek> list = await groupShedule;
                        Agenda agenda = new Agenda();
                        cvSchedule.ItemsSource = agenda.MyAgenda(dateSchedule.DdownDay, list);
                        cvSchedule.IsVisible = true;
                        loading.IsAnimationPlaying = false;
                        loading.IsVisible = false;

                    }
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                    continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }
        }

        private async void GetGroupList()
        {
            while (5 > 3)
            {
                try
                {
                    var resGroupList = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastdance/getgrouplist/"));
                    resGroupList.EnsureSuccessStatusCode();
                    ObservableCollection<string> vs = await resGroupList.Content.ReadAsAsync<ObservableCollection<string>>();
                    pGroup.ItemsSource = vs;
                    string res = Preferences.Get("groupSelected", "");
                    if (res != "")
                    {
                        vs.Insert(0, res);
                    }
                    if (Preferences.Get("loadGroup", false))
                    {
                        pGroup.SelectedItem = res;
                    }
                    break;
                }
                catch
                {
                    //bool resault = await DisplayAlert("Connection Failed", "Check your internet connection!", "Try again", "Cancel");
                    //if (resault)
                    //{
                    continue;
                    //}
                    //else
                    //    Environment.Exit(0);
                }
            }
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            while (5 > 3)
            {
                try
                {
                    if (NewSheduleBt.Source.ToString() == "File: gg.png")
                    {
                        string s = "Новое расписание!!!";
                        var resultNewWeek = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastDance/getnewWeek/{s}"));
                        resultNewWeek.EnsureSuccessStatusCode();
                        pGroup.SelectedIndex = -1;
                        dpDateSchedule.Date = DateTime.Today.AddDays(5);
                        NewSheduleBt.Source = "fr.png";
                    }
                    else
                    {
                        NewSheduleBt.Source = "gg.png";
                        pGroup.SelectedIndex = -1;
                        dpDateSchedule.Date = DateTime.Today;
                    }
                    break;
                }
                catch
                {
                    continue;
                }
            }
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Pages.Settings());
        }
        private async void tryingNewSchedule()
        {
            while (5 > 3)
            {
                try
                {
                    var resnew = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastdance/getnew"));
                    resnew.EnsureSuccessStatusCode();
                    var res = resnew.Content.ReadAsStringAsync().Result;
                    if (res.ToString() == "есть новое расписание")
                    {
                        Preferences.Set("haveWeek", true);
                        NewSheduleBt.IsVisible = true;
                    }
                    else
                        Preferences.Set("haveWeek", false);
                    break;
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
