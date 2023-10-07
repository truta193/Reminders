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
    [ObservableProperty]
    ObservableCollection<string> months = new() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"};
    [ObservableProperty]
    ObservableCollection<int> years = new() { 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028 };
    //TODO Settings file
    int sundayOffset = 0;
    int offset;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(selectedMonthString))]
    [NotifyPropertyChangedFor(nameof(DisplayDate))]

    int selectedMonth = (int)DateTime.Now.Month;

    partial void OnSelectedMonthChanged(int value)
    {
        GenerateCalendarView(SelectedYear, SelectedMonth);
    }

    public string selectedMonthString => dateService.GetMonthName(SelectedMonth);


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(DisplayDate))]
    int selectedYear = DateTime.Now.Year;

    partial void OnSelectedYearChanged(int value)
    {
        GenerateCalendarView(SelectedYear, SelectedMonth);
    }


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
    [NotifyPropertyChangedFor(nameof(myRotation))]
    bool isMYSelectorVisible = false;

    
    public int myRotation => IsMYSelectorVisible ? 90 : 0;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledDate))]
    bool dateToggle = false;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledTime))]
    bool timeToggle = false;
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledTime))]
    TimeSpan selectedTime = TimeSpan.Zero;
    
    public string DisplayDate => dateService.GetMonthName(SelectedMonth) + " " + SelectedYear.ToString();

    public DateTime ScheduledDate => DateToggle ? new DateTime(SelectedYear, SelectedMonth, SelectedDay.Day) : new DateTime();
    public DateTime ScheduledTime => TimeToggle ? new DateTime() + SelectedTime : new DateTime();


    DateTimeService dateService;

    public NewReminderDetalisViewModel(MainViewModel mvm) 
    {
        this.dateService = new();
        this.Collection = mvm.Collection;
        this.sundayOffset = 0;
        GenerateCalendarView(SelectedYear, SelectedMonth);
        DaySelect(Days[this.offset + (int)DateTime.Now.Day - 1]);
      
        for (int i = 0; i < 24; i++)
        {
            Hours.Add(i.ToString("D2"));
        }
        for (int i = 0; i < 60; i++)
        {
            Minutes.Add(i.ToString("D2"));
        }

    }

    [RelayCommand]
    public void DaySelect(CalendarDay calendarDay)
    {
        if (calendarDay.Day == 0) { return; }

        if (SelectedDay == null)
        {
            SelectedDay = calendarDay;
        }
        
        //Selected day will be null if the user selects a day in a different month, write a guard clause to fix this


        int index = Days.IndexOf(SelectedDay);
        if (index < 0)
        {
            foreach (CalendarDay day in Days)
            {
                if (SelectedDay.Day == day.Day)
                {
                    index = Days.IndexOf(day);
                    break;
                }
            }
        }
        if (index < 0) { return; }
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
        this.offset = dateService.GetFirstDayOfTheMonthCustomOffset(SelectedYear, SelectedMonth, sundayOffset);

        for (int i = 0; i < this.offset; i++)
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
        int index = this.offset + (int)DateTime.Now.Day - 1;
        if (SelectedMonth == DateTime.Now.Month && SelectedYear == DateTime.Now.Year)
        {
            Days[this.offset + (int)DateTime.Now.Day - 1].MainColor = "#007BFF";
        }

        foreach (ReminderGroup list in Collection.Groups)
        {
            foreach (Reminder reminder in list.Reminders)
            {
                Days[(int)reminder.ScheduledAtDate.Day].events.Add(Color.FromArgb(list.MainColor));
            }
        }
    }

    [RelayCommand]
    public void NextMonth()
    {
        if (SelectedMonth == 12)
        {
            SelectedMonth = 1;
            SelectedYear++;
        }
        else
        {
            SelectedMonth++;
        }
        
        GenerateCalendarView(SelectedYear, SelectedMonth);
    }

    [RelayCommand]
    public void PreviousMonth()
    {
        //Write a guard clause to check if year is valid
        if (SelectedMonth == 1)
        {
            SelectedMonth = 12;
            SelectedYear--;
        }
        else
        {
            SelectedMonth--;
        }
       
        GenerateCalendarView(SelectedYear, SelectedMonth);
    }

    [RelayCommand]
    public void UpdateCalendarMonthFromPicker(string monthString)
    {
        SelectedMonth = Months.IndexOf(monthString) + 1;
    }

    [RelayCommand]
    public void UpdateCalendarYearFromPicker(int year)
    {
        SelectedYear = year;
    }

    [RelayCommand]
    public void UpdateMYSelectorVisibility()
    {
        IsMYSelectorVisible = !IsMYSelectorVisible;
    }

}
