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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentNavigator));
            this.navigationTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // navigationTree
            // 
            this.navigationTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navigationTree.HideSelection = false;
            this.navigationTree.Location = new System.Drawing.Point(0, 0);
            this.navigationTree.Name = "navigationTree";
            this.navigationTree.Size = new System.Drawing.Size(618, 509);
            this.navigationTree.TabIndex = 0;
            // 
            // DocumentNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 509);
            this.Controls.Add(this.navigationTree);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DocumentNavigator";
            this.Text = "Document Navigator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView navigationTree;
    }
}