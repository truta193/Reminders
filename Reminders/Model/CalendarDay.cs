using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Reminders.Model;

public class CalendarDay
{
    public ObservableCollection<Color> events { get; set; } = new();
    public int Day { get; set; }
    public string DayString { get; set; }
    public string MainColor { get; set; } = "#000000";
    public CalendarDay() { }
}
