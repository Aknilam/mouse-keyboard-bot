using mouse_keyboard_bot.Model;
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

namespace mouse_keyboard_bot.Recorder.View
{
    public delegate void EmptyEvent();

    public partial class Recordings : Window
    {
        public Recordings()
        {
            InitializeComponent();

            this.Deactivated += Recordings_Deactivated;
        }

        public void AttachToSubscribe(Subscribe subscribe)
        {
            subscribe.MouseClickAt += Subscribe_MouseClickAt;
            subscribe.MouseDown += Subscribe_MouseDown;
            subscribe.MouseUp += Subscribe_MouseUp;

            subscribe.KeyPress += Subscribe_KeyboardKey;
            subscribe.KeyDown += Subscribe_KeyDown;
            subscribe.KeyUp += Subscribe_KeyUp;
        }

        private void Subscribe_MouseUp(MouseDetails mouseDetails)
        {
            Append("mouse up: " + mouseDetails.ToString());
        }

        private void Subscribe_MouseDown(MouseDetails mouseDetails)
        {
            Append("mouse down: " + mouseDetails.ToString());
        }

        private void Subscribe_KeyUp(Keys key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            Append("key up: " + key + " " + code.ToString());
        }

        private void Subscribe_KeyDown(Keys key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            Append("key down: " + key + " " + code.ToString());
        }

        private void Subscribe_MouseClickAt(MouseDetails mouseDetails)
        {
            Append("mouse click: " + mouseDetails.ToString());
        }

        private void Subscribe_KeyboardKey(char key, Gma.System.MouseKeyHook.VirtualKeyCode code)
        {
            Append("key press: " + key + " " + code.ToString());
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

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //mouse_keyboard_bot.MouseMove.SetCursorPos(mouseDetails.X, mouseDetails.Y);
            //mouse_keyboard_bot.HostActions.MouseMove.LeftMouseClick(172, 130);
        }


        public event EmptyEvent Start;
        public event EmptyEvent End;
        public event EmptyEvent ReplyStart;
        public event EmptyEvent ReplyEnd;

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Start?.Invoke();
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            End?.Invoke();
        }

        private void Reply_Start_Click(object sender, RoutedEventArgs e)
        {
            ReplyStart?.Invoke();
        }

        private void Reply_End_Click(object sender, RoutedEventArgs e)
        {
            ReplyEnd?.Invoke();
        }
    }
}
