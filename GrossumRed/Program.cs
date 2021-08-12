using GrossumRed.Parser;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowUsage();
                return -1;
            }

            var errorCount = 0;
            foreach (var arg in args)
            {
                var errors = new List<CompilerError>();
                CompileManager.CompileFile(arg, errors.Add);
                ShowErrors(errors);
                errorCount += errors.Count;
            }

            return errorCount;
        }

        private static void ShowErrors(List<CompilerError> errors)
        {
            var startingColor = Console.ForegroundColor;

            foreach (var e in errors)
            {
                Console.ForegroundColor = e.IsWarning ? ConsoleColor.Yellow : ConsoleColor.Red;
                Console.WriteLine(e);
            }

            Console.ForegroundColor = startingColor;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("usage\r\n    filepath");
        }
    }
}
