// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Jyuch.ReflectionToStringBuilder
{
    public abstract class ToStringMap<T>
    {
        private List<ToStringPropertyMap> _target = new List<ToStringPropertyMap>();

        protected ToStringPropertyMap Map(Expression<Func<T, object>> expr)
        {
            var p = new ToStringPropertyMap(typeof(T), ReflectionHelper.GetMember(expr));
            _target.Add(p);
            return p;
        }

        public string ToString(T obj)
        {
            return $"{typeof(T).Name}{{{string.Join(",", _target.Where(it => !it.IsIgnore).Select(it => it.ToStringMember(obj)))}}}";
        }
    }
}
