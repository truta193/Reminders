﻿using System;
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

public partial class NewReminderDetailsViewModel : ObservableObject
{
    private bool isLoadingReminderState = false;

    [ObservableProperty]
    public ObservableCollection<string> months = new() { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
    [ObservableProperty]
    public ObservableCollection<int> years = new() { 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028 };
    //TODO Settings file
    int sundayOffset = 0;
    int offset = 0;

    public ObservableCollection<CalendarDay> Days { get; set; }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(selectedMonthString))]
    [NotifyPropertyChangedFor(nameof(DisplayDate))]
    int selectedMonth = (int)DateTime.Now.Month;

    partial void OnSelectedMonthChanged(int value)
    {
        GenerateCalendarView(SelectedYear, SelectedMonth);
    }

    public string selectedMonthString => dateTimeService.GetMonthName(SelectedMonth);


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
        if (!isLoadingReminderState)
            UpdateReminder();
    }


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

    partial void OnDateToggleChanged(bool oldValue, bool newValue)
    {
        if (!isLoadingReminderState)
            UpdateReminder();
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledTime))]
    bool timeToggle = false;

    partial void OnTimeToggleChanged(bool value)
    {
        if (!isLoadingReminderState)
            UpdateReminder();
    }

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ScheduledTime))]
    TimeSpan selectedTime = TimeSpan.Zero;
    partial void OnSelectedTimeChanged(TimeSpan oldValue, TimeSpan newValue)
    {
        if (!isLoadingReminderState)
            UpdateReminder();
    }

    public string DisplayDate => dateTimeService.GetMonthName(SelectedMonth) + " " + SelectedYear.ToString();

    public DateTime ScheduledDate => DateToggle ? new DateTime(SelectedYear, SelectedMonth, SelectedDay.Day) : new DateTime();
    public DateTime ScheduledTime => TimeToggle ? new DateTime() + SelectedTime : new DateTime();

    ObservableCollection<ReminderListModel> lists = new();

    DateTimeService dateTimeService;
    StorageService storageService;
    NewReminderViewModel nrvm;
    ReminderModel reminderToUpdate;
    public NewReminderDetailsViewModel(NewReminderViewModel nrvm, StorageService storageService)
    {
        this.nrvm = nrvm;
        reminderToUpdate = nrvm.NewReminder;
        this.dateTimeService = new();
        this.storageService = storageService;

        GetLists();
        GenerateCalendarView(SelectedYear, SelectedMonth);

        if (reminderToUpdate.HasDate)
        {
            isLoadingReminderState = true;
            SelectedYear = reminderToUpdate.ScheduledAt.Year;
            SelectedMonth = reminderToUpdate.ScheduledAt.Month;
            SelectedDay = Days[this.offset + (int)reminderToUpdate.ScheduledAt.Day - 1];
            DateToggle = true;
            isLoadingReminderState = false;
        }

        if (reminderToUpdate.HasTime)
        {
            isLoadingReminderState = true;
            SelectedTime = reminderToUpdate.ScheduledAt.TimeOfDay;
            TimeToggle = true;
            isLoadingReminderState = false;
        }


        if (!reminderToUpdate.HasDate)
            DaySelect(Days[this.offset + (int)DateTime.Now.Day - 1]);
        else
            DaySelect(Days[this.offset + (int)reminderToUpdate.ScheduledAt.Day - 1]);




    }

    public async Task GetLists()
    {
        lists = new(await this.storageService.GetAllLists());

    }

    private void UpdateReminder()
    {
        reminderToUpdate.HasDate = DateToggle;
        reminderToUpdate.HasTime = TimeToggle;
        reminderToUpdate.ScheduledAt = new DateTime(ScheduledDate.Year, ScheduledDate.Month, ScheduledDate.Day) + ScheduledTime.TimeOfDay;
        reminderToUpdate.IsAllDay = !TimeToggle;
        reminderToUpdate.IsRepeating = false;
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
        //If the selected day is not in the current month, find it
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
        this.offset = dateTimeService.GetFirstDayOfTheMonthCustomOffset(SelectedYear, SelectedMonth, sundayOffset);

        //Add empty days to the start of the month
        for (int i = 0; i < this.offset; i++)
        {
            CalendarDay d = new();
            d.Day = 0;
            d.DayString = string.Empty;
            Days.Add(d);
        }
        //Add days to the month
        for (int i = 1; i <= DateTime.DaysInMonth(SelectedYear, SelectedMonth); i++)
        {
            CalendarDay d = new();
            d.Day = i;
            d.DayString = d.Day.ToString();
            Days.Add(d);
        }
        //Add empty days to the end of the month
        int index = this.offset + (int)DateTime.Now.Day - 1;
        if (SelectedMonth == DateTime.Now.Month && SelectedYear == DateTime.Now.Year)
        {
            Days[this.offset + (int)DateTime.Now.Day - 1].MainColor = "#007BFF";
        }

        //Add events to the calendar
        foreach (ReminderListModel list in lists)
        {
            foreach (ReminderModel reminder in list.Reminders)
            {
                if (reminder.ScheduledAt.Month != SelectedMonth || reminder.ScheduledAt.Year != SelectedYear) { continue; }
                if (reminder.HasDate)
                {
                    Days[(int)reminder.ScheduledAt.Day].events.Add(Color.FromArgb(list.Color));
                }
                
            }
        }
    }

    [RelayCommand]
    public void NextMonth()
    {
        if (SelectedYear == Years[Years.Count - 1] && SelectedMonth == 12) { return; }

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
        if (SelectedYear == Years[0] && SelectedMonth == 1) { return; }

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
