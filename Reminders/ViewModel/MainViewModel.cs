using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.Services;

namespace Reminders.ViewModel;

public partial class MainViewModel : ObservableObject
{
    private StorageService storageService;

    [ObservableProperty]
    public ObservableCollection<ReminderListModel> lists = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TodayRemindersCount))]
    public ObservableCollection<ReminderModel> todayReminders = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledRemindersCount))]
    public ObservableCollection<ReminderModel> scheduledReminders = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AllRemindersCount))]
    public ObservableCollection<ReminderModel> allReminders = new();

    public int AllRemindersCount => AllReminders?.Count ?? 0;
    public int TodayRemindersCount => TodayReminders?.Count ?? 0;
    public int ScheduledRemindersCount => ScheduledReminders?.Count ?? 0;

    public MainViewModel(StorageService storageService)
    {
        this.storageService = storageService;
        RefreshData();
    }

    [RelayCommand]
    public async Task AddDummyData()
    {
        ReminderModel reminder = new("Dummy Reminder Future", "Dummy Reminder Description", 1)
        {
            ScheduledAt = DateTime.Now.AddDays(-1),
            HasDate = true
        };
        await this.storageService.AddReminder(reminder);
        await RefreshData();
    }


    public async Task RefreshData()
    {
        this.Lists = new(await this.storageService.GetAllLists());
        this.AllReminders = new(await this.storageService.GetAllReminders());
        this.TodayReminders = new(await this.storageService.GetTodayReminders());
        this.ScheduledReminders = new(await this.storageService.GetScheduledReminders());
    } 


    [RelayCommand]
    async Task GoToTodayRemindersAsync()
    {
       /* await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object> {
                { "Group", TodayReminders }
            });*/
    }

    [RelayCommand]
    async Task GoToScheduledRemindersAsync()
    {
       /* await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object> {
                { "Group", ScheduledReminders }
            });*/
    }

    [RelayCommand]
    async Task GoToAllRemindersAsync()
    {
       /* await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object> {
                { "Group", AllReminders }
            });*/
    }

    [RelayCommand]
    async Task GoToGroupListAsync(ReminderListModel group)
    {
        /*if (group is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(GroupListPage)}", true,
            new Dictionary<string, object>
            {
                { "Group", group }
            });*/
    }

    [RelayCommand]
    async Task GoToNewListAsync()
    {
        //await Shell.Current.GoToAsync($"{nameof(NewListPage)}", true);
    }

    [RelayCommand]
    async Task GoToNewReminderAsync()
    {
        /*await Shell.Current.GoToAsync($"{nameof(NewReminderPage)}", true, new Dictionary<string, object>
        {
            {"Title", string.Empty },
            {"Description", string.Empty }
        });*/
    }
}



/*    [RelayCommand]
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
    }*/
