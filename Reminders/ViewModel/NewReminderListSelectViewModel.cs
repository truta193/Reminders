using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;

namespace Reminders.ViewModel;

public partial class NewReminderListSelectViewModel : ObservableObject
{
    [ObservableProperty]
    ReminderCollection collection;
    [ObservableProperty]
    NewReminderViewModel nrvm;
    public NewReminderListSelectViewModel(MainViewModel mvm, NewReminderViewModel nrvm) 
    {
        this.Collection = mvm.Collection;
        this.Nrvm = nrvm;
    }

    [RelayCommand]
    public void OnClickedSelection(ReminderGroup list)
    {
        Nrvm.SelectedList = list;
        Shell.Current.GoToAsync("../", true);
    }
}
