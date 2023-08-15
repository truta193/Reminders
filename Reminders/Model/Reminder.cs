using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reminders.Model;
public class Reminder
{
    //TODO: Getters and setters for DateTime stuff
    [XmlAttribute]
    public string Title { get; set; }
    [XmlAttribute]
    public string Description { get; set; }
    //public DateTime CreatedAt { get; set; }
    //public DateTime StartingAt { get; set; }
    //public DateTime EndingAt { get; set; }
    //public bool IsAllDay { get; set; }
    //public bool IsRepeating { get; set; }
    //public int RepeatDayInterval { get; set; }


    public Reminder() 
    { 
    }

    public Reminder(string title, string description)
    {
        this.Title = title;
        this.Description = description;
    }

}

