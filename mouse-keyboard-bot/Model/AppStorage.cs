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
    public class AppStorage
    {
        public int RecordingId { get; set; } = 0;
        public int HotkeyId { get; set; } = 0;

        public List<Recording> Recordings { get; set; } = new List<Recording>();

        public List<Hotkey> Hotkeys { get; set; } = new List<Hotkey>();

        public List<HotkeyRecordings> Assignings { get; set; } = new List<HotkeyRecordings>();

        public void Serialize()
        {
            Serialize(this);
        }

        private static string export = "exportStorage.bin";

        public static void Serialize(AppStorage recording)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(export, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, recording);
            stream.Close();
        }

        public static AppStorage Deserialize()
        {
            string fileName = export;
            if (!File.Exists(fileName))
                return new AppStorage();

            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                AppStorage obj = (AppStorage)formatter.Deserialize(stream);
                stream.Close();
                return obj;
            }
            catch { }

            return new AppStorage();
        }
    }
}
