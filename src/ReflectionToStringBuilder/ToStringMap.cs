// Copyright (c) 2015-2016 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// Maps class members to string-format.
    /// </summary>
    /// <typeparam name="T">The type of object generate a string-format.</typeparam>
    public abstract class ToStringMap<T>
    {
        private List<ToStringPropertyMap> _target = new List<ToStringPropertyMap>();

        /// <summary>
        /// Maps a member to string-format.
        /// </summary>
        /// <param name="expr">The member of mapping.</param>
        /// <returns></returns>
        protected ToStringPropertyMap Map(Expression<Func<T, object>> expr)
        {
            var p = new ToStringPropertyMap(typeof(T), ReflectionHelper.GetMember(expr));
            _target.Add(p);
            return p;
        }

        /// <summary>
        /// Generate string-format of specified instance.
        /// </summary>
        /// <param name="obj">The instance of generate a string-format.</param>
        /// <returns>The string-format of instance.</returns>
        public string ToString(T obj)
        {
            return $"{typeof(T).Name}{{{string.Join(",", _target.Where(it => !it.IsIgnore).Select(it => it.ToStringMember(obj)))}}}";
        }
    }
}
