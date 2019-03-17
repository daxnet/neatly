using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Sdk.Windows
{
    public sealed class WindowHiddenEventArgs : WindowEventArgs
    {
        public WindowHiddenEventArgs(BaseWindow window) : base(window)
        {
        }
    }
}
