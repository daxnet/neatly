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
    public partial class FrmCreateNewDocument : Form
    {
        public FrmCreateNewDocument(Document document)
            : this()
        {
            this.Document = document;
        }

        private FrmCreateNewDocument()
        {
            InitializeComponent();
        }

        public Document Document { get; }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            txtTitle.Text = $"NewDocument_{DateTime.Now.ToString("yyyyMMddhhmmss")}";
            txtAuthor.Text = Environment.UserName;

            txtTitle.SelectAll();
        }
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            Document.Title = txtTitle.Text;
            Document.Description = txtDescription.Text;
            Document.Author = txtAuthor.Text;
        }
    }
}
