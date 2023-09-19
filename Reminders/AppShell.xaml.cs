using Reminders.View;

namespace Reminders;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(GroupListPage), typeof(GroupListPage));
		Routing.RegisterRoute(nameof(ReminderDetailsPage), typeof(ReminderDetailsPage));
        Routing.RegisterRoute(nameof(NewListPage), typeof(NewListPage));

    }
}
