using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHealth.Controllers
{
    public class AdminController : Controller
    {
        private MyHealthDbEntities db = new MyHealthDbEntities();
        // GET: PatientAppointment
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllDoctor()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var doclist = db.DoctorTables.Where(d => d.UserTable.IsVerified == true);
            return View(doclist);
        }
        public ActionResult AllLab()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var doclist = db.LabTables.Where(d => d.UserTable.IsVerified == true);
            return View(doclist);
        }
     

        public ActionResult AllPatient()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var doclist = db.PatientTables.Where(d => d.UserTable.IsVerified == true);
            return View(doclist);
        }
    }
}