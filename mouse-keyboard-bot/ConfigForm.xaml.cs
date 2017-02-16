using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace mouse_keyboard_bot
{
    public partial class ConfigForm : Window
    {
        public ConfigForm()
        {
            InitializeComponent();
            this.Closing += new CancelEventHandler(Config_Closing);

            Icon = new BitmapImage(new Uri(BotAppContext.IconFileName, UriKind.RelativeOrAbsolute));

            saveFile.Text = Config.SaveFile.StringValue;
            skipLast.Text = Config.SkipLast.StringValue;
            openForms.IsChecked = Config.OpenFormsAtInit.Value;
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            Config.SaveFile.Value = saveFile.Text;
            int skipLastValue;
            if (int.TryParse(skipLast.Text, out skipLastValue))
            {
                if (skipLastValue >= 0)
                {
                    Config.SkipLast.Value = skipLastValue;
                }
                else
                {
                    MessageBox.Show("Skip last has to be >= 0.", "Wrong value");
                }
            }
            else
            {
                MessageBox.Show("Wrong integer value.", "Error");
            }
            if (openForms.IsChecked != null)
            {
                Config.OpenFormsAtInit.Value = openForms.IsChecked.Value;
            }
            Config.save();
        }

        bool checkDiff()
        {
            return saveFile.Text != Config.SaveFile.StringValue ||
                    skipLast.Text != Config.SkipLast.StringValue ||
                    openForms.IsChecked != Config.OpenFormsAtInit.Value;
        }

        void Config_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (checkDiff())
            {
                var result = MessageBox.Show("Config has been changed but not save. Do you want to save?", "Unsaved changes", MessageBoxButton.YesNoCancel, MessageBoxImage.Asterisk);
                if (result == MessageBoxResult.Yes)
                {
                    Zapisz_Click(null, null);
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
