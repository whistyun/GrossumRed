using Microsoft.CodeAnalysis.CSharp.Syntax;
using Pegasus.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed.Parser
{
    class AttributeInfo : SyntaxInfo
    {
        public string Name { get; }
        public BracketedParameterListSyntax Parameters { get; }

        public AttributeInfo(string name) : this(name, null)
        {
        }
        public AttributeInfo(string name, BracketedParameterListSyntax parameters) : base(SyntaxType.Attribute)
        {
            Name = name;
            Parameters = parameters;
        }
    }
}
