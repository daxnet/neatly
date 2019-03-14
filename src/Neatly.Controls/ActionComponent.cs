using Neatly.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Controls
{
    public sealed class ActionComponent : Component
    {
        private readonly Control parentControl;
        private readonly List<ToolStripItem> managedToolStrips = new List<ToolStripItem>();
        private readonly EventHandler clickHandler;
        private bool enabled;
        private bool visible;
        private string text;
        private string tooltipText;
        private bool disposed;

        public ActionComponent(Control parentControl, ToolStripMenuItem menuItem, string tooltipText, EventHandler clickHandler)
            : this(parentControl, new ToolStripItem[] { menuItem }, clickHandler)
        {
            this.TooltipText = tooltipText;
        }

        public ActionComponent(Control parentControl, ToolStripButton button, string tooltipText, EventHandler clickHandler)
            : this(parentControl, new ToolStripItem[] { button }, clickHandler)
        {
            this.TooltipText = tooltipText;
        }

        public ActionComponent(Control parentControl, ToolStripMenuItem menuItem, ToolStripButton button, EventHandler clickHandler)
            : this(parentControl, new ToolStripItem[] { menuItem, button }, clickHandler)
        { }

        public ActionComponent(Control parentControl, ToolStripMenuItem menuItem, ToolStripButton button, string tooltipText, EventHandler clickHandler)
            : this(parentControl, menuItem, button, clickHandler)
        {
            this.TooltipText = tooltipText;
        }

        public ActionComponent(Control parentControl, ToolStripMenuItem menuItem, ToolStripButton button, string text, string tooltipText, EventHandler clickHandler)
            : this(parentControl, menuItem, button, clickHandler)
        {
            this.Text = text;
            this.TooltipText = tooltipText;
        }

        public ActionComponent(Control parentControl, IEnumerable<ToolStripItem> managedToolStrips, EventHandler clickHandler)
        {
            this.parentControl = parentControl;
            this.managedToolStrips.AddRange(managedToolStrips);
            this.clickHandler = clickHandler;
            this.managedToolStrips.ForEach(ts => ts.Click += ExecuteClickHandler);
        }

        private void ExecuteClickHandler(object sender, EventArgs e)
        {
            using (new LengthyOperation(this.parentControl))
            {
                this.clickHandler(sender, e);
            }
        }

        public bool Enabled
        {
            get => enabled;
            set
            {
                this.managedToolStrips.ForEach(ts => ts.Enabled = value);
                enabled = value;
            }
        }

        public bool Visible
        {
            get => visible;
            set
            {
                this.managedToolStrips.ForEach(ts => ts.Visible = value);
                visible = value;
            }
        }

        public string Text
        {
            get => text;
            set
            {
                this.managedToolStrips.ForEach(ts => ts.Text = value);
                text = value;
            }
        }

        public string TooltipText
        {
            get => tooltipText;
            set
            {
                this.managedToolStrips.ForEach(ts => ts.ToolTipText = value);
                tooltipText = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.managedToolStrips.ForEach(ts => ts.Click -= ExecuteClickHandler);
                }

                disposed = true;
            }
        }
    }
}
