using Neatly.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Framework
{
    public sealed class ActionComponentManager : ComponentManager<ActionComponent>, IActionComponentProvider
    {
        #region Private Fields

        private readonly Control parent;

        #endregion Private Fields

        #region Public Constructors

        public ActionComponentManager(Control parent)
        {
            this.parent = parent;
        }

        public ActionComponentManager(Control parent, IEnumerable<ActionComponent> components)
            : base(components)
        {
            this.parent = parent;
        }

        #endregion Public Constructors

        #region Public Methods

        public ActionComponent Add(string name, ToolStripMenuItem menuItem, ToolStripButton button, string tooltipText, EventHandler clickHandler, Keys? shortcutKeys = null)
        {
            var action = new ActionComponent(name, parent, menuItem, button, tooltipText, clickHandler, shortcutKeys);
            if (Contains(action))
            {
                throw new InvalidOperationException("The action component has already been defined.");
            }

            Add(action);
            return action;
        }

        public ActionComponent Add(string name, ToolStripMenuItem menuItem, string tooltipText, EventHandler clickHandler, Keys? shortcutKeys = null)
        {
            var action = new ActionComponent(name, parent, menuItem, tooltipText, clickHandler, shortcutKeys);
            if (Contains(action))
            {
                throw new InvalidOperationException("The action component has already been defined.");
            }

            Add(action);
            return action;
        }

        public ActionComponent this[string name] => Get(name);

        /// <summary>
        /// Gets the action component by name.
        /// </summary>
        /// <param name="name">The name of the component.</param>
        /// <returns>The action component that has the specified name.</returns>
        public ActionComponent Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var actionComponent = components.FirstOrDefault(c => string.Equals(c.Name, name));
            if (actionComponent == null)
            {
                throw new InvalidOperationException("The action component has not been defined.");
            }

            return actionComponent;
        }

        public ActionComponent Get(Keys shortcutKeys, bool throwIfNotFound = true)
        {
            var actionComponent = components.FirstOrDefault(c => c.ShortcutKeys != null && c.ShortcutKeys.Value.Equals(shortcutKeys));
            if (actionComponent == null && throwIfNotFound)
            {
                throw new InvalidOperationException("The action component has not been defined.");
            }

            return actionComponent;
        }

        #endregion Public Methods
    }
}
