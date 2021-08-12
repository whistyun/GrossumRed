using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed.Parser
{
    class ClassInfo : SyntaxInfo
    {
        /// <summary>
        /// "public" / "protected" / "internal" / "private" otherwise null
        /// </summary>
        public string AccessLevelLabel { get; }
        public bool IsPartial { get; }
        public string Name { get; }
        public IList<string> Inheriteds { get; }


        public ClassInfo(string accessLvLbl, bool isPartial, string classNm, IList<string> inheriteds) : base(SyntaxType.StartClass)
        {
            AccessLevelLabel = accessLvLbl;
            IsPartial = isPartial;
            Name = classNm;
            Inheriteds = inheriteds;
        }
    }
}
