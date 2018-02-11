using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using DietManagerIdentity.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DietManagerIdentity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            string[] roles =
            {
                "Admin", "Dietician"
            };

            foreach (var item in roles)
            {
                if (!context.Roles.Any(r => r.Name == item))
                {
                    var store = new RoleStore<IdentityRole>(context);
                    var manager = new RoleManager<IdentityRole>(store);
                    var role = new IdentityRole { Name = item };

                    manager.Create(role);
                }
            }

            if (!context.Users.Any(u => u.UserName == "admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "admin", Email = "admin@gmail.com" };

                manager.Create(user, "ChangeItAsap!");
                manager.AddToRole(user.Id, "Admin");
            }


            if (!context.Users.Any(u => u.UserName == "jan123"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "jan123", Email = "jan123@gmail.com" };

                manager.Create(user, "Jan123***");
                manager.AddToRole(user.Id, "Dietician");
            }


            #region Dieticians

            var usersList = context.Users.ToList();
            var userToAdd = usersList.First(u => u.UserName == "jan123");

            var dieticians = new List<Dietician>()
            {
                new Dietician{ FullName="Jan Kowalski", ApplicationUser = userToAdd}
            };

            dieticians.ForEach(d => context.Dieticians.AddOrUpdate(c => c.FullName, d));
            context.SaveChanges();



            #endregion


            #region Diets

            var diets = new List<Diet>
            {
                new Diet{ Name = "Paleo", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Dukana", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Kapuœciana", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "W¹trobowa", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Bia³kowa", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Bananowa", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Œródziemnomorska", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Cukrzycowa", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Montignaca", DateOfAddition= DateTime.Now, Meals = new List<Meal>()},
                new Diet{ Name = "Norweska", DateOfAddition= DateTime.Now, Meals = new List<Meal>()}

            };

            diets.ForEach(d => context.Diets.AddOrUpdate(c => c.Name, d));
            context.SaveChanges();
            #endregion



            #region Patients

            var patients = new List<Patient>()
            {
                new Patient{ FullName="Adam S³odowy", DateOfAddition = DateTime.Now, DietLength=28,
                    NumberOfConsultation = 5, Weight=65, Height=175, PatientAge=33, Allergy="None",
                    Contraindications ="None", Dislikes="None", PlannedWeight = 73, DieticianId = 1, DietId = 1},
                new Patient{ FullName="Krzysztof Krawczyk", DateOfAddition = DateTime.Now, DietLength=66,
                    NumberOfConsultation = 7, Weight=65, Height=175, PatientAge=33, Allergy="None",
                    Contraindications ="None", Dislikes="None", PlannedWeight = 72, DieticianId = 1, DietId = 1},
                new Patient{ FullName="Krzysztof Ibisz", Weight=65, Height=175, PatientAge=33, Allergy="None",
                    Contraindications ="None", Dislikes="None", DateOfAddition = DateTime.Now, DietLength=24,
                    NumberOfConsultation = 7, PlannedWeight = 80, DieticianId = 1, DietId = 5},
                new Patient{ FullName="Mariusz Kowalski", DateOfAddition = DateTime.Now, DietLength=17,
                    NumberOfConsultation = 7, Weight=65, Height=175, PatientAge=33, Allergy="None",
                    Contraindications ="None", Dislikes="None", PlannedWeight = 70, DieticianId = 1, DietId = 7}

            };

            patients.ForEach(p => context.Patients.AddOrUpdate(c => c.FullName, p));
            context.SaveChanges();

            #endregion


            #region WeightData

            var weightDatas = new List<WeightData>
            {
                new WeightData{ Date = new DateTime(2018, 1, 21), Weight = 76, PatientId = 1},
                new WeightData{ Date = new DateTime(2018, 1, 22), Weight = 75, PatientId = 1},
                new WeightData{ Date = new DateTime(2018, 1, 23), Weight = 74, PatientId = 1},
                new WeightData{ Date = new DateTime(2018, 1, 24), Weight = 74, PatientId = 1},
                new WeightData{ Date = new DateTime(2018, 1, 25), Weight = 76, PatientId = 1}
            };
            weightDatas.ForEach(p => context.WeightDatas.AddOrUpdate(c => c.Date, p));
            context.SaveChanges();

            #endregion


            #region Meals

            var meals = new List<Meal>
            {
                new Meal{ Name = "Makaron z pesto", DateOfAddition = DateTime.Now, MealType = MealType.Supper, ShoppingList = "200g pasta, 20g pesto, salt, pepper"},
                new Meal{ Name = "Creme cauliflower's soup", DateOfAddition = DateTime.Now, MealType = MealType.Dinner, ShoppingList = "20dag cauliflower, 1 carrot, salt, pepper"},
                new Meal{ Name = "Raspberry joghurt", DateOfAddition = DateTime.Now, MealType = MealType.SecondBreakfast, ShoppingList = "200ml joghurt, 50 dag raspberries"},
                new Meal{ Name = "Sausage, pickled cucumber", DateOfAddition = DateTime.Now, MealType = MealType.Breakfast, ShoppingList = "3 ham's slices, pickled cucumber"},
                new Meal{ Name = "Graham breed with cottage cheese and fruits", DateOfAddition = DateTime.Now, MealType = MealType.Breakfast, ShoppingList = "1 graham's roll, 1 boiled egg, 1 glass of cottage cheese, 1/2 grapefruit"},
                new Meal{ Name = "Almonds with apple", DateOfAddition = DateTime.Now, MealType = MealType.AfternoonSnack, ShoppingList = "20 almonds, 1 apple"},
                new Meal{ Name = "Cheese-coconut pizza", DateOfAddition = DateTime.Now, MealType = MealType.Dinner, ShoppingList = "220g chesse, 40g cottage cheese, 2 eggs, 45g coconut flour, oregano, basil, garlic"}
            };

            //

            meals.ForEach(m => context.Meals.AddOrUpdate(c => c.Name, m));
            context.SaveChanges();

            #endregion


            foreach (var t in diets)
                foreach (var t1 in meals)
                    AddOrUpdateMealForDiet(context, t.Name, t1.Name);


            context.SaveChanges();

        }

        private void AddOrUpdateMealForDiet(ApplicationDbContext context, string dietName, string mealName)
        {
            var diets = context.Diets.SingleOrDefault(c => c.Name == dietName);
            var meal = diets.Meals.SingleOrDefault(i => i.Name == mealName);
            if (meal == null)
                diets.Meals.Add(context.Meals.Single(i => i.Name == mealName));
        }
    }
}
