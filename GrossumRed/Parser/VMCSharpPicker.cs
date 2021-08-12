using Pegasus.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GrossumRed.Parser
{
    class VMCSharpPicker
    {
        public string LeftSubject { private set; get; }
        public string FileName { get; }

        public SyntaxInfo Current { private set; get; }
        public CursorInfo CurrentStart { private set; get; }
        public CursorInfo CurrentEnd { private set; get; }
        public string CurrentText { private set; get; }


        public VMCSharpPicker(string subject, string fileName)
        {
            LeftSubject = subject;
            FileName = fileName;
        }

        public bool HasNext()
        {
            if (String.IsNullOrWhiteSpace(LeftSubject))
            {
                Current = null;
                CurrentStart = default(CursorInfo);
                CurrentEnd = default(CursorInfo);
                CurrentText = null;

                return false;
            }

            var offset = new CursorInfo(CurrentEnd);

            SyntaxInfo parsedResult;
            Cursor leftStart;
            Cursor leftEnd;
            try
            {
                var parser = new VMCSharp();
                parsedResult = parser.Parse(LeftSubject, FileName, out var lexicals);
                leftStart = lexicals.Select(lex => lex.StartCursor).Min();
                leftEnd = lexicals.Select(lex => lex.EndCursor).Max();
            }
            catch (FormatException ex)
            {
                var cursor = ex.Data["cursor"] as Cursor;
                if (cursor != null)
                {
                    var ex2 = new FormatException(ex.Message, ex);
                    ex2.Data["cursor"] = offset + cursor;

                    throw ex2;
                }

                throw;
            }

            Current = parsedResult;

            var len = leftEnd.Location - leftStart.Location;
            CurrentText = LeftSubject.Substring(leftStart.Location, len);
            LeftSubject = LeftSubject.Substring(leftEnd.Location);

            CurrentStart = leftStart + offset;
            CurrentEnd = leftEnd + offset;

            return true;
        }
    }

    struct CursorInfo
    {
        public string FileName { get; }
        public int Line { get; }
        public int Column { get; }
        public int Location { get; }

        public CursorInfo(string fileName, int line, int column, int location)
        {
            FileName = fileName;
            Line = line;
            Column = column;
            Location = location;
        }

        public CursorInfo(Cursor cursor)
        {
            if (cursor is null)
            {
                FileName = null;
                Line = Column = Location = 0;
            }
            else
            {
                this.FileName = cursor.FileName;
                this.Line = cursor.Line;
                this.Column = cursor.Column;
                this.Location = cursor.Location;
            }
        }

        public CursorInfo(CursorInfo cursor)
        {
            this.FileName = cursor.FileName;
            this.Line = cursor.Line;
            this.Column = cursor.Column;
            this.Location = cursor.Location;
        }

        public static CursorInfo operator +(Cursor c1, CursorInfo c2) => c2 + c1;
        public static CursorInfo operator +(CursorInfo c1, Cursor c2)
        {
            var c1Ln = c1.Line == 0 ? 0 : c1.Line - 1;
            var c1Ch = c1.Column == 0 ? 0 : c1.Column - 1;

            return new CursorInfo(
                c2.FileName,
                c1Ln + c2.Line,
                c2.Line == 1 ? c1Ch + c2.Column : c2.Column,
                c1.Location + c2.Location);
        }

        public string ToString()
            => $"CursorInfo{{FileName:{FileName}, Line:{Line}, Column:{Column}, Location:{Location}}}";
    }
}
