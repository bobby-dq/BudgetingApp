using System.Linq;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;

namespace BudgetingApp.Models.ViewModelFactories
{
    public static class BudgetFactory
    {
        public static BudgetCrudViewModel Create(Budget budget) 
        {
            return new BudgetCrudViewModel
            {
                Budget = budget,
                Action = "Create",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-green-200",
                ButtonTheme = "bg-green-200 hover:bg-green-300"
            };
        }
        public static BudgetCrudViewModel Details(Budget budget)
        {
            return new BudgetCrudViewModel
            {
                Budget = budget,
                Action = "Details",
                ReadOnly = true,
                ShowAction = false,
                ActionTheme = "",
                ButtonTheme = ""
            };
        }

        public static BudgetCrudViewModel Edit (Budget budget)
        {
            return new BudgetCrudViewModel
            {
                Budget = budget,
                Action = "Edit",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-yellow-200",
                ButtonTheme = "bg-yellow-200 hover:bg-yellow-300"
            };
        }

        public static BudgetCrudViewModel Delete (Budget budget)
        {
            return new BudgetCrudViewModel
            {
                Budget = budget,
                Action = "Delete",
                ReadOnly = true,
                ShowAction = true,
                ActionTheme = "bg-red-200",
                ButtonTheme = "bg-red-200 hover:bg-red-300"               
            };
        }

        public static BudgetBreakdownViewModel Breakdown (Budget budget, IQueryable<IncomeCategory> incomeCategories, IQueryable<ExpenseCategory> expenseCategories)
        {
            return new BudgetBreakdownViewModel 
            {
                Budget = budget,
                ExpenseCategories = expenseCategories,
                IncomeCategories = incomeCategories
            };
        }

        public static BudgetTransactionsViewModel Transactions (Budget budget, IQueryable<IncomeItem> incomeItems, IQueryable<ExpenseItem> expenseItems)
        {
            return new BudgetTransactionsViewModel
            {
                Budget = budget,
                ExpenseItems = expenseItems,
                IncomeItems = incomeItems
                
            };
        }
    }
}