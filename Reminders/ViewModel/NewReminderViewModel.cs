using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.View;

namespace Reminders.ViewModel;

public partial class NewReminderViewModel : ObservableObject
{
    [ObservableProperty]
    ReminderCollection collection;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayListName))]
    [NotifyPropertyChangedFor(nameof(DisplayListColor))]
    public ReminderGroup selectedList = null;

    public string DisplayListName => (SelectedList != null) ? SelectedList.Title : string.Empty;
    public string DisplayListColor => (SelectedList != null) ? SelectedList.MainColor : "#000000";

    public NewReminderViewModel(MainViewModel mvm) 
    {
        this.Collection = mvm.Collection;
        if (mvm.Collection.Groups.Count == 0)
        {
            SelectedList = null;
        } 
        else if (SelectedList == null)
        {     
            SelectedList = mvm.Collection.Groups[0];
            
        }
    }

    [RelayCommand]
    async Task GoToListSelectAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(NewReminderListSelectPage)}", true);
    }

    
}
