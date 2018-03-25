using System;
using System.Collections.Generic;
using System.Text;
using Nexusat.AspNetCore.Exceptions;
using Nexusat.AspNetCore.Models;
using Nexusat.AspNetCore.Properties;
using static Nexusat.AspNetCore.Utils.StringFormatter;

namespace Nexusat.AspNetCore.Builders
{
    internal abstract class ApiResponseBuilderBase: IApiResponseBuilderBase
    {
        protected readonly IApiResponseInternal _response;
        private IApiResponseInternal Response  => _response;

        public bool IsBuilderValid => SingleInstanceChecker.IsBuilderValid;
        protected readonly BuilderSingleInstanceChecker SingleInstanceChecker = new BuilderSingleInstanceChecker();

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
            SingleInstanceChecker.CheckBuildStateWhileBuilding();  // TODO: OPTIMIZABLE
            Response.Status.HttpCode = code;
        }

        protected void InternalSetStatusCodeSuccess()
        {
            SingleInstanceChecker.CheckBuildStateWhileBuilding();
            Response.Status.SetSuccessCode();
        }

        protected void InternalSetStatusCodeFailed()
        {
            SingleInstanceChecker.CheckBuildStateWhileBuilding();
            Response.Status.SetFailedCode();
        }

        protected void InternalSetStatusCodeSuccess(string subcode)
        {
            SingleInstanceChecker.CheckBuildStateWhileBuilding();
            Response.Status.SetSuccessCode(subcode);
        }

        protected void InternalSetStatusCodeFailed(string subcode)
        {
            SingleInstanceChecker.CheckBuildStateWhileBuilding();
            Response.Status.SetFailedCode(subcode);
        }

        protected void InternalSetException(Exception exception)
        {
            SingleInstanceChecker.CheckBuildStateWhileBuilding();
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            Response.Exception = ExceptionInfo.GetFromException(exception);
        }

        
    }
}
