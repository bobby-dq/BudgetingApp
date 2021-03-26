using System.Linq;
using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.ViewModels
{
    public class BudgetViewModel
    {
        public Budget Budget {get; set;}
        public string Action {get; set;}
        public bool ReadOnly {get; set;}
        public string ObjectTheme {get;set;}
        public bool ShowAction {get; set;}
        public string ActionTheme {get;set;}

        // These entities are for enumarble objects
        public IQueryable<ExpenseCategory> ExpenseCategories {get; set;}
        public IQueryable<ExpenseItem> ExpenseItems {get; set;}
        public IQueryable<IncomeCategory> IncomeCategories {get; set;}
        public IQueryable<IncomeItem> IncomeItems {get; set;}
    }
}