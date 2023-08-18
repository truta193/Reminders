using Reminders.ViewModel;

namespace Reminders.View;

public partial class ReminderDetailsPage : ContentPage
{
	public ReminderDetailsPage(ReminderDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}