using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Reminders.Model;
using Reminders.Services;

namespace Reminders.ViewModel;

public partial class NewReminderDetalisViewModel : ObservableObject
{
    [ObservableProperty]
    public ObservableCollection<CalendarDay> days;
    //TODO Settings file
    [ObservableProperty]
    int sundayOffset = 0;
    [ObservableProperty]
    int offset;
    [ObservableProperty]
    int selectedMonth;
    [ObservableProperty]
    int selectedYear;
    [ObservableProperty]
    ReminderCollection collection;
    [ObservableProperty]
    CalendarDay selectedDay;

    DateTimeService dateService;

    public NewReminderDetalisViewModel(MainViewModel mvm) 
    {
        this.dateService = new();
        this.Collection = mvm.Collection;
        this.sundayOffset = 0;
        GenerateCalendarView((int)DateTime.Now.Year, (int)DateTime.Now.Month);
    } 

    public void GenerateCalendarView(int year, int month)
    {
        if (Days != null)
        {
            Days.Clear();
        }
        else
        {
            Days = new();
        }
        SelectedYear = year;
        SelectedMonth = month;
        this.Offset = dateService.GetFirstDayOfTheMonthCustomOffset(SelectedYear, SelectedMonth, SundayOffset);

        for (int i = 0; i < this.Offset; i++)
        {
            CalendarDay d = new();
            d.Day = 0;
            d.DayString = string.Empty;
            Days.Add(d);
        }
        for (int i = 1; i <= DateTime.DaysInMonth(SelectedYear, SelectedMonth); i++)
        {
            CalendarDay d = new();
            d.Day = i;
            d.DayString = d.Day.ToString();
            Days.Add(d);
        }
        int index = this.Offset + (int)DateTime.Now.Day - 1;
        Days[this.Offset + (int)DateTime.Now.Day - 1].MainColor = "#007BFF";
        foreach (ReminderGroup list in Collection.Groups)
        {
            foreach (Reminder reminder in list.Reminders)
            {
                Days[(int)reminder.StartingAt.Day].events.Add(Color.FromArgb(list.MainColor));
            }
        }
    }

}
