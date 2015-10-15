using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class ExpandIEnumerableTests
    {
        [TestMethod]
        public void ExpandIEnumerable()
        {
            var e1p1 = "Property11";
            var e1p2 = "Property12";
            var e2p1 = "Property21";
            var e2p2 = "Property22";
            var e1 = new DualPropertyClass2() { Property1 = e1p1, Property2 = e1p2 };
            var e2 = new DualPropertyClass2() { Property1 = e2p1, Property2 = e2p2 };
            var source = new IncludingIEnumerable()
            {
                Property1 = new DualPropertyClass2[] { e1, e2 }
            };
            var expected =
                $"{nameof(IncludingIEnumerable)}{{" +
                $"{nameof(IncludingIEnumerable.Property1)}=[" +
                $"{e1.ToString()},{e2.ToString()}]}}";
            var config = new ToStringConfig<IncludingIEnumerable>() { ExpandIEnumerable = true };
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        class DualPropertyClass2 : DualPropertyClass
        {
            public override string ToString()
            {
                return ToStringBuilder.ToString(this);
            }
        }

        class IncludingIEnumerable
        {
            public IEnumerable<DualPropertyClass2> Property1 { get; set; }
        }
    }
}
