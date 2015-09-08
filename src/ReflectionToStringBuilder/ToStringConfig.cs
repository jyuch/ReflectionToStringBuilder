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
        private HashSet<MemberInfo> _ignoreMember = new HashSet<MemberInfo>();

        /// <summary>
        /// プロパティを無視するモードを指定します。既定値は<see cref="IgnoreMemberMode.None"/>です。
        /// </summary>
        public IgnoreMemberMode IgnoreMode { get; set; } = IgnoreMemberMode.None;

        /// <summary>
        /// 出力する型を指定します。既定値は<see cref="TargetType.Property"/>です。
        /// </summary>
        public TargetType OutputTarget { get; set; } = TargetType.Property;

        internal HashSet<MemberInfo> IgnoreMember
        {
            get
            {
                return _ignoreMember;
            }
        }

        /// <summary>
        /// オブジェクトから文字列形式を生成するときに無視するプロパティを指定します。
        /// </summary>
        /// <param name="expression">無視するプロパティ。</param>
        public void SetIgnoreMember(Expression<Func<T, object>> expression)
        {
            _ignoreMember.Add(ReflectionHelper.GetMember(expression));
        }
    }
}
