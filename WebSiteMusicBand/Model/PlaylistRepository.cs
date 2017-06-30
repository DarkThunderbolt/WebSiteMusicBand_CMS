using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSiteMusicBand.Model;

namespace WebSiteMusicBand
{
    public class PlaylistRepository
    {
        private MusicBandDB_AlbumsPart _db = new MusicBandDB_AlbumsPart();
        public IEnumerable<Playlist> Playlists { get { return _db.Playlist; } }

        public IEnumerable<Playlist> GetPlaylistByUserId(int userId)
        {
            return Playlists.Where(x => x.UserId == userId).OrderBy(x => x.Position);
        }
        public IEnumerable<Track> GetTracksInPlaylist(int playlistId)
        {
            return _db.Playlist.Find(playlistId).Tracks;
        }

        public Playlist CreatePlaylist(Playlist palylist)
        {
            try
            {
                _db.Playlist.Add(palylist);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Track {palylist.PlaylistId} created");
                return GetPlaylistById(palylist.PlaylistId);
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + ". Creating new track end with fail");
                return null;
            }
        }

        public bool EditPlaylist(Playlist palylist)
        {
            try
            {
                _db.Entry(palylist).State = EntityState.Modified;
                _db.SaveChanges();
                MvcApplication.logger.Info($"Playlis {palylist.PlaylistId} was edited");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Editing playlis ({palylist.PlaylistId}) end with fail");
                return false;
            }
        }

        public bool DeletePlaylist(int palylistId)
        {
            try
            {
                Playlist palylist = GetPlaylistById(palylistId);
                _db.Playlist.Remove(palylist);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Playlist ({palylistId}) deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Deleting of playlist ({palylistId}) end with fail");
                return false;
            }
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        private Playlist GetPlaylistById(int trackId)
        {
            return _db.Playlist.Find(trackId);
        }
    }
}