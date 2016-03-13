// Copyright (c) 2015-2016 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class IgnorePropertyTests
    {
        [TestMethod]
        public void IgnoreNullProperty()
        {
            var property1Value = " ";
            var source = new DualPropertyClass() { Property1 = property1Value, Property2 = null };
            var config = new ToStringConfig<DualPropertyClass>() { IgnoreMode = IgnoreMemberMode.Null };
            var expected = $"{nameof(DualPropertyClass)}{{{nameof(DualPropertyClass.Property1)}={property1Value}}}";
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IgnoreWhiteSpaceProperty()
        {
            var source = new DualPropertyClass() { Property1 = " ", Property2 = null };
            var config = new ToStringConfig<DualPropertyClass>() { IgnoreMode = IgnoreMemberMode.NullOrWhiteSpace };
            var expected = $"{nameof(DualPropertyClass)}{{}}";
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IgnoreSpecifiedProperty()
        {
            var property1Value = "Property1";
            var property2Value = "Property2";
            var source = new DualPropertyClass() { Property1 = property1Value, Property2 = property2Value };
            var expected =
                $"{nameof(DualPropertyClass)}{{" +
                $"{nameof(DualPropertyClass.Property1)}={property1Value}}}";
            var config = new ToStringConfig<DualPropertyClass>();
            config.SetIgnoreMember(it => it.Property2);
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
