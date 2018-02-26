using DietManagerIdentity.Helpers;
using DietManagerIdentity.Models;
using DietManagerIdentity.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DietManagerIdentity.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize(Roles = "Dietician")]
        public ActionResult Add()
        {
            return View(new Patient());
        }

        [Authorize(Roles = "Dietician")]
        [HttpPost]
        public ActionResult Add(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var loggedDietician    = UserHelper.GetLoggedUser();
                var dieticianId        = DieticianHelper.GetLoggedDieticianId(loggedDietician);
                patient.DieticianId    = dieticianId;
                patient.DateOfAddition = DateTime.Now;

                _db.Patients.Add(patient);
                _db.SaveChanges();

                return RedirectToAction("Patients", "Dietician");
            }
            return View(patient);
        }

        public JsonResult Get_Diets()
        {
            return Json(_db.Diets.Select(d => new { DietId = d.Id, DietName = d.Name}), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Dietician")]
        public ActionResult ManagePatient(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var patientItem = _db.Patients.Find(id);
            if (patientItem == null)
                return RedirectToAction("Patients", "Dietician");

            var patient = PatientHelper.CreatePatientViewModel(patientItem);

            return View(patient);
        }

        [Authorize(Roles = "Dietician")]
        [HttpPost]
        public ActionResult ManagePatient(PatientVm patient, string dietId)
        {
            if (!int.TryParse(dietId, out var id))
            {
                ViewBag.Error = "Wystąpił błąd.";
                return RedirectToAction("Patients", "Dietician");
            }
            
            patient.DietID    = id;
            var patientToSave = PatientHelper.FillPatientFromViewModel(patient);
            var date          = DateTime.Now;

            _db.WeightDatas.Add(new WeightData
            {
                Date      = date.Date,
                Weight    = patient.Weight,
                PatientId = patient.PatientID
            });

            _db.Entry(patientToSave).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Patients", "Dietician");
        }
        
    }
}