using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed.Templates
{
    static class Template
    {
        private static string GetText(string name)
        {
            using (var strm = Assembly.GetExecutingAssembly().GetManifestResourceStream("GrossumRed.Templates." + name))
            using (var reader = new StreamReader(strm, new UTF8Encoding(true)))
            {
                return reader.ReadToEnd();
            }
        }


        public static string GetTextSetterINotifyPropertyChanged(string propNm, string fieldNm)
        {
            var rawText = GetText("SetterForINotifyPropertyChanged.txt");

            return rawText.Replace("$n", propNm).Replace("$f", fieldNm);
        }

    }
}
