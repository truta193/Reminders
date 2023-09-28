using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.Services;
using Reminders.View;

namespace Reminders.ViewModel;

[QueryProperty("Group", "Group")]
public partial class GroupListViewModel : ObservableObject
{
    [ObservableProperty]
    ReminderGroup group;

    public GroupListViewModel() 
    { 
    }

    [RelayCommand]
    public void ToggleRadioButton(RadioButton radioButton)
    {
        radioButton.IsChecked = !radioButton.IsChecked;
    }

}
