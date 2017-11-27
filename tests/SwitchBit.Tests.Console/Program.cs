using SwitchBit.Connectivity.GitHub;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SwitchBit.Tests.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(async ()=> {
                var git = GitHubBuilder.New()
                            .ForOwner("switchbit")
                            .WithRepository("packages")
                            .Create();

                var commits = await git.GetRepositoryCommits();

                foreach (var commit in commits)
                    System.Console.WriteLine($@"Commit:{commit.sha} Author:{commit.author.login}");
            });
            
            System.Console.ReadKey();

            //return 0;
        }
    }
}
