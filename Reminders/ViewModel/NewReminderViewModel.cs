using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Reminders.Model;

namespace Reminders.ViewModel;

[QueryProperty("Collection", "Collection")]
public partial class NewReminderViewModel : ObservableObject
{
    [ObservableProperty]
    ReminderCollection collection;
    public NewReminderViewModel() { }
}
