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
        Reminder rem = new Reminder("Hello", "World");
        dataStorageService.DataXmlSerialize(rem);
        Debug.WriteLine(rem);
    }

    [RelayCommand]
    async Task LoadDataAsync()
    {
        Reminder rem = dataStorageService.DataXmlDeserialize();
        Debug.WriteLine(rem.Title + " " + rem.Description);
    }
}

