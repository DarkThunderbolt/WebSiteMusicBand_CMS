using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class NewsController : Controller
    {
        INewsRepository repo;
        private int pageSizeForShort = 3;
        private int pageSizeForLong= 10;

        public NewsController()
        {
            repo = new NewsRepository("News");
        }

        public ActionResult ListLong(int page = 1)
        {
            PagingInfo pageInfo = new PagingInfo(repo.GetTotalNumberOfNews(), pageSizeForLong, page);
            ViewBag.PageInfo = pageInfo;
            return View(repo.GetNewsPage(pageInfo));
        }

        // GET: News
        public ActionResult ListShort(int page = 1)
        {
            PagingInfo pageInfo = new PagingInfo(repo.GetTotalNumberOfNews(), pageSizeForShort, page);
            ViewBag.PageInfo = pageInfo;
            return View(repo.GetNewsPage(pageInfo));
        }



        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = repo.GetNewsById(id); 
            if (repo.GetNewsById(id) == null)
            {
                return HttpNotFound();
            }
            return View(news);
      
        }

        // GET: News/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            //ViewBag.NewsSectionId = new SelectList(db.NewsSections, "Id", "Name");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,TextContent,CreateDate,")] News news)
        {
            if (ModelState.IsValid)
            {
                repo.CreateNews(news,7);
                return RedirectToAction("ShortList");
            }
            return View(news);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = repo.GetNewsById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "Id,Title,TextContent,CreateDate,UserId,NewsSectionId")] News news)
        {
            if (ModelState.IsValid)
            {
                repo.EditNews(news);
                return RedirectToAction("ShortList");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = repo.GetNewsById(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.DeleteNews(id);
            return RedirectToAction("ShortList");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
