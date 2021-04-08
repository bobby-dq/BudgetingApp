using System.Linq;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;

namespace BudgetingApp.Models.ViewModelFactories
{
    public static class ExpenseCategoryFactory
    {
        public static ExpenseCategoryCrudViewModel Create (Budget budget, ExpenseCategory expenseCategory)
        {
            return new ExpenseCategoryCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                Action="Create",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-pink-200",
                ButtonTheme = "bg-pink-200 hover:bg-pink-300"
            };
        }

        public static ExpenseCategoryCrudViewModel Details (Budget budget, ExpenseCategory expenseCategory)
        {
            return new ExpenseCategoryCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                Action = "Details",
                ReadOnly = true,
                ShowAction = false,
                ActionTheme = "",
                ButtonTheme = ""
            };
        }

        public static ExpenseCategoryCrudViewModel Edit (Budget budget, ExpenseCategory expenseCategory)
        {
            return new ExpenseCategoryCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                Action = "Edit",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-yellow-200",
                ButtonTheme = "bg-yellow-200 hover:bg-yellow-300"
            };
        }

        public static ExpenseCategoryCrudViewModel Delete (Budget budget, ExpenseCategory expenseCategory)
        {
            return new ExpenseCategoryCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory, 
                Action = "Delete",
                ReadOnly = true,
                ShowAction = true,
                ActionTheme = "bg-red-200",
                ButtonTheme = "bg-red-200 hover:bg-red-300"
            };
        }

        public static ExpenseCategoryBreakdownViewModel Breakdown (Budget budget, ExpenseCategory expenseCategory, IQueryable<ExpenseItem> expenseItems)
        {
            return new ExpenseCategoryBreakdownViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                ExpenseItems = expenseItems
            };
        }
    }
}