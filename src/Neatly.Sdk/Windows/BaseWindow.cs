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
            this.HideOnClose = true;
        }

        public event EventHandler DockWindowShown;

        public event EventHandler DockWindowHidden;

        protected BaseWindow(INeatlyShell shell)
            : this()
        {
            this.Shell = shell;
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

        public override string ToString()
        {
            return this.Text;
        }
    }
}
