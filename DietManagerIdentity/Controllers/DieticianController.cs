using DietManagerIdentity.Helpers;
using DietManagerIdentity.Models;
using DietManagerIdentity.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;


namespace DietManagerIdentity.Controllers
{
    public class DieticianController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
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
            var username  = dietician.ApplicationUser.UserName;

            var user = new AccountDieticianVM
            {
                Login    = username,
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
            var user      = _db.Users.Find(dietician.ApplicationUser.Id);

            _db.Dieticians.Remove(dietician);
            _db.Users.Remove(user);

            _db.SaveChanges();
            return RedirectToAction("Dieticians");
        }

        [Authorize(Roles = "Dietician")]
        public ActionResult Patients()
        {
            var loggedUser = UserHelper.GetLoggedUser();
            var dietician  = _db.Dieticians
                .SingleOrDefault(p => p.ApplicationUser.Id == loggedUser.Id);

            if (dietician == null)
                return RedirectToAction("Login", "Account");

            var patients = _db.Patients
                .Where(p => p.DieticianId == dietician.Id).ToList();

            return View(patients);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add()
        {
            return RedirectToAction("Register", "Account");
        }

        
    }
}