using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pubg_database.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Map(int id)
        {
            ViewBag.PutId = id;
            return View();
        }
    }
}
