using DatabaseLayer;
using MyHealth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHealth.Controllers
{
    public class LabApproveController : Controller
    {
        private MyHealthDbEntities db = new MyHealthDbEntities();
       

        public ActionResult PendingAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var lab = (LabTable)Session["Lab"];
            var pendingappointment = db.LabAppointTables.Where(d => d.LabID == lab.LabID && d.IsComplete == false && d.IsFeeSubmit == false);

            return View(pendingappointment);

        }
        public ActionResult CompleteAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var lab = (LabTable)Session["Lab"];
            var completeappointment = db.LabAppointTables.Where(d => d.LabID == lab.LabID && d.IsComplete == true && d.IsFeeSubmit == true);

            return View(completeappointment);

        }

        public ActionResult CurrentAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var lab = (LabTable)Session["Lab"];
            var completeappointment = db.LabAppointTables.Where(d => d.LabID == lab.LabID && d.IsComplete == false && d.IsFeeSubmit == true);

            return View(completeappointment);

        }

        public ActionResult AllAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var lab = (LabTable)Session["Lab"];
            var allappointment = db.LabAppointTables.Where(d => d.LabID == lab.LabID);

            return View(allappointment);

        }

        public ActionResult ChangeStatus(int? id)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var appointment = db.LabAppointTables.Find(id);
            ViewBag.LabTimeSlotID = new SelectList(db.LabTimeSlotTables.Where(d => d.LabID == appointment.LabID), "LabTimeSlotID", "Name", appointment.LabTimeSlotID);
            return View(appointment);
        }
        [HttpPost]
        public ActionResult ChangeStatus(LabAppointTable app)
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
            ViewBag.LabTimeSlotID = new SelectList(db.LabTimeSlotTables.Where(d => d.LabID == app.LabID), "LabTimeSlotID", "Name", app.LabTimeSlotID);
            return View(app);
        }

        public ActionResult ProcessAppointment(int ? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            List<PatientAppointmentReportMV> details = new List<PatientAppointmentReportMV>();

            var appointmnt = db.LabAppointTables.Find(id);
            var testdetails = db.LabTestDetailsTables.Where(p => p.LabTestID == appointmnt.LabTestID);
            foreach(var item in testdetails)
            {
                var detaillist = new PatientAppointmentReportMV()
                {
                    DetailName = item.Name,
                    LabAppointID = appointmnt.LabAppointID,
                    LabTestDetailID = item.LabTestDetailID,
                    MaxValue = item.MaxValue,
                    MinValue = item.MinValue

                };
            }
            ViewBag.TestName = appointmnt.LabTestTable.Name;
            return View(details);
        }

    }
}