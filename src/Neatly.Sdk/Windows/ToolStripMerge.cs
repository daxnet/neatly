using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neatly.Sdk.Windows
{
    /// <summary>
    /// Represents the merging strategy of a <see cref="System.Windows.Forms.ToolStrip"/> object. 
    /// </summary>
    public class ToolStripMerge
    {
        /// <summary>
        /// Initializes a new instance of the <c>ToolStripMerge</c> class.
        /// </summary>
        /// <param name="toolStrip"></param>
        public ToolStripMerge(ToolStrip toolStrip)
            : this(toolStrip, true) { }

        /// <summary>
        /// Initializes a new instance of the <c>ToolStripMerge</c> class.
        /// </summary>
        /// <param name="toolStrip">The tool strip that needs to be merged to the main tool strip.</param>
        /// <param name="needHide">A <see cref="bool"/> value which indicates whether the menu strip should be hidden when
        /// the hosting window is hidden.</param>
        public ToolStripMerge(ToolStrip toolStrip, bool needHide)
        {
            ToolStrip = toolStrip;
            NeedHide = needHide;
        }

        /// <summary>
        /// Gets a <see cref="bool"/> value which indicates whether the menu strip should be hidden when
        /// the hosting window is hidden.
        /// </summary>
        public bool NeedHide { get; }

        /// <summary>
        /// Gets the tool strip that needs to be merged to the main tool strip.
        /// </summary>
        public ToolStrip ToolStrip { get; }
    }
}
