using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Reminders.Model;

namespace Reminders.ViewModel;

[QueryProperty("Reminder", "Reminder")]
public partial class ReminderDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    Reminder reminder;
    public ReminderDetailsViewModel()
    {

    }
}
