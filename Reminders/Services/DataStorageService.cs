using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Reminders.Model;
namespace Reminders.Services;

public class DataStorageService
{
    private XmlSerializer serializer;
    private string fileName = "reminders.xml";
    private string appDirectory = FileSystem.Current.AppDataDirectory;
    private string filePath;
                                              
    public DataStorageService()
    {
        serializer = new XmlSerializer(typeof(ReminderCollection));
        filePath = Path.Combine(appDirectory, fileName);
    }

    public ReminderCollection DataRetrieve()
    {
        if (!File.Exists(filePath))
        {
            Shell.Current.DisplayAlert("I/O Error", "Necessary files not yet created!", "OK");
            return null;
        }

        FileStream fs = File.OpenRead(filePath);
        if (fs == null)
        {
            Shell.Current.DisplayAlert("I/O Error", "Unable to access necessary files!", "OK");
            return null;
        }

        ReminderCollection reminder = (ReminderCollection)serializer.Deserialize(fs);

        fs.Close();
        return reminder;
    }

    public void DataStore(ReminderCollection collection)
    {
        if (collection == null)
            return;

        FileStream fs = File.OpenWrite(filePath);
        if (fs == null)
        {
            Shell.Current.DisplayAlert("I/O Error", "Unable to access necessary files!", "OK");
            return;
        }

        serializer.Serialize(fs, collection);
        fs.Close();
    }

}
