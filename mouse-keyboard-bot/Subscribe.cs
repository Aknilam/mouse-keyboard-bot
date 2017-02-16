using System;
using System.ComponentModel;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Gma.System.MouseKeyHook.Implementation;

namespace mouse_keyboard_bot
{
    public class Subscribe : IDisposable
    {
        public delegate void MouseClickAtSchema(MouseButtons button, int px, int py);
        public event MouseClickAtSchema MouseClickAt;

        public delegate void MouseDblClickAtSchema(MouseButtons button, int px, int py);
        public event MouseDblClickAtSchema MouseDblClickAt;


        public delegate void KeyboardKeySchema(char key, VirtualKeyCode code);
        public event KeyboardKeySchema KeyboardKey;


        private bool isDisposed = false;
        private IKeyboardMouseEvents m_Events;

        public Subscribe()
        {
            SubscribeGlobal();
        }

        public void Dispose()
        {
            Unsubscribe();
            isDisposed = true;
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
            //m_Events.KeyDown += OnKeyDown;
            //m_Events.KeyUp += OnKeyUp;
            m_Events.KeyPress += HookManager_KeyPress;

            //m_Events.MouseUp += OnMouseUp;
            m_Events.MouseClick += OnMouseClick;
            m_Events.MouseDoubleClick += OnMouseDoubleClick;

            m_Events.MouseMove += HookManager_MouseMove;

            //m_Events.MouseDragStarted += OnMouseDragStarted;
            //m_Events.MouseDragFinished += OnMouseDragFinished;
            
            //m_Events.MouseWheel += HookManager_MouseWheel;
            
            //m_Events.MouseDown += OnMouseDown;
        }

        private void Unsubscribe()
        {
            if (m_Events == null) return;
            //m_Events.KeyDown -= OnKeyDown;
            //m_Events.KeyUp -= OnKeyUp;
            m_Events.KeyPress -= HookManager_KeyPress;

            //m_Events.MouseUp -= OnMouseUp;
            m_Events.MouseClick -= OnMouseClick;
            m_Events.MouseDoubleClick -= OnMouseDoubleClick;

            m_Events.MouseMove -= HookManager_MouseMove;

            //m_Events.MouseDragStarted -= OnMouseDragStarted;
            //m_Events.MouseDragFinished -= OnMouseDragFinished;
            
            //m_Events.MouseWheel -= HookManager_MouseWheel;
            
            //m_Events.MouseDown -= OnMouseDown;

            m_Events.Dispose();
            m_Events = null;
        }
        
        private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ext = e as KeyPressEventArgsExt;
            if (ext != null)
            {
                KeyboardKey?.Invoke(e.KeyChar, ext.Code);
            }
        }

        private int? px = null;
        private int? py = null;
        private void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            px = e.X;
            py = e.Y;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (px.HasValue && py.HasValue)
                MouseClickAt?.Invoke(e.Button, px.Value, py.Value);
        }

        private void OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (px.HasValue && py.HasValue)
                MouseDblClickAt?.Invoke(e.Button, px.Value, py.Value);
        }
    }
}