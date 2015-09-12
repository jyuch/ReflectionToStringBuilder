// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class BasicMapTests
    {
        [TestMethod]
        public void BasicMapping()
        {
            var propertyValue = "Property";
            var fieldValue = "Field";
            var source = new PropertyAndFieldClass();
            source.Property = propertyValue;
            source.Field = fieldValue;
            var expected =
                $"{nameof(PropertyAndFieldClass)}{{" +
                $"{nameof(PropertyAndFieldClass.Property)}={propertyValue}," +
                $"{nameof(PropertyAndFieldClass.Field)}={fieldValue}}}";
            var map = new PropertyAndFieldClassMap();
            var actual = map.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        private class PropertyAndFieldClassMap : ToStringMap<PropertyAndFieldClass>
        {
            public PropertyAndFieldClassMap()
            {
                Map(it => it.Property);
                Map(it => it.Field);
            }
        }
    }
}
