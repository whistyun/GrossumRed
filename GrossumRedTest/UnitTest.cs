using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using GrossumRed;
using NUnit.Framework;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GrossumRedTest
{
    [UseReporter(typeof(DiffReporter))]
    public class UnitTest
    {
        static UnitTest()
        {
            Approvals.RegisterDefaultNamerCreation(() => new Renamer("Out"));
        }

        [Test]
        public void Test1()
        {
            var source = Utils.LoadText("VmCls.vm.cs");
            var generated = CompileManager.CompileString(source, "test", errInfo => { });

            Approvals.Verify(generated);
        }

        [Test]
        public void Test2()
        {
            var source = Utils.LoadText("VmCls2.vm.cs");
            var generated = CompileManager.CompileString(source, "test", errInfo => { });

            Approvals.Verify(generated);
        }

        [Test]
        public void Test3()
        {
            var errors = new List<CompilerError>();

            var source = Utils.LoadText("VmCls3.vm.cs");
            var generated = CompileManager.CompileString(source, "test", errors.Add);

            Approvals.Verify(errors.AsString());
        }
    }


    static class ListExt
    {

        public static string AsString<T>(this List<T> list)
            => AsString(list, elm => elm.ToString());

        public static string AsString<T>(this List<T> list, Func<T, string> format )
        {
            var buf = new StringBuilder();
            buf.AppendLine("[");
            buf.Append("  ");
            buf.AppendLine(String.Join(",\r\n  ", list.Select(elm => format(elm))));
            buf.AppendLine("]");

            return buf.ToString();
        }
    }


    class Renamer : UnitTestFrameworkNamer
    {
        private string dir;

        public override string SourcePath
        {
            get => Path.Combine(base.SourcePath, dir);
        }

        public Renamer(string dir)
        {
            this.dir = dir;
        }
    }
}