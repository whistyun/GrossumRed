using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using GrossumRed;
using NUnit.Framework;
using System.IO;

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