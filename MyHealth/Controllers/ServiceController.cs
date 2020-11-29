using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHealth.Controllers
{
    public class ServiceController : Controller
    {
        // GET: Service
        public ActionResult BloodBank()
        {
            return View();
        }
        public ActionResult Ambulance()
        {
            return View();
        }
    }
}