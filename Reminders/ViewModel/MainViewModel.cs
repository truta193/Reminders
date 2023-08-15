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
        Reminder rem2 = new Reminder("Hello", "Ben");
        Reminder rem3 = new Reminder("You shall be called", "World");
        ReminderList remList = new ReminderList();
        remList.Add(rem);
        remList.Add(rem2);
        remList.Add(rem3);
        ReminderList remList2 = new ReminderList();
        remList2.Add(rem3);
        remList2.Add(rem2);
        remList2.Add(rem3);
        XmlStructure lists = new XmlStructure();
        lists.Lists.Add(remList);
        lists.Lists.Add(remList2);

        dataStorageService.DataXmlSerializeCollection(lists);
        Debug.WriteLine(rem);
    }

    [RelayCommand]
    async Task LoadDataAsync()
    {
        XmlStructure rem = dataStorageService.DataXmlDeserializeCollection();
        Debug.WriteLine(rem.Lists[0][0].Title + " " + rem.Lists[0][0].Description);
        Debug.WriteLine(rem.Lists[0][1].Title + " " + rem.Lists[0][1].Description);
        Debug.WriteLine(rem.Lists[0][2].Title + " " + rem.Lists[0][2].Description);
        Debug.WriteLine(rem.Lists[1][0].Title + " " + rem.Lists[1][0].Description);
        Debug.WriteLine(rem.Lists[1][1].Title + " " + rem.Lists[1][1].Description);
        Debug.WriteLine(rem.Lists[1][2].Title + " " + rem.Lists[1][2].Description);
    }
}

