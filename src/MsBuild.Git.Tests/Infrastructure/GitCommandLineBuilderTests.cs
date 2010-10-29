using MbUnit.Framework;
using MsBuild.Git.Infrastructure;

namespace MsBuild.Git.Tests.Infrastructure
{
    [TestFixture]
    public class GitCommandLineBuilderTests
    {

        [Test]
        public void Build_NullCommand_ReturnNull()
        {
            var sut = CreateSut(null);
            Assert.IsNull(sut.Build());
        }

        [Test]
        [Row("describe")]
        public void Build_GitDescribeWithoutCommitish(string expected)
        {
            var task = new GitDescribe();

            var sut = CreateSut(task);
            Assert.AreEqual(expected, sut.Build());
        }

        [Test]
        [Row("describe commitish")]
        public void Build_GitDescribeWithCommitish(string expected)
        {
            var task = new GitDescribe();
            task.Committish = "commitish";

            var sut = CreateSut(task);
            Assert.AreEqual(expected, sut.Build());
        }

        [Test]
        [Row("describe --all commitish")]
        public void Build_GitDescribeWithCommitishAndAll(string expected)
        {
            var task = new GitDescribe();
            task.Committish = "commitish";
            task.All = true;

            var sut = CreateSut(task);
            Assert.AreEqual(expected, sut.Build());
        }

        [Test]
        [Row("describe --all --tags commitish")]
        public void Build_GitDescribeWithCommitishAndAllAndTags(string expected)
        {
            var task = new GitDescribe();
            task.Committish = "commitish";
            task.All = true;
            task.Tags = true;

            var sut = CreateSut(task);
            Assert.AreEqual(expected, sut.Build());
        }

        private static GitCommandLineBuilder CreateSut(IGitCommand command)
        {
            return new GitCommandLineBuilder(command);
        }
    }
}
