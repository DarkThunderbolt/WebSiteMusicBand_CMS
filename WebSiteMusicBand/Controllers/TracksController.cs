using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class TracksController : ApiController
    {
        private AlbumsRepository _repo;

        public TracksController()
        {
            _repo = new AlbumsRepository();
        }

        // Get tracks in album
        [System.Web.Http.HttpGet]
        public IEnumerable<Track> Get(int albumId)
        {
            return _repo.GetTracksByAlbumId(albumId);
        }

        // Create new track
        [System.Web.Http.HttpPost]
        public void Post(int id,[FromBody]Track track)
        {
           track.AlbumId = id;
           _repo.CreateTrack(track);
        }

        // Edit track
        [System.Web.Http.HttpPut]
        public void Put([FromBody]Track track)
        {
            _repo.EditTrack(track);           
        }

        // Delete track
        [System.Web.Http.HttpDelete]
        public void Delete(int id)
        {
            _repo.DeleteTrack(id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose(disposing);
            base.Dispose(disposing);
        }


    }
}
