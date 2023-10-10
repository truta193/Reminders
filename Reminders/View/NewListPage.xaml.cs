using Reminders.ViewModel;

namespace Reminders.View;

public partial class NewListPage : ContentPage
{
	public NewListPage(NewListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}