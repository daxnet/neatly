using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Windows
{
    internal sealed class WindowHiddenEventArgs : WindowEventArgs
    {
        public WindowHiddenEventArgs(BaseWindow window) : base(window)
        {
        }
    }
}
