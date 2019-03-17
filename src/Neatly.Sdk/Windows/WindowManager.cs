using Neatly.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeifenLuo.WinFormsUI.Docking;

namespace Neatly.Sdk.Windows
{
    public sealed class WindowManager : Component
    {
        private readonly INeatlyShell shell;
        private readonly List<BaseWindow> managedWindows = new List<BaseWindow>();
        private bool disposed;

        public event EventHandler<WindowHiddenEventArgs> WindowHidden;
        public event EventHandler<WindowShownEventArgs> WindowShown;

        public WindowManager(INeatlyShell shell) => this.shell = shell;

        public TWindow CreateWindow<TWindow>()
            where TWindow : BaseWindow
        {
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow), this.shell);
            window.DockWindowShown += Window_DockWindowShown;
            window.DockWindowHidden += Window_DockWindowHidden;
            this.managedWindows.Add(window);
            return window;
        }

        private void Window_DockWindowHidden(object sender, EventArgs e)
        {
            WindowHidden?.Invoke(sender, new WindowHiddenEventArgs((BaseWindow)sender));
        }

        private void Window_DockWindowShown(object sender, EventArgs e)
        {
            WindowShown?.Invoke(sender, new WindowShownEventArgs((BaseWindow)sender));
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
                    window.DockWindowHidden -= Window_DockWindowHidden;
                    window.DockWindowShown -= Window_DockWindowShown;
                    window.Dispose();
                }

                managedWindows.Clear();
                disposed = true;
            }
        }
    }
}
