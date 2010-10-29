using System;
using System.Reflection;
using System.Text;
using Microsoft.Build.Utilities;

namespace MsBuild.Git.Infrastructure
{
    public class GitCommandLineBuilder
    {
        private IGitCommand gitCommand;

        public GitCommandLineBuilder(IGitCommand gitCommand)
        {
            this.gitCommand = gitCommand;
        }

        public string Build()
        {
            if (gitCommand == null) return null;
            Type type = gitCommand.GetType().GetInterfaces()[2];
            var sb = new CommandLineBuilder();

            var commandAttribute = (CommandAttribute) type.GetCustomAttributes(typeof(CommandAttribute), true)[0];
            sb.AppendSwitch(commandAttribute.Name);

            PropertyInfo lastPropertyInfo = null;
            LastArgumentAttribute lastArgumentAttribute = null;

            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public |BindingFlags.GetProperty))
            {
                var customAttributes = propertyInfo.GetCustomAttributes(typeof (ArgumentAttribute), true);
                if (customAttributes.Length > 0)
                {
                    var argument = (ArgumentAttribute)customAttributes[0];
                    if (argument is LastArgumentAttribute)
                    {
                        lastArgumentAttribute = (LastArgumentAttribute) argument;
                        lastPropertyInfo = propertyInfo;
                    }
                    else
                    {
                        var argumentString = argument.ToString(propertyInfo.GetValue(gitCommand, null));
                        if (string.IsNullOrEmpty(argumentString) == false)
                        {
                            sb.AppendSwitch(argumentString);
                        }
                    }
                }
            }

            if (lastArgumentAttribute != null)
            {
                var switchName = lastArgumentAttribute.ToString(lastPropertyInfo.GetValue(gitCommand, null));
                if (string.IsNullOrEmpty(switchName) == false)
                    sb.AppendSwitch(switchName);
            }

            return sb.ToString();
        }
    }
}