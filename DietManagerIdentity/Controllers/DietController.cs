using DietManagerIdentity.Models;
using System;
using System.Linq;
using System.Web.Mvc;

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
                return View();
            diet.DateOfAddition = DateTime.Now;
            _db.Diets.Add(diet);
            _db.SaveChanges();
            return RedirectToAction("Diets");
        }
        
        [Authorize(Roles = "Dietician")]
        public ActionResult AddMeal(int? dietId)
        {
            var diet = _db.Diets.Find(dietId);
            return View(diet);
        }

        public JsonResult Get_Meals()
        {
            return Json(_db.Meals.Select(m => new {MealId = m.Id, MealName = m.Name}), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Dietician")]
        [HttpPost]
        public ActionResult AddMeal(Diet diet, string mealId)
        {
            if (!int.TryParse(mealId, out var id))
            {
                ViewBag.Error = "Wystąpił błąd.";
                return RedirectToAction("AddMeal", diet.Id);
            }


            var meal      = _db.Meals.SingleOrDefault(p => p.Id == id);
            var dietMeals = _db.Diets.SingleOrDefault(p => p.Id == diet.Id);


            if (dietMeals == null)
            {
                return RedirectToAction("AddMeal", diet.Id);
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

        public ActionResult GetMealDetails(int id)
        {
            var meal = _db.Meals.Find(id);
            return View(meal);
        }

    }
}