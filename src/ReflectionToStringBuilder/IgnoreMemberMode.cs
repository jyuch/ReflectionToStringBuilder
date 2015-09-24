// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// Specifics whether exclude null or whitespace member.
    /// </summary>
    public enum IgnoreMemberMode
    {
        /// <summary>
        /// Include all member to string.
        /// </summary>
        None,

        /// <summary>
        /// Exclude <c>null</c> member.
        /// </summary>
        Null,

        /// <summary>
        /// Exclude <c>null</c> or whitespace member.
        /// </summary>
        NullOrWhiteSpace
    }
}
