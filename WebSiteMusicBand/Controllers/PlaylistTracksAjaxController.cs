using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand.Controllers
{
    public class PlaylistTracksAjaxController : ApiController
    {
        private PlaylistRepository _repo;

        public PlaylistTracksAjaxController()
        {
            _repo = new PlaylistRepository();
        }

        /// <param name="id">Track id</param>
        [System.Web.Http.HttpPost]
        public bool Post(int id, int playlistId)
        {
            bool success = _repo.AddTrackToPlaylist(id, playlistId);
            return success;
        }

        /// <param name="id">Track id</param>
        [System.Web.Http.HttpDelete]
        public void Delete(int id, int playlistId)
        {
            
        }

        /// <param name="id">Track id</param>
        [System.Web.Http.HttpPut]
        public void Put(int id, int oldPlaylistId, int newPlaylistId)
        {

        }
        /// <param name="id">Playlist id</param>
        [System.Web.Http.HttpGet]
        public IEnumerable<Track> Get(int id)
        {              
            return _repo.GetTracksInPlaylist(id);
        }
    }
}
