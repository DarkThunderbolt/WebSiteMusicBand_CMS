using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSiteMusicBand.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageNotFound()
        {
            return View("404");
        }

        public ActionResult ServerError()
        {
            return View("503");
        }
        
        public ActionResult Maintenance()
        {
            return View("maintenance");
        }
    }
}