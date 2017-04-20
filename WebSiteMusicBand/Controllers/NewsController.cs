using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteMusicBand.Infrastructure;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class NewsController : Controller
    {
        [Inject]
        INewsRepository _newsRepo;

        private int pageSizeForShort = 3;
        private int pageSizeForLong = 10;

        public NewsController(INewsRepository news)
        {
            _newsRepo = news;
            MvcApplication.logger.Info("Create News Controller");
        }

        // GET: News/ListLong/page
        [HttpGet]
        public ActionResult ListLong(int id = 1)
        {
            PagingInfo pageInfo = new PagingInfo(_newsRepo.GetTotalNumberOfNews(), pageSizeForLong, id);
            ViewBag.PageInfo = pageInfo;
            return View(_newsRepo.GetNewsPage(pageInfo));
        }

        // GET: News/ListShort/page
        [HttpGet]
        public ActionResult ListShort(int id = 1)
        {
            PagingInfo pageInfo = new PagingInfo(_newsRepo.GetTotalNumberOfNews(), pageSizeForShort, id);
            ViewBag.PageInfo = pageInfo;
            return View(_newsRepo.GetNewsPage(pageInfo));
        }



        // GET: News/Details/newsId
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _newsRepo.GetNewsById((int)id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            ViewBag.NewsModel = news;
            return View();
        }

        // GET: News/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,TextContent,CreateDate,")] News news)
        {
            if (ModelState.IsValid)
            {
                _newsRepo.CreateNews(news);
                MvcApplication.logger.Info($"News {news.Id} created ");
                return RedirectToAction("Details", new { id = news.Id });
            }
            return View(news);
        }

        // GET: News/Edit/newsId
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _newsRepo.GetNewsById((int)id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(news.CustomUsers.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            return View(news);
        }

        // POST: News/Edit/newsId
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,TextContent,CreateDate,UserId,NewsSectionId")] News news)
        {
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(news.CustomUsers.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            if (ModelState.IsValid)
            {
                _newsRepo.EditNews(news);
                MvcApplication.logger.Info($"News {news.Id} edited");
                return RedirectToAction("Details", new { id = news.Id });
            }
            return View(news);
        }

        // GET: News/Delete/newsId
        [HttpGet]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _newsRepo.GetNewsById((int)id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(news.CustomUsers.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _newsRepo.GetNewsById(id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(news.CustomUsers.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            _newsRepo.DeleteNews(id);
            MvcApplication.logger.Info($"Member {id} deleted");
            return RedirectToAction("Index");
        }

        // POST: News/PostComment/comment
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult PostComment([Bind(Include = "NewsId,CommentsText")] NewsComments comment)
        {
            if (ModelState.IsValid)
            {
                _newsRepo.AddComment(comment);
                MvcApplication.logger.Info($"Comment {comment.Id} posted to news {comment.NewsId}");
                return RedirectToAction("Details", new { id = comment.NewsId });
            }
            return RedirectToAction("");
        }

        // GET: News/DeleteComment/commentId
        [HttpGet]
        [Authorize]
        public ActionResult DeleteComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = _newsRepo.GetCommentById((int)id);
            if (comment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(comment.CustomUsers.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            return View(comment);
        }

        // POST: News/DeleteComment/commentId
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteComment([Bind(Include = "Id,NewsId,UserId,CommentsText")] NewsComments comment)
        {
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(comment.UserId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _newsRepo.DeleteComment(comment.Id);
                MvcApplication.logger.Info($"Comment {comment.Id} deleted");
                return RedirectToAction("Details", new { id = comment.NewsId });
            }
            return View(comment);
        }

        // GET: News/EditComment/comment
        [HttpGet]
        [Authorize]
        public ActionResult EditComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = _newsRepo.GetCommentById((int)id);
            if (comment == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(comment.CustomUsers.Id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            return View(comment);
        }

        // POST: News/EditComment/comment
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment([Bind(Include = "Id,UserId,NewsId,CreateDate,CommentsText")] NewsComments comment)
        {
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(comment.UserId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            if (ModelState.IsValid)
            {
                _newsRepo.EditComment(comment);
                MvcApplication.logger.Info($"Comment {comment.Id} edited");
                return RedirectToAction("Details", new { id = comment.NewsId });
            }
            return View(comment);
        }

        // POST: News/Like/NewsLikes
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Like([Bind(Include = "NewsId")] NewsLikes like)
        {
            _newsRepo.AddLike(like.NewsId);
            return RedirectToAction("Details", new { id = like.NewsId });
        }

        // POST: News/Dislike/NewsLikes
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Dislike([Bind(Include = "NewsId")] NewsLikes like)
        {
            _newsRepo.DeleteLike(like.NewsId);
            return RedirectToAction("Details", new { id = like.NewsId });
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _newsRepo.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
