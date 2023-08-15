using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.Services;

namespace Reminders.ViewModel;


public partial class MainViewModel : ObservableObject
{
    private DataStorageService dataStorageService;

    public MainViewModel(DataStorageService dataStorageService)
    {
        this.dataStorageService = dataStorageService;
    }

    [RelayCommand]
    async Task SaveDataAsync()
    {
        Reminder rem1 = new Reminder("Hello", "World", false, false);
        Reminder rem2 = new Reminder("Hello", "Kenobi", false, false);
        Reminder rem3 = new Reminder("Hello", "Friends", false, false);
        ReminderGroup remGroup1 = new ReminderGroup("Hellos");
        remGroup1.Add(rem1);
        remGroup1.Add(rem2);
        remGroup1.Add(rem3);
        Reminder rem4 = new Reminder("Bye", "World", false, false);
        Reminder rem5 = new Reminder("Bye", "Kenobi", false, false);
        Reminder rem6 = new Reminder("Bye", "Friends", false, false);
        ReminderGroup remGroup2 = new ReminderGroup("Byes");
        remGroup2.Add(rem4);
        remGroup2.Add(rem5);
        remGroup2.Add(rem6);

        ReminderCollection remCol = new();
        remCol.Add(remGroup1);
        remCol.Add(remGroup2);

        dataStorageService.DataStore(remCol);
        Debug.WriteLine(remCol);
    }

    [RelayCommand]
    async Task LoadDataAsync()
    {
        ReminderCollection remCol = dataStorageService.DataRetrieve();
        Debug.WriteLine(remCol?.Groups[0].Title + " " + remCol?.Groups[1].Title);
    }
}

