using Neatly.Framework.Workspaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    public sealed class NeatlyWorkspace : Workspace<Document>
    {
        protected override string WorkspaceFileDescription => throw new NotImplementedException();

        protected override string WorkspaceFileExtension => throw new NotImplementedException();

        protected override Document Create()
        {
            throw new NotImplementedException();
        }

        protected override Document OpenFromFile(string fileName)
        {
            throw new NotImplementedException();
        }

        protected override void SaveToFile(Document model, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
