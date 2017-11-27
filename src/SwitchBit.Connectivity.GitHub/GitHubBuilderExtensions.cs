using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchBit.Connectivity.GitHub
{
    public static class GitHubBuilderExtensions
    {
        public static GitHubBuilder UsingAccount(this GitHubBuilder git, string account)
        {
            git = git ?? throw new ArgumentNullException("git");
            git.Account = account ?? throw new ArgumentNullException("account");
            return git;
        }
        public static GitHubBuilder ForOwner(this GitHubBuilder git, string owner)
        {
            git = git ?? throw new ArgumentNullException("git");
            git.Owner = owner ?? throw new ArgumentNullException("owner");
            return git;
        }
        public static GitHubBuilder WithRepository(this GitHubBuilder git, string repoName)
        {
            git = git ?? throw new ArgumentNullException("git");
            git.Repository = repoName ?? throw new ArgumentNullException("repoName");
            return git;
        }

        public static GitHub Create(this GitHubBuilder git)
            => git.Instance;
    }
}
