using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Reminders.Model;

[Table("Lists")]
public class ReminderListModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Color { get; set; } = "#000000";
    public int IconID { get; set; } = 0;

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public ObservableCollection<ReminderModel> Reminders { get; set; } = new();



    public ReminderListModel() { }

#nullable enable
    public ReminderListModel(string Title, Color? Color)
    {
        if (String.IsNullOrEmpty(Title))
        {
            Title = "My List";
        }
        //Check if color is null
        if (Color is null)
        {
            Color = Colors.Black;
        }
        this.Title = Title;
        this.Color = Color.ToArgbHex();
    }

    public ReminderListModel(string Title, Color? Color, int IconID)
    {
        if (String.IsNullOrEmpty(Title))
        {
            Title = "My List";
        }
        if (Color is null)
        {
            Color = Colors.Black;
        }

        this.Title = Title;
        this.Color = Color.ToArgbHex();
        this.IconID = IconID;
    }
#nullable disable
}

