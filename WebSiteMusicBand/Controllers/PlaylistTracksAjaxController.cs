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

        /// <summary>
        /// Add track to palylist
        /// </summary>
        /// <param name="id">Track id</param>
        [System.Web.Http.HttpPost]
        public bool Post(int id, int playlistId)
        {
            bool success = _repo.AddTrackToPlaylist(id, playlistId);
            return success;
        }

        /// <summary>
        /// Delete track from playlist
        /// </summary>
        /// <param name="id">Track id</param>
        [System.Web.Http.HttpDelete]
        public bool Delete(int id, int playlistId)
        {
            bool success = _repo.DeleteTrackFromPlaylist(id, playlistId);
            return success;
        }

        /// <summary>
        /// Move track to other playlist
        /// </summary>
        /// <param name="id">Track id</param>
        [System.Web.Http.HttpPut]
        public bool Put(int id, int oldPlaylistId, int newPlaylistId)
        {
            bool success = _repo.MoveTrackToPlaylist(id, oldPlaylistId, newPlaylistId);
            return success;
        }

        /// <summary>
        /// Get tracks from playlist
        /// </summary>
        /// <param name="id">Playlist id</param>
        [System.Web.Http.HttpGet]
        public IEnumerable<Track> Get(int id)
        {              
            return _repo.GetTracksInPlaylist(id);
        }
    }
}
