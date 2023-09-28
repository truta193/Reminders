using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
#if ANDROID
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif
using Microsoft.Maui.Platform;
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

		//Remove Entry underline
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) => {
#if ANDROID
			h.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#endif
        });
		Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping("EditorCustomization", (handler, view) =>
		{
#if ANDROID
        handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
		});



            var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>().UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<DataStorageService>();
		builder.Services.AddSingleton<StringToColorConverter>();

		builder.Services.AddSingleton<MainViewModel>();
		builder.Services.AddTransient<GroupListViewModel>();
        builder.Services.AddTransient<NewListViewModel>();
		builder.Services.AddSingleton<NewReminderViewModel>();
		builder.Services.AddTransient<NewReminderListSelectViewModel>();


        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddTransient<GroupListPage>();
        builder.Services.AddTransient<NewListPage>();
		builder.Services.AddTransient<NewReminderPage>();
		builder.Services.AddTransient<NewReminderListSelectPage>();


        return builder.Build();
	}
}
