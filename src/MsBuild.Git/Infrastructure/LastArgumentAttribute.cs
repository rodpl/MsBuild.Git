namespace MsBuild.Git.Infrastructure
{
    public class LastArgumentAttribute : ArgumentAttribute
    {
        public override string ToString(object value)
        {
            return value != null ? value.ToString() : null;
        }
    }
}