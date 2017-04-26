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

        public NewsRepository(string section) //!? VD How set it in interface for being in all inteface implementations
        {
            newsSection = db.NewsSection.Where(n => n.Name == section).FirstOrDefault();
        }

        public bool CreateNews(News news)
        {
            try
            {
                news.UserId = HttpContext.Current.User.Identity.GetUserId<int>(); ;
                news.NewsSection = newsSection;
                news.CreateDate = DateTime.Now;
                db.News.Add(news);
                db.SaveChanges();
                MvcApplication.logger.Info($"News {news.Id} created ");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "create news fail/ Ex: " + e.ToString());
                return false;
            }
        }

        public bool DeleteNews(int newsId)
        {
            try
            {
                News news = GetNewsById(newsId);
                db.NewsComments.RemoveRange(GetNewsComments(newsId));
                db.News.Remove(news);
                db.SaveChanges();
                MvcApplication.logger.Info($"news {newsId} deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "delete news fail/ Ex: " + e.ToString());
                return false;
            }
        }

        public bool EditNews(News news)
        {
            try
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.logger.Info($"News {news.Id} edited");
                return true;
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "edit news fail/ Ex: " + e.ToString());
                return false;
            }
        }

        public News GetNewsById(int newsId)
        {
            return SelectedNews.Where(x=> x.Id == newsId).FirstOrDefault();
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

        public bool AddComment(NewsComments comment)
        {
            try
            {
                comment.UserId = HttpContext.Current.User.Identity.GetUserId<int>();
                comment.CreateDate = DateTime.Now;
                db.NewsComments.Add(comment);
                db.SaveChanges();
                MvcApplication.logger.Info($"Comment {comment.Id} posted to news {comment.NewsId}");
                return true;
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "add comment fail/ Ex: "+ e.ToString());
                return false;
            }
        }

        public NewsComments GetCommentById(int id)
        {
            return db.NewsComments.Find(id);
        }

        public bool EditComment(NewsComments comment)
        {
            try
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.logger.Info($"Comment {comment.Id} edited");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "edit comment fail/ Ex: " + e.ToString());
                return false;
            }
        }

        public bool DeleteComment(int id)
        {
            try
            {
                NewsComments comment = GetCommentById(id);
                db.NewsComments.Remove(comment);
                db.SaveChanges();
                MvcApplication.logger.Info($"Comment {comment.Id} deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "delete comment fail/ Ex: " + e.ToString());
                return false;
            }
          
        }

        public bool AddLike(int newsId)
        {
            try
            {
                db.NewsLikes.Add(new NewsLikes { UserId = SecureCustomHelper.GetCurrentUserId(), NewsId = newsId });
                db.SaveChanges();
                MvcApplication.logger.Info($"Favoite add to user {SecureCustomHelper.GetCurrentUserId()}, news {newsId}");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "delete comment failg/ Ex: " + e.ToString());
                return false;
            }
        }

        public bool DeleteLike(int newsId)
        {
            try
            {
                int userId = SecureCustomHelper.GetCurrentUserId();
                NewsLikes like = db.NewsLikes.Where(x => x.NewsId == newsId).Where(x => x.UserId == userId).FirstOrDefault();
                db.NewsLikes.Remove(like);
                db.SaveChanges();
                MvcApplication.logger.Info($"Favoite removed from user {SecureCustomHelper.GetCurrentUserId()}, news {newsId}");
                return true;
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(this.ToString() + "delete comment failg/ Ex: " + e.ToString());

                return false;
            }
        }
    }
}            
