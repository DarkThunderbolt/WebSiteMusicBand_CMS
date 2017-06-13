using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class MembersController : Controller
    {
        [Inject]
        INewsRepository _newsRepo;

        public MembersController( INewsRepository news)
        {
            _newsRepo = news;
            MvcApplication.logger.Info("Created Members Controller");
        }

        // GET: Members
        [HttpGet]
        public ActionResult Index()
        {
            var members = _newsRepo.SelectedNews;
            if (members != null)
            {
                return View(members);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
           
        }

        // GET: Members/Create
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,TextContent,CreateDate,")] News news)
        {
            if (ModelState.IsValid)
            {
                if(_newsRepo.CreateNews(news))
                {
                    return RedirectToAction("Details", new { id = news.Id });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }  
            }
            return View(news);
        }

        // GET: Members/Details/id
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

        // GET: Members/Edit/id
        [HttpGet]
        [Authorize(Roles = "admin")]
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

        // POST: Members/Edit/id
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,TextContent,CreateDate,UserId,NewsSectionId")] News news)
        {
            if (!SecureCustomHelper.IsThisCurrentUserOrAdmin(news.UserId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            if (ModelState.IsValid)
            {
                if(_newsRepo.EditNews(news))
                {
                    return RedirectToAction("Details", new { id = news.Id });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }               
            return View(news);
        }

        // GET: Members/Delete/id
        [HttpGet]
        [Authorize(Roles = "admin")]
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

        // POST: Members/Delete/id
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
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
            if (_newsRepo.DeleteNews(id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}