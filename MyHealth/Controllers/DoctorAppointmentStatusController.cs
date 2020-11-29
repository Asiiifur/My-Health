using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHealth.Controllers
{

    public class DoctorAppointmentStatusController : Controller
    {

        private MyHealthDbEntities db = new MyHealthDbEntities();
        // GET: DoctorAppointmentStatus
        public ActionResult PendingAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            var patient = (PatientTable)Session["Patient"];
            var pendingappointment = db.DoctorAppointTables.Where(d => d.PatientID == patient.PatientID && d.IsChecked == false && d.IsFeeSubmit == false && d.DoctorComment.Trim().Length == 0);

         
            return View(pendingappointment);
        }
        public ActionResult CurrentAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var currentappointment = db.DoctorAppointTables.Where(d => d.PatientID == patient.PatientID && d.IsChecked == false && d.IsFeeSubmit == true );


            return View(currentappointment);
        }
        public ActionResult CompleteAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var completeappointment = db.DoctorAppointTables.Where(d => d.PatientID == patient.PatientID && d.IsChecked == true && d.IsFeeSubmit == true && d.DoctorComment.Trim().Length > 0);


            return View(completeappointment);
        }
        public ActionResult CancleAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var cancleappointment = db.DoctorAppointTables.Where(d => d.PatientID == patient.PatientID && d.IsChecked == false && d.IsFeeSubmit == true &&  d.DoctorComment.Trim().Length > 0);


            return View(cancleappointment);
        }
        public ActionResult AllAppointment()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var patient = (PatientTable)Session["Patient"];
            var allappointment = db.DoctorAppointTables.Where(d => d.PatientID == patient.PatientID);


            return View(allappointment);
        }
    }
}