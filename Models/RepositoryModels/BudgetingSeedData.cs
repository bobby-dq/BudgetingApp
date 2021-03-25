using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.RepositoryModels
{
    public static class BudgetingSeedData
    {
        public static void SeedBudgetingDatabase (BudgetingContext context) 
        {
            context.Database.Migrate();
            if (context.Budgets.Count() == 0 && context.ExpenseCategories.Count() == 0 &&
                context.ExpenseItems.Count() == 0 && context.IncomeCategories.Count() == 0 &&
                context.IncomeItems.Count() == 0)
            {
                //-------------------------------------------------------
                // Create Budgets
                Budget budgetA = new Budget 
                {
                    Description = "Budget A",
                    StartDate = new DateTime(2021, 01, 01),
                    EndDate = new DateTime(2021, 01, 31)
                };
                Budget budgetB = new Budget 
                {
                    Description = "Budget B",
                    StartDate = new DateTime(2021, 03, 01),
                    EndDate = new DateTime(2021, 03, 31)
                };
                //------------------------------------------------------
                // Create Expense Categories
                ExpenseCategory expenseCategoryAA = new ExpenseCategory 
                {
                    Description = "Expense Category AA",
                    BudgetedAmount = 100, 
                    ExpectedDate = new DateTime(2021, 01, 30)
                };
                ExpenseCategory expenseCategoryAB = new ExpenseCategory 
                {
                    Description = "Expense Category AB",
                    BudgetedAmount = 200, 
                    ExpectedDate = new DateTime(2021, 04, 30)
                };
                ExpenseCategory expenseCategoryBA = new ExpenseCategory 
                {
                    Description = "Expense Category BA",
                    BudgetedAmount = 222, 
                    ExpectedDate = new DateTime(2021, 01, 30)
                };
                ExpenseCategory expenseCategoryBB = new ExpenseCategory 
                {
                    Description = "Expense Category BB",
                    BudgetedAmount = 333, 
                    ExpectedDate = new DateTime(2021, 04, 30)
                };
            }
        }
    }
}