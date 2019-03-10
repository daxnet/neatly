using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Framework.Workspaces
{
    public class WorkspaceException : Exception
    {
        public WorkspaceException(string message)
            : base(message)
        { }

        public WorkspaceException()
        { }
    }
}
