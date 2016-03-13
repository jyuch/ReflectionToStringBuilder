// Copyright (c) 2015-2016 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class IgnoreMap
    {
        [TestMethod]
        public void SpecificIgnoreMemberUsingMap()
        {
            var property1Value = "Property1";
            var property2Value = "Property2";
            var source = new DualPropertyClass();
            source.Property1 = property1Value;
            source.Property2 = property2Value;
            var expected =
                $"{nameof(DualPropertyClass)}{{" +
                $"{nameof(DualPropertyClass.Property1)}={property1Value}}}";
            var map = new IgnoreMemberMap();
            var actual = map.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        private class IgnoreMemberMap : ToStringMap<DualPropertyClass>
        {
            public IgnoreMemberMap()
            {
                Map(it => it.Property1);
                Map(it => it.Property2).Ignore();
            }
        }
    }
}
