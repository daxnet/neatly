using Neatly.Controls;
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
using Neatly.Sdk.Windows;

namespace Neatly
{
    public partial class FrmMain : Form, INeatlyShell
    {

        #region Private Fields

        private const float ResizeFactor = 0.8F;

        private readonly ActionComponent closeDocumentAction;
        private readonly DocumentNavigator documentNavigator;
        private readonly ActionComponent documentNavigatorAction;
        private readonly ActionComponent newDocumentAction;
        private readonly ActionComponent openDocumentAction;
        private readonly ActionComponent saveDocumentAction;
        private readonly WindowManager windowManager;
        private ShellState state;

        #endregion Private Fields

        #region Public Constructors

        public FrmMain()
        {
            InitializeComponent();

            // Initialize Action Components.
            newDocumentAction = new ActionComponent(this, mnuNewDocument, tbtnNewDocument, Resources.Tooltip_NewDocument, Action_NewDocument);
            openDocumentAction = new ActionComponent(this, mnuOpenDocument, tbtnOpenDocument, Resources.Tooltip_OpenDocument, Action_OpenDocument);
            saveDocumentAction = new ActionComponent(this, mnuSaveDocument, tbtnSaveDocument, Resources.Tooltip_SaveDocument, Action_SaveDocument);
            closeDocumentAction = new ActionComponent(this, mnuCloseDocument, Resources.Tooltip_CloseDocument, Action_CloseDocument);
            documentNavigatorAction = new ActionComponent(this, mnuDocumentNavigator, Resources.Tooltip_DocumentNavigator, Action_ShowHideWindow) { Tag = typeof(DocumentNavigator) };

            // Initialize Workspace.
            Workspace = new NeatlyWorkspace();
            Workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            Workspace.WorkspaceOpened += Workspace_WorkspaceOpened;
            Workspace.WorkspaceSaved += Workspace_WorkspaceSaved;
            Workspace.WorkspaceChanged += Workspace_WorkspaceChanged;
            Workspace.WorkspaceClosed += Workspace_WorkspaceClosed;
            Workspace.NodeOpened += Workspace_NodeOpened;

            // Initialize Windows.
            windowManager = new WindowManager(this);
            windowManager.WindowHidden += WindowManager_WindowHidden;
            windowManager.WindowShown += WindowManager_WindowShown;
            documentNavigator = windowManager.CreateWindow<DocumentNavigator>();
            documentNavigator.Show(dockPanel, DockState.DockLeft);
        }

        private void Workspace_NodeOpened(object sender, NodeEventArgs e)
        {
            if (e.Node.Type == NodeType.DocumentNode)
            {
                var editorWindow = this.windowManager.CreateWindow<Editor>(e.Node);
                editorWindow.Show(dockPanel, DockState.Document);
            }
        }

        #endregion Public Constructors

        #region Public Properties

        public IDocumentNavigator DocumentNavigator => documentNavigator;

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

        public NeatlyWorkspace Workspace { get; }

        #endregion Public Properties

        #region Public Methods

        public void MergeTools(WindowTools windowTools)
        {
            if (windowTools?.IsEmpty ?? true)
            {
                return;
            }

            if (windowTools.MergingToolbar != null)
            {
                ToolStripManager.Merge(windowTools.MergingToolbar.ToolStrip, this.mainToolStrip);
                windowTools.MergingToolbar.ToolStrip.Hide();
            }

            if (windowTools.MergingMenus != null &&
                windowTools.MergingMenus.Count() > 0)
            {
                foreach(var mergingMenu in windowTools.MergingMenus)
                {
                    switch(mergingMenu.Position)
                    {
                        case MenuMergePosition.File:
                            ToolStripManager.Merge(mergingMenu.ToolStrip, mnuFile.DropDown);
                            break;
                        case MenuMergePosition.Edit:
                            ToolStripManager.Merge(mergingMenu.ToolStrip, mnuEdit.DropDown);
                            break;
                        case MenuMergePosition.View:
                            ToolStripManager.Merge(mergingMenu.ToolStrip, mnuView.DropDown);
                            break;
                        case MenuMergePosition.Tools:
                            ToolStripManager.Merge(mergingMenu.ToolStrip, mnuTools.DropDown);
                            break;
                        case MenuMergePosition.Help:
                            ToolStripManager.Merge(mergingMenu.ToolStrip, mnuHelp.DropDown);
                            break;
                    }
                }
            }
        }

