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
        private BudgetingContext context;
        public HomeController(BudgetingContext ctx)
        {
            context = ctx;
        }


        public IActionResult Index()
        {
            IEnumerable<Budget> budgets = context.Budgets
                .Include(b => b.ExpenseCategories)
                .Include(b => b.IncomeCategories);
            
            return View("Index", budgets);
        }
    }
}