using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Windows
{
    internal abstract class WindowEventArgs : EventArgs
    {
        public WindowEventArgs(BaseWindow window)
        {
            this.Window = window;
        }

        public BaseWindow Window { get; }
    }
}
