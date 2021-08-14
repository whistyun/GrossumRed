using System;

namespace Hoge
{
    public class VmCls : System.Object
    {
        public int PropertyA { get; set; }
        public string PropertyB { get; set; }

        class InnerVmCls
        {
            public List<string> PropertyB { get; set; }
        }
    }
}