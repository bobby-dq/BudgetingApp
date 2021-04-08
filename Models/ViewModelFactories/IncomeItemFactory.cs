using System.Linq;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;

namespace BudgetingApp.Models.ViewModelFactories
{
    public static class IncomeItemFactory
    {
        public static IncomeItemCrudViewModel Create (Budget budget, IncomeCategory incomeCategory, IncomeItem incomeItem)
        {
            return new IncomeItemCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                IncomeItem = incomeItem,
                Action="Create",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-indigo-200",
                ButtonTheme = "bg-indigo-200 hover:bg-indigo-300"
            };
        }

        public static IncomeItemCrudViewModel Details (Budget budget, IncomeCategory incomeCategory, IncomeItem incomeItem)
        {
            return new IncomeItemCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                IncomeItem = incomeItem,
                Action = "Details",
                ReadOnly = true,
                ShowAction = false,
                ActionTheme = "",
                ButtonTheme = ""
            };
        }

        public static IncomeItemCrudViewModel Edit (Budget budget, IncomeCategory incomeCategory, IncomeItem incomeItem)
        {
            return new IncomeItemCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                IncomeItem = incomeItem,
                Action = "Edit",
                ReadOnly = false,
                ShowAction = true,
                ActionTheme = "bg-yellow-200",
                ButtonTheme = "bg-yellow-200 hover:bg-yellow-300"
            };
        }

        public static IncomeItemCrudViewModel Delete (Budget budget, IncomeCategory incomeCategory, IncomeItem incomeItem)
        {
            return new IncomeItemCrudViewModel
            {
                Budget = budget,
                IncomeCategory = incomeCategory,
                IncomeItem = incomeItem, 
                Action = "Delete",
                ReadOnly = true,
                ShowAction = true,
                ActionTheme = "bg-red-200",
                ButtonTheme = "bg-red-200 hover:bg-red-300"
            };
        }
    }
}