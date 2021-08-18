using System;

namespace Hoge
{
    using System.Linq;

    public class VmCls2 : System.Object
    {
        int _pa;

        @ignore
        public int PropertyA
        {
            set => _pa = value;

            get
            {
                return _pa;
            }
        }

        public string PropertyB
        {
            set => PropertyA = Int32.Parse(value);
        }

        public string PropertyC
        {
            get
            {
                return PropertyA.ToString();
            }
        }

        public int PropertyD {
            set;
            set;
            get;
            get;
        }
    }
}