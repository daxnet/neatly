using Neatly.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Windows
{
    internal sealed class WindowManager : Component
    {
        private readonly INeatlyShell shell;
        private readonly List<BaseWindow> managedWindows = new List<BaseWindow>();
        private bool disposed;

        internal WindowManager(INeatlyShell shell) => this.shell = shell;

        public TWindow CreateWindow<TWindow>()
            where TWindow : BaseWindow
        {
            var window = (TWindow)Activator.CreateInstance(typeof(TWindow), this.shell);
            this.managedWindows.Add(window);
            return window;
        }

        public TWindow CreateOrReuseWindow<TWindow>()
            where TWindow : BaseWindow
        {
            var window = managedWindows.FirstOrDefault(w => w.GetType() == typeof(TWindow));
            return (TWindow) window ?? CreateWindow<TWindow>();
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                foreach(var window in managedWindows)
                {
                    window.Dispose();
                }

                managedWindows.Clear();
                disposed = true;
            }
        }
    }
}
