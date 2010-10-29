using System;

namespace MsBuild.Git.Infrastructure
{
    public class BoolArgumentAttribute : NamedArgumentAttribute
    {
        public BoolArgumentAttribute(string name)
        {
            Name = name;
        }

        public override string ToString(object value)
        {
            return (bool) value ? Name : null;
        }
    }
}