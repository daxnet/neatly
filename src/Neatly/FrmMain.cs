using Neatly.Controls;
using Neatly.DocumentModel;
using Neatly.Properties;
using Neatly.Sdk;
using Neatly.Windows;
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

namespace Neatly
{
    public partial class FrmMain : Form, INeatlyShell
    {
        private const float ResizeFactor = 0.8F;

        private NeatlyWorkspace workspace;

        private ActionComponent newDocumentAction;
        private ActionComponent openDocumentAction;
        private ActionComponent saveDocumentAction;

        public FrmMain()
        {
            InitializeComponent();
            InitializeWorkspace();
            InitializeActionComponent();
            InitializeDockingSurface();
        }

        private void InitializeWorkspace()
        {
            workspace = new NeatlyWorkspace();
            workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            workspace.WorkspaceOpened += Workspace_WorkspaceOpened;
            workspace.WorkspaceSaved += Workspace_WorkspaceSaved;
            workspace.WorkspaceChanged += Workspace_WorkspaceChanged;
            workspace.WorkspaceClosed += Workspace_WorkspaceClosed;
        }

        private void Workspace_WorkspaceClosed(object sender, EventArgs e)
        {
            
        }

        private void Workspace_WorkspaceChanged(object sender, EventArgs e)
        {
            
        }

        private void Workspace_WorkspaceSaved(object sender, Framework.Workspaces.WorkspaceSavedEventArgs<Document> e)
        {
            
        }

        private void Workspace_WorkspaceOpened(object sender, Framework.Workspaces.WorkspaceOpenedEventArgs<Document> e)
        {
            
        }

        private void Workspace_WorkspaceCreated(object sender, Framework.Workspaces.WorkspaceCreatedEventArgs<Document> e)
        {
            
        }

        private void InitializeDockingSurface()
        {
            var navigationWindow = new DocumentNavigatorWindow();
            navigationWindow.Show(dockPanel, DockState.DockLeft);
        }

        private void InitializeActionComponent()
        {
            newDocumentAction = new ActionComponent(mnuNewDocument, tbtnNewDocument, Resources.Tooltip_NewDocument, Action_NewDocument);
            openDocumentAction = new ActionComponent(mnuOpenDocument, tbtnOpenDocument, Resources.Tooltip_OpenDocument, Action_OpenDocument);
            saveDocumentAction = new ActionComponent(mnuSaveDocument, tbtnSaveDocument, Resources.Tooltip_SaveDocument, Action_SaveDocument);
        }

        private void ResetApplicationState()
        {
            this.newDocumentAction.Enabled = true;
            this.openDocumentAction.Enabled = true;
            this.saveDocumentAction.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var currentScreen = Screen.FromControl(this);
            this.Width = Convert.ToInt32(currentScreen.Bounds.Width * ResizeFactor);
            this.Height = Convert.ToInt32(currentScreen.Bounds.Height * ResizeFactor);
            this.Location = new Point(currentScreen.Bounds.X + (currentScreen.Bounds.Width - this.Width) / 2,
                currentScreen.Bounds.Y + (currentScreen.Bounds.Height - this.Height) / 2);

            ResetApplicationState();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            workspace.WorkspaceChanged -= Workspace_WorkspaceChanged;
            workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
            workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            workspace.WorkspaceOpened -= Workspace_WorkspaceOpened;
            workspace.WorkspaceSaved -= Workspace_WorkspaceSaved;

            newDocumentAction.Dispose();
            openDocumentAction.Dispose();
            saveDocumentAction.Dispose();
        }

        private void Action_NewDocument(object sender, EventArgs e)
        {
            this.workspace.New(doc =>
            {
                using (var createNewDocumentDialog = new FrmCreateNewDocument(doc))
                {
                    if (createNewDocumentDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        return (true, createNewDocumentDialog.Document);
                    }

                    return (false, doc);
                }
            });
        }

        private void Action_OpenDocument(object sender, EventArgs e)
        {

        }

        private void Action_SaveDocument(object sender, EventArgs e)
        {

        }
    }
}
