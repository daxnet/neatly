using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Sdk
{
    /// <summary>
    /// Represents the state of the current Neatly application.
    /// </summary>
    public enum ShellState
    {
        /// <summary>
        /// Indicates that the application has started and initialized.
        /// </summary>
        ApplicationInitialized,

        /// <summary>
        /// Indicates that the workspace has been created.
        /// </summary>
        WorkspaceCreated,

        /// <summary>
        /// Indicates that there is a document being opened by the workspace.
        /// </summary>
        WorkspaceOpened,

        /// <summary>
        /// Indicates that the workspace has been saved.
        /// </summary>
        WorkspaceSaved,

        /// <summary>
        /// Indicates that the workspace has been closed.
        /// </summary>
        WorkspaceClosed,

        /// <summary>
        /// Indicates that the workspace has been changed.
        /// </summary>
        WorkspaceChanged
    }
}
