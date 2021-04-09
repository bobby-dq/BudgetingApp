using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetingApp.Models.RepositoryModels;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;
using BudgetingApp.Models.ViewModelFactories;

namespace BudgetingApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class IncomeItemController:Controller
    {
        private BudgetingContext context;
        public IncomeItemController(BudgetingContext ctx)
        {
            context = ctx;
        }

        // HTTP Get Request
        // Shows details of an income item
        public async Task<IActionResult> Details (long id)
        {
            IncomeItem incomeItem = await context.IncomeItems.FindAsync(id);
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(incomeItem.IncomeCategoryId);
            Budget budget = await context.Budgets.FindAsync(incomeItem.BudgetId);
            return View("IncomeItemEditor", IncomeItemFactory.Details(budget, incomeCategory, incomeItem));
        }

        // HTTP Get Request
        // Create a new Income Item
        public async Task<IActionResult> Create (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories.FindAsync(id);
            Budget budget = await context.Budgets.FindAsync(incomeCategory.BudgetId);
            IncomeItem incomeItem = new IncomeItem
            {
                IncomeCategoryId = id,
                BudgetId = budget.BudgetId,
                Description = $"{incomeCategory.Description} Transaction Item",
                TransactionDate = DateTime.Now
            };

            return View("IncomeItemEditor", IncomeItemFactory.Create(budget, incomeCategory, incomeItem));
        }
    }
}