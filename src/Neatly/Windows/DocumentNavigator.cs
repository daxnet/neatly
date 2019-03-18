using Neatly.DocumentModel;
using Neatly.Framework.Workspaces;
using Neatly.Sdk;
using Neatly.Sdk.Windows;
using System;
using System.Linq;
using System.Windows.Forms;

namespace Neatly.Windows
{
    public partial class DocumentNavigator : BaseWindow, IDocumentNavigator
    {
        private readonly WindowTools tools;

        #region Public Constructors

        public DocumentNavigator(INeatlyShell shell)
            : base(shell)
        {
            InitializeComponent();

            tools = new WindowTools(new ToolStripMerge(toolStrip, false));

            Shell.Workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceOpened += Workspace_WorkspaceOpened;
            Shell.Workspace.WorkspaceClosed += Workspace_WorkspaceClosed;
            Shell.Workspace.NodeOpened += Workspace_NodeOpened;
        }

        private void Workspace_NodeOpened(object sender, NodeEventArgs e)
        {
            var treeNode = this.navigationTree.Nodes.Find(e.Node.Id.ToString(), true).FirstOrDefault();
            if (treeNode != null)
            {
                if (treeNode.Parent != null)
                {
                    treeNode.Parent.Expand();
                }

                navigationTree.SelectedNode = treeNode;
            }
        }

        #endregion Public Constructors

        #region Protected Properties

        protected override WindowTools Tools => tools;

        #endregion Protected Properties

        #region Protected Methods

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Shell.Workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
            Shell.Workspace.WorkspaceOpened -= Workspace_WorkspaceOpened;
            Shell.Workspace.NodeOpened -= Workspace_NodeOpened;
        }

        #endregion Protected Methods

        #region Private Methods

        private void BuildNavigationTree(Document document)
        {
            void BuildSubNodes(TreeNode treeNode, INode documentNode)
            {
                var currentNode = treeNode.Nodes.Add(documentNode.Title);
                currentNode.Tag = documentNode;
                currentNode.Name = documentNode.Id.ToString();

                foreach (var subDocumentNode in documentNode.ChildNodes)
                {
                    BuildSubNodes(currentNode, subDocumentNode);
                }
            }

            navigationTree.Nodes.Clear();
            var rootNode = navigationTree.Nodes.Add(document.Title);
            rootNode.Tag = document;
            rootNode.Name = document.Id.ToString();

            foreach (var documentNode in document.ChildNodes)
            {
                BuildSubNodes(rootNode, documentNode);
            }
        }

        private void Workspace_WorkspaceClosed(object sender, EventArgs e)
        {
            navigationTree.Nodes.Clear();
        }

        private void Workspace_WorkspaceCreated(object sender, WorkspaceCreatedEventArgs<Document> e)
        {
            BuildNavigationTree(e.Model);
        }

        private void Workspace_WorkspaceOpened(object sender, WorkspaceOpenedEventArgs<Document> e)
        {
            BuildNavigationTree(e.Model);
        }

        #endregion Private Methods
    }
}
