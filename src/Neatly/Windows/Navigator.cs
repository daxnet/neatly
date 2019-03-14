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
    public partial class Navigator : DockContent
    {
        private readonly INeatlyShell shell;

        public Navigator(INeatlyShell shell)
        {
            this.shell = shell;

            InitializeComponent();
            
            this.shell.Workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            this.shell.Workspace.WorkspaceClosed += Workspace_WorkspaceClosed;
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

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            this.shell.Workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            this.shell.Workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
        }
    }
}
