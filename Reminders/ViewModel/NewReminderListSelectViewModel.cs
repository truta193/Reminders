using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.Services;

namespace Reminders.ViewModel;

public partial class NewReminderListSelectViewModel : ObservableObject
{
    StorageService storageService;
    NewReminderViewModel nrvm;
    [ObservableProperty]
    ObservableCollection<ReminderListModel> lists = new();
    public NewReminderListSelectViewModel(StorageService storageService, NewReminderViewModel nrvm)
    {
        this.storageService = storageService;
        this.nrvm = nrvm;
        LoadLists();
    }

    public async Task LoadLists()
    {
        this.Lists = new(await storageService.GetAllLists());
    }

    [RelayCommand]
    public void OnClickedSelection(ReminderListModel list)
    {
        nrvm.DisplayedList = list;
        Shell.Current.GoToAsync("../", true);
    }
}
