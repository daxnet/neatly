using Neatly.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Framework
{
    /// <summary>
    /// Represents the component that manages <see cref="ToolStripMenuItem"/> and <see cref="ToolStripButton"/>
    /// in a single action.
    /// </summary>
    public sealed class ActionComponent : Component
    {

        #region Private Fields

        private readonly EventHandler clickHandler;
        private readonly List<ToolStripItem> managedToolStrips = new List<ToolStripItem>();
        private readonly Control parentControl;
        private bool @checked;
        private bool disposed;
        private bool enabled;
        private Keys? shortcutKeys;
        private object tag;
        private string text;
        private string tooltipText;
        private bool visible;

        #endregion Private Fields

        #region Public Constructors

        public ActionComponent(string name,
            Control parentControl, ToolStripMenuItem menuItem, string tooltipText, EventHandler clickHandler,
            Keys? shortcutKeys = null)
            : this(name, parentControl, new ToolStripItem[] { menuItem }, clickHandler, shortcutKeys)
        {
            this.TooltipText = tooltipText;
        }

        public ActionComponent(string name,
            Control parentControl, 
            ToolStripMenuItem menuItem, 
            ToolStripButton button, 
            EventHandler clickHandler,
            Keys? shortcutKeys = null)
            : this(name, parentControl, new ToolStripItem[] { menuItem, button }, clickHandler, shortcutKeys)
        { }

        public ActionComponent(string name,
            Control parentControl, 
            ToolStripMenuItem menuItem, 
            ToolStripButton button, 
            string tooltipText, 
            EventHandler clickHandler,
            Keys? shortcutKeys = null)
            : this(name, parentControl, menuItem, button, clickHandler, shortcutKeys)
        {
            this.TooltipText = tooltipText;
        }

        public ActionComponent(string name,
            Control parentControl, 
            IEnumerable<ToolStripItem> managedToolStrips, 
            EventHandler clickHandler, 
            Keys? shortcutKeys = null)
        {
            Name = name;
            this.parentControl = parentControl;
            this.managedToolStrips.AddRange(managedToolStrips);
            this.clickHandler = clickHandler;
            this.managedToolStrips.ForEach(ts => ts.Click += ExecuteClickHandler);
            this.ShortcutKeys = shortcutKeys;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool Checked
        {
            get => @checked;
            set
            {
                // The Checked property is not defined in the base class.
                this.managedToolStrips.ForEach(ts =>
                {
                    switch (ts)
                    {
                        case ToolStripMenuItem menuItem:
                            menuItem.Checked = value;
                            break;
                        case ToolStripButton button:
                            button.Checked = value;
                            break;
                        default: break;
                    }
                });

                @checked = value;
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

        public string Name { get; }

        public Keys? ShortcutKeys
        {
            get => shortcutKeys;
            set
            {
                if (shortcutKeys != value && value.HasValue)
                {
                    this.managedToolStrips.Where(t => t is ToolStripMenuItem)
                        .Select(t => t as ToolStripMenuItem)
                        .ToList()
                        .ForEach(t => t.ShortcutKeys = value ?? Keys.None);

                    shortcutKeys = value;
                }
                else
                {
                    this.managedToolStrips.Where(t => t is ToolStripMenuItem)
                        .Select(t => t as ToolStripMenuItem)
                        .ToList()
                        .ForEach(t => t.ShortcutKeys = Keys.None);
                }
            }
        }

        public object Tag
        {
            get => tag;
            set
            {
                this.managedToolStrips.ForEach(ts => ts.Tag = value);
                tag = value;
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

        public bool Visible
        {
            get => visible;
            set
            {
                this.managedToolStrips.ForEach(ts => ts.Visible = value);
                visible = value;
            }
        }

        #endregion Public Properties

        #region Public Methods

        public override bool Equals(object obj)
        {
            return obj is ActionComponent component &&
                   Name == component.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public void Invoke() => ExecuteClickHandler(this, EventArgs.Empty);

        public override string ToString() => Name;

        #endregion Public Methods

        #region Protected Methods

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

        #endregion Protected Methods

        #region Private Methods

        private void ExecuteClickHandler(object sender, EventArgs e)
        {

            using (new LengthyOperation(this.parentControl))
            {
                this.clickHandler(sender, e);
            }
        }

        #endregion Private Methods
    }
}
