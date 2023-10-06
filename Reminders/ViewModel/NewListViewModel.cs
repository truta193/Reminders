using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;

namespace Reminders.ViewModel;
//TODO There's a delay when opening the page
//TODO Save to XML when adding!!!!!!
//TODO Add a way to change the icon

public partial class NewListViewModel : ObservableObject
{
    [ObservableProperty]
    ReminderCollection collection;
    [ObservableProperty]
    Color mainColor;
    [ObservableProperty]
    int iconID;
    [ObservableProperty]
    String name;
    MainViewModel mainViewModel;

    public NewListViewModel(MainViewModel mvm)
	{
        MainColor = Color.FromArgb("#F14C3C");
        Name = String.Empty;
        this.mainViewModel = mvm;
    }

    [RelayCommand]
    public void UpdateListColor(RadioButton radioButton)
    {
        MainColor = radioButton.BackgroundColor;
    }

    [RelayCommand]
    public void AddNewList()
    {
        if (Name == String.Empty)
        {
            int count = 1;
            foreach (ReminderGroup rg in this.mainViewModel.Collection.Groups)
            {
                if (Regex.IsMatch(rg.Title, @"New List*")) count++;
            }
            Name = $"New List {count}";
        }
        
        ReminderGroup list = new(Name, MainColor, IconID);
        this.mainViewModel.Collection.Add(list);
        
        Shell.Current.GoToAsync("../", true);

    }

}