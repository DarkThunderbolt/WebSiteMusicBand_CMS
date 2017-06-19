using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteMusicBand.Model
{
    public interface IAlbumRepository
    {
        /// <summary>
        /// Get news selected in section
        /// </summary>
        IEnumerable<Album> Albums { get; }
        /// <summary>
        /// Create new news
        /// </summary>
        /// <param name="album"></param>
        Album CreateAlbum(Album album);
        /// <summary>
        /// Delete news
        /// </summary>
        /// <param name="albumId"></param>
        bool DeleteAlbum(int albumId);
        /// <summary>
        /// Edit news
        /// </summary>
        /// <param name="news"></param>
        bool EditAlbum(Album album);
        /// <summary>
        /// Get news by newsId
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        Album GetAlbumById(int albumId);

        IEnumerable<Track> GetTracksByAlbumId(int albumId);

        bool UpdateCover(string path, int id);

        Track CreateTrack(Track track);

        bool EditTrack(Track track);

        bool DeleteTrack(int trackId);

        bool UploadTrack(string path, int trackId);

        Track GetTrackById(int trackId);

        void Dispose(bool disposing);
    }
}
