using Reminders.ViewModel;

namespace Reminders.View;

public partial class NewReminderPage : ContentPage
{
	public NewReminderPage(NewReminderViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}