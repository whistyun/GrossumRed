// This file is based on CompilePegGrammar.cs of otac0n/Pegasus (https://github.com/otac0n/Pegasus/).
// CompilePegGrammar.cs is licensed under the MIT license. 

// Copyright © John Gietzen. All Rights Reserved. This source is subject to the MIT license.
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;

namespace GrossumRed
{
    public class CompileGrossumVwMdl : Task
    {
        [Required]
        public string[] InputFiles { get; set; }

        public string[] OutputFiles { get; set; }

        [Output]
        public TaskItem[] GeneratedCodeFiles { get; set; } = new TaskItem[0];

        public override bool Execute()
        {
            var inputs = this.InputFiles.ToList();
            var outputs = (this.OutputFiles ?? new string[inputs.Count]).ToList();

            if (inputs.Count != outputs.Count)
            {
                this.Log.LogError(Message.WrongNumberOfOutputFiles);
                return false;
            }

            for (var i = 0; i < this.InputFiles.Length; i++)
            {
                this.Log.LogMessage("GrossumRed: " + inputs[i] + " -> " + outputs[i]);
                CompileManager.CompileFile(inputs[i], outputs[i], this.LogError);
            }

            GeneratedCodeFiles = outputs.Select(path => new TaskItem(Path.GetFullPath(path))).ToArray();

            return !this.Log.HasLoggedErrors;
        }

        private void LogError(CompilerError error)
        {
            if (error.IsWarning)
            {
                this.Log.LogWarning(null, error.ErrorNumber, null, error.FileName, error.Line, error.Column, 0, 0, error.ErrorText);
            }
            else
            {
                this.Log.LogError(null, error.ErrorNumber, null, error.FileName, error.Line, error.Column, 0, 0, error.ErrorText);
            }
        }
    }
}
