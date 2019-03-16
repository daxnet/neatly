using Neatly.DocumentModel;
using Neatly.Framework.Workspaces;
using Neatly.Sdk;
using System;
using System.Windows.Forms;

namespace Neatly.Windows
{
    public partial class DocumentNavigator : BaseWindow, IDocumentNavigator
    {
        public DocumentNavigator(INeatlyShell shell)
            : base(shell)
        {
            InitializeComponent();
            
            Shell.Workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceOpened += Workspace_WorkspaceOpened;
            Shell.Workspace.WorkspaceClosed += Workspace_WorkspaceClosed;
        }

        private void Workspace_WorkspaceOpened(object sender, WorkspaceOpenedEventArgs<Document> e)
        {
            BuildNavigationTree(e.Model);
        }

        private void Workspace_WorkspaceClosed(object sender, EventArgs e)
        {
            navigationTree.Nodes.Clear();
        }

        private void Workspace_WorkspaceCreated(object sender, WorkspaceCreatedEventArgs<Document> e)
        {
            BuildNavigationTree(e.Model);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Shell.Workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
            Shell.Workspace.WorkspaceOpened -= Workspace_WorkspaceOpened;
        }

        private void BuildNavigationTree(Document document)
        {
            void BuildSubNodes(TreeNode treeNode, INode documentNode)
            {
                var currentNode = treeNode.Nodes.Add(documentNode.Title);
                currentNode.Tag = documentNode;

                foreach (var subDocumentNode in documentNode.ChildNodes)
                {
                    BuildSubNodes(currentNode, subDocumentNode);
                }
            }

            navigationTree.Nodes.Clear();
            var rootNode = navigationTree.Nodes.Add(document.Title);
            rootNode.Tag = document;

            foreach (var documentNode in document.ChildNodes)
            {
                BuildSubNodes(rootNode, documentNode);
            }
        }
    }
}
