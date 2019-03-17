using Neatly.DocumentModel;
using Neatly.Sdk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Sdk
{
    /// <summary>
    /// Represents the abstraction of the Neatly main application. Shells can be
    /// referenced by any components that extends Neatly application.
    /// </summary>
    public interface INeatlyShell : IWin32Window
    {

        string Text { get; }

        ShellState State { get; }

        NeatlyWorkspace Workspace { get; }

        IDocumentNavigator DocumentNavigator { get; }

        void MergeTools(WindowTools windowTools);

        void RevertMerge(WindowTools windowTools);
    }
}
