using Neatly.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace Neatly.Windows
{
    internal sealed class WindowManager : Component
    {
        private readonly INeatlyShell shell;
        private readonly List<BaseWindow> managedWindows = new List<BaseWindow>();
        private bool disposed;

        public event EventHandler<WindowHiddenEventArgs> WindowHidden;
        public event EventHandler<WindowShownEventArgs> WindowShown;

        internal WindowManager(INeatlyShell shell) => this.shell = shell;

        public TWindow CreateWindow<TWindow>()
            where TWindow : BaseWindow
        {
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow), this.shell);
            window.DockStateChanged += Window_DockStateChanged;
            this.managedWindows.Add(window);
            return window;
        }

        private void Window_DockStateChanged(object sender, EventArgs e)
        {
            if (sender is BaseWindow window)
            {
                switch (window.DockState)
                {
                    case DockState.Hidden:
                        this.WindowHidden?.Invoke(sender, new WindowHiddenEventArgs(window));
                        break;
                    case DockState.Unknown:
                        break;
                    default:
                        this.WindowShown?.Invoke(sender, new WindowShownEventArgs(window));
                        break;
                }
            }
        }

        public TWindow CreateOrReuseWindow<TWindow>()
            where TWindow : BaseWindow
        {
            var window = managedWindows.FirstOrDefault(w => w.GetType() == typeof(TWindow));
            return (TWindow)window ?? CreateWindow<TWindow>();
        }

        public IEnumerable<TWindow> GetWindows<TWindow>()
            where TWindow : BaseWindow
        {
            return (IEnumerable<TWindow>)managedWindows.Where(w => w.GetType() == typeof(TWindow));
        }

        public IEnumerable<BaseWindow> GetWindows(Type windowType)
        {
            return managedWindows.Where(w => w.GetType() == windowType);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                foreach (var window in managedWindows)
                {
                    window.DockStateChanged -= Window_DockStateChanged;
                    window.Dispose();
                }

                managedWindows.Clear();
                disposed = true;
            }
        }
    }
}
