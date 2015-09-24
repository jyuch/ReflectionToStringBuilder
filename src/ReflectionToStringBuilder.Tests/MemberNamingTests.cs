// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class MemberNamingTests
    {
        [TestMethod]
        public void MemberNaming()
        {
            var property1Value = "Property";
            var source = new SinglePropertyClass() { Property1 = property1Value };
            var expected =
                $"{nameof(SinglePropertyClass)}{{" +
                $"NamedProperty={property1Value}}}";
            var map = new MemberNamingMap();
            var actual = map.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        private class MemberNamingMap : ToStringMap<SinglePropertyClass>
        {
            public MemberNamingMap()
            {
                Map(it => it.Property1).Name("NamedProperty");
            }
        }
    }
}
