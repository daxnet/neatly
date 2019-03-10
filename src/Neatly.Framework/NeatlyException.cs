using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Neatly.Framework
{
    public class NeatlyException : Exception
    {
        public NeatlyException() : base() { }

        public NeatlyException(string message) : base(message) { }

        public NeatlyException(string message, Exception innerException) : base(message, innerException) { }

        protected NeatlyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
