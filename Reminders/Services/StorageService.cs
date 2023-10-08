using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reminders.Model;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Reminders.Services;

public class StorageService
{

    private const string DatabaseFilename = "Reminders.db3";
    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    SQLiteAsyncConnection Database;

    private async Task Init()
    {
        if (Database is not null) return;
        Database = new SQLiteAsyncConnection(DatabasePath, Flags);
        var result = await Database.CreateTableAsync<ReminderListModel>();
        result = await Database.CreateTableAsync<ReminderModel>();

    }

    public async Task AddList(ReminderListModel list)
    {
        await Init();
        await Database.InsertWithChildrenAsync(list, recursive: true);
    }

    public async Task AddReminder(ReminderModel reminder)
    {
        await Init();
        await Database.InsertAsync(reminder);
    }

    public async Task<IEnumerable<ReminderListModel>> GetAllLists()
    {
        await Init();
        var lists = await Database.GetAllWithChildrenAsync<ReminderListModel>(recursive: true);
        return lists;
    }

    public async Task<IEnumerable<ReminderModel>> GetAllReminders()
    {
        await Init();
        var reminders = await Database.Table<ReminderModel>().ToListAsync();
        return reminders;
    }

    //TODO : Implement this method
    public async Task<IEnumerable<ReminderModel>> GetTodayReminders()
    {
        await Init();
        //Query for today reminders
        return null;
    }

    //TODO : Implement this method
    public async Task<IEnumerable<ReminderModel>> GetScheduledReminders()
    {
        await Init();
        //Query for scheduled reminders
        return null;
    }

    public async Task RemoveList(int listId)
    {
        await Init();
        await Database.DeleteAsync<ReminderListModel>(listId);
    }

    public async Task RemoveList(ReminderListModel list)
    {
        await Init();
        await Database.DeleteAsync(list);
    }

    public async Task RemoveReminder(int Id)
    {
        await Init();
        await Database.DeleteAsync<ReminderModel>(Id);
    }

    public async Task RemoveReminder(ReminderModel reminder)
    {
        await Init();
        await Database.DeleteAsync(reminder);
    }

    //TODO : Implement this method
    public async Task RemoveOldReminders()
    {
        await Init();
        //Update database to remove older reminders

    }
    //Update database to remove older reminders

}

