
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
                MvcApplication.logger.Error("Cant load albums");
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Albums/Details
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            var album = _repo.GetAlbumById((int)id);
            if (album == null)
            {
                return RedirectToAction("PageNotFound", "Error");
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
        public ActionResult Create([Bind(Include = "Title,Year,file,NumOfTracks")] Album alb)
        {
            // var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                alb.CoverLink = "~/Content/Upload/Albums/no_cover.png";
                _repo.CreateAlbum(alb);               
                if (alb.file != null)
                {
                    FileUploader fu = new FileUploader();
                    fu.changeDB += _repo.UpdateCover;
                    alb.CoverLink = fu.UlpoadFile(alb.file, "\\Content\\Upload\\Albums", System.Web.Hosting.HostingEnvironment.MapPath("~/"), alb.AlbumId);                 
                }
                else
                {
                    MvcApplication.logger.Error("Cant upload cover file");
                    return RedirectToAction("ServerError", "Error");
                }
                return RedirectToAction("Details", new { id = alb.AlbumId });
            }
            return View(alb);
        }

        // GET: Albums/Edit/id
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            Album album = _repo.GetAlbumById((int)id);
            if (album == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            return View(album);
        }

        // POST: Albums/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,Title,Year,file,NumOfTracks")] Album alb)
        {
            if (ModelState.IsValid)
            {
                if (alb.file != null)
                {
                    FileUploader fu = new FileUploader();                   
                    fu.changeDB += _repo.UpdateCover;
                    alb.CoverLink = fu.UlpoadFile(alb.file, "\\Content\\Upload\\Albums", System.Web.Hosting.HostingEnvironment.MapPath("~/"), alb.AlbumId);
                }
                else
                {
                    alb.CoverLink = _repo.GetAlbumById(alb.AlbumId).CoverLink;
                }
                return RedirectToAction("Details", new { id = alb.AlbumId });

            }
            return View(alb);
        }

        // GET: News/Delete/id
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            var news = _repo.GetAlbumById((int)id);
            if (news == null)
            {
                return RedirectToAction("PageNotFound", "Error");
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
                return RedirectToAction("ServerError", "Error");
            }
        }


        /* 
        // GET: Albums/CreateTrack  
        [HttpGet]
        public ActionResult CreateTrack(int id)
        {
            Track track = new Track();
            track.AlbumId = id;
            return View(track);
        }

        
        // POST: Albums/CreateTrack  
        [HttpPost]
        [ValidateAntiForgeryToken]

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
                            return RedirectToAction("ServerError", "Error");
                        }
                    }
                    return RedirectToAction("Details", new { id = trackVM.AlbumId });
                }
                else
                {
                    return RedirectToAction("ServerError", "Error");
                }
            }
            return View(trackVM);
        }

        // Get: Albums/Edit
        [HttpGet]
        public ActionResult EditTrack(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            var track = _repo.GetTrackById((int)id);
            if (track == null)
            {
                return RedirectToAction("PageNotFound", "Error");
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
                    return RedirectToAction("ServerError", "Error"); ;
                }
            }
            return View(track);
        }
        // POST: Albums/DeleteTrack/id
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
                return RedirectToAction("ServerError", "Error");
            }
        }

        // GET: Albums/UploadTrack/id
        [HttpGet]
        public ActionResult UploadTrack(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            Track track = _repo.GetTrackById((int)id);
            if (track == null)
            {
                return RedirectToAction("PageNotFound", "Error");
            }
            return View(track);
        }

        // POST: Albums/UploadTrack
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
        */

        [HttpPost]
        public ActionResult UploadTrack(int id, HttpPostedFileBase file)
        {
            FileUploader fu = new FileUploader();
            fu.changeDB += _repo.UploadTrack;
            if(  fu.UlpoadFile(file, "\\Content\\Upload\\Music", System.Web.Hosting.HostingEnvironment.MapPath("~/"), id) ==null)
            {
                //  Send "false"
                return Json(new { success = false, responseText = "The attached file is not uploaded." }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //  Send "Success"
                return Json(new { success = true}, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose(disposing);
            base.Dispose(disposing);
        }

    }
}
