using System;
using System.Collections.Generic;
using System.Text;
using Gallio.Framework;
using MbUnit.Framework;
using MbUnit.Framework.ContractVerifiers;
using Microsoft.Build.Framework;
using Moq;

namespace MsBuild.Git.Tests
{
    [TestFixture]
    public class GitDescribeTests
    {
        [Test]
        public void Test()
        {
            GitDescribe task = new GitDescribe();
            var buildEngineMock = new Mock<IBuildEngine3>();
            buildEngineMock.SetupAllProperties();
            task.BuildEngine = buildEngineMock.Object;

            Assert.IsTrue(task.Execute(), task.StandardError);
        }
    }
}
