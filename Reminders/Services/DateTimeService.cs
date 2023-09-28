using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminders.Services;

class DateTimeService
{



    public DateTimeService()
    {
    }

    public int GetFirstDayOfTheMonthSundayOffset(int year, int month)
    {
        DateTime date = new DateTime(year, month, 1);

        return (int)date.DayOfWeek;
    }

    public int GetFirstDayOfTheMonthMondayOffset(int year, int month)
    {
        DateTime date = new DateTime(year, month, 1);

        return ((int)date.DayOfWeek + 6) % 7;
    }

    public int GetFirstDayOfTheMonthCustomOffset(int year, int month, int sundayOffset)
    {
        DateTime date = new DateTime(year, month, 1);

        return ((int)date.DayOfWeek + 7 - (sundayOffset % 7)) % 7;
    }

    
}