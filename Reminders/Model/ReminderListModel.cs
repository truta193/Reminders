using System.Collections.ObjectModel;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Reminders.Model;

[Table("Lists")]
public class ReminderListModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string HexColor { get; set; } = String.Empty;
    public int IconID { get; set; } = 0;

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public ObservableCollection<ReminderModel> Reminders { get; set; } = new();



    public ReminderListModel() { }

    public ReminderListModel(string Title, string HexColor)
    {
        this.Title = Title;
        this.HexColor = HexColor;
    }

    public ReminderListModel(string Title, string HexColor, int IconID)
    {
        this.Title = Title;
        this.HexColor = HexColor;
        this.IconID = IconID;
    }

}

