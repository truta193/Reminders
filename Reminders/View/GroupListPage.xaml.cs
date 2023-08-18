using Reminders.ViewModel;

namespace Reminders.View;

public partial class GroupListPage : ContentPage
{
	public GroupListPage(GroupListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}