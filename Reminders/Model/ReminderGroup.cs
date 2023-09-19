using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.VisualBasic;

namespace Reminders.Model;


public class ReminderGroup
{
    [XmlArray]
    public ObservableCollection<Reminder> Reminders { get; set; } = new();

    [XmlAttribute]
    public string Title { get; set; } = "Placeholder";
    [XmlAttribute]
    public String MainColor { get; set; } = "#000000";

    public ReminderGroup() { }

    public ReminderGroup(string title,  Color mainColor)
    {
        this.Title= title;
        this.MainColor = mainColor.ToHex();
    }

    public void Add(Reminder reminder)
    {
        
        this.Reminders.Add(reminder);
    }
}
