using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Specialized;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;

namespace ProjectF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();


            ////Redmine
            //string host = "localhost:3000";
            //string apiKey = "11b9a54a46850052e067141f514a96830b882399";

            //var manager = new RedmineManager(host, apiKey);

            //var parameters = new NameValueCollection { { "status_id", "*" } };
            //foreach (var issue in manager.GetObjects<Issue>(parameters))
            //{
            //    Console.WriteLine("#{0}: {1}", issue.Id, issue.Subject);
            //}

            //Create a issue.
            //var newIssue = new Issue { Subject = "test", Project = new IdentifiableName { Name="New"} };
            //manager.CreateObject(newIssue);

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
