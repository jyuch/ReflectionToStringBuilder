﻿// Copyright (c) 2015-2016 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass()]
    public class BasicToStringTests
    {
        [TestMethod()]
        public void ProcessNonePropertyClass()
        { 
            var source = new NonePropertyClass();
            var expected = $"{nameof(NonePropertyClass)}{{}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected,actual);
        }

        [TestMethod()]
        public void ProcessSinglePropertyClass()
        {
            var property1Value = "Property1";
            var source = new SinglePropertyClass() { Property1 = property1Value };
            var expected = $"{nameof(SinglePropertyClass)}{{{nameof(SinglePropertyClass.Property1)}={property1Value}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProcessDualPropertyClass()
        {
            var property1Value = "Property1";
            var property2Value = "Property2";
            var source = new DualPropertyClass() { Property1 = property1Value, Property2 = property2Value };
            var expected = 
                $"{nameof(DualPropertyClass)}{{" + 
                $"{nameof(DualPropertyClass.Property1)}={property1Value}," + 
                $"{nameof(DualPropertyClass.Property2)}={property2Value}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProcessTriplePropertyClass()
        {
            var property1Value = "Property1";
            var property2Value = "Property2";
            var property3Value = "Property3";
            var source = new TriplePropertyClass()
            {
                Property1 = property1Value,
                Property2 = property2Value,
                Property3 = property3Value
            };
            var expected =
                $"{nameof(TriplePropertyClass)}{{" +
                $"{nameof(TriplePropertyClass.Property1)}={property1Value}," +
                $"{nameof(TriplePropertyClass.Property2)}={property2Value}," +
                $"{nameof(TriplePropertyClass.Property3)}={property3Value}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProcessNullProperty()
        {
            string property1Value = null;
            var source = new SinglePropertyClass() { Property1 = property1Value };
            var expected = $"{nameof(SinglePropertyClass)}{{{nameof(SinglePropertyClass.Property1)}={string.Empty}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ProcessOneFieldClass()
        {
            var field1Value = "Field1";
            var source = new SingleFieldClass();
            source.Field1 = field1Value;
            var expected = $"{nameof(SingleFieldClass)}{{{nameof(SingleFieldClass.Field1)}={field1Value}}}";
            var config = new ToStringConfig<SingleFieldClass>();
            config.OutputTarget = TargetType.Both;
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}