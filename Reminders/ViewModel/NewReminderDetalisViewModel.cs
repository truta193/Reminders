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
    int sundayOffset = 0;
    int offset;
    int selectedMonth;
    int selectedYear;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledDate))]
    CalendarDay selectedDay = null;

    partial void OnSelectedDayChanged(CalendarDay value)
    {
        if (value == null)
        {
            this.SelectedDay = Days[this.offset + (int)DateTime.Now.Day - 1];
        }
    }

    [ObservableProperty]
    ReminderCollection collection;


    [ObservableProperty]
    ObservableCollection<string> hours = new();
    [ObservableProperty]
    ObservableCollection<string> minutes = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledDate))]
    bool dateToggle = false;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledTime))]

    bool timeToggle = false;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledTime))]
    TimeSpan selectedTime = TimeSpan.Zero;

    public DateTime ScheduledDate => DateToggle ? new DateTime(selectedYear, selectedMonth, SelectedDay.Day) : new DateTime();
    public DateTime ScheduledTime => TimeToggle ? new DateTime() + SelectedTime : new DateTime();


    DateTimeService dateService;

    public NewReminderDetalisViewModel(MainViewModel mvm) 
    {
        this.dateService = new();
        this.Collection = mvm.Collection;
        this.sundayOffset = 0;
        GenerateCalendarView((int)DateTime.Now.Year, (int)DateTime.Now.Month);
        DaySelect(Days[this.offset + (int)DateTime.Now.Day - 1]);
        #region 
        for (int i = 0; i < 24; i++)
        {
            if (i < 10)
                hours.Add($"0{i}");
            else
                hours.Add($"{i}");
        }
        for (int i = 0; i < 60;  i++)
        {
            if (i < 10)
                minutes.Add($"0{i}");
            else
                minutes.Add($"{i}");
        }
        #endregion
    }

    [RelayCommand]
    public void DaySelect(CalendarDay calendarDay)
    {
        if (calendarDay.Day == 0) { return; }

        if (SelectedDay == null)
        {
            SelectedDay = calendarDay;
        }
        Debug.WriteLine(calendarDay.BackColor, calendarDay.Day);
        int index = Days.IndexOf(SelectedDay);
        Days[index].BackColor = "#FFFFFF";
        if (SelectedDay.Day == (int)DateTime.Now.Day)
        { 
            Days[index].MainColor = "#007BFF"; 
        }
        else
        {
            Days[index].MainColor = "#000000";

        }
        index = Days.IndexOf(calendarDay);
        Days[index].BackColor = "#007BFF";
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
        selectedYear = year;
        selectedMonth = month;
        this.offset = dateService.GetFirstDayOfTheMonthCustomOffset(selectedYear, selectedMonth, sundayOffset);

        for (int i = 0; i < this.offset; i++)
        {
            CalendarDay d = new();
            d.Day = 0;
            d.DayString = string.Empty;
            Days.Add(d);
        }
        for (int i = 1; i <= DateTime.DaysInMonth(selectedYear, selectedMonth); i++)
        {
            CalendarDay d = new();
            d.Day = i;
            d.DayString = d.Day.ToString();
            Days.Add(d);
        }
        int index = this.offset + (int)DateTime.Now.Day - 1;
        Days[this.offset + (int)DateTime.Now.Day - 1].MainColor = "#007BFF";
        foreach (ReminderGroup list in Collection.Groups)
        {
            foreach (Reminder reminder in list.Reminders)
            {
                Days[(int)reminder.ScheduledAtDate.Day].events.Add(Color.FromArgb(list.MainColor));
            }
        }
    }

}
