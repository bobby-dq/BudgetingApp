using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using BudgetingApp.Models.RepositoryModels;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}