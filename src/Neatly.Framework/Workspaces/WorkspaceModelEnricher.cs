using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Framework.Workspaces
{
    public delegate TModel WorkspaceModelEnricher<TModel>(in TModel input) where TModel : IWorkspaceModel;
}
