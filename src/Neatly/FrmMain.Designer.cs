namespace Neatly
{
    partial class FrmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.tbtnNewDocument = new System.Windows.Forms.ToolStripButton();
            this.tbtnOpenDocument = new System.Windows.Forms.ToolStripButton();
            this.tbtnSaveDocument = new System.Windows.Forms.ToolStripButton();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.mnuOpenDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSaveDocument = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            resources.ApplyResources(this.toolStripContainer.ContentPanel, "toolStripContainer.ContentPanel");
            resources.ApplyResources(this.toolStripContainer, "toolStripContainer");
            this.toolStripContainer.Name = "toolStripContainer";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.mainToolStrip);
            // 
            // mainToolStrip
            // 
            resources.ApplyResources(this.mainToolStrip, "mainToolStrip");
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnNewDocument,
            this.tbtnOpenDocument,
            this.tbtnSaveDocument});
            this.mainToolStrip.Name = "mainToolStrip";
            // 
            // tbtnNewDocument
            // 
            this.tbtnNewDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnNewDocument.Image = global::Neatly.Properties.Resources.page_white;
            resources.ApplyResources(this.tbtnNewDocument, "tbtnNewDocument");
            this.tbtnNewDocument.Name = "tbtnNewDocument";
            // 
            // tbtnOpenDocument
            // 
            this.tbtnOpenDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnOpenDocument.Image = global::Neatly.Properties.Resources.folder_page;
            resources.ApplyResources(this.tbtnOpenDocument, "tbtnOpenDocument");
            this.tbtnOpenDocument.Name = "tbtnOpenDocument";
            // 
            // tbtnSaveDocument
            // 
            this.tbtnSaveDocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnSaveDocument.Image = global::Neatly.Properties.Resources.disk;
            resources.ApplyResources(this.tbtnSaveDocument, "tbtnSaveDocument");
            this.tbtnSaveDocument.Name = "tbtnSaveDocument";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.Name = "mainMenu";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewDocument,
            this.mnuOpenDocument,
            this.toolStripMenuItem1,
            this.mnuSaveDocument});
            this.mnuFile.Name = "mnuFile";
            resources.ApplyResources(this.mnuFile, "mnuFile");
            // 
            // mnuNewDocument
            // 
            this.mnuNewDocument.Image = global::Neatly.Properties.Resources.page_white;
            this.mnuNewDocument.Name = "mnuNewDocument";
            resources.ApplyResources(this.mnuNewDocument, "mnuNewDocument");
            // 
            // statusStrip
            // 
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            // 
            // mnuOpenDocument
            // 
            this.mnuOpenDocument.Image = global::Neatly.Properties.Resources.folder_page;
            this.mnuOpenDocument.Name = "mnuOpenDocument";
            resources.ApplyResources(this.mnuOpenDocument, "mnuOpenDocument");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // mnuSaveDocument
            // 
            this.mnuSaveDocument.Image = global::Neatly.Properties.Resources.disk;
            this.mnuSaveDocument.Name = "mnuSaveDocument";
            resources.ApplyResources(this.mnuSaveDocument, "mnuSaveDocument");
            // 
            // FrmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStripContainer);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.mainMenu;
            this.Name = "FrmMain";
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton tbtnNewDocument;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripButton tbtnOpenDocument;
        private System.Windows.Forms.ToolStripButton tbtnSaveDocument;
        private System.Windows.Forms.ToolStripMenuItem mnuNewDocument;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenDocument;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveDocument;
    }
}

