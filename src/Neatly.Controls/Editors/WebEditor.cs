using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Controls.Editors
{
    public sealed class WebEditor : WebBrowser
    {

        #region Private Fields

        private const int DefaultChangeCheckingInterval = 500;
        private const int DefaultShortcutKeyCheckingInterval = 200;

        private readonly Timer changeCheckingTimer = new Timer();
        private readonly Timer shortcutKeyCheckingTimer = new Timer();

        private bool disposed;
        private string originalText;

        private PreviewKeyDownEventArgs previewKeyDownEventArgs;

        #endregion Private Fields

        #region Public Constructors

        public WebEditor()
        {
            InitializeAsEditor();
            ChangeCheckingInterval = DefaultChangeCheckingInterval;
            changeCheckingTimer.Interval = ChangeCheckingInterval;
            changeCheckingTimer.Tick += ChangeCheckingTimer_Tick;
            changeCheckingTimer.Enabled = true;
            changeCheckingTimer.Start();

            ShortcutKeyCheckingInterval = DefaultShortcutKeyCheckingInterval;
            shortcutKeyCheckingTimer.Interval = ShortcutKeyCheckingInterval;
            shortcutKeyCheckingTimer.Tick += ShortcutKeyCheckingTimer_Tick;
            shortcutKeyCheckingTimer.Enabled = true;
            shortcutKeyCheckingTimer.Start();
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<WebEditorKeyDownEventArgs> EditorKeyDown;

        public event EventHandler HtmlContentChanged;

        #endregion Public Events

        #region Public Properties

        [Description("The interval of the timer, in milliseconds, for checking the changing of the content.")]
        [DefaultValue(DefaultChangeCheckingInterval)]
        public int ChangeCheckingInterval { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HtmlContent
        {
            get => DocumentText;
            set => DocumentText = value;
        }

        [Description("The interval of the timer, in milliseconds, for checking the shortcut key press on the editor.")]
        [DefaultValue(DefaultChangeCheckingInterval)]
        public int ShortcutKeyCheckingInterval { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.changeCheckingTimer.Stop();
                    this.changeCheckingTimer.Enabled = false;
                    this.changeCheckingTimer.Tick -= ChangeCheckingTimer_Tick;
                    this.changeCheckingTimer.Dispose();

                    this.shortcutKeyCheckingTimer.Stop();
                    this.shortcutKeyCheckingTimer.Enabled = false;
                    this.shortcutKeyCheckingTimer.Tick -= ShortcutKeyCheckingTimer_Tick;
                    this.shortcutKeyCheckingTimer.Dispose();
                }

                base.Dispose(disposing);

                disposed = true;
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            previewKeyDownEventArgs = e;
            base.OnPreviewKeyDown(e);
        }

        #endregion Protected Methods

        #region Private Methods

        private void ChangeCheckingTimer_Tick(object sender, EventArgs e)
        {


            // Note: accessing DocumentText property will result in the construction of the MemoryStream
            // object. From Reference Source, it looks like this MemoryStream object will never be getting
            // disposed. Not sure if there will be memory leak at this point. Assume that GC will take care
            // of this.
            if (!string.Equals(originalText, DocumentText, StringComparison.InvariantCulture))
            {
                originalText = DocumentText;
                OnTextChanged();
            }
        }

        private void InitializeAsEditor()
        {
            WebBrowserShortcutsEnabled = false;

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

        private void OnTextChanged()
        {
            HtmlContentChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ShortcutKeyCheckingTimer_Tick(object sender, EventArgs e)
        {
            if (previewKeyDownEventArgs != null && 
                (previewKeyDownEventArgs.KeyCode >= Keys.F1 && previewKeyDownEventArgs.KeyCode <= Keys.F24) || (
                previewKeyDownEventArgs.Modifiers != Keys.None &&
                previewKeyDownEventArgs.KeyCode != Keys.Control &&
                previewKeyDownEventArgs.KeyCode != Keys.ControlKey &&
                previewKeyDownEventArgs.KeyCode != Keys.Alt &&
                previewKeyDownEventArgs.KeyCode != Keys.Menu &&
                previewKeyDownEventArgs.KeyCode != Keys.Shift &&
                previewKeyDownEventArgs.KeyCode != Keys.ShiftKey))
            {
                var handler = EditorKeyDown;
                if (handler != null)
                {
                    try
                    {
                        shortcutKeyCheckingTimer.Stop();
                        var args = new WebEditorKeyDownEventArgs(previewKeyDownEventArgs);
                        handler(this, args);
                        if (args.CancelPreviewKeyDownEvent)
                        {
                            return;
                        }
                    }
                    finally
                    {
                        previewKeyDownEventArgs = null;
                        shortcutKeyCheckingTimer.Start();
                    }
                }
            }
        }

        #endregion Private Methods
    }
}
