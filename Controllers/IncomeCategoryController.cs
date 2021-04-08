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
    public class IncomeCategoryController: Controller
    {
        private BudgetingContext context;
        public IncomeCategoryController (BudgetingContext ctx)
        {
            context = ctx;
        }

        // HTTP Get Request
        // A breadown of a user's Income Category
        public async Task<IActionResult> Breakdown (long id)
        {
            IncomeCategory incomeCategory = await context.IncomeCategories
                .FirstAsync(ic => ic.IncomeCategoryId == id);

            Budget budget = await context.Budgets
                .FirstAsync(b => b.BudgetId == incomeCategory.BudgetId);
            
            IQueryable<IncomeItem> incomeItems = context.IncomeItems
                .Where(ii => ii.IncomeCategoryId == id);

            IncomeCategoryBreakdownViewModel incomeCategoryBreakdownViewModel = new IncomeCategoryBreakdownViewModel 
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                IncomeItems = incomeItems
            };

            return View("Breakdown", incomeCategoryBreakdownViewModel);
        }
    }
}