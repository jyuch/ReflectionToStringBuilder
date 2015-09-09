// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

namespace Jyuch.ReflectionToStringBuilder
{
    /// <summary>
    /// 文字列形式のもととなるメンバーの選択モードを列挙します。
    /// </summary>
    public enum TargetType
    {
        /// <summary>
        /// プロパティを文字列形式に含めます。
        /// </summary>
        Property,

        /// <summary>
        /// フィールドを文字列形式に含めます。
        /// </summary>
        Field,

        /// <summary>
        /// プロパティとフィールドの両方を文字列形式に含めます。
        /// </summary>
        Both
    }
}
