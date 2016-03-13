// Copyright (c) 2015-2016 jyuch
// Released under the MIT license
// https://github.com/jyuch/ReflectionToStringBuilder/blob/master/LICENSE

namespace Jyuch.ReflectionToStringBuilder.Tests
{
    class NonePropertyClass
    {
    }

    class SinglePropertyClass
    {
        public string Property1 { get; set; }
    }

    class DualPropertyClass
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }

    class TriplePropertyClass
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
    }

    class SingleFieldClass
    {
        public string Field1;
    }

    class PropertyAndFieldClass
    {
        public string Property { set; get; }
        public string Field;
    }
}
