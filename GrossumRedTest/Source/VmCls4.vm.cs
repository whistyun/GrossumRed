using System;

namespace Hoge
{
    using System.Linq;
    delegate void Gamma();
    delegate void Delta(string hoge);

    public interface IVmCls
    {
        public int Property0 { get; set; }
        public int Property1 { get; set; }

        void A();
    }

    public class VmCls2 : System.Object
    {
        delegate void Alpha();

        delegate void Beta(string hoge);

        public int PropertyD
        {

            set;
            get;
        }
    }
}