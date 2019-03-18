using Neatly.DocumentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly
{
    public partial class FrmCreateDocument : Form
    {
        public FrmCreateDocument(Document document)
            : this()
        {
            this.Document = document;
        }

        private FrmCreateDocument()
        {
            InitializeComponent();
        }

        public Document Document { get; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtTitle.Text = $"New Document";
            txtAuthor.Text = Environment.UserName;

            txtTitle.SelectAll();
        }
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            Document.Title = txtTitle.Text;
            Document.Description = txtDescription.Text;
            Document.Author = txtAuthor.Text;

            Document.Add(new DocumentNode("New Article", string.Empty));
        }
    }
}
