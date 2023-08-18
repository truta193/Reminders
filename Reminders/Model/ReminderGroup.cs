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

    public ReminderGroup() { }

    public ReminderGroup(string title) 
    { 
        this.Title = title;
    }

    public void Add(Reminder reminder)
    {
        
        this.Reminders.Add(reminder);
    }
}
