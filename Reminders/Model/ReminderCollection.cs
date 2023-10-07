using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Reminders.Model;

public class ReminderCollection
{
    [XmlArray]
    public ObservableCollection<ReminderGroup> Groups { get; set; } = new();
    public ReminderCollection() { }

    public void Add(ReminderGroup group)
    {
        this.Groups.Add(group);
    }
}
