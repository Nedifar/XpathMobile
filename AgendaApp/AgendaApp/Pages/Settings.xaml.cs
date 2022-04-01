using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        ObservableCollection<string> groupList;
        ObservableCollection<string> teacherList;
        ObservableCollection<string> cabinetList;
        public Settings()
        {
            InitializeComponent();
            //Preferences.Set("cabinetSelected", "");
            loading.IsAnimationPlaying = true;
            currentVersion.Text = VersionTracking.CurrentVersion;
            latestVersion.Text = Models.Http.versionRelease;
            load();
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkGroup.IsChecked = false;
            checkGroup.IsEnabled = false;
            if (pickGroup.SelectedItem.ToString() == "Не выбрано")
            {
                if (Preferences.Get("groupSelected", "") == "")
                    Preferences.Set("groupSelected", "");
            }
            else
            {
                if (Preferences.Get("loadGroup", true))
                { checkGroup.IsChecked = true; }
                checkGroup.IsEnabled = true;
                Preferences.Set("groupSelected", "");
                Preferences.Set("groupSelected", "★" + pickGroup.SelectedItem.ToString().Replace("★", ""));
            }
        }

        private void PickerTeach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickTeacher.SelectedItem.ToString() == "Не выбрано")
            {
                checkTeacher.IsChecked = false;
                checkTeacher.IsEnabled = false;
                if (Preferences.Get("teacherSelected", "") == "")
                    Preferences.Set("teacherSelected", "");
            }
            else
            {
                if (Preferences.Get("loadTeacher", true))
                { checkTeacher.IsChecked = true; }
                checkTeacher.IsEnabled = true;
                Preferences.Set("teacherSelected", "");
                Preferences.Set("teacherSelected", "★" + pickTeacher.SelectedItem.ToString().Replace("★", ""));
            }
        }

        private void PickerCav_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkCabinet.IsChecked = false;
            checkCabinet.IsEnabled = false;
            if (pickCabinet.SelectedItem.ToString() == "Не выбрано")
            {
                if (Preferences.Get("cabinetSelected", "") == "")
                    Preferences.Set("cabinetSelected", "");
            }
            else
            {
                if (Preferences.Get("loadCabinet", true))
                { checkCabinet.IsChecked = true; }
                checkCabinet.IsEnabled = true;
                Preferences.Set("cabinetSelected", "");
                Preferences.Set("cabinetSelected", "★" + pickCabinet.SelectedItem.ToString().Replace("★", ""));
            }
        }
        private async void load()
        {
            sp.IsVisible = false;
            using (var http = new HttpClient())
            {
                var resGroupList = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastdance/getgrouplist/"));
                resGroupList.EnsureSuccessStatusCode();
                groupList = await resGroupList.Content.ReadAsAsync<ObservableCollection<string>>();
                groupList.Insert(0, "Не выбрано");
                pickGroup.ItemsSource = groupList;
                pickGroup.SelectedIndex = 0;
                var resTeachList = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastdance/getteachersList"));
                resTeachList.EnsureSuccessStatusCode();
                teacherList = await resTeachList.Content.ReadAsAsync<ObservableCollection<string>>();
                teacherList.Insert(0, "Не выбрано");
                pickTeacher.ItemsSource = teacherList;
                pickTeacher.SelectedIndex = 0;

                var resCabinetList = await http.GetAsync(new Uri($"http://lastdance.somee.com/api/lastdance/getcabinetslist/"));
                resCabinetList.EnsureSuccessStatusCode();
                cabinetList = await resCabinetList.Content.ReadAsAsync<ObservableCollection<string>>();
                cabinetList.Insert(0, "Не выбрано");
                pickCabinet.ItemsSource = cabinetList;
                pickCabinet.SelectedIndex = 0;
            }
            if (!Preferences.ContainsKey("teacherSelected"))
            {
                Preferences.Set("teacherSelected", "");
            }
            if (!Preferences.ContainsKey("groupSelected"))
            {
                Preferences.Set("groupSelected", "");
            }
            if (!Preferences.ContainsKey("cabinetSelected"))
            {
                Preferences.Set("cabinetSelected", "");
            }
            string tS = Preferences.Get("teacherSelected", "");
            if (tS != "")
            {
                teacherList.Insert(1, tS);
                //pickTeacher.ItemsSource = null;
                pickTeacher.ItemsSource = teacherList;
                pickTeacher.SelectedIndex = 1;
            }
            string gS = Preferences.Get("groupSelected", "");
            if (gS != "")
            {
                groupList.Insert(1, gS);
                //pickGroup.ItemsSource = null;
                pickGroup.ItemsSource = groupList;
                pickGroup.SelectedIndex = 1;
            }
            string cS = Preferences.Get("cabinetSelected", "");
            if (cS != "")
            {
                cabinetList.Insert(1, cS);
                //pickCabinet.ItemsSource = null;
                pickCabinet.ItemsSource = cabinetList;
                pickCabinet.SelectedIndex = 1;
            }
            sp.IsVisible = true;
            loading.IsVisible = false;
        }

        private void checkGroup_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkGroup.IsChecked == true)
                Preferences.Set("loadGroup", true);
            else
                Preferences.Set("loadGroup", false);
        }

        private void checkCabinet_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkCabinet.IsChecked == true)
                Preferences.Set("loadCabinet", true);
            else
                Preferences.Set("loadCabinet", false);
        }

        private void checkTeacher_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (checkTeacher.IsChecked == true)
                Preferences.Set("loadTeacher", true);
            else
                Preferences.Set("loadTeacher", false);
        }
    }
}