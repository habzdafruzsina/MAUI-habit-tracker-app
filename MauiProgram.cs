using Microsoft.Extensions.Logging;

namespace beadando
{
    public static class MauiProgram
    {
        public static MauiApp AppInstance { get; private set; }

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<HabitsDataSource>();

            builder.Services.AddTransient<HabitFormViewModel>();
            builder.Services.AddTransient<HabitsListViewModel>();
            builder.Services.AddTransient<TodayHabitViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            AppInstance = builder.Build();

            return AppInstance;
        }
    }
}
