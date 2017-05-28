using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMusicBand.Controllers
{
    public class ChatController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


    }
}