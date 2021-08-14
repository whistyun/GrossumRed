using System;

namespace Hoge
{
    public class VmCls2 : System.Object
    {
        private double _innerField1;
        private double _innerField2=12;
        public event Action<string> ViewEvent;

        public int PropertyA { get; set; }

        void PrintText1(string txt)
        {
            int idx = 0;
            foreach (var ch in txt)
            {
                Console.WriteLine($"char[{idx++}]: {ch}");
            }
        }

        public string PropertyB { get; set; }

        void PrintText2(string txt) => PrintText1(txt);
    }
}