using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed.Parser
{
    class PropInfo : SyntaxInfo
    {
        /// <summary>
        /// "public" / "protected" / "internal" / "private" otherwise null
        /// </summary>
        public string AccessLevelLabel { get; }
        public string StyleName { get; }
        public string Name { get; }

        public string Callback { get; }


        public PropInfo(string accessLvLbl, string styleNm, string propNm, string callback) : base(SyntaxType.StartProperty)
        {
            AccessLevelLabel = accessLvLbl;
            StyleName = styleNm;
            Name = propNm;

            Callback = callback;
        }
    }

    class PropGetter : SyntaxInfo
    {
        public string AccessLevelLabel { get; }

        public PropGetter(string accessLvLbl) : base(SyntaxType.PropertyGet)
        {
            AccessLevelLabel = accessLvLbl;
        }
    }

    class PropSetter : SyntaxInfo
    {
        public string AccessLevelLabel { get; }

        public PropSetter(string accessLvLbl) : base(SyntaxType.PropertySet)
        {
            AccessLevelLabel = accessLvLbl;
        }
    }
}
