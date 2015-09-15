using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    [TestClass]
    public class IgnoreStaticMemberTests
    {
        [TestMethod]
        public void IgnoreStaticMember()
        {
            var staticPropertyValue = "StaticProperty";
            var property1Value = "Property1";
            HasStaticMemberClass.StaticProperty1 = staticPropertyValue;
            var source = new HasStaticMemberClass() { Property1 = property1Value };
            var expected = 
                $"{nameof(HasStaticMemberClass)}{{" +
                $"{nameof(HasStaticMemberClass.Property1)}={property1Value}}}";
            var actual = ToStringBuilder.ToString(source);

            Console.WriteLine(actual);
            Assert.AreEqual(expected, actual);
        }
    }

    class HasStaticMemberClass
    {
        public static string StaticProperty1 {get;set;}
        public string Property1 { get; set; }
    }
}
