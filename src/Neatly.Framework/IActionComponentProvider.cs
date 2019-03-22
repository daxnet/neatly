using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Framework
{
    public interface IActionComponentProvider
    {
        #region Public Methods

        ActionComponent Get(string name);
        ActionComponent Get(Keys shortcutKeys, bool throwIfNotFound = true);

        #endregion Public Methods
    }
}
