using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Reminders.Model;
public class Reminder
{
    [XmlAttribute]
    public string Title { get; set; }
    [XmlAttribute]
    public string Description { get; set; }
    [XmlAttribute]
    public DateTime CreatedAt { get; set; }
    [XmlAttribute]
    public DateTime StartingAt { get; set; }
    [XmlAttribute]
    public DateTime EndingAt { get; set; }
    [XmlAttribute]
    public bool IsAllDay { get; set; }
    [XmlAttribute]
    public bool IsRepeating { get; set; }
    [XmlAttribute]
    public int RepeatDayInterval { 
        get 
        { 
            if (this.IsRepeating == false) 
                return 0; 
            else 
                return RepeatDayInterval;
        } 
        set { } 
    }

    public Reminder() { }

    public Reminder(string title, string description, bool isAllDay, bool isRepeating)
    {
        this.Title = title;
        this.Description = description;
        this.CreatedAt = DateTime.Now;
        this.IsAllDay = isAllDay;
        this.IsRepeating = isRepeating;

    }

}

