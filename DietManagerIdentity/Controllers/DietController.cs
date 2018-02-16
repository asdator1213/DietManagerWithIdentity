using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DietManagerIdentity.Models;

namespace DietManagerIdentity.Controllers
{
    public class DietController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [Authorize(Roles = "Dietician")]
        public ActionResult Diets()
        {
            var diets = _db.Diets.ToList();
            return View(diets);
        }

        [Authorize(Roles = "Dietician")]
        public ActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Dietician")]
        [HttpPost]
        public ActionResult Add(Diet diet)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Login", "Account");
            diet.DateOfAddition = DateTime.Now;
            _db.Diets.Add(diet);
            _db.SaveChanges();
            return RedirectToAction("Diets");

        }

        private void PrepareMealList()
        {
            var mealsList = _db.Meals.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.MealsList = mealsList;
        }

        [Authorize(Roles = "Dietician")]
        public ActionResult AddMeal(int? dietId)
        {
            var diet = _db.Diets.Find(dietId);
            PrepareMealList();

            return View(diet);
        }

        public JsonResult Get_Meals()
        {
            return Json(_db.Meals.Select(m => new {MealId = m.Id, MealName = m.Name}), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMealInfo(Meal meal)
        {
            return new JsonResult{Data = new{ meal.Name}};
        }

        [Authorize(Roles = "Dietician")]
        [HttpPost]
        public ActionResult AddMeal(Diet diet, string mealId)
        {
            int.TryParse(mealId, out var id);

            var meal = _db.Meals
                .SingleOrDefault(p => p.Id == id);

            var dietMeals = _db.Diets
                .SingleOrDefault(p => p.Id == diet.Id);

            if (dietMeals == null)
            {
                int dietId = diet.Id;
                return RedirectToAction("AddMeal", dietId);
            }
            dietMeals.Meals.Add(meal);

            _db.SaveChanges();

            return RedirectToAction("ManageDiet", new { dietId = diet.Id });
        }

        [Authorize(Roles = "Dietician")]
        public ActionResult ManageDiet(int? dietId)
        {
            var diet = _db.Diets.Find(dietId);
            return View(diet);
        }

        [Authorize(Roles = "Dietician")]
        public ActionResult RemoveMealFromDiet(int mealId, int dietId)
        {
            var diet = _db.Diets.Find(dietId);
            if (diet == null)
                return RedirectToAction("Diets");
            var meal = diet.Meals
                .SingleOrDefault(p => p.Id== mealId);

            diet.Meals.Remove(meal);
            _db.SaveChanges();

            return RedirectToAction("ManageDiet", new { dietId = diet.Id });
        }
    }
}