using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Controls.Editors
{
    public sealed class WebEditorKeyDownEventArgs : EventArgs
    {

        public WebEditorKeyDownEventArgs(PreviewKeyDownEventArgs eventData)
            : this(eventData, false)
        { }

        public WebEditorKeyDownEventArgs(PreviewKeyDownEventArgs eventData, bool cancelPreviewKeyDownEvent)
        {
            EventData = eventData;
            CancelPreviewKeyDownEvent = cancelPreviewKeyDownEvent;
        }

        public bool CancelPreviewKeyDownEvent { get; set; }

        public PreviewKeyDownEventArgs EventData { get; }
    }
}
