using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebSiteMusicBand.Controllers
{
    public class PlaylistTracksAjaxController : ApiController
    {
        /// <summary>
        /// add track in playlist
        /// </summary>
        /// <param name="id"> playlist id </param>
        /// <param name="trackId"></param>
        [System.Web.Http.HttpPut]
        public void Put(int id,int trackId)
        {
            
        }
    }
}
