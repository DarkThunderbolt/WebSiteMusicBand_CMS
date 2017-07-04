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
            List<Track> tracks = new List<Track>();
            int i = 0;
            foreach (var item in _db.TracksInPlaylists.Where(x => x.PlaylistId == playlistId).OrderBy(x=>x.Position))
            {
                item.Tracks.GridPosition = i++;
                tracks.Add(item.Tracks);
            }
            return tracks;
        }

        public Playlist CreatePlaylist(Playlist palylist)
        {
            try
            {
                _db.Playlist.Add(palylist);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Track ({palylist.PlaylistId}) created");
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
                MvcApplication.logger.Info($"Playlis ({palylist.PlaylistId}) was edited");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Editing playlis ({palylist.PlaylistId}) end with fail");
                return false;
            }
        }

        public bool DeletePlaylist(int playlistId)
        {
            try
            {
                Playlist palylist = GetPlaylistById(playlistId);
                _db.TracksInPlaylists.RemoveRange(palylist.TracksInPlaylists);                
                _db.Playlist.Remove(palylist);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Playlist ({playlistId}) deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Deleting of playlist ({playlistId}) end with fail");
                return false;
            }
        }

        public bool AddTrackToPlaylist(int trackId, int playlistId)
        {
            try
            {
                int? maxPos = _db.Playlist.Find(playlistId).TracksInPlaylists.Max(x => x.Position);
                if (maxPos == null)
                {
                    maxPos = 0;
                }
                _db.TracksInPlaylists.Add(new TracksInPlaylists(){ TrackId = trackId, PlaylistId = playlistId, Position = (int)maxPos+1 });
                _db.SaveChanges();
                MvcApplication.logger.Info($"Track ({trackId}) was add to palylist ({playlistId})");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Adding track ({trackId}) to palylist ({playlistId}) end with fail");
                return false;
            }
        }

        public bool DeleteTrackFromPlaylist(int trackid, int playlistId)
        {
            try
            {
                TracksInPlaylists temp = _db.TracksInPlaylists.First(x => x.TrackId == trackid && x.PlaylistId == playlistId);
                _db.TracksInPlaylists.Remove(temp);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Track({trackid}) in playlist({playlistId}) was deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Deleting track({trackid}) in playlist({playlistId}) end with fail");
                return false;
            }
        }

        public bool MoveTrackToPlaylist(int trackid, int oldPlaylistId, int newPlaylistId)
        {
            try
            {
                DeleteTrackFromPlaylist(trackid, oldPlaylistId);
                AddTrackToPlaylist(trackid, newPlaylistId);
                MvcApplication.logger.Info($"Track({trackid}) was move from playlis ({oldPlaylistId}) to playlis ({newPlaylistId})");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Track({trackid}) was move from playlis ({oldPlaylistId}) to playlis ({newPlaylistId}) end with fail");
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

        private Playlist GetPlaylistById(int playlistId)
        {
            return _db.Playlist.Find(playlistId);
        }
    }
}