using System;
using System.Collections.Generic;
using System.Text;

namespace SwitchBit.Connectivity.GitHub
{
    public class ContributorResult
    {
        public ContributingAuthor author { get; set; }
        public int total { get; set; }
        public Week[] weeks { get; set; }
    }

    public class ContributingAuthor
    {
        public string login { get; set; }
        public int id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
    }

    public class Week
    {
        public string w { get; set; }
        public int a { get; set; }
        public int d { get; set; }
        public int c { get; set; }
    }

}
