using Reminders.Services;
using Reminders.ViewModel;

namespace Reminders;

public partial class MainPage : ContentPage
{

    public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

