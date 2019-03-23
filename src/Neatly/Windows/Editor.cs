using Neatly.Controls.Editors;
using Neatly.DocumentModel;
using Neatly.Sdk;
using Neatly.Sdk.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Windows
{
    public partial class Editor : BaseWindow
    {
        private readonly DocumentNode documentNode;

        public Editor(INeatlyShell shell, INode node)
            : base(shell, false)
        {
            InitializeComponent();

            if (node is DocumentNode docn)
            {
                documentNode = docn;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = documentNode.Title;
        }

        private void WebEditor_HtmlContentChanged(object sender, EventArgs e)
        {
            documentNode.Content = webEditor.HtmlContent;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
        }

        private void WebEditor_EditorKeyDown(object sender, WebEditorKeyDownEventArgs e)
        {
            var actionComponent = Shell.ActionComponents.Get(e.EventData.KeyData, false);
            if (actionComponent != null)
            {
                actionComponent.Invoke();
                
            }

            e.CancelPreviewKeyDownEvent = true;
        }
    }
}
