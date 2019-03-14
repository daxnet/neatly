using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Sdk
{
    public enum ShellState
    {
        ApplicationInitialized,
        WorkspaceCreated,
        WorkspaceOpened,
        WorkspaceSaved,
        WorkspaceClosed,
        WorkspaceChanged
    }
}
