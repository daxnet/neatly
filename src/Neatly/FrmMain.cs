﻿using Neatly.Controls;
using Neatly.DocumentModel;
using Neatly.Framework.Workspaces;
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

        #region Private Fields

        private const float ResizeFactor = 0.8F;

        private ActionComponent closeDocumentAction;
        private ActionComponent newDocumentAction;
        private ActionComponent openDocumentAction;
        private ActionComponent saveDocumentAction;
        private ShellState state;
        private NeatlyWorkspace workspace;

        #endregion Private Fields

        #region Public Constructors

        public FrmMain()
        {
            InitializeComponent();
            InitializeWorkspace();
            InitializeActionComponent();
            InitializeDockingSurface();
        }

        #endregion Public Constructors

        #region Public Properties

        public ShellState State
        {
            get => state;
            set
            {
                state = value;
                switch (state)
                {
                    case ShellState.ApplicationInitialized:
                        this.newDocumentAction.Enabled = true;
                        this.openDocumentAction.Enabled = true;
                        this.saveDocumentAction.Enabled = false;
                        this.closeDocumentAction.Enabled = false;
                        break;
                    case ShellState.WorkspaceOpened:
                        this.saveDocumentAction.Enabled = false;
                        this.closeDocumentAction.Enabled = true;
                        break;
                    case ShellState.WorkspaceCreated:
                        this.saveDocumentAction.Enabled = true;
                        this.closeDocumentAction.Enabled = true;
                        break;
                    case ShellState.WorkspaceSaved:
                        this.saveDocumentAction.Enabled = false;
                        break;
                    case ShellState.WorkspaceClosed:
                        this.saveDocumentAction.Enabled = false;
                        this.closeDocumentAction.Enabled = false;
                        break;
                }
            }
        }

        public NeatlyWorkspace Workspace => workspace;

        #endregion Public Properties

        #region Protected Methods

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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !this.workspace.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var currentScreen = Screen.FromControl(this);
            this.Width = Convert.ToInt32(currentScreen.Bounds.Width * ResizeFactor);
            this.Height = Convert.ToInt32(currentScreen.Bounds.Height * ResizeFactor);
            this.Location = new Point(currentScreen.Bounds.X + (currentScreen.Bounds.Width - this.Width) / 2,
                currentScreen.Bounds.Y + (currentScreen.Bounds.Height - this.Height) / 2);

            this.State = ShellState.ApplicationInitialized;
        }

        #endregion Protected Methods

        #region Private Methods

        private void Action_CloseDocument(object sender, EventArgs e)
        {
            this.workspace.Close();
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
            this.workspace.Open();
        }

        private void Action_SaveDocument(object sender, EventArgs e)
        {
            this.workspace.Save();
        }

        private void InitializeActionComponent()
        {
            newDocumentAction = new ActionComponent(this, mnuNewDocument, tbtnNewDocument, Resources.Tooltip_NewDocument, Action_NewDocument);
            openDocumentAction = new ActionComponent(this, mnuOpenDocument, tbtnOpenDocument, Resources.Tooltip_OpenDocument, Action_OpenDocument);
            saveDocumentAction = new ActionComponent(this, mnuSaveDocument, tbtnSaveDocument, Resources.Tooltip_SaveDocument, Action_SaveDocument);
            closeDocumentAction = new ActionComponent(this, mnuCloseDocument, Resources.Tooltip_CloseDocument, Action_CloseDocument);
        }

        private void InitializeDockingSurface()
        {
            var navigator = new Navigator(this);
            navigator.Show(dockPanel, DockState.DockLeft);
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

        private void Workspace_WorkspaceChanged(object sender, EventArgs e)
        {
            State = ShellState.WorkspaceChanged;
        }

        private void Workspace_WorkspaceClosed(object sender, EventArgs e)
        {
            State = ShellState.WorkspaceClosed;
        }
        private void Workspace_WorkspaceCreated(object sender, WorkspaceCreatedEventArgs<Document> e)
        {
            State = ShellState.WorkspaceCreated;
        }

        private void Workspace_WorkspaceOpened(object sender, WorkspaceOpenedEventArgs<Document> e)
        {
            State = ShellState.WorkspaceOpened;
        }

        private void Workspace_WorkspaceSaved(object sender, WorkspaceSavedEventArgs<Document> e)
        {
            State = ShellState.WorkspaceSaved;
        }

        #endregion Private Methods

    }
}
