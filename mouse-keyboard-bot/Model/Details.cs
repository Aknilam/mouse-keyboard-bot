using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mouse_keyboard_bot.Model
{
    [Serializable]
    public abstract class Details
    {
        public virtual DetailsType Type { get; }

        public long Time { get; set; }
    }
}
