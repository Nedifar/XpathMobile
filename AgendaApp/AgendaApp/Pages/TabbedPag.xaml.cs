using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using System.Net.Http;
using System.Net;
using System.IO;

namespace AgendaApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class TabbedPag : Xamarin.Forms.TabbedPage
    {
        [Obsolete]
        public TabbedPag()
        {
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom)
             .SetBarItemColor(Color.FromRgb(0, 198, 174))
             .SetBarSelectedItemColor(Color.FromRgb(0, 83, 153));
            Models.DateSave.dateGet();
            ContentPage groupPage = new MainPage();
            groupPage.IconImageSource = "group.png";
            groupPage.Title = "Group";
            ContentPage cabinetPage = new CabinetPage();
            cabinetPage.IconImageSource = "cab.png";
            cabinetPage.Title = "Cabinet";
            ContentPage teacherpage = new TeacherPage();
            teacherpage.IconImageSource = "teach.png";
            teacherpage.Title = "Teacher";
            Children.Add(groupPage);
            Children.Add(cabinetPage);
            Children.Add(teacherpage);
            Sign();
            //version();
        }
        async void version()
        {
            VersionTracking.Track();
            var currentVersion = VersionTracking.CurrentVersion;
            WebClient web = new WebClient();//Для использования WebClient подключи System.Net 
            string vars = web.DownloadString("http://lastdance.somee.com/i/updateMobile/mobileVersion.txt");
            string link;
            if(vars != currentVersion)
            {
                bool b = await DisplayAlert("Warning", "App have new update. Download?", "Ok", "Cancel");
                if (b)
                {
                    //link = web.DownloadString("http://lastdance.somee.com/i/updateMobile/mobileLink.txt");
                    //web.DownloadFile(link, $"{System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal)}com.companyname.agendaapp.apk");
                    web.DownloadFileCompleted += (s, e) =>
                     {

                        //string error = e.Error.Message;
                        //string documentPath = 
                    };
                }


            }
        }
        async void Sign()
        {
            string data = "";
            var backing = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "appConfigMod.txt");
            try
            {
                using (var reader = new StreamReader(backing))
                {
                    double l;
                    double g = 0;
                    data = await reader.ReadToEndAsync();
                    reader.Close();
                    if (data != "Mat'NeTrogai")
                        throw new NullReferenceException();
                }
            }
            catch
            {
                using (HttpClient client = new HttpClient())
                {
                    string result = await DisplayPromptAsync("Scaning", "Please, enter password.(ggwp)");
                    var resulti = await client.GetAsync($"http://lastdance.somee.com/api/lastdance/getSignData/{result}");
                    if (resulti.StatusCode == HttpStatusCode.BadRequest)
                    {
                        await DisplayAlert("Imposter", "А вот неть, а вот тебя я не пущу, с уважением", "OK");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        using (var sw = new StreamWriter(backing, false, Encoding.Default))
                        {
                            sw.Write(result);
                            data = result;
                        }
                    }
                }
            }
            if (data.Trim() == "")
            {
                await DisplayAlert("Imposter", "Защита от Егора на месте.", "OK");
                System.Environment.Exit(0);
            }
        }
    }
}