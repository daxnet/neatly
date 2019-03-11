namespace Neatly.Windows
{
    partial class DocumentNavigatorWindow
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
            this.documentOutlineTree = new Aga.Controls.Tree.TreeViewAdv();
            this.SuspendLayout();
            // 
            // documentOutlineTree
            // 
            this.documentOutlineTree.BackColor = System.Drawing.SystemColors.Window;
            this.documentOutlineTree.DefaultToolTipProvider = null;
            this.documentOutlineTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentOutlineTree.DragDropMarkColor = System.Drawing.Color.Black;
            this.documentOutlineTree.LineColor = System.Drawing.SystemColors.ControlDark;
            this.documentOutlineTree.Location = new System.Drawing.Point(0, 0);
            this.documentOutlineTree.Model = null;
            this.documentOutlineTree.Name = "documentOutlineTree";
            this.documentOutlineTree.SelectedNode = null;
            this.documentOutlineTree.Size = new System.Drawing.Size(800, 450);
            this.documentOutlineTree.TabIndex = 0;
            this.documentOutlineTree.Text = "treeViewAdv1";
            // 
            // DocumentNavigatorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.documentOutlineTree);
            this.Name = "DocumentNavigatorWindow";
            this.Text = "Document Navigator";
            this.ResumeLayout(false);

        }

        #endregion

        private Aga.Controls.Tree.TreeViewAdv documentOutlineTree;
    }
}