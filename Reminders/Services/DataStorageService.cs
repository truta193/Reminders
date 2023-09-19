using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Reminders.Model;
namespace Reminders.Services;

//TODO: Storage access plist iOS?
public class DataStorageService
{
    private XmlSerializer serializer;
    private string fileName = "reminders.xml";
    private string appDirectory = FileSystem.Current.AppDataDirectory;
    private string filePath;
                                              
    public DataStorageService()
    {
        
        filePath = Path.Combine(appDirectory, fileName);
        serializer = new XmlSerializer(typeof(ReminderCollection));
        
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

        ReminderCollection reminder = new();

        try
        {
            reminder = (ReminderCollection) serializer.Deserialize(fs);
        }
        catch (InvalidOperationException)
        {
            //XML broken, delete for now
            File.Delete(filePath);
            Shell.Current.DisplayAlert("I/O Error", "XML file is broken!", "OK");
        }
            
        fs.Close();
        return reminder;
    }

    //TODO: If it overwrites a shorter list stuff will linger from the old xml and break it
    //      Empty it's contents to a backup before emptying it and writing
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
