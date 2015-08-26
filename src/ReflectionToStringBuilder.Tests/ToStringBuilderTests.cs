using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jyuch.ReflectionToStringBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass()]
    public class ToStringBuilderTests
    {
        [TestMethod()]
        public void ProcessSinglePropertyClass()
        {
            var property1Value = "Property1";
            var source = new SinglePropertyClass() { Property1 = property1Value };
            var expected = $"{nameof(SinglePropertyClass)}{{{nameof(SinglePropertyClass.Property1)}={property1Value}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(actual, expected);
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
            Assert.AreEqual(actual, expected);
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
            Assert.AreEqual(actual, expected);
        }

        [TestMethod()]
        public void ProcessNullProperty()
        {
            string property1Value = null;
            var source = new SinglePropertyClass() { Property1 = property1Value };
            var expected = $"{nameof(SinglePropertyClass)}{{{nameof(SinglePropertyClass.Property1)}={string.Empty}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(actual, expected);
        }

        private class SinglePropertyClass
        {
            public string Property1 { get; set; }
        }

        private class DualPropertyClass
        {
            public string Property1 { get; set; }
            public string Property2 { get; set; }
        }

        private class TriplePropertyClass
        {
            public string Property1 { get; set; }
            public string Property2 { get; set; }
            public string Property3 { get; set; }
        }
    }
}