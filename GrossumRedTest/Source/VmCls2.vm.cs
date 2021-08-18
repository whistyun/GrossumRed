using System;

namespace Hoge
{
    using System.Linq;

    public class VmCls2 : System.Object
    {
        private double _innerField1;
        private double _innerField2 = 12;
        private double _innerField3 = System.Linq.Enumerable(0, 10).Select((itm, idx) =>
        {
            if (itm % 2 == 0 && idx % 4 == 0)
            {
                return itm;
            }
            else
            {
                return itm + idx;
            }
        });
        public event Action<string> ViewEvent;

        public int PropertyA { get; set; }

        void PrintText1(string txt)
        {
            int idx = 0;
            foreach (var ch in txt)
            {
                Console.WriteLine($"char[{idx++}]: \"{ch}\"\"");
            }
            // }
            /* } */
            var ch1 = '}';
            var txt1 = @"""}";
            var txt2 = "}";
        }

        @callback PropertyBChanged
        public string PropertyB { get; set; }

        void PrintText2(string txt) => PrintText1(txt);

        @ignore
        public int PropertyC { set; get; }

    }
}