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

        public IEnumerable<Track> GetTracksByAlbumId(int albumId)
        {
            return db.Albums.Where(x => x.AlbumId == albumId).FirstOrDefault().Tracks;
        }
    
    }
}