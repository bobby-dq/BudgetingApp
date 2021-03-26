using System.Linq;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;

namespace BudgetingApp.Models.ViewModelFactories
{
    public static class BudgetFactory
    {
        public static BudgetCrud Create(Budget budget) 
        {
            return new BudgetCrud
            {
                Budget = budget,
                Action = "Create",
                ReadOnly = true,
                ShowAction = true,
                ActionTheme = ""
            };
        }
        public static BudgetCrud Details(Budget budget)
        {
            return new BudgetCrud
            {
                Budget = budget,
                Action = "Details",
                ReadOnly = true,
                ShowAction = false,
                ActionTheme = ""
            };
        }

        public static BudgetCrud Edit (Budget budget)
        {
            return new BudgetCrud
            {
                Budget = budget,
                Action = "Edit",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = ""
            };
        }

        public static BudgetCrud Delete (Budget budget)
        {
            return new BudgetCrud
            {
                Budget = budget,
                Action = "Delete",
                ReadOnly = true,
                ShowAction = true,
                ActionTheme = ""                
            };
        }

        public static BudgetBreakdown Breakdown (Budget budget, IQueryable<IncomeCategory> incomeCategories, IQueryable<ExpenseCategory> expenseCategories)
        {
            return new BudgetBreakdown 
            {
                Budget = budget,
                ExpenseCategories = expenseCategories,
                IncomeCategories = incomeCategories
            };
        }

        public static BudgetTransactions Transactions (Budget budget, IQueryable<IncomeItem> incomeItems, IQueryable<ExpenseItem> expenseItems)
        {
            return new BudgetTransactions
            {
                Budget = budget,
                ExpenseItems = expenseItems,
                IncomeItems = incomeItems
            };
        }
    }
}