using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    public class NodeEventArgs : EventArgs
    {
        public NodeEventArgs(INode node) => Node = node;

        public INode Node { get; }
    }
}
