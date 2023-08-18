using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Reminders.Model;
using Reminders.Services;
using Reminders.View;
using Reminders.ViewModel;

namespace Reminders;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{

		//Reminder rem = new Reminder("Hello", "World");
		//DataStorage ds = new DataStorage();
		//ds.DataXmlSerialize(rem);

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<DataStorageService>();

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<GroupListViewModel>();
		builder.Services.AddTransient<ReminderDetailsViewModel>();

		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<GroupListPage>();
		builder.Services.AddTransient<ReminderDetailsPage>();

		return builder.Build();
	}
}
