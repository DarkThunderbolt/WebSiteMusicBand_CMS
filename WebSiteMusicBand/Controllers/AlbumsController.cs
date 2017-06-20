
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
        public ActionResult Create([Bind(Include = "Title,Year,file,NumOfTracks")] AlbumEditViewM albumVM)
        {
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                albumVM.CoverPath = "~/Content/Upload/Albums/no_cover.png";
                Album alb = _repo.CreateAlbum(albumVM.ConvertToAlbum());
                if (alb != null)
                {
                    if (albumVM.file != null)
                    {
                        upload += _repo.UpdateCover;
                        albumVM.CoverPath = UlpoadFile(albumVM.file, "~/Content/Upload/Albums", alb.AlbumId);
                    }
                    return RedirectToAction("Details", new { id = albumVM.AlbumId });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return View(albumVM);
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
        public ActionResult Edit([Bind(Include = "AlbumId,Title,Year,file,NumOfTracks")] AlbumEditViewM albumVM)
        {
            if (ModelState.IsValid)
            {
                if (albumVM.file != null)
                {
                    upload += _repo.UpdateCover;
                    albumVM.CoverPath = UlpoadFile(albumVM.file, "~/Content/Upload/Albums", albumVM.AlbumId);
                }
                else
                {
                    albumVM.CoverPath = _repo.GetAlbumById(albumVM.AlbumId).CoverLink;
                }
                if (_repo.EditAlbum(albumVM.ConvertToAlbum()))
                {
                    return RedirectToAction("Details", new { id = albumVM.AlbumId });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return View(albumVM);
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
        public ActionResult Delete([Bind(Include = "AlbumId")] Album album)
        {
            if (_repo.DeleteAlbum(album.AlbumId))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
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
        public ActionResult CreateTrack([Bind(Include = "AlbumId,NameOfTrack,file,Position")] TrackUploadViewM trackVM)
        {
            if (ModelState.IsValid)
            {
                trackVM.TrackLink = "";
                Track track = _repo.CreateTrack(trackVM.ConvertToTrack());
                if (track != null)
                {
                    if (trackVM.file != null)
                    {
                        upload += _repo.UploadTrack;
                        trackVM.TrackLink = UlpoadFile(trackVM.file, "~/Content/Upload/Music", track.TrackId);
                        if (trackVM.TrackLink == null)
                        {
                            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                        }
                    }
                    return RedirectToAction("Details", new { id = trackVM.AlbumId });
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            return View(trackVM);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTrack(int trackId)
        {
            if (_repo.DeleteTrack(trackId))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
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
        public ActionResult UploadTrack([Bind(Include = "AlbumId,TrackId,file")] TrackUploadViewM track)
        {
            if (track.file != null)
            {
                string pic = Path.GetFileName(track.file.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Upload/Music"), pic);
                track.file.SaveAs(path);
                _repo.UploadTrack(path, track.TrackId);
            }
            return RedirectToAction("Edit", "Albums", new { id = track.AlbumId });
        }

        private delegate bool UploadDel(string path, int id);
        UploadDel upload;
        private string UlpoadFile(HttpPostedFileBase file, string folder, int id)
        {
            try
            {
                if (file != null && file.ContentLength > 0)
                {
                    string pic = Path.GetFileName(file.FileName);
                    string path = Path.Combine(Server.MapPath(folder), pic);
                    file.SaveAs(path);
                    if (upload.Invoke(folder + "/" + pic, id))
                    {
                        return folder + "/" + pic;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                upload = null;
            }
        }
        //[HttpGet]
        //public PartialViewResult GetTracks(int albumId)
        //{
        //    IEnumerable<Track> trackss = _repo.GetTracksByAlbumId(albumId);
        //    return PartialView(trackss);
        //}

        public JsonResult GetTracksAJAX(int albumId)
        {
            return Json(_repo.GetTracksByAlbumId(albumId),JsonRequestBehavior.AllowGet);
        }

        public void AddTrackAJAX([Bind(Include = "AlbumId,NameOfTrack,file,Position")]Track track)
        {
            _repo.CreateTrack(track);
        }

        public void EditTrackAJAX([Bind(Include = "AlbumId,NameOfTrack,Position,TrackLink,TrackId")] Track track)
        {
            _repo.EditTrack(track);
        }
        public void DeleteTrackAJAX(int trackId)
        {
            _repo.DeleteTrack(trackId);
        }

        [AllowAnonymous]
        public ActionResult MusicPlayerBar()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            _repo.Dispose(disposing);
            base.Dispose(disposing);
        }

    }
}
