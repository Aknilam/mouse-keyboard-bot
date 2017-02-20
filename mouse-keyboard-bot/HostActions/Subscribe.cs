using System;
using System.ComponentModel;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;
using mouse_keyboard_bot.Model;

namespace mouse_keyboard_bot
{
    public class Subscribe : IDisposable
    {
        public delegate void MouseClickAtSchema(MouseDetails details);
        public event MouseClickAtSchema MouseClickAt;
        public event MouseClickAtSchema MouseDown;
        public event MouseClickAtSchema MouseUp;

        public delegate void MouseDblClickAtSchema(MouseDetails details);
        public event MouseDblClickAtSchema MouseDblClickAt;


        public delegate void KeyboardKeySchema(char key, VirtualKeyCode code);
        public event KeyboardKeySchema KeyPress;

        public delegate void KeyboardKeySingleSchema(Keys key, VirtualKeyCode code);
        public event KeyboardKeySingleSchema KeyDown;
        public event KeyboardKeySingleSchema KeyUp;


        private bool isDisposed = false;
        private IKeyboardMouseEvents m_Events;

        public Subscribe()
        {
            SubscribeGlobal();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                Unsubscribe();
                isDisposed = true;
            }
        }

        //private void SubscribeApplication()
        //{
        //    Unsubscribe();
        //    Run(Hook.AppEvents());
        //}

        private void SubscribeGlobal()
        {
            Unsubscribe();
            Run(Hook.GlobalEvents());
        }

        private void Run(IKeyboardMouseEvents events)
        {
            m_Events = events;
            m_Events.KeyDown += OnKeyDown;
            m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            m_Events.MouseUp += OnMouseUp;
            m_Events.MouseClick += OnMouseClick;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            //m_Events.MouseDragStarted += OnMouseDragStarted;
            //m_Events.MouseDragFinished += OnMouseDragFinished;

            //m_Events.MouseWheel += HookManager_MouseWheel;

            m_Events.MouseDown += OnMouseDown;
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            m_Events.KeyDown -= OnKeyDown;
            m_Events.KeyUp -= OnKeyUp;
            m_Events.KeyPress -= HookManager_KeyPress;

            m_Events.MouseUp -= OnMouseUp;
            m_Events.MouseClick -= OnMouseClick;
            m_Events.MouseDoubleClick -= OnMouseDoubleClick;

            m_Events.MouseMove -= HookManager_MouseMove;

            //m_Events.MouseDragStarted -= OnMouseDragStarted;
            //m_Events.MouseDragFinished -= OnMouseDragFinished;

            //m_Events.MouseWheel -= HookManager_MouseWheel;

            m_Events.MouseDown -= OnMouseDown;

            m_Events.Dispose();
            m_Events = null;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            MouseDown?.Invoke(new MouseDetails { Button = e.Button, X = e.X, Y = e.Y, EventType = MouseEventType.Down });
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            MouseUp?.Invoke(new MouseDetails { Button = e.Button, X = e.X, Y = e.Y, EventType = MouseEventType.Up });
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            var ext = e as KeyEventArgsExt;
            if (ext != null)
            {
                KeyUp?.Invoke(e.KeyCode, ext.Code);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            var ext = e as KeyEventArgsExt;
            if (ext != null)
            {
                KeyDown?.Invoke(e.KeyCode, ext.Code);
            }
        }

        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ext = e as KeyPressEventArgsExt;
            if (ext != null)
            {
                KeyPress?.Invoke(e.KeyChar, ext.Code);
            }
        }

        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            MouseClickAt?.Invoke(new MouseDetails { Button = e.Button, X = e.X, Y = e.Y, EventType = MouseEventType.Click });
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            MouseDblClickAt?.Invoke(new MouseDetails { Button = e.Button, X = e.X, Y = e.Y, EventType = MouseEventType.DbClick });
        }
    }
}