namespace Neatly.Windows
{
    partial class DocumentNavigator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentNavigator));
            this.navigationTree = new System.Windows.Forms.TreeView();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtnAddArticle = new System.Windows.Forms.ToolStripButton();
            this.ctxArticles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAddArticle = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip.SuspendLayout();
            this.ctxArticles.SuspendLayout();
            this.SuspendLayout();
            // 
            // navigationTree
            // 
            this.navigationTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationTree.HideSelection = false;
            this.navigationTree.Location = new System.Drawing.Point(0, 25);
            this.navigationTree.Name = "navigationTree";
            this.navigationTree.Size = new System.Drawing.Size(388, 445);
            this.navigationTree.TabIndex = 0;
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnAddArticle});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(388, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // tbtnAddArticle
            // 
            this.tbtnAddArticle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnAddArticle.Image = global::Neatly.Properties.Resources.page_add;
            this.tbtnAddArticle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnAddArticle.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.tbtnAddArticle.MergeIndex = 3;
            this.tbtnAddArticle.Name = "tbtnAddArticle";
            this.tbtnAddArticle.Size = new System.Drawing.Size(23, 22);
            this.tbtnAddArticle.Text = "Add Article";
            // 
            // ctxArticles
            // 
            this.ctxArticles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAddArticle});
            this.ctxArticles.Name = "ctxArticles";
            this.ctxArticles.Size = new System.Drawing.Size(181, 48);
            // 
            // mnuAddArticle
            // 
            this.mnuAddArticle.Name = "mnuAddArticle";
            this.mnuAddArticle.Size = new System.Drawing.Size(180, 22);
            this.mnuAddArticle.Text = "&Add Article";
            // 
            // DocumentNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 470);
            this.Controls.Add(this.navigationTree);
            this.Controls.Add(this.toolStrip);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentNavigator";
            this.Text = "Document Navigator";
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ctxArticles.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView navigationTree;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton tbtnAddArticle;
        private System.Windows.Forms.ContextMenuStrip ctxArticles;
        private System.Windows.Forms.ToolStripMenuItem mnuAddArticle;
    }
}