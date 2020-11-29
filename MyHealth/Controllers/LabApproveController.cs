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
        public ActionResult AppointmentReport()
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
            List<PatientAppointmentReportMV> detaillist = new List<PatientAppointmentReportMV>();

            var appoint = db.LabAppointTables.Find(id);
            var testdetails = db.LabTestDetailsTables.Where(p => p.LabTestID == appoint.LabTestID);
            foreach (var item in testdetails)
            {

            
            
                var detail = new PatientAppointmentReportMV()
                {
                    DetailName = item.Name,
                    LabAppointID = appoint.LabAppointID,
                    LabTestDetailID = item.LabTestDetailID,
                    MaxValue= item.MaxValue,
                    MinValue = item.MinValue,
                    PatientValue = 0
                };
                detaillist.Add(detail);
            }
            ViewBag.TestName = appoint.LabTestTable.Name;
            return View(detaillist);
        }
        [HttpPost]
        public ActionResult ProcessAppointment(FormCollection collection)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            
            string[] LabTestDetailID = collection["item.LabTestDetailID"].Split(',');
            string[] LabAppointID = collection["item.LabAppointID"].Split(',');
            string[] DetailName = collection["item.DetailName"].Split(',');
            string[] MinValue = collection["item.MinValue"].Split(',');
            string[] MaxValue = collection["item.MaxValue"].Split(',');
            string[] PatientValue = collection["item.PatientValue"].Split(',');

            List<PatientAppointmentReportMV> detaillist = new List<PatientAppointmentReportMV>();
            bool issubmit = false;
            for(int i =0; i < LabTestDetailID.Length; i++)
            {

                var detail = new PatientAppointmentReportMV()
                {
                    DetailName = DetailName[i],
                    LabAppointID = Convert.ToInt32(LabAppointID [i]) ,
                    LabTestDetailID =Convert.ToInt32(LabTestDetailID[i]),
                    MaxValue = Convert.ToInt32(MaxValue[i]),
                    MinValue = Convert.ToInt32(MinValue[i]),
                    PatientValue = Convert.ToInt32(PatientValue[i])
                };
                if(detail.PatientValue > 0) 
                {
                    issubmit = true;
                }
                detaillist.Add(detail);
            }

            if (issubmit == true)
            {


                foreach (var item in detaillist)
                {
                    var ptintdetails = new PatientTestDetailTable()
                    {
                        LabAppointID = item.LabAppointID,
                        LabTestDetailID = item.LabTestDetailID,
                        PatientValue = item.PatientValue
                    };
                    db.PatientTestDetailTables.Add(ptintdetails);
                    db.SaveChanges();
                }

                int appointid = Convert.ToInt32(LabAppointID[0]);
                var appoint = db.LabAppointTables.Find(appointid);
                appoint.IsComplete = true;
                db.Entry(appoint).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("PendingAppointment");
            }
            else
            {
                ViewBag.Message = "Enter Patient Test details";
            }
            return View(detaillist);

        }

        public ActionResult ViewDetails(int? id)
        {

            ViewBag.TestName = db.LabAppointTables.Find(id).LabTestTable.Name;
            ViewBag.Lab = db.LabAppointTables.Find(id).LabTable.Name;
            ViewBag.Patient = db.LabAppointTables.Find(id).PatientTable.Name;
            ViewBag.AppointmentNo = db.LabAppointTables.Find(id).TransectionNo;
            ViewBag.LabLogo= db.LabAppointTables.Find(id).LabTable.Photo;
            ViewBag.PatientPhoto = db.LabAppointTables.Find(id).PatientTable.Photo;

            return View(db.PatientTestDetailTables.Where(p => p.LabAppointID == id).ToList());
        }

    }
}