using GrossumRed.Parser;
using Pegasus.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrossumRed
{
    public static class CompileManager
    {
        public static void CompileFile(string inputFile, Action<CompilerError> logError)
            => CompileFile(inputFile, inputFile + ".g.cs", logError);

        public static void CompileFile(string inputFile, string outputFile, Action<CompilerError> logError)
        {
            if (inputFile == null)
                throw new ArgumentNullException(nameof(inputFile));

            if (logError == null)
                throw new ArgumentNullException(nameof(logError));

            var subject = File.ReadAllText(inputFile);
            var fileName = MakePragmaPath(inputFile, outputFile);

            var generated = CompileString(subject, fileName, logError);

            if (generated != null)
            {
                using var outStream = new FileStream(outputFile, FileMode.Create);
                using var outWriter = new StreamWriter(outStream, new UTF8Encoding(true));
                outWriter.Write(generated);
            }
        }

        public static string CompileString(string subject, string fileName, Action<CompilerError> logError)
        {
            try
            {
                using var writer = new StringWriter();
                var picker = new VMCSharpPicker(subject, fileName);
                var generator = new CSGenerator(picker, writer, logError);
                generator.Treat();

                return generator.HasError ? null : writer.ToString();
            }
            catch (FormatException ex)
            {
                var cursor = ex.Data["cursor"] as Cursor;
                if (cursor != null && Regex.IsMatch(ex.Message, @"^PEG\d+:"))
                {
                    var parts = ex.Message.Split(new[] { ':' }, 2);
                    logError(new CompilerError(cursor.FileName ?? string.Empty, cursor.Line, cursor.Column, parts[0], parts[1]));
                    return null;
                }

                throw;
            }
        }

        private static string MakePragmaPath(string input, string output)
        {
            output = Path.GetFullPath(output);
            input = Path.GetFullPath(input);
            var relativeUri = new Uri(output).MakeRelativeUri(new Uri(input));
            return relativeUri.ToString().IndexOf('/') != -1
                ? input
                : Path.GetFileName(input);
        }
    }
}
