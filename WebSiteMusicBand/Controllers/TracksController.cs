using System;
using System.Collections.Generic;
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

        [System.Web.Http.HttpGet]
        public IEnumerable<Track> Get(int albumId)
        {
            return _repo.GetTracksByAlbumId(albumId).ToArray();
        }

        [System.Web.Http.HttpPost]
        public void Post([FromBody]Track track)
        {
           _repo.CreateTrack(track);
        }

        [System.Web.Http.HttpPut]
        public void Edit([FromBody]Track track)
        {
            _repo.EditTrack(track);           
        }

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
