using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Controls
{
    public sealed class WebEditor : WebBrowser
    {
        private const int DefaultChangeCheckingInterval = 20;
        private readonly Timer timer = new Timer();
        private bool disposed;
        private string originalText;

        public WebEditor()
        {
            InitializeAsEditor();
            ChangeCheckingInterval = DefaultChangeCheckingInterval;
            timer.Interval = ChangeCheckingInterval;
            timer.Tick += Timer_Tick;
            timer.Enabled = true;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //timer.Stop();
            if (!string.Equals(originalText, DocumentText))
            {
                originalText = DocumentText;
                OnTextChanged();
            }
        }

        private void OnTextChanged()
        {

        }

        [Description("The interval of the timer, in milliseconds, for checking the changing of the content.")]
        [DefaultValue(DefaultChangeCheckingInterval)]
        public int ChangeCheckingInterval { get; set; }

        private void InitializeAsEditor()
        {
            // It is necessary to add a body to the control before you can apply changes to the DOM document
            DocumentText = "<html><body></body></html>";
            if (Document != null)
            {
                var doc = Document.DomDocument as IHTMLDocument2;
                if (doc != null) doc.designMode = "On";

                // replace the context menu for the web browser control so the default IE browser context menu doesn't show up
                IsWebBrowserContextMenuEnabled = false;
                if (this.ContextMenuStrip == null)
                {
                    Document.ContextMenuShowing += (sender, e) => {; };
                }
            }

            originalText = DocumentText;
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.timer.Tick -= Timer_Tick;
                    this.timer.Dispose();
                }

                base.Dispose(disposing);

                disposed = true;
            }
        }
    }
}
