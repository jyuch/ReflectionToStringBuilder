using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jyuch.ReflectionToStringBuilder
{
    public sealed class ToStringConfig<T>
    {
        public IgnorePropertyMode IgnoreMode { get; set; }

        public ToStringConfig()
        {
            IgnoreMode = IgnorePropertyMode.None;
        }

        public static ToStringConfig<T> Default
        {
            get
            {
                return new ToStringConfig<T>();
            }
        }
    }

    public enum IgnorePropertyMode
    {
        None,
        Null,
        NullOrWhiteSpace
    }
}
