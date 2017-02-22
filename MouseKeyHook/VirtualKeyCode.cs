using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gma.System.MouseKeyHook
{
    [Serializable]
    public class VirtualKeyCode
    {
        public int virtualKeyCode;
        public int scanCode;

        public override string ToString()
        {
            return "v: " + virtualKeyCode + " s: " + scanCode;
        }
    }
}
