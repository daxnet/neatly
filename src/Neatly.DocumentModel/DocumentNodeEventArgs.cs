using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    public class DocumentNodeEventArgs : NodeEventArgs
    {
        public DocumentNodeEventArgs(DocumentNode documentNode)
            : base(documentNode)
        {
         //   DocumentNode = documentNode;
        }

        // public DocumentNode DocumentNode { get; }
    }
}
