using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed.Parser
{
    abstract class SyntaxInfoCollection : SyntaxInfo
    {
        public SyntaxType ElementSyntaxType { get; }

        public SyntaxInfoCollection(SyntaxType elementType) : base(SyntaxType.AttributeList)
        {
            ElementSyntaxType = elementType;
        }
    }
}
