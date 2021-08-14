using GrossumRed.Parser;
using GrossumRed.Templates;
using Pegasus.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrossumRed
{
    class CSGenerator
    {
        private VMCSharpPicker Picker { get; }
        private TextWriter Output { get; }
        private Action<CompilerError> LogError { get; }

        public bool HasError { private set; get; }

        public CSGenerator(VMCSharpPicker picker, TextWriter output, Action<CompilerError> logError)
        {
            Picker = picker;
            Output = output;
            LogError = logError;
            HasError = false;
        }

        public void Treat()
        {
            int blockLevel = 0;
            AttributeInfoCollection prevAttribute = null;

            while (Picker.HasNext())
            {
                var syntaxInfo = Picker.Current;


                switch (syntaxInfo.SyntaxType)
                {
                    default:
                        throw new InvalidOperationException();

                    case SyntaxType.AttributeList:
                    case SyntaxType.Using:
                    case SyntaxType.Comment:
                    case SyntaxType.DefField:
                    case SyntaxType.DefEvent:
                        OutputParsedCode();
                        break;

                    case SyntaxType.StartFieldWithInit:
                    case SyntaxType.StartMethodWithLambda:
                        ProcessBlockOrExpression(true);
                        break;

                    case SyntaxType.StartMethodWithBlock:
                        ProcessBlockOrExpression(false);
                        break;


                    case SyntaxType.Whitespace:
                        OutputWhitespace();
                        break;

                    case SyntaxType.StartNamespace:
                        blockLevel++;
                        OutputParsedCode();
                        break;

                    case SyntaxType.StartClass:
                        blockLevel++;
                        TreatClass((ClassInfo)Picker.Current);
                        break;

                    case SyntaxType.StartProperty:
                        TreatProperty((PropInfo)Picker.Current);
                        break;


                    case SyntaxType.BlockEnd:
                        blockLevel--;
                        if (blockLevel < 0)
                        {
                            FireCompileError(Message.UnexpectedBlockEnd);
                        }
                        OutputParsedCode();

                        break;
                }

                if (prevAttribute != null)
                {
                    prevAttribute = null;
                }
            }

            if (blockLevel > 0)
            {
                FireCompileError(Message.NotFoundBlockEnd, Picker.CurrentEnd);
            }
        }

        private void TreatClass(ClassInfo current)
        {
            string NtfyPropChng = nameof(INotifyPropertyChanged);

            var explicitAnyInherit = current.Inheriteds != null;
            var inheritCodeWritable = explicitAnyInherit || !current.IsPartial;
            var explicitNtfyPrpChng =
                    explicitAnyInherit ?
                        current.Inheriteds.Any(inh => inh == NtfyPropChng || inh.EndsWith("." + NtfyPropChng)) :
                        false;

            if (inheritCodeWritable && !explicitNtfyPrpChng)
            {
                // implements INotifyPropertyChanged
                Output.WriteLine($"#line {Picker.CurrentStart.Line} \"{Picker.FileName}\"");
                // output 'public? partial? class Foo (: Bar, Car)?'
                var parsedCd = Picker.CurrentText;
                var blockBgnIdx = parsedCd.LastIndexOf('{');
                Output.WriteLine(parsedCd.Substring(0, blockBgnIdx));
                Output.WriteLine($"#line default");

                // output (,/:) INotiyPropertyChanged
                Output.Write(explicitAnyInherit ? ',' : ':');
                Output.Write("System.ComponentModel.INotifyPropertyChanged");
                Output.WriteLine("{");

                Output.WriteLine("public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;");
            }
            else
            {
                OutputParsedCode();
            }
        }

        private void TreatProperty(PropInfo pinf)
        {
            bool getterOutputed = false;
            bool setterOutputed = false;

            OutputParsedCode();

            while (Picker.HasNext())
            {
                var syntaxInfo = Picker.Current;
                switch (syntaxInfo.SyntaxType)
                {
                    default:
                        var msg = String.Format(Message.UnexpectedSyntaxInfo, Picker.CurrentText[0]);
                        FireCompileError(msg);
                        break;

                    case SyntaxType.Comment:
                    case SyntaxType.AttributeList:
                        OutputParsedCode();
                        break;

                    case SyntaxType.Whitespace:
                        OutputWhitespace();
                        break;

                    case SyntaxType.PropertyGet:
                        if (getterOutputed)
                        {
                            FireCompileError(Message.GetterDuplicated);
                        }
                        else
                        {
                            var getter = (PropGetter)Picker.Current;
                            Output.WriteLine(getter.AccessLevelLabel ?? "");
                            Output.WriteLine($"get{{ return __{pinf.Name.Trim()}; }}");

                            getterOutputed = true;
                        }
                        break;

                    case SyntaxType.PropertySet:
                        if (setterOutputed)
                        {
                            FireCompileError(Message.SetterDuplicated);
                            break;
                        }
                        else
                        {
                            var setter = (PropSetter)Picker.Current;
                            Output.WriteLine(setter.AccessLevelLabel ?? "");
                            Output.WriteLine(Template.GetTextSetterINotifyPropertyChanged(pinf.Name.Trim(), "__" + pinf.Name.Trim()));

                            setterOutputed = true;
                        }
                        break;

                    case SyntaxType.BlockEnd:
                        OutputParsedCode();
                        goto end;
                }
            }

        end:
            Output.WriteLine($"private {pinf.StyleName} __{pinf.Name};");
        }

        private void ProcessBlockOrExpression(bool isExpression)
        {
            Output.WriteLine($"#line {Picker.CurrentStart.Line} \"{Picker.FileName}\"");

            Output.Write(Picker.CurrentText);

            if (isExpression)
                Picker.Comsume((new Skipper()).ParseSkipperExpression);
            else
                Picker.Comsume((new Skipper()).ParseSkipperBlock);

            Output.Write(Picker.CurrentText);

            Output.WriteLine();
            Output.WriteLine("#line default");
        }

        private void OutputWhitespace()
        {
            if (!string.IsNullOrWhiteSpace(Picker.CurrentText))
                throw new InvalidOperationException("current text is not whitespace!!");

            if (Picker.CurrentText.Contains('\r') || Picker.CurrentText.Contains('\n'))
            {
                Output.WriteLine();
            }
        }

        private void OutputParsedCode()
        {
            Output.WriteLine($"#line {Picker.CurrentStart.Line} \"{Picker.FileName}\"");

            Output.Write(Picker.CurrentText);

            Output.WriteLine();
            Output.WriteLine("#line default");
        }

        private void FireCompileError(string cdAndMsg)
            => FireCompileError(cdAndMsg, Picker.CurrentStart);

        private void FireCompileError(string cdAndMsg, CursorInfo cursor)
        {
            var idx = cdAndMsg.IndexOf(':');
            var code = cdAndMsg.Substring(0, idx);
            var message = cdAndMsg.Substring(idx + 1);

            LogError(new CompilerError(
                Picker.FileName,
                cursor.Line, cursor.Column,
                code, message));

            HasError = true;
        }
    }
}
