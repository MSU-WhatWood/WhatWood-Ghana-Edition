using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using GhanaWoods.Database;
//using GhanaWoodsNETLibrary;
using CommunityToolkit.Maui;

namespace GhanaWoods
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Bold.ttf", "OpenSansBold");
                    fonts.AddFont("OpenSans-BoldItalic.ttf", "OpenSansBoldItalic");
                    fonts.AddFont("OpenSans-Italic.ttf", "OpenSansItalic");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("OpenSans-SemiBoldItalic.ttf", "OpenSansSemiboldItalic");
                })
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
                    //handlers.AddHandler(typeof(Shell), typeof(GhanaWoods.CustomShellRenderer));
#endif
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddDbContext<GhanaWoodsContext>(
                options => options.UseSqlite($"Filename={GetDatabasePath()}", x => x.MigrationsAssembly(nameof(GhanaWoods))));
            //options => options.UseSqlite($"Filename={GetDatabasePath()}", x => x.MigrationsAssembly(nameof(GhanaWoodsNETLibrary))));

            //builder.Services.AddTransient<IDMainPageNew>();
            //builder.Services.AddSingleton<ImageZoomPage>();

            return builder.Build();
        }

        public static string GetDatabasePath()
        {
            var databasePath = "";
            var databaseName = "ghanawoods.db";

            //if (DeviceInfo.Platform == DevicePlatform.Android)
            //{
            //    databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseName);
            //}
            //if (DeviceInfo.Platform == DevicePlatform.iOS)
            //{
            //    SQLitePCL.Batteries_V2.Init();
            //    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", databaseName); ;
            //}
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            databasePath = System.IO.Path.Join(path, databaseName);

            return databasePath;

        }

    }
}
