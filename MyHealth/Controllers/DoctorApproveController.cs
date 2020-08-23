using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHealth.Controllers
{
    public class DoctorApproveController : Controller
    {
        private MyHealthDbEntities db = new MyHealthDbEntities();
        // GET: DoctorApprove
        public ActionResult PendingAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var doc = (DoctorTable)Session["Doctor"];
            var pendingappointment = db.DoctorAppointTables.Where(d => d.DoctorID== doc.DoctorID && d.IsChecked == false && d.IsFeeSubmit == false && string.IsNullOrEmpty(d.DoctorComment) == true);

            return View(pendingappointment);
           
        }
        public ActionResult CompleteAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var doc = (DoctorTable)Session["Doctor"];
            var pendingappointment = db.DoctorAppointTables.Where(d => d.DoctorID == doc.DoctorID && d.IsChecked == true && d.IsFeeSubmit == true && string.IsNullOrEmpty(d.DoctorComment) != true);

            return View(pendingappointment);

        }
        public ActionResult AllAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var doc = (DoctorTable)Session["Doctor"];
            var pendingappointment = db.DoctorAppointTables.Where(d => d.DoctorID == doc.DoctorID );

            return View(pendingappointment);

        }

        public ActionResult ChangeStatus(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var appointment = db.DoctorAppointTables.Find(id);
            ViewBag.DoctorTimeSlotID = new SelectList( db.DoctorTimeSlotTables.Where(d => d.DoctorID == appointment.DoctorID), "DoctorTimeSlotID", "Name", appointment.DoctorTimeSlotID );
            return View(appointment);
        }
        [HttpPost]
        public ActionResult ChangeStatus(DoctorAppointTable app)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(app).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PendingAppointment");
            }
            ViewBag.DoctorTimeSlotID = new SelectList(db.DoctorTimeSlotTables.Where(d => d.DoctorID == app.DoctorID), "DoctorTimeSlotID", "DoctorTimeSlotID", app. DoctorTimeSlotID);
            return View(app);
        }
        public ActionResult CurrentAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var doc = (DoctorTable)Session["Doctor"];
            var currentappointment = db.DoctorAppointTables.Where(d => d.DoctorID == doc.DoctorID && d.IsChecked == false && d.IsFeeSubmit == true && string.IsNullOrEmpty(d.DoctorComment) == true );

            return View(currentappointment);

        }
        public ActionResult ProcessAppointment(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var appointment = db.DoctorAppointTables.Find(id);

            return View(appointment);
        }
        [HttpPost]
        public ActionResult ProcessAppointment(DoctorAppointTable app)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(app).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PendingAppointment");
            }
            return View(app);
        }
    }
}