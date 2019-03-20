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
            this.webEditor1 = new Neatly.Controls.WebEditor();
            this.SuspendLayout();
            // 
            // webEditor1
            // 
            this.webEditor1.ChangeCheckingInterval = 0;
            this.webEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webEditor1.Location = new System.Drawing.Point(0, 0);
            this.webEditor1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webEditor1.Name = "webEditor1";
            this.webEditor1.Size = new System.Drawing.Size(800, 450);
            this.webEditor1.TabIndex = 0;
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.webEditor1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Editor";
            this.Text = "Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WebEditor webEditor1;
    }
}