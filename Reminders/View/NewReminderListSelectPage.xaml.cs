using Reminders.ViewModel;

namespace Reminders.View;

public partial class NewReminderListSelectPage : ContentPage
{
	public NewReminderListSelectPage(NewReminderListSelectViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}