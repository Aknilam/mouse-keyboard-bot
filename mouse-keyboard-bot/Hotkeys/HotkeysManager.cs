using mouse_keyboard_bot.HostActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mouse_keyboard_bot.Hotkeys
{
    public class HotkeysManager
    {
        KeyboardHook hook = new KeyboardHook();

        public HotkeysManager()
        {
            hook.KeyPressed += hook_KeyPressed;

            // TODO
            hook.RegisterHotKey(ModifierKeys.Control | ModifierKeys.Alt, Keys.F12);
        }

        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
        }
    }
}
