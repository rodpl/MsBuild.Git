namespace MsBuild.Git.Infrastructure
{
    public abstract class NamedArgumentAttribute : ArgumentAttribute
    {
        public string Name { get; set; }
    }
}