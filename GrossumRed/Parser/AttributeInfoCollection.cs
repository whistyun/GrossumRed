using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed.Parser
{
    class AttributeInfoCollection : SyntaxInfoCollection, IEnumerable<AttributeInfo>
    {
        private AttributeInfo[] _children;

        public AttributeInfoCollection(AttributeInfo info, IList<AttributeInfo> infos) : base(SyntaxType.AttributeList)
        {
            var list = new List<AttributeInfo>();
            list.Add(info);
            if (infos != null) list.AddRange(infos);

            _children = list.ToArray();
        }

        public int Count => _children.Length;

        public AttributeInfo this[int index] => _children[index];

        public IEnumerator<AttributeInfo> GetEnumerator()
           => ((IEnumerable<AttributeInfo>)_children).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
