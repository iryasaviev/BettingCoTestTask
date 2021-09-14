using BettingCoTestTask.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace BettingCoTestTask.Services
{
    public class ArticleParsHelper
    {
        public List<ArticleModel> GetArticles(string htmlLine)
        {
            List<ArticleModel> articleModels = new();

            HtmlDocument htmlDocument = new();
            htmlDocument.LoadHtml(htmlLine);

            HtmlNodeCollection articlesHtml = htmlDocument.DocumentNode.SelectNodes(
                "//article[contains(@class, '" + ArticleModel.Classes.ARTICLE + "')]");

            if (articlesHtml != null)
            {
                foreach (var articleHtml in articlesHtml)
                {
                    articleModels.Add(GetArticleModelByHtml(articleHtml));
                }
            }

            return articleModels;
        }

        private static ArticleModel GetArticleModelByHtml(HtmlNode articleHtml)
        {
            ArticleModel articleModel = new()
            {
                Title = GetArticleTitleByHtml(articleHtml),
                Description = GetArticleDescriptionByHtml(articleHtml),
                AuthorName = GetArticleAuthorNameByHtml(articleHtml),
                PublishedDate = GetArticlePublishDateByHtml(articleHtml),
                ImagePath = GetArticleImagePathByHtml(articleHtml)
            };

            return articleModel;
        }

        private static string GetArticleTitleByHtml(HtmlNode articleHtml)
        {
            HtmlNode articleTitleHtml = articleHtml
                .SelectSingleNode(".//a[contains(@class, '" + ArticleModel.Classes.TITLE + "')]");

            string articleTitle = "";
            if (articleTitleHtml != null)
            {
                articleTitle = articleTitleHtml.InnerText;
            }

            return articleTitle;
        }

        private static string GetArticleDescriptionByHtml(HtmlNode articleHtml)
        {
            HtmlNode articleBodyHtml = articleHtml.SelectSingleNode(
                ".//div[contains(@class, '" + ArticleModel.Classes.BODY + "')]");

            string articleDescription = "";
            if (articleBodyHtml != null)
            {
                articleDescription = articleBodyHtml.InnerText;
            }

            if (articleDescription.Length > 250)
            {
                articleDescription = articleDescription.Substring(0, 250) + "...";
            }

            return articleDescription;
        }

        private static string GetArticleAuthorNameByHtml(HtmlNode articleHtml)
        {
            HtmlNode articleAuthorNameHtml = articleHtml.SelectSingleNode(
                ".//a[contains(@class, '" + ArticleModel.Classes.AUTHORNAME + "')]");

            string articleAuthorName = "";
            if (articleAuthorNameHtml != null)
            {
                articleAuthorName = articleAuthorNameHtml.InnerText;
            }

            return articleAuthorName;
        }

        private static DateTime GetArticlePublishDateByHtml(HtmlNode articleHtml)
        {
            HtmlNode articlePublishDateHtml = articleHtml.SelectSingleNode(
                ".//span[contains(@class, '" + ArticleModel.Classes.PUBLISHEDDATE + "')]");
            DateTime articlePublishDate = new DateTime();
            if (articlePublishDateHtml != null)
            {
                string articlePublishDateLine = articlePublishDateHtml.SelectSingleNode("//time")
                    .GetAttributeValue("datetime", new DateTime().ToString());

                articlePublishDate = DateTime.Parse(articlePublishDateLine);
            }

            return articlePublishDate;
        }

        private static string GetArticleImagePathByHtml(HtmlNode articleHtml)
        {
            HtmlNode articleBodyHtml = articleHtml.SelectSingleNode(
                ".//div[contains(@class, '" + ArticleModel.Classes.BODY + "')]");

            if (articleBodyHtml == null)
            {
                return "";
            }

            HtmlNode articleBodyImageHtml = articleBodyHtml.SelectSingleNode(".//img");

            string articleImagePath = "";
            if (articleBodyImageHtml != null)
            {
                articleImagePath = articleBodyImageHtml.GetAttributeValue("src", "");
            }

            return articleImagePath;
        }
    }
}