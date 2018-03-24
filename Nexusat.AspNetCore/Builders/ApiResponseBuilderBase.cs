using System;
using System.Collections.Generic;
using System.Text;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Properties;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Builders
{
    internal abstract class ApiResponseBuilderBase //: IApiResponseBuilderBase
    {
        protected readonly IApiResponseInternal _response;
        private bool _isObjectBuilt = false;

        private IApiResponseInternal Response  => _response;
        public bool IsBuilderValid => !_isObjectBuilt;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response">Subclasses must provided a concrete type compatible with the builder class</param>
        protected ApiResponseBuilderBase(IApiResponse response) 
        {
            if (response == null) throw new ArgumentNullException(nameof(response));
            _response = response as IApiResponseInternal 
                ?? throw new ArgumentException(
                    FormatSystemMessage(ExceptionMessages.IApiResponseInternalNotImplemented), 
                    nameof(response));
        }

        protected void InternalSetHttpCode(int code)
        {
            CheckBuildStateWhileBuilding();  // TODO: OPTIMIZABLE
            Response.Status.HttpCode = code;
        }

        protected void InternalSetStatusCodeSuccess()
        {
            CheckBuildStateWhileBuilding();
            Response.Status.SetSuccessCode();
        }

        protected void InternalSetStatusCodeFailed()
        {
            CheckBuildStateWhileBuilding();
            Response.Status.SetFailedCode();
        }

        protected void InternalSetStatusCodeSuccess(string subcode)
        {
            CheckBuildStateWhileBuilding();
            Response.Status.SetSuccessCode(subcode);
        }

        protected void InternalSetStatusCodeFailed(string subcode)
        {
            CheckBuildStateWhileBuilding();
            Response.Status.SetFailedCode(subcode);
        }

        protected void InternalSetException(Exception exception)
        {
            CheckBuildStateWhileBuilding();
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            Response.Exception = ExceptionInfo.GetFromException(exception);
        }

        /// <summary>
        /// Check if an object was already built.
        /// In case of misuses throw an exception
        /// </summary>
        protected void CheckBuildStateWhileBuilding()
        {
            if (_isObjectBuilt)
            {
                throw new BuilderInvalidStateException(FormatSystemMessage(ExceptionMessages.BuilderInvalidStateAlreadyUsed));
            }
        }

        protected void CheckBuildStateForFinalBuild()
        {
            CheckBuildStateWhileBuilding();
            _isObjectBuilt = true;
        }
    }
}
