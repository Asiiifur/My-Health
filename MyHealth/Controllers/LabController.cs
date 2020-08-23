using DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyHealth.Controllers
{
    public class LabController : Controller
    {
        private MyHealthDbEntities db = new MyHealthDbEntities();

        // GET: Lab

      
        
        public ActionResult AllLabTest()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (Session["Lab"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var lab = (LabTable)Session["Lab"];

            var textlist = db.LabTestTables.Where(l => l.LabID == lab.LabID).ToString();
            return View(textlist);
        }
        public ActionResult AddTest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddTest(LabTestTable test)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }



            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["Lab"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var lab = (LabTable)Session["Lab"];
            test.LabID = lab.LabID;
            if (ModelState.IsValid)
            {
                var findlab = db.LabTestTables.Where(l => l.Name == test.Name).FirstOrDefault();
                if(findlab == null)
                {
                    db.LabTestTables.Add(test);
                    db.SaveChanges();
                    return RedirectToAction("LabAllTest");
                }
                else
                {
                    ViewBag.Message = "Already Registered";
                }
            }

            return View(test);
        }

        public ActionResult TestDetails(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            Session["LabTestID"] = id;
            var detailslist = db.LabTestDetailsTables.Where(l => l.LabTestID == id).ToList();
            return View(detailslist);
        }

        public ActionResult AddTestDetails()
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddTestDetails(LabTestDetailsTable testDetails)
        {

           
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["Lab"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            var lab = (LabTable)Session["Lab"];
            testDetails.LabID = lab.LabID;
            int testid = Convert.ToInt32(Convert.ToString(Session["LabTestID"]));
            testDetails.LabTestID = testid;

            if (ModelState.IsValid)
            {
                var findDetails = db.LabTestDetailsTables.Where(l => l.Name == testDetails.Name).FirstOrDefault();
                if (findDetails == null)
                {
                    db.LabTestDetailsTables.Add(testDetails);
                    db.SaveChanges();
                    return RedirectToAction("TestDetails", new { id = testid });
                }
                else
                {
                    ViewBag.Message = "Already Registered";
                }
            }
            return View(testDetails);
        }
        public ActionResult EditTest(int ? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var labtest = db.LabTestTables.Find(id);
            return View(labtest);
        }
        [HttpPost]
        public ActionResult EditTest(LabTestTable test)
        {

            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["Lab"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
           
            if (ModelState.IsValid)
            {
                var findlab = db.LabTestTables.Where(l => l.Name == test.Name && l.LabTestID != test.LabTestID ).FirstOrDefault();
                if (findlab == null)
                {
                   db.Entry(test).State=System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("LabAllTest");
                }
                else
                {
                    ViewBag.Message = "Already Registered";
                }
            }

            return View(test);
        }

        public ActionResult EditTestDetails(int ? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            var details = db.LabTestDetailsTables.Find(id);

            return View(details);
        }
        [HttpPost]
        public ActionResult EditTestDetails(LabTestDetailsTable testDetails)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }


            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }

            if (Session["Lab"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            if (ModelState.IsValid)
            {
                var findDetails = db.LabTestDetailsTables.Where(l => l.Name == testDetails.Name && l.LabTestDetailID != testDetails.LabTestDetailID).FirstOrDefault();
                if (findDetails == null)
                {
                    db.Entry(testDetails).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("TestDetails",new { id = testDetails.LabTestID });
                }
                else
                {
                    ViewBag.Message = "Already Registered";
                }
            }
            return View(testDetails);
        }


    }
}