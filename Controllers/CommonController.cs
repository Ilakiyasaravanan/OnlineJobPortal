using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineJobPortal.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Help()
        {
            return View();
        }
    }
}