using IeuanWalker.Maui.Switch.Events;
using IeuanWalker.Maui.Switch.Helpers;
using IeuanWalker.Maui.Switch;
using System.Windows.Input;
using Reminders.ViewModel;

namespace Reminders.View;

public partial class NewReminderDetailsPage : ContentPage
{
	public NewReminderDetailsPage(NewReminderDetailsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    public event EventHandler<ToggledEventArgs>? Toggled = null;

    public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(NewReminderDetailsPage), false, BindingMode.TwoWay);
    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }

    public static readonly BindableProperty AccessibilityHintProperty = BindableProperty.Create(nameof(AccessibilityHint), typeof(string), typeof(NewReminderDetailsPage), string.Empty);

    public string AccessibilityHint
    {
        get => (string)GetValue(AccessibilityHintProperty);
        set => SetValue(AccessibilityHintProperty, value);
    }

    public static readonly BindableProperty ToggledCommandProperty = BindableProperty.Create(nameof(ToggledCommand), typeof(ICommand), typeof(NewReminderDetailsPage));

    public ICommand ToggledCommand
    {
        get => (ICommand)GetValue(ToggledCommandProperty);
        set => SetValue(ToggledCommandProperty, value);
    }

    static void CustomSwitch_SwitchPanUpdate(CustomSwitch customSwitch, SwitchPanUpdatedEventArgs e)
    {
        //Color Animation
        /*Color fromColor = e.IsToggled ? Color.FromArgb("#4ACC64") : Color.FromArgb("#EBECEC");
        Color toColor = e.IsToggled ? Color.FromArgb("#EBECEC") : Color.FromArgb("#4ACC64");*/
        Color fromColor = e.IsToggled ? Color.FromArgb("#4ACC64") : Color.FromArgb("#EBECEC");
        Color toColor = e.IsToggled ? Color.FromArgb("#EBECEC") : Color.FromArgb("#4ACC64");

        double t = e.Percentage * 0.01;

        customSwitch.BackgroundColor = ColorAnimationUtil.ColorAnimation(fromColor, toColor, t);
    }

    void CustomSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        Toggled?.Invoke(sender, e);
    }
}