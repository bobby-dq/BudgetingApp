using System.Linq;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.ViewModels
{
    public class BudgetTransactions
    {
        public Budget Budget {get; set;}
        public IQueryable<ExpenseItem> ExpenseItems {get; set;}
        public IQueryable<IncomeItem> IncomeItems {get; set;}
    }
}