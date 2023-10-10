using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Reminders.Model;
using Reminders.Services;

namespace Reminders.ViewModel;
//TODO There's a delay when opening the page
//TODO Add a way to change the icon

public partial class NewListViewModel : ObservableObject
{
    [ObservableProperty]
    Color color;
    [ObservableProperty]
    int iconId;
    [ObservableProperty]
    string name;

    MainViewModel mvm;
    StorageService storageService;

    public ObservableCollection<Color> ColorOptions { get; } = new()
    {
        Color.FromArgb("#F14C3C"),
        Color.FromArgb("#FFA033"),
        Color.FromArgb("#F7CE45"),
        Color.FromArgb("#5DC466"),
        Color.FromArgb("#0C79FE"),
        Color.FromArgb("#B67AD5"),
        Color.FromArgb("#998667")
    };

    public ObservableCollection<RadioColorButtonModel> RadioButtons { get; set; } = new();

    public NewListViewModel(MainViewModel mvm, StorageService storageService)
    {
        this.mvm = mvm;
        this.storageService = storageService;
        IconId = 0;
        Color = ColorOptions.FirstOrDefault();
        Name = String.Empty;

        foreach (Color color in ColorOptions)
        {
            RadioButtons.Add(new RadioColorButtonModel() { Color = color, IsChecked = false});
        }
        RadioButtons.FirstOrDefault().IsChecked = true;

    }

    [RelayCommand]
    public void UpdateListColor(RadioButton radioButton)
    {
        this.Color = radioButton.BackgroundColor;
    }

    [RelayCommand]
    public async Task AddNewList()
    {
        if (Name == String.Empty)
        {
            int count = 1 + await this.storageService.GetListOrderedDuplicateCount("New List");
            Name = $"New List {count}";
        }

        ReminderListModel list = new(Name, Color, IconId);
        await this.storageService.AddList(list);
        await mvm.RefreshData();
        await Shell.Current.GoToAsync("../", true);
    }

}