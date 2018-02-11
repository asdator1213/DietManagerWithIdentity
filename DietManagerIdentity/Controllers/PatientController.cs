using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DietManagerIdentity.Models;
using DietManagerIdentity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DietManagerIdentity.Controllers
{
    public class PatientController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize(Roles = "Dietician")]
        public ActionResult Add()
        {
            var patient = new Patient();
            return View(patient);
        }

        [Authorize(Roles = "Dietician")]
        [HttpPost]
        public ActionResult Add(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var loggedDietician = System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

                var dieticianId = _db.Dieticians
                    .Single(p => p.ApplicationUser.Id == loggedDietician.Id).Id;

                patient.DieticianId = dieticianId;
                patient.DateOfAddition = DateTime.Now;


                _db.Patients.Add(patient);
                _db.SaveChanges();

                return RedirectToAction("Patients", "Dietician");
            }
            return View(patient);
        }

        private void PrepareDietsList()
        {
            var dietList = _db.Diets.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            ViewBag.DietList = dietList;

        }

        [Authorize(Roles = "Dietician")]
        public ActionResult ManagePatient(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            PrepareDietsList();

            var patientItem = _db.Patients.Find(id);

            if (patientItem == null)
                return RedirectToAction("Patients", "Dietician");

            var patient = new PatientVm
            {
                PatientID = patientItem.Id,
                FullName = patientItem.FullName,
                DateOfAddition = patientItem.DateOfAddition,
                Height = patientItem.Height,
                Weight = patientItem.Weight,
                PatientAge = patientItem.PatientAge,
                Dislikes = patientItem.Dislikes,
                Contraindications = patientItem.Contraindications,
                Allergy = patientItem.Allergy,
                NumberOfConsultation = patientItem.NumberOfConsultation,
                PlannedWeight = patientItem.PlannedWeight,
                DietLength = patientItem.DietLength,
                WagesList = patientItem.WeightDatas.Select(p => p.Weight).ToArray(),
                WagesDates = patientItem.WeightDatas.Select(p => p.Date).ToArray(),
                DieticianID = patientItem.DieticianId,
                DietID = patientItem.DietId,
                Diet = patientItem.Diet
            };

            return View(patient);
        }

        [Authorize(Roles = "Dietician")]
        [HttpPost]
        public ActionResult ManagePatient(PatientVm patient, string dietId)
        {
            int.TryParse(dietId, out var id);
            patient.DietID = id;

            var patientToSave = new Patient
            {
                Id = patient.PatientID,
                FullName = patient.FullName,
                DateOfAddition = patient.DateOfAddition,
                Height = patient.Height,
                Weight = patient.Weight,
                PatientAge = patient.PatientAge,
                Dislikes = patient.Dislikes,
                Contraindications = patient.Contraindications,
                Allergy = patient.Allergy,
                NumberOfConsultation = patient.NumberOfConsultation,
                PlannedWeight = patient.PlannedWeight,
                DietLength = patient.DietLength,

                DieticianId = patient.DieticianID,
                DietId = patient.DietID,
            };

            var date = DateTime.Now;

            _db.WeightDatas.Add(new WeightData
            {
                Date = date.Date,
                Weight = patient.Weight,
                PatientId = patient.PatientID
            });

            _db.Entry(patientToSave).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Patients", "Dietician");
        }
        
    }
}