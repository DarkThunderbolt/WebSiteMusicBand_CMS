using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class NewsRepository : INewsRepository
    {
        private MusicBandSiteDB db = new MusicBandSiteDB();
        private NewsSection newsSection;

        public IEnumerable<News> SelectedNews { get { return newsSection.News; } }

        public NewsRepository(string sction)
        {
            newsSection = db.NewsSections.Where(n => n.Name == sction).First();
        }

        public void CreateNews(News news,int user)
        {
            news.UserId = user;
            news.NewsSection = newsSection;
            news.CreateDate = DateTime.Now;
            db.News.Add(news);
            db.SaveChanges();
        }

        public void DeleteNews(int newsId)
        {
            News news = GetNewsById(newsId);
            db.NewsComments.RemoveRange(GetNewsComments(newsId));
            db.News.Remove(news);
            db.SaveChanges();
        }

        public void EditNews(News news)
        {
            db.Entry(news).State = EntityState.Modified;
            db.SaveChanges();
        }

        public News GetNewsById(int? newsId)
        {
            return db.News.Find(newsId);
        }

        public IEnumerable<News> GetNewsPage(PagingInfo pageInfo, bool down = true)
        {
            pageInfo.TotalItems = SelectedNews.Count();

            var news = SelectedNews
                .OrderBy(n => n.CreateDate)
                .Skip((pageInfo.CurrentPage - 1) * pageInfo.ItemsPerPage)
                .Take(pageInfo.ItemsPerPage).ToList();

            if (down)
                news.Reverse();
            return news;
        }

        public IEnumerable<NewsComment> GetNewsComments(int newsId)
        {
            return db.NewsComments.Where(n => n.NewsId == newsId);
        }

        public void Dispose() { }

        public int GetTotalNumberOfNews()
        {
            return SelectedNews.Count();
        }
    }
}