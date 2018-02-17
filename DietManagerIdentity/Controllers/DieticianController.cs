﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DietManagerIdentity.Helpers;
using DietManagerIdentity.Models;
using DietManagerIdentity.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;


namespace DietManagerIdentity.Controllers
{
    public class DieticianController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        // GET: Dietician
        public ActionResult Dieticians()
        {
            var dieticians = _db.Dieticians.ToList();
            return View(dieticians);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var dietician = _db.Dieticians.Find(id);

            var username = dietician.ApplicationUser.UserName;

            var user = new AccountDieticianVM
            {
                Login = username,
                FullName = dietician.FullName
            };

            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveConfirmed(int id)
        {
            var dietician = _db.Dieticians.Find(id);

            var user = _db.Users.Find(dietician.ApplicationUser.Id);

            _db.Dieticians.Remove(dietician);
            _db.Users.Remove(user);

            _db.SaveChanges();
            return RedirectToAction("Dieticians");
        }

        [Authorize(Roles = "Dietician")]
        public ActionResult Patients()
        {
            var loggedUser = UserHelper.GetLoggedUser();

            var dietician = _db.Dieticians
                .SingleOrDefault(p => p.ApplicationUser.Id == loggedUser.Id);

            if (dietician == null)
                return RedirectToAction("Login", "Account");

            var dieticianId = dietician.Id;

            var patients = _db.Patients
                .Where(p => p.DieticianId == dieticianId).ToList();

            return View(patients);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return RedirectToAction("Register", "Account");
        }

        
    }
}