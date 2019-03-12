using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Framework.Workspaces
{
    public delegate (bool, TModel) WorkspaceModelEnricher<TModel>(TModel input) where TModel : IWorkspaceModel;
}
