using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.Model.Factories
{
    public class ModelFactory
    {
        AppStorage appStorage;

        public ModelFactory(AppStorage appStorage)
        {
            this.appStorage = appStorage;
        }

        public Hotkey CreateHotkey()
        {
            return new Hotkey(appStorage.HotkeyId++);
        }

        public Recording CreateRecording()
        {
            return new Recording(appStorage.HotkeyId++);
        }
    }
}
