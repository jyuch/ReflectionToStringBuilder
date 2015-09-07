// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// オブジェクトの文字列形式を生成するときの設定を表します。
    /// </summary>
    /// <typeparam name="T">出力対象となるオブジェクトの型。</typeparam>
    public sealed class ToStringConfig<T>
    {
        private HashSet<PropertyInfo> _ignoreProperty = new HashSet<PropertyInfo>();

        /// <summary>
        /// プロパティを無視するモードを指定します。既定値は<see cref="IgnoreProperty.None"/>です。
        /// </summary>
        public IgnorePropertyMode IgnoreMode { get; set; } = IgnorePropertyMode.None;

        internal HashSet<PropertyInfo> IgnoreProperty
        {
            get
            {
                return _ignoreProperty;
            }
        }

        /// <summary>
        /// オブジェクトから文字列形式を生成するときに無視するプロパティを指定します。
        /// </summary>
        /// <param name="expression">無視するプロパティ。</param>
        public void SetIgnoreProperty(Expression<Func<T, object>> expression)
        {
            _ignoreProperty.Add(ReflectionHelper.GetProperty(expression));
        }
    }
}
