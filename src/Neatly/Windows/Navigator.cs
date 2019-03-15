using Neatly.DocumentModel;
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

namespace Neatly.Windows
{
    public partial class Navigator : BaseWindow
    {
        public Navigator(INeatlyShell shell)
            : base(shell)
        {
            InitializeComponent();
            
            Shell.Workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceClosed += Workspace_WorkspaceClosed;
        }

        private void Workspace_WorkspaceClosed(object sender, EventArgs e)
        {
            tvOutline.Nodes.Clear();
        }

        private void Workspace_WorkspaceCreated(object sender, WorkspaceCreatedEventArgs<Document> e)
        {
            tvOutline.Nodes.Clear();
            tvOutline.Nodes.Add(e.Model.Title);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Shell.Workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
        }
    }
}
