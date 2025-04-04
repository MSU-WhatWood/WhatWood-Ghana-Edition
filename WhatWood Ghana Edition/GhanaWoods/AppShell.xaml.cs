using Microsoft.Maui.Devices;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel;
namespace GhanaWoods
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            DevicePlatform dPlat = new DevicePlatform();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                dPlat = DeviceInfo.Current.Platform;
            });

            if (dPlat == DevicePlatform.Android) Preferences.Set("DevicePlatform", "Android");
            if (dPlat == DevicePlatform.iOS) Preferences.Set("DevicePlatform", "iOS");
            if (dPlat == DevicePlatform.macOS || dPlat == DevicePlatform.MacCatalyst) Preferences.Set("DevicePlatform", "Mac");
            if (dPlat == DevicePlatform.WinUI) Preferences.Set("DevicePlatform", "Windows");


            //Routing.RegisterRoute("Study/StudyPreface", typeof(StudyPreface));
            //Routing.RegisterRoute("Study/StudyAcknowledgements", typeof(StudyAcknowledgements));
            Routing.RegisterRoute("Study/StudyBackground", typeof(StudyBackground));
            Routing.RegisterRoute("Study/StudyChapter1", typeof(StudyChapter1));
            Routing.RegisterRoute("Study/StudyChapter2", typeof(StudyChapter2));
            Routing.RegisterRoute("Study/StudyChapter3", typeof(StudyChapter3));
            Routing.RegisterRoute("Study/StudyChapter4", typeof(StudyChapter4));
            Routing.RegisterRoute("Study/StudyChapter5", typeof(StudyChapter5));
            Routing.RegisterRoute("Study/StudyChapter6", typeof(StudyChapter6));
            Routing.RegisterRoute("Study/StudyChapter7", typeof(StudyChapter7));
            Routing.RegisterRoute("Study/StudyChapter8", typeof(StudyChapter8));
            Routing.RegisterRoute("Study/StudyIndex", typeof(StudyIndex));
            Routing.RegisterRoute("Learn/LearnFeaturesPage", typeof(LearnFeaturesPage));
            Routing.RegisterRoute("Settings/Credits", typeof(Credits));
            Routing.RegisterRoute("InitPage", typeof(InitPage));

        }
    }
}
