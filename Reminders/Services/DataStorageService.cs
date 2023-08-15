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
    private XmlSerializer serializerCollection;
    private string fileName = "reminders.xml";
    private string appDirectory = FileSystem.Current.AppDataDirectory;
    private string filePath;
    public DataStorageService()
    {
        serializer = new XmlSerializer(typeof(Reminder));
        serializerCollection = new XmlSerializer(typeof(ReminderCollection));
        filePath = Path.Combine(appDirectory, fileName);
    }

    public void DataXmlSerialize(Reminder reminder)
    {
        if (reminder == null)
        {
            return;
        }

        FileStream fs = File.OpenWrite(filePath);
        if (fs == null)
        {
            return;
        }

        serializer.Serialize(fs, reminder);
        fs.Close();
    }

    public Reminder DataXmlDeserialize()
    {
        if (!File.Exists(filePath)) 
        {
            return null;
        }

        FileStream fs = File.OpenRead(filePath);
        if (fs == null)
        {
            return null;
        }

        Reminder reminder = (Reminder)serializer.Deserialize(fs);

        fs.Close();
        return reminder;
    }

    public void DataXmlSerializeCollection(ReminderCollection lists)
    {
        if (lists == null)
        {
            return;
        }

        FileStream fs = File.OpenWrite(filePath);
        if (fs == null)
        {
            return;
        }

        serializerCollection.Serialize(fs, lists);
        fs.Close();
    }

    public ReminderCollection DataXmlDeserializeCollection()
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        FileStream fs = File.OpenRead(filePath);
        if (fs == null)
        {
            return null;
        }

        ReminderCollection reminder = (ReminderCollection)serializerCollection.Deserialize(fs);

        fs.Close();
        return reminder;
    }

    /*
    public void DataXmlSerialize(Reminder reminder)
    {
        FileStream fs = File.OpenWrite("../../../ser.txt");
        serializer.Serialize(fs, reminder);
    }

    public Reminder DataXmlDeserialize()
    {
        FileStream fs = File.OpenRead("../../../ser.txt");
        if (fs == null)
            return null;

        Reminder reminder = (Reminder)serializer.Deserialize(fs);

        return reminder;
    }

     */

}
