using System;

namespace MsBuild.Git.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ArgumentAttribute : Attribute
    {
        public abstract string ToString(object value);
    }
}