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
		public const string OK_DEFAULT = OK_ + "DEFAULT";
        /// <summary>
        /// The default status code used in case of failed operations
        /// </summary>
		public const string KO_DEFAULT = KO_ + "DEFAULT";
        /// <summary>
        /// The NotFound status code.
        /// </summary>
		public const string NOT_FOUND = KO_ + "NOT_FOUND";
        /// <summary>
        /// The BadRequest status code.
        /// </summary>
		public const string BAD_REQUEST = KO_ + "BAD_REQUEST";
        /// <summary>
        /// The unhandled exception status code.
        /// </summary>
		public const string UNHANDLED_EXCEPTION = KO_ + "UNHANDLED_EXCEPTION";
        /// <summary>
        /// The unsupported media status code.
        /// </summary>
		public const string UNSUPPORTED_MEDIA_TYPE = KO_ + "UNSUPPORTED_MEDIA_TYPE";

        /// <summary>
        /// The default status code used in case of ambigous operations not specifically configured by the user
        /// </summary>
        public const string UNK_DEFAULT = KO_DEFAULT + "_UNK";
        /// <summary>
        /// This status code should never be served to the client
        /// </summary>
		internal const string OK_INTERNAL_PIZZA = OK_ + "INTERNAL_PIZZA";


    }
}
