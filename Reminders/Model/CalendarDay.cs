using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Reminders.Model;

public partial class CalendarDay : ObservableObject
{
    public ObservableCollection<Color> events { get; set; } = new();
    public int Day { get; set; }
    public string DayString { get; set; }
    [ObservableProperty]
    public string mainColor = "#000000";
    [ObservableProperty]
    public string backgColor  = "#FFFFFF";
    public CalendarDay() { }
}
