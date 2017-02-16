﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Reflection;
using mouse_keyboard_bot.Recorder;

namespace mouse_keyboard_bot
{
    public class BotAppContext : ApplicationContext
    {
        // Icon graphic from http://prothemedesign.com/circular-icons/
        public static readonly string IconFileName = "route.ico";
        private static readonly string DefaultTooltip = "Recorder";

        private Subscribe subscribe;
        private UserActionsRecorder recorder;

        /// <summary>
		/// This class should be created and passed into Application.Run( ... )
		/// </summary>
		public BotAppContext()
        {
            InitializeContext();

            if (Config.OpenFormsAtInit.Value)
            {
                ShowConfigForm();
            }

            subscribe = new Subscribe();
            recorder = new UserActionsRecorder(subscribe);
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = false;
            notifyIcon.ContextMenuStrip.Items.Clear();
            notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&Bot", botItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&Opcje", showConfigItem_Click));
            notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator());
            notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler("&Zamknij", exitItem_Click));
        }

        public ToolStripMenuItem ToolStripMenuItemWithHandler(string displayText, EventHandler eventHandler)
        {
            var item = new ToolStripMenuItem(displayText);
            if (eventHandler != null) { item.Click += eventHandler; }
            return item;
        }

        # region the child forms
        
        private System.Windows.Window configForm;
        private System.Windows.Window botForm;

        public void ShowBotForm()
        {
            if (botForm == null)
            {
                botForm = new Recordings(subscribe);
                botForm.Closed += botForm_Closed; // avoid reshowing a disposed form
                ElementHost.EnableModelessKeyboardInterop(botForm);
                botForm.Show();
            }
            else { botForm.Activate(); }
        }

        public void ShowConfigForm()
        {
            if (configForm == null)
            {
                configForm = new ConfigForm();
                configForm.Closed += configForm_Closed; // avoid reshowing a disposed form
                ElementHost.EnableModelessKeyboardInterop(configForm);
                configForm.Show();
            }
            else { configForm.Activate(); }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e) { }

        // From http://stackoverflow.com/questions/2208690/invoke-notifyicons-context-menu
        private void notifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
        }

        private void botItem_Click(object sender, EventArgs e)
        {
            ShowBotForm();
        }

        private void showConfigItem_Click(object sender, EventArgs e)
        {
            ShowConfigForm();
        }

        private void botForm_Closed(object sender, EventArgs e)
        {
            botForm = null;
        }

        private void configForm_Closed(object sender, EventArgs e)
        {
            configForm = null;
        }

        # endregion the child forms

        # region generic code framework

        private System.ComponentModel.IContainer components;	// a list of components to dispose when the context is disposed
        private NotifyIcon notifyIcon;				            // the icon that sits in the system tray

        private void InitializeContext()
        {
            components = new System.ComponentModel.Container();

            notifyIcon = new NotifyIcon(components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new Icon(IconFileName),
                Text = DefaultTooltip,
                Visible = true
            };
            notifyIcon.ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            notifyIcon.MouseUp += notifyIcon_MouseUp;
        }

        /// <summary>
		/// When the application context is disposed, dispose things like the notify icon.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) { components.Dispose(); }
        }

        /// <summary>
        /// When the exit menu item is clicked, make a call to terminate the ApplicationContext.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitItem_Click(object sender, EventArgs e)
        {
            exit();
        }

        public bool exitting = false;

        public void exit()
        {
            exitting = true;
            string question = "Czy na pewno chcesz zamknąć program?";
            if (MessageBox.Show(question, "Zamykanie programu", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ExitThread();
            } else
            {
                exitting = false;
            }
        }

        /// <summary>
        /// If we are presently showing a form, clean it up.
        /// </summary>
        protected override void ExitThreadCore()
        {
            // before we exit, let forms clean themselves up.
            if (configForm != null) { configForm.Close(); }

            notifyIcon.Visible = false; // should remove lingering tray icon
            base.ExitThreadCore();
        }

        # endregion generic code framework

    }
}
