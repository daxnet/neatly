using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    /// <summary>
    /// Represents the object that contains the event data that was generated when a document
    /// node has been added to its parent node.
    /// </summary>
    /// <seealso cref="Neatly.DocumentModel.DocumentNodeEventArgs" />
    public sealed class DocumentNodeAddedEventArgs : DocumentNodeEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentNodeAddedEventArgs"/> class.
        /// </summary>
        /// <param name="parent">The parent node to which the document node had been added.</param>
        /// <param name="addingNode">The adding document node.</param>
        public DocumentNodeAddedEventArgs(INode parent, DocumentNode addingNode)
            : base(addingNode)
        {
            Parent = parent;
        }

        /// <summary>
        /// Gets the parent node to which the document node had been added.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public INode Parent { get; }
    }
}
