using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.Services;
using Reminders.View;

namespace Reminders.ViewModel;

public partial class NewReminderViewModel : ObservableObject
{
    /*stuff declared here*/

    MainViewModel mvm;
    StorageService storageService;

    //Test polling DisplayedList.Title/Color directly instead of using the lambdas
    //Leave this, lambdas => safety net for null
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayListName))]
    [NotifyPropertyChangedFor(nameof(DisplayListColor))]
    ReminderListModel displayedList;

    //Gives more control, currently not in use
    public string DisplayListName => DisplayedList?.Title ?? string.Empty;
    public string DisplayListColor => DisplayedList?.Color ?? string.Empty;


    [ObservableProperty]
    ReminderModel newReminder = new();

    ObservableCollection<ReminderListModel> lists;

    public NewReminderViewModel(MainViewModel mvm, StorageService storageService)
    {
        this.mvm = mvm;
        this.storageService = storageService;
        GetFirstList();


    }

    public async Task GetFirstList()
    {
        this.lists = new(await this.storageService.GetAllLists());
        DisplayedList = lists.FirstOrDefault();
    }

    [RelayCommand]
    public async Task AddNewReminder()
    {
        //Or Title is empty OR provide default behavior when title is empty
        if (DisplayedList == null)
        {
            await Shell.Current.DisplayAlert("Error", "No list selected!", "OK");
            return;
        }

        if (string.IsNullOrEmpty(NewReminder.Title))
        {
            await Shell.Current.DisplayAlert("Error", "Title cannot be empty!", "OK");
            return;
        }

        NewReminder.ListId = DisplayedList.Id;
        NewReminder.CreatedAt = DateTime.Now;
        await storageService.AddReminder(NewReminder);
        await this.mvm.RefreshData();
        Debug.WriteLine($"Reminder added {NewReminder.CreatedAt}, {NewReminder.HasDate}, {NewReminder.HasTime}, {NewReminder.ScheduledAt}");
        await Shell.Current.GoToAsync("..", true);
        
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
