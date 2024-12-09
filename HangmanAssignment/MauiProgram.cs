using Microsoft.Extensions.Logging;
using HangmanAssignment.ViewModel;
using CommunityToolkit.Maui;

namespace HangmanAssignment
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddTransient<HangmanGamePage>();
            builder.Services.AddTransient<HangmanViewModel>();

#endif

            return builder.Build();
        }
    }
}
