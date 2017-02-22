using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mouse_keyboard_bot.Model
{
    [Serializable]
    public class MouseDetails : Details
    {
        public int X { get; set; }
        public int Y { get; set; }
        public MouseButtons Button { get; set; }
        public int Wheel { get; set; }

        public MouseEventType EventType { get; set; }

        public System.Drawing.Point Point
        {
            get
            {
                return new System.Drawing.Point(X, Y);
            }
        }

        public override string ToString()
        {
            return Button + " " + X + " " + Y;
        }

        public override DetailsType Type { get { return DetailsType.Mouse; } }
    }

    public enum MouseEventType
    {
        Down,
        Up,
        Click,
        DbClick,
        Wheel
    }
}
