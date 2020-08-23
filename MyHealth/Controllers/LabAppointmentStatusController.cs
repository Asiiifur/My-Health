using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHealth.Controllers
{
    public class LabAppointmentStatusController : Controller
    {

        private MyHealthDbEntities db = new MyHealthDbEntities();
        // GET:LabAppointmentStatus
        public ActionResult PendingAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var pendingappointment = db.LabAppointTables.Where(d => d.PatientID == patient.PatientID && d.IsComplete == false && d.IsFeeSubmit == false);


            return View(pendingappointment);
        }
        public ActionResult CurrentAppointment()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var currentappointment = db.LabAppointTables.Where(d => d.PatientID == patient.PatientID && d.IsComplete == false && d.IsFeeSubmit == true);

            return View(currentappointment);
        }
        public ActionResult CompleteAppointment()
        {


            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var completeappointment = db.LabAppointTables.Where(d => d.PatientID == patient.PatientID && d.IsComplete == true && d.IsFeeSubmit == true);

            return View(completeappointment);
        }
        public ActionResult CancleAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var cancleappointment = db.LabAppointTables.Where(d => d.PatientID == patient.PatientID  &&( d.IsComplete == false || d.IsFeeSubmit == false) && d.AppointDate < DateTime.Now);

            return View(cancleappointment);
        }
        public ActionResult AllAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var allappointment = db.LabAppointTables.Where(d => d.PatientID == patient.PatientID);

            return View(allappointment);
        }
    }
}