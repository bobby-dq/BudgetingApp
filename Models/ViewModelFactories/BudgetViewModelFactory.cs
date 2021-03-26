using System.Linq;
using BudgetingApp.Models.BudgetingModels;
using BudgetingApp.Models.ViewModels;

namespace BudgetingApp.Models.ViewModelFactories
{
    public static class BudgetViewModelFactory
    {
        public static BudgetViewModel 
        public static BudgetViewModel Create(Budget budget) 
        {
            return new BudgetViewModel
            {
                Budget = budget,
                Action = "create",
                ReadOnly = true,
                ObjectTheme = "",
                ShowAction = true,
                ActionTheme = ""
            };
        }

        
    }
}