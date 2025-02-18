﻿using System.Collections;
using System.Collections.Generic;

namespace TesteICI.Infra.CrossCutting.Security.Constants
{
    /// <summary>
    /// X-Frame-Options-related constants.
    /// </summary>
    public static class FrameOptionsConstant
    {
        /// <summary>
        /// The header value for X-Frame-Options
        /// </summary>
        public const string Header = "X-Frame-Options";

        /// <summary>
        /// The page cannot be displayed in a frame, regardless of the site attempting to do so.
        /// </summary>
        public const string Deny = "DENY";

        /// <summary>
        /// The page can only be displayed in a frame on the same origin as the page itself.
        /// </summary>
        public const string SameOrigin = "SAMEORIGIN";

        /// <summary>
        /// The page can only be displayed in a frame on the specified origin. {0} specifies the format string
        /// </summary>
        public const string AllowFromUri = "ALLOW-FROM {0}";
    }
}
