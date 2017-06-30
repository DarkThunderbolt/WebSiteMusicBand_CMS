using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class PlaylistAjaxController : ApiController
    {
        private PlaylistRepository _repo;

        public PlaylistAjaxController()
        {
            _repo = new PlaylistRepository();
        }

        // Get user playlist 
        [System.Web.Http.HttpGet]
        public IEnumerable<Playlist> Get(int id)
        {
            return _repo.GetPlaylistByUserId(id);
        }

        // Create new palylist
        [System.Web.Http.HttpPost]
        public void Post([FromBody]Playlist palylist)
        {
            palylist.UserId = SecureCustomHelper.GetCurrentUserId();
            _repo.CreatePlaylist(palylist);
        }

        // Edit track
        [System.Web.Http.HttpPut]
        public void Put([FromBody]Playlist palylist)
        {
            _repo.EditPlaylist(palylist);
        }

        // Delete track
        [System.Web.Http.HttpDelete]
        public void Delete(int id)
        {
            _repo.DeletePlaylist(id);
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
