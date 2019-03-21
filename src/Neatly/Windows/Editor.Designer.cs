namespace Neatly.Windows
{
    partial class Editor
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
            this.webEditor = new Neatly.Controls.Editors.WebEditor();
            this.SuspendLayout();
            // 
            // webEditor
            // 
            this.webEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webEditor.IsWebBrowserContextMenuEnabled = false;
            this.webEditor.Location = new System.Drawing.Point(0, 0);
            this.webEditor.MinimumSize = new System.Drawing.Size(20, 20);
            this.webEditor.Name = "webEditor";
            this.webEditor.Size = new System.Drawing.Size(800, 450);
            this.webEditor.TabIndex = 0;
            this.webEditor.HtmlContentChanged += new System.EventHandler(this.WebEditor_HtmlContentChanged);
            this.webEditor.EditorKeyDown += new System.EventHandler<Neatly.Controls.Editors.WebEditorKeyDownEventArgs>(this.WebEditor_EditorKeyDown);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webEditor);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Editor";
            this.Text = "Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Editors.WebEditor webEditor;
    }
}