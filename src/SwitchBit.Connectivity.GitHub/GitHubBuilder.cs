using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchBit.Connectivity.GitHub
{
    public class GitHubBuilder
    {
        private GitHub gitHub;

        internal string Repository { set => gitHub.Repository = value; }
        internal string Owner { set => gitHub.Owner = value; }
        internal string Account { set => gitHub.Account = value; }
        internal GitHub Instance { get => gitHub; }

        private GitHubBuilder(GitHub git)
        {
            gitHub = git;
        }
        public static GitHubBuilder New()
            => new GitHubBuilder(new GitHub());
    }
}
