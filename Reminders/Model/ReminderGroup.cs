using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.VisualBasic;

namespace Reminders.Model;


public class ReminderGroup
{
    [XmlArray]
    public ObservableCollection<Reminder> Reminders { get; set; } = new();

    [XmlAttribute]
    public string Title { get; set; } = "Placeholder";
    [XmlAttribute]
    public string MainColor { get; set; } = "#000000";
    [XmlAttribute]
    public int IconID { get; set; } = 0;

    public ReminderGroup() { }

    public ReminderGroup(string title,  Color mainColor, int iconID)
    {
        this.Title= title;
        this.MainColor = mainColor.ToHex();
        this.IconID = iconID;
    }

    public void Add(Reminder reminder)
    {
        
        this.Reminders.Add(reminder);
    }
}
