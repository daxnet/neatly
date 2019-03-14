using Neatly.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Sdk
{
    public interface INeatlyShell : IWin32Window
    {

        string Text { get; }

        ShellState State { get; }

        NeatlyWorkspace Workspace { get; }
    }
}
