using mouse_keyboard_bot.HostActions;
using System;
using System.Windows.Forms;

namespace mouse_keyboard_bot.Model
{
    [Serializable]
    public class Hotkey
    {
        public int Id { get; internal set; }

        public Hotkey(int id)
        {
            Id = id;
        }

        ModifierKeys Modifiers { get; set; }
        Keys Keys { get; set; }
    }
}