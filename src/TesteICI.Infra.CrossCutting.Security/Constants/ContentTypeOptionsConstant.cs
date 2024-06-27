using System.Collections;
using System.Collections.Generic;

namespace TesteICI.Infra.CrossCutting.Security.Constants
{
    /// <summary>
    /// X-Content-Type-Options-related constants.
    /// </summary>
    public static class ContentTypeOptionsConstant
    {
        /// <summary>
        /// Header value for X-Content-Type-Options
        /// </summary>
        public const string Header = "X-Content-Type-Options";

        /// <summary>
        /// Disables content sniffing
        /// </summary>
        public const string NoSniff = "nosniff";
    }
}
