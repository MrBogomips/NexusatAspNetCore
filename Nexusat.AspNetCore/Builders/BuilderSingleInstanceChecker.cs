using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Properties;
using static Nexusat.AspNetCore.Utils.StringFormatter;
using System;
using System.Collections.Generic;
using System.Text;


namespace Nexusat.AspNetCore.Builders
{
    /// <summary>
    /// Utility class to support single instance builders
    /// </summary>
    internal sealed class BuilderSingleInstanceChecker: ISingleInstanceBuilder
    {
        private bool _isObjectBuilt = false;
        public bool IsBuilderValid => !_isObjectBuilt;

        /// <summary>
        /// Check if an object was already built.
        /// In case of misuses throw an exception
        /// </summary>
        public void CheckBuildStateWhileBuilding()
        {
            if (_isObjectBuilt)
            {
                throw new BuilderInvalidStateException(FormatSystemMessage(ExceptionMessages.BuilderInvalidStateAlreadyUsed));
            }
        }

        public void CheckBuildStateForFinalBuild()
        {
            //CheckBuildStateWhileBuilding();
            _isObjectBuilt = true;
        }
    }
}
