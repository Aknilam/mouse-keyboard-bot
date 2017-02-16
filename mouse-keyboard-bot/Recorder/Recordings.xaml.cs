using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mouse_keyboard_bot.Recorder
{
    public partial class Recordings : Window
    {
        private Subscribe subscribe;

        public Recordings()
        {
            InitializeComponent();

            this.Deactivated += Recordings_Deactivated;
        }

        public Recordings(Subscribe subscribe) : this()
        {
            this.subscribe = subscribe;

            AttachSubscribe();
        }

        private void AttachSubscribe()
        {
            subscribe.MouseClickAt += Subscribe_MouseClickAt;
            subscribe.KeyboardKey += Subscribe_KeyboardKey; ;
        }

        MouseDetails mouseDetails;
        private void Subscribe_MouseClickAt(MouseButtons button, int px, int py)
        {
            Append("mouse: " + button + " " + px + " " + py);
        }

        private void Subscribe_KeyboardKey(char key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            Append("keyboard: " + key + " " + code.ToString());
        }

        private void Recordings_Deactivated(object sender, EventArgs e)
        {
            this.Topmost = true;
        }

        public void Append(string text)
        {
            logArea.AppendText(text + "\n");
            logArea.ScrollToEnd();
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            //mouse_keyboard_bot.MouseMove.SetCursorPos(mouseDetails.X, mouseDetails.Y);
            mouse_keyboard_bot.HostActions.MouseMove.LeftMouseClick(172, 130);
        }
    }
}
