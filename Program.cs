using BettingCoTestTask.Models;
using BettingCoTestTask.Services;
using System;
using System.Collections.Generic;

namespace BettingCoTestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- habr.com parser ---");
            Console.WriteLine();

            GetHubsInfoList();
            Console.WriteLine();


            Console.Write("Please set the hub: ");
            string hub = Console.ReadLine();

            if (hub.Length == 0 || hub == " ")
            {
                Console.WriteLine("Error! Hub can't be empty.");
                Console.ReadLine();
                return;
            }

            for (int pageNum = 1; true; pageNum++)
            {
                string searchUri = "https://habr.com/ru/hub/" + hub + "/page" + pageNum,
                    htmlLine;

                try
                {
                    htmlLine = new NetworkHelper().GetHtml(searchUri);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return;
                }

                List<ArticleModel> articleModels = new ArticleParsHelper().GetArticles(htmlLine);

                if (articleModels.Count == 0 || articleModels == null)
                    break;

                Console.WriteLine("Page num: " + pageNum);
                Console.WriteLine("---------------");

                foreach (var articleModel in articleModels)
                {
                    Console.WriteLine(articleModel.Title);
                    Console.WriteLine(articleModel.Description);
                    Console.WriteLine(articleModel.AuthorName);
                    Console.WriteLine(articleModel.PublishedDate);
                    Console.WriteLine(articleModel.ImagePath);
                    Console.WriteLine("---------------");
                }
            }

            Console.WriteLine("Finish!");
            Console.ReadLine();
        }

        /// <summary>
        /// Set hubs list on console.
        /// </summary>
        static void GetHubsInfoList()
        {
            string[] hubs = {
                "infosecurity",
                "popular_science",
                "programming",
                "itcompanies",
                "health",
                "career",
                "diy",
                "cpu",
                "it-infrastructure",
                "nix",
                "read",
                "business-laws",
                "reverse-engineering",
                "controllers",
                "electronics",
                "cellular",
                "machine_learning",
            };

            Console.WriteLine("Example of a list hubs:");
            foreach (string hub in hubs)
            {
                Console.WriteLine("- " + hub);
            }
        }
    }
}