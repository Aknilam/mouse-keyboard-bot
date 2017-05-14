using mouse_keyboard_bot.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public delegate void StringEvent(string name);
    //public delegate void RecordingEvent(Recording recording);

    public partial class Recordings : Window
    {
        ObservableCollection<VVV> recordings = new ObservableCollection<VVV>();

        public Recordings()
        {
            InitializeComponent();


            listOfRecordings.ItemsSource = recordings;

            recordings.Add(new VVV("Task1"));
            recordings.Add(new VVV("Task2"));
            recordings.Add(new VVV("Task3"));
            recordings.Add(new VVV("Task4"));
            recordings.Add(new VVV("Task5"));
            recordings.Add(new VVV("Task6"));


            this.Deactivated += Recordings_Deactivated;
        }

        public void AttachToSubscribe(Subscribe subscribe)
        {
            subscribe.MouseClickAt += Subscribe_MouseClickAt;
            subscribe.MouseDown += Subscribe_MouseDown;
            subscribe.MouseUp += Subscribe_MouseUp;

            subscribe.MouseWheel += Subscribe_MouseWheel;

            subscribe.KeyPress += Subscribe_KeyboardKey;
            subscribe.KeyDown += Subscribe_KeyDown;
            subscribe.KeyUp += Subscribe_KeyUp;
        }

        private void Subscribe_MouseWheel(MouseDetails mouseDetails)
        {
            Append("mouse wheel: " + mouseDetails.Wheel);
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

        private bool CanBeTopMost = true;
        
        public void DisableTopmost()
        {
            CanBeTopMost = false;
            this.Topmost = false;
        }
        public void EnableTopmost()
        {
            CanBeTopMost = true;
        }

        private void Recordings_Deactivated(object sender, EventArgs e)
        {
            if (CanBeTopMost)
                this.Topmost = true;
        }

        public void Append(string text)
        {
            logArea.AppendText(text + "\n");
            logArea.ScrollToEnd();
        }

        public void SetRecording(Recording recording)
        {
            ActualRecordingName.Content = recording.Name;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
        }


        public event EmptyEvent Start;
        public event EmptyEvent End;
        public event EmptyEvent ReplyStart;
        public event EmptyEvent ReplyEnd;
        public event EmptyEvent Setialize;
        public event StringEvent AddRecording;

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

        private void Serialize_Click(object sender, RoutedEventArgs e)
        {
            Setialize?.Invoke();
        }

        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuClose_Click(object sender, RoutedEventArgs e)
        {

        }

        public class VVV
        {
            public string DisplayName { get; set; }

            public VVV(string taskname)
            {
                this.DisplayName = taskname;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            if (button != null)
            {
                var task = button.DataContext as VVV;

                ((ObservableCollection<VVV>)listOfRecordings.ItemsSource).Remove(task);
            }
            else
            {
                return;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveRecording_Click(object sender, RoutedEventArgs e)
        {
            AddRecording?.Invoke(recordingName.Text);
        }
    }
}
