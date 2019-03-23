﻿using Neatly.DocumentModel;
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
        private readonly WindowTools tools;
        private readonly ActionComponentManager actions;

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

            Shell.Workspace.WorkspaceInitializing += Workspace_WorkspaceInitializing;
            Shell.Workspace.WorkspaceCreated += Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceOpened += Workspace_WorkspaceOpened;
            Shell.Workspace.WorkspaceClosed += Workspace_WorkspaceClosed;
        }

        private void Workspace_WorkspaceInitializing(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        #endregion Public Constructors

        #region Protected Properties

        protected override WindowTools Tools => tools;

        #endregion Protected Properties

        #region Protected Methods

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            
        }

        #endregion Protected Methods

        #region Private Methods

        private void Action_AddArticle(object sender, EventArgs e)
        {
            
        }

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
            var newArticleNode = e.Model.GetChildDocumentNodesByTitle("New Article").First();
            SelectNodeOnNavigationTree(newArticleNode);
        }

        private void Workspace_WorkspaceOpened(object sender, WorkspaceOpenedEventArgs<Document> e)
        {
            BuildNavigationTree(e.Model);
        }

        private void SelectNodeOnNavigationTree(INode node)
        {
            var treeNode = this.navigationTree.Nodes.Find(node.Id.ToString(), true).FirstOrDefault();
            if (treeNode != null)
            {
                if (treeNode.Parent != null)
                {
                    treeNode.Parent.Expand();
                }

                navigationTree.SelectedNode = treeNode;
            }
        }

        #endregion Private Methods

        protected override void Cleanup()
        {
            Shell.Workspace.WorkspaceInitializing -= Workspace_WorkspaceInitializing;
            Shell.Workspace.WorkspaceCreated -= Workspace_WorkspaceCreated;
            Shell.Workspace.WorkspaceClosed -= Workspace_WorkspaceClosed;
            Shell.Workspace.WorkspaceOpened -= Workspace_WorkspaceOpened;

            actions.Dispose();
        }
    }
}
