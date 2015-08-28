using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class IgnorePropertyTests
    {
        [TestMethod]
        public void IgnoreNullProperty()
        {
            var source = new DualPropertyClass() { Property1 = string.Empty, Property2 = null };
            var config = new ToStringConfig<DualPropertyClass>() { IgnoreMode = IgnorePropertyMode.Null };
            var expected = $"{nameof(DualPropertyClass)}{{{nameof(DualPropertyClass.Property1)}=}}";
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IgnoreWhiteSpaceProperty()
        {
            var source = new DualPropertyClass() { Property1 = " ", Property2 = null };
            var config = new ToStringConfig<DualPropertyClass>() { IgnoreMode = IgnorePropertyMode.NullOrWhiteSpace };
            var expected = $"{nameof(DualPropertyClass)}{{}}";
            var actual = ToStringBuilder.ToString(source, config);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }
    }
}
