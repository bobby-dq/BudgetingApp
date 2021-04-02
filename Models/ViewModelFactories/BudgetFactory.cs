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
                ActionTheme = ""
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
                ActionTheme = ""
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
                ActionTheme = ""
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
                ActionTheme = ""                
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