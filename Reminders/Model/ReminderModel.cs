using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Reminders.Model;

[Table("Reminders")]
public class ReminderModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    public DateTime ScheduledAt { get; set; } = DateTime.MinValue;
    public bool HasDate { get; set; } = false;
    public bool HasTime { get; set; } = false;
    public bool IsAllDay { get; set; } = false;
    public bool IsRepeating { get; set; } = false;
    public int RepeatDayInterval { get; set; } = 0;

    [ForeignKey(typeof(ReminderListModel))]
    public int ListId { get; set; }

    public ReminderModel() { }

    public ReminderModel(string Title, string Description, int parentListId)
    {
        this.Title = Title;
        this.Description = Description;
        this.ListId = parentListId;
        this.CreatedAt = DateTime.Now;
    }

    public ReminderModel(string Title, string Description, ReminderListModel parent)
    {
        this.Title = Title;
        this.Description = Description;
        this.ListId = parent.Id;
        this.CreatedAt = DateTime.Now;
    }

}