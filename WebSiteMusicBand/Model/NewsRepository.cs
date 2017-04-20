using Microsoft.AspNet.Identity;
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

        public IEnumerable<News> SelectedNews { get { return newsSection.News.OrderBy(n => n.CreateDate); } }

        public NewsRepository(string section)
        {
            newsSection = db.NewsSection.Where(n => n.Name == section).First();
        }

        public void CreateNews(News news)
        {
            news.UserId = HttpContext.Current.User.Identity.GetUserId<int>(); ;
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

        public News GetNewsById(int newsId)
        {
            return SelectedNews.Where(x=> x.Id == newsId).First();
        }

        public IEnumerable<News> GetNewsPage(PagingInfo pageInfo, bool down = true)
        {
            pageInfo.TotalItems = SelectedNews.Count();

            var news = SelectedNews
                .Reverse()
                .Skip((pageInfo.CurrentPage - 1) * pageInfo.ItemsPerPage)
                .Take(pageInfo.ItemsPerPage).ToList();

            if (!down)
                news.Reverse();
            return news;
        }

        public IEnumerable<NewsComments> GetNewsComments(int newsId)
        {
            return db.NewsComments.Where(n => n.NewsId == newsId);
        }

        public void Dispose() { } //?! VD

        public int GetTotalNumberOfNews()
        {
            return SelectedNews.Count();
        }

        public void AddComment(NewsComments comment)
        {
            comment.UserId = HttpContext.Current.User.Identity.GetUserId<int>();
            comment.CreateDate = DateTime.Now;
            db.NewsComments.Add(comment);
            db.SaveChanges();
        }


        public NewsComments GetCommentById(int id)
        {
            return db.NewsComments.Find(id);
        }

        public void EditComment(NewsComments comment)
        {
            db.Entry(comment).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteComment(int id)
        {
            NewsComments comment = GetCommentById(id);
            db.NewsComments.Remove(comment);
            db.SaveChanges();
        }

        public void AddLike(int newsId)
        {
            db.NewsLikes.Add(new NewsLikes { UserId = SecureCustomHelper.GetCurrentUserId(), NewsId = newsId});
            db.SaveChanges();
        }

        public void DeleteLike(int newsId)
        {
            int userId = SecureCustomHelper.GetCurrentUserId();
            NewsLikes like = db.NewsLikes.Where(x => x.NewsId == newsId).Where(x => x.UserId == userId).First();
            db.NewsLikes.Remove(like);
            db.SaveChanges();
        }
    }
}