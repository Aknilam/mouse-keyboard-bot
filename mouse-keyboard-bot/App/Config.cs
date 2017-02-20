using mouse_keyboard_bot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace mouse_keyboard_bot.App
{
    class ConfigProperty<T>
    {
        string saveName;
        T defaultValue;
        T value;
        Action setterInfo;
        public ConfigProperty(string saveName, T defaultValue, Action setterInfo = null)
        {
            this.saveName = saveName;
            this.defaultValue = defaultValue;
            this.setterInfo = setterInfo;
        }

        public T Value
        {
            get
            {
                Config.init();
                return GetValue();
            }
            set
            {
                if (this.value == null || !this.value.Equals(value))
                {
                    this.value = value;
                    setterInfo?.Invoke();
                }
            }
        }

        private T GetValue()
        {
            if (typeof(T) == typeof(bool))
            {
                return value;
            }
            else if (typeof(T) == typeof(int))
            {
                if ((int)(object)value <= 0)
                {
                    return defaultValue;
                }
                else
                {
                    return value;
                }
            }
            else
            {
                if ((string)(object)value == "" || value == null)
                {
                    return defaultValue;
                }
                else
                {
                    return value;
                }
            }
        }

        public string StringValue
        {
            get
            {
                return Value.ToString();
            }
        }

        public bool DetectMe(string key, string value)
        {
            if (key == saveName)
            {
                if (typeof(T) == typeof(bool))
                {
                    bool res;
                    if (bool.TryParse(value, out res))
                    {
                        this.value = (T)(object)res;
                        return true;
                    }
                }
                else if (typeof(T) == typeof(int))
                {
                    int res;
                    if (int.TryParse(value, out res))
                    {
                        this.value = (T)(object)res;
                        return true;
                    }
                }
                else
                {
                    this.value = (T)(object)value;
                    return true;
                }
            }
            return false;
        }

        public override string ToString()
        {
            return saveName + "=" + Value;
        }

        public void save(StreamWriter sw)
        {
            sw.WriteLine(this.ToString());
        }
    }

    class Config
    {
        static Config()
        {
            OpenFormsAtInit = new ConfigProperty<bool>("open-forms", true);

            boolProps = new List<ConfigProperty<bool>>
            {
                OpenFormsAtInit
            };
            
            SkipLast = new ConfigProperty<int>("skip-last", 1);

            intProps = new List<ConfigProperty<int>>
            {
                SkipLast
            };

            SaveFile = new ConfigProperty<string>("config-file", "sth.config");

            stringProps = new List<ConfigProperty<string>>
            {
                SaveFile
            };
        }

        private static string fileUrl = "init.ini";

        private static bool inited = false;

        private static List<ConfigProperty<bool>> boolProps;
        private static List<ConfigProperty<int>> intProps;
        private static List<ConfigProperty<string>> stringProps;

        public static ConfigProperty<bool> OpenFormsAtInit;

        public static ConfigProperty<int> SkipLast;

        public static ConfigProperty<string> SaveFile;
        
        public static void init()
        {
            if (inited)
            {
                return;
            }
            else
            {
                inited = true;
                initFromFile();
            }
        }

        private static void initFromFile()
        {
            if (File.Exists(fileUrl))
            {
                StreamReader sr = new StreamReader(fileUrl);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    handleLine(line);
                }
                sr.Close();
            }
        }
        private static void handleLine(string line)
        {
            string[] vals = line.Split('=');
            if (vals.Length == 2)
            {
                string key = vals[0].Trim();
                string value = vals[1].Trim();
                if (value == "") return;

                boolProps.ForEach(p => p.DetectMe(key, value));
                intProps.ForEach(p => p.DetectMe(key, value));
                stringProps.ForEach(p => p.DetectMe(key, value));
            }
        }
        public static void save()
        {
            StreamWriter sw = new StreamWriter(fileUrl);
            boolProps.ForEach(p => p.save(sw));
            intProps.ForEach(p => p.save(sw));
            stringProps.ForEach(p => p.save(sw));
            
            sw.Close();
        }
    }
}
