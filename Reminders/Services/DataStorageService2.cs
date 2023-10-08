using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reminders.Model;
using SQLite;

namespace Reminders.Services;

public class DataStorageService2
{
    private const string DatabaseFilename = "Reminders.db3";    
    public const SQLite.SQLiteOpenFlags Flags =
        SQLite.SQLiteOpenFlags.ReadWrite |
        SQLite.SQLiteOpenFlags.Create |
        SQLite.SQLiteOpenFlags.SharedCache;
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    SQLiteAsyncConnection Database;

    public DataStorageService2() { }

    public async Task Init()
    {
        if (Database is not null) return;
        Database = new SQLiteAsyncConnection(DatabasePath, Flags);
        var result = await Database.CreateTableAsync<ReminderGroup>();
    }


}
