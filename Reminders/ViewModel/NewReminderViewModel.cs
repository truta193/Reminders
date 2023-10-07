using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

    [ObservableProperty]
    string title;
    [ObservableProperty]
    string description;
    [ObservableProperty]
    DateTime scheduledDate = DateTime.MinValue;
    [ObservableProperty]
    DateTime scheduledTime = DateTime.MinValue;
    [ObservableProperty]
    bool hasTime = false;
    [ObservableProperty]
    bool hasDate = false;
    [ObservableProperty]
    Reminder newReminder = new();
    [ObservableProperty]
    NewReminderDetalisViewModel nrdvm;
    [ObservableProperty]
    MainViewModel mvm;

    public NewReminderViewModel(MainViewModel mvm, NewReminderDetalisViewModel nrdvm) 
    {
        this.Collection = mvm.Collection;
        this.Mvm = mvm;
        this.Nrdvm = nrdvm;
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
    public async Task AddNewReminder()
    {
        if (SelectedList == null) { return; }

        //TODO: Might want to do some alerts here in case of error even though a list should always be valid
        int index = Collection.Groups.IndexOf(SelectedList);
        if (index < 0) { return; }


        //TEMPORARY BOOLS
        Reminder newReminder = new();
        newReminder.Title = Title; 
        newReminder.Description = Description;
        newReminder.CreatedAt = DateTime.Now;
        newReminder.ScheduledAtTime = this.Nrdvm.TimeToggle ? this.Nrdvm.ScheduledTime : DateTime.MinValue;
        newReminder.ScheduledAtDate =  this.Nrdvm.DateToggle ? this.Nrdvm.ScheduledDate : DateTime.MinValue;

        Debug.WriteLine($"{newReminder.Title}, {newReminder.Description}, {newReminder.CreatedAt}, {newReminder.ScheduledAtDate}, {newReminder.ScheduledAtTime}");        //TODO repeating
        newReminder.IsRepeating = false;

        Collection.Groups[index].Add(newReminder);

        //Reset state, since it's a singleton page
        this.Title = string.Empty;
        this.Description = string.Empty;
        this.Nrdvm.DateToggle = false;
        this.Nrdvm.TimeToggle = false;
        this.Nrdvm.SelectedTime = TimeSpan.Zero;
        this.Nrdvm.SelectedDay = null;

        this.Mvm.GetTodayReminders();
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
