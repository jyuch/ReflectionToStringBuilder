// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// プロパティの<c>null</c>もしくは空白で無視するモードを列挙します。
    /// </summary>
    public enum IgnoreMemberMode
    {
        /// <summary>
        /// <c>null</c>もしくは空白にかかわらずすべてのプロパティを文字列形式に含めます。
        /// </summary>
        None,
        
        /// <summary>
        /// プロパティが<c>null</c>の場合は文字列形式に含めません。
        /// </summary>
        Null,

        /// <summary>
        /// プロパティが<c>null</c>もしくは空白のの場合は文字列形式に含めません。
        /// </summary>
        NullOrWhiteSpace
    }
}
