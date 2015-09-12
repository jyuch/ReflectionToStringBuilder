// Copyright (c) 2015 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class SwitchPropertyAndFieldTests
    {
        [TestMethod]
        public void OutputProperty()
        {
            var property1Value = "Property1";
            var field1Value = "Field1";
            var source = new PropertyAndFieldClass() { Property = property1Value };
            source.Field = field1Value;
            var expected =
                $"{nameof(PropertyAndFieldClass)}{{" +
                $"{nameof(PropertyAndFieldClass.Property)}={property1Value}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputField()
        {
            var property1Value = "Property1";
            var field1Value = "Field1";
            var source = new PropertyAndFieldClass() { Property = property1Value };
            source.Field = field1Value;
            var expected =
                $"{nameof(PropertyAndFieldClass)}{{" +
                $"{nameof(PropertyAndFieldClass.Field)}={field1Value}}}";
            var config = new ToStringConfig<PropertyAndFieldClass>()
            {
                OutputTarget = TargetType.Field
            };
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void OutputBoth()
        {
            var property1Value = "Property1";
            var field1Value = "Field1";
            var source = new PropertyAndFieldClass() { Property = property1Value };
            source.Field = field1Value;
            var expected =
                $"{nameof(PropertyAndFieldClass)}{{" +
                $"{nameof(PropertyAndFieldClass.Property)}={property1Value}," +
                $"{nameof(PropertyAndFieldClass.Field)}={field1Value}}}";
            ;
            var config = new ToStringConfig<PropertyAndFieldClass>()
            {
                OutputTarget = TargetType.Both
            };
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
