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
            txtTitle.Text = $"NewDocument_{DateTime.Now.ToString("yyyyMMddhhmmss")}";
            txtAuthor.Text = Environment.UserName;

            txtTitle.SelectAll();
        }
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            Document.Title = txtTitle.Text;
            Document.Description = txtDescription.Text;
            Document.Author = txtAuthor.Text;

            //var chap1 = new DocumentNode(Document, "abc");
            //var chap11 = new DocumentNode(chap1, "def");
            //var chap111 = new DocumentNode(chap11, "ghi");

            //var chap2 = new DocumentNode(Document, "111");
            //var chap22 = new DocumentNode(chap2, "222");
            //var chap222 = new DocumentNode(chap22, "333");
        }
    }
}
