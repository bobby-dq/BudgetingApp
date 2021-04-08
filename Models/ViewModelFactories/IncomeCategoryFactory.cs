using System.Linq;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;

namespace BudgetingApp.Models.ViewModelFactories
{
    public static class IncomeCategoryFactory
    {
        public static IncomeCategoryCrudViewModel Create (Budget budget, IncomeCategory incomeCategory)
        {
            return new IncomeCategoryCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                Action="Create",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-indigo-200",
                ButtonTheme = "bg-indigo-200 hover:bg-indigo-300"
            };
        }

        public static IncomeCategoryCrudViewModel Details (Budget budget, IncomeCategory incomeCategory)
        {
            return new IncomeCategoryCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                Action = "Details",
                ReadOnly = true,
                ShowAction = false,
                ActionTheme = "",
                ButtonTheme = ""
            };
        }

        public static IncomeCategoryCrudViewModel Edit (Budget budget, IncomeCategory incomeCategory)
        {
            return new IncomeCategoryCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                Action = "Edit",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-yellow-200",
                ButtonTheme = "bg-yellow-200 hover:bg-yellow-300"
            };
        }

        public static IncomeCategoryCrudViewModel Delete (Budget budget, IncomeCategory incomeCategory)
        {
            return new IncomeCategoryCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory, 
                Action = "Delete",
                ReadOnly = true,
                ShowAction = true,
                ActionTheme = "bg-red-200",
                ButtonTheme = "bg-red-200 hover:bg-red-300"
            };
        }

        public static IncomeCategoryBreakdownViewModel Breakdown (Budget budget, IncomeCategory incomeCategory, IQueryable<IncomeItem> incomeItems)
        {
            return new IncomeCategoryBreakdownViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                IncomeItems = incomeItems
            };
        }
    }
}