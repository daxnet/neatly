using Neatly.DocumentModel;
using Neatly.Framework;
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
        #region Private Fields

        private readonly ActionComponentManager actions;
        private readonly WindowTools tools;
        private bool isDoubleClick;

        #endregion Private Fields

        #region Public Constructors

        public DocumentNavigator(INeatlyShell shell)
            : base(shell)
        {
            InitializeComponent();

            actions = new ActionComponentManager(this,
                new[]
                {
                    new ActionComponent(Constants.AddArticleAction, this, mnuAddArticle, tbtnAddArticle, Action_AddArticle, Keys.Control | Keys.Shift | Keys.A)
                });

            tools = new WindowTools(new ToolStripMerge(toolStrip), new[] { new MenuStripMerge(ctxArticles, MenuMergePosition.Edit) });
        }

        #endregion Public Constructors

        #region Protected Properties

        protected override WindowTools Tools => tools;

        #endregion Protected Properties

        #region Protected Methods

        protected override void Cleanup()
        {
            base.Cleanup();

            actions.Dispose();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            actions[Constants.AddArticleAction].Enabled = false;
        }
        protected override void OnWorkspaceClosed(object sender, EventArgs e)
        {
            navigationTree.Nodes.Clear();
            actions[Constants.AddArticleAction].Enabled = false;
        }

        protected override void OnWorkspaceCreated(object sender, WorkspaceCreatedEventArgs<Document> e)
        {
            BuildNavigationTree(e.Model);
            var newArticleNode = e.Model.GetChildDocumentNodesByTitle("New Article").First();
            SelectNodeOnNavigationTree(newArticleNode);
        }

        protected override void OnWorkspaceDocumentNodeAdded(object sender, DocumentNodeAddedEventArgs e)
        {
            var parentTreeNode = FindTreeNode(e.Parent);
            var childTreeNode = CreateNavigationTreeNode(e.Node);
            parentTreeNode.Nodes.Add(childTreeNode);
            parentTreeNode.Expand();
            navigationTree.SelectedNode = childTreeNode;
            Shell.Workspace.OpenNode(e.Node);
        }

        protected override void OnWorkspaceOpened(object sender, WorkspaceOpenedEventArgs<Document> e)
        {
            BuildNavigationTree(e.Model);
        }

        #endregion Protected Methods

        #region Private Methods

        private void Action_AddArticle(object sender, EventArgs e)
        {
            var parentNode = navigationTree.SelectedNode.Tag as INode;
            Shell.Workspace.AddDocumentNode(parentNode, "New Article", string.Empty);
        }

        private void BuildNavigationTree(Document document)
        {
            void BuildSubNodes(TreeNode treeNode, INode documentNode)
            {
                var currentNode = CreateNavigationTreeNode(documentNode);
                treeNode.Nodes.Add(currentNode);

                foreach (var subDocumentNode in documentNode.ChildNodes)
                {
                    BuildSubNodes(currentNode, subDocumentNode);
                }
            }

            navigationTree.Nodes.Clear();
            var rootNode = CreateNavigationTreeNode(document);
            navigationTree.Nodes.Add(rootNode);
            
            foreach (var documentNode in document.ChildNodes)
            {
                BuildSubNodes(rootNode, documentNode);
            }
        }
        private TreeNode FindTreeNode(INode node) => this.navigationTree.Nodes.Find(node.Id.ToString(), true).FirstOrDefault();

        private void navigationTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            actions[Constants.AddArticleAction].Enabled = true;
        }

        private void navigationTree_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            if (isDoubleClick &&
                e.Action == TreeViewAction.Collapse &&
                e.Node?.Tag is INode n && n.Type == NodeType.DocumentNode)
            {
                e.Cancel = true;
            }
        }

        private void navigationTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (isDoubleClick &&
                e.Action == TreeViewAction.Expand &&
                e.Node?.Tag is INode n && n.Type == NodeType.DocumentNode)
            {
                e.Cancel = true;
            }
        }

        private void navigationTree_MouseDown(object sender, MouseEventArgs e)
        {
            isDoubleClick = e.Clicks > 1;
        }

        private void navigationTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != null && e.Node.Tag is INode n && n.Type == NodeType.DocumentNode)
            {
                Shell.Workspace.OpenNode(n);
            }
        }

        private void SelectNodeOnNavigationTree(INode node)
        {
            var treeNode = FindTreeNode(node);
            if (treeNode != null)
            {
                if (treeNode.Parent != null)
                {
                    treeNode.Parent.Expand();
                }

                navigationTree.SelectedNode = treeNode;
            }
        }

        private TreeNode CreateNavigationTreeNode(INode node)
        {
            var treeNode = new TreeNode(node.Title)
            {
                Tag = node,
                Name = node.Id.ToString()
            };

            switch (node)
            {
                case DocumentNode dn when dn.Type == NodeType.DocumentNode:
                    treeNode.ImageKey = treeNode.StateImageKey = treeNode.SelectedImageKey = "article";
                    break;
                case Document doc when doc.Type == NodeType.Document:
                    treeNode.ImageKey = treeNode.StateImageKey = treeNode.SelectedImageKey = "document";
                    break;
            }

            return treeNode;
        }

        #endregion Private Methods
    }
}
