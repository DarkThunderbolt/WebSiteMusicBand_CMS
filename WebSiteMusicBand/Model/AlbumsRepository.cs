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
                MvcApplication.logger.Info($"Album({album.AlbumId}) was created ");
                return GetAlbumById(album.AlbumId);
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + ". Creating new album end with fail");
                return null;
            }
        }

        public bool DeleteAlbum(int albumId)
        {
            try
            {
                Album album = GetAlbumById(albumId);
                foreach(var item in GetTracksByAlbumId(albumId))
                {
                    if (!DeleteTrack(item.TrackId))
                    {
                        MvcApplication.logger.Info($"Can`t delete all tracks in album {albumId}");
                        return false;
                    }                       
                }
                //_db.Tracks.RemoveRange(GetTracksByAlbumId(albumId));
                _db.Albums.Remove(album);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Album({albumId}) was deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Deleting album({albumId}) end with fail");
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
                MvcApplication.logger.Info($"Album({album.AlbumId}) was edited");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Editing album({album.AlbumId}) end with fail");
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
            SetAllGridPositions(albumId);
            return _db.Albums.Find(albumId).Tracks.OrderBy(x => x.Position).ToArray();
        }

        public bool UpdateCover(string path,int albumId)
        {
            try
            {
                Album album = _db.Albums.Find(albumId);
                album.CoverLink = path;
                _db.Entry(album).State = EntityState.Modified;
                _db.SaveChanges();
                MvcApplication.logger.Info($"Cover file for album({album.AlbumId}) was uploaded");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Updating cover to album({albumId}) end with fail");
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
                MvcApplication.logger.Error(e.ToString() + ". Creating new track end with fail");
                return null;
            }
        }
        public bool EditTrack(Track track)
        {
            try
            {
                _db.Entry(track).State = EntityState.Modified;
                _db.SaveChanges();
                MvcApplication.logger.Info($"Track {track.TrackId} was edited");
                return true;
            }
            catch(Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Editing track({track.TrackId}) end with fail");
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
                MvcApplication.logger.Info($"Track file for track({track.TrackId}) was uplaoded");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Uploading track file ({trackId}) end  with fail");
                return false;
            }
        }

        public bool DeleteTrack(int trackId)
        {
            try
            {
                Track track = GetTrackById(trackId);
                _db.Tracks.Remove(track);
                _db.SaveChanges();
                MvcApplication.logger.Info($"Album {trackId} deleted");
                return true;
            }
            catch (Exception e)
            {
                MvcApplication.logger.Error(e.ToString() + $". Deletin of track ({trackId}) end with fail");
                return false;
            }

        }

        private void SetAllGridPositions(int albumId)
        {
            int count = 0;
            foreach(var item in _db.Albums.Find(albumId).Tracks.OrderBy(x => x.Position).ToArray())
            {
                item.GridPosition = count++;
            }
        }
        
        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }            
        }
    }
}