using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Build.Tasks;
using MsBuild.Git.Infrastructure;

namespace MsBuild.Git
{
    /// <summary>
    /// Show the most recent tag that is reachable from a commit
    /// </summary>
    [Command("describe")]
    public interface IGitDescribe
    {
        /// <summary>
        /// Instead of using only the annotated tags, use any ref found in <c>.git/refs/</c>.
        /// This option enables matching any known branch, remote branch, or lightweight tag.
        /// </summary>
        [BoolArgument("--all")]
        bool All { get; set; }

        /// <summary>
        /// Instead of using only the annotated tags, use any ref found in <c>.git/refs/</c>.
        /// This option enables matching a lightweight (non-annotated) tag.
        /// </summary>
        [BoolArgument("--tags")]
        bool Tags { get; set; }

        /// <summary>
        /// Committish object names to describe.
        /// </summary>
        [LastArgument]
        string Committish { get; set; }
    }

    /// <summary>
    /// Show the most recent tag that is reachable from a commit
    /// </summary>
    public class GitDescribe : AbstractGitTask, IGitDescribe
    {
        /// <summary>
        /// Instead of using only the annotated tags, use any ref found in <c>.git/refs/</c>.
        /// This option enables matching any known branch, remote branch, or lightweight tag.
        /// </summary>
        public bool All { get; set; }

        /// <summary>
        /// Instead of using only the annotated tags, use any ref found in <c>.git/refs/</c>.
        /// This option enables matching a lightweight (non-annotated) tag.
        /// </summary>
        public bool Tags { get; set; }

        /// <summary>
        /// Committish object names to describe.
        /// </summary>
        public string Committish { get; set; }
    }
}
