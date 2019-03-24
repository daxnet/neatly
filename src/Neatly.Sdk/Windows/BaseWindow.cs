using Neatly.DocumentModel;
using Neatly.Framework;
using Neatly.Framework.Workspaces;
using Neatly.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Neatly.Sdk.Windows
{
    public partial class BaseWindow : DockContent
    {

        #region Protected Constructors

        protected BaseWindow(INeatlyShell shell, bool hideOnClose = true)
            : this()
        {
            Shell = shell;

            Shell.Workspace.WorkspaceCreated += OnWorkspaceCreated;
            Shell.Workspace.WorkspaceChanged += OnWorkspaceChanged;
            Shell.Workspace.WorkspaceClosed += OnWorkspaceClosed;
            Shell.Workspace.WorkspaceOpened += OnWorkspaceOpened;
            Shell.Workspace.WorkspaceSaved += OnWorkspaceSaved;
            Shell.Workspace.NodeOpened += OnWorkspaceNodeOpened;
            Shell.Workspace.NodeSelected += OnWorkspaceNodeSelected;
            Shell.Workspace.DocumentNodeAdded += OnWorkspaceDocumentNodeAdded;

            HideOnClose = hideOnClose;
        }

        #endregion Protected Constructors

        #region Private Constructors

        private BaseWindow()
        {
            InitializeComponent();
        }

        #endregion Private Constructors

        #region Public Events

        public event EventHandler DockWindowHidden;

        public event EventHandler DockWindowShown;

        #endregion Public Events

        #region Protected Properties

        protected INeatlyShell Shell { get; }

        protected virtual WindowTools Tools => WindowTools.Empty;

        #endregion Protected Properties

        #region Public Methods

        public override string ToString() => this.Text;

        #endregion Public Methods

        #region Protected Internal Methods

        internal protected virtual void Cleanup()
        {
            Shell.Workspace.WorkspaceCreated -= OnWorkspaceCreated;
            Shell.Workspace.WorkspaceChanged -= OnWorkspaceChanged;
            Shell.Workspace.WorkspaceClosed -= OnWorkspaceClosed;
            Shell.Workspace.WorkspaceOpened -= OnWorkspaceOpened;
            Shell.Workspace.WorkspaceSaved -= OnWorkspaceSaved;
            Shell.Workspace.NodeOpened -= OnWorkspaceNodeOpened;
            Shell.Workspace.NodeSelected -= OnWorkspaceNodeSelected;
            Shell.Workspace.DocumentNodeAdded -= OnWorkspaceDocumentNodeAdded;
        }

        #endregion Protected Internal Methods

        #region Protected Methods

        protected override void OnDockStateChanged(EventArgs e)
        {
            switch (DockState)
            {
                case DockState.Hidden:
                    this.OnDockWindowHidden(e);
                    break;
                case DockState.Unknown:
                    break;
                default:
                    this.OnDockWindowShown(e);
                    break;
            }
        }
        protected virtual void OnDockWindowHidden(EventArgs e)
        {
            DockWindowHidden?.Invoke(this, e);
            if (!(Tools?.IsEmpty ?? true))
            {
                Shell.RevertMerge(Tools);
            }
        }

        protected virtual void OnDockWindowShown(EventArgs e)
        {
            DockWindowShown?.Invoke(this, e);
            if (!(Tools?.IsEmpty ?? true))
            {
                Shell.MergeTools(Tools);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // If the setting is to close the window instead of hiding, we need
            // to cleanup the resources as the form is going to be closed and disposed.
            if (!HideOnClose)
            {
                Cleanup();
            }

            base.OnFormClosing(e);
        }

        protected virtual void OnWorkspaceChanged(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceClosed(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs<Document> e) { }

        protected virtual void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs<Document> e) { }

        protected virtual void OnWorkspaceSaved(object sender, EventArgs e) { }

        protected virtual void OnWorkspaceNodeOpened(object sender, NodeEventArgs e) { }

        protected virtual void OnWorkspaceNodeSelected(object sender, NodeEventArgs e) { }

        protected virtual void OnWorkspaceDocumentNodeAdded(object sender, DocumentNodeAddedEventArgs e) { }

        #endregion Protected Methods

    }
}
