using Neatly.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.DocumentModel
{
    public sealed class DocumentException : NeatlyException
    {
        public DocumentException(string message) : base(message) { }
    }
}
