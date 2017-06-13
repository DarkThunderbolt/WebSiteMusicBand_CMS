
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class AlbumsController : Controller
    {
        private IAlbumRepository _repo;

        public AlbumsController(IAlbumRepository albums)
        {
            _repo = albums;
            MvcApplication.logger.Info("Created Members Controller");
        }

        // GET: Albums
        public ActionResult Index()
        {
            var albums = _repo.Albums;
            if (albums != null)
            {
                return View(albums);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        // GET: Albums/Details
        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var album = _repo.GetAlbumById((int)id);
            if (album == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(album);
        }

        // GET: Albums/Create
        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,NumOfTracks")] Album album)
        {
            if (ModelState.IsValid)
            {
                if (_repo.CreateAlbum(album))
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

        // GET: Albums/Edit/id
        [HttpGet]
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _repo.GetAlbumById((int)id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(news);
        }

        // POST: Albums/Edit/id
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Title,Year,CoverLink,NumOfTracks")] Album album)
        {
            if (ModelState.IsValid)
            {
                if (_repo.EditAlbum(album))
                {
                    return RedirectToAction("Details", new { id = album.CoverLink });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return View(album);
        }

        
        [HttpPost]
        [Authorize]
        public ActionResult CoverUpload([Bind(Include = "Id")] int id, HttpPostedFileBase file)
        {
            if (file != null)
            {
                string pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(
                                       Server.MapPath("~/Content/Upload/Albums"), pic);
                // file is uploaded
                file.SaveAs(path);

                // save the image path path to the database or you can send image 
                // directly to database
                // in-case if you want to store byte[] ie. for DB
                //using (MemoryStream ms = new MemoryStream())
                //{
                //    file.InputStream.CopyTo(ms);
                //    byte[] array = ms.GetBuffer();
                //}
                _repo.UpdateCover("~/Content/Upload/Albums/" + pic, id);

            }
            // after successfully uploading redirect the user
            return RedirectToAction("Edit", "Account");
        }

        // GET: News/Delete/id
        [HttpGet]
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var news = _repo.GetAlbumById((int)id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(news);
        }

        // POST: News/Delete/id
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var news = _repo.GetAlbumById(id);
            if (news == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (_repo.DeleteAlbum(id))
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