using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.Services;
using Reminders.View;

namespace Reminders.ViewModel;


public partial class MainViewModel : ObservableObject
{
    private DataStorageService dataStorageService;
    [ObservableProperty]
    public ReminderCollection collection;

    public MainViewModel(DataStorageService dataStorageService)
    {
        this.dataStorageService = dataStorageService;
        this.Collection = this.dataStorageService.DataRetrieve();
    }

    //For testing
    [RelayCommand]
    public void AppendDummyData()
    {
        ReminderCollection collection = this.dataStorageService.DataRetrieve();
        Reminder r = new Reminder();
        r.Title = "Pills";
        r.Description = "Take pills";
        r.CreatedAt = DateTime.Now;
        r.StartingAt = DateTime.Now;
        r.EndingAt = DateTime.Now.AddHours(2);
        r.IsRepeating = false;
        r.IsAllDay = false;
        collection.Groups[0].Add(r);

        Reminder s = new Reminder();
        s.Title = "Water";
        s.Description = "Hydrate yourself";
        s.CreatedAt = DateTime.Now;
        s.StartingAt = DateTime.Now;
        s.EndingAt = DateTime.Now.AddHours(1);
        s.IsRepeating = false;
        s.IsAllDay = false;
        collection.Groups[1].Add(s);

        this.dataStorageService.DataStore(collection);
        this.Collection = collection;

    }

    [RelayCommand]
    public void SaveData()
    {
        this.dataStorageService.DataStore(Collection);
        Debug.WriteLine("Saved data!");
    }

    [RelayCommand]
    public void LoadData()
    {
        this.Collection = dataStorageService.DataRetrieve();
        Debug.WriteLine("Loaded data!");
    }

    [RelayCommand]
    async Task GoToGroupListAsync(ReminderGroup group)
    {
        if (group is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object>
            {
                { "Group", group }
            });
    }

    /*    [RelayCommand]
        public void GetTodayReminders()
        {
            Debug.WriteLine(DateTime.Today.Date);
            //ObservableCollection<Reminder> reminders = new();
            foreach (ReminderGroup group in this.Collection.Groups)
            {
                foreach (Reminder reminder in group.Reminders)
                {
                    if (reminder.StartingAt.Date.Equals(DateTime.Today.Date))
                    {
                        //reminders.Add(reminder);
                        Debug.WriteLine($"Added {reminder.Title}");
                    }
                }
            }

        }*/
}

