using Reminders.View;

namespace Reminders;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(GroupListPage), typeof(GroupListPage));
        Routing.RegisterRoute(nameof(NewListPage), typeof(NewListPage));
		Routing.RegisterRoute(nameof(NewReminderPage), typeof(NewReminderPage));
		Routing.RegisterRoute(nameof(NewReminderListSelectPage), typeof(NewReminderListSelectPage));
    }
}
