using CommunityToolkit.Maui;
using Doc_Historico.Handlers;
using Doc_Historico.Interfaces;
using Doc_Historico.Services;
using Microsoft.Extensions.Logging;

namespace Doc_Historico;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .RegisterAppServices()
            .RegisterViewModels()
			.RegisterViews()
			.UseMauiCommunityToolkit()

            .ConfigureMauiHandlers(handlers =>
			{
				FormHandler.RemoverBorders();	
			})
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddSingleton<ViewModels.PatientListViewModel>();
		return mauiAppBuilder;
	}
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IRequestProvider, RequestProvider>();
        mauiAppBuilder.Services.AddSingleton<IPatient, PatientService>();
        mauiAppBuilder.Services.AddSingleton<IMedicalHistory, MedicalHistoryService>();
		return mauiAppBuilder;
    }
	public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddSingleton<Views.PatientListPage>();
		return mauiAppBuilder;
	}
}
