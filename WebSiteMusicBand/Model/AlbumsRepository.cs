using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class AlbumsRepository : IAlbumRepository
    {
        private MusicBandDB_AlbumsPart _db = new MusicBandDB_AlbumsPart();
        
        public IEnumerable<Album> Albums { get { return _db.Albums; } }

        public Album CreateAlbum(Album album)
        {
            try
            {
                _db.Albums.Add(album);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Album {album.AlbumId} created ");
                return GetAlbumById(album.AlbumId);
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + " create album fail");
                return null;
            }
        }

        public bool DeleteAlbum(int albumId)
        {
            try
            {
                Album album = GetAlbumById(albumId);
                _db.Tracks.RemoveRange(GetTracksByAlbumId(albumId));
                _db.Albums.Remove(album);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Album {albumId} deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + " delete album fail");
                return false;
            }
        }

        public bool EditAlbum(Album album)
        {
            try
            {
                var entity = _db.Albums.Where(c => c.AlbumId == album.AlbumId).FirstOrDefault();
                _db.Entry(entity).CurrentValues.SetValues(album);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Album {album.AlbumId} edited");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + " edit album fail");
                return false;
            }
        }

        public Album GetAlbumById(int albumId)
        {
            return _db.Albums.Where(x => x.AlbumId == albumId).FirstOrDefault();
        }

        public Track GetTrackById(int trackId)
        {
            return _db.Tracks.Find(trackId);
        }

        public IEnumerable<Track> GetTracksByAlbumId(int albumId)
        {
            return _db.Albums.Find(albumId).Tracks;
        }

        public bool UpdateCover(string path,int albumId)
        {
            try
            {
                Album album = _db.Albums.Find(albumId);
                album.CoverLink = path;
                _db.Entry(album).State = EntityState.Modified;
                _db.SaveChanges();
                MvcApplication.logger.Info($"Cover file {album.AlbumId} uploaded");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + " cover file upload was fail");
                return false;
            }           
        }

        public Track CreateTrack(Track track)
        {
            try
            {
                _db.Tracks.Add(track);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Track {track.TrackId} created ");
                return GetTrackById(track.TrackId);
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + " create track fail");
                return null;
            }
        }
        public bool EditTrack(Track track)
        {
            try
            {
                _db.Entry(track).State = EntityState.Modified;
                _db.SaveChanges();
                MvcApplication.logger.Info($"Trak {track.TrackId} edited");
                return true;
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + " edit track fail");
                return false;
            }
        }

        public bool UploadTrack(string path, int trackId)
        {
            try
            {
                Track track = _db.Tracks.Find(trackId);
                track.TrackLink = path;
                _db.Entry(track).State = EntityState.Modified;
                _db.SaveChanges();
                MvcApplication.logger.Info($"Track file {track.TrackId} uplaoded");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + "  track file upload was fail");
                return false;
            }
        }
    }
}