        public void RevertMerge(WindowTools windowTools)
        {
            if (windowTools?.IsEmpty ?? true)
            {
                return;
            }

            if (windowTools.MergingToolbar != null && windowTools.MergingToolbar.NeedHide)
            {
                windowTools.MergingToolbar.ToolStrip.Show();
                ToolStripManager.RevertMerge(this.mainToolStrip, windowTools.MergingToolbar.ToolStrip);
            }

            if (windowTools.MergingMenus != null &&
                windowTools.MergingMenus.Count() > 0)
            {
                foreach (var mergingMenu in windowTools.MergingMenus)
                {
                    if (mergingMenu.NeedHide)
                    {
                        switch (mergingMenu.Position)
                        {
                            case MenuMergePosition.File:
                                ToolStripManager.RevertMerge(mnuFile.DropDown, mergingMenu.ToolStrip);
                                break;
                            case MenuMergePosition.Edit:
                                ToolStripManager.RevertMerge(mnuEdit.DropDown, mergingMenu.ToolStrip);
                                break;
                            case MenuMergePosition.View:
                                ToolStripManager.RevertMerge(mnuView.DropDown, mergingMenu.ToolStrip);
                                break;
                            case MenuMergePosition.Tools:
                                ToolStripManager.RevertMerge(mnuTools.DropDown, mergingMenu.ToolStrip);
                                break;
                            case MenuMergePosition.Help:
                                ToolStripManager.RevertMerge(mnuHelp.DropDown, mergingMenu.ToolStrip);
                                break;
                        }
                    }
                }
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            Workspace.WorkspaceChanged -= Workspace_WorkspaceChanged;
            Workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
            Workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            Workspace.WorkspaceOpened -= Workspace_WorkspaceOpened;
            Workspace.WorkspaceSaved -= Workspace_WorkspaceSaved;
            Workspace.NodeOpened -= Workspace_NodeOpened;

            newDocumentAction.Dispose();
            openDocumentAction.Dispose();
            saveDocumentAction.Dispose();

            windowManager.WindowHidden -= WindowManager_WindowHidden;
            windowManager.WindowShown -= WindowManager_WindowShown;
            windowManager.Dispose();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            e.Cancel = !this.Workspace.Close();
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
            this.Workspace.Close();
        }

        private void Action_NewDocument(object sender, EventArgs e)
        {
            this.Workspace.New(doc =>
            {
                using (var createNewDocumentDialog = new FrmCreateDocument(doc))
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
            this.Workspace.Open();
        }

        private void Action_SaveDocument(object sender, EventArgs e)
        {
            this.Workspace.Save();
        }

        private void Action_ShowHideWindow(object sender, EventArgs e)
        {
            if (sender is ToolStripItem tsi &&
                tsi.Tag is Type windowType)
            {
                var windows = windowManager.GetWindows(windowType);
                foreach (var window in windows)
                {
                    if (window.DockState == DockState.Hidden)
                    {
                        window.Show();
                    }
                    else
                    {
                        window.Hide();
                    }
                }
            }
        }
        private void WindowManager_WindowHidden(object sender, WindowHiddenEventArgs e)
        {
            documentNavigatorAction.Checked = false;
        }

        private void WindowManager_WindowShown(object sender, WindowShownEventArgs e)
        {
            documentNavigatorAction.Checked = true;
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
            var articleNode = e.Model.ChildNodes.First();
            Workspace.OpenNode(articleNode);
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
