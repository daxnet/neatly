using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    /// <summary>
    /// Represents the document nodes that have navigation properties
    /// </summary>
    public interface INode
    {
        Guid Id { get; set; }

        string Title { get; }

        NodeType Type { get; }

        DateTime DateCreated { get; set; }

        DateTime? DateLastModified { get; set; }

        INode Parent { get; }

        IEnumerable<INode> ChildNodes { get; }

        void Add(DocumentNode documentNode);
    }
}
