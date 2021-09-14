using System;

namespace BettingCoTestTask.Models
{
    public class ArticleModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string AuthorName { get; set; }

        public DateTime PublishedDate { get; set; }

        public string ImagePath { get; set; }

        public class Classes
        {
            public const string ARTICLE = "tm-articles-list__item";

            public const string TITLE = "tm-article-snippet__title-link";

            public const string BODY = "tm-article-body";

            public const string AUTHORNAME = "tm-user-info__username";

            public const string PUBLISHEDDATE = "tm-article-snippet__datetime-published";
        }
    }
}
