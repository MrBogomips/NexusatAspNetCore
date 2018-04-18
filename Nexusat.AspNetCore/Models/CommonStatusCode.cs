using System;
namespace Nexusat.AspNetCore.Models
{
    public static class CommonStatusCodes
    {
        /*
        * ATTENTION!!!!! 
        * 
        * Initialization order for static fields matters!!!
        * 
        * Never declare a static field that depends on a field declared later on!
        * Even with strings order matters!
        */
        public const string OK = "OK";
        public const string KO = "KO";
        internal const string OK_ = OK + "_";
        internal const string KO_ = KO + "_";

        /// <summary>
        /// The default status code used in case of successful operations
        /// </summary>
        public const string DEFAULT_OK_STATUS_CODE = OK + "_DEFAULT";
        /// <summary>
        /// The default status code used in case of failed operations
        /// </summary>
        public const string DEFAULT_KO_STATUS_CODE = KO + "_DEFAULT";
        /// <summary>
        /// The NotFound status code.
        /// </summary>
        public const string NOT_FOUND_STATUS_CODE = KO + "_NOT_FOUND";
        /// <summary>
        /// The BadRequest status code.
        /// </summary>
        public const string BAD_REQUEST_STATUS_CODE = KO + "_BAD_REQUEST";
        /// <summary>
        /// The unhandled exception status code.
        /// </summary>
        public const string UNHANDLED_EXCEPTION_STATUS_CODE = KO + "_UNHANDLED_EXCEPTION";

        /// <summary>
        /// The default status code used in case of ambigous operations not specifically configured by the user
        /// </summary>
        public const string DEFAULT_UNK_STATUS_CODE = DEFAULT_KO_STATUS_CODE + "_UNK";
        /// <summary>
        /// This status code should never be served to the client
        /// </summary>
        internal const string OK_INTERNAL_PIZZA = OK + "_INTERNAL_PIZZA";
    }
}
