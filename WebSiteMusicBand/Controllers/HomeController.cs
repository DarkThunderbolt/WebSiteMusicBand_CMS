using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMusicBand.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.BackgroundPic = MvcApplication.linkToApp + $"Content/img/home.jpg";
            return View();
        }
    }
}