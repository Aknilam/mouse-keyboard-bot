using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.Model
{
    public class KeyboardDetails : Details
    {
        public VirtualKeyCode Code { get; set; }

        public KeyEventType EventType { get; set; }

        public override DetailsType Type { get { return DetailsType.Keyboard; } }
    }

    public enum KeyEventType
    {
        Down,
        Up,
        Press
    }
}
