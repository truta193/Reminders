using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
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
    [NotifyPropertyChangedFor(nameof(TodayReminders))]
    //This is not really observable when it's elements change (or subelements aka Reminders) so it's a bit of a pain
    public ReminderCollection collection = null;

    [ObservableProperty]
    public ReminderGroup todayReminders = null;
    [ObservableProperty]
    public int todayReminderCount = 0;

    [ObservableProperty]
    public ReminderGroup scheduledReminders = null;
    [ObservableProperty]
    public int scheduledReminderCount = 0;

    [ObservableProperty]
    public ReminderGroup allReminders = null;
    [ObservableProperty]
    public int allReminderCount = 0;

    public MainViewModel(DataStorageService dataStorageService)
    {
        this.dataStorageService = dataStorageService;
        this.Collection = this.dataStorageService.DataRetrieve();
        TodayReminders = new ReminderGroup("Today",  Color.FromArgb("#000000"), 0);
        ScheduledReminders = new ReminderGroup("Scheduled",  Color.FromArgb("#000000"), 0);
        AllReminders = new ReminderGroup("All",  Color.FromArgb("#000000"), 0);
        GetTodayReminders();
        GetScheduledReminders();
        GetAllReminders();
    }

    [RelayCommand]
    public void DeleteDummyData()
    {
        string fileName = "reminders.xml";
        string appDirectory = FileSystem.Current.AppDataDirectory;
        string filePath = Path.Combine(appDirectory, fileName);
        File.Delete(filePath);
        LoadData();
    }

    [RelayCommand]
    public void CreateDummyEmptyFile()
    {
        string fileName = "reminders.xml";
        string appDirectory = FileSystem.Current.AppDataDirectory;
        string filePath = Path.Combine(appDirectory, fileName);
        File.Create(filePath).Close();
        LoadData();
    }

    [RelayCommand]
    public void CreateDummyData()
    {
        string fileName = "reminders.xml";
        string appDirectory = FileSystem.Current.AppDataDirectory;
        string filePath = Path.Combine(appDirectory, fileName);
        FileStream fs = File.OpenWrite(filePath);
        string data = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<ReminderCollection xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Groups>\r\n    <ReminderGroup Title=\"Hellos\">\r\n      <Reminders>\r\n        <Reminder Title=\"Hello\" Description=\"World\" CreatedAt=\"2023-08-15T21:31:52.2708733+03:00\" StartingAt=\"0001-01-01T00:00:00\" EndingAt=\"0001-01-01T00:00:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n        <Reminder Title=\"Hello\" Description=\"Kenobi\" CreatedAt=\"2023-08-15T21:31:52.2711533+03:00\" StartingAt=\"0001-01-01T00:00:00\" EndingAt=\"0001-01-01T00:00:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n        <Reminder Title=\"Hello\" Description=\"Friends\" CreatedAt=\"2023-08-15T21:31:52.2712422+03:00\" StartingAt=\"0001-01-01T00:00:00\" EndingAt=\"0001-01-01T00:00:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n        <Reminder Title=\"Pills\" Description=\"Take pills\" CreatedAt=\"2023-08-18T15:51:28.7799604+03:00\" StartingAt=\"2023-08-18T15:51:28.7799617+03:00\" EndingAt=\"2023-08-18T17:51:28.779963+03:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n      </Reminders>\r\n    </ReminderGroup>\r\n    <ReminderGroup Title=\"Byes\">\r\n      <Reminders>\r\n        <Reminder Title=\"Bye\" Description=\"World\" CreatedAt=\"2023-08-15T21:31:52.2718538+03:00\" StartingAt=\"0001-01-01T00:00:00\" EndingAt=\"0001-01-01T00:00:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n        <Reminder Title=\"Bye\" Description=\"Kenobi\" CreatedAt=\"2023-08-15T21:31:52.2718555+03:00\" StartingAt=\"0001-01-01T00:00:00\" EndingAt=\"0001-01-01T00:00:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n        <Reminder Title=\"Bye\" Description=\"Friends\" CreatedAt=\"2023-08-15T21:31:52.2718606+03:00\" StartingAt=\"0001-01-01T00:00:00\" EndingAt=\"0001-01-01T00:00:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n        <Reminder Title=\"Water\" Description=\"Hydrate yourself\" CreatedAt=\"2023-08-18T15:51:28.7800975+03:00\" StartingAt=\"2023-08-18T15:51:28.7800989+03:00\" EndingAt=\"2023-08-18T16:51:28.7800991+03:00\" IsAllDay=\"false\" IsRepeating=\"false\" RepeatDayInterval=\"0\" />\r\n      </Reminders>\r\n    </ReminderGroup>\r\n  </Groups>\r\n</ReminderCollection>";
        byte[] info = new UTF8Encoding(true).GetBytes(data);
        fs.Write(info, 0, info.Length);
        fs.Close();
        LoadData();
    }
    //For testing
    [RelayCommand]
    public void AppendDummyData()
    {
        ReminderCollection collection = this.dataStorageService.DataRetrieve();
        if (collection == null)
        {
            collection = new();
        }

        /*        Reminder r = new Reminder();
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
                collection.Groups[1].Add(s);*/
        //collection.Groups[1].MainColor = "#F14C3C";
        //collection.Groups[0].MainColor = "#5DC466";
        collection.Groups[0].IconID = 0;
        collection.Groups[1].IconID = 1;

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
    public void GetTodayReminders()
    {
        if (TodayReminders == null)
        {
            return;
        }
        TodayReminders.Reminders.Clear();
        foreach (ReminderGroup group in this.Collection.Groups)
        {
            foreach (Reminder reminder in group.Reminders)
            {
                if (reminder.ScheduledAtDate.Equals(DateTime.Today.Date))
                {
                    TodayReminders.Add(reminder);
                    Debug.WriteLine($"Added {reminder.Title}");
                }
            }
        }

        TodayReminderCount = TodayReminders.Reminders.Count;
    }

    public void GetScheduledReminders()
    {
        if (ScheduledReminders == null)
        {
            return;
        }
        ScheduledReminders.Reminders.Clear();
        foreach (ReminderGroup group in this.Collection.Groups)
        {
            foreach (Reminder reminder in group.Reminders)
            {
                if (reminder.ScheduledAtDate != DateTime.MinValue.Date && reminder.ScheduledAtDate >= DateTime.Now.Date)
                {
                    ScheduledReminders.Add(reminder);
                    Debug.WriteLine($"Added {reminder.Title}");
                }
            }
        }

        ScheduledReminderCount = ScheduledReminders.Reminders.Count;
    }

    [RelayCommand]
    public void GetAllReminders()
    {
        if (AllReminders == null)
        {
            return;
        }
        AllReminders.Reminders.Clear();
        foreach (ReminderGroup group in this.Collection.Groups)
        {
            foreach (Reminder reminder in group.Reminders)
            {
                AllReminders.Add(reminder);
                Debug.WriteLine($"Added {reminder.Title}");
            }
        }

        AllReminderCount = AllReminders.Reminders.Count;
    }

    [RelayCommand]
    async Task GoToTodayRemindersAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object> {
                { "Group", TodayReminders }
            });
    }

    [RelayCommand]
    async Task GoToScheduledRemindersAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object> {
                { "Group", ScheduledReminders }
            });
    }

    [RelayCommand]
    async Task GoToAllRemindersAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object> {
                { "Group", AllReminders }
            });
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

    [RelayCommand]
    async Task GoToNewListAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(NewListPage)}", true);
    }

    [RelayCommand]
    async Task GoToNewReminderAsync()
    {
        await Shell.Current.GoToAsync($"{nameof(NewReminderPage)}", true, new Dictionary<string, object>
        {
            {"Title", string.Empty },
            {"Description", string.Empty }
        });
    }
}

