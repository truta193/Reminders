using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reminders.Model;

public class ReminderCollection
{
    [XmlArray]
    public ObservableCollection<ReminderGroup> Lists { get; set; } = new();

}
