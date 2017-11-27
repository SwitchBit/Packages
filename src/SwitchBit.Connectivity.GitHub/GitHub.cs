using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SwitchBit.Connectivity.GitHub
{
    public class GitHub
    {
        private const string apiRoot = "https://api.github.com"; //Accept: application/vnd.github.v3+json

        
        private string oauthToken = string.Empty;

        private HttpClient http;

        internal GitHub() { }

        public string Account { get; internal set; }
        public string Repository { get; internal set; }
        public string Owner { get; internal set; }
        
        private HttpClient Client {
            get
            {
                if (http != null)
                    return http;

                http = new HttpClient
                {
                    BaseAddress = new Uri(apiRoot)
                };
                http.DefaultRequestHeaders.Clear();
                http.DefaultRequestHeaders.Add("Accept", "application/json");
                //http.DefaultRequestHeaders.Add("Authorization", oauthToken);
                http.DefaultRequestHeaders.Add("User-Agent", "SWITCHBIT001");
                return http;
            }
        }
        public async Task<CommitResult[]> GetRepositoryCommits()
        {
            var result = await Client.GetStringAsync(string.Format(GitHubTemplates.GET_REPO_COMMITS, Owner, Repository));

            var list = JArray.Parse(result).ToObject<CommitResult[]>();

            return list;
        }

        public async Task<ContributorResult[]> GetRepositoryContributors()
        {
            var result = await Client.GetStringAsync(string.Format(GitHubTemplates.GET_REPO_CONTRIBUTORS, Owner, Repository));

            var list = JArray.Parse(result).ToObject<ContributorResult[]>();

            return list;
        }
    }
}
