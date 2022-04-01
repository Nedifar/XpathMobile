using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Distribute;
using System.Threading.Tasks;

namespace AgendaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pages.TabbedPag());
        }

        protected override void OnStart()
        {
            Distribute.ReleaseAvailable = OnReleaseAvailable;
            Distribute.NoReleaseAvailable = OnNoReleaseAvailable;
            AppCenter.Start("android=530bebd3-f579-46c7-bd7d-8840c92d0bef", typeof(Distribute));
            Distribute.SetEnabledAsync(true);
        }

        private void OnNoReleaseAvailable()
        {
            Models.Http.versionRelease = "нет";
        }

        private bool OnReleaseAvailable(ReleaseDetails releaseDetails)
        {
            string versionName = releaseDetails.ShortVersion;
            string versionCodeOrBuildNumber = releaseDetails.Version;
            string releaseNotes = releaseDetails.ReleaseNotes;
            Uri releaseNotesUrl = releaseDetails.ReleaseNotesUrl;
            Models.Http.versionRelease = versionName + "\n" + versionCodeOrBuildNumber + "\n" + releaseNotes + "\n" + releaseNotesUrl;
            // custom dialog
            var title = "Version " + versionName + " available!";
            Task answer;

            // On mandatory update, user can't postpone
            if (releaseDetails.MandatoryUpdate)
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install");
            }
            else
            {
                answer = Current.MainPage.DisplayAlert(title, releaseNotes, "Download and Install", "Maybe tomorrow...");
            }
            answer.ContinueWith((task) =>
            {
                // If mandatory or if answer was positive
                if (releaseDetails.MandatoryUpdate || (task as Task<bool>).Result)
                {
                    // Notify SDK that user selected update
                    Distribute.NotifyUpdateAction(UpdateAction.Update);
                }
                else
                {
                    // Notify SDK that user selected postpone (for 1 day)
                    // This method call is ignored by the SDK if the update is mandatory
                    Distribute.NotifyUpdateAction(UpdateAction.Postpone);
                }
            });

            // Return true if you're using your own dialog, false otherwise
            return true;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
