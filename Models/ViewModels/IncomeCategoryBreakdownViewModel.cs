using System.Linq;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.ViewModels
{
    public class IncomeCategoryBreakdownViewModel
    {
        public Budget Budget {get; set;}
        public IncomeCategory IncomeCategory {get; set;}
        public IQueryable<IncomeItem> IncomeItems {get; set;}
    }
}