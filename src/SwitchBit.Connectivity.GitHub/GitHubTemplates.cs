using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchBit.Connectivity.GitHub
{
    //https://developer.github.com/v3

    internal static class GitHubTemplates
    {
        internal const string GET_REPO_CONTRIBUTORS = @"/repos/{0}/{1}/stats/contributors";
        internal const string GET_REPO_COMMITACTIVITY = @"/repos/{0}/{1}/stats/commit_activity";
        internal const string GET_REPO_PARTICIPATION = @"/repos/{0}/{1}/stats/participation";
        internal const string GET_REPO_COMMITS = @"/repos/{0}/{1}/commits";
        internal const string GET_REPO_VIEWS = @"/repos/{0}/{1}/traffic/views";
        internal const string GET_REPO_CLONECOUNT = @"/repos/{0}/{1}/traffic/clones";
    }
}
