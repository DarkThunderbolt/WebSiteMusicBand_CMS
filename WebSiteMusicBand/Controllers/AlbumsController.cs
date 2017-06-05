using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class AlbumsController : Controller
    {
        private IAlbumRepository _albumRepo;

        public AlbumsController(IAlbumRepository albums)
        {
            _albumRepo = albums;
            MvcApplication.logger.Info("Created Members Controller");
        }

        // GET: Albums
        public ActionResult Index()
        {
            var albums = _albumRepo.Albums;
            if (albums != null)
            {
                return View(albums);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _albumRepo.GetAlbumById((int)id);
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
        public ActionResult Create([Bind(Include = "Id,Title,TextContent,CreateDate,")] Album album)
        {
            if (ModelState.IsValid)
            {
                if (_albumRepo.CreateAlbum(album))
                {
                    return RedirectToAction("Details", new { id = album.AlbumId });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }

            }
            return View(album);
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
            var news = _albumRepo.GetAlbumById((int)id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(news);
        }

        // POST: News/Edit/newsId
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,TextContent,CreateDate,UserId,NewsSectionId")] Album album)
        {
            if (ModelState.IsValid)
            {
                if (_albumRepo.EditAlbum(album))
                {
                    return RedirectToAction("Details", new { id = album.AlbumId });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return View(album);
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
            var news = _albumRepo.GetAlbumById((int)id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _albumRepo.GetAlbumById(id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (_albumRepo.DeleteAlbum(id))
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