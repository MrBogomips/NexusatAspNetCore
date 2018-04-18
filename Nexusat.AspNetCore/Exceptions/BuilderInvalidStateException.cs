using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Exceptions
{
    /// <summary>
    /// Thrown by a single instance builder used to produce more than one object.
    /// <see cref="Builders.ISingleInstanceBuilder"/>.
    /// </summary>
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
