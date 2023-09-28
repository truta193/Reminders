using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.View;

namespace Reminders.ViewModel;

[QueryProperty("Title", "Title")]
[QueryProperty("Description", "Description")]
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

    [ObservableProperty]
    string title = string.Empty;
    [ObservableProperty]
    string description = string.Empty;

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
    async Task AddNewReminder()
    {
        if (SelectedList == null) { return; }

        //TODO: Might want to do some alerts here in case of error even though a list should always be valid
        int index = Collection.Groups.IndexOf(SelectedList);
        if (index < 0) { return; }


        //TEMPORARY BOOLS
        Reminder newReminder = new(Title, Description, false, false);
        Collection.Groups[index].Add(newReminder);

        //Reset state, since it's a singleton page
        this.Title = string.Empty;
        this.Description = string.Empty;

        await Shell.Current.GoToAsync("../", true);
        
    }

    [RelayCommand]
    async Task GoToListSelectAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(NewReminderListSelectPage)}", true);
    }

    [RelayCommand]
    async Task GoToDetailsAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(NewReminderDetailsPage)}", true);
    }

    
}
