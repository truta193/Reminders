using Reminders.Model;
using Reminders.ViewModel;

namespace Reminders.View;

public partial class NewListPage : ContentPage
{
	ReminderCollection collection;
	public NewListPage(NewListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}