using Neatly.Framework;
using Neatly.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Neatly.Sdk.Windows
{
    public partial class BaseWindow : DockContent
    {
        private BaseWindow()
        {
            InitializeComponent();
        }

        public event EventHandler DockWindowShown;

        public event EventHandler DockWindowHidden;

        protected BaseWindow(INeatlyShell shell, bool hideOnClose = true)
            : this()
        {
            Shell = shell;
            HideOnClose = hideOnClose;
        }

        protected override void OnDockStateChanged(EventArgs e)
        {
            switch (DockState)
            {
                case DockState.Hidden:
                    this.OnDockWindowHidden(e);
                    break;
                case DockState.Unknown:
                    break;
                default:
                    this.OnDockWindowShown(e);
                    break;
            }
        }

        protected INeatlyShell Shell { get; }

        protected virtual WindowTools Tools => WindowTools.Empty;

        internal protected virtual void Cleanup() { }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // If the setting is to close the window instead of hiding, we need
            // to cleanup the resources as the form is going to be closed and disposed.
            if (!HideOnClose)
            {
                Cleanup();
            }

            base.OnFormClosing(e);
        }

        protected virtual void OnDockWindowHidden(EventArgs e)
        {
            DockWindowHidden?.Invoke(this, e);
            if (!(Tools?.IsEmpty ?? true))
            {
                Shell.RevertMerge(Tools);
            }
        }

        protected virtual void OnDockWindowShown(EventArgs e)
        {
            DockWindowShown?.Invoke(this, e);
            if (!(Tools?.IsEmpty ?? true))
            {
                Shell.MergeTools(Tools);
            }
        }

        public override string ToString() => this.Text;
    }
}
