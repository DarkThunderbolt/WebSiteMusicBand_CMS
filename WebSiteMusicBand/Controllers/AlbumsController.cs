
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
    [Authorize(Roles = "admin")]
    public class AlbumsController : Controller
    {
        private IAlbumRepository _repo;

        public AlbumsController(IAlbumRepository albums)
        {
            _repo = albums;
            MvcApplication.logger.Trace("Created Album Controller");
        }

        // GET: Albums
        [HttpGet]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Albums/Create
        [HttpPost]
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
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = _repo.GetAlbumById((int)id);
            if (album == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(album);
        }

        // POST: Albums/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Title,Year,CoverLink,NumOfTracks")] Album album)
        {
            if (ModelState.IsValid)
            {
                if (_repo.EditAlbum(album))
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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CoverUpload([Bind(Include = "AlbumId,file")] CoverUploadViewM uploadVM)
        {
            if (uploadVM.file != null && uploadVM.file.ContentLength > 0)
            {
                string pic = Path.GetFileName(uploadVM.file.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Upload/Albums"), pic);
                uploadVM.file.SaveAs(path);
                _repo.UpdateCover(path, uploadVM.AlbumId);
            }
            return RedirectToAction("Edit", "Albums", new {id= uploadVM.AlbumId });
        }

        // GET: News/Delete/id
        [HttpGet]
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
        [ValidateAntiForgeryToken]
        public ActionResult Delete1()
        {
            // !VD
            return View();
        }

        [HttpGet]
        public ActionResult CreateTrack(int id)
        {
            Track track = new Track();
            track.AlbumId = id;
            return View(track);
        }

        // POST: Albums/Create
        [HttpPost]
        public ActionResult CreateTrack([Bind(Include = "AlbumId,NameOfTrack,Position")] Track track)
        {
            if (ModelState.IsValid)
            {
                if (_repo.CreateTrack(track))
                {
                    return RedirectToAction("Details", new { id = track.AlbumId });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return View(track);
        }

        [HttpGet]
        public ActionResult EditTrack(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var track = _repo.GetTrackById((int)id);
            if (track == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            if (!SecureCustomHelper.IsCurrenAdmin())
            {
                return new HttpStatusCodeResult(HttpStatusCode.MethodNotAllowed);
            }
            return View(track);
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrack([Bind(Include = "AlbumId,NameOfTrack,Position,TrackLink,TrackId")] Track track)
        {
            if (ModelState.IsValid)
            {
                if (_repo.EditTrack(track))
                {
                    return RedirectToAction("Details", new { id = track.AlbumId });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return View(track);
        }

        [HttpGet]
        public ActionResult UploadTrack(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Track track = _repo.GetTrackById((int)id);
            if (track == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(track);
        }

        // POST: Albums/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadTrack([Bind(Include = "AlbumId,TrackId,file")] TrackUploadViewModel track)
        {
            if (track.file != null)
            {
                string pic = Path.GetFileName(track.file.FileName);
                string path = Path.Combine(
                Server.MapPath("~/Content/Upload/Music"), pic);
                track.file.SaveAs(path);
                _repo.UploadTrack(path, track.TrackId);
            }
            return RedirectToAction("Edit", "Albums", new {id = track.AlbumId });
        }

    }
}