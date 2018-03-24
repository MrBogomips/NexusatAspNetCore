using System;
using System.Collections.Generic;
using System.Text;

namespace Nexusat.AspNetCore.Builders
{
    /// <summary>
    /// A builder that can produce only one object
    /// </summary>
    public interface ISingleInstanceBuilder: IBuilder
    {
        /// <summary>
        /// Return <c>true</c> if you can use the builder to construct the object, <c>false</c> if the object
        /// has already been built
        /// </summary>
        bool IsBuilderValid { get; }
    }
}
