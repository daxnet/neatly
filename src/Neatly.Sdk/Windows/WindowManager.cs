using Neatly.Framework;
using Neatly.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Neatly.Sdk.Windows
{
    public sealed class WindowManager : ComponentManager<BaseWindow>
    {
        #region Private Fields

        private readonly INeatlyShell shell;
        private bool disposed;

        #endregion Private Fields

        #region Public Constructors

        public WindowManager(INeatlyShell shell) => this.shell = shell;

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<WindowHiddenEventArgs> WindowHidden;
        public event EventHandler<WindowShownEventArgs> WindowShown;

        #endregion Public Events

        #region Public Methods

        public void CloseWindows<TWindow>()
            where TWindow : BaseWindow
        {
            var windows = components.Where(c => c is TWindow).Select(c => (TWindow)c).ToList();
            foreach (var window in windows)
            {
                window.Close();
                // If the window is closed instead of hidden, means it has been disposed,
                // then remove it from the components list.
                //if (!window.HideOnClose)
                //{
                //    components.Remove(window);
                //}
            }
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

        public TWindow CreateWindow<TWindow>()
            where TWindow : BaseWindow
        {
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow), this.shell);
            window.DockWindowShown += Window_DockWindowShown;
            window.DockWindowHidden += Window_DockWindowHidden;
            window.FormClosed += Window_FormClosed;

            this.Add(window);
            return window;
        }

        private void Window_FormClosed(object sender, FormClosedEventArgs e)
        {
            var window = (BaseWindow)sender;
            if (!window.HideOnClose)
            {
                components.Remove(window);
            }
        }

        public TWindow CreateWindow<TWindow>(params object[] additionalArgs)
            where TWindow : BaseWindow
        {
            var parms = new List<object> { shell };
            parms.AddRange(additionalArgs);
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow), parms.ToArray());
            window.DockWindowShown += Window_DockWindowShown;
            window.DockWindowHidden += Window_DockWindowHidden;
            window.FormClosed += Window_FormClosed;

            this.Add(window);
            return window;
        }

        public IEnumerable<TWindow> GetWindows<TWindow>()
            where TWindow : BaseWindow
        {
            return components.Where(w => w is TWindow).Select(w => w as TWindow);
        }

        public IEnumerable<BaseWindow> GetWindows(Type windowType)
        {
            return components.Where(w => w.GetType() == windowType);
        }

        public IEnumerable<TWindow> GetWindows<TWindow>(Func<TWindow, bool> predicate)
            where TWindow : BaseWindow
        {
            return GetWindows<TWindow>().Where(predicate);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                foreach (var window in components)
                {
                    window.DockWindowHidden -= Window_DockWindowHidden;
                    window.DockWindowShown -= Window_DockWindowShown;
                    window.FormClosed -= Window_FormClosed;
                    if (window.HideOnClose)
                    {
                        window.Cleanup();
                    }
                }

                base.Dispose(disposing);
                disposed = true;
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private void Window_DockWindowHidden(object sender, EventArgs e)
        {
            WindowHidden?.Invoke(sender, new WindowHiddenEventArgs((BaseWindow)sender));
        }

        private void Window_DockWindowShown(object sender, EventArgs e)
        {
            WindowShown?.Invoke(sender, new WindowShownEventArgs((BaseWindow)sender));
        }

        #endregion Private Methods
    }
}
