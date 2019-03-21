using Neatly.Framework;
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
    public sealed class WindowManager : ComponentManager<BaseWindow>
    {
        private readonly INeatlyShell shell;
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
            this.Add(window);
            return window;
        }

        public TWindow CreateWindow<TWindow>(params object[] additionalArgs)
            where TWindow : BaseWindow
        {
            var parms = new List<object> { shell };
            parms.AddRange(additionalArgs);
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow), parms.ToArray());
            window.DockWindowShown += Window_DockWindowShown;
            window.DockWindowHidden += Window_DockWindowHidden;
            this.Add(window);
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
            var window = components.FirstOrDefault(w => w.GetType() == typeof(TWindow));
            return (TWindow)window ?? CreateWindow<TWindow>();
        }

        public TWindow CreateOrReuseWindow<TWindow>(params object[] additionalArgs)
            where TWindow : BaseWindow
        {
            var window = components.FirstOrDefault(w => w.GetType() == typeof(TWindow));
            return (TWindow)window ?? CreateWindow<TWindow>(additionalArgs);
        }

        public IEnumerable<TWindow> GetWindows<TWindow>()
            where TWindow : BaseWindow
        {
            return (IEnumerable<TWindow>)components.Where(w => w.GetType() == typeof(TWindow));
        }

        public IEnumerable<BaseWindow> GetWindows(Type windowType)
        {
            return components.Where(w => w.GetType() == windowType);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                foreach (var window in components)
                {
                    window.DockWindowHidden -= Window_DockWindowHidden;
                    window.DockWindowShown -= Window_DockWindowShown;
                }

                base.Dispose(disposing);
                disposed = true;
            }
        }
    }
}
