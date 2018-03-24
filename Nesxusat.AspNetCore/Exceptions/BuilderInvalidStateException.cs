using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Exceptions
{
    [Serializable]
    public class BuilderInvalidStateException : InvalidOperationException, IFrameworkException
    {
        public BuilderInvalidStateException() { }
        public BuilderInvalidStateException(string message) : base(message) { }
        public BuilderInvalidStateException(string message, Exception inner) : base(message, inner) { }
        protected BuilderInvalidStateException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
