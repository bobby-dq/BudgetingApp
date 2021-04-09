using BudgetingApp.Models.BudgetingModels;

namespace BudgetingApp.Models.ViewModels
{
    public class ExpenseItemCrudViewModel
    {
        public Budget Budget {get; set;}
        public ExpenseCategory ExpenseCategory {get; set;}
        public ExpenseItem ExpenseItem {get; set;}
        public string Action {get; set;}
        public bool ReadOnly {get; set;}
        public string ObjectTheme {get; set;}
        public bool ShowAction {get; set;}
        public string ActionTheme {get;set;}
        public string ButtonTheme {get;set;}

    }
}