using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSiteMusicBand.Model
{
    public class AlbumsRepository : IAlbumRepository
    {
        private MusicBandDB_AlbumsPart db = new MusicBandDB_AlbumsPart();
        
        public IEnumerable<Album> Albums { get { return db.Albums; } }

        public bool CreateAlbum(Album album)
        {
            try
            {
                album.CoverLink = "~/Content/Upload/Albums/no_cover.png";
                db.Albums.Add(album);
                db.SaveChanges();
                MvcApplication.logger.Info($"Album {album.AlbumId} created ");
                return true;
            }
            catch
            {
                MvcApplication.logger.Error(this.ToString() + " create album fail");
                return false;
            }
        }

        public bool DeleteAlbum(int albumId)
        {
            try
            {
                Album album = GetAlbumById(albumId);
                db.Tracks.RemoveRange(GetTracksByAlbumId(albumId));
                db.Albums.Remove(album);
                db.SaveChanges();
                MvcApplication.logger.Info($"Album {albumId} deleted");
                return true;
            }
            catch
            {
                MvcApplication.logger.Error(this.ToString() + " delete album fail");
                return false;
            }
        }

        public bool EditAlbum(Album album)
        {
            try
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.logger.Info($"Album {album.AlbumId} edited");
                return true;
            }
            catch
            {
                MvcApplication.logger.Error(this.ToString() + " edit album fail");
                return false;
            }
        }

        public Album GetAlbumById(int albumId)
        {
            return db.Albums.Where(x => x.AlbumId == albumId).FirstOrDefault();
        }

        public Track GetTrackById(int trackId)
        {
            return db.Tracks.Where(x => x.TrackId == trackId).FirstOrDefault();
        }

        public IEnumerable<Track> GetTracksByAlbumId(int albumId)
        {
            return db.Albums.Where(x => x.AlbumId == albumId).FirstOrDefault().Tracks;
        }

        public bool UpdateCover(string path,int albumId)
        {
            try
            {
                Album album = db.Albums.Find(albumId);
                album.CoverLink = path;
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.logger.Info($"Cover file {album.AlbumId} uploaded");
                return true;
            }
            catch
            {
                MvcApplication.logger.Error(this.ToString() + " cover file upload was fail");
                return false;
            }           
        }

        public bool CreateTrack(Track track)
        {
            try
            {
                track.TrackLink = "";
                db.Tracks.Add(track);
                db.SaveChanges();
                MvcApplication.logger.Info($"Track {track.TrackId} created ");
                return true;
            }
            catch
            {
                MvcApplication.logger.Error(this.ToString() + " create track fail");
                return false;
            }
        }
        public bool EditTrack(Track track)
        {
            try
            {
                db.Entry(track).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.logger.Info($"Trak {track.TrackId} edited");
                return true;
            }
            catch
            {
                MvcApplication.logger.Error(this.ToString() + " edit track fail");
                return false;
            }
        }

        public bool UploadTrack(string path, int trackId)
        {
            try
            {
                Track track = db.Tracks.Find(trackId);
                track.TrackLink = path;
                db.Entry(track).State = EntityState.Modified;
                db.SaveChanges();
                MvcApplication.logger.Info($"Track file {track.TrackId} uplaoded");
                return true;
            }
            catch
            {
                MvcApplication.logger.Error(this.ToString() + "  track file upload was fail");
                return false;
            }
        }
    }
}