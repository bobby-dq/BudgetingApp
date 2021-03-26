using System.Linq;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.ViewModels
{
    public class BudgetBreakdown
    {
        public Budget Budget {get; set;}
        public IQueryable<ExpenseCategory> ExpenseCategories {get; set;}
        public IQueryable<IncomeCategory> IncomeCategories {get; set;}
    }
}