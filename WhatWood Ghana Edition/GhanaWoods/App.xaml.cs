//using CAMCWoodsNETLibrary;

using GhanaWoods.Database;
using System.Globalization;
using Microsoft.Maui.Storage;

namespace GhanaWoods
{
    public partial class App : Application
    {
        public static GhanaWoodsContext database;

        public App()
        {
            InitializeComponent();

            //if (Preferences.Get("LanguageSet", false))
            //{
                //int language = Preferences.Get("Language", 0);
                //if (language == 0)
                //{
                    CultureInfo.CurrentCulture = new CultureInfo("en", false);
                    CultureInfo.CurrentUICulture = new CultureInfo("en", false);
                //}
                //else if (language == 1)
                //{
                //    CultureInfo.CurrentCulture = new CultureInfo("es", false);
                //    CultureInfo.CurrentUICulture = new CultureInfo("es", false);
                //}
            //}

            //MainPage = new AppShell();

            if (Application.Current != null)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                int appTheme = Preferences.Get("AppTheme", 0);
                if (mergedDictionaries != null)
                {
                    if (appTheme == 0)
                    {
                        mergedDictionaries.Clear();

                        mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewColors());
                        mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewStyles());
                    }
                    else if (appTheme == 1)
                    {
                        mergedDictionaries.Clear();

                        mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewCBColors());
                        mergedDictionaries.Add(new GhanaWoods.Resources.Styles.NewStyles());
                    }
                }


                Application.Current.ActivateWindow(new Window(new AppShell()));
            }



            //int launchPage = Preferences.Get("LaunchPage", 2);
            //switch (launchPage)
            //{
            //    case 0:
            //        Shell.Current.GoToAsync("//Study", false);
            //        break;
            //    case 1:
            //        Shell.Current.GoToAsync("//Learn", false);
            //        break;
            //    case 2:
            //        Shell.Current.GoToAsync("//ID", false);
            //        break;
            //    case 3:
            //        Shell.Current.GoToAsync("//Reference", false);
            //        break;
            //    default:
            //        Shell.Current.GoToAsync("//ID", false);
            //        break;
            //}
            //Shell.Current.Navigation.PushAsync(new InitPage(), false);
        }

        public static GhanaWoodsContext DB
        {
            get
            {
                if (database == null) 
                {
                    database = GhanaWoodsContext.Create();
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            int launchPage = Preferences.Get("LaunchPage", 2);
            switch (launchPage)
            {
                case 0:
                    Shell.Current.GoToAsync("//Study", false);
                    break;
                case 1:
                    Shell.Current.GoToAsync("//Learn", false);
                    break;
                case 2:
                    Shell.Current.GoToAsync("//ID", false);
                    break;
                case 3:
                    Shell.Current.GoToAsync("//Reference", false);
                    break;
                default:
                    Shell.Current.GoToAsync("//ID", false);
                    break;
            }
            Shell.Current.Navigation.PushAsync(new InitPage(), false);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}
