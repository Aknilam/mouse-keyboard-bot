using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.Model
{
    [Serializable]
    public class Recording
    {
        public int Id { get; set; }

        public long StartTime { get; set; } = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        public List<Details> Events { get; set; } = new List<Details>();
        public string Name { get; set; }

        public void Add(Details details)
        {
            details.Time = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Events.Add(details);
        }

        public Recording(int id)
        {
            this.Id = id;
        }

        //private static string export = "export.bin";

        //public static void Serialize(Recording recording)
        //{
        //    IFormatter formatter = new BinaryFormatter();
        //    Stream stream = new FileStream(export, FileMode.Create, FileAccess.Write, FileShare.None);
        //    formatter.Serialize(stream, recording);
        //    stream.Close();
        //}

        //public static Recording Deserialize()
        //{
        //    string fileName = export;
        //    if (!File.Exists(fileName))
        //        return null;

        //    try
        //    {
        //        IFormatter formatter = new BinaryFormatter();
        //        Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
        //        Recording obj = (Recording)formatter.Deserialize(stream);
        //        stream.Close();
        //        return obj;
        //    } catch { }

        //    return null;
        //}
    }
}
