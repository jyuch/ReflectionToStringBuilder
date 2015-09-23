// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// Specifics whether <see cref="ToStringBuilder"/> includes property or field.
    /// </summary>
    public enum TargetType
    {
        /// <summary>
        /// Include property.
        /// </summary>
        Property,

        /// <summary>
        /// Include field.
        /// </summary>
        Field,

        /// <summary>
        /// Include property and field.
        /// </summary>
        Both
    }
}
