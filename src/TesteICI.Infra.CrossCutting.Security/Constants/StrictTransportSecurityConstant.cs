﻿using System.Collections;
using System.Collections.Generic;

namespace TesteICI.Infra.CrossCutting.Security.Constants
{
    /// <summary>
    /// Strict-Transport-Security-related constants.
    /// </summary>
    public static class StrictTransportSecurityConstant
    {
        /// <summary>
        /// Header value for Strict-Transport-Security
        /// </summary>
        public const string Header = "Strict-Transport-Security";

        /// <summary>
        /// Tells the user-agent to cache the domain in the STS list for the provided number of seconds {0} 
        /// </summary>
        public const string MaxAge = "max-age={0}";

        /// <summary>
        /// Tells the user-agent to cache the domain in the STS list for the provided number of seconds {0} and include any sub-domains.
        /// </summary>
        public const string MaxAgeIncludeSubdomains = "max-age={0}; includeSubDomains";

        /// <summary>
        /// Tells the user-agent to remove, or not cache the host in the STS cache.
        /// </summary>
        public const string NoCache = "max-age=0";
    }
}
