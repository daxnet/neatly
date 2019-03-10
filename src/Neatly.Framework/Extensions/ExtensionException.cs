using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Framework.Extensions
{
    public sealed class ExtensionException : Exception
    {
        public ExtensionException(string message)
            : base(message) { }
    }
}
