using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    CalendarDay selectedDay = null;

    DateTimeService dateService;

    public NewReminderDetalisViewModel(MainViewModel mvm) 
    {
        this.dateService = new();
        this.Collection = mvm.Collection;
        this.sundayOffset = 0;
        GenerateCalendarView((int)DateTime.Now.Year, (int)DateTime.Now.Month);
    }

    [RelayCommand]
    public void DaySelect(CalendarDay calendarDay)
    {
        if (calendarDay.Day == 0) { return; }

        if (SelectedDay == null)
        {
            SelectedDay = calendarDay;
        }
        Debug.WriteLine(calendarDay.BackgColor, calendarDay.Day);
        int index = Days.IndexOf(SelectedDay);
        Days[index].BackgColor = "#FFFFFF";
        if (SelectedDay.Day == (int)DateTime.Now.Day)
        { 
            Days[index].MainColor = "#007BFF"; 
        }
        else
        {
            Days[index].MainColor = "#000000";

        }
        index = Days.IndexOf(calendarDay);
        Days[index].BackgColor = "#007BFF";
        Days[index].MainColor = "#FFFFFF";

        SelectedDay = calendarDay;
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
