using System.Linq;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.ViewModels
{
    public class ExpenseCategoryBreakdownViewModel
    {
        public Budget Budget {get; set;}
        public ExpenseCategory ExpenseCategory {get; set;}
        public IQueryable<ExpenseItem> ExpenseItems {get; set;}
    }
}