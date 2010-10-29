using System;
using System.IO;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace MsBuild.Git.Infrastructure
{
    public abstract class AbstractGitTask : ToolTask, IGitCommand
    {
        private StringBuilder _errorBuffer = new StringBuilder();
        private StringBuilder _outputBuffer = new StringBuilder();

        /// <summary>
        /// Gets the output of git command-line client.
        /// </summary>
        [Output]
        public string StandardOutput
        {
            get { return _outputBuffer.ToString(); }
        }

        /// <summary>
        /// Gets the error output of git command-line client.
        /// </summary>
        [Output]
        public string StandardError
        {
            get { return _errorBuffer.ToString(); }
        }

        protected override string ToolName
        {
            get { return "git.exe"; }
        }

        public override bool Execute()
        {
            _outputBuffer.Length = 0;
            _errorBuffer.Length = 0;
            return base.Execute();
        }

        protected override string GenerateFullPathToTool()
        {
            var toolPath = ToolPath ?? FindFileInPathEnvironment(ToolName);
            var fullPath = Path.Combine(toolPath, ToolName);
            if (toolPath == null || File.Exists(fullPath) == false)
                throw new ApplicationException(string.Format("Tool '{0}' cannot be found.", ToolName));

            return fullPath;
        }

        private string FindFileInPathEnvironment(string fileName)
        {
            string toolPath = null;
            string pathEnvironmentVariable = Environment.GetEnvironmentVariable("PATH") ?? string.Empty;
            string[] paths = pathEnvironmentVariable.Split(new[] { Path.PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string path in paths)
            {
                string fullPathToClient = Path.Combine(path, fileName);
                if (File.Exists(fullPathToClient))
                {
                    toolPath = path;
                    break;
                }
            }

            if (toolPath == null)
                throw new ApplicationException(string.Format("Command '{0}' cannot be found in PATH.", ToolName));

            return toolPath;
        }

        protected override string GenerateCommandLineCommands()
        {
            return new GitCommandLineBuilder(this).Build();
        }

        protected override void LogEventsFromTextOutput(string singleLine, MessageImportance messageImportance)
        {
            bool isError = messageImportance == StandardErrorLoggingImportance;

            if (isError)
            {
                base.LogEventsFromTextOutput(singleLine, messageImportance);
                _errorBuffer.AppendLine(singleLine);
                return;
            }

            _outputBuffer.Append(singleLine);
        }
    }
}