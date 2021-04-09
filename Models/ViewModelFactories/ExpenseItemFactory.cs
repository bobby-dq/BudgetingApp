using System.Linq;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;

namespace BudgetingApp.Models.ViewModelFactories
{
    public static class ExpenseItemFactory
    {
        public static ExpenseItemCrudViewModel Create (Budget budget, ExpenseCategory expenseCategory, ExpenseItem expenseItem)
        {
            return new ExpenseItemCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                ExpenseItem = expenseItem,
                Action = "Create",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-indigo-200",
                ButtonTheme = "bg-indigo-200 hover:bg-indigo-300"
            };
        }

        public static ExpenseItemCrudViewModel Details (Budget budget, ExpenseCategory expenseCategory, ExpenseItem expenseItem)
        {
            return new ExpenseItemCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                ExpenseItem = expenseItem,
                Action = "Details",
                ReadOnly = true,
                ShowAction = false,
                ActionTheme = "",
                ButtonTheme = ""
            };
        }

        public static ExpenseItemCrudViewModel Edit (Budget budget, ExpenseCategory expenseCategory, ExpenseItem expenseItem)
        {
            return new ExpenseItemCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                ExpenseItem = expenseItem,
                Action = "Edit",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-yellow-200",
                ButtonTheme = "bg-yellow-200 hover:bg-yellow-300"
            };
        }

        public static ExpenseItemCrudViewModel Delete (Budget budget, ExpenseCategory expenseCategory, ExpenseItem expenseItem)
        {
            return new ExpenseItemCrudViewModel
            {
                Budget = budget,
                ExpenseCategory = expenseCategory,
                ExpenseItem = expenseItem, 
                Action = "Delete",
                ReadOnly = true,
                ShowAction = true,
                ActionTheme = "bg-red-200",
                ButtonTheme = "bg-red-200 hover:bg-red-300"
            };
        }
    }
}