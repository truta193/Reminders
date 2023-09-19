using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;

namespace Reminders.ViewModel;

[QueryProperty("Collection", "Collection")]
public partial class NewListViewModel : ObservableObject
{
    [ObservableProperty]
    ReminderCollection collection;
    [ObservableProperty]
    Color mainColor;
    [ObservableProperty]
    String name;
    public NewListViewModel()
	{
        MainColor = Color.FromArgb("#F14C3C");
        Name = String.Empty;
    }

    [RelayCommand]
    public void UpdateListColor(RadioButton radioButton)
    {
        MainColor = radioButton.BackgroundColor;
        AddNewList();
    }

    public void AddNewList()
    {
        if (Name == String.Empty)
        {
            int count = 1;
            foreach (ReminderGroup rg in Collection.Groups)
            {
                if (Regex.IsMatch(rg.Title, @"New List*")) count++;
            }
            Name = $"New List {count}";
        }
        //TODO Add color
        ReminderGroup list = new(Name, MainColor);
        Collection.Add(list);
    }

}